using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CocosSharp;

namespace ruigeruben
{
    class BackgroundLayer : CCLayer //this class is for creating the background
    {
        public BackgroundLayer(string filename)
        {
            CCSprite background = new CCSprite(filename);
            background.AnchorPoint = new CCPoint(0, 0);
            AddChild(background);
        }

    }
}