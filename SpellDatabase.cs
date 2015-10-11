namespace Ensage.Common
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// </summary>
    public static class SpellDatabase
    {
        #region Static Fields

        /// <summary>
        /// </summary>
        public static List<SpellData> Spells = new List<SpellData>();

        #endregion

        #region Constructors and Destructors

        static SpellDatabase()
        {
            #region dark_seer_ion_shell

            Spells.Add(
                new SpellData
                    {
                        SpellName = "dark_seer_ion_shell", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region magnataur_shockwave

            Spells.Add(
                new SpellData
                    {
                        SpellName = "magnataur_shockwave", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "shock_speed", Width = "shock_width", AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region ursa_earthshock

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ursa_earthshock", IsDisable = false, IsSlow = true, IsSilence = false, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = "shock_radius", Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region chaos_knight_reality_rift

            Spells.Add(
                new SpellData
                    {
                        SpellName = "chaos_knight_reality_rift", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region skywrath_mage_mystic_flare

            Spells.Add(
                new SpellData
                    {
                        SpellName = "skywrath_mage_mystic_flare", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0.2,
                        Radius = 0, StringRadius = null, Speed = null, Width = "radius", AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region death_prophet_carrion_swarm

            Spells.Add(
                new SpellData
                    {
                        SpellName = "death_prophet_carrion_swarm", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region zuus_lightning_bolt

            Spells.Add(
                new SpellData
                    {
                        SpellName = "zuus_lightning_bolt", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "spread_aoe", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region axe_berserkers_call

            Spells.Add(
                new SpellData
                    {
                        SpellName = "axe_berserkers_call", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region beastmaster_wild_axes

            Spells.Add(
                new SpellData
                    {
                        SpellName = "beastmaster_wild_axes", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region puck_waning_rift

            Spells.Add(
                new SpellData
                    {
                        SpellName = "puck_waning_rift", IsDisable = false, IsSlow = false, IsSilence = true, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = "radius", Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region lone_druid_rabid

            Spells.Add(
                new SpellData
                    {
                        SpellName = "lone_druid_rabid", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region rattletrap_battery_assault

            Spells.Add(
                new SpellData
                    {
                        SpellName = "rattletrap_battery_assault", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0.7,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region sven_storm_bolt

            Spells.Add(
                new SpellData
                    {
                        SpellName = "sven_storm_bolt", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = "bolt_speed", Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region chen_test_of_faith

            Spells.Add(
                new SpellData
                    {
                        SpellName = "chen_test_of_faith", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region phantom_assassin_stifling_dagger

            Spells.Add(
                new SpellData
                    {
                        SpellName = "phantom_assassin_stifling_dagger", IsDisable = false, IsSlow = true,
                        IsSilence = false, IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false,
                        AdditionalDelay = 0, Radius = 0, StringRadius = null, Speed = "dagger_speed", Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region bounty_hunter_track

            Spells.Add(
                new SpellData
                    {
                        SpellName = "bounty_hunter_track", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region axe_battle_hunger

            Spells.Add(
                new SpellData
                    {
                        SpellName = "axe_battle_hunger", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region storm_spirit_electric_vortex

            Spells.Add(
                new SpellData
                    {
                        SpellName = "storm_spirit_electric_vortex", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region ember_spirit_sleight_of_fist

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ember_spirit_sleight_of_fist", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region batrider_flamebreak

            Spells.Add(
                new SpellData
                    {
                        SpellName = "batrider_flamebreak", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0.3,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region mirana_arrow

            Spells.Add(
                new SpellData
                    {
                        SpellName = "mirana_arrow", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = "arrow_speed", Width = "arrow_width", AllyBlock = true,
                        EnemyBlock = true, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region abaddon_aphotic_shield

            Spells.Add(
                new SpellData
                    {
                        SpellName = "abaddon_aphotic_shield", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = true, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region windrunner_shackleshot

            Spells.Add(
                new SpellData
                    {
                        SpellName = "windrunner_shackleshot", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "arrow_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region ancient_apparition_cold_feet

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ancient_apparition_cold_feet", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 2.01,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region rattletrap_hookshot

            Spells.Add(
                new SpellData
                    {
                        SpellName = "rattletrap_hookshot", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = "latch_radius", AllyBlock = true,
                        EnemyBlock = true, MagicImmunityPierce = true, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region shadow_shaman_mass_serpent_ward

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shadow_shaman_mass_serpent_ward", IsDisable = false, IsSlow = false,
                        IsSilence = false, IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false,
                        AdditionalDelay = 0, Radius = 0, StringRadius = null, Speed = null, Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region ogre_magi_unrefined_fireblast

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ogre_magi_unrefined_fireblast", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region oracle_fortunes_end

            Spells.Add(
                new SpellData
                    {
                        SpellName = "oracle_fortunes_end", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "bolt_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region shredder_timber_chain

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shredder_timber_chain", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "chain_radius", Speed = "speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region doom_bringer_lvl_death

            Spells.Add(
                new SpellData
                    {
                        SpellName = "doom_bringer_lvl_death", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region crystal_maiden_freezing_field

            Spells.Add(
                new SpellData
                    {
                        SpellName = "crystal_maiden_freezing_field", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = -0.1,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region lich_frost_armor

            Spells.Add(
                new SpellData
                    {
                        SpellName = "lich_frost_armor", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = true, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region enigma_malefice

            Spells.Add(
                new SpellData
                    {
                        SpellName = "enigma_malefice", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region elder_titan_earth_splitter

            Spells.Add(
                new SpellData
                    {
                        SpellName = "elder_titan_earth_splitter", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region shredder_return_chakram_2

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shredder_return_chakram_2", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region zuus_thundergods_wrath

            Spells.Add(
                new SpellData
                    {
                        SpellName = "zuus_thundergods_wrath", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = -0.1,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region shredder_chakram_2

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shredder_chakram_2", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0.5,
                        Radius = 0, StringRadius = "radius", Speed = "speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region phantom_assassin_phantom_strike

            Spells.Add(
                new SpellData
                    {
                        SpellName = "phantom_assassin_phantom_strike", IsDisable = false, IsSlow = false,
                        IsSilence = false, IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false,
                        AdditionalDelay = 0, Radius = 0, StringRadius = null, Speed = null, Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region pugna_decrepify

            Spells.Add(
                new SpellData
                    {
                        SpellName = "pugna_decrepify", IsDisable = false, IsSlow = true, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region omniknight_purification

            Spells.Add(
                new SpellData
                    {
                        SpellName = "omniknight_purification", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = true, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region keeper_of_the_light_blinding_light

            Spells.Add(
                new SpellData
                    {
                        SpellName = "keeper_of_the_light_blinding_light", IsDisable = false, IsSlow = false,
                        IsSilence = false, IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false,
                        AdditionalDelay = 0, Radius = 0, StringRadius = null, Speed = null, Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region queenofpain_shadow_strike

            Spells.Add(
                new SpellData
                    {
                        SpellName = "queenofpain_shadow_strike", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "projectile_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region shredder_chakram

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shredder_chakram", IsDisable = false, IsSlow = true, IsSilence = false, IsNuke = true,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0.5, Radius = 0,
                        StringRadius = "radius", Speed = "speed", Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region undying_soul_rip

            Spells.Add(
                new SpellData
                    {
                        SpellName = "undying_soul_rip", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region invoker_sun_strike

            Spells.Add(
                new SpellData
                    {
                        SpellName = "invoker_sun_strike", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 1.7,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region winter_wyvern_splinter_blast

            Spells.Add(
                new SpellData
                    {
                        SpellName = "winter_wyvern_splinter_blast", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region dark_seer_surge

            Spells.Add(
                new SpellData
                    {
                        SpellName = "dark_seer_surge", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = true, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region mirana_starfall

            Spells.Add(
                new SpellData
                    {
                        SpellName = "mirana_starfall", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 400,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region luna_lucent_beam

            Spells.Add(
                new SpellData
                    {
                        SpellName = "luna_lucent_beam", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region nyx_assassin_mana_burn

            Spells.Add(
                new SpellData
                    {
                        SpellName = "nyx_assassin_mana_burn", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region dark_seer_vacuum

            Spells.Add(
                new SpellData
                    {
                        SpellName = "dark_seer_vacuum", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region rattletrap_power_cogs

            Spells.Add(
                new SpellData
                    {
                        SpellName = "rattletrap_power_cogs", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0.1,
                        Radius = 125, StringRadius = null, Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region dark_seer_wall_of_replica

            Spells.Add(
                new SpellData
                    {
                        SpellName = "dark_seer_wall_of_replica", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region ogre_magi_fireblast

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ogre_magi_fireblast", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region riki_blink_strike

            Spells.Add(
                new SpellData
                    {
                        SpellName = "riki_blink_strike", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region invoker_cold_snap

            Spells.Add(
                new SpellData
                    {
                        SpellName = "invoker_cold_snap", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region wisp_spirits

            Spells.Add(
                new SpellData
                    {
                        SpellName = "wisp_spirits", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 1300,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region warlock_fatal_bonds

            Spells.Add(
                new SpellData
                    {
                        SpellName = "warlock_fatal_bonds", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region oracle_purifying_flames

            Spells.Add(
                new SpellData
                    {
                        SpellName = "oracle_purifying_flames", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = true
                    });

            #endregion

            #region sandking_burrowstrike

            Spells.Add(
                new SpellData
                    {
                        SpellName = "sandking_burrowstrike", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "burrow_speed", Width = "burrow_width", AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region pudge_meat_hook

            Spells.Add(
                new SpellData
                    {
                        SpellName = "pudge_meat_hook", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = "hook_speed", Width = "hook_width", AllyBlock = true,
                        EnemyBlock = true, MagicImmunityPierce = true, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region keeper_of_the_light_mana_leak

            Spells.Add(
                new SpellData
                    {
                        SpellName = "keeper_of_the_light_mana_leak", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region keeper_of_the_light_spirit_form_illuminate

            Spells.Add(
                new SpellData
                    {
                        SpellName = "keeper_of_the_light_spirit_form_illuminate", IsDisable = false, IsSlow = false,
                        IsSilence = false, IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false,
                        AdditionalDelay = 0, Radius = 0, StringRadius = null, Speed = null, Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region templar_assassin_meld

            Spells.Add(
                new SpellData
                    {
                        SpellName = "templar_assassin_meld", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region disruptor_glimpse

            Spells.Add(
                new SpellData
                    {
                        SpellName = "disruptor_glimpse", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region disruptor_thunder_strike

            Spells.Add(
                new SpellData
                    {
                        SpellName = "disruptor_thunder_strike", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region disruptor_kinetic_field

            Spells.Add(
                new SpellData
                    {
                        SpellName = "disruptor_kinetic_field", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 1.2,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region ember_spirit_searing_chains

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ember_spirit_searing_chains", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region silencer_curse_of_the_silent

            Spells.Add(
                new SpellData
                    {
                        SpellName = "silencer_curse_of_the_silent", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region venomancer_plague_ward

            Spells.Add(
                new SpellData
                    {
                        SpellName = "venomancer_plague_ward", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region phoenix_icarus_dive_stop

            Spells.Add(
                new SpellData
                    {
                        SpellName = "phoenix_icarus_dive_stop", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region phoenix_icarus_dive

            Spells.Add(
                new SpellData
                    {
                        SpellName = "phoenix_icarus_dive", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region puck_ethereal_jaunt

            Spells.Add(
                new SpellData
                    {
                        SpellName = "puck_ethereal_jaunt", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region enchantress_enchant

            Spells.Add(
                new SpellData
                    {
                        SpellName = "enchantress_enchant", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region treant_leech_seed

            Spells.Add(
                new SpellData
                    {
                        SpellName = "treant_leech_seed", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region phoenix_launch_fire_spirit

            Spells.Add(
                new SpellData
                    {
                        SpellName = "phoenix_launch_fire_spirit", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "spirit_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region nyx_assassin_impale

            Spells.Add(
                new SpellData
                    {
                        SpellName = "nyx_assassin_impale", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region tidehunter_gush

            Spells.Add(
                new SpellData
                    {
                        SpellName = "tidehunter_gush", IsDisable = false, IsSlow = true, IsSilence = false, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = "projectile_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region shadow_demon_demonic_purge

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shadow_demon_demonic_purge", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region puck_illusory_orb

            Spells.Add(
                new SpellData
                    {
                        SpellName = "puck_illusory_orb", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "orb_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region legion_commander_duel

            Spells.Add(
                new SpellData
                    {
                        SpellName = "legion_commander_duel", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region jakiro_dual_breath

            Spells.Add(
                new SpellData
                    {
                        SpellName = "jakiro_dual_breath", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region lina_light_strike_array

            Spells.Add(
                new SpellData
                    {
                        SpellName = "lina_light_strike_array", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0.5,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region clinkz_strafe

            Spells.Add(
                new SpellData
                    {
                        SpellName = "clinkz_strafe", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 630,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region shadow_demon_disruption

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shadow_demon_disruption", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = true, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region slardar_slithereen_crush

            Spells.Add(
                new SpellData
                    {
                        SpellName = "slardar_slithereen_crush", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "crush_radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region terrorblade_conjure_image

            Spells.Add(
                new SpellData
                    {
                        SpellName = "terrorblade_conjure_image", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region terrorblade_metamorphosis

            Spells.Add(
                new SpellData
                    {
                        SpellName = "terrorblade_metamorphosis", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 700, StringRadius = null, Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region slark_dark_pact

            Spells.Add(
                new SpellData
                    {
                        SpellName = "slark_dark_pact", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region terrorblade_reflection

            Spells.Add(
                new SpellData
                    {
                        SpellName = "terrorblade_reflection", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region shadow_demon_soul_catcher

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shadow_demon_soul_catcher", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region terrorblade_sunder

            Spells.Add(
                new SpellData
                    {
                        SpellName = "terrorblade_sunder", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = true, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region bloodseeker_rupture

            Spells.Add(
                new SpellData
                    {
                        SpellName = "bloodseeker_rupture", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = true, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region undying_tombstone

            Spells.Add(
                new SpellData
                    {
                        SpellName = "undying_tombstone", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region undying_decay

            Spells.Add(
                new SpellData
                    {
                        SpellName = "undying_decay", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region gyrocopter_flak_cannon

            Spells.Add(
                new SpellData
                    {
                        SpellName = "gyrocopter_flak_cannon", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region spectre_spectral_dagger

            Spells.Add(
                new SpellData
                    {
                        SpellName = "spectre_spectral_dagger", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "dagger_radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region naga_siren_rip_tide

            Spells.Add(
                new SpellData
                    {
                        SpellName = "naga_siren_rip_tide", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region visage_soul_assumption

            Spells.Add(
                new SpellData
                    {
                        SpellName = "visage_soul_assumption", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region earthshaker_echo_slam

            Spells.Add(
                new SpellData
                    {
                        SpellName = "earthshaker_echo_slam", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 625, StringRadius = null, Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region invoker_ice_wall

            Spells.Add(
                new SpellData
                    {
                        SpellName = "invoker_ice_wall", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 1,
                        Radius = 590, StringRadius = null, Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region tinker_laser

            Spells.Add(
                new SpellData
                    {
                        SpellName = "tinker_laser", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region rattletrap_rocket_flare

            Spells.Add(
                new SpellData
                    {
                        SpellName = "rattletrap_rocket_flare", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region invoker_forge_spirit

            Spells.Add(
                new SpellData
                    {
                        SpellName = "invoker_forge_spirit", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 700, StringRadius = null, Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region weaver_the_swarm

            Spells.Add(
                new SpellData
                    {
                        SpellName = "weaver_the_swarm", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region ember_spirit_fire_remnant

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ember_spirit_fire_remnant", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region earthshaker_fissure

            Spells.Add(
                new SpellData
                    {
                        SpellName = "earthshaker_fissure", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = -0.1,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region magnataur_skewer

            Spells.Add(
                new SpellData
                    {
                        SpellName = "magnataur_skewer", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = "skewer_speed", Width = "skewer_radius", AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region beastmaster_primal_roar

            Spells.Add(
                new SpellData
                    {
                        SpellName = "beastmaster_primal_roar", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region tusk_walrus_kick

            Spells.Add(
                new SpellData
                    {
                        SpellName = "tusk_walrus_kick", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region brewmaster_thunder_clap

            Spells.Add(
                new SpellData
                    {
                        SpellName = "brewmaster_thunder_clap", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region bane_nightmare

            Spells.Add(
                new SpellData
                    {
                        SpellName = "bane_nightmare", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 1, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region keeper_of_the_light_illuminate

            Spells.Add(
                new SpellData
                    {
                        SpellName = "keeper_of_the_light_illuminate", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region nevermore_shadowraze3

            Spells.Add(
                new SpellData
                    {
                        SpellName = "nevermore_shadowraze3", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "shadowraze_range", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region windrunner_powershot

            Spells.Add(
                new SpellData
                    {
                        SpellName = "windrunner_powershot", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "arrow_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region tusk_ice_shards

            Spells.Add(
                new SpellData
                    {
                        SpellName = "tusk_ice_shards", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = "shard_speed", Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region brewmaster_drunken_haze

            Spells.Add(
                new SpellData
                    {
                        SpellName = "brewmaster_drunken_haze", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region leshrac_lightning_storm

            Spells.Add(
                new SpellData
                    {
                        SpellName = "leshrac_lightning_storm", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region necrolyte_death_pulse

            Spells.Add(
                new SpellData
                    {
                        SpellName = "necrolyte_death_pulse", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = true, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "area_of_effect", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region invoker_alacrity

            Spells.Add(
                new SpellData
                    {
                        SpellName = "invoker_alacrity", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region shadow_shaman_voodoo

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shadow_shaman_voodoo", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region pugna_nether_blast

            Spells.Add(
                new SpellData
                    {
                        SpellName = "pugna_nether_blast", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region morphling_adaptive_strike

            Spells.Add(
                new SpellData
                    {
                        SpellName = "morphling_adaptive_strike", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region lone_druid_true_form

            Spells.Add(
                new SpellData
                    {
                        SpellName = "lone_druid_true_form", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = true, AdditionalDelay = 0,
                        Radius = 700, StringRadius = null, Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region nevermore_shadowraze2

            Spells.Add(
                new SpellData
                    {
                        SpellName = "nevermore_shadowraze2", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "shadowraze_range", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region storm_spirit_ball_lightning

            Spells.Add(
                new SpellData
                    {
                        SpellName = "storm_spirit_ball_lightning", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "ball_lightning_move_speed", Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region tusk_snowball

            Spells.Add(
                new SpellData
                    {
                        SpellName = "tusk_snowball", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = "snowball_speed", Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region phoenix_sun_ray_toggle_move

            Spells.Add(
                new SpellData
                    {
                        SpellName = "phoenix_sun_ray_toggle_move", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region doom_bringer_scorched_earth

            Spells.Add(
                new SpellData
                    {
                        SpellName = "doom_bringer_scorched_earth", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region phoenix_sun_ray

            Spells.Add(
                new SpellData
                    {
                        SpellName = "phoenix_sun_ray", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = true
                    });

            #endregion

            #region phoenix_fire_spirits

            Spells.Add(
                new SpellData
                    {
                        SpellName = "phoenix_fire_spirits", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "spirit_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region silencer_last_word

            Spells.Add(
                new SpellData
                    {
                        SpellName = "silencer_last_word", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 4,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region phantom_lancer_spirit_lance

            Spells.Add(
                new SpellData
                    {
                        SpellName = "phantom_lancer_spirit_lance", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "lance_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region ursa_enrage

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ursa_enrage", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 350,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = true, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region chen_penitence

            Spells.Add(
                new SpellData
                    {
                        SpellName = "chen_penitence", IsDisable = false, IsSlow = true, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region invoker_deafening_blast

            Spells.Add(
                new SpellData
                    {
                        SpellName = "invoker_deafening_blast", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "travel_distance", Speed = "travel_speed", Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region warlock_shadow_word

            Spells.Add(
                new SpellData
                    {
                        SpellName = "warlock_shadow_word", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region centaur_double_edge

            Spells.Add(
                new SpellData
                    {
                        SpellName = "centaur_double_edge", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region earth_spirit_stone_caller

            Spells.Add(
                new SpellData
                    {
                        SpellName = "earth_spirit_stone_caller", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 400, StringRadius = null, Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region bane_fiends_grip

            Spells.Add(
                new SpellData
                    {
                        SpellName = "bane_fiends_grip", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region ogre_magi_bloodlust

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ogre_magi_bloodlust", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = true, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region earthshaker_enchant_totem

            Spells.Add(
                new SpellData
                    {
                        SpellName = "earthshaker_enchant_totem", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 350, StringRadius = null, Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region earth_spirit_rolling_boulder

            Spells.Add(
                new SpellData
                    {
                        SpellName = "earth_spirit_rolling_boulder", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0.6,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region visage_grave_chill

            Spells.Add(
                new SpellData
                    {
                        SpellName = "visage_grave_chill", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region tusk_frozen_sigil

            Spells.Add(
                new SpellData
                    {
                        SpellName = "tusk_frozen_sigil", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region night_stalker_void

            Spells.Add(
                new SpellData
                    {
                        SpellName = "night_stalker_void", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region riki_smoke_screen

            Spells.Add(
                new SpellData
                    {
                        SpellName = "riki_smoke_screen", IsDisable = false, IsSlow = true, IsSilence = true,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = -0.1,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region vengefulspirit_magic_missile

            Spells.Add(
                new SpellData
                    {
                        SpellName = "vengefulspirit_magic_missile", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "magic_missile_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region leshrac_diabolic_edict

            Spells.Add(
                new SpellData
                    {
                        SpellName = "leshrac_diabolic_edict", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region earth_spirit_boulder_smash

            Spells.Add(
                new SpellData
                    {
                        SpellName = "earth_spirit_boulder_smash", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region slardar_sprint

            Spells.Add(
                new SpellData
                    {
                        SpellName = "slardar_sprint", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region razor_plasma_field

            Spells.Add(
                new SpellData
                    {
                        SpellName = "razor_plasma_field", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region ember_spirit_flame_guard

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ember_spirit_flame_guard", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = true, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region morphling_waveform

            Spells.Add(
                new SpellData
                    {
                        SpellName = "morphling_waveform", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "projectile_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region axe_culling_blade

            Spells.Add(
                new SpellData
                    {
                        SpellName = "axe_culling_blade", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = true, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = true
                    });

            #endregion

            #region weaver_shukuchi

            Spells.Add(
                new SpellData
                    {
                        SpellName = "weaver_shukuchi", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = true, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region death_prophet_silence

            Spells.Add(
                new SpellData
                    {
                        SpellName = "death_prophet_silence", IsDisable = false, IsSlow = false, IsSilence = true,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = -0.1,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region earth_spirit_geomagnetic_grip

            Spells.Add(
                new SpellData
                    {
                        SpellName = "earth_spirit_geomagnetic_grip", IsDisable = false, IsSlow = false, IsSilence = true,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region enigma_midnight_pulse

            Spells.Add(
                new SpellData
                    {
                        SpellName = "enigma_midnight_pulse", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region sniper_assassinate

            Spells.Add(
                new SpellData
                    {
                        SpellName = "sniper_assassinate", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = true
                    });

            #endregion

            #region elder_titan_echo_stomp

            Spells.Add(
                new SpellData
                    {
                        SpellName = "elder_titan_echo_stomp", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region nevermore_shadowraze1

            Spells.Add(
                new SpellData
                    {
                        SpellName = "nevermore_shadowraze1", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "shadowraze_range", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region nevermore_requiem

            Spells.Add(
                new SpellData
                    {
                        SpellName = "nevermore_requiem", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region tiny_toss

            Spells.Add(
                new SpellData
                    {
                        SpellName = "tiny_toss", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region slark_pounce

            Spells.Add(
                new SpellData
                    {
                        SpellName = "slark_pounce", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = "pounce_radius", Speed = "pounce_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region ogre_magi_ignite

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ogre_magi_ignite", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "projectile_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region furion_sprout

            Spells.Add(
                new SpellData
                    {
                        SpellName = "furion_sprout", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = -0.1, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region kunkka_ghostship

            Spells.Add(
                new SpellData
                    {
                        SpellName = "kunkka_ghostship", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "ghostship_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region troll_warlord_whirling_axes_ranged

            Spells.Add(
                new SpellData
                    {
                        SpellName = "troll_warlord_whirling_axes_ranged", IsDisable = false, IsSlow = true,
                        IsSilence = false, IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false,
                        AdditionalDelay = 0, Radius = 0, StringRadius = null, Speed = "axe_speed", Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region ember_spirit_activate_fire_remnant

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ember_spirit_activate_fire_remnant", IsDisable = false, IsSlow = false,
                        IsSilence = false, IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false,
                        AdditionalDelay = 0, Radius = 0, StringRadius = null, Speed = null, Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region storm_spirit_static_remnant

            Spells.Add(
                new SpellData
                    {
                        SpellName = "storm_spirit_static_remnant", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region skeleton_king_hellfire_blast

            Spells.Add(
                new SpellData
                    {
                        SpellName = "skeleton_king_hellfire_blast", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "blast_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region centaur_hoof_stomp

            Spells.Add(
                new SpellData
                    {
                        SpellName = "centaur_hoof_stomp", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region tusk_walrus_punch

            Spells.Add(
                new SpellData
                    {
                        SpellName = "tusk_walrus_punch", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region leshrac_split_earth

            Spells.Add(
                new SpellData
                    {
                        SpellName = "leshrac_split_earth", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0.35,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            //data below might be wrong or outdated

            #region spirit_breaker_charge_of_darkness

            Spells.Add(
                new SpellData
                    {
                        SpellName = "spirit_breaker_charge_of_darkness", IsDisable = true, IsSlow = false,
                        IsSilence = false, IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false,
                        AdditionalDelay = 0, Radius = 0, StringRadius = null, Speed = "movement_speed", Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region vengefulspirit_wave_of_terror

            Spells.Add(
                new SpellData
                    {
                        SpellName = "vengefulspirit_wave_of_terror", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region queenofpain_scream_of_pain

            Spells.Add(
                new SpellData
                    {
                        SpellName = "queenofpain_scream_of_pain", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "area_of_effect", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region earth_spirit_petrify

            Spells.Add(
                new SpellData
                    {
                        SpellName = "earth_spirit_petrify", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region gyrocopter_rocket_barrage

            Spells.Add(
                new SpellData
                    {
                        SpellName = "gyrocopter_rocket_barrage", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region invoker_emp

            Spells.Add(
                new SpellData
                    {
                        SpellName = "invoker_emp", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 2.9, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region bane_enfeeble

            Spells.Add(
                new SpellData
                    {
                        SpellName = "bane_enfeeble", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region bristleback_viscous_nasal_goo

            Spells.Add(
                new SpellData
                    {
                        SpellName = "bristleback_viscous_nasal_goo", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = true, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region shadow_demon_shadow_poison

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shadow_demon_shadow_poison", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region shredder_return_chakram

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shredder_return_chakram", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region shadow_shaman_ether_shock

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shadow_shaman_ether_shock", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region ancient_apparition_ice_vortex

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ancient_apparition_ice_vortex", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region bloodseeker_blood_bath

            Spells.Add(
                new SpellData
                    {
                        SpellName = "bloodseeker_blood_bath", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 2.6,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region gyrocopter_homing_missile

            Spells.Add(
                new SpellData
                    {
                        SpellName = "gyrocopter_homing_missile", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 3,
                        Radius = 0, StringRadius = null, Speed = "speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region jakiro_ice_path

            Spells.Add(
                new SpellData
                    {
                        SpellName = "jakiro_ice_path", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0.5, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region drow_ranger_wave_of_silence

            Spells.Add(
                new SpellData
                    {
                        SpellName = "drow_ranger_wave_of_silence", IsDisable = false, IsSlow = false, IsSilence = true,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "wave_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region invoker_tornado

            Spells.Add(
                new SpellData
                    {
                        SpellName = "invoker_tornado", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = "travel_distance", Speed = "travel_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region dragon_knight_breathe_fire

            Spells.Add(
                new SpellData
                    {
                        SpellName = "dragon_knight_breathe_fire", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region naga_siren_ensnare

            Spells.Add(
                new SpellData
                    {
                        SpellName = "naga_siren_ensnare", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "net_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = true, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region kunkka_x_marks_the_spot

            Spells.Add(
                new SpellData
                    {
                        SpellName = "kunkka_x_marks_the_spot", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0.1,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region spirit_breaker_nether_strike

            Spells.Add(
                new SpellData
                    {
                        SpellName = "spirit_breaker_nether_strike", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region razor_static_link

            Spells.Add(
                new SpellData
                    {
                        SpellName = "razor_static_link", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region necrolyte_reapers_scythe

            Spells.Add(
                new SpellData
                    {
                        SpellName = "necrolyte_reapers_scythe", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = true
                    });

            #endregion

            #region broodmother_spawn_spiderlings

            Spells.Add(
                new SpellData
                    {
                        SpellName = "broodmother_spawn_spiderlings", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region bristleback_quill_spray

            Spells.Add(
                new SpellData
                    {
                        SpellName = "bristleback_quill_spray", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region legion_commander_overwhelming_odds

            Spells.Add(
                new SpellData
                    {
                        SpellName = "legion_commander_overwhelming_odds", IsDisable = false, IsSlow = false,
                        IsSilence = false, IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false,
                        AdditionalDelay = 0, Radius = 0, StringRadius = null, Speed = null, Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region ancient_apparition_ice_blast_release

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ancient_apparition_ice_blast_release", IsDisable = false, IsSlow = false,
                        IsSilence = false, IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false,
                        AdditionalDelay = 0, Radius = 0, StringRadius = null, Speed = null, Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region tinker_heat_seeking_missile

            Spells.Add(
                new SpellData
                    {
                        SpellName = "tinker_heat_seeking_missile", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region shredder_whirling_death

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shredder_whirling_death", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "whirling_radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region elder_titan_ancestral_spirit

            Spells.Add(
                new SpellData
                    {
                        SpellName = "elder_titan_ancestral_spirit", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region chaos_knight_chaos_bolt

            Spells.Add(
                new SpellData
                    {
                        SpellName = "chaos_knight_chaos_bolt", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region legion_commander_press_the_attack

            Spells.Add(
                new SpellData
                    {
                        SpellName = "legion_commander_press_the_attack", IsDisable = false, IsSlow = false,
                        IsSilence = false, IsNuke = false, IsSkillShot = false, IsHeal = true, IsShield = true,
                        AdditionalDelay = 0, Radius = 0, StringRadius = null, Speed = null, Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region kunkka_torrent

            Spells.Add(
                new SpellData
                    {
                        SpellName = "kunkka_torrent", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 1.7, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region bounty_hunter_shuriken_toss

            Spells.Add(
                new SpellData
                    {
                        SpellName = "bounty_hunter_shuriken_toss", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = true
                    });

            #endregion

            #region omniknight_repel

            Spells.Add(
                new SpellData
                    {
                        SpellName = "omniknight_repel", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = true, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region batrider_sticky_napalm

            Spells.Add(
                new SpellData
                    {
                        SpellName = "batrider_sticky_napalm", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0.2,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region pudge_dismember

            Spells.Add(
                new SpellData
                    {
                        SpellName = "pudge_dismember", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = true, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region batrider_flaming_lasso

            Spells.Add(
                new SpellData
                    {
                        SpellName = "batrider_flaming_lasso", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = true, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region slardar_amplify_damage

            Spells.Add(
                new SpellData
                    {
                        SpellName = "slardar_amplify_damage", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region lion_voodoo

            Spells.Add(
                new SpellData
                    {
                        SpellName = "lion_voodoo", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region lich_frost_nova

            Spells.Add(
                new SpellData
                    {
                        SpellName = "lich_frost_nova", IsDisable = false, IsSlow = true, IsSilence = false, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region beastmaster_call_of_the_wild_boar

            Spells.Add(
                new SpellData
                    {
                        SpellName = "beastmaster_call_of_the_wild_boar", IsDisable = false, IsSlow = true,
                        IsSilence = false, IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false,
                        AdditionalDelay = 0, Radius = 0, StringRadius = null, Speed = null, Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region tiny_avalanche

            Spells.Add(
                new SpellData
                    {
                        SpellName = "tiny_avalanche", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0.5, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region doom_bringer_doom

            Spells.Add(
                new SpellData
                    {
                        SpellName = "doom_bringer_doom", IsDisable = true, IsSlow = false, IsSilence = true,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region tinker_rearm

            Spells.Add(
                new SpellData
                    {
                        SpellName = "tinker_rearm", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region abaddon_death_coil

            Spells.Add(
                new SpellData
                    {
                        SpellName = "abaddon_death_coil", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = true, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region templar_assassin_refraction

            Spells.Add(
                new SpellData
                    {
                        SpellName = "templar_assassin_refraction", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = true, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region troll_warlord_whirling_axes_melee

            Spells.Add(
                new SpellData
                    {
                        SpellName = "troll_warlord_whirling_axes_melee", IsDisable = false, IsSlow = false,
                        IsSilence = false, IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false,
                        AdditionalDelay = 0, Radius = 400, StringRadius = null, Speed = null, Width = null,
                        AllyBlock = false, EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false,
                        RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region leshrac_pulse_nova

            Spells.Add(
                new SpellData
                    {
                        SpellName = "leshrac_pulse_nova", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region lion_finger_of_death

            Spells.Add(
                new SpellData
                    {
                        SpellName = "lion_finger_of_death", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region life_stealer_open_wounds

            Spells.Add(
                new SpellData
                    {
                        SpellName = "life_stealer_open_wounds", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region bane_brain_sap

            Spells.Add(
                new SpellData
                    {
                        SpellName = "bane_brain_sap", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region zuus_arc_lightning

            Spells.Add(
                new SpellData
                    {
                        SpellName = "zuus_arc_lightning", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region tidehunter_anchor_smash

            Spells.Add(
                new SpellData
                    {
                        SpellName = "tidehunter_anchor_smash", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region night_stalker_crippling_fear

            Spells.Add(
                new SpellData
                    {
                        SpellName = "night_stalker_crippling_fear", IsDisable = false, IsSlow = false, IsSilence = true,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region skywrath_mage_arcane_bolt

            Spells.Add(
                new SpellData
                    {
                        SpellName = "skywrath_mage_arcane_bolt", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region invoker_chaos_meteor

            Spells.Add(
                new SpellData
                    {
                        SpellName = "invoker_chaos_meteor", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 1.3,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region medusa_mystic_snake

            Spells.Add(
                new SpellData
                    {
                        SpellName = "medusa_mystic_snake", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region sniper_shrapnel

            Spells.Add(
                new SpellData
                    {
                        SpellName = "sniper_shrapnel", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = false,
                        IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 1.4, Radius = 0,
                        StringRadius = "radius", Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region lina_laguna_blade

            Spells.Add(
                new SpellData
                    {
                        SpellName = "lina_laguna_blade", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region ursa_overpower

            Spells.Add(
                new SpellData
                    {
                        SpellName = "ursa_overpower", IsDisable = false, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 0,
                        StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = true, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region queenofpain_sonic_wave

            Spells.Add(
                new SpellData
                    {
                        SpellName = "queenofpain_sonic_wave", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region lion_impale

            Spells.Add(
                new SpellData
                    {
                        SpellName = "lion_impale", IsDisable = true, IsSlow = false, IsSilence = false, IsNuke = true,
                        IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0, Radius = 450,
                        StringRadius = null, Speed = null, Width = "width", AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region lina_dragon_slave

            Spells.Add(
                new SpellData
                    {
                        SpellName = "lina_dragon_slave", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "dragon_slave_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region venomancer_venomous_gale

            Spells.Add(
                new SpellData
                    {
                        SpellName = "venomancer_venomous_gale", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region skywrath_mage_ancient_seal

            Spells.Add(
                new SpellData
                    {
                        SpellName = "skywrath_mage_ancient_seal", IsDisable = false, IsSlow = false, IsSilence = true,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region antimage_mana_void

            Spells.Add(
                new SpellData
                    {
                        SpellName = "antimage_mana_void", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = true, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = true
                    });

            #endregion

            #region shadow_shaman_shackles

            Spells.Add(
                new SpellData
                    {
                        SpellName = "shadow_shaman_shackles", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region huskar_life_break

            Spells.Add(
                new SpellData
                    {
                        SpellName = "huskar_life_break", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = "charge_speed", Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region dragon_knight_dragon_tail

            Spells.Add(
                new SpellData
                    {
                        SpellName = "dragon_knight_dragon_tail", IsDisable = true, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region crystal_maiden_frostbite

            Spells.Add(
                new SpellData
                    {
                        SpellName = "crystal_maiden_frostbite", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = true, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion

            #region skywrath_mage_concussive_shot

            Spells.Add(
                new SpellData
                    {
                        SpellName = "skywrath_mage_concussive_shot", IsDisable = false, IsSlow = true, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = "launch_radius", Speed = null, Width = null, AllyBlock = false,
                        EnemyBlock = false, MagicImmunityPierce = false, FakeCastRange = false, RealCastRange = null,
                        OnlyForKillSteal = false
                    });

            #endregion

            #region bloodseeker_bloodrage

            Spells.Add(
                new SpellData
                    {
                        SpellName = "bloodseeker_bloodrage", IsDisable = false, IsSlow = false, IsSilence = false,
                        IsNuke = false, IsSkillShot = false, IsHeal = false, IsShield = false, AdditionalDelay = 0,
                        Radius = 0, StringRadius = null, Speed = null, Width = null, AllyBlock = false, EnemyBlock = false,
                        MagicImmunityPierce = true, FakeCastRange = false, RealCastRange = null, OnlyForKillSteal = false
                    });

            #endregion
        }

        #endregion

        #region Public Methods and Operators

        public static SpellData Find(string spellName)
        {
            return Spells.FirstOrDefault(data => data.SpellName == spellName);
        }

        #endregion
    }
}