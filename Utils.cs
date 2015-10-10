namespace Ensage.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Extensions;

    /// <summary>
    ///     Utility methods
    /// </summary>
    public class Utils
    {
        #region Static Fields

        /// <summary>
        ///     Stores sleep values
        /// </summary>
        public static readonly Dictionary<string, double> Sleeps = new Dictionary<string, double>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Switches given degrees to radians
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        /// <summary>
        ///     Checks if given unit wont be stunned after given delay in seconds.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="delay">Delay of possible stun in seconds</param>
        /// <param name="except">Entering a modifier name will ignore that modifier</param>
        /// <param name="onlychain">Entering true will make the function return true only in case enemy is already stunned</param>
        /// <returns></returns>
        public static bool ChainStun(Unit unit, double delay, string except, bool onlychain)
        {
            var chain = false;
            var stunned = false;
            string[] modifiersList =
                {
                    "modifier_shadow_demon_disruption",
                    "modifier_obsidian_destroyer_astral_imprisonment_prison", "modifier_eul_cyclone",
                    "modifier_invoker_tornado", "modifier_bane_nightmare",
                    "modifier_shadow_shaman_shackles", "modifier_crystal_maiden_frostbite",
                    "modifier_ember_spirit_searing_chains", "modifier_axe_berserkers_call",
                    "modifier_lone_druid_spirit_bear_entangle_effect", "modifier_meepo_earthbind",
                    "modifier_naga_siren_ensnare", "modifier_storm_spirit_electric_vortex_pull",
                    "modifier_treant_overgrowth", "modifier_cyclone", "modifier_sheepstick_debuff",
                    "modifier_shadow_shaman_voodoo", "modifier_lion_voodoo",
                    "modifier_brewmaster_storm_cyclone", "modifier_puck_phase_shift",
                    "modifier_dark_troll_warlord_ensnare",
                    "modifier_invoker_deafening_blast_knockback"
                };
            var modifiers = unit.Modifiers.OrderByDescending(x => x.RemainingTime);
            foreach (
                var m in
                    modifiers.Where(
                        m => (m.IsStunDebuff || modifiersList.Contains(m.Name)) && (except == null || m.Name == except))
                )
            {
                stunned = true;
                var remainingTime = m.RemainingTime;
                if (m.Name == "modifier_eul_cyclone" || m.Name == "modifier_invoker_tornado")
                {
                    remainingTime += 0.07f;
                }
                chain = remainingTime <= delay;
            }
            return ((((!(stunned || unit.IsStunned()) || chain) && !onlychain) || (onlychain && chain)));
        }

        /// <summary>
        ///     Sleeps the sleeping engine with the given id for given miliseconds. If engine is already sleeping for more than the
        ///     given time it will be ignored.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="name"></param>
        public static void Sleep(double duration, string name)
        {
            double dur;
            var tick = Environment.TickCount;
            if (!Sleeps.TryGetValue(name, out dur) || dur < tick + duration)
            {
                Sleeps[name] = tick + duration;
            }
        }

        /// <summary>
        ///     Checks sleeping status of the sleep engine with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true in case id was not found or is not sleeping</returns>
        public static bool SleepCheck(string id)
        {
            double asd;
            return !Sleeps.TryGetValue(id, out asd) || Environment.TickCount > asd;
        }

        #endregion
    }
}