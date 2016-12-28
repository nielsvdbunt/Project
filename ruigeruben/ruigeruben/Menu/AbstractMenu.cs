using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    abstract class AbstractMenu : CCLayerColor
    {
        public AbstractMenu() : base(CCColor4B.Black)
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

        public static CCGameView GameView
        {
            get;
            private set;
        }

        public static void GoToScene(CCScene scene)
        {
            GameView.Director.ReplaceScene(scene);
        }

        public abstract void OnClick(CCPoint Location);
        public abstract void OnBack();
    }

    /* test test test een andere manier om menu's aan te roepen 
    public static class GameController
    {
        public static CCGameView GameView
        {
            get;
            private set;
        }

        public static void Initialize(CCGameView gameView)
        {
            GameView = gameView;
            var scene = new HelpMenuTest(GameView);
            GameView.Director.RunWithScene(scene);
        }

        public static void GoToScene(CCScene scene)
        {
            GameView.Director.ReplaceScene(scene);
        }
    }
 */
}