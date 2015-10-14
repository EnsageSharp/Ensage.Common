namespace Ensage.Common.Extensions
{
    using System.Linq;

    /// <summary>
    /// </summary>
    public static class AbilityExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Checks if given ability can be used
        /// </summary>
        /// <param name="ability"></param>
        /// <returns>returns true in case ability can be used</returns>
        public static bool CanBeCasted(this Ability ability)
        {
            return ability != null && ability.AbilityState == AbilityState.Ready && ability.Level > 0;
        }

        /// <summary>
        ///     Checks if given ability can be used
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <returns>returns true in case ability can be used</returns>
        public static bool CanBeCasted(this Ability ability, Unit target)
        {
            if (!target.IsValidTarget())
            {
                return false;
            }

            var canBeCasted = ability.CanBeCasted();
            if (!target.IsMagicImmune())
            {
                return canBeCasted;
            }

            var data = SpellDatabase.Find(ability.Name);
            return data == null ? canBeCasted : data.MagicImmunityPierce;
        }

        /// <summary>
        ///     Uses prediction to cast given skillshot ability
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <returns>returns true in case of successfull cast</returns>
        public static bool CastSkillShot(this Ability ability, Unit target)
        {
            var data = SpellDatabase.Find(ability.Name);
            var owner = ability.Owner as Unit;
            var delay = Game.Ping / 1000 + (float)owner.GetTurnTime(target);
            var speed = 0f;
            var radius = 0f;
            if (data != null)
            {
                if (data.AdditionalDelay > 0)
                {
                    delay += (float)data.AdditionalDelay;
                }
                if (data.Speed != null)
                {
                    speed = ability.AbilityData.FirstOrDefault(x => x.Name == data.Speed).Value;
                }
                if (data.Width != null)
                {
                    radius = ability.AbilityData.FirstOrDefault(x => x.Name == data.Width).Value;
                }
            }
            var xyz = Prediction.SkillShotXYZ(owner, target, delay, speed, radius);
            if (!(owner.Distance2D(xyz) <= (ability.CastRange + radius / 2)))
            {
                return false;
            }
            ability.UseAbility(xyz);
            return true;
        }

        /// <summary>
        ///     Uses given ability in case enemy is not disabled or would be chain stunned.
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <returns>returns true in case of successfull cast</returns>
        public static bool CastStun(this Ability ability, Unit target)
        {
            if (!ability.CanBeCasted())
            {
                return false;
            }
            var data = SpellDatabase.Find(ability.Name);
            var owner = ability.Owner;
            var delay = Game.Ping / 1000 + ability.GetCastPoint();
            var radius = 0f;
            if (!ability.AbilityBehavior.HasFlag(AbilityBehavior.NoTarget))
            {
                delay += (float)owner.GetTurnTime(target);
            }
            if (data != null)
            {
                if (data.AdditionalDelay > 0)
                {
                    delay += (float)data.AdditionalDelay;
                }
                if (data.Speed != null)
                {
                    var speed = ability.AbilityData.FirstOrDefault(x => x.Name == data.Speed)
                        .GetValue(ability.Level - 1);
                    delay += owner.Distance2D(target) / speed;
                }
                if (data.Radius != 0)
                {
                    radius = data.Radius;
                }
                else if (data.StringRadius != null)
                {
                    radius =
                        ability.AbilityData.FirstOrDefault(x => x.Name == data.StringRadius).GetValue(ability.Level - 1);
                }
                else if (data.Width != null)
                {
                    radius = ability.AbilityData.FirstOrDefault(x => x.Name == data.Width).GetValue(ability.Level - 1);
                }
            }
            var canUse = Utils.ChainStun(target, delay, null, false);
            if (!canUse)
            {
                return false;
            }
            if (ability.AbilityBehavior.HasFlag(AbilityBehavior.UnitTarget))
            {
                ability.UseAbility(target);
            }
            else if (ability.AbilityBehavior.HasFlag(AbilityBehavior.AreaOfEffect))
            {
                var pos = target.Position
                          + VectorExtensions.FromPolarCoordinates(1f, target.NetworkRotationRad).ToVector3()
                          * (float)(target.MovementSpeed * (delay));
                ability.UseAbility(pos);
            }
            else if (ability.AbilityBehavior.HasFlag(AbilityBehavior.NoTarget))
            {
                if (target.Distance2D(owner) > radius)
                {
                    return false;
                }
                ability.UseAbility();
            }
            Utils.Sleep(delay * 1000, "CHAINSTUN_SLEEP");
            return true;
        }

        public static double GetCastPoint(this Ability ability)
        {
            var castPoint = Game.FindKeyValues(ability.Name + "/AbilityCastPoint", KeyValueSource.Ability).FloatValue;
            return castPoint;
        }

        #endregion
    }
}