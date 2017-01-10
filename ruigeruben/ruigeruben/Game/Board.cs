using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class Board
    {      
        List<Card> m_virCards;
        List<Point> m_virLocations;

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
            Card c = new Card("yolo");
            return c;
        }
    }
}