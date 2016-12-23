using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace SpaceSonne
{
    class Board
    {      
        List<List<Card>> m_Cards; // List van een list, eerste is row, 2e is column. Dus als je iets op de 3e rij wilt en 7e colums doe je, m_Cards[3][7]
        int m_GrowSize;

        public Board()
        {
            //Berken de standaard grote en de standaard groei grote.
            m_GrowSize = 4; //random, iets afhangelijk van aantal spelers etc

            //Dit maakt een 10x10 veld om mee te beginnen, we kunnen deze dan uitbreiden als het moet in een bepaalde richting.
            m_Cards = new List<List<Card>>();

            for (int i = 0; i < 10; i++) // Dit kan ook in 1 keer, maar dat wordt super onduidelijk
            {
                List<Card> l = new List<Card>(new Card[10]);
               
                m_Cards.Add(new List<Card>());
            }           
        }

        //Add an extra row
        void GrowHor()
        {
            for(int i = 0; i < m_GrowSize; i++)
            {
                List<Card> l = new List<Card>(new Card[m_Cards[0].Count]);
                m_Cards.Add(new List<Card>());
            }
        }

        //Add an extra column
        void GrowVer()
        {
            for (int i = 0; i < m_GrowSize; i++)
            {
               //List<Card> l = new List<Card>(new Card[m_Cards[0].Count]);
              
                m_Cards.Add(new List<Card>());
            }
        }
    }
}