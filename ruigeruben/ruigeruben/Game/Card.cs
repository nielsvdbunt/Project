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
        intersection =4, // is dit nodig?
        None = 0
            // doen we we nog die schilden

    }

    class Card
    {
        public static readonly string[] CardTypes = {
          "20003", 
          "00003",
          "00001",
          "00012",
          "00100",
          "01011",
          "10101",
          "01010",
          "10010",
          "20120", // dit is een hoek bocht 
          "02210",
          "22214",
          "01100",
          "21120",
          "01111",
          "21111",
          "20202",
          "22000",
          "22020",
          "22220",

              
        };

        public static readonly int[] CardQuantity = {
           2,
           4,
           1,
           4,
           5,
           2,
           1,
           3,
           2,
           3,
           3,
           3,
           5,
           5,
           4,
           3,
           8,
           9,
           4,
           1
        };

        string m_Hash;
        CardAttributes[] m_Attributes = new CardAttributes[5];
        int m_Rotation = 0;

        public Card(string Hash) 
        {
            if (Hash.Length != 5)
                return;

            m_Hash = Hash;

            for(int i = 0; i < Hash.Length; i++)
            {
                m_Attributes[i] = (CardAttributes) (Hash[i]- '0');
            }
        }

        CardAttributes GetAttribute(int Spot)
        {
            return m_Attributes[Spot];
        }

        public void Rotate(int Degrees)
        {
            CardAttributes[] Attr = new CardAttributes[5];
            m_Attributes.CopyTo(Attr, 0);
            m_Rotation += Degrees;

            for(int i = 0; i < 4; i++)
            {
                if(Degrees > 0)
                {
                    if (i == 3)
                        m_Attributes[0] = Attr[3];
                    else
                        m_Attributes[i + 1] = Attr[i];
                }
                if(Degrees < 0)
                {
                    if (i == 0)
                        m_Attributes[3] = Attr[0];
                    else
                        m_Attributes[i - 1] = Attr[i];
                }
            }
        }

        public int GetRotation()
        {
            return m_Rotation;
        }

    }
}