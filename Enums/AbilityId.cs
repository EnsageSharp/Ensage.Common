// <copyright file="AbilityId.cs" company="EnsageSharp">
//    Copyright (c) 2017 EnsageSharp.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>

#pragma warning disable 1591

namespace Ensage.Common.Enums
{
    /// <summary>
    ///     Ability ID which can be used to identify and find an ability.
    /// </summary>
    public enum AbilityId : uint
    {
        ability_base = 0,

        default_attack = 5001,

        attribute_bonus = 5002,

        ability_deward = 5669,

        antimage_mana_break = 5003,

        antimage_blink = 5004,

        antimage_spell_shield = 5005,

        antimage_mana_void = 5006,

        axe_berserkers_call = 5007,

        axe_battle_hunger = 5008,

        axe_counter_helix = 5009,

        axe_culling_blade = 5010,

        bane_enfeeble = 5012,

        bane_brain_sap = 5011,

        bane_fiends_grip = 5013,

        bane_nightmare = 5014,

        bane_nightmare_end = 5523,

        bloodseeker_bloodrage = 5015,

        bloodseeker_blood_bath = 5016,

        bloodseeker_thirst = 5017,

        bloodseeker_rupture = 5018,

        drow_ranger_frost_arrows = 5019,

        drow_ranger_silence = 5020,

        drow_ranger_wave_of_silence = 5632,

        drow_ranger_trueshot = 5021,

        drow_ranger_marksmanship = 5022,

        earthshaker_fissure = 5023,

        earthshaker_enchant_totem = 5024,

        earthshaker_aftershock = 5025,

        earthshaker_echo_slam = 5026,

        juggernaut_blade_dance = 5027,

        juggernaut_blade_fury = 5028,

        juggernaut_healing_ward = 5029,

        juggernaut_omni_slash = 5030,

        kunkka_torrent = 5031,

        kunkka_tidebringer = 5032,

        kunkka_x_marks_the_spot = 5033,

        kunkka_return = 5034,

        kunkka_ghostship = 5035,

        lina_dragon_slave = 5040,

        lina_light_strike_array = 5041,

        lina_fiery_soul = 5042,

        lina_laguna_blade = 5043,

        lion_impale = 5044,

        lion_voodoo = 5045,

        lion_mana_drain = 5046,

        lion_finger_of_death = 5047,

        mirana_arrow = 5048,

        mirana_invis = 5049,

        mirana_leap = 5050,

        mirana_starfall = 5051,

        morphling_waveform = 5052,

        morphling_adaptive_strike = 5053,

        morphling_morph = 5054,

        morphling_morph_agi = 5055,

        morphling_morph_str = 5056,

        morphling_replicate = 5057,

        morphling_morph_replicate = 5058,

        morphling_hybrid = 5674,

        nevermore_shadowraze1 = 5059,

        nevermore_shadowraze2 = 5060,

        nevermore_shadowraze3 = 5061,

        nevermore_necromastery = 5062,

        nevermore_dark_lord = 5063,

        nevermore_requiem = 5064,

        phantom_lancer_spirit_lance = 5065,

        phantom_lancer_doppelwalk = 5066,

        phantom_lancer_juxtapose = 5067,

        phantom_lancer_phantom_edge = 5068,

        puck_illusory_orb = 5069,

        puck_ethereal_jaunt = 5070,

        puck_waning_rift = 5071,

        puck_phase_shift = 5072,

        puck_dream_coil = 5073,

        pudge_flesh_heap = 5074,

        pudge_meat_hook = 5075,

        pudge_rot = 5076,

        pudge_dismember = 5077,

        shadow_shaman_ether_shock = 5078,

        shadow_shaman_voodoo = 5079,

        shadow_shaman_shackles = 5080,

        shadow_shaman_mass_serpent_ward = 5081,

        razor_plasma_field = 5082,

        razor_static_link = 5083,

        razor_unstable_current = 5084,

        razor_eye_of_the_storm = 5085,

        skeleton_king_hellfire_blast = 5086,

        skeleton_king_vampiric_aura = 5087,

        skeleton_king_mortal_strike = 5088,

        skeleton_king_reincarnation = 5089,

        death_prophet_carrion_swarm = 5090,

        death_prophet_silence = 5091,

        death_prophet_witchcraft = 5092,

        death_prophet_spirit_siphon = 5685,

        death_prophet_exorcism = 5093,

        sven_storm_bolt = 5094,

        sven_great_cleave = 5095,

        sven_warcry = 5096,

        sven_gods_strength = 5097,

        storm_spirit_static_remnant = 5098,

        storm_spirit_electric_vortex = 5099,

        storm_spirit_overload = 5100,

        storm_spirit_ball_lightning = 5101,

        sandking_burrowstrike = 5102,

        sandking_sand_storm = 5103,

        sandking_caustic_finale = 5104,

        sandking_epicenter = 5105,

        tiny_avalanche = 5106,

        tiny_toss = 5107,

        tiny_craggy_exterior = 5108,

        tiny_grow = 5109,

        zuus_arc_lightning = 5110,

        zuus_lightning_bolt = 5111,

        zuus_cloud = 6325,

        zuus_static_field = 5112,

        zuus_thundergods_wrath = 5113,

        slardar_sprint = 5114,

        slardar_slithereen_crush = 5115,

        slardar_bash = 5116,

        slardar_amplify_damage = 5117,

        tidehunter_gush = 5118,

        tidehunter_kraken_shell = 5119,

        tidehunter_anchor_smash = 5120,

        tidehunter_ravage = 5121,

        vengefulspirit_magic_missile = 5122,

        vengefulspirit_command_aura = 5123,

        vengefulspirit_wave_of_terror = 5124,

        vengefulspirit_nether_swap = 5125,

        crystal_maiden_crystal_nova = 5126,

        crystal_maiden_frostbite = 5127,

        crystal_maiden_brilliance_aura = 5128,

        crystal_maiden_freezing_field = 5129,

        windrunner_shackleshot = 5130,

        windrunner_powershot = 5131,

        windrunner_windrun = 5132,

        windrunner_focusfire = 5133,

        lich_frost_nova = 5134,

        lich_frost_armor = 5135,

        lich_dark_ritual = 5136,

        lich_chain_frost = 5137,

        witch_doctor_paralyzing_cask = 5138,

        witch_doctor_voodoo_restoration = 5139,

        witch_doctor_maledict = 5140,

        witch_doctor_death_ward = 5141,

        riki_smoke_screen = 5142,

        riki_blink_strike = 5143,

        riki_permanent_invisibility = 5144,

        riki_tricks_of_the_trade = 5145,

        enigma_malefice = 5146,

        enigma_demonic_conversion = 5147,

        enigma_midnight_pulse = 5148,

        enigma_black_hole = 5149,

        tinker_laser = 5150,

        tinker_heat_seeking_missile = 5151,

        tinker_march_of_the_machines = 5152,

        tinker_rearm = 5153,

        sniper_shrapnel = 5154,

        sniper_headshot = 5155,

        sniper_take_aim = 5156,

        sniper_assassinate = 5157,

        necrolyte_death_pulse = 5158,

        necrolyte_heartstopper_aura = 5159,

        necrolyte_sadist = 5160,

        necrolyte_sadist_stop = 6316,

        necrolyte_reapers_scythe = 5161,

        warlock_fatal_bonds = 5162,

        warlock_shadow_word = 5163,

        warlock_upheaval = 5164,

        warlock_rain_of_chaos = 5165,

        warlock_golem_flaming_fists = 5166,

        warlock_golem_permanent_immolation = 5167,

        beastmaster_wild_axes = 5168,

        beastmaster_call_of_the_wild = 5169,

        beastmaster_call_of_the_wild_boar = 5580,

        beastmaster_hawk_invisibility = 5170,

        beastmaster_boar_poison = 5171,

        beastmaster_greater_boar_poison = 5352,

        beastmaster_inner_beast = 5172,

        beastmaster_primal_roar = 5177,

        queenofpain_shadow_strike = 5173,

        queenofpain_blink = 5174,

        queenofpain_scream_of_pain = 5175,

        queenofpain_sonic_wave = 5176,

        venomancer_venomous_gale = 5178,

        venomancer_poison_sting = 5179,

        venomancer_plague_ward = 5180,

        venomancer_poison_nova = 5181,

        faceless_void_time_walk = 5182,

        faceless_void_backtrack = 5183,

        faceless_void_time_lock = 5184,

        faceless_void_time_dilation = 5691,

        faceless_void_chronosphere = 5185,

        pugna_nether_blast = 5186,

        pugna_decrepify = 5187,

        pugna_nether_ward = 5188,

        pugna_life_drain = 5189,

        phantom_assassin_stifling_dagger = 5190,

        phantom_assassin_phantom_strike = 5191,

        phantom_assassin_blur = 5192,

        phantom_assassin_coup_de_grace = 5193,

        templar_assassin_refraction = 5194,

        templar_assassin_meld = 5195,

        templar_assassin_psi_blades = 5196,

        templar_assassin_psionic_trap = 5197,

        templar_assassin_trap = 5198,

        templar_assassin_self_trap = 5199,

        viper_poison_attack = 5218,

        viper_nethertoxin = 5219,

        viper_corrosive_skin = 5220,

        viper_viper_strike = 5221,

        luna_lucent_beam = 5222,

        luna_moon_glaive = 5223,

        luna_lunar_blessing = 5224,

        luna_eclipse = 5225,

        dragon_knight_breathe_fire = 5226,

        dragon_knight_dragon_tail = 5227,

        dragon_knight_dragon_blood = 5228,

        dragon_knight_elder_dragon_form = 5229,

        dragon_knight_frost_breath = 5232,

        dazzle_poison_touch = 5233,

        dazzle_shallow_grave = 5234,

        dazzle_shadow_wave = 5235,

        dazzle_weave = 5236,

        rattletrap_battery_assault = 5237,

        rattletrap_power_cogs = 5238,

        rattletrap_rocket_flare = 5239,

        rattletrap_hookshot = 5240,

        leshrac_split_earth = 5241,

        leshrac_diabolic_edict = 5242,

        leshrac_lightning_storm = 5243,

        leshrac_pulse_nova = 5244,

        furion_sprout = 5245,

        furion_teleportation = 5246,

        furion_force_of_nature = 5247,

        furion_wrath_of_nature = 5248,

        life_stealer_rage = 5249,

        life_stealer_feast = 5250,

        life_stealer_open_wounds = 5251,

        life_stealer_infest = 5252,

        life_stealer_assimilate = 5671,

        life_stealer_assimilate_eject = 5675,

        life_stealer_consume = 5253,

        life_stealer_control = 5655,

        life_stealer_empty_1 = 5657,

        life_stealer_empty_2 = 5658,

        life_stealer_empty_3 = 5659,

        life_stealer_empty_4 = 5660,

        dark_seer_vacuum = 5255,

        dark_seer_ion_shell = 5256,

        dark_seer_surge = 5257,

        dark_seer_wall_of_replica = 5258,

        clinkz_strafe = 5259,

        clinkz_searing_arrows = 5260,

        clinkz_wind_walk = 5261,

        clinkz_death_pact = 5262,

        omniknight_purification = 5263,

        omniknight_repel = 5264,

        omniknight_degen_aura = 5265,

        omniknight_guardian_angel = 5266,

        enchantress_untouchable = 5267,

        enchantress_enchant = 5268,

        enchantress_natures_attendants = 5269,

        enchantress_impetus = 5270,

        huskar_inner_vitality = 5271,

        huskar_burning_spear = 5272,

        huskar_berserkers_blood = 5273,

        huskar_life_break = 5274,

        night_stalker_void = 5275,

        night_stalker_crippling_fear = 5276,

        night_stalker_hunter_in_the_night = 5277,

        night_stalker_darkness = 5278,

        broodmother_spawn_spiderlings = 5279,

        broodmother_poison_sting = 5284,

        broodmother_spawn_spiderite = 5283,

        broodmother_spin_web = 5280,

        broodmother_incapacitating_bite = 5281,

        broodmother_insatiable_hunger = 5282,

        bounty_hunter_shuriken_toss = 5285,

        bounty_hunter_jinada = 5286,

        bounty_hunter_wind_walk = 5287,

        bounty_hunter_track = 5288,

        weaver_the_swarm = 5289,

        weaver_shukuchi = 5290,

        weaver_geminate_attack = 5291,

        weaver_time_lapse = 5292,

        jakiro_dual_breath = 5297,

        jakiro_ice_path = 5298,

        jakiro_liquid_fire = 5299,

        jakiro_macropyre = 5300,

        batrider_sticky_napalm = 5320,

        batrider_flamebreak = 5321,

        batrider_firefly = 5322,

        batrider_flaming_lasso = 5323,

        chen_penitence = 5328,

        chen_test_of_faith = 5329,

        chen_test_of_faith_teleport = 5522,

        chen_holy_persuasion = 5330,

        chen_hand_of_god = 5331,

        spectre_spectral_dagger = 5334,

        spectre_desolate = 5335,

        spectre_dispersion = 5336,

        spectre_haunt = 5337,

        spectre_reality = 5338,

        doom_bringer_devour = 5339,

        doom_bringer_scorched_earth = 5340,

        doom_bringer_infernal_blade = 5341,

        doom_bringer_doom = 5342,

        doom_bringer_empty1 = 5343,

        doom_bringer_empty2 = 5344,

        ancient_apparition_cold_feet = 5345,

        ancient_apparition_ice_vortex = 5346,

        ancient_apparition_chilling_touch = 5347,

        ancient_apparition_ice_blast = 5348,

        ancient_apparition_ice_blast_release = 5349,

        spirit_breaker_charge_of_darkness = 5353,

        spirit_breaker_empowering_haste = 5354,

        spirit_breaker_greater_bash = 5355,

        spirit_breaker_nether_strike = 5356,

        ursa_earthshock = 5357,

        ursa_overpower = 5358,

        ursa_fury_swipes = 5359,

        ursa_enrage = 5360,

        gyrocopter_rocket_barrage = 5361,

        gyrocopter_homing_missile = 5362,

        gyrocopter_flak_cannon = 5363,

        gyrocopter_call_down = 5364,

        alchemist_acid_spray = 5365,

        alchemist_unstable_concoction = 5366,

        alchemist_unstable_concoction_throw = 5367,

        alchemist_goblins_greed = 5368,

        alchemist_chemical_rage = 5369,

        invoker_quas = 5370,

        invoker_wex = 5371,

        invoker_exort = 5372,

        invoker_empty1 = 5373,

        invoker_empty2 = 5374,

        invoker_invoke = 5375,

        invoker_attribute_bonus = 5690,

        invoker_cold_snap = 5376,

        invoker_ghost_walk = 5381,

        invoker_tornado = 5382,

        invoker_emp = 5383,

        invoker_alacrity = 5384,

        invoker_chaos_meteor = 5385,

        invoker_sun_strike = 5386,

        invoker_forge_spirit = 5387,

        forged_spirit_melting_strike = 5388,

        invoker_ice_wall = 5389,

        invoker_deafening_blast = 5390,

        silencer_curse_of_the_silent = 5377,

        silencer_glaives_of_wisdom = 5378,

        silencer_last_word = 5379,

        silencer_global_silence = 5380,

        obsidian_destroyer_arcane_orb = 5391,

        obsidian_destroyer_astral_imprisonment = 5392,

        obsidian_destroyer_essence_aura = 5393,

        obsidian_destroyer_mind_over_matter = 5684,

        obsidian_destroyer_sanity_eclipse = 5394,

        lycan_summon_wolves = 5395,

        lycan_howl = 5396,

        lycan_feral_impulse = 5397,

        lycan_shapeshift = 5398,

        lycan_summon_wolves_critical_strike = 5399,

        lycan_summon_wolves_invisibility = 5500,

        lone_druid_spirit_bear = 5412,

        lone_druid_rabid = 5413,

        lone_druid_savage_roar = 5414,

        lone_druid_savage_roar_bear = 5687,

        lone_druid_true_form = 5415,

        lone_druid_true_form_druid = 5416,

        lone_druid_true_form_battle_cry = 5417,

        lone_druid_spirit_bear_return = 5418,

        lone_druid_spirit_bear_entangle = 5419,

        lone_druid_spirit_bear_demolish = 5420,

        brewmaster_thunder_clap = 5400,

        brewmaster_drunken_haze = 5401,

        brewmaster_drunken_brawler = 5402,

        brewmaster_primal_split = 5403,

        brewmaster_earth_hurl_boulder = 5404,

        brewmaster_earth_spell_immunity = 5405,

        brewmaster_earth_pulverize = 5406,

        brewmaster_storm_dispel_magic = 5408,

        brewmaster_storm_cyclone = 5409,

        brewmaster_storm_wind_walk = 5410,

        brewmaster_fire_permanent_immolation = 5411,

        shadow_demon_disruption = 5421,

        shadow_demon_soul_catcher = 5422,

        shadow_demon_shadow_poison = 5423,

        shadow_demon_shadow_poison_release = 5424,

        shadow_demon_demonic_purge = 5425,

        chaos_knight_chaos_bolt = 5426,

        chaos_knight_reality_rift = 5427,

        chaos_knight_chaos_strike = 5428,

        chaos_knight_phantasm = 5429,

        meepo_earthbind = 5430,

        meepo_poof = 5431,

        meepo_geostrike = 5432,

        meepo_divided_we_stand = 5433,

        treant_natures_guise = 5434,

        treant_leech_seed = 5435,

        treant_living_armor = 5436,

        treant_overgrowth = 5437,

        treant_eyes_in_the_forest = 5649,

        ogre_magi_fireblast = 5438,

        ogre_magi_unrefined_fireblast = 5466,

        ogre_magi_ignite = 5439,

        ogre_magi_bloodlust = 5440,

        ogre_magi_multicast = 5441,

        undying_decay = 5442,

        undying_soul_rip = 5443,

        undying_tombstone = 5444,

        undying_tombstone_zombie_aura = 5445,

        undying_tombstone_zombie_deathstrike = 5446,

        undying_flesh_golem = 5447,

        rubick_telekinesis = 5448,

        rubick_telekinesis_land = 5449,

        rubick_fade_bolt = 5450,

        rubick_null_field = 5451,

        rubick_spell_steal = 5452,

        rubick_empty1 = 5453,

        rubick_empty2 = 5454,

        rubick_hidden1 = 5455,

        rubick_hidden2 = 5456,

        rubick_hidden3 = 5457,

        disruptor_thunder_strike = 5458,

        disruptor_glimpse = 5459,

        disruptor_kinetic_field = 5460,

        disruptor_static_storm = 5461,

        nyx_assassin_impale = 5462,

        nyx_assassin_mana_burn = 5463,

        nyx_assassin_spiked_carapace = 5464,

        nyx_assassin_vendetta = 5465,

        nyx_assassin_burrow = 5666,

        nyx_assassin_unburrow = 5673,

        naga_siren_mirror_image = 5467,

        naga_siren_ensnare = 5468,

        naga_siren_rip_tide = 5469,

        naga_siren_song_of_the_siren = 5470,

        naga_siren_song_of_the_siren_cancel = 5478,

        keeper_of_the_light_illuminate = 5471,

        keeper_of_the_light_mana_leak = 5472,

        keeper_of_the_light_chakra_magic = 5473,

        keeper_of_the_light_empty1 = 5501,

        keeper_of_the_light_empty2 = 5502,

        keeper_of_the_light_spirit_form = 5474,

        keeper_of_the_light_recall = 5475,

        keeper_of_the_light_blinding_light = 5476,

        keeper_of_the_light_illuminate_end = 5477,

        keeper_of_the_light_spirit_form_illuminate = 5479,

        keeper_of_the_light_spirit_form_illuminate_end = 5503,

        visage_grave_chill = 5480,

        visage_soul_assumption = 5481,

        visage_gravekeepers_cloak = 5482,

        visage_summon_familiars = 5483,

        visage_summon_familiars_stone_form = 5484,

        wisp_tether = 5485,

        wisp_spirits = 5486,

        wisp_overcharge = 5487,

        wisp_relocate = 5488,

        wisp_tether_break = 5489,

        wisp_spirits_in = 5490,

        wisp_spirits_out = 5493,

        wisp_empty1 = 5498,

        wisp_empty2 = 5499,

        slark_dark_pact = 5494,

        slark_pounce = 5495,

        slark_essence_shift = 5496,

        slark_shadow_dance = 5497,

        medusa_split_shot = 5504,

        medusa_mystic_snake = 5505,

        medusa_mana_shield = 5506,

        medusa_stone_gaze = 5507,

        troll_warlord_berserkers_rage = 5508,

        troll_warlord_whirling_axes_ranged = 5509,

        troll_warlord_whirling_axes_melee = 5510,

        troll_warlord_fervor = 5511,

        troll_warlord_battle_trance = 5512,

        centaur_hoof_stomp = 5514,

        centaur_double_edge = 5515,

        centaur_return = 5516,

        centaur_stampede = 5517,

        magnataur_shockwave = 5518,

        magnataur_empower = 5519,

        magnataur_skewer = 5520,

        magnataur_reverse_polarity = 5521,

        shredder_whirling_death = 5524,

        shredder_timber_chain = 5525,

        shredder_reactive_armor = 5526,

        shredder_chakram = 5527,

        shredder_chakram_2 = 5645,

        shredder_return_chakram = 5528,

        shredder_return_chakram_2 = 5646,

        bristleback_viscous_nasal_goo = 5548,

        bristleback_quill_spray = 5549,

        bristleback_bristleback = 5550,

        bristleback_warpath = 5551,

        tusk_ice_shards = 5565,

        tusk_ice_shards_stop = 5668,

        tusk_snowball = 5566,

        tusk_launch_snowball = 5641,

        tusk_frozen_sigil = 5567,

        tusk_walrus_punch = 5568,

        tusk_walrus_kick = 5672,

        skywrath_mage_arcane_bolt = 5581,

        skywrath_mage_concussive_shot = 5582,

        skywrath_mage_ancient_seal = 5583,

        skywrath_mage_mystic_flare = 5584,

        abaddon_death_coil = 5585,

        abaddon_aphotic_shield = 5586,

        abaddon_frostmourne = 5587,

        abaddon_borrowed_time = 5588,

        elder_titan_echo_stomp = 5589,

        elder_titan_echo_stomp_spirit = 5590,

        elder_titan_ancestral_spirit = 5591,

        elder_titan_return_spirit = 5592,

        elder_titan_natural_order = 5593,

        elder_titan_natural_order_spirit = 5750,

        elder_titan_earth_splitter = 5594,

        legion_commander_overwhelming_odds = 5595,

        legion_commander_press_the_attack = 5596,

        legion_commander_moment_of_courage = 5597,

        legion_commander_duel = 5598,

        ember_spirit_searing_chains = 5603,

        ember_spirit_sleight_of_fist = 5604,

        ember_spirit_flame_guard = 5605,

        ember_spirit_fire_remnant = 5606,

        ember_spirit_activate_fire_remnant = 5607,

        earth_spirit_boulder_smash = 5608,

        earth_spirit_rolling_boulder = 5609,

        earth_spirit_geomagnetic_grip = 5610,

        earth_spirit_stone_caller = 5611,

        earth_spirit_petrify = 5648,

        earth_spirit_magnetize = 5612,

        abyssal_underlord_firestorm = 5613,

        abyssal_underlord_pit_of_malice = 5614,

        abyssal_underlord_atrophy_aura = 5615,

        abyssal_underlord_dark_rift = 5616,

        abyssal_underlord_cancel_dark_rift = 5617,

        terrorblade_reflection = 5619,

        terrorblade_conjure_image = 5620,

        terrorblade_metamorphosis = 5621,

        terrorblade_sunder = 5622,

        phoenix_icarus_dive = 5623,

        phoenix_icarus_dive_stop = 5624,

        phoenix_fire_spirits = 5625,

        phoenix_sun_ray = 5626,

        phoenix_sun_ray_stop = 5627,

        phoenix_sun_ray_toggle_move = 5628,

        phoenix_sun_ray_toggle_move_empty = 5629,

        phoenix_supernova = 5630,

        phoenix_launch_fire_spirit = 5631,

        oracle_fortunes_end = 5637,

        oracle_fates_edict = 5638,

        oracle_purifying_flames = 5639,

        oracle_false_promise = 5640,

        broodmother_spin_web_destroy = 5643,

        monkey_king_boundless_strike = 5716,

        monkey_king_mischief = 5719,

        monkey_king_untransform = 5722,

        monkey_king_tree_dance = 5721,

        monkey_king_primal_spring = 5724,

        monkey_king_primal_spring_early = 5726,

        monkey_king_wukongs_command = 5725,

        monkey_king_jingu_mastery = 5723,

        backdoor_protection = 5350,

        backdoor_protection_in_base = 5351,

        filler_ability = 6226,

        necronomicon_warrior_last_will = 5200,

        necronomicon_warrior_sight = 5201,

        necronomicon_warrior_mana_burn = 5202,

        necronomicon_archer_mana_burn = 5203,

        necronomicon_archer_aoe = 5204,

        courier_return_to_base = 5205,

        courier_go_to_secretshop = 5492,

        courier_transfer_items = 5206,

        courier_transfer_items_to_other_player = 6328,

        courier_return_stash_items = 5207,

        courier_take_stash_items = 5208,

        courier_take_stash_and_transfer_items = 5676,

        courier_shield = 5209,

        courier_burst = 5210,

        courier_morph = 5642,

        roshan_spell_block = 5213,

        roshan_halloween_spell_block = 5618,

        roshan_bash = 5214,

        roshan_slam = 5215,

        roshan_inherent_buffs = 5216,

        roshan_devotion = 5217,

        kobold_taskmaster_speed_aura = 5293,

        centaur_khan_endurance_aura = 5294,

        centaur_khan_war_stomp = 5295,

        spawnlord_master_stomp = 6270,

        spawnlord_master_freeze = 6278,

        gnoll_assassin_envenomed_weapon = 5296,

        ghost_frost_attack = 5301,

        polar_furbolg_ursa_warrior_thunder_clap = 5302,

        neutral_spell_immunity = 5303,

        ogre_magi_frost_armor = 5304,

        dark_troll_warlord_ensnare = 5305,

        dark_troll_warlord_raise_dead = 5306,

        mud_golem_rock_destroy = 5667,

        mud_golem_hurl_boulder = 5670,

        giant_wolf_critical_strike = 5307,

        alpha_wolf_critical_strike = 5308,

        alpha_wolf_command_aura = 5309,

        tornado_tempest = 5310,

        enraged_wildkin_tornado = 5312,

        enraged_wildkin_toughness_aura = 5313,

        granite_golem_hp_aura = 5656,

        granite_golem_bash = 5680,

        satyr_trickster_purge = 5314,

        satyr_soulstealer_mana_burn = 5315,

        satyr_hellcaller_shockwave = 5316,

        ancient_golem_rockslide = 5686,

        satyr_hellcaller_unholy_aura = 5317,

        forest_troll_high_priest_heal = 5318,

        harpy_storm_chain_lightning = 5319,

        big_thunder_lizard_wardrums_aura = 5682,

        black_dragon_dragonhide_aura = 5681,

        black_dragon_fireball = 5689,

        mudgolem_cloak_aura = 5688,

        black_dragon_splash_attack = 5324,

        blue_dragonspawn_sorcerer_evasion = 5325,

        blue_dragonspawn_overseer_evasion = 5326,

        spawnlord_aura = 6125,

        spawnlord_master_bash = 6126,

        blue_dragonspawn_overseer_devotion_aura = 5327,

        big_thunder_lizard_slam = 5332,

        big_thunder_lizard_frenzy = 5333,

        forest_troll_high_priest_mana_aura = 5491,

        roshan_halloween_candy = 9990,

        roshan_halloween_angry = 9991,

        roshan_halloween_wave_of_force = 9993,

        roshan_halloween_greater_bash = 9994,

        roshan_halloween_toss = 9995,

        roshan_halloween_shell = 9997,

        roshan_halloween_apocalypse = 9998,

        roshan_halloween_burn = 9999,

        roshan_halloween_levels = 10000,

        roshan_halloween_summon = 10001,

        roshan_halloween_fireball = 10002,

        greevil_magic_missile = 5529,

        greevil_cold_snap = 5530,

        greevil_decrepify = 5531,

        greevil_diabolic_edict = 5532,

        greevil_maledict = 5533,

        greevil_shadow_strike = 5534,

        greevil_laguna_blade = 5535,

        greevil_poison_nova = 5546,

        greevil_ice_wall = 5547,

        greevil_fatal_bonds = 5552,

        greevil_blade_fury = 5553,

        greevil_phantom_strike = 5554,

        greevil_time_lock = 5555,

        greevil_shadow_wave = 5556,

        greevil_leech_seed = 5557,

        greevil_echo_slam = 5558,

        greevil_natures_attendants = 5559,

        greevil_bloodlust = 5560,

        greevil_purification = 5561,

        greevil_flesh_golem = 5562,

        greevil_hook = 5563,

        greevil_rot = 5564,

        greevil_black_hole = 5569,

        greevil_miniboss_black_nightmare = 5536,

        greevil_miniboss_black_brain_sap = 5537,

        greevil_miniboss_blue_cold_feet = 5538,

        greevil_miniboss_blue_ice_vortex = 5539,

        greevil_miniboss_red_earthshock = 5540,

        greevil_miniboss_red_overpower = 5541,

        greevil_miniboss_yellow_ion_shell = 5542,

        greevil_miniboss_yellow_surge = 5543,

        greevil_miniboss_white_purification = 5544,

        greevil_miniboss_white_degen_aura = 5545,

        greevil_miniboss_green_living_armor = 5570,

        greevil_miniboss_green_overgrowth = 5571,

        greevil_miniboss_orange_dragon_slave = 5572,

        greevil_miniboss_orange_light_strike_array = 5573,

        greevil_miniboss_purple_venomous_gale = 5574,

        greevil_miniboss_purple_plague_ward = 5575,

        greevil_miniboss_sight = 5576,

        throw_snowball = 5577,

        throw_coal = 5578,

        healing_campfire = 5579,

        shoot_firework = 5650,

        techies_land_mines = 5599,

        techies_stasis_trap = 5600,

        techies_suicide = 5601,

        techies_remote_mines = 5602,

        techies_focused_detonate = 5635,

        techies_remote_mines_self_detonate = 5636,

        techies_minefield_sign = 5644,

        cny_beast_force_attack = 5661,

        cny2015_sonic_wave = 5662,

        cny2015_black_hole = 5663,

        cny2015_chronosphere = 5664,

        winter_wyvern_arctic_burn = 5651,

        winter_wyvern_splinter_blast = 5652,

        winter_wyvern_cold_embrace = 5653,

        winter_wyvern_winters_curse = 5654,

        cny_beast_teleport = 5665,

        arc_warden_flux = 5677,

        arc_warden_magnetic_field = 5678,

        arc_warden_spark_wraith = 5679,

        arc_warden_tempest_double = 5683,

        special_bonus_undefined = 6285,

        special_bonus_hp_100 = 5900,

        special_bonus_hp_125 = 5901,

        special_bonus_hp_150 = 5902,

        special_bonus_hp_175 = 6034,

        special_bonus_hp_200 = 5959,

        special_bonus_hp_250 = 5903,

        special_bonus_hp_275 = 6311,

        special_bonus_hp_300 = 5993,

        special_bonus_hp_350 = 6195,

        special_bonus_hp_400 = 5976,

        special_bonus_hp_500 = 6235,

        special_bonus_mp_100 = 6096,

        special_bonus_mp_125 = 5904,

        special_bonus_mp_150 = 5905,

        special_bonus_mp_175 = 6067,

        special_bonus_mp_200 = 6094,

        special_bonus_mp_250 = 6006,

        special_bonus_mp_300 = 5990,

        special_bonus_mp_350 = 6254,

        special_bonus_mp_400 = 6321,

        special_bonus_mp_500 = 6185,

        special_bonus_attack_speed_10 = 6118,

        special_bonus_attack_speed_15 = 6119,

        special_bonus_attack_speed_20 = 5906,

        special_bonus_attack_speed_25 = 6016,

        special_bonus_attack_speed_30 = 5907,

        special_bonus_attack_speed_35 = 6196,

        special_bonus_attack_speed_40 = 6210,

        special_bonus_attack_speed_45 = 6224,

        special_bonus_attack_speed_50 = 6020,

        special_bonus_attack_speed_60 = 5908,

        special_bonus_attack_speed_80 = 6030,

        special_bonus_attack_speed_100 = 6037,

        special_bonus_hp_regen_4 = 5909,

        special_bonus_hp_regen_5 = 5910,

        special_bonus_hp_regen_6 = 5969,

        special_bonus_hp_regen_7 = 5966,

        special_bonus_hp_regen_8 = 5911,

        special_bonus_hp_regen_10 = 5912,

        special_bonus_hp_regen_14 = 5913,

        special_bonus_hp_regen_15 = 5914,

        special_bonus_hp_regen_20 = 6079,

        special_bonus_hp_regen_25 = 6308,

        special_bonus_hp_regen_40 = 6022,

        special_bonus_hp_regen_50 = 6302,

        special_bonus_mp_regen_1 = 5915,

        special_bonus_mp_regen_2 = 5961,

        special_bonus_mp_regen_4 = 5916,

        special_bonus_mp_regen_3 = 6160,

        special_bonus_mp_regen_6 = 5980,

        special_bonus_mp_regen_8 = 6243,

        special_bonus_mp_regen_10 = 6116,

        special_bonus_mp_regen_14 = 6255,

        special_bonus_movement_speed_10 = 5958,

        special_bonus_movement_speed_15 = 5917,

        special_bonus_movement_speed_20 = 5918,

        special_bonus_movement_speed_25 = 5919,

        special_bonus_movement_speed_30 = 6141,

        special_bonus_movement_speed_35 = 6077,

        special_bonus_movement_speed_40 = 6093,

        special_bonus_movement_speed_45 = 6249,

        special_bonus_movement_speed_50 = 6306,

        special_bonus_lifesteal_10 = 6158,

        special_bonus_lifesteal_15 = 6289,

        special_bonus_lifesteal_20 = 6078,

        special_bonus_lifesteal_25 = 6111,

        special_bonus_lifesteal_30 = 6121,

        special_bonus_all_stats_4 = 5920,

        special_bonus_all_stats_5 = 5921,

        special_bonus_all_stats_6 = 5922,

        special_bonus_all_stats_7 = 6074,

        special_bonus_all_stats_8 = 5923,

        special_bonus_all_stats_10 = 6168,

        special_bonus_all_stats_12 = 6139,

        special_bonus_all_stats_14 = 6252,

        special_bonus_all_stats_15 = 6135,

        special_bonus_all_stats_20 = 6309,

        special_bonus_intelligence_6 = 5924,

        special_bonus_intelligence_7 = 6117,

        special_bonus_intelligence_8 = 5925,

        special_bonus_intelligence_10 = 5965,

        special_bonus_intelligence_12 = 5926,

        special_bonus_intelligence_13 = 6042,

        special_bonus_intelligence_15 = 5991,

        special_bonus_intelligence_16 = 6332,

        special_bonus_intelligence_20 = 5995,

        special_bonus_intelligence_25 = 6050,

        special_bonus_intelligence_30 = 6060,

        special_bonus_intelligence_35 = 6248,

        special_bonus_spell_lifesteal_20 = 6166,

        special_bonus_spell_lifesteal_70 = 6061,

        special_bonus_strength_3 = 6002,

        special_bonus_strength_4 = 6005,

        special_bonus_strength_5 = 5927,

        special_bonus_strength_6 = 6048,

        special_bonus_strength_7 = 6115,

        special_bonus_strength_8 = 5982,

        special_bonus_strength_9 = 6250,

        special_bonus_strength_10 = 6137,

        special_bonus_strength_12 = 5928,

        special_bonus_strength_14 = 6281,

        special_bonus_strength_15 = 6145,

        special_bonus_strength_20 = 6080,

        special_bonus_strength_25 = 5984,

        special_bonus_agility_8 = 6014,

        special_bonus_agility_10 = 6029,

        special_bonus_agility_13 = 6011,

        special_bonus_agility_14 = 6170,

        special_bonus_agility_15 = 5929,

        special_bonus_agility_16 = 6169,

        special_bonus_agility_20 = 5962,

        special_bonus_agility_25 = 6150,

        special_bonus_armor_2 = 6110,

        special_bonus_armor_3 = 5930,

        special_bonus_armor_4 = 5931,

        special_bonus_armor_5 = 5932,

        special_bonus_armor_6 = 5933,

        special_bonus_armor_7 = 5970,

        special_bonus_armor_8 = 5934,

        special_bonus_armor_9 = 6136,

        special_bonus_armor_10 = 6004,

        special_bonus_armor_12 = 6286,

        special_bonus_armor_15 = 6175,

        special_bonus_magic_resistance_5 = 5935,

        special_bonus_magic_resistance_6 = 5994,

        special_bonus_magic_resistance_8 = 5936,

        special_bonus_magic_resistance_10 = 5937,

        special_bonus_magic_resistance_12 = 6299,

        special_bonus_magic_resistance_15 = 6138,

        special_bonus_magic_resistance_20 = 6000,

        special_bonus_magic_resistance_25 = 6091,

        special_bonus_magic_resistance_30 = 6221,

        special_bonus_day_vision_400 = 6092,

        special_bonus_vision_200 = 6228,

        special_bonus_attack_damage_10 = 6095,

        special_bonus_attack_damage_12 = 6159,

        special_bonus_attack_damage_15 = 5938,

        special_bonus_attack_damage_20 = 5960,

        special_bonus_attack_damage_25 = 6009,

        special_bonus_attack_damage_30 = 5939,

        special_bonus_attack_damage_35 = 6164,

        special_bonus_attack_damage_40 = 5940,

        special_bonus_attack_damage_45 = 6253,

        special_bonus_attack_damage_50 = 5941,

        special_bonus_attack_damage_65 = 6142,

        special_bonus_attack_damage_75 = 5942,

        special_bonus_attack_damage_90 = 5968,

        special_bonus_attack_damage_100 = 5979,

        special_bonus_attack_damage_120 = 6112,

        special_bonus_attack_damage_150 = 6247,

        special_bonus_attack_range_50 = 5992,

        special_bonus_attack_range_75 = 6027,

        special_bonus_attack_range_100 = 5943,

        special_bonus_attack_range_125 = 5944,

        special_bonus_attack_range_150 = 5963,

        special_bonus_attack_range_175 = 6186,

        special_bonus_attack_range_200 = 5945,

        special_bonus_attack_range_250 = 6040,

        special_bonus_attack_range_300 = 6051,

        special_bonus_attack_range_400 = 6307,

        special_bonus_cast_range_50 = 5946,

        special_bonus_cast_range_60 = 6032,

        special_bonus_cast_range_75 = 5947,

        special_bonus_cast_range_100 = 6003,

        special_bonus_cast_range_125 = 6197,

        special_bonus_cast_range_150 = 6056,

        special_bonus_cast_range_175 = 6120,

        special_bonus_cast_range_200 = 6114,

        special_bonus_cast_range_250 = 6161,

        special_bonus_cast_range_300 = 6213,

        special_bonus_spell_amplify_3 = 6162,

        special_bonus_spell_amplify_4 = 6015,

        special_bonus_spell_amplify_5 = 5948,

        special_bonus_spell_amplify_6 = 5996,

        special_bonus_spell_amplify_8 = 5949,

        special_bonus_spell_amplify_10 = 5989,

        special_bonus_spell_amplify_12 = 6326,

        special_bonus_spell_amplify_15 = 6055,

        special_bonus_spell_amplify_20 = 6236,

        special_bonus_spell_amplify_25 = 6327,

        special_bonus_cooldown_reduction_8 = 5950,

        special_bonus_cooldown_reduction_10 = 6021,

        special_bonus_cooldown_reduction_12 = 6190,

        special_bonus_cooldown_reduction_15 = 5951,

        special_bonus_cooldown_reduction_20 = 5952,

        special_bonus_cooldown_reduction_25 = 6222,

        special_bonus_respawn_reduction_15 = 5975,

        special_bonus_respawn_reduction_20 = 5953,

        special_bonus_respawn_reduction_25 = 5954,

        special_bonus_respawn_reduction_30 = 5964,

        special_bonus_respawn_reduction_35 = 6038,

        special_bonus_respawn_reduction_40 = 6066,

        special_bonus_respawn_reduction_50 = 6269,

        special_bonus_respawn_reduction_60 = 6059,

        special_bonus_gold_income_5 = 5955,

        special_bonus_gold_income_10 = 5956,

        special_bonus_gold_income_15 = 6007,

        special_bonus_gold_income_20 = 6008,

        special_bonus_gold_income_25 = 5957,

        special_bonus_gold_income_30 = 6026,

        special_bonus_gold_income_40 = 6301,

        special_bonus_gold_income_50 = 6318,

        special_bonus_evasion_10 = 5971,

        special_bonus_evasion_12 = 6239,

        special_bonus_evasion_15 = 5972,

        special_bonus_evasion_20 = 5973,

        special_bonus_evasion_25 = 5974,

        special_bonus_20_bash_2 = 6025,

        special_bonus_exp_boost_5 = 5983,

        special_bonus_exp_boost_10 = 5985,

        special_bonus_exp_boost_15 = 5986,

        special_bonus_exp_boost_20 = 6017,

        special_bonus_exp_boost_25 = 5987,

        special_bonus_exp_boost_30 = 6317,

        special_bonus_unique_clockwerk = 5977,

        special_bonus_unique_omniknight_1 = 5981,

        special_bonus_unique_omniknight_2 = 6300,

        special_bonus_unique_centaur_1 = 6322,

        special_bonus_unique_centaur_2 = 5988,

        special_bonus_unique_witch_doctor_1 = 5998,

        special_bonus_unique_witch_doctor_2 = 6298,

        special_bonus_unique_necrophos = 6010,

        special_bonus_unique_antimage = 6012,

        special_bonus_unique_mirana_1 = 6013,

        special_bonus_unique_mirana_2 = 6242,

        special_bonus_unique_bounty_hunter = 6018,

        special_bonus_unique_underlord = 6019,

        special_bonus_unique_pudge_1 = 6023,

        special_bonus_unique_pudge_2 = 6245,

        special_bonus_unique_treant = 6024,

        special_bonus_unique_razor = 6028,

        special_bonus_unique_visage_1 = 6031,

        special_bonus_unique_visage_2 = 6320,

        special_bonus_unique_earthshaker = 6035,

        special_bonus_unique_lich_1 = 6039,

        special_bonus_unique_lich_2 = 6292,

        special_bonus_unique_rubick = 6041,

        special_bonus_unique_sven = 6045,

        special_bonus_unique_dark_seer = 6047,

        special_bonus_unique_dazzle_1 = 6049,

        special_bonus_unique_dazzle_2 = 6232,

        special_bonus_unique_shadow_shaman_1 = 6052,

        special_bonus_unique_shadow_shaman_2 = 6295,

        special_bonus_unique_warlock_1 = 6053,

        special_bonus_unique_warlock_2 = 6054,

        special_bonus_unique_vengeful_spirit_1 = 6310,

        special_bonus_unique_vengeful_spirit_2 = 6237,

        special_bonus_unique_vengeful_spirit_3 = 6057,

        special_bonus_unique_venomancer = 6058,

        special_bonus_unique_morphling_1 = 6062,

        special_bonus_unique_morphling_2 = 6225,

        special_bonus_unique_leshrac_1 = 6063,

        special_bonus_unique_leshrac_2 = 6240,

        special_bonus_unique_jakiro = 6064,

        special_bonus_unique_enigma = 6065,

        special_bonus_unique_bane_1 = 6068,

        special_bonus_unique_bane_2 = 6069,

        special_bonus_unique_nevermore_1 = 6670,

        special_bonus_unique_nevermore_2 = 6070,

        special_bonus_unique_templar_assassin = 6071,

        special_bonus_unique_crystal_maiden_1 = 6072,

        special_bonus_unique_crystal_maiden_2 = 6234,

        special_bonus_unique_doom_1 = 6073,

        special_bonus_unique_doom_2 = 6314,

        special_bonus_unique_brewmaster = 6082,

        special_bonus_unique_bristleback = 6083,

        special_bonus_unique_furion = 6084,

        special_bonus_unique_phoenix_1 = 6227,

        special_bonus_unique_phoenix_2 = 6085,

        special_bonus_unique_enchantress_1 = 6331,

        special_bonus_unique_enchantress_2 = 6086,

        special_bonus_unique_batrider_1 = 6087,

        special_bonus_unique_batrider_2 = 6229,

        special_bonus_unique_wraith_king_1 = 6201,

        special_bonus_unique_wraith_king_2 = 6088,

        special_bonus_unique_kunkka = 6089,

        special_bonus_unique_dragon_knight = 6090,

        special_bonus_unique_invoker_1 = 6097,

        special_bonus_unique_invoker_2 = 6098,

        special_bonus_unique_invoker_3 = 6099,

        special_bonus_unique_abaddon = 6100,

        special_bonus_unique_alchemist = 6101,

        special_bonus_unique_axe = 6102,

        special_bonus_unique_beastmaster = 6103,

        special_bonus_unique_clinkz_1 = 6104,

        special_bonus_unique_clinkz_2 = 6231,

        special_bonus_unique_juggernaut = 6105,

        special_bonus_unique_winter_wyvern_1 = 6106,

        special_bonus_unique_winter_wyvern_2 = 6297,

        special_bonus_unique_terrorblade = 6107,

        special_bonus_unique_luna_1 = 6180,

        special_bonus_unique_luna_2 = 6127,

        special_bonus_unique_faceless_void = 6128,

        special_bonus_unique_night_stalker = 6129,

        special_bonus_unique_nyx = 6130,

        special_bonus_unique_weaver_1 = 6200,

        special_bonus_unique_weaver_2 = 6131,

        special_bonus_unique_ursa = 6132,

        special_bonus_unique_chaos_knight = 6133,

        special_bonus_unique_lycan_1 = 6134,

        special_bonus_unique_lycan_2 = 6140,

        special_bonus_unique_windranger = 6144,

        special_bonus_unique_phantom_lancer = 6146,

        special_bonus_unique_slark = 6147,

        special_bonus_unique_spectre = 6148,

        special_bonus_unique_spirit_breaker_1 = 6149,

        special_bonus_unique_spirit_breaker_2 = 6296,

        special_bonus_unique_storm_spirit = 6167,

        special_bonus_unique_tidehunter = 6151,

        special_bonus_unique_tinker = 6152,

        special_bonus_unique_tiny = 6153,

        special_bonus_unique_troll_warlord = 6154,

        special_bonus_unique_undying = 6155,

        special_bonus_unique_viper_1 = 6156,

        special_bonus_unique_viper_2 = 6165,

        special_bonus_unique_zeus = 6157,

        special_bonus_unique_elder_titan = 6171,

        special_bonus_unique_ember_spirit_1 = 6172,

        special_bonus_unique_ember_spirit_2 = 6176,

        special_bonus_unique_lifestealer = 6173,

        special_bonus_unique_lion = 6174,

        special_bonus_unique_skywrath = 6181,

        special_bonus_unique_medusa = 6182,

        special_bonus_unique_ogre_magi = 6183,

        special_bonus_unique_silencer = 6184,

        special_bonus_unique_death_prophet = 6191,

        special_bonus_unique_phantom_assassin = 6192,

        special_bonus_unique_riki_1 = 6330,

        special_bonus_unique_riki_2 = 6193,

        special_bonus_unique_tusk = 6194,

        special_bonus_unique_sniper_1 = 6305,

        special_bonus_unique_sniper_2 = 6198,

        special_bonus_unique_magnus = 6199,

        special_bonus_unique_drow_ranger_1 = 6202,

        special_bonus_unique_drow_ranger_2 = 6209,

        special_bonus_unique_drow_ranger_3 = 6280,

        special_bonus_unique_earth_spirit = 6203,

        special_bonus_unique_huskar = 6204,

        special_bonus_unique_naga_siren = 6205,

        special_bonus_unique_oracle = 6206,

        special_bonus_unique_sand_king = 6207,

        special_bonus_unique_shadow_demon_1 = 6208,

        special_bonus_unique_shadow_demon_2 = 6293,

        special_bonus_unique_slardar = 6211,

        special_bonus_unique_lina_1 = 6212,

        special_bonus_unique_lina_2 = 6313,

        special_bonus_unique_ancient_apparition_1 = 6214,

        special_bonus_unique_ancient_apparition_2 = 6291,

        special_bonus_unique_disruptor = 6215,

        special_bonus_unique_gyrocopter_1 = 6216,

        special_bonus_unique_gyrocopter_2 = 6312,

        special_bonus_unique_outworld_devourer = 6241,

        special_bonus_unique_keeper_of_the_light = 6217,

        special_bonus_unique_legion_commander = 6218,

        special_bonus_unique_puck = 6219,

        special_bonus_unique_pugna_1 = 6220,

        special_bonus_unique_pugna_2 = 6238,

        special_bonus_unique_timbersaw = 6223,

        special_bonus_unique_bloodseeker = 6230,

        special_bonus_unique_broodmother_1 = 6257,

        special_bonus_unique_broodmother_2 = 6258,

        special_bonus_unique_chen_1 = 6259,

        special_bonus_unique_chen_2 = 6260,

        special_bonus_unique_lone_druid_1 = 6261,

        special_bonus_unique_lone_druid_2 = 6262,

        special_bonus_unique_lone_druid_3 = 6263,

        special_bonus_unique_lone_druid_4 = 6268,

        special_bonus_unique_wisp = 6265,

        special_bonus_unique_techies = 6282,

        special_bonus_unique_arc_warden = 6287,

        special_bonus_unique_meepo = 6288,

        special_bonus_unique_monkey_king = 6303,
    }
}