using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class HelpMenu : AbstractMenu
    {
        Button m_NextPageButton;
        Button m_PrevPageButton;
        Button m_BackMenuButton;
        int PageCounter = 0;
        CCLabel TitelHelp;
        CCRect bounds;
        string[] uitlegdeel1 = new string[5];
        string[] uitlegdeel2 = new string[5];
        string[] uitlegdeel3 = new string[5];
        List<CCLabel> labels = new List<CCLabel>();

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

            bounds = VisibleBoundsWorldspace;

            m_NextPageButton = new Button("->", new CCPoint(bounds.MaxX - 160, bounds.MinY + 100), Font, FontSize, this);
            m_NextPageButton.OnClicked += new ClickEventHandler(OnPageNext);

            m_PrevPageButton = new Button("<-", new CCPoint(bounds.MinX + 160, bounds.MinY + 100), Font, FontSize, this);
            m_PrevPageButton.OnClicked += new ClickEventHandler(OnPagePrev);

            m_BackMenuButton = new Button("<<<", new CCPoint(bounds.MinX + 70, bounds.MaxY - 100), Font, FontSize, this);
            m_BackMenuButton.OnClicked += new ClickEventHandler(OnBackMenu);

            TitelHelp = new CCLabel("Help", "Fonts/Coalition", 70, CCLabelFormat.SpriteFont);
            TitelHelp.Position = new CCPoint(bounds.Center.X, 950);
            AddChild(TitelHelp);
            FillArray();
          //  CreateText(PageCounter);


        }

        public void FillArray()
        {
            uitlegdeel1[0] = "Welkom bij Spacesonne";
            uitlegdeel1[1] = "First we would like to thank you for downloading our app and hope you have fun playing it!";
            uitlegdeel1[2] = "Assuming this is the first time you are playing this game we would advise you to take a quick look at the rules.";
            uitlegdeel1[3] = "By pressing the arrow buttons on the bottom left and right you can walk through the rules of rhis amazing game!";
            checkarray(PageCounter);
        }

           public void checkarray(int w)
        {
            
            bounds = VisibleBoundsWorldspace;
            if (PageCounter == 0)
                for (int i = 0; i < uitlegdeel1.Length; i++)
                {
                    CCLabel cclabel = new CCLabel("" + uitlegdeel1[i], "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
                    cclabel.Position = new CCPoint(bounds.Center.X, (600 - (i * 33)));
                    labels.Add(cclabel);
                }
            
             paintlabels();
        } 
           private void paintlabels()
        {
            foreach (CCLabel p in labels)
                AddChild(p);
        }
        /*  private void CreateText(int PageNumber)
        {
            if (PageNumber == 0)
                firsttext();
            else if (PageNumber == 1)
                secondtext();
               
        }
        */
        private void OnBackMenu()
        {
            MainActivity.SwitchToMenu(SceneIds.OpeningMenu);
        }

        private void OnPagePrev()
        {
            if (PageCounter > 0)
               PageCounter--;
               
            
            MainActivity.SwitchToMenu(SceneIds.HelpMenu);
        }

        private void OnPageNext()
        {
            PageCounter++;
            

        }

      /*     public void firsttext()
        {
            bounds = VisibleBoundsWorldspace;
            CCLabel opentext = new CCLabel("Welkom bij Spacesonne ", "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            CCLabel opentext2 = new CCLabel("First we would like to thank you for downloading our app and hope you have fun playing it!", "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            CCLabel opentext3 = new CCLabel("Assuming this is the first time you are playing this game we would advise you to take a quick look at the rules.", "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            opentext2.Position = new CCPoint(bounds.Center.X, 450);
            opentext3.Position = new CCPoint(bounds.Center.X, 500);
            opentext.Position = new CCPoint(bounds.Center.X, 600);
            AddChild(opentext3);
            AddChild(opentext2);
            AddChild(opentext);
        }

        private void secondtext()
        {
            CCLabel secondtext = new CCLabel("Welkom bij Spacesonne ", "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            AddChild(secondtext);
        } */
    }
}