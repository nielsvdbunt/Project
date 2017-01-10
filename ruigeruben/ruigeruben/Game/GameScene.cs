using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class GameScene : CCScene
    {   
        enum Layers : int
        {
            Background = 0,
            Tiles,
            
           
        }
        
        public GameScene(CCGameView View) : base(View)
        {
           // this.AddLayer(new BackgroundLayer());
            this.AddLayer(new OverlayMenu());
        }
    }
}