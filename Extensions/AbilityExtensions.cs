using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensage.Common.Extensions
{
    public static class AbilityExtensions
    {
        /// <summary>
        /// Uses given ability in case enemy is not disabled or would be chain stunned.
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <returns>returns true in case of succesfull cast</returns>
        public static bool CastStun(this Ability ability, Unit target)
        {
            var data = SpellDatabase.Find(ability.Name);
            var owner = ability.Owner;
            var delay = Game.Ping;
            if (!ability.AbilityBehavior.HasFlag(AbilityBehavior.NoTarget))
                delay += (float) owner.GetTurnTime(target);
            if (data != null)
            {
                if (data.AdditionalDelay > 0)
                    delay += (float)data.AdditionalDelay;
                if (data.Speed != null)
                {
                    var speed = ability.AbilityData.FirstOrDefault(x => x.Name == data.Speed).Value;
                    delay += owner.Distance2D(target)/speed;
                } 
            }
            var canUse = Utils.ChainStun(target, delay, null, false);
            if (!canUse) return false;
            if (ability.AbilityBehavior.HasFlag(AbilityBehavior.UnitTarget))
                ability.UseAbility(target);
            else if (ability.AbilityBehavior.HasFlag(AbilityBehavior.AreaOfEffect))
                ability.UseAbility(target.Position);
            else if (ability.AbilityBehavior.HasFlag(AbilityBehavior.NoTarget))
                ability.UseAbility();
            return true;
        }

        /// <summary>
        /// Uses prediction to cast given skillshot ability
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="target"></param>
        /// <returns>returns true in case of successfull cast</returns>
        public static bool CastSkillShot(this Ability ability, Unit target)
        {
            var data = SpellDatabase.Find(ability.Name);
            var owner = ability.Owner as Unit;
            var delay = Game.Ping + (float)owner.GetTurnTime(target);
            var speed = 0f;
            var radius = 0f;
            if (data != null)
            {
                if (data.AdditionalDelay > 0)
                    delay += (float)data.AdditionalDelay;
                if (data.Speed != null)
                {
                    speed = ability.AbilityData.FirstOrDefault(x => x.Name == data.Speed).Value;
                }
                if (data.Width != null)
                {
                    radius = ability.AbilityData.FirstOrDefault(x => x.Name == data.Width).Value;
                }
            }
            var xyz = Prediction.SkillShotXYZ(owner, target, delay, speed,radius);
            if (!(owner.Distance2D(xyz) <= (ability.CastRange + radius/2))) return false;
            ability.UseAbility(xyz);
            return true;
        }
    }
}
