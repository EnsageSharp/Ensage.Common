namespace Ensage.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.AbilityInfo;

    using global::SharpDX;

    /// <summary>
    /// </summary>
    public static class AbilityExtensions
    {
        #region Static Fields

        /// <summary>
        ///     Temporarily stores cast delay values
        /// </summary>
        public static Dictionary<string, double> DelayDictionary = new Dictionary<string, double>();

        /// <summary>
        ///     Temporarily stores radius values
        /// </summary>
        public static Dictionary<string, float> RadiusDictionary = new Dictionary<string, float>();

        /// <summary>
        ///     Temporarily stores speed values
        /// </summary>
        public static Dictionary<string, float> SpeedDictionary = new Dictionary<string, float>();

        private static readonly Dictionary<string, double> CastPointDictionary = new Dictionary<string, double>();

        private static readonly Dictionary<string, AbilityData> DataDictionary = new Dictionary<string, AbilityData>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Checks if given ability can be used
        /// </summary>
        /// <param name="ability"></param>
        /// <returns>returns true in case ability can be used</returns>
        public static bool CanBeCasted(this Ability ability)
        {
            //Console.WriteLine((ability == null) + " " + ability.Level + " " + ability.Cooldown + " " + ((ability.Owner as Hero) == null));
            try
            {
                var owner = ability.Owner as Hero;
                if (owner == null)
                {
                    return ability.Level > 0 && ability.Cooldown <= 0;
                }
                if (ability is Item || owner.ClassID != ClassID.CDOTA_Unit_Hero_Invoker)
                {
                    return ability.AbilityState == AbilityState.Ready && ability.Level > 0;
                }
                var spell4 = owner.Spellbook.Spell4;
                var spell5 = owner.Spellbook.Spell5;
                if (ability.Name != "invoker_invoke" && ability.Name != "invoker_quas" && ability.Name != "invoker_wex"
                    && ability.Name != "invoker_exort" && !ability.Equals(spell4) && !ability.Equals(spell5))
                {
                    return false;
                }
                return ability.AbilityState == AbilityState.Ready && ability.Level > 0;
            }
            catch (Exception)
            {
                return false;
            }
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

            AbilityInfo data;
            if (!AbilityDamage.DataDictionary.TryGetValue(ability, out data))
            {
                data = AbilityDatabase.Find(ability.Name);
                AbilityDamage.DataDictionary.Add(ability, data);
            }
            return data == null ? canBeCasted : data.MagicImmunityPierce;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool CanHit(this Ability ability, Hero target)
        {
            return CanHit(ability,target,target.Position);
        }

        /// <summary>
        ///     Checks if you could hit hero with given ability
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <param name="sourcePosition"></param>
        /// <returns></returns>
        public static bool CanHit(this Ability ability, Hero target, Vector3 sourcePosition)
        {
            var position = sourcePosition;
            if (ability.AbilityBehavior.HasFlag(AbilityBehavior.Point)
                || ability.AbilityBehavior.HasFlag(AbilityBehavior.NoTarget))
            {
                var pred = ability.GetPrediction(target);
                if (position.Distance2D(pred) <= (ability.GetCastRange() + ability.GetRadius()))
                {
                    return true;
                }
                return false;
            }
            if (ability.AbilityBehavior.HasFlag(AbilityBehavior.UnitTarget))
            {
                if (position.Distance2D(target.Position) <= ability.GetCastRange())
                {
                    return true;
                }
                if (ability.Name == "pudge_dismember" && target.Modifiers.Any(x => x.Name == "modifier_pudge_meat_hook") && position.Distance2D(target) < 600)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool CastSkillShot(this Ability ability, Unit target)
        {
            return CastSkillShot(ability,target,ability.Owner.Position);
        }

        /// <summary>
        ///     Uses prediction to cast given skillshot ability
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <param name="sourcePosition"></param>
        /// <returns>returns true in case of successfull cast</returns>
        public static bool CastSkillShot(this Ability ability, Unit target, Vector3 sourcePosition)
        {
            var owner = ability.Owner as Unit;
            var position = sourcePosition;
            var xyz = ability.GetPrediction(target);
            var radius = ability.GetRadius();
            var delay = ability.GetCastDelay(owner as Hero, target, true);
            var speed = ability.GetProjectileSpeed();
            if (!(position.Distance2D(xyz) <= (ability.GetCastRange() + radius)))
            {
                return false;
            }
           // Console.WriteLine(ability.GetCastRange() + " " + radius);
            if (ability.Name.Substring(0, Math.Min("nevermore_shadowraze".Length,ability.Name.Length)) == "nevermore_shadowraze")
            {
                xyz = Prediction.SkillShotXYZ(
                    owner,
                    target,
                    (float)((delay + (float)owner.GetTurnTime(xyz)) * 1000),
                    speed,
                    radius);
                if (position.Distance2D(xyz) < (ability.GetCastRange() + radius / 2)
                    && position.Distance2D(xyz) > (ability.GetCastRange() - radius / 2))
                {
                    owner.Move((position - xyz) * 10 / position.Distance2D(xyz) + xyz);
                    owner.Stop();
                    ability.UseAbility();
                    return true;
                }
                return false;
            }
            ability.UseAbility(xyz);
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <param name="straightTimeforSkillShot"></param>
        /// <param name="chainStun"></param>
        /// <returns></returns>
        public static bool CastStun(
            this Ability ability,
            Unit target,
            float straightTimeforSkillShot = 0,
            bool chainStun = true)
        {
            return CastStun(ability,target,ability.Owner.Position,straightTimeforSkillShot,chainStun);
        }

        /// <summary>
        ///     Uses given ability in case enemy is not disabled or would be chain stunned.
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <param name="sourcePosition"></param>
        /// <param name="straightTimeforSkillShot"></param>
        /// <param name="chainStun"></param>
        /// <returns>returns true in case of successfull cast</returns>
        public static bool CastStun(
            this Ability ability,
            Unit target, Vector3 sourcePosition,
            float straightTimeforSkillShot = 0,
            bool chainStun = true)
        {
            var owner = ability.Owner as Unit;
            var position = owner.Position;
            if (sourcePosition != Vector3.Zero)
            {
                position = sourcePosition;
            }
            if (!ability.CanBeCasted())
            {
                return false;
            }
            var delay = ability.GetHitDelay(target);
            var radius = ability.GetRadius();
            var canUse = Utils.ChainStun(target, delay, null, false);
            if (!canUse && chainStun)
            {
                return false;
            }
            if (ability.AbilityBehavior.HasFlag(AbilityBehavior.UnitTarget))
            {
                ability.UseAbility(target);
            }
            else if ((ability.AbilityBehavior.HasFlag(AbilityBehavior.AreaOfEffect)
                      || ability.AbilityBehavior.HasFlag(AbilityBehavior.Point)))
            {
                if (Prediction.StraightTime(target) > straightTimeforSkillShot * 1000
                    && ability.CastSkillShot(target))
                {
                    Utils.Sleep(delay * 1000 + 100, "CHAINSTUN_SLEEP");
                    return true;
                }
                return false;
            }
            else if (ability.AbilityBehavior.HasFlag(AbilityBehavior.NoTarget))
            {
                if (target.Distance2D(position) > radius)
                {
                    return false;
                }
                ability.UseAbility();
            }
            Utils.Sleep(delay * 1000 + 100, "CHAINSTUN_SLEEP");
            return true;
        }

        /// <summary>
        ///     Returns castpoint of given ability
        /// </summary>
        /// <param name="ability"></param>
        /// <returns></returns>
        public static double FindCastPoint(this Ability ability)
        {
            if (ability is Item)
            {
                return 0;
            }
            if (ability.OverrideCastPoint != -1)
            {
                return 0.1;
            }

            double castPoint;
            if (CastPointDictionary.TryGetValue(ability.Name + " " + ability.Level, out castPoint))
            {
                return castPoint;
            }
            castPoint = ability.GetCastPoint(ability.Level);
            CastPointDictionary.Add(ability.Name + " " + ability.Level, castPoint);
            return castPoint;
        }

        /// <summary>
        ///     Returns ability data with given name, checks if data are level dependent or not
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="dataName"></param>
        /// <param name="level">Custom level</param>
        /// <returns></returns>
        public static float GetAbilityData(this Ability ability, string dataName, uint level = 0)
        {
            var lvl = ability.Level;
            AbilityData data;
            if (!DataDictionary.TryGetValue(ability.Name + "_" + dataName, out data))
            {
                data = ability.AbilityData.FirstOrDefault(x => x.Name == dataName);
                DataDictionary.Add(ability.Name + "_" + dataName, data);
            }
            if (level > 0)
            {
                lvl = level;
            }
            if (data == null)
            {
                return 0;
            }
            return data.Count > 1 ? data.GetValue(lvl - 1) : data.Value;
        }

        /// <summary>
        ///     Returns delay before ability is casted
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="usePing"></param>
        /// <returns></returns>
        public static double GetCastDelay(this Ability ability, Hero source, Unit target, bool usePing = false, bool useCastPoint = true)
        {
            double delay;
            if (useCastPoint)
            {
                if (!DelayDictionary.TryGetValue(ability.Name + " " + ability.Level, out delay))
                {
                    delay = Math.Max(ability.FindCastPoint(), 0.05);
                    AbilityInfo data;
                    if (!AbilityDamage.DataDictionary.TryGetValue(ability, out data))
                    {
                        data = AbilityDatabase.Find(ability.Name);
                        AbilityDamage.DataDictionary.Add(ability, data);
                    }
                    delay += data.AdditionalDelay;
                    DelayDictionary.Add(ability.Name + " " + ability.Level, delay);
                }
                if ((ability.Name == "item_diffusal_blade" || ability.Name == "item_diffusal_blade_2"))
                {
                    delay += 2;
                }
            }
            else
            {
                delay = 0.01;
            }
            if (usePing)
            {
                delay += Game.Ping / 1000;
            }
            //Console.WriteLine(ability.GetChannelTime(ability.Level - 1) + "  " + delay + " " + ability.Name);
            delay += ability.GetChannelTime(ability.Level - 1);
            if (!ability.AbilityBehavior.HasFlag(AbilityBehavior.NoTarget))
            {
                return delay + (useCastPoint ? source.GetTurnTime(target) : source.GetTurnTime(target)/2);
            }
            return delay;
        }

        /// <summary>
        ///     Returns cast range of ability, if ability is NonTargeted it will return its radius!
        /// </summary>
        /// <param name="ability"></param>
        /// <returns></returns>
        public static float GetCastRange(this Ability ability)
        {
            if (ability.Name == "templar_assassin_meld")
            {
                return (ability.Owner as Hero).GetAttackRange() + 50;
            }
            if (!ability.AbilityBehavior.HasFlag(AbilityBehavior.NoTarget))
            {
                var castRange = ability.CastRange;
                var bonusRange = 0;
                if (castRange <= 0)
                {
                    castRange = 999999;
                }
                if (ability.Name == "dragon_knight_dragon_tail"
                    && (ability.Owner as Hero).Modifiers.Any(x => x.Name == "modifier_dragon_knight_dragon_form"))
                {
                    bonusRange = 250;
                }
                else if (ability.Name == "beastmaster_primal_roar" && (ability.Owner as Hero).AghanimState())
                {
                    bonusRange = 350;
                }
                return castRange + bonusRange + 100;
            }
            var radius = 0f;
            AbilityInfo data;
            if (!AbilityDamage.DataDictionary.TryGetValue(ability, out data))
            {
                data = AbilityDatabase.Find(ability.Name);
                AbilityDamage.DataDictionary.Add(ability, data);
            }
            if (data == null)
            {
                return ability.CastRange;
            }
            if (!data.FakeCastRange)
            {
                if (data.Width != null)
                {
                    radius = ability.GetAbilityData(data.Width);
                }
                if (data.StringRadius != null)
                {
                    radius = ability.GetAbilityData(data.StringRadius);
                }
                if (data.Radius > 0)
                {
                    radius = data.Radius;
                }
            }
            else
            {
                radius = ability.GetAbilityData(data.RealCastRange);
            }
            return radius + 50;
        }

        /// <summary>
        ///     Checks all aspects and returns full delay before target gets hit by given ability
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static double GetHitDelay(this Ability ability, Unit target)
        {
            AbilityInfo data;
            if (!AbilityDamage.DataDictionary.TryGetValue(ability, out data))
            {
                data = AbilityDatabase.Find(ability.Name);
                AbilityDamage.DataDictionary.Add(ability, data);
            }
            var owner = ability.Owner as Unit;
            var delay = ability.GetCastDelay(owner as Hero, target, true);
            var speed = ability.GetProjectileSpeed();
            var radius = ability.GetRadius();
            if (!ability.AbilityBehavior.HasFlag(AbilityBehavior.NoTarget) && speed < 6000 && speed > 0)
            {
                var xyz = ability.GetPrediction(target);
                delay += (Math.Max((owner.Distance2D(xyz) - radius / 2), 100) / speed);
            }
            return delay;
        }

        /// <summary>
        ///     Returns prediction for given target after given ability hit delay
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <param name="customDelay">enter your custom delay</param>
        /// <returns></returns>
        public static Vector3 GetPrediction(this Ability ability, Unit target, double customDelay = 0)
        {
            AbilityInfo data;
            if (!AbilityDamage.DataDictionary.TryGetValue(ability, out data))
            {
                data = AbilityDatabase.Find(ability.Name);
                AbilityDamage.DataDictionary.Add(ability, data);
            }
            var owner = ability.Owner as Unit;
            var delay = ability.GetCastDelay(owner as Hero, target, true);
            var speed = ability.GetProjectileSpeed();
            var radius = ability.GetRadius();
            var xyz = Prediction.SkillShotXYZ(owner, target, (float)((delay + owner.GetTurnTime(target.Position)) * 1000), speed, radius);
            if (!ability.AbilityBehavior.HasFlag(AbilityBehavior.NoTarget))
            {
                xyz = Prediction.SkillShotXYZ(
                    owner,
                    target,
                    (float)((delay + (float)owner.GetTurnTime(xyz)) * 1000),
                    speed,
                    radius);
            }
            return xyz;
        }

        /// <summary>
        ///     Returns projectile speed of the ability
        /// </summary>
        /// <param name="ability"></param>
        /// <returns></returns>
        public static float GetProjectileSpeed(this Ability ability)
        {
            float speed;
            if (!SpeedDictionary.TryGetValue(ability.Name + " " + ability.Level, out speed))
            {
                AbilityInfo data;
                if (!AbilityDamage.DataDictionary.TryGetValue(ability, out data))
                {
                    data = AbilityDatabase.Find(ability.Name);
                    AbilityDamage.DataDictionary.Add(ability, data);
                }
                if (data == null)
                {
                    speed = float.MaxValue;
                    SpeedDictionary.Add(ability.Name + " " + ability.Level, speed);
                    return speed;
                }
                if (data.Speed != null)
                {
                    speed = ability.GetAbilityData(data.Speed);
                    SpeedDictionary.Add(ability.Name + " " + ability.Level, speed);
                }
            }
            return speed;
        }

        /// <summary>
        ///     Returns impact radius of given ability
        /// </summary>
        /// <param name="ability"></param>
        /// <returns></returns>
        public static float GetRadius(this Ability ability)
        {
            float radius;
            if (!RadiusDictionary.TryGetValue(ability.Name + " " + ability.Level, out radius))
            {
                AbilityInfo data;
                if (!AbilityDamage.DataDictionary.TryGetValue(ability, out data))
                {
                    data = AbilityDatabase.Find(ability.Name);
                    AbilityDamage.DataDictionary.Add(ability, data);
                }
                if (data == null)
                {
                    radius = float.MaxValue;
                    RadiusDictionary.Add(ability.Name + " " + ability.Level, radius);
                    return radius;
                }
                if (data.Width != null)
                {
                    radius = ability.GetAbilityData(data.Width);
                    RadiusDictionary.Add(ability.Name + " " + ability.Level, radius);
                    return radius;
                }
                else if (data.StringRadius != null)
                {
                    radius = ability.GetAbilityData(data.StringRadius);
                    RadiusDictionary.Add(ability.Name + " " + ability.Level, radius);
                    return radius;
                }
                else if (data.Radius > 0)
                {
                    radius = data.Radius;
                    RadiusDictionary.Add(ability.Name + " " + ability.Level, radius);
                    return radius;
                }
            }
            return radius;
        }

        /// <summary>
        ///     Checks if this ability can be casted by Invoker, if the ability is not currently invoked, it is gonna check for
        ///     both invoke and the ability manacost.
        /// </summary>
        /// <param name="ability">given ability</param>
        /// <param name="invoke">invoker ultimate</param>
        /// <param name="spell4">current spell on slot 4</param>
        /// <param name="spell5">current spell on slot 5</param>
        /// <returns></returns>
        public static bool InvoCanBeCasted(this Ability ability, Ability invoke, Ability spell4, Ability spell5)
        {
            var owner = ability.Owner as Hero;
            if (owner == null)
            {
                return false;
            }
            if (!(ability is Item) && ability.Name != "invoker_invoke" && ability.Name != "invoker_quas"
                && ability.Name != "invoker_wex" && ability.Name != "invoker_exort" && !ability.Equals(spell4)
                && !ability.Equals(spell5))
            {
                return invoke.Level > 0 && invoke.Cooldown <= 0 && ability.Cooldown <= 0
                       && (ability.ManaCost + invoke.ManaCost) <= owner.Mana;
            }
            return ability.AbilityState == AbilityState.Ready;
        }

        #endregion
    }
}