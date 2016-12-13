using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    public class GameLayer : CCLayerColor
    {
        CCLabel label;
        public GameLayer() : base(new CCColor4B(5,30,70))
        {
            label = new CCLabel("SPACE RUBEN", "Fonts/Coalition", 70, CCLabelFormat.SpriteFont);
            
            AddChild(label);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Use the bounds to layout the positioning of our drawable assets
            var bounds = VisibleBoundsWorldspace;
            label.PositionX = bounds.Center.X;
            label.PositionY = 900;

            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            AddEventListener(touchListener, this);
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                // Perform touch handling here
            }
        }
    }
}

