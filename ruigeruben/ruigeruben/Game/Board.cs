using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    struct Alien
    {
        public CCPoint m_Point;
        public int m_CardAttr;
        public Player m_Player;
    }

    class Board //this class handles all the tiles and aliens
    {      
        public List<Card> m_virCards;
        public List<CCPoint> m_virLocations;

        public List<Alien> m_Aliens;
        
        public List<CCPoint> m_OpenSpots;

        public Board()
        {
            m_virLocations = new List<CCPoint>();
            m_virCards = new List<Card>();

            m_Aliens = new List<Alien>();

            m_OpenSpots = new List<CCPoint>();
        }

        public void RemoveCard(Card c, CCPoint p)
        {

            m_virCards.Remove(c);
            m_virLocations.Remove(p);
            for(int i= -1; i <2; i+=2 )
                m_OpenSpots.Remove(new CCPoint ((p.X + i), p.Y));
            for (int j = -1; j < 2; j+=2)
                m_OpenSpots.Remove(new CCPoint(p.X, (p.Y + j)));
            m_OpenSpots.Add(p);
            
           
        }

        public void AddCard(Card card, CCPoint p)
        {
            m_virCards.Add(card);
            m_virLocations.Add(p);

            if (m_OpenSpots.Count != 0) // Start tile
            {
                bool b = m_OpenSpots.Remove(p);
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
                    if (!m_OpenSpots.Contains(pp))
                        m_OpenSpots.Add(pp);
                }
            }
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

        public void AddAlien(Player player, CCPoint point, int CardAttr)
        {
            Alien all = new Alien();
            all.m_CardAttr = CardAttr;
            all.m_Point = point;
            all.m_Player = player;
            m_Aliens.Add(all);
        }

        public void RemoveAlien(CCPoint point)
        {
            int i = -1;
            for (int t = 0; t < m_Aliens.Count; t++)
            {
                if(m_Aliens[t].m_Point == point)
                {
                    i = t;
                }
            }

            if (i != -1)
                m_Aliens.RemoveAt(i);
        }

        public Player HasAlien(CCPoint CardPoint, int AlienPoint)
        {
            foreach(Alien i in m_Aliens)
            {
                if(i.m_Point == CardPoint)
                {
                    if (i.m_CardAttr == AlienPoint)
                        return i.m_Player;
                }
            }
            return null;
        }
    }
}