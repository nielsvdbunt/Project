using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    public class HelpMenuTest : CCScene
    {
        CCLayer layer;

        public HelpMenuTest(CCGameView gameView) : base(gameView)
        {
            layer = new CCLayer();
            this.AddChild(layer);

            CreateText();
          


        }

        private void CreateText()
        {
            var label = new CCLabel("Tap to begin", "Arial", 30, CCLabelFormat.SystemFont);
            label.PositionX = layer.ContentSize.Width / 2.0f;
            label.PositionY = layer.ContentSize.Height / 2.0f;

            layer.AddChild(label);
        }
    }
}