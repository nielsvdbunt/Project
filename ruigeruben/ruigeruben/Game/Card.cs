﻿using System;
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
        intersection = 4,
        None = 0

    }

    class Card // in this class all the cards are made and can get rotated
    {
        public static readonly string[] CardTypes = {
          "00003", 
          "01000",
          "01010",
          "01100",
          "02202",
          "02220",
          "10101",
          "11001",
          "11111",
          "11224", 
          "20003",
          "20122", 
          "10111", 
          "20202",
          "21111",
          "21202",
          "21220", 
          "22102", 
          "22220", 

              
        };

        public static readonly int[] CardQuantity = {
         4,
         5,
         3,
         2,
         9,
         4,
         3,
         5,
         1,
         5,
         2,
         3,
         4,
         8,
         3,
         3, 
         3,
         3,
         1
        };

        public string m_Hash;
        public CardAttributes[] m_Attributes = new CardAttributes[5];
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

        public CardAttributes GetAttribute(int Spot)
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