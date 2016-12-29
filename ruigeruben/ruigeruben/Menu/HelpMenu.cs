﻿using System;
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
        List<Button> m_Buttons;
        int PageCounter = 0;
        CCLabel TitelHelp;
        CCRect bounds;
        string[] uitlegdeel1 = new string[5];
        string[] uitlegdeel2 = new string[5];
        string[] uitlegdeel3 = new string[5];
        List<CCLabel> labels = new List<CCLabel>();

        public HelpMenu()
        {
            m_Buttons = new List<Button>();
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
            m_Buttons.Add(m_NextPageButton);

            m_PrevPageButton = new Button("<-", new CCPoint(bounds.MinX + 160, bounds.MinY + 100), Font, FontSize, this);
            m_PrevPageButton.OnClicked += new ClickEventHandler(OnPagePrev);

            m_BackMenuButton = new Button("<<<", new CCPoint(bounds.MinX + 70, bounds.MaxY - 100), Font, FontSize, this);
            m_BackMenuButton.OnClicked += new ClickEventHandler(OnBackMenu);
            m_Buttons.Add(m_BackMenuButton);
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
            uitlegdeel1[3] = "By pressing the arrow buttons on the bottom left and right you can walk through the rules of this amazing game!";
            uitlegdeel2[0] = "test van knop";
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
            else if (PageCounter == 1)
            {
                deletelabels();
                labels.Clear();
                for (int i = 0; i < uitlegdeel2.Length; i++)
                {
                    CCLabel cclabel = new CCLabel("" + uitlegdeel2[i], "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
                    cclabel.Position = new CCPoint(bounds.Center.X, (600 - (i * 33)));
                    labels.Add(cclabel);
                }
            }
            
             paintlabels();
        } 
           private void paintlabels()
        {
            foreach (CCLabel p in labels)
                AddChild(p);
        }

        private void deletelabels()
        {
            foreach (CCLabel p in labels)
                RemoveChild(p);
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
            FillArray();
            

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
    }
}