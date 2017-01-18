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
        public int CardMultiplier;
        public int Aliens;

    }

    class GameScene : CCScene
    {
        BackgroundLayer m_BackgroundLayer;
        public BoardLayer m_BoardLayer;
        public CardAttributeLayer m_CardAttrLayer;
        public Overlay m_Overlay;
        public TexturePool m_TeturePool;

        GameBase m_Game;

        Card test = new Card(Card.CardTypes[10]);
        
        public GameScene(CCGameView View, InputGameInfo info) : base(View)
        {
            m_Game = new GameBase(this, info);

            this.AddLayer(m_BackgroundLayer = new BackgroundLayer("achtergrond1"), 0);
            this.AddLayer(m_BoardLayer = new BoardLayer());
            this.AddLayer(m_CardAttrLayer = new CardAttributeLayer(), 2);
            this.AddLayer(m_Overlay = new Overlay(this), 3);

            m_TeturePool = new TexturePool();


            var touchListener = new CCEventListenerTouchAllAtOnce();
         //   touchListener.OnTouchesEnded = OnTouchesEnded;
            touchListener.OnTouchesMoved = OnTouchesMoved;
            AddEventListener(touchListener, this);

            m_BoardLayer.AddPanda(500, 500);
            m_BoardLayer.AddPanda(-500, 500);
            m_BoardLayer.AddPanda(2000, 500);
            //m_BoardLayer.AddPanda(0, 132);
        }

        public void StartGame()
        {
            m_BoardLayer.DrawRaster();
            m_Game.Start();
        }

           void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            foreach (CCTouch i in touches)
            {
                if (touches.Count > 0)
                {
                    CCPoint location = touches[0].LocationOnScreen;
                   
                    
                }
            }
        }
        
        void OnTouchesMoved(List<CCTouch> touches, CCEvent touchEvent)
        {
            
            foreach (CCTouch i in touches)
            {
                    //var bounds = VisibleBoundsWorldspace;

                    float x = touches[0].LocationOnScreen.X;
                    float y = touches[0].LocationOnScreen.Y;
                    CCPoint location = m_BackgroundLayer.ScreenToWorldspace(touches[0].LocationOnScreen);
                    //CCPoint location = new CCPoint((x - bounds.Size.Width),(-y + bounds.Size.Height));
                   // CCPoint location = new CCPoint(x, y);
                    m_BoardLayer.Position = location;
                
            }
        }

        public void OnNextClick()
        {
            m_Game.NextTurn();
        }
        
        public void OnRotateLeft()
        {
            test.Rotate(-90);
        }

        public void OnRotateRight()
        {
            test.Rotate(90);
        }

        public void OnAlienClick()
        {

        }
    }
}