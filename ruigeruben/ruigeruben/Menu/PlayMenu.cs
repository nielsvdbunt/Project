using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
using Xamarin;
using Android.Views.InputMethods;
using static Android.OS.DropBoxManager;
using Android.InputMethodServices;

namespace ruigeruben
{
    class PlayMenu : AbstractMenu
    {
        CCRect bounds;
        List<Button> m_Buttons;
        List<Button> playerbuttons;
        public List<Player> m_Players;
        List<CCLabel> playerlabels;
        

        Button m_BackMenuButton;
        float m_StartPlayerNames;
        const float m_SpaceBetweenPlayerNames = 150.0f;

        InputGameInfo m_GameInfo;

        public PlayMenu()
        {
            m_GameInfo = new InputGameInfo();
            m_GameInfo.CardMultiplier = 1;

      
            m_Buttons = new List<Button>();
            playerbuttons = new List<Button>();
            m_Players = new List<Player>();
            playerlabels = new List<CCLabel>();
            
            
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            
            bounds = VisibleBoundsWorldspace;

            // Om te testen? Echte graphics maken?
            var drawNode = new CCDrawNode();
            this.AddChild(drawNode);
            var shape = new CCRect(140, bounds.MidY - 500, 460, 1000);
            drawNode.DrawRect(shape, fillColor: CCColor4B.Transparent,
                borderWidth: 4,
                borderColor: CCColor4B.White);

            var shape2 = new CCRect(700, bounds.MidY - 500, 1000, 1000);
            drawNode.DrawRect(shape2, fillColor: CCColor4B.Transparent,
                borderWidth: 4,
                borderColor: CCColor4B.White);

            m_StartPlayerNames = bounds.MidY + 400;

            Button PlayButton = new Button("Play game", new CCPoint(bounds.MaxX - 100, bounds.MidY), "Coalition" , 70, this);
            PlayButton.OnClicked += new ClickEventHandler(OnPlayGame);
            PlayButton.Rotate();
            m_Buttons.Add(PlayButton);

            m_BackMenuButton = new Button("<<<", new CCPoint(bounds.MinX + 70, bounds.MaxY - 100), "Coalition" , 36, this);
            m_BackMenuButton.OnClicked += new ClickEventHandler(OnBackMenu);
            m_Buttons.Add(m_BackMenuButton);

            settings();
            players();
        }


        private void settings()
        {
            List<CCLabel> settinglabels = new List<CCLabel>();
            List<CCLabel> settings = new List<CCLabel>();

            CCLabel Alien_Setting,other_setting;
            other_setting = new CCLabel("Test", "Coalition", 60);
            Alien_Setting = new CCLabel("ALIENS","Coalition", 60, CCLabelFormat.SpriteFont);
            settinglabels.Add(Alien_Setting);
            settinglabels.Add(other_setting);


            for(int i = 0; i < settinglabels.Count; i++)
            {
                settinglabels[i].AnchorPoint = new CCPoint(0, 0);
                settinglabels[i].Position = new CCPoint(750, bounds.MaxY - 200 - i*150);
                AddChild(settinglabels[i]);
            }

            CCLabel Alien_count = new CCLabel("5", "Coalition", 70);
            CCLabel Test = new CCLabel("5", "Coalition", 70);
            settings.Add(Alien_count);
            settings.Add(Test);

            for (int i = 0; i<settings.Count; i++)
            {
                int j = i;
                settings[j].AnchorPoint = new CCPoint(0, 0);
                settings[j].Position = new CCPoint(1490, bounds.MaxY - 200 - i * 150);
                AddChild(settings[j]);

                Button minus = new Button("-", new CCPoint(1420, bounds.MaxY - 200 - i * 150), "Coalition", 70, this);
                minus.SetTextAnchorpoint(new CCPoint(0, 0));

                minus.OnClicked += delegate
                {
                    if (int.Parse(settings[j].Text) > 1)
                    {
                        m_GameInfo.Aliens = int.Parse(settings[j].Text) - 1;
                        settings[j].Text = (int.Parse(settings[j].Text) - 1).ToString();
                    }
                    if (int.Parse(settings[j].Text) < 10)
                    {
                        minus.SetTextPossition(new CCPoint(1420, 880 - 150*(j)));
                        settings[j].PositionX = 1490;
                    }
                };
                m_Buttons.Add(minus);

                Button plus = new Button("+", new CCPoint(1600, bounds.MaxY - 200 - i*150), "Coalition", 70, this);
                plus.SetTextAnchorpoint(new CCPoint(0, 0));

                plus.OnClicked += delegate {
                    if (int.Parse(settings[j].Text) < 15)
                    {
                        m_GameInfo.Aliens = int.Parse(settings[j].Text) + 1;
                        settings[j].Text = (int.Parse(settings[j].Text) + 1).ToString();
                    }
                    if (int.Parse(settings[j].Text) >= 10)
                    {
                        minus.SetTextPossition(new CCPoint(1400, 880 - 150*(j)));
                        settings[j].PositionX = 1460;
                    }
                };
                m_Buttons.Add(plus);
            }
            
        }

        private void players()
        {
            deleteplayerlabels();
            deleteplayerbuttons();

            for (int i = 0; i < m_Players.Count; i++)
            {
                CCLabel playerlabel = new CCLabel(m_Players[i].Name, "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                playerlabel.Position = new CCPoint(164, m_StartPlayerNames - i * m_SpaceBetweenPlayerNames);
                playerlabel.AnchorPoint = new CCPoint(0, 0);
                playerlabels.Add(playerlabel);
                AddChild(playerlabel);

                Button DeletePlayer = new Button("X", new CCPoint(620, m_StartPlayerNames - i * m_SpaceBetweenPlayerNames), "Fonts/Coalition", 36, this, 1);
                DeletePlayer.SetTextAnchorpoint(new CCPoint(0, 0));
                DeletePlayer.SetTextColor(CCColor3B.Red);

                int j = i;
                DeletePlayer.OnClicked += delegate
                {
                    m_Players.RemoveAt(m_Players.Count-1);
                    players();
                };
                playerbuttons.Add(DeletePlayer);
            }

            if (m_Players.Count < 6)
            {
                Button AddPlayer = new Button("+", new CCPoint(620, m_StartPlayerNames - m_Players.Count * m_SpaceBetweenPlayerNames), "Fonts/Coalition", 70, this, 1);
                AddPlayer.SetTextAnchorpoint(new CCPoint(0, 0));
                AddPlayer.OnClicked += new ClickEventHandler(OnAddplayer);
                playerbuttons.Add(AddPlayer);
            }
        }

        private void deleteplayerbuttons()
        {
            playerbuttons.Clear();
            RemoveAllChildrenByTag(1);
        }

        private void deleteplayerlabels()
        {
            var toetsenboord = new CCEventListenerKeyboard();
            
            AddEventListener(toetsenboord, this);
            foreach (CCLabel p in playerlabels)
                RemoveChild(p);
        }
       
        private void OnAddplayer()
        {
            Player player = new Player();
            string playername = "Player " + (m_Players.Count + 1);
            player.Name = playername;
            m_Players.Add(player);
            players();
            CCEventListenerKeyboard toetsenboord = new CCEventListenerKeyboard();
        }

        private void OnPlayGame()
        {
            MainActivity.SwitchToMenu(SceneIds.Game, m_GameInfo);
        }
       
        public override void OnClick(CCPoint Location)
        {
            Location = ScreenToWorldspace(Location);

            foreach (Button b in playerbuttons)
            {
                if (b.OnClickEvent(Location))
                    return;
            }
            foreach (Button b in m_Buttons)
            {
                if (b.OnClickEvent(Location))
                    return;
            }
        }

        public override void OnBack()
        {
            MainActivity.SwitchToMenu(SceneIds.OpeningMenu, 0);
        }

        private void OnBackMenu()
        {
            MainActivity.SwitchToMenu(SceneIds.OpeningMenu, 0);
        }
    }
}

