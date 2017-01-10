using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;


// de instellingen voordat het spel gaat beginnen
namespace ruigeruben
{
    class SettingMenu : AbstractMenu
    {
        CCRect bounds;
        Button m_BackMenuButton;
        List<Button> m_Buttons;

        public SettingMenu()
        {
            m_Buttons = new List<Button>();
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            string Font = "Fonts/Coalition";
            int FontSize = 36;

            bounds = VisibleBoundsWorldspace;

            CCLabel Title = new CCLabel("Settings", "Fonts/Coalition", 70, CCLabelFormat.SpriteFont);
            Title.Position = new CCPoint(bounds.Center.X, 950);
            AddChild(Title);

            m_BackMenuButton = new Button("<<<", new CCPoint(bounds.MinX + 70, bounds.MaxY - 100), Font, FontSize, this);
            m_BackMenuButton.OnClicked += new ClickEventHandler(OnBackMenu);
            m_Buttons.Add(m_BackMenuButton);

        }
        public override void OnClick(CCPoint Location)
        {
            Location = ScreenToWorldspace(Location);

            foreach (Button b in m_Buttons)
            {
                if (b.OnClickEvent(Location))
                    return;
            }
        }

        private void OnBackMenu()
        {
            MainActivity.SwitchToMenu(SceneIds.OpeningMenu, 0);
        }

        public override void OnBack()
        {
            CCLog.Log("test");
            //MainActivity.SwitchToMenu(SceneIds.OpeningMenu);
        }
    }
}