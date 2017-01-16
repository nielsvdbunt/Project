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
        List<Button> m_Buttons;
        public List<Player> m_Players;
        List<CCLabel> playerlabels = new List<CCLabel>();

        Button m_BackMenuButton;
        float m_StartPlayerNames;
        const float m_SpaceBetweenPlayerNames = 150.0f;

        InputGameInfo m_GameInfo;

        public PlayMenu()
        {
            m_GameInfo = new InputGameInfo();
            m_GameInfo.CardMultiplier = 1;
      
            m_Buttons = new List<Button>();
            m_Players = new List<Player>();
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            CCRect bounds = VisibleBoundsWorldspace;

            // Om te testen? Echte graphics maken?
            var drawNode = new CCDrawNode();
            this.AddChild(drawNode);
            var shape = new CCRect(140, bounds.MidY - 500, 460, 1000);
            drawNode.DrawRect(shape, fillColor: CCColor4B.Transparent,
                borderWidth: 4,
                borderColor: CCColor4B.White);

            m_StartPlayerNames = bounds.MidY + 400;

            Button PlayButton = new Button("Play game", new CCPoint(bounds.Center.X + 500, 100), "Coalition" , 70, this);
            PlayButton.OnClicked += new ClickEventHandler(OnPlayGame);
            m_Buttons.Add(PlayButton);

            m_BackMenuButton = new Button("<<<", new CCPoint(bounds.MinX + 70, bounds.MaxY - 100), "Coalition" , 36, this);
            m_BackMenuButton.OnClicked += new ClickEventHandler(OnBackMenu);
            m_Buttons.Add(m_BackMenuButton);
            
           
            SpelersToevoegen();
            
           

        }


        public void SpelersToevoegen()
        {
            
            Player player1 = new Player();
            player1.Name = "Ruben";
            player1.Points = 10;
            player1.NumberOfAliens = 5;
            player1.Turn = true;
            Player player2 = new Player();
            player2.Name = "Steven";
            player2.Points = 40;
            player2.NumberOfAliens = 6;
            player2.Turn = false;
            Player player3 = new Player();
            player3.Name = "gotvet";
            player3.Points = 100;
            player3.NumberOfAliens = 7;
            player3.Turn = false;
            m_Players.Add(player1);
            m_Players.Add(player2);
            m_Players.Add(player3);
            players();
        }

        private void players()
        {
            deleteplayerlabels();

            for (int i = 0; i < m_Players.Count; i++)
            {
                CCLabel playerlabel = new CCLabel(m_Players[i].Name, "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                playerlabel.Position = new CCPoint(164, m_StartPlayerNames - i * m_SpaceBetweenPlayerNames);
                playerlabel.AnchorPoint = new CCPoint(0, 0);
                playerlabels.Add(playerlabel);
                AddChild(playerlabel);

                Button DeletePlayer = new Button("X", new CCPoint(620, m_StartPlayerNames - i * m_SpaceBetweenPlayerNames), "Fonts/Coalition", 36, this);
                DeletePlayer.SetTextAnchorpoint(new CCPoint(0, 0));
                DeletePlayer.SetTextColor(CCColor3B.Red);
                DeletePlayer.OnClicked += new ClickEventHandler(OnDeleteplayer);
                m_Buttons.Add(DeletePlayer);
            }

            if (m_Players.Count < 6)
            {
                Button AddPlayer = new Button("+", new CCPoint(620, m_StartPlayerNames - m_Players.Count * m_SpaceBetweenPlayerNames), "Fonts/Coalition", 70, this);
                AddPlayer.SetTextAnchorpoint(new CCPoint(0, 0));
                AddPlayer.OnClicked += new ClickEventHandler(OnAddplayer);
              
       
                m_Buttons.Add(AddPlayer);
            }
        }

        //Hoe haal ik hier de buttons weg? Via de button class en zo het label verwijderen?
        private void deleteplayerbuttons()
        {
            
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
            CCEventListenerKeyboard toetsenboord = new CCEventListenerKeyboard();
        }

        private void OnPlayGame()
        {
            MainActivity.SwitchToMenu(SceneIds.Game, m_GameInfo);
        }
       
        private void OnDeleteplayer()
        {
            m_Players.RemoveAt(0);
            players();
            
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

