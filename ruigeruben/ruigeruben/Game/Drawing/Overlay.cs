﻿using System;
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
        List<CCSprite> sprites = new List<CCSprite>();
        CCSprite m_AlienButton;
        CCSprite m_CardButton;

        public Overlay(GameScene Scene) //Constructor method for creating the static part of the overlay
        {
            //here the overlay is put on the screen
            CCSprite overlay = new CCSprite("overlay");
            overlay.AnchorPoint = new CCPoint(0, 0);
            AddChild(overlay);

            //here sprites are made
            MakeSprite("coin", 500, 100);
            MakeSprite("alien", 770, 100);
            MakeSprite("tiles", 1760, 250);

            MakeBox(1125, 55, 90, 90, 1);
            MakeBox(1245, 55, 90, 90, 1);
            MakeBox(1370, 5, 110 , 190, 2);
            MakeBox(1510, 55, 375, 105, 2);

            //here buttons are made
            Button rotateleft = new Button("rotateleft","",new CCPoint(1170,100), font, 36, this);
            Button rotateright = new Button("rotateleft", "", new CCPoint(1290, 100), font, 36, this);
            rotateright.m_Sprite.FlipX = true;
            Button next = new Button("Next", new CCPoint(1700, 100), font, 70, this);
            m_AlienButton = new CCSprite("alien1");
            m_AlienButton.Position = new CCPoint(1420, 100);
            AddChild(m_AlienButton);

            m_CardButton = new CCSprite();
            AddChild(m_CardButton);

            buttons.Add(rotateleft);
            buttons.Add(rotateright);
            buttons.Add(next);
           
            rotateleft.OnClicked += Scene.OnRotateLeft;
            rotateright.OnClicked += Scene.OnRotateRight;
           
            next.OnClicked += Scene.OnNextClick;

        }

        public void update_interface(List<Player> playerlist, int amountoftiles, Card tile) // in this method the labels and button are made that need to update everytime a player has his turn
        {
            string currentplayer = "error";
            string currentpoints = "error";
            string currentaliens = "error";
            int t;
            int gotvet = 0;
            CCColor3B currentcolor = CCColor3B.White;
            CCColor3B label_color = CCColor3B.White;

            foreach(CCLabel l in labels)
            {
                RemoveChild(l);
            }
            labels.Clear();

            foreach(CCSprite s in sprites)
            {
                RemoveChild(s);
            }
            sprites.Clear();

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

            MakeLabel(currentplayer, font, 36, 200, 100, currentcolor);
            MakeLabel(currentpoints, font, 36, 650, 100, label_color);
            MakeLabel(currentaliens, font, 36, 870, 100, label_color);
            MakeLabel("X" + amountoftiles.ToString(), font, 36, 1850, 245, label_color);
            m_AlienButton.Color = currentcolor;

            for (int z = 0; z < (playerlist.Count - 1); z++) //for loop which makes the players on the right who are next in line
            {
                if (t + 1 == playerlist.Count)
                    t = -1;
                MakeLabel(playerlist[t + 1].Name, font, 20, 1820, 1050 - gotvet * 100, playerlist[t+1].PlayerColor);
                MakeLabel(playerlist[t + 1].Points.ToString(), font, 20, 1800, 1000 - gotvet * 100, playerlist[t + 1].PlayerColor);
                MakeLabel(playerlist[t + 1].NumberOfAliens.ToString(), font, 20, 1900, 1000 - gotvet * 100, playerlist[t + 1].PlayerColor);

                CCSprite smallalien = new CCSprite("alien");
                CCSprite smallcoin = new CCSprite("coin");
                smallalien.Scale = 0.25f;
                smallcoin.Scale = 0.25f;
                smallalien.Position = new CCPoint(1860, 1000 - gotvet * 100);
                smallcoin.Position = new CCPoint(1750, 1000 - gotvet * 100);
                AddChild(smallalien);
                AddChild(smallcoin);
                sprites.Add(smallalien);
                sprites.Add(smallcoin);

                gotvet++;
                t++;
           }
            RemoveChild(m_CardButton);
            m_CardButton = TexturePool.GetSprite(tile.m_Hash);
            m_CardButton.Position = new CCPoint(1020, 100);
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
            labels.Add(label);
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