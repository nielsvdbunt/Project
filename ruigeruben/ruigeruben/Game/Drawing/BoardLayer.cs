using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CocosSharp;

namespace ruigeruben
{
    class BoardLayer : CCLayer
    {
        float scale = 1;

        //http://stackoverflow.com/questions/31502314/how-to-zoom-with-two-fingers-on-imageview-in-android
        public BoardLayer()
        {
            this.AnchorPoint = new CCPoint(0, 0);
        }

        public void AddPanda(int x, int y)
        {
            CCSprite spr = new CCSprite("Panda");

            spr.PositionX = x;
            spr.PositionY = y;

            AddChild(spr);
        }

        public void t(CCPoint p)
        {
            scale /= 2f;

            CCPoint old = new CCPoint(p.X * this.ScaleX, p.Y * this.ScaleY); // 20, 1 = 20

            this.Scale = scale; // 0.5

            CCPoint New = new CCPoint(p.X * this.ScaleX, p.Y * this.ScaleY);// 20, 0.5 = 10

            CCPoint delta = old - New; // 10

            this.Position += delta;
            

        }

    }
}