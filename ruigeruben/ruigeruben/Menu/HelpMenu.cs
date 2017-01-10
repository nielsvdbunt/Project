﻿using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace ruigeruben
{
    class HelpMenu : AbstractMenu
    {
        Button m_NextPageButton;
        Button m_PrevPageButton;
        Button m_BackMenuButton;
        List<Button> m_Buttons;
        int PageCounter = 1;
        CCLabel TitelHelp;
        CCRect bounds;
        string[] uitlegdeel1 = new string[5];
        string[] uitlegdeel2 = new string[2];
        string[] uitlegdeel3 = new string[5];
        string[] uitlegdeel4 = new string[1];
        string[] uitlegdeel5 = new string[2];
        string[] uitlegdeel6 = new string[4];
        string[] uitlegdeel7 = new string[4];
        string[] uitlegdeel8 = new string[4];
        List<CCLabel> labels = new List<CCLabel>();
        List<CCSprite> plaatjes = new List<CCSprite>();

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
            m_Buttons.Add(m_PrevPageButton);

            m_BackMenuButton = new Button("<<<", new CCPoint(bounds.MinX + 70, bounds.MaxY - 100), Font, FontSize, this);
            m_BackMenuButton.OnClicked += new ClickEventHandler(OnBackMenu);
            m_Buttons.Add(m_BackMenuButton);
            TitelHelp = new CCLabel("Help", "Fonts/Coalition", 70, CCLabelFormat.SpriteFont);
            TitelHelp.Position = new CCPoint(bounds.Center.X, 950);
            AddChild(TitelHelp);
            FillArray();
          //  CreateText(PageCounter);


        }
        // er moeten nog sprites aan toegevoegd worden 
        public void FillArray()
        {
            
            uitlegdeel1[0] = "First we would like to thank you for downloading our app and hope you have fun playing it!";
            uitlegdeel1[1] = "Assuming this is the first time you are playing this game we would advise you to take a quick look at the rules.";
            uitlegdeel1[2] = "By pressing the arrow buttons on the bottom left and right you can walk through the rules of this amazing game!";
            uitlegdeel1[3] = "For those who played the board game “Carcassonne” it is very easy, our game has just a different lay-out..";
            uitlegdeel1[4] = "For those who never played the game “Carcassonne” here is a quick explanation.";
            uitlegdeel2[0] = "The game is made out of different playing material.";
            uitlegdeel2[1] = "-   72 board tiles\n- 40 astronauts divided 5 different colours ";
            uitlegdeel3[0] = "Your goal is to get more points than your opponents. Each player lays down a tile on his turn.";
            uitlegdeel3[1] = "These tiles can make: Roads, space stations and satellites.";
            uitlegdeel3[2] = "if you put a new tile on the board you can choose to put an astronaut on it to get points. "; 
            uitlegdeel3[3] = "The point distribution goes automatic in the app and you can see everyone score";
            uitlegdeel3[4] = " but for the curious player we will give a summary of the point distribution goes.";
            uitlegdeel4[0] = "1  Each turn you will get a board tile\n2  you have to put the tile on a legit place on the board\n3  the player chooses whether he plays an alien on his tile or not and on which side of the tile.\n4  If the tile you have put on the board completes a road, space station or satellite\nyou will get points and get your astronauts back.";
            uitlegdeel5[0] = "A road is finished if it has a continuous connection between two points.\nSo for example a road can start in a city and end on a intersection"; // plaatje toevogen 
            uitlegdeel5[1] = "If a player finishes a road the player with the most astronauts on it gets the points the number of points is equal to the number of connected tiles.";
            uitlegdeel6[0] = "A space station is finished when it is completely closed. A space station can contain as many tiles as you want.";
            uitlegdeel6[1] = "You get the points from a space station if you have the most astronauts in it. ";
            uitlegdeel6[2] = "If there are a equal amount of astronauts from two different players both players get all the points.";
            uitlegdeel6[3] = "For a space station you get 2 points per tile. So in the example below the player gets xxxxxxx points"; // sprite toevoegen van spacestation 
            uitlegdeel7[0] = "A satellite is finished when it is surrounded by 8 tiles.";
            uitlegdeel7[1] = "It doesn’t matter what kind of tiles these are it just needs to be surrounded and the player that placed the satellite and placed a astronaut in it gets the points.";
            uitlegdeel7[2] = "For a satellite you get 9 points. If the game is over and you don’t have 8 tiles surrounding your satellite you get 1 point per tile at the end of the game."; //sprite van satelite toevogen 
            uitlegdeel8[0] = "The game is finished when all the tiles are placed. Now the final count starts.";
            uitlegdeel8[1] = "If the game is over and you still have unfinished roads and space stations don’t you worry, you will still get points for these tiles.";
            uitlegdeel8[2] = "First all the unfinished roads space stations and satellites are counted. The owner of a unfinished roads space stations or satellites Gets 1 point for each tile."; 
            checkarray(PageCounter);
        }

        public void AddSprites()
        {

        }

           public void checkarray(int w)
        {
            
            bounds = VisibleBoundsWorldspace;
            if (PageCounter == 1)
            {
                deletelabels();
                labels.Clear();
                CCLabel Welcome = new CCLabel("Welkom bij Spacesonne", "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                Welcome.Position = new CCPoint(bounds.Center.X, 700);
                labels.Add(Welcome);
                for (int i = 0; i < uitlegdeel1.Length; i++)
                {
                    CCLabel cclabel = new CCLabel("" + uitlegdeel1[i], "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
                    cclabel.Position = new CCPoint(bounds.Center.X, (600 - (i * 33)));
                    labels.Add(cclabel);
                }
            }
            else if (PageCounter == 2)
            {
                deletelabels();
                labels.Clear();
                CCLabel components = new CCLabel("Compnents", "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                components.Position = new CCPoint(bounds.Center.X, 700);
                labels.Add(components);
                for (int i = 0; i < uitlegdeel2.Length; i++)
                {
                    CCLabel cclabel = new CCLabel("" + uitlegdeel2[i], "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
                    cclabel.Position = new CCPoint(400, (600 - (i * 100)));
                    labels.Add(cclabel);
                }
            }

            else if (PageCounter == 3)
            {
                deletelabels();
                labels.Clear();
                CCLabel Goaltitle = new CCLabel("The goal of the game", "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                Goaltitle.Position = new CCPoint(bounds.Center.X, 700);
                labels.Add(Goaltitle);
                for (int i = 0; i < uitlegdeel3.Length; i++)
                {
                    CCLabel cclabel = new CCLabel("" + uitlegdeel3[i], "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
                    cclabel.Position = new CCPoint((bounds.MidX), (600 - (i * 33)));
                    labels.Add(cclabel);
                }
            }
            else if (PageCounter == 4)
            {
                deletelabels();
                labels.Clear();

                CCLabel whatturn = new CCLabel("What can I do on my turn", "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                whatturn.Position = new CCPoint(bounds.Center.X, 700);
                labels.Add(whatturn);
                for (int i = 0; i < uitlegdeel4.Length; i++)
                {
                    CCLabel cclabel = new CCLabel("" + uitlegdeel4[i], "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
                    cclabel.Position = new CCPoint(600, (550 - (i * 100)));
                    labels.Add(cclabel);
                }
            }

            else if (PageCounter == 5)
            {
                deletelabels();
                labels.Clear();
                CCLabel FinishedRoad = new CCLabel("How too finish a road?", "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                FinishedRoad.Position = new CCPoint(bounds.Center.X, 700);
                labels.Add(FinishedRoad);
                for (int i = 0; i < uitlegdeel5.Length; i++)
                {
                    CCLabel cclabel = new CCLabel("" + uitlegdeel5[i], "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
                    cclabel.Position = new CCPoint((bounds.MidX), (600 - (i * 400)));
                    labels.Add(cclabel);
                }
            }

            else if (PageCounter == 6)
            {
                deletelabels();
                labels.Clear();
                CCLabel FinishedSpacestation = new CCLabel("How too finish a spacestation?", "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                FinishedSpacestation.Position = new CCPoint(bounds.Center.X, 700);
                labels.Add(FinishedSpacestation);
                for (int i = 0; i < uitlegdeel6.Length; i++)
                {
                    CCLabel cclabel = new CCLabel("" + uitlegdeel6[i], "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
                    cclabel.Position = new CCPoint((bounds.MidX), (600 - (i * 33)));
                    labels.Add(cclabel);
                }
            }

            else if (PageCounter == 7)
            {
                deletelabels();
                labels.Clear();
                CCLabel FinishedSatalite = new CCLabel("How too finish a satelite?", "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                FinishedSatalite.Position = new CCPoint(bounds.Center.X, 700);
                labels.Add(FinishedSatalite);
                for (int i = 0; i < uitlegdeel7.Length; i++)
                {
                    CCLabel cclabel = new CCLabel("" + uitlegdeel7[i], "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
                    cclabel.Position = new CCPoint((bounds.MidX), (600 - (i * 33)));
                    labels.Add(cclabel);
                }
            }

            else if (PageCounter == 8)
            {
                deletelabels();
                labels.Clear();
                CCLabel FinishedGame = new CCLabel("When is the game finished?", "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                FinishedGame.Position = new CCPoint(bounds.Center.X, 700);
                labels.Add(FinishedGame);
                for (int i = 0; i < uitlegdeel8.Length; i++)
                {
                    CCLabel cclabel = new CCLabel("" + uitlegdeel8[i], "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
                    cclabel.Position = new CCPoint((bounds.MidX), (600 - (i * 33)));
                    labels.Add(cclabel);
                }

                CCLabel EndofHalp = new CCLabel("You are ready to play the game now.\nGood luck but most of all have fun!", "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
                EndofHalp.Position = new CCPoint(bounds.Center.X, 300);
                labels.Add(EndofHalp);
            }

            else
            {
                OnBackMenu();
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
               FillArray();
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