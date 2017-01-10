using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    enum CardAttributes : int
    {
        SpaceStation = 1,
        RainbowRoad = 2,
        Satellite = 3,
        None = 0

    }

    class Card
    {
        public static readonly string[] CardTypes = {
            "11222",
            "10000",
            "20202"     
        };

        string m_Hash;
        CardAttributes[] m_Attributes = new CardAttributes[5];

        public Card(string Hash) 
        {
            if (Hash.Length != 5)
                return;

            m_Hash = Hash;

            for(int i = 0; i < Hash.Length; i++)
            {
                m_Attributes[i] = (CardAttributes)Convert.ToInt32(Hash[i]);
            }
        }

        CardAttributes GetAttribute(int Spot)
        {
            return m_Attributes[Spot];
        }
    }
}