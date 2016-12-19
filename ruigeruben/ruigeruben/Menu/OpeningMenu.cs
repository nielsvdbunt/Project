using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class OpeningMenu   : AbstractMenu
    {
        List<Button> m_Buttons;

        public OpeningMenu()  
        {
            m_Buttons = new List<Button>();
           
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            string Font = "Fonts/Coalition";
            int FontSize = 70;

            CCRect bounds = VisibleBoundsWorldspace;

            Button PlayButton = new Button("Play game", new CCPoint(bounds.Center.X, 1000), Font, FontSize, this);
            PlayButton.OnClicked += new ClickEventHandler(OnPlayGame);
            m_Buttons.Add(PlayButton);

            Button Test = new Button("Button", "Test", new CCPoint(bounds.Center.X, 800), Font, FontSize, this);
            m_Buttons.Add(Test);
            Test.GetSprite().ScaleX *= 2;
            Test.GetSprite().ScaleY *= 1.33f;
        }

        private void OnPlayGame()
        {
            MainActivity.SwitchToMenu(SceneIds.PlayMenu);    
        }

        public override void OnClick(CCPoint Location)
        {
            Location = ScreenToWorldspace(Location);

            foreach(Button b in m_Buttons)
            {
                if (b.OnClickEvent(Location))
                    return;
            }
        }
    }
}