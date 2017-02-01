using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
using Xamarin;
using Xamarin.Android;
using Android.Views.InputMethods;
using static Android.OS.DropBoxManager;
using Android.InputMethodServices;

namespace ruigeruben
{
     class PlayMenu : AbstractMenu
    {
        int NumberOfPlayers;
        CCRect bounds;
        List<Button> m_Buttons;
        List<Button> playerbuttons;
        List<InputPlayer> m_Players;
        List<CCLabel> playerlabels;
       
        Button m_BackMenuButton;
        float m_StartPlayerNames;
        const float m_SpaceBetweenPlayerNames = 150.0f;

        InputGameInfo m_GameInfo;

        public PlayMenu()
        {
            m_GameInfo = new InputGameInfo();
            m_GameInfo.CardMultiplier = 1;
            m_GameInfo.Aliens = 5;
            
            m_Buttons = new List<Button>();
            playerbuttons = new List<Button>();
            m_Players = new List<InputPlayer>();
            playerlabels = new List<CCLabel>();

            OnAddplayer();
            OnAddplayer();
            
        }

        public PlayMenu(List<InputPlayer> PlayerInput)
        {
            m_GameInfo = new InputGameInfo();
            m_GameInfo.CardMultiplier = 1;
            m_GameInfo.Aliens = 5;

            m_Buttons = new List<Button>();
            playerbuttons = new List<Button>();
            m_Players = PlayerInput;
            playerlabels = new List<CCLabel>();

            

        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            
            bounds = VisibleBoundsWorldspace;

            // Om te testen? Echte graphics maken?
            var drawNode = new CCDrawNode();
            this.AddChild(drawNode);
            var shape = new CCRect(140, bounds.MidY - 400, 460, 900);
            drawNode.DrawRect(shape, fillColor: CCColor4B.Transparent,
                borderWidth: 4,
                borderColor: CCColor4B.White);

            var shape2 = new CCRect(700, bounds.MidY - 400, 1000, 900);
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

            CCLabel Alien_Setting,other_setting;
            Alien_Setting = new CCLabel("ALIENS","Coalition", 60, CCLabelFormat.SpriteFont);
            other_setting = new CCLabel("CARDS (X)", "Coalition", 60);
            settinglabels.Add(Alien_Setting);
            settinglabels.Add(other_setting);


            for(int i = 0; i < settinglabels.Count; i++)
            {
                settinglabels[i].AnchorPoint = new CCPoint(0, 0);
                settinglabels[i].Position = new CCPoint(750, bounds.MaxY - 200 - i*150);
                AddChild(settinglabels[i]);
            }
            
            CCLabel Alien_count = new CCLabel(m_GameInfo.Aliens.ToString(), "Coalition", 70);
            CCLabel CardsInDeck = new CCLabel(m_GameInfo.CardMultiplier.ToString(), "Coalition", 70);

            Setting(Alien_count, ref m_GameInfo.Aliens, 1, 15, 0, 1);
            Setting(CardsInDeck, ref m_GameInfo.CardMultiplier, 0.5f, 5, 1, 0.5f);


        }

        private void players()
        {
            deleteplayerlabels();
            deleteplayerbuttons();

            for (NumberOfPlayers = 0; NumberOfPlayers < m_Players.Count; NumberOfPlayers++)
            {
                CCLabel playerlabel = new CCLabel(m_Players[NumberOfPlayers].Name, "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                playerlabel.Position = new CCPoint(164, m_StartPlayerNames - NumberOfPlayers* m_SpaceBetweenPlayerNames);
                playerlabel.AnchorPoint = new CCPoint(0, 0);
                playerlabel.Color = m_Players[NumberOfPlayers].Color;
                playerlabels.Add(playerlabel);
                AddChild(playerlabel);
                
                Button DeletePlayer = new Button("X", new CCPoint(620, m_StartPlayerNames - NumberOfPlayers * m_SpaceBetweenPlayerNames), "Fonts/Coalition", 36, this, 1);
                DeletePlayer.SetTextAnchorpoint(new CCPoint(0, 0));
                DeletePlayer.SetTextColor(CCColor3B.Red);

                int j = NumberOfPlayers;
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
            InputPlayer player = new InputPlayer();
            string playername = "Player " + (m_Players.Count + 1);
            player.Name = playername;
            CCColor3B[] Colors = { new CCColor3B(0,191,255), CCColor3B.Red, CCColor3B.Green, CCColor3B.Magenta, CCColor3B.Orange, CCColor3B.Yellow };
            player.Color =  Colors[m_Players.Count];
            m_Players.Add(player);
            players();
            
        }

        private void OnPlayGame()
        {
            if (NumberOfPlayers >= 2)
            {
                m_GameInfo.Players = m_Players;
                MainActivity.SwitchToMenu(SceneIds.Game, m_GameInfo);
            }
            else
            {
                CCLabel Error = new CCLabel("You need at least 2 players!", "Coalition", 60);
                Error.PositionX = bounds.MidX;
                Error.PositionY = bounds.MinY +60;
                Error.Color = CCColor3B.Red;
                AddChild(Error);
                Error.RunActions(new CCDelayTime(2), new CCFadeOut(3));
            }
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

        private void Setting(CCLabel label, ref float value, float min, float max, int order, float stepsize)
        {
            label.AnchorPoint = new CCPoint(0, 0);
            label.Position = new CCPoint(1575-label.ContentSize.Width, bounds.MaxY - 200 - order * 150);
            AddChild(label);

            
            Button minus = new Button("-", new CCPoint(1,1), "Coalition", 70, this);
            CCPoint Minuspos = new CCPoint((1550 - label.ContentSize.Width - minus.m_Label.ContentSize.Width), bounds.MaxY - 200 - order * 150);
            minus.SetTextPossition(Minuspos);
            minus.SetTextAnchorpoint(new CCPoint(0, 0));
            
            
            float temp = value;

            minus.OnClicked += delegate
            {
                if (temp > min)
                {
                    if (order == 0)
                    {
                        m_GameInfo.Aliens -= (int) stepsize;
                        temp = m_GameInfo.Aliens;
                    }
                    else if (order == 1)
                    {
                        m_GameInfo.CardMultiplier -= stepsize;
                        temp = m_GameInfo.CardMultiplier;
                    }
                    label.Text = temp.ToString();
                }

                label.Position = new CCPoint(1575 - label.ContentSize.Width, bounds.MaxY - 200 - order * 150);
                Minuspos = new CCPoint((1550 - label.ContentSize.Width - minus.m_Label.ContentSize.Width), bounds.MaxY - 200 - order * 150);
                minus.SetTextPossition(Minuspos);
            };
            m_Buttons.Add(minus);

            Button plus = new Button("+", new CCPoint(1600, bounds.MaxY - 200 - order * 150), "Coalition", 70, this);
            plus.SetTextAnchorpoint(new CCPoint(0, 0));
            if (temp >= 100)
                plus.SetTextPossition(new CCPoint(1630, bounds.MaxY - 200 - order * 150));

            plus.OnClicked += delegate {
                if (temp < max)
                {
                    if (order == 0)
                    {
                        m_GameInfo.Aliens+= (int) stepsize;
                        temp = m_GameInfo.Aliens;
                    }
                    else if (order == 1)
                    {
                        m_GameInfo.CardMultiplier += stepsize;
                        temp = m_GameInfo.CardMultiplier;
                    }
                    label.Text = temp.ToString();
                }

                label.Position = new CCPoint(1575 - label.ContentSize.Width, bounds.MaxY - 200 - order * 150);
                Minuspos = new CCPoint((1550 - label.ContentSize.Width - minus.m_Label.ContentSize.Width), bounds.MaxY - 200 - order * 150);
                minus.SetTextPossition(Minuspos);
            };
            m_Buttons.Add(plus);
        }
    }
}

