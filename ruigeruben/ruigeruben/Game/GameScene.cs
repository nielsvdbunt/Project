﻿using System;
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

    class GameScene : CCScene //gamescene displays all the layers and handles all the toucheventhandlers
    {
        BackgroundLayer m_BackgroundLayer;
        public BoardLayer m_BoardLayer;
        bool m_IsCardDragging;
        public Overlay m_Overlay;
        public bool m_CardPutDown = false;
        public bool m_AllienPutDown = false;
        public int m_AllienCardSpot = 0;
        GameBase m_Game;
        int m_Touches;
        public CCPoint m_PlacedCard;

        public GameScene(CCGameView View, InputGameInfo info) : base(View)
        {
            m_Game = new GameBase(this, info);

            this.AddLayer(m_BackgroundLayer = new BackgroundLayer("achtergrond1"), 0);
            this.AddLayer(m_BoardLayer = new BoardLayer(), 1);
            this.AddLayer(m_Overlay = new Overlay(this), 2);

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
            if (touches.Count <= 0)
                return;

            float x = touches[0].LocationOnScreen.X;
            float y = touches[0].LocationOnScreen.Y;

            CCPoint Location = new CCPoint(x, y);

            if (y <= 1200 && x <= 2300) //  Test if click on overlay
            {
                m_Touches += touches.Count;
            }

            Location = m_Overlay.ScreenToWorldspace(Location);
           
            if (m_Overlay.m_CardButton.BoundingBox.ContainsPoint(Location)) //for draging
            {
                if (!m_CardPutDown)
                    m_IsCardDragging = true;

            }

            if(m_CardPutDown && m_AllienPutDown == false && m_Game.m_CurrentPlayer.NumberOfAliens > 0)
            {
                for(int i = 0; i < m_BoardLayer.PossiblePositionsAliens.Count; i++)
                {
                    CCPoint pos = m_BoardLayer.ScreenToWorldspace(touches[0].LocationOnScreen);

                    if (m_BoardLayer.PossiblePositionsAliens[i].BoundingBoxTransformedToWorld.ContainsPoint(pos))
                    {
                        m_AllienCardSpot = m_BoardLayer.m_AllienCardPos[i];
                        m_AllienPutDown = true;
                        m_BoardLayer.DrawAlien(m_BoardLayer.PossiblePositionsAliens[i].PositionWorldspace, m_Game.m_CurrentPlayer.PlayerColor);
                        m_BoardLayer.DeleteCircles();
                        m_Game.m_CurrentPlayer.NumberOfAliens = m_Game.m_CurrentPlayer.NumberOfAliens - 1;
                        return;
                    }
                }
               
            }
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (m_IsCardDragging)
            {
                m_IsCardDragging = false;

                CCPoint p = m_BoardLayer.ScreenToWorldspace(touches[0].LocationOnScreen);
                CCPoint pp = m_BoardLayer.toLocation(p);

                if (m_Game.m_PosiblePos.Contains(pp))
                {
                    m_BoardLayer.DrawCard(m_Game.m_CurrentCard, pp);
                    m_PlacedCard = pp;
                    m_Overlay.m_CardButton.Visible = false;
                    m_BoardLayer.RemoveRaster();
                    m_CardPutDown = true;
                    if (m_Game.m_CurrentPlayer.NumberOfAliens > 0)
                    {
                        m_BoardLayer.DrawAlienPossiblePosition(m_Game.m_CurrentCard, pp);
                    }
    
                }

    m_Overlay.m_CardButton.Position = m_Overlay.m_CardPos;
            }
            m_Touches -= touches.Count;

            if (m_Touches < 0)
                m_Touches = 0;


        }

        void OnTouchesMoved(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count <= 0)
                return;

            float x = touches[0].LocationOnScreen.X;
            float y = touches[0].LocationOnScreen.Y;

            if (m_IsCardDragging)
            {
                CCSprite Spr = TexturePool.GetSprite(m_Game.m_CurrentCard.m_Hash);
                Spr.Position = touches[0].LocationOnScreen;

                foreach (CCTouch i in touches)
                {

                    CCPoint p = i.LocationOnScreen;
                    p = m_Overlay.ScreenToWorldspace(p);
                    m_Overlay.m_CardButton.Position = p;
                }
            }
            else
            {
                if (m_Touches == 1) // Pan
                {

                    foreach (CCTouch i in touches)
                    {
                        var s = m_BoardLayer.Camera.CenterInWorldspace;
                        s.X += i.PreviousLocationOnScreen.X - i.LocationOnScreen.X;
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
                        CCPoint third = touches[i ].PreviousLocationOnScreen;
                        CCPoint four = touches[i + 1].PreviousLocationOnScreen;

                        float one = CCPoint.Distance(fir, sec);
                        float two = CCPoint.Distance(third, four);

                        if (one < two)
                            m_BoardLayer.UpdateScale(-0.05f);
                        else
                            m_BoardLayer.UpdateScale(0.05f);
                    }
                }
            }
        }

        public void OnNextClick()
        {
            if (m_CardPutDown)
            {
                m_Game.m_Board.AddCard(m_Game.m_CurrentCard, m_PlacedCard);
                if (m_AllienPutDown)
                    m_Game.m_Board.AddAlien(m_Game.m_CurrentPlayer, m_PlacedCard, m_AllienCardSpot); 
                m_BoardLayer.DeleteCircles();
                m_Game.NextTurn();
                m_CardPutDown = false;
                m_AllienPutDown = false;
            }
        }

        public void OnRotateLeft()
        {
            if (m_CardPutDown == false)
                m_Game.RotateCard(-90);
        }

        public void OnRotateRight()
        {
            if (m_CardPutDown == false)
                m_Game.RotateCard(90);
        }
        public void OnUndoClick()

        {   
            if (m_CardPutDown )
            {
                if (m_AllienPutDown)
                {
                    m_BoardLayer.DeleteLastAlien();
                    m_BoardLayer.DrawAlienPossiblePosition(m_Game.m_CurrentCard, m_PlacedCard);
                    m_AllienPutDown = false;
                }
                else
                {
                    m_CardPutDown = false;
                    m_Overlay.m_CardButton.Visible = true;
                    m_BoardLayer.DeleteLastCard();
                    m_BoardLayer.DeleteCircles();
                    m_Game.refresh();
                }
            }
          
        }
        public void OnAlienClick()
        {

        }

        public CCPoint GetPlacedCard()
        {
            return m_PlacedCard; 
        }
    }
}