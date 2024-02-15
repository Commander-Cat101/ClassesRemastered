using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Utils;
using Il2CppSystem.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ClassesRemastered
{
    
    public class None : BaseClass
    {
        public override int EffectsHeight => 800;
        public override string Name => "Dart Monkey";
        public override string Description => "The Dart Monkey does nothing but scratch thier butt";
        public override string Pros => "Dart monkeys will scratch their butt :)";

        public override string Cons => "Dart monkeys will scratch their butt :(";

        public override Sprite Icon => ModContent.GetSprite<ClassesRemasteredMain>("None");
       
        public override void EditTower(Tower tower)
        {
            //Nothing
        }
        
    }
    public class Infantry : BaseClass
    {
        public override string Name => "Infantry";
        public override string Description => "The Infantry excels at close quarters combat, teaching close combat monkey advanced tactics never seen before";

        public override int EffectsHeight => 1200;

        public override string Pros => "All Close Combat Towers Get More Attack Speed\nAll Close Combat Towers Get +1 Damage\nAll Close Combat Monkeys Gain Be Ability To Sometimes Push Back Bloons";

        public override string Cons => "All Non Close Combat Monkeys Lose Attack Speed, and 1 Damage";

        public override Sprite Icon => ModContent.GetSprite<ClassesRemasteredMain>("Infantry");
        public override void EditTower(Tower tower)
        {
            var model = tower.rootModel.Duplicate().Cast<TowerModel>();
            if (model.range <= 40)
            {
                foreach (var weapon in model.GetDescendants<WeaponModel>().ToArray())
                {
                    weapon.rate *= 0.7f;
                }
                foreach (var weapon in model.GetDescendants<DamageModel>().ToArray())
                {
                    weapon.damage += 1;
                }
                foreach (var weapon in model.GetDescendants<ProjectileModel>().ToArray())
                {
                    weapon.pierce += 1;
                    weapon.AddBehavior(new WindModel("WindModelAddedByClass", 5, 10, 0.04f, true, ""));
                }
            }
            else
            {
                foreach (var weapon in model.GetDescendants<WeaponModel>().ToArray())
                {
                    weapon.rate *= 1.2f;
                }
                foreach (var weapon in model.GetDescendants<DamageModel>().ToArray())
                {
                    weapon.damage -= 1;
                }
            }
            tower.UpdateRootModel(model);
        }
    }
    public class Giant : BaseClass
    {
        public override int EffectsHeight => 1200;
        public override string Name => "Giant";
        public override string Description => "With Dr.Monkey's Giant Potion all monkeys can grow to the size of pat fusty";

        public override string Pros => "All Towers Become BIG \nAll Towers Get Double Damage (up to +5)\nAll Towers Get Double Pierce\nPat Fusty Gets 1.5x Attack Speed";

        public override string Cons => "All Towers Have A 1.5x Large Footprint\nAll Towers Other Than Pat Fusty Get Less Attack Speed";

        public override Sprite Icon => ModContent.GetSprite<ClassesRemasteredMain>("Giant");
        public override void EditTower(Tower tower)
        {
            var Tower = tower.rootModel.Cast<TowerModel>().Duplicate();
            Tower.displayScale = 1.3f;
            if (Tower.footprint.Is<CircleFootprintModel>())
            {
                Tower.footprint.Cast<CircleFootprintModel>().radius *= 1.5f;
            }
            if (Tower.footprint.Is<RectangleFootprintModel>())
            {
                Tower.footprint.Cast<RectangleFootprintModel>().xWidth *= 1.5f;
                Tower.footprint.Cast<RectangleFootprintModel>().yWidth *= 1.5f;
            }
            foreach (var damagemodel in Tower.GetDescendants<DamageModel>().ToArray())
            {
                if (damagemodel.damage < 6)
                {
                    damagemodel.damage *= 2;
                }
                else
                {
                    damagemodel.damage += 5;
                }
            }
            foreach (var projmodel in Tower.GetDescendants<ProjectileModel>().ToArray())
            {
                projmodel.pierce *= 2f;
            }
            if (Tower.baseId == "PatFusty")
            {
                foreach (var weapon in Tower.GetDescendants<WeaponModel>().ToArray())
                {
                    weapon.rate *= 0.75f;
                }
            }
            else
            {
                foreach (var weapon in Tower.GetDescendants<WeaponModel>().ToArray())
                {
                    weapon.rate *= 2f;
                }
            }
            tower.UpdateRootModel(Tower);
        }
    }
    public class Economist : BaseClass
    {
        public override int EffectsHeight => 1400;
        public override string Name => "Economist";
        public override string Description => "The fiscally-responsible Economist Class makes cash generation and tax evasion insanely easy, but as a result some monkeys are less powerful";

        public override string Pros => "Cash gain for all sources is increased by 20%\nBenjamin's bank hack gives an extra 10% cash gain";

        public override string Cons => "All Towers Lose 1 damage (1 minimum damage)\nAll Towers Lose 2 pierce (1 minimum pierce)\nAll T4 and up towers get 10% slower attack speed";

        public override Sprite Icon => ModContent.GetSprite<ClassesRemasteredMain>("Economist");
        public override float CashMultiplier => 1.2f; 
        public override void EditTower(Tower tower)
        {
            var Model = tower.rootModel.Duplicate().Cast<TowerModel>();

            foreach (var weapon in Model.GetDescendants<WeaponModel>().ToArray())
            {
                if (!Model.IsHero())
                {
                    if (Model.tiers.Max() > 3)
                    {
                        weapon.Rate *= 1.1f;
                    }
                }
            }
            foreach (var projectile in Model.GetDescendants<ProjectileModel>().ToArray())
            {
                if (projectile.pierce > 2)
                {
                    projectile.pierce -= 2;
                }
                else
                {
                    projectile.pierce = 1;
                }
            }
            foreach (var damage in Model.GetDescendants<DamageModel>().ToArray())
            {
                if (damage.damage > 1)
                {
                    damage.damage -= 1;
                }
            }
            if (Model.baseId == TowerType.Benjamin)
            {
                if (Model.tiers[0] > 4)
                {
                    Model.GetBehavior<BananaCashIncreaseSupportModel>().multiplier += .1f;
                }
            }
            tower.UpdateRootModel(Model);
        }
    }
    public class Necromancer : BaseClass
    {
        public override int EffectsHeight => 1200;
        public override string Name => "Necromancer";
        public override string Description => "The power of the Necromancer Class gives many abilities. One being your fellow necromancers gain the power of sacrifice and the other being the power of strength at the loss of money";

        public override string Pros => "Ezili gets a 50% attack speed buff\n004 Wizard Monkey Gets Double The Space In Its Graveyard\n004 & 005 Wizard Monkeys Get Double Attack Speed\nAll 'Evil' Towers get a small buff";

        public override string Cons => "ALL Cash Gain is cut by 15%";

        public override Sprite Icon => ModContent.GetSprite<ClassesRemasteredMain>("Necromancer");
        public override float CashMultiplier => 0.85f;
        public override void EditTower(Tower tower)
        {
            var towerModel = tower.rootModel.Duplicate().Cast<TowerModel>();
            switch (towerModel.baseId)
            {
                case "WizardMonkey":
                    if (tower.towerModel.tiers[2] == 4)
                    {
                        tower.towerModel.behaviors.First(a => a.name == "AttackModel_Attack Necromancer_").Cast<AttackModel>().weapons[0].emission.Cast<NecromancerEmissionModel>().maxRbeStored = 1000;
                        foreach (var weapon in towerModel.GetWeapons())
                        {
                            weapon.Rate *= .5f;
                        }
                    }
                    if (tower.towerModel.tiers[2] == 5)
                    {
                        foreach (var weapon in towerModel.GetWeapons())
                        {
                            weapon.Rate *= .5f;
                        }
                    }
                    break;
                case "Ezili":
                    foreach (var weapon in towerModel.GetWeapons())
                    {
                        weapon.Rate *= .75f;
                    }
                    foreach (var abilitymod in towerModel.GetAbilities())
                    {
                        abilitymod.Cooldown *= .75f;
                    }
                    break;
                case "IceMonkey":
                    if (towerModel.tiers[0] >= 3)
                    {
                        foreach (var weapon in towerModel.GetWeapons())
                        {
                            weapon.Rate *= .85f;
                        }
                    }
                    break;
                case "MonkeyBuccaneer":
                    if (towerModel.tiers[1] >= 3)
                    {
                        foreach (var weapon in towerModel.GetWeapons())
                        {
                            weapon.Rate *= .85f;
                        }
                    }
                    break;
                case "DartlingGunner":
                    if (towerModel.tiers[1] >= 3)
                    {
                        foreach (var weapon in towerModel.GetWeapons())
                        {
                            weapon.Rate *= .85f;
                        }
                    }
                    if (towerModel.tiers[0] >= 4)
                    {
                        foreach (var weapon in towerModel.GetWeapons())
                        {
                            weapon.Rate *= .85f;
                        }
                    }
                    break;
                case "SuperMonkey":
                    if (towerModel.tiers[1] >= 3)
                    {
                        foreach (var weapon in towerModel.GetWeapons())
                        {
                            weapon.Rate *= .85f;
                        }
                    }
                    break;
                case "NinjaMonkey":
                    if (towerModel.tiers[2] >= 3)
                    {
                        foreach (var weapon in towerModel.GetWeapons())
                        {
                            weapon.Rate *= .85f;
                        }
                    }
                    break;
                case "Alchemist":
                    if (towerModel.tiers[1] >= 4)
                    {
                        foreach (var weapon in towerModel.GetWeapons())
                        {
                            weapon.Rate *= .85f;
                        }
                    }
                    break;
                case "Druid":
                    if (towerModel.tiers[2] >= 2)
                    {
                        foreach (var weapon in towerModel.GetWeapons())
                        {
                            weapon.Rate *= .85f;
                        }
                    }
                    break;
                case "SpikeFactory":
                    if (towerModel.tiers[0] >= 3)
                    {
                        foreach (var weapon in towerModel.GetWeapons())
                        {
                            weapon.Rate *= .85f;
                        }
                    }
                    break;
                case "MonkeyVillage":
                    foreach (var attackmodel in towerModel.GetAttackModels())
                    {
                        attackmodel.range *= .7f;
                    }
                    break;
                default:
                    break;

            }
            tower.UpdateRootModel(towerModel);
        }
    }
}