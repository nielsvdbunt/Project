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
            string font = "Fonts/Coalition";
            CCColor3B label_color = CCColor3B.White;

            //hiet překrytí staví sám
            CCSprite overlay = new CCSprite("overlay");
            make_sprite("coin", 500, 100);
            make_sprite("alien", 800, 100);
            make_sprite("example", 1050, 100);
            CCSprite left = new CCSprite("rotateleft");
            CCSprite right = new CCSprite("rotateright");
            make_label(currentplayer, font, 36, 200, 100, label_color);
            make_label(currentpoints, font, 36, 650, 100, label_color);
            make_label(currentaliens, font, 36, 900, 100, label_color);

            overlay.AnchorPoint = new CCPoint(0, 0);
            left.Position = new CCPoint(1200, 100);
            right.Position = new CCPoint(1300, 100);

            AddChild(overlay);
            AddChild(left);
            AddChild(right);
        }
        public void make_sprite(string name, int x, int y)
        {
            CCSprite sprite = new CCSprite(name);
            sprite.Position = new CCPoint(x,y);
            AddChild(sprite);
        }
        public void make_label(string text, string font, int textsize, int x, int y, CCColor3B color)
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