using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    abstract class AbstractMenu : CCLayerColor
    {
        public AbstractMenu() : base()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            
            AddEventListener(touchListener, this);
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            foreach (CCTouch i in touches)
            {
                if (touches.Count > 0)
                {
                    CCPoint location = touches[0].LocationOnScreen;
                    OnClick(location);
                }
            }
        }

     

        public abstract void OnClick(CCPoint Location);
        public abstract void OnBack();
    }

    
}