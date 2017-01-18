using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class Overlay : AbstractMenu
    {
        string font = "Fonts/Coalition";
        List<Button> buttons = new List<Button>();
        List<CCLabel> labels = new List<CCLabel>();

        public Overlay(GameScene Scene) //Constructor method for creating the static part of the overlay
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
            Button rotateleft = new Button("rotateleft","",new CCPoint(1200,100), font,36, this);
            Button rotateright = new Button("rotateright", "", new CCPoint(1300, 100), font, 36, this);
            Button alien_button= new Button("alien", "", new CCPoint(1450, 100), font, 36, this);
            Button next = new Button("Next", new CCPoint(1750, 100), font, 70, this);
            
            buttons.Add(rotateleft);
            buttons.Add(rotateright);
            buttons.Add(alien_button);
            buttons.Add(next);

            //rotateleft.OnClicked+=
            //rotateright.OnClicked+=
            //alien_button.OnClicked+=
            //next.OnClicked += new ClickEventHandler(OnNextPlayer);
            next.OnClicked += Scene.OnNextClick;

        }

        
        public void update_interface(List<Player> playerlist, int amountoftiles) // in this method the labels and button are made that need to update everytime a player has his turn
        {
            string currentplayer="error";
            string currentpoints = "error";
            string currentaliens="error";
            int gotvet = 0;
            CCColor3B currentcolor = CCColor3B.White;
            CCColor3B label_color = CCColor3B.White;

            foreach(CCLabel l in labels)
            {
                RemoveChild(l);
            }
            labels.Clear();

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
            make_label("X" + amountoftiles.ToString(), font, 36, 1850, 245, label_color);

            //for loop which makes the players on the right who are next in line
            for (int z = 0; z < (playerlist.Count - 1); z++)
            {
                if (t + 1 == playerlist.Count)
                    t = -1;
                make_label(playerlist[t + 1].Name, font, 20, 1820, 1050 - gotvet * 100, playerlist[t+1].PlayerColor);
                make_label(playerlist[t + 1].Points.ToString(), font, 20, 1800, 1000 - gotvet * 100, playerlist[t + 1].PlayerColor);
                make_label(playerlist[t + 1].NumberOfAliens.ToString(), font, 20, 1900, 1000 - gotvet * 100, playerlist[t + 1].PlayerColor);

                CCSprite smallalien = new CCSprite("alien");
                CCSprite smallcoin = new CCSprite("coin");
                smallalien.Scale = 0.25f;
                smallcoin.Scale = 0.25f;
                smallalien.Position = new CCPoint(1860, 1000 - gotvet * 100);
                smallcoin.Position = new CCPoint(1750, 1000 - gotvet * 100);
                AddChild(smallalien);
                AddChild(smallcoin);

                gotvet++;
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
            labels.Add(label);
            AddChild(label);
        }
        public override void OnBack()
        {
            throw new NotImplementedException();
        }

        public override void OnClick(CCPoint Location)
        {
            Location = ScreenToWorldspace(Location);

            foreach (Button b in buttons)
            {
                if (b.OnClickEvent(Location))
                    return;
            }
        }
    }
}