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
        OverlayMenu m_OverlayMenu;
        TexturePool m_TeturePool;
        
        public GameScene(CCGameView View, InputGameInfo info) : base(View)
        {
           // this.AddLayer(new BackgroundLayer());
            this.AddLayer(new OverlayMenu());
        }
    }
}