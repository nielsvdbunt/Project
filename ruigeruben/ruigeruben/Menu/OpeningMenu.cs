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

            CCLabel Titel = new CCLabel("SPACESONNE", "Fonts/Coalition", 100, CCLabelFormat.SpriteFont);
            Titel.Position = new CCPoint(bounds.Center.X, 950);
            AddChild(Titel);


            //Button zonder plaatje
            Button PlayButton = new Button("Play game", new CCPoint(bounds.Center.X, 700), Font, FontSize, this);
            PlayButton.OnClicked += new ClickEventHandler(OnPlayGame);
            m_Buttons.Add(PlayButton);

            Button HelpButton = new Button("Help", new CCPoint(bounds.Center.X, 500), Font, FontSize, this);
            HelpButton.OnClicked += new ClickEventHandler(OnHelp);
            m_Buttons.Add(HelpButton);

            Button SettingsButton = new Button("Settings", new CCPoint(bounds.Center.X, 300), Font, FontSize, this);
            SettingsButton.OnClicked += new ClickEventHandler(OnSettings);
            m_Buttons.Add(SettingsButton);
            CCAudioEngine.SharedEngine.PlayBackgroundMusic(filename: "FruityFalls",loop: false);
            //  Button Test = new Button("Button", "Test", new CCPoint(bounds.Center.X, 800), Font, FontSize, this);
            // m_Buttons.Add(Test);
            //  Test.GetSprite().ScaleX *= 2;
            // Test.GetSprite().ScaleY *= 1.33f;
            //Zo maak je een button MET een plaatje, en het plaatje kan je daarna veranderen, text pos veranderen etcetc
        }

        private void OnPlayGame()
        {
            MainActivity.SwitchToMenu(SceneIds.PlayMenu, 0);    
        }

           private void OnHelp()
         {
          //  var newScene = new HelpMenuTest(AbstractMenu.GameView);
           // AbstractMenu.GoToScene(newScene);
            MainActivity.SwitchToMenu(SceneIds.HelpMenu, 0);
         } 

      
        private void OnSettings()
        {
            MainActivity.SwitchToMenu(SceneIds.SettingsMenu, 0);
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

        public override void OnBack()
        {
            //Do nothing
        }
    }
}