using System.Linq;
using Ensage.Common.Extensions;

namespace Ensage.Common
{
    using System;
    using System.Collections.Generic;

    public class Utils
    {
        #region Static Fields

        public static readonly Dictionary<string, double> Sleeps = new Dictionary<string, double>();

        #endregion

        #region Public Methods and Operators

        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static void Sleep(double duration, string name)
        {
            double dur;
            var tick = Environment.TickCount;
            if (!Sleeps.TryGetValue(name, out dur) || dur < tick + duration)
            {
                Sleeps[name] = tick + duration;
            }
        }

        public static bool SleepCheck(string name)
        {
            double asd;
            return !Sleeps.TryGetValue(name, out asd) || Environment.TickCount > asd;
        }

        public static bool ChainStun(Unit unit, double delay, string except, bool onlychain)
        {
            var chain = false;
            var stunned = false;
            string[] modifiersList =
            {
                "modifier_shadow_demon_disruption", "modifier_obsidian_destroyer_astral_imprisonment_prison",
                "modifier_eul_cyclone", "modifier_invoker_tornado", "modifier_bane_nightmare",
                "modifier_shadow_shaman_shackles",
                "modifier_crystal_maiden_frostbite", "modifier_ember_spirit_searing_chains",
                "modifier_axe_berserkers_call",
                "modifier_lone_druid_spirit_bear_entangle_effect", "modifier_meepo_earthbind",
                "modifier_naga_siren_ensnare",
                "modifier_storm_spirit_electric_vortex_pull", "modifier_treant_overgrowth", "modifier_cyclone",
                "modifier_sheepstick_debuff", "modifier_shadow_shaman_voodoo", "modifier_lion_voodoo",
                "modifier_brewmaster_storm_cyclone",
                "modifier_puck_phase_shift", "modifier_dark_troll_warlord_ensnare",
                "modifier_invoker_deafening_blast_knockback"
            };
            var modifiers = unit.Modifiers.OrderByDescending(x => x.RemainingTime);
            foreach (var m in modifiers.Where(m => (m.IsStunDebuff || modifiersList.Contains(m.Name)) && (except == null || m.Name == except)))
            {
                stunned = true;
                var remainingTime = m.RemainingTime;
                if (m.Name == "modifier_eul_cyclone" || m.Name == "modifier_invoker_tornado")
                    remainingTime += 0.07f;
                chain = remainingTime <= delay;
            }
            return ((((!(stunned || unit.IsStunned()) || chain) && !onlychain) || (onlychain && chain)));
        }

        #endregion
    }
}