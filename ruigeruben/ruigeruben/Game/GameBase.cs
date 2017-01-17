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

        public GameBase(GameScene Scene, InputGameInfo info)
        {
            m_Players = new List<Player>();

            foreach(InputPlayer i in info.Players)
            {
                Player p = new Player();
                p.Name = i.Name;
                p.PlayerColor = i.Color;
                m_Players.Add(p);
            }

            m_Scene = Scene;
            m_Board = new Board();
            m_Deck = new Deck(1);
            Start();

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
            int y = 5;
        }
        public void NextTurn()
        {

        }
        public void Walktiles(int x, int y)
        {
            for (int i = -1; i <= 2; i += 2)
            {
                Checktiles(x + i, y);
                Checktiles(x, y + i);
            }
            

        }

        public void Checktiles(int x, int y)
        {
            Card CardInHand = new Card("yolo2");
            Card c = m_Board.GetCard(x, y);
            
        }

    }
}