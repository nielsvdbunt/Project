using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class Overlay : AbstractMenu
    {
        public Overlay()
        {
            string currentplayer = "gotvet";
            string currentpoints = "8";
            string currentaliens = "6";
            string currenttiles = "123";
            string font = "Fonts/Coalition";
            CCColor3B label_color = CCColor3B.White;

            //here the overlay is put on the screen
            CCSprite overlay = new CCSprite("overlay");
            overlay.AnchorPoint = new CCPoint(0, 0);
            AddChild(overlay);

            //here sprites are made
            make_sprite("coin", 500, 100);
            make_sprite("alien", 800, 100);
            make_sprite("tiles", 1760, 250);

            //here labels are made
            make_label(currentplayer, font, 36, 200, 100, label_color);
            make_label(currentpoints, font, 36, 650, 100, label_color);
            make_label(currentaliens, font, 36, 900, 100, label_color);
            make_label(currenttiles,font , 36 , 1850, 240, label_color);

            //here buttons are made
            Button example= new Button("example", "", new CCPoint(1050, 100), "Fonts/Coalition", 36, this);
            Button rotateleft = new Button("rotateleft","",new CCPoint(1200,100), "Fonts/Coalition",36, this);
            Button rotateright = new Button("rotateright", "", new CCPoint(1300, 100), "Fonts/Coalition", 36, this);
            Button alien_button= new Button("alien", "", new CCPoint(1450, 100), "Fonts/Coalition", 36, this);
            Button next = new Button("next", new CCPoint(1750, 100), "Fonts/Coalition", 70, this);
            
            //example.OnClicked+=
            //rotateleft.OnClicked+=
            //rotateleft.OnClicked+=
            //alien_button.OnClicked+=
            //next.OnClicked+=
        }
        public void make_sprite(string name, int x, int y)//this method is for creating sprites
        {
            CCSprite sprite = new CCSprite(name);
            sprite.Position = new CCPoint(x,y);
            AddChild(sprite);
        }
        public void make_label(string text, string font, int textsize, int x, int y, CCColor3B color)//method for creating simple labels
        {
            CCLabel label = new CCLabel( text, font, textsize,CCLabelFormat.SpriteFont);
            label.Position = new CCPoint(x, y);
            label.Color = color;
            AddChild(label);
        }
        public void change_player()
        {

        }
        public override void OnBack()
        {
            throw new NotImplementedException();
        }

        public override void OnClick(CCPoint Location)
        {
            
        }
    }
}