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
        public float CardMultiplier;
        public float Aliens;
    }

    class GameScene : CCScene
    {
        BackgroundLayer m_BackgroundLayer;
        public BoardLayer m_BoardLayer;
        public CardAttributeLayer m_CardAttrLayer;
        bool m_IsCardDragging;
        public Overlay m_Overlay;
        public bool CardOnBoard = false;
        GameBase m_Game;
        CCPoint pp;
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

            Location = m_Overlay.ScreenToWorldspace(Location);
            CCRect p = m_Overlay.m_CardButton.BoundingBox;
            CCRect r = m_Overlay.m_CardButton.BoundingBoxTransformedToWorld;
           
            if (m_Overlay.m_CardButton.BoundingBox.ContainsPoint(Location)) //Voor het slepen van de kaart in layer
            {
                m_IsCardDragging = true;

            }
            //else
                //m_IsCardDragging = false;

        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (m_IsCardDragging)
            {
                var bounds = m_BoardLayer.VisibleBoundsWorldspace;
                CCPoint3 ciw = m_BoardLayer.Camera.CenterInWorldspace;

                float x = bounds.Size.Width / 2 - ciw.X;//bounds.MaxX;// - ciw.X;
                float y = bounds.Size.Height /2 - ciw.Y;//bounds.MaxY;// - ciw.Y;

                m_IsCardDragging = false;

                CCPoint p = m_Overlay.ScreenToWorldspace(touches[0].LocationOnScreen);

                pp = new CCPoint();
                pp.X = p.X - x;
                pp.Y = p.Y - y ;

                pp = m_BoardLayer.toLocation(pp);

                if(m_Game.m_PosiblePos.Contains(pp))
                {
                    m_BoardLayer.DrawCard(m_Game.m_CurrentCard, pp);
                    m_Overlay.m_CardButton.Visible = false;
                    m_Game.m_Board.AddCard(m_Game.m_CurrentCard, pp);
                    CardOnBoard = true;
                }
                else
                    m_Overlay.m_CardButton.Position = m_Overlay.m_CardPos;
            }

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

                foreach (CCTouch i in touches)
                {

                    CCPoint p = i.LocationOnScreen;
                    p = m_Overlay.ScreenToWorldspace(p);
                    //   m_Overlay.m_CardButton.RunAction(new CCMoveTo(0f,p));
                    m_Overlay.m_CardButton.Position = p;
                }


            //        m_Overlay.m_CardButton.RunAction(new CCMoveTo(0f, new CCPoint(touches[0].PreviousLocation.X - x, y - touches[0].PreviousLocationOnScreen.Y)));
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
            if (CardOnBoard)
            {
                m_Game.NextTurn();
                CardOnBoard = false;
            }
        }
        
        public void OnRotateLeft()
        {
            if(CardOnBoard == false)
                 m_Game.RotateCard(-90);
        }

        public void OnRotateRight()
        {
            if(CardOnBoard == false)
                m_Game.RotateCard(90);
        }
        public void OnUndoClick()
        {
            if (CardOnBoard)
            {

                CardOnBoard = false;
                m_Overlay.m_CardButton.Visible = true;
                m_BoardLayer.DeleteCard();
                m_Game.m_Board.RemoveCard(m_Game.m_CurrentCard,pp);
                m_Game.refresh();
               // Overlay.UpdateInterface(m_GameBase.m_Players, d.GetCardsLeft(), m_GameBase.m_CurrentCard);

            }
        }
        public void OnAlienClick()
        {

        }
    }
}