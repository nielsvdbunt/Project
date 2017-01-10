using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CocosSharp;

namespace ruigeruben
{
    class BackgroundLayer : CCLayer
    {
        public BackgroundLayer(string filename)
        {
            CCSprite background = new CCSprite(filename);
            background.AnchorPoint = new CCPoint(0,0);
            AddChild(background);
        }

    }
}