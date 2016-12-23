using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace SpaceSonne
{
    class HelpMenu : AbstractMenu
    {
        Button m_NextPageButton;
        Button m_PrevPageButton;
        Button m_BackMenuButton;

        public HelpMenu()
        {

        }

        public override void OnClick(CCPoint Location)
        {

        }

        public override void OnBack()
        {
            MainActivity.SwitchToMenu(SceneIds.OpeningMenu);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            string Font = "Fonts/Coalition";
            int FontSize = 36;

            CCRect bounds = VisibleBoundsWorldspace;

            Button m_NextPageButton = new Button("->", new CCPoint(bounds.MaxX - 160, bounds.MinY + 100), Font, FontSize, this);
            m_NextPageButton.OnClicked += new ClickEventHandler(OnPageNext);

            Button m_PrevPageButton = new Button("<-", new CCPoint(bounds.MinX + 160, bounds.MinY + 100), Font, FontSize, this);
            m_PrevPageButton.OnClicked += new ClickEventHandler(OnPagePrev);

            Button m_BackMenuButton = new Button("<<<", new CCPoint(bounds.MinX + 70, bounds.MaxY - 100), Font, FontSize, this);
            m_BackMenuButton.OnClicked += new ClickEventHandler(OnBackMenu);

            CCLabel TitelHelp = new CCLabel("Help", "Fonts/Coalition", 70, CCLabelFormat.SpriteFont);
           TitelHelp.Position = new CCPoint(bounds.Center.X, 950);
            /* de tekst groote moet nog aangepast worden*/
            CCLabel firsttext = new CCLabel("Welkom bij Spacesonne \n dit is de uitleg met de pijltjes rechts en links onderin kunt u navigeren door de uitleg" , "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            firsttext.Position = new CCPoint(500, 200);
            AddChild(TitelHelp);
            AddChild(firsttext);

        }

        private void OnBackMenu()
        {
            MainActivity.SwitchToMenu(SceneIds.OpeningMenu);
        }

        private void OnPagePrev()
        {

        }

        private void OnPageNext()
        {

        }
    }
}