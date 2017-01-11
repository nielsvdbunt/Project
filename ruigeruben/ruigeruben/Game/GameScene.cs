using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{

    struct InputPlayer
    {
        public string Name;
        public PlayerColor Color;
    }

    struct InputGameInfo
    {
        public List<InputPlayer> Players;
        public int CardMultiplier;

    }

    class GameScene : CCScene
    {
        BackgroundLayer m_BackgroundLayer;
        BoardLayer m_BoardLayer;
        CardAttributeLayer m_CardAttrLayer;
        Overlay m_Overlay;
        TexturePool m_TeturePool;

        GameBase m_Game;

        public GameScene(CCGameView View, InputGameInfo info) : base(View)
        {
            this.AddLayer(m_BackgroundLayer = new BackgroundLayer("achtergrond1"), 0);
            this.AddLayer(m_BoardLayer = new BoardLayer(), 1);
            this.AddLayer(m_CardAttrLayer = new CardAttributeLayer(), 2);
            this.AddLayer(m_Overlay = new Overlay(), 3);

            m_TeturePool = new TexturePool();
            m_Game = new GameBase(info);

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            touchListener.OnTouchesMoved = zoomen();
            AddEventListener(touchListener, this);

            m_BoardLayer.AddPanda(500, 500);
          //  m_BoardLayer.AddPanda(-50, 32);
            //m_BoardLayer.AddPanda(0, 132);
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            foreach (CCTouch i in touches)
            {
                if (touches.Count > 0)
                {
                    CCPoint location = touches[0].LocationOnScreen;

                    m_BoardLayer.t(location);
                }
            }
        }

        void zoomen(List<CC>)

    }
}