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
        public Card m_CurrentCard;
        public List<CCPoint> m_PosiblePos;
        List<CCPoint> m_CheckedCards;
        public Player m_CurrentPlayer;
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
            m_CurrentPlayer = m_Players[0];

            Card BeginCard = new Card("21202");
            m_Scene.m_BoardLayer.DrawCard(BeginCard, new CCPoint(0, 0));
            m_Board.AddCard(BeginCard, new CCPoint(0, 0));

            m_CurrentCard = m_Deck.GetNextCard();
            m_Scene.m_Overlay.UpdateInterface(m_Players, m_Deck.GetCardsLeft(), m_CurrentCard);

            FindPossibleMoves();
        }
  
        public void NextTurn()
        {

            int points1; int points2; int points3; int points4;
            List<Player> playerlist = new List<Player>();

            Points(m_PlacedCard, CardAttributes.SpaceStation, out points1, out points2, out points3, out points4);
            // points voor afgemaakte spacestation
            if (points1 != 0)
            {
                playerlist = CheckAliens(m_PlacedCard, 1);
                foreach (Player ply in playerlist)
                {
                    ply.Points += points1;
                }
            }
            if (points2 != 0)
            {
                playerlist = CheckAliens(m_PlacedCard, 2);
                foreach (Player ply in playerlist)
                {
                    ply.Points += points2;
                }
            }

            Points(m_PlacedCard, CardAttributes.RainbowRoad, out points1, out points2, out points3, out points4);
            // points voor afgemaakte rainbowroad
            if (points1 != 0)
            {
                playerlist = CheckAliens(m_PlacedCard, 1);
                foreach (Player ply in playerlist)
                {
                    ply.Points += points1;
                }
            }
            if (points2 != 0)
            {
                playerlist = CheckAliens(m_PlacedCard, 2);
                foreach (Player ply in playerlist)
                {
                    ply.Points += points2;
                }
            }
            if (points3 != 0)
            {
                playerlist = CheckAliens(m_PlacedCard, 3);
                foreach (Player ply in playerlist)
                {
                    ply.Points += points3;
                }
            }
            if (points4 != 0)
            {
                playerlist = CheckAliens(m_PlacedCard, 0);
                foreach (Player ply in playerlist)
                {
                    ply.Points += points4;
                }
            }

            List<CCPoint> satellitelist = CheckSatellite(m_PlacedCard);
            Player player;
            foreach (CCPoint point in satellitelist)
            {
                int satellitepoints = CheckSatelliteFinished(point);
                if (satellitepoints == 8)
                {
                    player = m_Board.HasAlien(point, 4);
                    if (player != null)
                        player.Points += 9;
                }

            }


            for (int i=0; i<m_Players.Count; i++ )
            {
                if (m_Players[i].Turn)
                {
                    m_Players[i].Turn = false;

                    int j = 0;
                    if (i != (m_Players.Count - 1))
                        j = ++i;
                    m_Players[j].Turn = true;
                    m_CurrentPlayer = m_Players[j];
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

        public List<Player> CheckAliens(CCPoint p, int side)
        {
            List<Player> res = new List<Player>();

            return res;
        }

        public void Points(CCPoint p, CardAttributes attr, out int points1, out int points2, out int points3, out int points4) //moet nog aangepast worden aan waar aliens staan
        {
            points1 = 0;  points2 = 0; points3 = 0; points4 = 0;
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
                        if (CheckFinished(p, attr, true, 4, 4))
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
                        if (side1 != 4)
                            sides.Add(side1);
                        if (side2 != 4)
                            sides.Add(side2);
                        if (side3 != 4)
                            sides.Add(side3);
                        if (side0 != 4)
                            sides.Add(side0);
                        if (attr == CardAttributes.SpaceStation)
                            if (Connected(p, attr, sides[0], sides[1]))
                            {
                                if (CheckFinished(p, attr, true, 4, 4))
                                    if (m_CheckedCards.Count <= 2)
                                        points1 = 2;
                                    else
                                        points1 = m_CheckedCards.Count * 2;
                            }
                            else
                            {
                                if (CheckFinished(p, attr, sides[0], 4))
                                    points1 = m_CheckedCards.Count;
                                if (CheckFinished(p, attr, sides[1], 4))
                                    points2 = m_CheckedCards.Count;
                            }
                        else
                        {
                            if (sides.Count == 2)
                                if (Connected(p, attr, sides[0], sides[1]))
                                    if (CheckFinished(p, attr, true, 4, 4))
                                        points1 = m_CheckedCards.Count;
                                    else;
                                else
                                {
                                    if (CheckFinished(p, attr, sides[0], 4))
                                        points1 = m_CheckedCards.Count;
                                    if (CheckFinished(p, attr, sides[1], 4))
                                        points2 = m_CheckedCards.Count;
                                }
                        
                            if (sides.Count == 3)
                            {
                                int first = 4, second = 4;
                                if (Connected(p, attr, sides[0], sides[1]))
                                {
                                    first = 0;
                                    second = 2;
                                }
                                else if (Connected(p, attr, sides[1], sides[2]))
                                {
                                    first = 1;
                                    second = 0;
                                }
                                else if (Connected(p, attr, sides[2], sides[0]))
                                {
                                    first = 2;
                                    second = 1;
                                }
                                else
                                {
                                    if (CheckFinished(p, attr, sides[0], 4))
                                        points1 = m_CheckedCards.Count;
                                    if (CheckFinished(p, attr, sides[1], 4))
                                        points2 = m_CheckedCards.Count;
                                    if (CheckFinished(p, attr, sides[2], 4))
                                        points3 = m_CheckedCards.Count;
                                }

                                if (first != 4 && second != 4)
                                {
                                    if (CheckFinished(p, attr, sides[first], 4))
                                        points1 = m_CheckedCards.Count;
                                    if (CheckFinished(p, attr, sides[second], 4))
                                    {
                                        if (points1 == 0)
                                            points1 = m_CheckedCards.Count;
                                        else
                                            points2 = m_CheckedCards.Count;
                                    }
                                }
                            }
                            if (sides.Count == 4)
                            {
                                int first = 4, second = 4, third = 4;
                                if (Connected(p, attr, sides[0], sides[1]))
                                    if (Connected(p, attr, sides[2], sides[3]))
                                    {
                                        first = 0;
                                        second = 2;
                                    }
                                    else
                                    {
                                        first = 0;
                                        second = 2;
                                        third = 3;
                                    }
                                else if (Connected(p, attr, sides[2], sides[3]))
                                {
                                    first = 2;
                                    second = 0;
                                    third = 1;
                                }
                                else if (Connected(p, attr, sides[1], sides[2]))
                                    if (Connected(p, attr, sides[3], sides[0]))
                                    {
                                        first = 1;
                                        second = 3;
                                    }
                                    else
                                    {
                                        first = 1;
                                        second = 3;
                                        third = 0;
                                    }
                                else if (Connected(p, attr, sides[3], sides[0]))
                                {
                                    first = 3;
                                    second = 1;
                                    third = 2;
                                }
                                else if (Connected(p, attr, sides[1], sides[3]))
                                    if (Connected(p, attr, sides[2], sides[0]))
                                    {
                                        first = 1;
                                        second = 2;
                                    }
                                    else
                                    {
                                        first = 1;
                                        second = 2;
                                        third = 0;
                                    }
                                else if (Connected(p, attr, sides[2], sides[0]))
                                {
                                    first = 2;
                                    second = 1;
                                    third = 3;
                                }
                                else
                                {
                                    if (CheckFinished(p, attr, 0, 4))
                                        points1 = m_CheckedCards.Count;
                                    if (CheckFinished(p, attr, 1, 4))
                                        points2 = m_CheckedCards.Count;
                                    if (CheckFinished(p, attr, 2, 4))
                                        points3 = m_CheckedCards.Count;
                                    if (CheckFinished(p, attr, 3, 4))
                                        points4 = m_CheckedCards.Count;
                                }
                                
                                if (first != 4 && third == 4)
                                {
                                    if (CheckFinished(p, attr, sides[first], 4))
                                        points1 = m_CheckedCards.Count;
                                    if (CheckFinished(p, attr, sides[second], 4))
                                        points2 = m_CheckedCards.Count;
                                }
                                if (third != 4)
                                {
                                    if (CheckFinished(p, attr, sides[first], 4))
                                        points1 = m_CheckedCards.Count;
                                    if (CheckFinished(p, attr, sides[second], 4))
                                        points2 = m_CheckedCards.Count;
                                    if (CheckFinished(p, attr, sides[third], 4))
                                        points3 = m_CheckedCards.Count;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (CheckFinished(p, attr, true, 4, 4))
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
                if (CheckSatelliteFinished(p) == 8)
                    points1 = 9;
            }
            m_CheckedCards = new List<CCPoint>();
        }

        public bool CheckFinished(CCPoint p, CardAttributes c, bool firstcard, int side1, int side2)
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
                    if (l == c && side1 != 1)
                    {
                        if (L == null)
                            ml = false;
                        else
                        {
                            checkl = CheckFinished(new CCPoint(p.X - 1, p.Y), c, firstcard, 3, side2);
                            if (!checkl)
                                return false;
                        }
                        middencheck.Add(ml);
                    }
                    if (t == c && side1 != 2)
                    {
                        if (T == null)
                            mt = false;
                        else
                        {
                            checkt = CheckFinished(new CCPoint(p.X, p.Y + 1), c, firstcard, 0, side2);
                            if (!checkt)
                                return false;
                        }
                        middencheck.Add(mt);
                    }
                    if (r == c && side1 != 3)
                    {
                        if (R == null)
                            mr = false;
                        else
                        {
                            checkr = CheckFinished(new CCPoint(p.X + 1, p.Y), c, firstcard, 1, side2);
                            if (!checkr)
                                return false;
                        }
                        middencheck.Add(mr);
                    }
                    if (b == c && side1 != 0)
                    {
                        if (B == null)
                            mb = false;
                        else
                        {
                            checkb = CheckFinished(new CCPoint(p.X, p.Y - 1), c, firstcard, 2, side2);
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
            {
                if ((side1 == 1 && side2 == 3) || (side1 == 2 && side2 == 0) || (side1 == 3 && side2 == 1) || (side1 == 0 && side2 == 2))
                    connect = true;
            }
            return true;

        }
        
        public bool CheckFinished(CCPoint p, CardAttributes c, int side, int side2)
        {
            Card cur = m_Board.GetCard(p);
            m_CheckedCards.Add(p);
            if (side == 1)
            {
                if (!CheckFinished(new CCPoint(p.X - 1, p.Y), c, false, 3, side2))
                    return false;
            }
            if (side == 2)
            {
                if (!CheckFinished(new CCPoint(p.X, p.Y + 1), c, false, 0, side2))
                    return false;
            }
            if (side == 3)
            {
                if (!CheckFinished(new CCPoint(p.X + 1, p.Y), c, false, 1, side2))
                    return false;
            }
            if (side == 0)
            {
                if (!CheckFinished(new CCPoint(p.X, p.Y - 1), c, false, 2, side2))
                    return false;
            }
            return true;
        }

        public bool Connected(CCPoint p, CardAttributes c, int side1, int side2)
        {
            connect = false;
            CheckFinished(p, c, true, side1, side2);
            m_CheckedCards = new List<CCPoint>();
            return connect;
        }

        public int CheckSatelliteFinished(CCPoint p)
        {
            List<Card> list = new List<Card>();
            int t = 0;

            for(int i = -1; i<=1; i++)
            {
                for (int j = -1; j <= 1; j++)
                    list.Add(m_Board.GetCard(new CCPoint(p.X + i, p.Y + j)));
            }

            foreach (Card c in list)
                if (c != null)
                    t += 1;
            return t;
        }

        public List<CCPoint> CheckSatellite(CCPoint p)
        {
            List<Card> list = new List<Card>();
            List<CCPoint> list2 = new List<CCPoint>();
            List<CCPoint> res = new List<CCPoint>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    list.Add(m_Board.GetCard(new CCPoint(p.X + i, p.Y + j)));
                    list2.Add(new CCPoint(p.X + i, p.Y + j));
                }
            }

            foreach (Card c in list)
                if (c != null)
                    if (c.GetAttribute(4) == CardAttributes.Satellite)
                    //if () hier moet gecheckt worden of er een alien op positie 4 staat
                    {
                        int index = list.IndexOf(c);
                        res.Add(list2[index]);
                    }

            return res;

        }

        public void refresh()
        {
            //m_Scene.m_Overlay.UpdateInterface(m_Players, m_Deck.GetCardsLeft(), m_CurrentCard);
            m_Scene.m_BoardLayer.DrawRaster(m_PosiblePos);
        }

        private void EndGame()
        {
            //Hier moeten de punten nog worden berekend
            if(m_Deck.GetCardsLeft() == 0)
                 m_Scene.AddLayer(new Game.EndLayer(m_Players), 4);
        }
    }
}