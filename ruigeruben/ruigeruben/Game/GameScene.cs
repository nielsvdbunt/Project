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
        public Overlay m_Overlay;
        public TexturePool m_TeturePool;

        GameBase m_Game;

        int m_Touches;

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
            touchListener.OnTouchesEnded = OnTouchesEnded;
            touchListener.OnTouchesBegan = OnTouchesBegan;
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

        public void OnTouchesBegan(List<CCTouch> touches, CCEvent touchEvent)
        {
            m_Touches += touches.Count;
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            m_Touches -= touches.Count;

            if (m_Touches < 0)
                m_Touches = 0;
        }
        float scale = 1;
        void OnTouchesMoved(List<CCTouch> touches, CCEvent touchEvent)
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
            else if(m_Touches == 2) // Zoom
            {
                if (touches.Count < 2)
                    return;

                for(int i = 0; i < touches.Count; i++)
                {

                    //CCPoint Location = i.LocationOnScreen;
                    //Location = m_BoardLayer.ConvertToWorldspace(Location);
                    scale += 0.10f;
                    m_BoardLayer.Scale = scale;
                }           
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