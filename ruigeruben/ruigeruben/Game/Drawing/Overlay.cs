using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class Overlay : AbstractMenu
    {
        string font = "Fonts/Coalition";
        int gotvet=0;

        public Overlay() //Constructor method for creating the static part of the overlay
        {
            //here the overlay is put on the screen
            CCSprite overlay = new CCSprite("overlay");
            overlay.AnchorPoint = new CCPoint(0, 0);
            AddChild(overlay);

            //here sprites are made
            make_sprite("coin", 500, 100);
            make_sprite("alien", 800, 100);
            make_sprite("tiles", 1760, 250);

            //here buttons are made
            Button rotateleft = new Button("rotateleft","",new CCPoint(1200,100), "Fonts/Coalition",36, this);
            Button rotateright = new Button("rotateright", "", new CCPoint(1300, 100), "Fonts/Coalition", 36, this);
            Button alien_button= new Button("alien", "", new CCPoint(1450, 100), "Fonts/Coalition", 36, this);
            Button next = new Button("next", new CCPoint(1750, 100), "Fonts/Coalition", 70, this);
            
            //rotateleft.OnClicked+=
            //rotateleft.OnClicked+=
            //alien_button.OnClicked+=
            //next.OnClicked+=

            List<Player> playerlist = new List<Player>();

            Player player1 = new Player();
            Player player2 = new Player();
            Player player3 = new Player();

            player1.Name = "gotvet";
            player1.Turn = true;
            player1.Points = 30;
            player1.NumberOfAliens = 7;
            player1.PlayerColor = CCColor3B.Red;

            player2.Name = "Ruben";
            player2.Turn = false;
            player2.Points = -20;
            player2.NumberOfAliens = 4;
            player2.PlayerColor = CCColor3B.Blue;

            player3.Name = "Bart";
            player3.Turn = false;
            player3.Points = 10;
            player3.NumberOfAliens = 8;
            player3.PlayerColor = CCColor3B.Yellow;

            playerlist.Add(player1);
            playerlist.Add(player2);
            playerlist.Add(player3);

            int numberoftiles = 123;

            update_interface(playerlist, numberoftiles);
        }
        public void update_interface(List<Player> playerlist, int amountoftiles)// in this method the labels and button are made that need to update everytime a player has his turn
        {
            string currentplayer="error";
            string currentpoints = "error";
            string currentaliens="error";
            CCColor3B currentcolor = CCColor3B.White;
            CCColor3B label_color = CCColor3B.White;

            this.Cleanup();
            int t;
            for (t = 0; t < playerlist.Count; t++) //for loop for creating the values for the currentplayer
            {
                if (playerlist[t].Turn == true)
                {
                    currentplayer = playerlist[t].Name;
                    currentpoints = playerlist[t].Points.ToString();
                    currentaliens = playerlist[t].NumberOfAliens.ToString();
                    currentcolor = playerlist[t].PlayerColor;
                    break;
                }
            }

            make_label(currentplayer, font, 36, 200, 100, currentcolor);
            make_label(currentpoints, font, 36, 650, 100, label_color);
            make_label(currentaliens, font, 36, 900, 100, label_color);
            make_label(amountoftiles.ToString(), font, 36, 1850, 240, label_color);

            //for loop which makes the players on the right who are next in line
            for (int z=0; z<(playerlist.Count-1);z++)
            {
               if (t + 1 == playerlist.Count)
                   t = -1;
                make_playerlabel(playerlist[t + 1].Name, playerlist[t + 1].Points.ToString(), playerlist[t + 1].NumberOfAliens.ToString(), playerlist[t+1].PlayerColor);
                t++;
            }

            Button example = new Button("example", "", new CCPoint(1050, 100), "Fonts/Coalition", 36, this);

            //example.OnClicked+=

        }
        public void make_sprite(string name, int x, int y)//this method is for creating sprites
        {
            CCSprite sprite = new CCSprite(name);
            sprite.Position = new CCPoint(x, y);
            AddChild(sprite);
        }
        public void make_label(string text, string font, int textsize, int x, int y, CCColor3B color)//method for creating simple labels
        {
            CCLabel label = new CCLabel(text, font, textsize, CCLabelFormat.SpriteFont);
            label.Position = new CCPoint(x, y);
            label.Color = color;
            AddChild(label);
        }
        public void make_playerlabel(string name, string points, string aliens, CCColor3B color)//method for creating a playerlabel
        {
            make_label(name, font, 20, 1800, 1050-gotvet*100, color);
            make_label(points, font, 20, 1800, 1000-gotvet*100, color);
            make_label(aliens, font, 20, 1900, 1000-gotvet*100, color);
            CCSprite smallalien = new CCSprite("alien");
            CCSprite smallcoin = new CCSprite("coin");
            smallalien.Scale = 0.25f;
            smallcoin.Scale = 0.25f;
            smallalien.Position = new CCPoint(1750,1000-gotvet*100);
            smallcoin.Position = new CCPoint(1860, 1000-gotvet*100);
            AddChild(smallalien);
            AddChild(smallcoin);
            gotvet++;
        }
        public override void OnBack()
        {
            throw new NotImplementedException();
        }

        public override void OnClick(CCPoint Location)//this method is empty because nothing is supposed to happen when you click on it
        {
            
        }
    }
}