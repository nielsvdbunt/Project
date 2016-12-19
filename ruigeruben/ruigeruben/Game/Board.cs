using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class Board
    {
        List<List<Card>> m_Cards;

        public Board()
        {
            //Dit maakt een 10x10 veld om mee te beginnen, we kunnen deze dan uitbreiden als het moet in een bepaalde richting.
            m_Cards = new List<List<Card>>();

            for (int i = 0; i < 10; i++)
            {
                List<Card> l = new List<Card>();
                for (int j = 0; j < 10; j++)
                {
                    Card c = new Card();
                    l.Add(c);
                }

                m_Cards.Add(new List<Card>());
            }

            
        }
    }
}