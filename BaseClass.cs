using BTD_Mod_Helper.Api;
using Il2CppAssets.Scripts.Simulation.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ClassesRemastered
{
    public abstract class BaseClass : ModContent
    {
        public static bool IsSelected(BaseClass @class) => ClassesRemasteredMain.activeclass == @class;

        public override void Register()
        {

        }
        /// <summary>
        /// A cash multipler that is used to change cash gain
        /// </summary>
        public virtual float CashMultiplier { get; } = 1f;
        /// <summary>
        /// Determines whether the class will show in the selector screen
        /// </summary>
        public virtual bool ShowInSelector { get; set; } = true;
        /// <summary>
        /// Determines the Height of the effects content
        /// </summary>
        public virtual int EffectsHeight { get; } = 800;
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
}
