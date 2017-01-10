using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CocosSharp;

namespace ruigeruben
{
    class BoardLayer : CCLayer
    {
        //http://stackoverflow.com/questions/31502314/how-to-zoom-with-two-fingers-on-imageview-in-android
        public BoardLayer()
        {
            
        }

        public void AddPanda(int x, int y)
        {
            CCSprite spr = new CCSprite("Panda");
            spr.PositionX = x;
            spr.PositionY = y;

            AddChild(spr);
        }

        public void t()
        {
            this.Scale = 2;
        }

    }
}