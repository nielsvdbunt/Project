using System;
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
        public Card m_CurrentCard;
        List<CCPoint> m_PosiblePos;
        List<CCPoint> m_CheckedCards;

        public GameBase(GameScene Scene, InputGameInfo info)
        {
            m_Players = new List<Player>();
            foreach (InputPlayer i in info.Players)
            {
                Player p = new Player();
                p.Name = i.Name;
                p.PlayerColor = i.Color;
                p.NumberOfAliens = info.Aliens;
                m_Players.Add(p);
            }

            m_Scene = Scene;
            m_Board = new Board();
            m_Deck = new Deck(info.CardMultiplier);
            m_CheckedCards = new List<CCPoint>();
        }

        public void Start()
        {
            Random r = new Random();
            for (int n = m_Players.Count - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);
                Player temp = m_Players[n];
                m_Players[n] = m_Players[k];
                m_Players[k] = temp;
            }

            m_Players[0].Turn = true;

            Card BeginCard = new Card("21202");
            m_Scene.m_BoardLayer.DrawCard(BeginCard, new CCPoint(0, 0));
            m_Scene.m_BoardLayer.DrawCard(BeginCard, new CCPoint(1, 1));
            m_Board.AddCard(BeginCard, new CCPoint(0, 0));
            m_Board.AddCard(BeginCard, new CCPoint(1, 1));

            m_CurrentCard = m_Deck.GetNextCard();
            m_Scene.m_Overlay.UpdateInterface(m_Players, m_Deck.GetCardsLeft(), m_CurrentCard);

            FindPossibleMoves();
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
            m_Scene.m_Overlay.UpdateInterface(m_Players, m_Deck.GetCardsLeft(), m_CurrentCard);

            FindPossibleMoves();
        } 

        public void RotateCard(int Rot)
        {
            m_CurrentCard.Rotate(Rot);
            m_Scene.m_Overlay.UpdateInterface(m_Players, m_Deck.GetCardsLeft(), m_CurrentCard);
            FindPossibleMoves();
        }

        public void FindPossibleMoves()
        {
            m_PosiblePos = new List<CCPoint>();

            foreach (CCPoint p in m_Board.m_OpenSpots)
            {
                Card L = m_Board.GetCard(new CCPoint(p.X - 1, p.Y));
                Card T = m_Board.GetCard(new CCPoint(p.X , p.Y + 1));
                Card R = m_Board.GetCard(new CCPoint(p.X + 1, p.Y));
                Card B = m_Board.GetCard(new CCPoint(p.X , p.Y - 1));

                bool Add = true;

                if (L != null)
                    if (L.GetAttribute(3) != m_CurrentCard.GetAttribute(1))
                        Add = false;

                if (T != null)
                    if (T.GetAttribute(0) != m_CurrentCard.GetAttribute(2))
                        Add = false;

                if (R != null)
                    if (R.GetAttribute(1) != m_CurrentCard.GetAttribute(3))
                        Add = false;

                if (B != null)
                    if (B.GetAttribute(2) != m_CurrentCard.GetAttribute(0))
                        Add = false;

                if (Add)
                    m_PosiblePos.Add(p);
            }

            m_Scene.m_BoardLayer.DrawRaster(m_PosiblePos);
        }

        public void CheckFinished(CCPoint p, CardAttributes c)
        {
            Card cur = m_Board.GetCard(p);
            Card L = m_Board.GetCard(new CCPoint(p.X - 1, p.Y));
            Card T = m_Board.GetCard(new CCPoint(p.X, p.Y + 1));
            Card R = m_Board.GetCard(new CCPoint(p.X + 1, p.Y));
            Card B = m_Board.GetCard(new CCPoint(p.X, p.Y - 1));

            if (L != null && !m_CheckedCards.Contains(p))
                if (L.GetAttribute(3) == cur.GetAttribute(1) && L.GetAttribute(3) == c)
                {
                    m_CheckedCards.Add(p);
                    CheckFinished(new CCPoint(p.X - 1, p.Y), c);
                }            

            if (T != null && !m_CheckedCards.Contains(p))
                if (T.GetAttribute(0) == cur.GetAttribute(2) && T.GetAttribute(0) == c)
                {
                    m_CheckedCards.Add(p);
                    CheckFinished(new CCPoint(p.X, p.Y + 1), c);
                }

            if (R != null && !m_CheckedCards.Contains(p))
                if (R.GetAttribute(1) == cur.GetAttribute(3) && R.GetAttribute(1) == c)
                {
                    m_CheckedCards.Add(p);
                    CheckFinished(new CCPoint(p.X + 1, p.Y), c);
                }

            if (B != null && !m_CheckedCards.Contains(p))
                if (B.GetAttribute(2) == cur.GetAttribute(0) && B.GetAttribute(2) == c)
                {
                    m_CheckedCards.Add(p);
                    CheckFinished(new CCPoint(p.X, p.Y - 1), c);
                }

        }
       
    }
}