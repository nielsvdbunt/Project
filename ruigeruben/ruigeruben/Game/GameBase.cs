﻿using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class GameBase
    {
        GameScene m_Scene;
        public List<Player> m_Players;
        Deck m_Deck;
        public Board m_Board;
        InputGameInfo m_GameInfo;
        public Card m_CurrentCard;

        public GameBase(GameScene Scene, InputGameInfo info)
        {
            m_Players = new List<Player>();
            m_GameInfo = info;
            foreach(InputPlayer i in info.Players)
            {
                Player p = new Player();
                p.Name = i.Name;
                p.PlayerColor = i.Color;
                p.NumberOfAliens = m_GameInfo.Aliens; 
                m_Players.Add(p);
            }

            m_Scene = Scene;
            m_Board = new Board();
            m_Deck = new Deck(info.CardMultiplier);            
        }

        public void Start()
        {
            Random r = new Random();
            for (int n = m_Players.Count- 1; n > 0; --n)
            {
                int k = r.Next(n + 1);
                Player temp = m_Players[n];
                m_Players[n] = m_Players[k];
                m_Players[k] = temp;
            }

            m_Players[0].Turn = true;
            m_CurrentCard = m_Deck.GetNextCard();
            m_Scene.m_Overlay.update_interface(m_Players, m_Deck.GetCardsLeft(), m_CurrentCard);


        }

        public void NextTurn()
        {
            for(int i=0; i<m_Players.Count; i++ )
            {
                if (m_Players[i].Turn)
                {
                    m_Players[i].Turn = false;

                    int j = 0;
                    if (i != (m_Players.Count - 1))
                        j = ++i;
                    m_Players[j].Turn = true;
                    break;
                }
            }
            m_CurrentCard = m_Deck.GetNextCard();
            m_Scene.m_Overlay.update_interface(m_Players, m_Deck.GetCardsLeft(), m_CurrentCard);
        } 

        public void RotateCard(int Rot)
        {
            m_CurrentCard.Rotate(Rot);
            m_Scene.m_Overlay.update_interface(m_Players, m_Deck.GetCardsLeft(), m_CurrentCard);

        }

        public void Walktiles(int x, int y)
        {
            for (int i = -1; i <= 2; i += 2)
            {
                Checktiles(x + i, y);
                Checktiles(x, y + i);
            }
            

        }
/*
        public CCSprite getSprite(string n)
        {
            CCSprite tile;
            CCSpriteSheet sheet = new CCSpriteSheet("sheet.plist", "sheetimage.png");
            CCSpriteFrame frame = sheet.Frames.Find(item => item.TextureFilename == n + ".png");
            tile = new CCSprite(frame);
            return tile;
        }*/

        public bool Checktiles(int x, int y)
        {
          
            Card c = m_Board.GetCard(x, y);
            foreach (Card kaart in m_Board.m_virCards)
            {
                if (kaart.GetHashCode() == m_CurrentCard.GetHashCode()  )
                    return true;
                else
                    return false;
            }

            return false;
        }



    }
}