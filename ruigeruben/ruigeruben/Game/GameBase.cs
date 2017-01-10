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

        public void Walktiles(int x, int y)
        {
            for (int i = -1; i <= 2; i += 2)
            {
                Checktiles(x + i, y);
                Checktiles(x, y + i);
            }
            

        }

        public void Checktiles(int x, int y)
        {
            Card CardInHand = new Card("yolo2");
            Card c = m_Board.GetCard(x, y);
            
        }

    }
}