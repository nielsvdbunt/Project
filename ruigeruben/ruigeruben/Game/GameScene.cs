using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{

    struct InputPlayer
    {
        public string Name;
        public CCColor3B Color;
    }

    struct InputGameInfo
    {
        public List<InputPlayer> Players;
        public int CardMultiplier;
        public int Aliens;
    }

    class GameScene : CCScene
    {
        BackgroundLayer m_BackgroundLayer;
        public BoardLayer m_BoardLayer;
        public CardAttributeLayer m_CardAttrLayer;
        bool m_IsCardDragging;
        public Overlay m_Overlay;
       
        GameBase m_Game;
    
        int m_Touches;
      
        public GameScene(CCGameView View, InputGameInfo info) : base(View)
        {
            m_Game = new GameBase(this, info);

            this.AddLayer(m_BackgroundLayer = new BackgroundLayer("achtergrond1"), 0);
            this.AddLayer(m_BoardLayer = new BoardLayer());
            this.AddLayer(m_CardAttrLayer = new CardAttributeLayer(), 2);
            this.AddLayer(m_Overlay = new Overlay(this), 3);          

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            touchListener.OnTouchesBegan = OnTouchesBegan;
            touchListener.OnTouchesMoved = OnTouchesMoved;
            AddEventListener(touchListener, this);
        }

     
        public void StartGame()
        {
            m_Game.Start();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            var s = m_BoardLayer.Camera.CenterInWorldspace;
            s.X = 0;
            s.Y = 0;
            m_BoardLayer.Camera.CenterInWorldspace = s;

            var target = m_BoardLayer.Camera.TargetInWorldspace;
            target.X = s.X;
            target.Y = s.Y;
            m_BoardLayer.Camera.TargetInWorldspace = target;
        }


        public void OnTouchesBegan(List<CCTouch> touches, CCEvent touchEvent)
        {
            if(touches.Count <= 0)
                return;
            
            float x = touches[0].LocationOnScreen.X;
            float y = touches[0].LocationOnScreen.Y;     

            CCPoint Location = new CCPoint(x, y);
 
            if (y <= 1200 && x <= 2300) //  Test if click on overlay
            {
                m_Touches += touches.Count;
            }

            
            if (x > 1295 && x < 1430 && touches.Count > 0) //Voor het slepen van de kaart in layer
            {
                m_IsCardDragging = true;

            }
            else
                m_IsCardDragging = false;

        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (m_Touches == 2)
                zooming = false;

            m_Touches -= touches.Count;

            if (m_Touches < 0)
                m_Touches = 0;
            
               
        }
        float scale = 1;
        CCPoint m_mid = new CCPoint();
        bool zooming = false;

        void OnTouchesMoved(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count <= 0)
                return;

            float x = touches[0].LocationOnScreen.X;
            float y = touches[0].LocationOnScreen.Y;

            if (m_IsCardDragging)
            {
                //if(m_Game.m_CurrentCard != null)
                CCSprite Spr = TexturePool.GetSprite(m_Game.m_CurrentCard.m_Hash);
                Spr.Position = touches[0].LocationOnScreen;
               
                m_Overlay.m_CardButton.RunAction(new CCMoveTo(0f, new CCPoint(x, y)));
                //AddChild(Spr);

            }
            else
            {
                if (m_Touches == 1) // Pan
                {

                    foreach (CCTouch i in touches)
                    {
                        var s = m_BoardLayer.Camera.CenterInWorldspace;
                        s.X += i.PreviousLocationOnScreen.X - i.LocationOnScreen.X;//i.LocationOnScreen.X - i.PreviousLocationOnScreen.X;
                        s.Y += i.LocationOnScreen.Y - i.PreviousLocationOnScreen.Y;
                        m_BoardLayer.Camera.CenterInWorldspace = s;

                        var target = m_BoardLayer.Camera.TargetInWorldspace;
                        target.X = s.X;
                        target.Y = s.Y;
                        m_BoardLayer.Camera.TargetInWorldspace = target;
                    }
                }
                else if (m_Touches == 2) // Zoom
                {
                    if (touches.Count < 2)
                        return;

                    for (int i = 0; i < touches.Count; i += 2)
                    {
                        CCPoint fir = touches[i].LocationOnScreen;
                        CCPoint sec = touches[i + 1].LocationOnScreen;

                        CCPoint mid = m_BackgroundLayer.ConvertToWorldspace(fir - sec);
                        mid.X = Math.Abs(mid.X);
                        mid.Y = Math.Abs(mid.Y);

                        if (zooming)
                        {
                            if (mid.Length < m_mid.Length)
                            {

                            }
                        }

                        scale += 0.001f;
                        m_BoardLayer.Scale = scale;

                        var s = m_BoardLayer.Camera.CenterInWorldspace;
                        s.X = mid.X;//i.LocationOnScreen.X - i.PreviousLocationOnScreen.X;
                        s.Y = mid.Y;
                        m_BoardLayer.Camera.CenterInWorldspace = s;

                        var target = m_BoardLayer.Camera.TargetInWorldspace;
                        target.X = mid.X;
                        target.Y = mid.Y;
                        m_BoardLayer.Camera.TargetInWorldspace = target;

                    }
                }
            } 
        }

        public void OnNextClick()
        {
            m_Game.NextTurn();
        }
        
        public void OnRotateLeft()
        {
            m_Game.RotateCard(-90);
        }

        public void OnRotateRight()
        {
            m_Game.RotateCard(90);
        }

        public void OnAlienClick()
        {

        }
    }
}