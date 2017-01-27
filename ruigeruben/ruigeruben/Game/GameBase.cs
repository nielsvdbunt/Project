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
        public List<CCPoint> m_PosiblePos;
        List<CCPoint> m_CheckedCards;

        public CCPoint m_PlacedCard;
        bool connect;

        public GameBase(GameScene Scene, InputGameInfo info)
        {
            m_Players = new List<Player>();
            foreach (InputPlayer i in info.Players)
            {
                Player p = new Player();
                p.Name = i.Name;
                p.PlayerColor = i.Color;
                p.NumberOfAliens = (int) info.Aliens;
                m_Players.Add(p);
            }

            m_Scene = Scene;
            m_Board = new Board();
            m_Deck = new Deck(info.CardMultiplier);
            m_CheckedCards = new List<CCPoint>();

            connect = false;
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
            m_Board.AddCard(BeginCard, new CCPoint(0, 0));

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

            do
            {
                if (m_Deck.GetCardsLeft() == 0)
                {
                    EndGame();
                    return;
                }
                m_CurrentCard = m_Deck.GetNextCard();
            } while (!AnyMovePossible());
           
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

        private bool AnyMovePossible()
        {
            for (int rot = 0; rot <= 2; rot++)
            {
                foreach (CCPoint p in m_Board.m_OpenSpots)
                {
                    Card L = m_Board.GetCard(new CCPoint(p.X - 1, p.Y));
                    Card T = m_Board.GetCard(new CCPoint(p.X, p.Y + 1));
                    Card R = m_Board.GetCard(new CCPoint(p.X + 1, p.Y));
                    Card B = m_Board.GetCard(new CCPoint(p.X, p.Y - 1));

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
                        return true;
                }
                RotateCard(90);
            }
            return false;
        }

        public int Points(CCPoint p, CardAttributes attr, out int points2, out int points3, out int points4) //moet nog aangepast worden aan waar aliens staan
        {
            int points1 = 0;
            points2 = 0; points3 = 0; points4 = 0;
            if (attr == CardAttributes.SpaceStation || attr == CardAttributes.RainbowRoad)
            {
                Card card = m_Board.GetCard(p);

                if (card.GetAttribute(4) == CardAttributes.None)
                {
                    List<CardAttributes> attrlist = new List<CardAttributes>();
                    int s = 0, r = 0;
                    for (int t = 0; t < 4; t++)
                        attrlist.Add(card.GetAttribute(t));
                    foreach (CardAttributes c in attrlist)
                    {
                        if (c == CardAttributes.SpaceStation)
                            s += 1;
                        if (c == CardAttributes.RainbowRoad)
                            r += 1;
                    }
                    if ((s < 2 && attr == CardAttributes.SpaceStation) || (r < 2 && attr == CardAttributes.RainbowRoad))
                    {
                        if (CheckFinished(p, attr, true, 4))
                            if (attr == CardAttributes.SpaceStation)
                            {
                                if (m_CheckedCards.Count <= 2)
                                    points1 = 2;
                                else
                                    points1 = m_CheckedCards.Count * 2;
                            }
                            else
                                points1 = m_CheckedCards.Count;
                    }
                    else
                    {
                        int side1 = 4, side2 = 4, side3 = 4, side0 = 4;
                        if (card.GetAttribute(1) == attr)
                            side1 = 1;
                        if (card.GetAttribute(2) == attr)
                            side2 = 2;
                        if (card.GetAttribute(3) == attr)
                            side3 = 3;
                        if (card.GetAttribute(0) == attr)
                            side0 = 0;
                        List<int> sides = new List<int>();
                        sides.Add(side1); sides.Add(side2); sides.Add(side3); sides.Add(side0);
                        foreach(int i in sides)
                        {
                            if (i == 4)
                                sides.Remove(i);
                        }
                        if (attr == CardAttributes.SpaceStation)
                            if (Connected(p, attr, sides[0], sides[1]))
                            {
                                if (CheckFinished(p, attr, true, 4))
                                    if (m_CheckedCards.Count <= 2)
                                        points1 = 2;
                                    else
                                        points1 = m_CheckedCards.Count * 2;
                            }
                            else
                            {
                                if (CheckFinished(p, attr, sides[0]))
                                    points1 = m_CheckedCards.Count;
                                if (CheckFinished(p, attr, sides[1]) && points1 != 0)
                                    points1 = m_CheckedCards.Count;
                                else
                                    points2 = m_CheckedCards.Count;
                            }
                        else
                        {
                            //midden = none rainbowroadcheck
                        }
                    }
                }
                else
                {
                    if (CheckFinished(p, attr, true, 4))
                        if (attr == CardAttributes.SpaceStation)
                        {
                            if (m_CheckedCards.Count <= 2)
                                points1 = 2;
                            else
                                points1 = m_CheckedCards.Count * 2;
                        }
                        else
                            points1 = m_CheckedCards.Count;
                }
            }
            else
            {
                if (CheckSatelite(p) == 8)
                    points1 = 9;
            }
            m_CheckedCards = new List<CCPoint>();
            return points1;

        }

        public bool CheckFinished(CCPoint p, CardAttributes c, bool firstcard, int side)
        {
            Card cur = m_Board.GetCard(p);
            Card L = m_Board.GetCard(new CCPoint(p.X - 1, p.Y));
            Card T = m_Board.GetCard(new CCPoint(p.X, p.Y + 1));
            Card R = m_Board.GetCard(new CCPoint(p.X + 1, p.Y));
            Card B = m_Board.GetCard(new CCPoint(p.X, p.Y - 1));

            CardAttributes l = cur.GetAttribute(1);
            CardAttributes t = cur.GetAttribute(2);
            CardAttributes r = cur.GetAttribute(3);
            CardAttributes b = cur.GetAttribute(0);
            CardAttributes m = cur.GetAttribute(4);

            bool ml = true, mt = true, mr = true, mb = true;
            bool checkl, checkt, checkr, checkb;

            List<bool> middencheck = new List<bool>();

            if (!m_CheckedCards.Contains(p))
            {
                m_CheckedCards.Add(p);
                if (m == c || m == CardAttributes.intersection || firstcard)
                {
                    firstcard = false;
                    if (l == c && side != 1)
                    {
                        if (L == null)
                            ml = false;
                        else
                        {
                            checkl = CheckFinished(new CCPoint(p.X - 1, p.Y), c, firstcard, 3);
                            if (!checkl)
                                return false;
                        }
                        middencheck.Add(ml);
                    }
                    if (t == c && side != 2)
                    {
                        if (T == null)
                            mt = false;
                        else
                        {
                            checkt = CheckFinished(new CCPoint(p.X, p.Y + 1), c, firstcard, 0);
                            if (!checkt)
                                return false;
                        }
                        middencheck.Add(mt);
                    }
                    if (r == c && side != 3)
                    {
                        if (R == null)
                            mr = false;
                        else
                        {
                            checkr = CheckFinished(new CCPoint(p.X + 1, p.Y), c, firstcard, 1);
                            if (!checkr)
                                return false;
                        }
                        middencheck.Add(mr);
                    }
                    if (b == c && side != 0)
                    {
                        if (B == null)
                            mb = false;
                        else
                        {
                            checkb = CheckFinished(new CCPoint(p.X, p.Y - 1), c, firstcard, 2);
                            if (!checkb)
                                return false;
                        }
                        middencheck.Add(mb);
                    }

                    if (middencheck.Contains(false))
                        return false;
                }
            }
            if (p == m_PlacedCard && m != c)
                connect = true;
            return true;

        }
        
        public bool CheckFinished(CCPoint p, CardAttributes c, int side)
        {
            Card cur = m_Board.GetCard(p);
            m_CheckedCards.Add(p);
            if (side == 1)
            {
                if (!CheckFinished(new CCPoint(p.X - 1, p.Y), c, false, 3))
                    return false;
            }
            if (side == 2)
            {
                if (!CheckFinished(new CCPoint(p.X, p.Y + 1), c, false, 0))
                    return false;
            }
            if (side == 3)
            {
                if (!CheckFinished(new CCPoint(p.X + 1, p.Y), c, false, 1))
                    return false;
            }
            if (side == 0)
            {
                if (!CheckFinished(new CCPoint(p.X, p.Y - 1), c, false, 2))
                    return false;
            }
            return true;
        }

        public bool Connected(CCPoint p, CardAttributes c, int side1, int side2)
        {
            connect = false;
            CheckFinished(p, c, 4);
            if (connect)
                return true;
            else
                return false;
        }

        public int CheckSatelite(CCPoint p)
        {
            int i = 0;
            List<Card> list = new List<Card>();
            Card l = m_Board.GetCard(new CCPoint(p.X - 1, p.Y));
            Card lb = m_Board.GetCard(new CCPoint(p.X - 1, p.Y - 1));
            Card b = m_Board.GetCard(new CCPoint(p.X, p.Y - 1));
            Card rb = m_Board.GetCard(new CCPoint(p.X + 1, p.Y - 1));
            Card r = m_Board.GetCard(new CCPoint(p.X + 1, p.Y));
            Card rt = m_Board.GetCard(new CCPoint(p.X + 1, p.Y + 1));
            Card t = m_Board.GetCard(new CCPoint(p.X, p.Y + 1));
            Card lt = m_Board.GetCard(new CCPoint(p.X - 1, p.Y + 1));
            list.Add(l);
            list.Add(lb);
            list.Add(b);
            list.Add(rb);
            list.Add(r);
            list.Add(rt);
            list.Add(t);
            list.Add(lt);
            foreach (Card c in list)
                if (c != null)
                    i += 1;
            return i;
        }

        public void refresh()
        {
            m_Scene.m_Overlay.UpdateInterface(m_Players, m_Deck.GetCardsLeft(), m_CurrentCard);
            m_Scene.m_BoardLayer.DrawRaster(m_PosiblePos);
        }

        private void EndGame()
        {
            //Hier moeten de punten nog worden berekend

            m_Scene.AddLayer(new Game.EndLayer(m_Players), 4);
        }
    }
}