#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Ensage.Common
{

    public static class UnitDatabase
    {
        public static List<AttackAnimationData> Units = new List<AttackAnimationData>();

        static UnitDatabase()
        {
            #region npc_dota_hero_terrorblade

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_terrorblade",
                    UnitClassId = 0,
                    AttackRate = 1.5,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.6,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_lycan

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_lycan",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.55,
                    AttackBackswing = 0.55,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_lycan_wolf1

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_lycan_wolf1",
                    UnitClassId = 0,
                    AttackRate = 1.25,
                    AttackPoint = 0.33,
                    AttackBackswing = 0.64,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_necronomicon_warrior_1

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_necronomicon_warrior_1",
                    UnitClassId = 0,
                    AttackRate = 0.75,
                    AttackPoint = 0.56,
                    AttackBackswing = 0.44,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_goodguys_tower2_bot

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_goodguys_tower2_bot",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_brewmaster_earth_1

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_brewmaster_earth_1",
                    UnitClassId = 0,
                    AttackRate = 1.35,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_ancient_apparition

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_ancient_apparition",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.45,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = 1250,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_brewmaster

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_brewmaster",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.35,
                    AttackBackswing = 0.65,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_luna

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_luna",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.46,
                    AttackBackswing = 0.46,
                    ProjectileSpeed = 900,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_venomancer_plague_ward_3

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_venomancer_plague_ward_3",
                    UnitClassId = 0,
                    AttackRate = 1.5,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 1900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_centaur

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_centaur",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_puck

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_puck",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.8,
                    ProjectileSpeed = 900,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_tusk

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_tusk",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.64,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_broodmother

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_broodmother",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_drow_ranger

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_drow_ranger",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.7,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = 1250,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_phoenix

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_phoenix",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.35,
                    AttackBackswing = 0.633,
                    ProjectileSpeed = 1100,
                    TurnRate = 1,
                });

            #endregion

            #region npc_dota_hero_windrunner

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_windrunner",
                    UnitClassId = 0,
                    AttackRate = 1.5,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = 1250,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_dragon_knight

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_dragon_knight",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 900,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_badguys_tower1_mid

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_badguys_tower1_mid",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_sven

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_sven",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_visage_familiar2

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_visage_familiar2",
                    UnitClassId = 0,
                    AttackRate = 0.4,
                    AttackPoint = 0.33,
                    AttackBackswing = 0.2,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_warlock

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_warlock",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = 1200,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_slardar

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_slardar",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.36,
                    AttackBackswing = 0.64,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_badguys_tower1_top

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_badguys_tower1_top",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_invoker

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_invoker",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_wisp

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_wisp",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.15,
                    AttackBackswing = 0.4,
                    ProjectileSpeed = 1200,
                    TurnRate = 0.7,
                });

            #endregion

            #region npc_dota_goodguys_tower2_mid

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_goodguys_tower2_mid",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_brewmaster_earth_3

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_brewmaster_earth_3",
                    UnitClassId = 0,
                    AttackRate = 1.35,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_creep_badguys_ranged

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_creep_badguys_ranged",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_life_stealer

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_life_stealer",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.39,
                    AttackBackswing = 0.44,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 1,
                });

            #endregion

            #region npc_dota_necronomicon_archer_1

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_necronomicon_archer_1",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0.7,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_crystal_maiden

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_crystal_maiden",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.55,
                    AttackBackswing = 0,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_tiny

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_tiny",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.49,
                    AttackBackswing = 1,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_goodguys_siege

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_goodguys_siege",
                    UnitClassId = 0,
                    AttackRate = 2.7,
                    AttackPoint = 0.7,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = 1100,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_chen

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_chen",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 1100,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_huskar

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_huskar",
                    UnitClassId = 0,
                    AttackRate = 1.6,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 1400,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_troll_warlord

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_troll_warlord",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = 1200,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_zuus

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_zuus",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.633,
                    AttackBackswing = 0.366,
                    ProjectileSpeed = 1100,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_clinkz

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_clinkz",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.7,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = 900,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_mirana

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_mirana",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 900,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_sand_king

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_sand_king",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.53,
                    AttackBackswing = 0.47,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_morphling

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_morphling",
                    UnitClassId = 0,
                    AttackRate = 1.6,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 1300,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_witch_doctor

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_witch_doctor",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 1200,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_lycan_wolf2

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_lycan_wolf2",
                    UnitClassId = 0,
                    AttackRate = 1.2,
                    AttackPoint = 0.33,
                    AttackBackswing = 0.64,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_skeleton_king

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_skeleton_king",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.56,
                    AttackBackswing = 0.44,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_lion

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_lion",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.43,
                    AttackBackswing = 0.74,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_bounty_hunter

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_bounty_hunter",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.59,
                    AttackBackswing = 0.59,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_elder_titan

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_elder_titan",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.35,
                    AttackBackswing = 0.97,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_beastmaster

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_beastmaster",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_omniknight

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_omniknight",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.433,
                    AttackBackswing = 0.567,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_brewmaster_fire_1

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_brewmaster_fire_1",
                    UnitClassId = 0,
                    AttackRate = 1.35,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_vengefulspirit

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_vengefulspirit",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.33,
                    AttackBackswing = 0.64,
                    ProjectileSpeed = 1500,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_pugna

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_pugna",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_bloodseeker

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_bloodseeker",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.43,
                    AttackBackswing = 0.74,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_badguys_tower4

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_badguys_tower4",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_shadow_demon

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_shadow_demon",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.35,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 900,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_necronomicon_archer_2

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_necronomicon_archer_2",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0.7,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_axe

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_axe",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_creep_goodguys_ranged

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_creep_goodguys_ranged",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_tidehunter

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_tidehunter",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.6,
                    AttackBackswing = 0.56,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_lone_druid_bear2

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_lone_druid_bear2",
                    UnitClassId = 0,
                    AttackRate = 1.65,
                    AttackPoint = 0.43,
                    AttackBackswing = 0.67,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_spectre

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_spectre",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_weaver

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_weaver",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.64,
                    AttackBackswing = 0.36,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_badguys_tower2_top

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_badguys_tower2_top",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_templar_assassin

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_templar_assassin",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 900,
                    TurnRate = 0.7,
                });

            #endregion

            #region npc_dota_hero_shadow_shaman

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_shadow_shaman",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 900,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_goodguys_tower2_top

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_goodguys_tower2_top",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_doom_bringer

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_doom_bringer",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_earthshaker

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_earthshaker",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.467,
                    AttackBackswing = 0.863,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_dazzle

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_dazzle",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = 1200,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_naga_siren

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_naga_siren",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_chaos_knight

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_chaos_knight",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_winter_wyvern

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_winter_wyvern",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.25,
                    AttackBackswing = 0.8,
                    ProjectileSpeed = 700,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_spirit_breaker

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_spirit_breaker",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.6,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_badguys_tower2_bot

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_badguys_tower2_bot",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_pudge

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_pudge",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 1.17,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_treant

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_treant",
                    UnitClassId = 0,
                    AttackRate = 1.9,
                    AttackPoint = 0.6,
                    AttackBackswing = 0.4,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_brewmaster_storm_3

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_brewmaster_storm_3",
                    UnitClassId = 0,
                    AttackRate = 1.5,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.77,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_badguys_tower1_bot

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_badguys_tower1_bot",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_enigma

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_enigma",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.77,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_visage_familiar3

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_visage_familiar3",
                    UnitClassId = 0,
                    AttackRate = 0.4,
                    AttackPoint = 0.33,
                    AttackBackswing = 0.2,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_kunkka

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_kunkka",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_creep_badguys_melee

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_creep_badguys_melee",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0.467,
                    AttackBackswing = 0.633,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_queenofpain

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_queenofpain",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.56,
                    AttackBackswing = 0.41,
                    ProjectileSpeed = 1500,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_gyrocopter

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_gyrocopter",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.2,
                    AttackBackswing = 0.97,
                    ProjectileSpeed = 3000,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_nevermore

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_nevermore",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.54,
                    ProjectileSpeed = 1200,
                    TurnRate = 1,
                });

            #endregion

            #region npc_dota_hero_rubick

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_rubick",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.77,
                    ProjectileSpeed = 1125,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_visage_familiar1

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_visage_familiar1",
                    UnitClassId = 0,
                    AttackRate = 0.4,
                    AttackPoint = 0.33,
                    AttackBackswing = 0.2,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_bristleback

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_bristleback",
                    UnitClassId = 0,
                    AttackRate = 1.8,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 1,
                });

            #endregion

            #region npc_dota_beastmaster_boar

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_beastmaster_boar",
                    UnitClassId = 0,
                    AttackRate = 1.25,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.47,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_storm_spirit

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_storm_spirit",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = 1100,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_venomancer_plague_ward_2

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_venomancer_plague_ward_2",
                    UnitClassId = 0,
                    AttackRate = 1.5,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 1900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_necronomicon_warrior_2

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_necronomicon_warrior_2",
                    UnitClassId = 0,
                    AttackRate = 0.75,
                    AttackPoint = 0.56,
                    AttackBackswing = 0.44,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_lycan_wolf3

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_lycan_wolf3",
                    UnitClassId = 0,
                    AttackRate = 1.15,
                    AttackPoint = 0.33,
                    AttackBackswing = 0.64,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_legion_commander

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_legion_commander",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.46,
                    AttackBackswing = 0.64,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_meepo

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_meepo",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.38,
                    AttackBackswing = 0.6,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_leshrac

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_leshrac",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.77,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_undying

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_undying",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_badguys_siege

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_badguys_siege",
                    UnitClassId = 0,
                    AttackRate = 2.7,
                    AttackPoint = 0.7,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = 1100,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_brewmaster_storm_2

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_brewmaster_storm_2",
                    UnitClassId = 0,
                    AttackRate = 1.5,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.77,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_brewmaster_storm_1

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_brewmaster_storm_1",
                    UnitClassId = 0,
                    AttackRate = 1.5,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.77,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_brewmaster_fire_3

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_brewmaster_fire_3",
                    UnitClassId = 0,
                    AttackRate = 1.35,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_brewmaster_fire_2

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_brewmaster_fire_2",
                    UnitClassId = 0,
                    AttackRate = 1.35,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_skywrath_mage

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_skywrath_mage",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.78,
                    ProjectileSpeed = 1000,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_brewmaster_earth_2

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_brewmaster_earth_2",
                    UnitClassId = 0,
                    AttackRate = 1.35,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_lone_druid_bear3

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_lone_druid_bear3",
                    UnitClassId = 0,
                    AttackRate = 1.55,
                    AttackPoint = 0.43,
                    AttackBackswing = 0.67,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_invoker_forged_spirit

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_invoker_forged_spirit",
                    UnitClassId = 0,
                    AttackRate = 1.5,
                    AttackPoint = 0.2,
                    AttackBackswing = 0.4,
                    ProjectileSpeed = 1000,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_goodguys_tower4

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_goodguys_tower4",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_furion

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_furion",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.77,
                    ProjectileSpeed = 1125,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_goodguys_tower3_top

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_goodguys_tower3_top",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_jakiro

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_jakiro",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 1100,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_goodguys_tower3_mid

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_goodguys_tower3_mid",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_badguys_tower2_mid

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_badguys_tower2_mid",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_goodguys_tower1_top

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_goodguys_tower1_top",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_death_prophet

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_death_prophet",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.56,
                    AttackBackswing = 0.51,
                    ProjectileSpeed = 1000,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_lone_druid

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_lone_druid",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.33,
                    AttackBackswing = 0.53,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_sniper

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_sniper",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.17,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 3000,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_batrider

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_batrider",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.54,
                    ProjectileSpeed = 900,
                    TurnRate = 1,
                });

            #endregion

            #region npc_dota_badguys_tower3_top

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_badguys_tower3_top",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_badguys_tower3_mid

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_badguys_tower3_mid",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_oracle

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_oracle",
                    UnitClassId = 0,
                    AttackRate = 1.4,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 900,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_dark_seer

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_dark_seer",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.59,
                    AttackBackswing = 0.58,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_ursa

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_ursa",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_rattletrap

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_rattletrap",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.33,
                    AttackBackswing = 0.64,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_medusa

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_medusa",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.6,
                    ProjectileSpeed = 1200,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_razor

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_razor",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 2000,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_tinker

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_tinker",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.35,
                    AttackBackswing = 0.65,
                    ProjectileSpeed = 900,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_earth_spirit

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_earth_spirit",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.35,
                    AttackBackswing = 0.65,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_goodguys_tower3_bot

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_goodguys_tower3_bot",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_venomancer_plague_ward_4

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_venomancer_plague_ward_4",
                    UnitClassId = 0,
                    AttackRate = 1.5,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 1900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_magnataur

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_magnataur",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.84,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_necronomicon_warrior_3

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_necronomicon_warrior_3",
                    UnitClassId = 0,
                    AttackRate = 0.75,
                    AttackPoint = 0.56,
                    AttackBackswing = 0.44,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_keeper_of_the_light

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_keeper_of_the_light",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.85,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_ember_spirit

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_ember_spirit",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_slark

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_slark",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_creep_goodguys_melee

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_creep_goodguys_melee",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0.467,
                    AttackBackswing = 0.633,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_riki

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_riki",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_lone_druid_bear4

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_lone_druid_bear4",
                    UnitClassId = 0,
                    AttackRate = 1.45,
                    AttackPoint = 0.43,
                    AttackBackswing = 0.67,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_antimage

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_antimage",
                    UnitClassId = 0,
                    AttackRate = 1.45,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.6,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_lycan_wolf4

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_lycan_wolf4",
                    UnitClassId = 0,
                    AttackRate = 1.1,
                    AttackPoint = 0.33,
                    AttackBackswing = 0.64,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_lone_druid_bear1

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_lone_druid_bear1",
                    UnitClassId = 0,
                    AttackRate = 1.75,
                    AttackPoint = 0.43,
                    AttackBackswing = 0.67,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_necrolyte

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_necrolyte",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.53,
                    AttackBackswing = 0.77,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_techies

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_techies",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_phantom_assassin

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_phantom_assassin",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_beastmaster_greater_boar

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_beastmaster_greater_boar",
                    UnitClassId = 0,
                    AttackRate = 1.25,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.47,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_lina

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_lina",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.75,
                    AttackBackswing = 0.78,
                    ProjectileSpeed = 1000,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_obsidian_destroyer

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_obsidian_destroyer",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.46,
                    AttackBackswing = 0.54,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_venomancer

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_venomancer",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 900,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_visage

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_visage",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.46,
                    AttackBackswing = 0.54,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_bane

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_bane",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 900,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_venomancer_plague_ward_1

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_venomancer_plague_ward_1",
                    UnitClassId = 0,
                    AttackRate = 1.5,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 1900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_nyx_assassin

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_nyx_assassin",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.46,
                    AttackBackswing = 0.54,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_disruptor

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_disruptor",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.4,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 1200,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_ogre_magi

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_ogre_magi",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_necronomicon_archer_3

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_necronomicon_archer_3",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0.7,
                    AttackBackswing = 0.3,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_goodguys_tower1_mid

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_goodguys_tower1_mid",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_enchantress

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_enchantress",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.3,
                    AttackBackswing = 0.7,
                    ProjectileSpeed = 900,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_silencer

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_silencer",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = 1000,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_phantom_lancer

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_phantom_lancer",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.5,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_alchemist

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_alchemist",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.35,
                    AttackBackswing = 0.65,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

            #region npc_dota_hero_night_stalker

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_night_stalker",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.55,
                    AttackBackswing = 0.56,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_badguys_tower3_bot

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_badguys_tower3_bot",
                    UnitClassId = 0,
                    AttackRate = 0.95,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_viper

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_viper",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.33,
                    AttackBackswing = 1,
                    ProjectileSpeed = 1200,
                    TurnRate = 0.4,
                });

            #endregion

            #region npc_dota_hero_abaddon

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_abaddon",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.56,
                    AttackBackswing = 0.41,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_faceless_void

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_faceless_void",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.5,
                    AttackBackswing = 0.56,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_goodguys_tower1_bot

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_goodguys_tower1_bot",
                    UnitClassId = 0,
                    AttackRate = 1,
                    AttackPoint = 0,
                    AttackBackswing = 1,
                    ProjectileSpeed = 750,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_lich

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_lich",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.46,
                    AttackBackswing = 0.54,
                    ProjectileSpeed = 900,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_shredder

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_shredder",
                    UnitClassId = 0,
                    AttackRate = 1.7,
                    AttackPoint = 0.36,
                    AttackBackswing = 0.64,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.5,
                });

            #endregion

            #region npc_dota_hero_juggernaut

            Units.Add(
                new AttackAnimationData
                {
                    UnitName = "npc_dota_hero_juggernaut",
                    UnitClassId = 0,
                    AttackRate = 1.6,
                    AttackPoint = 0.33,
                    AttackBackswing = 0.84,
                    ProjectileSpeed = int.MaxValue,
                    TurnRate = 0.6,
                });

            #endregion

        }

        public static AttackAnimationData GetByName(string unitName)
        {
            return Units.FirstOrDefault(unitData => unitData.UnitName.ToLower() == unitName);
        }

        public static AttackAnimationData GetByClassId(ClassId classId)
        {
            return Units.FirstOrDefault(unitData => unitData.UnitClassId.Equals(classId));
        }

        public static float GetAttackSpeed(Unit unit)
        {
            var attackSpeed = Math.Min(unit.AttackSpeed, 600);
            if (unit.Modifiers.Any(x => (x.Name == "modifier_ursa_overpower")))
                attackSpeed = 600;
            return attackSpeed;
        }

        public static double GetAttackPoint(Unit unit)
        {
            if (unit == null)
                return 0;
            var classId = unit.ClassId;
            var name = unit.Name;
            var data = GetByClassId(classId) ?? GetByName(name);
            if (data == null) return 0;
            var attackSpeed = GetAttackSpeed(unit);
            // Console.WriteLine(data.AttackPoint + " " + attackSpeed + " " + Game.Ping + " " + AttackAnimationData.MaxCount);
            return ((data.AttackPoint / (1 + (attackSpeed - 100) / 100)) - ((Game.Ping / 1000) / (1 + (1 - 1 / UnitData.MaxCount))) * 2 + (1 / UnitData.MaxCount) * 3 * (1 + (1 - 1 / UnitData.MaxCount)));
        }

        public static double GetAttackBackswing(Unit unit)
        {
            //ClassId classId = unit.ClassId;
            //String name = unit.Name;
            //AttackAnimationData data = GetByClassId(classId);
            //if (data == null)
            //    data = GetByName(name);
            //var attackSpeed = GetAttackSpeed(unit);
            //return (data.AttackBackswing / (1 + (attackSpeed - 100) / 100)) ;
            var attackRate = GetAttackRate(unit);
            var attackPoint = GetAttackPoint(unit);
            return attackRate - attackPoint;
        }

        public static double GetAttackRate(Unit unit)
        {
            var attackSpeed = GetAttackSpeed(unit);
            var attackBaseTime = unit.AttackBaseTime;
            Ability spell = null;
            if (
                !unit.Modifiers.Any(
                    x =>
                        (x.Name == "modifier_alchemist_chemical_rage" || x.Name == "modifier_terrorblade_metamorphosis" ||
                         x.Name == "modifier_lone_druid_true_form" || x.Name == "modifier_troll_warlord_berserkers_rage")))
                return (attackBaseTime / (1 + (attackSpeed - 100) / 100)) - 0.03;
            switch (unit.ClassId)
            {
                case ClassId.CDOTA_Unit_Hero_Alchemist:
                    spell = unit.Spellbook.Spells.First(x => x.Name == "alchemist_chemical_rage");
                    break;
                case ClassId.CDOTA_Unit_Hero_Terrorblade:
                    spell = unit.Spellbook.Spells.First(x => x.Name == "terrorblade_metamorphosis");
                    break;
                case ClassId.CDOTA_Unit_Hero_LoneDruid:
                    spell = unit.Spellbook.Spells.First(x => x.Name == "lone_druid_true_form");
                    break;
                case ClassId.CDOTA_Unit_Hero_TrollWarlord:
                    spell = unit.Spellbook.Spells.First(x => x.Name == "troll_warlord_berserkers_rage");
                    break;
            }
            attackBaseTime = spell.AbilityData.FirstOrDefault(x => x.Name == "base_attack_time").Value;
            return (attackBaseTime / (1 + (attackSpeed - 100) / 100)) - ((Game.Ping / 1000) / (1 + (1 - 1 / UnitData.MaxCount))) * 2 + (1 / UnitData.MaxCount) * 3 * (1 + (1 - 1 / UnitData.MaxCount));
        }
    }
}
