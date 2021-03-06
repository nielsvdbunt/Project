﻿using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class Deck //this clas handles the deck of carfds
    {
        Stack<string> m_Cards;

        public Deck(float CardMultiplier)
        {
            List<string> buf = new List<string>();

            for (int i = 0; i < Card.CardTypes.Length; i++)
            {
                string Name = Card.CardTypes[i];
                int Quantity = Card.CardQuantity[i];
                if ((int)(Quantity * CardMultiplier) != Quantity * CardMultiplier)
                {
                    Quantity = (int)(Quantity * CardMultiplier) + 1;
                }
                else Quantity = (int)(Quantity * CardMultiplier);

                 buf.AddRange(Enumerable.Repeat(Name, Quantity));
            }

            string[] deck = buf.ToArray();

            Random r = new Random();

            for (int n = deck.Length - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);
                string temp = deck[n];
                deck[n] = deck[k];
                deck[k] = temp;
            }

            m_Cards = new Stack<string>(deck);
        }

        public Card GetNextCard()
        {
            return new Card(m_Cards.Pop());
        }
       
        public int GetCardsLeft()
        {
            return m_Cards.Count;
        }


    }
}