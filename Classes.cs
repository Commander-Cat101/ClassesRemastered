using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
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
    public abstract class ClassBase
    {
        public virtual bool ShowInSelector { get; set; } = true;
        /// <summary>
        /// The name of the class
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// The description for the class
        /// </summary>
        public abstract string Description { get; }
        /// <summary>
        /// The pros that will be shown in the class select screen
        /// </summary>
        public abstract string Pros { get; }
        /// <summary>
        /// The cons that will be shown in the class select screen
        /// </summary>
        public abstract string Cons { get; }
        /// <summary>
        /// The icon for the class
        /// </summary>
        public abstract Sprite Icon { get; }
        /// <summary>
        /// Edits each tower once placed or upgraded
        /// </summary>
        /// <param name="tower">The placed or upgraded tower</param>
        public abstract void EditTower(Tower tower);
    }
    
    public class None : ClassBase
    {
        public override string Name => "Dart Monkey";
        public override string Description => "The Dart Monkey does nothing but scratch thier butt";
        public override string Pros => "Dart monkeys will scratch thier butt :)";

        public override string Cons => "Dart monkeys will scratch thier butt :(";

        public override Sprite Icon => ModContent.GetSprite<ClassesRemasteredMain>("None");
       
        public override void EditTower(Tower tower)
        {
        }
        
    }
    public class Infantry : ClassBase
    {
        public override bool ShowInSelector => false;
        public override string Name => "Infantry";
        public override string Description => "The Infantry excels at close quarters combat, teaching close combat monkey advanced tactics never seen before";

        public override string Pros => "Null";

        public override string Cons => "Null";

        public override Sprite Icon => ModContent.GetSprite<ClassesRemasteredMain>("Infantry");
        public override void EditTower(Tower tower)
        {
        }
    }
    public class Giant : ClassBase
    {
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
                    weapon.rate *= 0.66f;
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
}
