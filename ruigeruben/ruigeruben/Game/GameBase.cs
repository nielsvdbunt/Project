using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class GameBase
    {
        GameScene m_Scene;
        public List<Player> m_Players;
        Deck m_Deck;
        public Board m_Board;

        public GameBase(GameScene Scene, InputGameInfo info)
        {
            m_Scene = Scene;
            m_Board = new Board();
            m_Deck = new Deck(1);

        }

        public GameBase()
        {
        }
        public void NextTurn()
        {

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