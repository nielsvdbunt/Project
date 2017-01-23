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
        List<CCPoint> PosiblePos;

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

        }

        public void Walktiles(int x, int y)
        {       
        
            

        }


        public void FindPossibleMoves()
        {
            PosiblePos = new List<CCPoint>();

            foreach (CCPoint p in m_Board.m_OpenSpots)
            {
                for (int j = -1; j <= 2; j += 2)
                {
                    Card c = m_Board.GetCard(new CCPoint(p.X + j, p.Y));

                    if(c != null)
                    { 
                        if (j < 0)
                        {
                            if (m_CurrentCard.GetAttribute(1) == c.GetAttribute(3))
                                PosiblePos.Add(p);
                        }                     
                        else
                        {
                            if (m_CurrentCard.GetAttribute(3) == c.GetAttribute(1))
                                PosiblePos.Add(p);
                        }
                    }

                    c = m_Board.GetCard(new CCPoint(p.X, p.Y + j));

                    if(c != null)
                    {
                        if (j < 0)
                        {
                            if (m_CurrentCard.GetAttribute(0) == c.GetAttribute(2))
                                PosiblePos.Add(p);
                        }
                        else
                        {
                            if (m_CurrentCard.GetAttribute(2) == c.GetAttribute(0))
                                PosiblePos.Add(p);
                        }
                    }
                }
            }
        }


       
    }
}