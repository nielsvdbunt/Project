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

        float m_StartPlayerNames;
        const float m_SpaceBetweenPlayerNames = 150.0f;

        public PlayMenu() 
        {
            m_Buttons = new List<Button>();
            m_Players = new List<string>();
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            CCRect bounds = VisibleBoundsWorldspace;

            // Om te testen? Echte graphics maken?
            var drawNode = new CCDrawNode();
            
            this.AddChild(drawNode);
            var shape = new CCRect(40, bounds.MidY - 500, 400, 1000);
            drawNode.DrawRect(shape,fillColor: CCColor4B.Transparent,
                borderWidth: 4,
                borderColor: CCColor4B.White);

            m_StartPlayerNames = bounds.MidY - 350;

            for(int i = 5; i >= 0; i--)
            {
                CCLabel Titel = new CCLabel("Ruub", "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                Titel.Position = new CCPoint(200, m_StartPlayerNames + i * m_SpaceBetweenPlayerNames);
                AddChild(Titel);
                m_Players.Add("Ruub");
            }


        }

        public override void OnClick(CCPoint Location)
        {
            MainActivity.SwitchToMenu(SceneIds.OpeningMenu);
        }
    }
}
