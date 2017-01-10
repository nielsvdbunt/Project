using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class GameBase
    {
        List<Player> m_Players;
        Deck m_Deck;
        Board m_Board;

        public GameBase(InputGameInfo info)
        {
            m_Board = new Board();
            m_Deck = new Deck(1);

        }

    }
}