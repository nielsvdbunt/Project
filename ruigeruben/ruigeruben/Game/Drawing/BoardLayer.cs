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
          
            this.Scale = scale;
            this.Position = p;
            

        }

    }
}