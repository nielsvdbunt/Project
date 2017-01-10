using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    
    class GameBase
    {

        List<Player> DeSpelers;


        public void SpelersToevoegen()
        {
            DeSpelers = new List<Player>();
            Player Ruub = new Player();
            Ruub.Name = "Ruben"; 
            
        }

    }

    
}