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

        public List<Player> m_virAliens;
        public List<Point> m_virAlienLocations;

        public Board()
        {
            m_virLocations = new List<Point>();
            m_virCards = new List<Card>();

            m_virAliens = new List<Player>();
            m_virAlienLocations = new List<Point>();
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
            return null;
        }

        public void AddAlien(Player player, Point point)
        {
            m_virAliens.Add(player);
            m_virAlienLocations.Add(point);
        }

        public void RemoveAlien(int x, int y)
        {
            Point location = new Point(x, y);
            for (int t = 0; t < m_virAlienLocations.Count; t++)
            {
                if(m_virAlienLocations[t] == location)
                {
                    m_virAliens.RemoveAt(t);
                    m_virAlienLocations.RemoveAt(t);
                }
            }
        }
    }
}