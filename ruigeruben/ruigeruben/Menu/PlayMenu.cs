using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
using Xamarin;
using static Android.OS.DropBoxManager;

namespace ruigeruben
{
    class PlayMenu : AbstractMenu
    {
        List<Button> m_Buttons;
        List<Player> m_Players;
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

        private void deleteplayerbuttons()
        {
            RemoveAllChildren();
            m_Buttons.Clear();
        }

        private void deleteplayerlabels()
        {
            foreach (CCLabel p in playerlabels)
                RemoveChild(p);
        }

        private void OnAddplayer()
        {
            string player = "Player_" + (m_Players.Count + 1);
            Player player1 = new Player();
            player1.Name = player;
            m_Players.Add(player1);
            deleteplayerbuttons();
            AddedToScene();

        }

        private void OnPlayGame()
        {
            MainActivity.SwitchToMenu(SceneIds.Game, m_GameInfo);
        }
       
        private void OnDeleteplayer()
        {
            m_Players.RemoveAt(m_Players.Count - 1);
            deleteplayerbuttons();
            AddedToScene();
            
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

