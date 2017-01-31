using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class Overlay : AbstractMenu
    {
        string m_font = "Fonts/Coalition";
        List<Button> m_buttons = new List<Button>();
        List<CCLabel> m_labels = new List<CCLabel>();
        List<CCSprite> m_sprites = new List<CCSprite>();
        public CCSprite m_CardButton;
        public CCPoint m_CardPos = new CCPoint(1110, 100);

        public Overlay(GameScene Scene) //Constructor method for creating the static part of the overlay
        {
            //here the overlay is put on the screen
            CCSprite overlay = new CCSprite("overlay");
            overlay.AnchorPoint = new CCPoint(0, 0);
            AddChild(overlay);

            //here sprites are made
            MakeSprite("coin", 580, 100);
            MakeSprite("alien", 850, 100);

            //here boxes are made around the buttons
            MakeBox(160, 35 ,200, 60, 2);
            MakeBox(1235, 55, 90, 90, 2);
            MakeBox(1355, 55, 90, 90, 2);
            MakeBox(1510, 55, 375, 105, 2);

            //here buttons are made
            Button rotateleft = new Button("rotateleft","",new CCPoint(1280,100), m_font, 36, this);
            Button rotateright = new Button("rotateleft", "", new CCPoint(1400, 100), m_font, 36, this);
            rotateright.m_Sprite.FlipX = true;
            Button next = new Button("Next", new CCPoint(1700, 100), m_font, 70, this);
            Button undo = new Button("Undo", new CCPoint(260, 60), m_font, 36, this);

            m_CardButton = new CCSprite();
            AddChild(m_CardButton);

            m_buttons.Add(undo);
            m_buttons.Add(rotateleft);
            m_buttons.Add(rotateright);
            m_buttons.Add(next);
           
            rotateleft.OnClicked += Scene.OnRotateLeft;
            rotateright.OnClicked += Scene.OnRotateRight;  
            
            next.OnClicked += Scene.OnNextClick;
            undo.OnClicked += Scene.OnUndoClick;

        }

        public void UpdateInterface(List<Player> playerlist, int amountoftiles, Card tile) // in this method the labels and button are made that need to update everytime a player has his turn
        {
            string currentplayer = "error";
            string currentpoints = "error";
            string currentaliens = "error";
            int t;
            int gotvet = 0;
            CCColor3B currentcolor = CCColor3B.White;
            CCColor3B label_color = CCColor3B.White;

            foreach(CCLabel l in m_labels)
            {
                RemoveChild(l);
            }
            m_labels.Clear();

            foreach(CCSprite s in m_sprites)
            {
                RemoveChild(s);
            }
            m_sprites.Clear();

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

            MakeLabel(currentplayer, m_font, 36, 260, 140, currentcolor);
            MakeLabel(currentpoints, m_font, 36, 730, 100, label_color);
            MakeLabel(currentaliens, m_font, 36, 950, 100, label_color);
            MakeLabel(amountoftiles.ToString() + "X", m_font, 36, 1810, 245, label_color);

            for (int z = 0; z < (playerlist.Count - 1); z++) //for loop which makes the players on the right who are next in line
            {
                if (t + 1 == playerlist.Count)
                    t = -1;
                MakeLabel(playerlist[t + 1].Name, m_font, 20, 1820, 1050 - gotvet * 100, playerlist[t+1].PlayerColor);
                MakeLabel(playerlist[t + 1].Points.ToString(), m_font, 20, 1800, 1000 - gotvet * 100, playerlist[t + 1].PlayerColor);
                MakeLabel(playerlist[t + 1].NumberOfAliens.ToString(), m_font, 20, 1890, 1000 - gotvet * 100, playerlist[t + 1].PlayerColor);

                CCSprite smallalien = new CCSprite("alien");
                CCSprite smallcoin = new CCSprite("coin");
                smallalien.Scale = 0.25f;
                smallcoin.Scale = 0.25f;
                smallalien.Position = new CCPoint(1850, 1000 - gotvet * 100);
                smallcoin.Position = new CCPoint(1750, 1000 - gotvet * 100);
                AddChild(smallalien);
                AddChild(smallcoin);
                m_sprites.Add(smallalien);
                m_sprites.Add(smallcoin);

                gotvet++;
                t++;
           }
            RemoveChild(m_CardButton);
            m_CardButton = TexturePool.GetSprite(tile.m_Hash);
            m_CardButton.Position = m_CardPos;
            m_CardButton.Rotation = tile.GetRotation();
          
            AddChild(m_CardButton);


        }

        private void MakeSprite(string name, int x, int y)//this method is for creating sprites
        {
            CCSprite sprite = new CCSprite(name);
            sprite.Position = new CCPoint(x, y);
            AddChild(sprite);
        }

        private void MakeLabel(string text, string font, int textsize, int x, int y, CCColor3B color)//method for creating simple labels
        {
            CCLabel label = new CCLabel(text, font, textsize, CCLabelFormat.SpriteFont);
            label.Position = new CCPoint(x, y);
            label.Color = color;
            m_labels.Add(label);
            AddChild(label);
        }
        private void MakeBox(int xpos, int ypos, int width, int height, int thickness)
        {
            var drawNode = new CCDrawNode();
            var rect = new CCRect(xpos, ypos, width, height);
            drawNode.DrawRect(rect, fillColor: CCColor4B.Transparent, borderWidth: thickness, borderColor: CCColor4B.White);
            AddChild(drawNode);
        }
        public override void OnBack()
        {
           
        }

        public override void OnClick(CCPoint Location)
        {
            Location = ScreenToWorldspace(Location);

            foreach (Button b in m_buttons)
            {
                if (b.OnClickEvent(Location))
                    return;
            }
        }
    }
}