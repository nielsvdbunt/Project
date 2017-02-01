using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class SettingMenu : AbstractMenu //this class is for settings menu
    {
        CCRect bounds;
        Button m_BackMenuButton;
        Button m_MusicButton;
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

            CCLabel Music = new CCLabel("Music:", Font, FontSize, CCLabelFormat.SpriteFont);
            Music.Position = new CCPoint(bounds.Center.X - 200, bounds.Center.Y);
            AddChild(Music);

            if (CCAudioEngine.SharedEngine.BackgroundMusicPlaying == true)
                m_MusicButton = new Button("On", new CCPoint(bounds.Center), Font, FontSize, this);
            else
            {
                m_MusicButton = new Button("Off", new CCPoint(bounds.Center), Font, FontSize, this);
                m_MusicButton.m_Label.Color = CCColor3B.Red;
            }
            m_MusicButton.OnClicked += delegate
            {
                if (m_MusicButton.m_Label.Text == "On")
                {
                    m_MusicButton.m_Label.Text = "Off";
                    m_MusicButton.m_Label.Color = CCColor3B.Red;
                    CCAudioEngine.SharedEngine.PauseBackgroundMusic();
                }
                else
                {
                    m_MusicButton.m_Label.Text = "On";
                    m_MusicButton.m_Label.Color = CCColor3B.White;
                    CCAudioEngine.SharedEngine.ResumeBackgroundMusic();
                }
            };
            m_Buttons.Add(m_MusicButton);

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