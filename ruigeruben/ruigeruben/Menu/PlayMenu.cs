using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class PlayMenu : AbstractMenu
    {
        public PlayMenu() 
        {
           
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            CCRect bounds = VisibleBoundsWorldspace;

            CCSprite panda = new CCSprite("Panda");
            panda.Position = new CCPoint(bounds.Center.X, bounds.Center.Y);
            AddChild(panda);
        }

        public override void OnClick(CCPoint Location)
        {
            MainActivity.SwitchToMenu(SceneIds.OpeningMenu);
        }
    }
}
