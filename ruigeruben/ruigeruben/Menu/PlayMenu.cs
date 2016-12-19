using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class PlayMenu : AbstractMenu
    {
        List<Button> m_Buttons;
        List<string> m_Players;

        public PlayMenu() 
        {
            m_Buttons = new List<Button>();
            m_Players = new List<string>();
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
