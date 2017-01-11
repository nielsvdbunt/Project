using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class Board
    {      
        public List<Card> m_virCards;
        public List<Point> m_virLocations;

        public Board()
        {
            m_virLocations = new List<Point>();
            m_virCards = new List<Card>();
        }

        public void AddCard(Card card, Point point)
        {
            m_virCards.Add(card);
            m_virLocations.Add(point);
        }

        public Card GetCard(int x, int y)
        {
            int i;
            Card c;
            Point location = new Point(x, y);
            for (int t = 0; t < m_virLocations.Count; t++)
            {
                if (m_virLocations[t] == location)
                {
                    i = t;
                    c = m_virCards[i];
                    return c;
                }
            }
            c = new Card("00000");
            return c;
        }
    }
}