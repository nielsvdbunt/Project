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
            this.AddLayer(m_BackgroundLayer = new BackgroundLayer("Wat moet hier"), 0);
            this.AddLayer(m_BoardLayer = new BoardLayer(), 1);
            this.AddLayer(m_CardAttrLayer = new CardAttributeLayer(), 2);
            this.AddLayer(m_Overlay = new Overlay(), 3);

            m_TeturePool = new TexturePool();
            m_Game = new GameBase(info);
        }
    }
}