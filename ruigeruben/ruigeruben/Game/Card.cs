using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace SpaceSonne
{
    enum CardAttributes
    {
        SpaceStation,
        RainbowRoad,
        Satellite,
        None

    }

    class Card
    {
        int m_SpriteID;
        CardAttributes[] m_Attributes = new CardAttributes[5];

        public Card() // Goeie manier vinden om dit te initliazen
        {
            //Berekent doormiddel van de Attributen de SpriteID
        }

        CardAttributes  GetAttribute(int Spot)
        {
            return m_Attributes[Spot];
        }
    }
}