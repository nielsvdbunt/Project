using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class Board
    {      
        public List<Card> m_virCards;
        public List<CCPoint> m_virLocations;

        public List<Player> m_virAliens;
        public List<CCPoint> m_virAlienLocations;

        public List<CCPoint> m_OpenSpots;

        public Board()
        {
            m_virLocations = new List<CCPoint>();
            m_virCards = new List<Card>();

            m_virAliens = new List<Player>();
            m_virAlienLocations = new List<CCPoint>();

            m_OpenSpots = new List<CCPoint>();
        }

        public void AddCard(Card card, CCPoint p)
        {
            m_virCards.Add(card);
            m_virLocations.Add(p);

            if(m_OpenSpots.Count != 0) // Start tile
            {
                bool b = m_OpenSpots.Remove(p);
                int x = 2;
               // m_OpenSpots.Find(x => x.X == point.X && x.Y == point.Y);
            }

            for (int j = -1; j <= 2; j += 2)
            {
                CCPoint pp = new CCPoint(p.X + j, p.Y);
                if (GetCard(pp) == null)
                {
                    if (!m_OpenSpots.Contains(pp))
                        m_OpenSpots.Add(pp);
                }

                pp = new CCPoint(p.X, p.Y + j);
                if (GetCard(pp) == null)
                {
                    if(!m_OpenSpots.Contains(pp))
                        m_OpenSpots.Add(pp);
                }
            }

            int t = 2;
        }

        public Card GetCard(CCPoint Point)
        {
            for (int t = 0; t < m_virLocations.Count; t++)
            {
                if (m_virLocations[t] == Point)
                {
                    if(t < m_virCards.Count)
                        return m_virCards[t];
                   
                }
            }
            return null;
        }

        public void AddAlien(Player player, CCPoint point)
        {
            m_virAliens.Add(player);
            m_virAlienLocations.Add(point);
        }

        public void RemoveAlien(int x, int y)
        {
            CCPoint location = new CCPoint(x, y);
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