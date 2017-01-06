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
        List<CCLabel> playerlabels = new List<CCLabel>();

        float m_StartPlayerNames;
        const float m_SpaceBetweenPlayerNames = 150.0f;

        public PlayMenu()
        {
            m_Buttons = new List<Button>();
            m_Players = new List<string>();
            m_Players.Add("test1");
            m_Players.Add("testletters");
            m_Players.Add("testjes");
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            CCRect bounds = VisibleBoundsWorldspace;

            // Om te testen? Echte graphics maken?
            var drawNode = new CCDrawNode();
            this.AddChild(drawNode);
            var shape = new CCRect(40, bounds.MidY - 500, 460, 1000);
            drawNode.DrawRect(shape, fillColor: CCColor4B.Transparent,
                borderWidth: 4,
                borderColor: CCColor4B.White);

            m_StartPlayerNames = bounds.MidY + 400;
            
            

            players();
        }

        private void players()
        {
            deleteplayerlabels();
            for (int i = 0; i < m_Players.Count; i++)
            {
                CCLabel playerlabel = new CCLabel(m_Players[i], "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                playerlabel.Position = new CCPoint(64, m_StartPlayerNames - i * m_SpaceBetweenPlayerNames);
                playerlabel.AnchorPoint = new CCPoint(0, 0);
                playerlabels.Add(playerlabel);
                AddChild(playerlabel);

                Button DeletePlayer = new Button("X", new CCPoint(520, m_StartPlayerNames - i * m_SpaceBetweenPlayerNames), "Fonts/Coalition", 36, this);
                DeletePlayer.SetTextAnchorpoint(new CCPoint(0, 0));
                DeletePlayer.SetTextColor(CCColor3B.Red);
                DeletePlayer.OnClicked += new ClickEventHandler(OnDeleteplayer);
                m_Buttons.Add(DeletePlayer);
            }

            if (m_Players.Count < 6)
            {
                Button AddPlayer = new Button("+", new CCPoint(520, m_StartPlayerNames - m_Players.Count * m_SpaceBetweenPlayerNames), "Fonts/Coalition", 70, this);
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
            foreach (CCLabel p in playerlabels)
                RemoveChild(p);
        }

        private void OnAddplayer()
        {
            
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
            MainActivity.SwitchToMenu(SceneIds.OpeningMenu);
        }
    }
}

