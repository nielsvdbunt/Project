using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    struct PLayerScore
    {
        public Player m_Player;
        public CCPoint m_Punt;
    }

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
        //  bool connect;

        public GameBase(GameScene Scene, InputGameInfo info)
        {
            m_Players = new List<Player>();
            foreach (InputPlayer i in info.Players)
            {
                Player p = new Player();
                p.Name = i.Name;
                p.PlayerColor = i.Color;
                p.NumberOfAliens = (int)info.Aliens;
                m_Players.Add(p);
            }

            m_Scene = Scene;
            m_Board = new Board();
            m_Deck = new Deck(info.CardMultiplier);
            m_CheckedCards = new List<CCPoint>();

            //connect = false;
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
            m_PlacedCard = m_Scene.GetPlacedCard();

            HandlePoints();

            for (int i = 0; i < m_Players.Count; i++)
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




        public void refresh()
        {
            //m_Scene.m_Overlay.UpdateInterface(m_Players, m_Deck.GetCardsLeft(), m_CurrentCard);
            m_Scene.m_BoardLayer.DrawRaster(m_PosiblePos);
        }

        private void EndGame()
        {
            //Hier moeten de punten nog worden berekend
            if (m_Deck.GetCardsLeft() == 0)
                m_Scene.AddLayer(new Game.EndLayer(m_Players), 4);
        }











        //Ruben's commented punten ding


        private void HandlePoints()
        {
            //Dit is een check om de kaart zelf + de 8 omringen kaarten te checken of er een satelite is en of ie klaar is
            for (int i = -1; i <= 1; i++) // doorloop de x waardes (-1 tot 1)
            {
                for (int j = -1; j <= 1; j++) // doorloop de y waardes (-1 tot 1)
                {
                    CCPoint NewP = new CCPoint(m_PlacedCard.X + i, m_PlacedCard.Y + j); // PlacedCard is de laatste neergelegde card, dus daar lopoen we omheen + zichtzelf
                    Card c = m_Board.GetCard(NewP);

                    if (c != null)
                    {
                        HandleSateliteCard(NewP, c);
                    }
                }
            }

            //Handle rainbow road, dit nog doen, is makkelijker denk ik
            if (m_CurrentCard.GetAttribute(4) == CardAttributes.None ||
                m_CurrentCard.GetAttribute(4) == CardAttributes.SpaceStation ||
                m_CurrentCard.GetAttribute(4) == CardAttributes.Satellite)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (m_CurrentCard.GetAttribute(i) == CardAttributes.RainbowRoad)
                    {
                        CCPoint NextCardpos = m_PlacedCard;

                        if (i == 0)
                        {
                            NextCardpos.Y -= 1;
                        }
                        else if (i == 1)
                        {
                            NextCardpos.X -= 1;
                        }
                        else if (i == 2)
                        {
                            NextCardpos.Y += 1;
                        }
                        else if (i == 3)
                        {
                            NextCardpos.X += 1;
                        }

                        if (m_Board.GetCard(NextCardpos) == null)
                            continue;

                        List<CCPoint> Cards = new List<CCPoint>();
                        Cards.Add(m_PlacedCard);
                        List<PLayerScore> Score = new List<PLayerScore>();
                        Player pl = m_Board.HasAlien(m_PlacedCard, i);
                        if (pl != null)
                        {
                            PLayerScore PS = new PLayerScore();
                            PS.m_Player = pl;
                            PS.m_Punt = m_PlacedCard;
                            Score.Add(PS);
                        }
                        if (FindRoadSpots(NextCardpos, i, ref Cards, ref Score) == false)
                        {
                            int CardScore = Cards.Count;

                            Dictionary<Player, int> Dic = new Dictionary<Player, int>();

                            foreach (PLayerScore ps in Score)
                            {
                                if (!Dic.ContainsKey(ps.m_Player))
                                    Dic.Add(ps.m_Player, 0);
                            }


                            foreach (PLayerScore ps in Score)
                            {
                                Dic[ps.m_Player] += 1;

                                ps.m_Player.NumberOfAliens++;
                                m_Board.RemoveAlien(ps.m_Punt);
                                m_Scene.m_BoardLayer.RemoveAlien(ps.m_Punt);
                            }

                            List<Player> player = new List<Player>();
                            int HighestScore = 0;

                            foreach (var v in Dic)
                            {
                                if (v.Value > HighestScore)
                                {
                                    player.Clear();
                                    HighestScore = v.Value;
                                    player.Add(v.Key);
                                }
                                else if (v.Value == HighestScore)
                                    player.Add(v.Key);
                            }

                            foreach (Player p in player)
                            {
                                p.Points += CardScore;
                            }

                        }
                    }
                }
            }

            else
            {
                List<CCPoint> Cards = new List<CCPoint>();
                List<PLayerScore> Score = new List<PLayerScore>();
                bool Done = false;

                for (int i = 0; i <= 3; i++)
                {
                    if (m_CurrentCard.GetAttribute(i) == CardAttributes.RainbowRoad)
                    {
                        CCPoint NextCardpos = m_PlacedCard;

                        if (i == 0)
                        {
                            NextCardpos.Y -= 1;
                        }
                        else if (i == 1)
                        {
                            NextCardpos.X -= 1;
                        }
                        else if (i == 2)
                        {
                            NextCardpos.Y += 1;
                        }
                        else if (i == 3)
                        {
                            NextCardpos.X += 1;
                        }

                        if (m_Board.GetCard(NextCardpos) == null)
                            continue;

                        if (!Cards.Contains(m_PlacedCard))
                        {
                            Cards.Add(m_PlacedCard);
                            Player pl = m_Board.HasAlien(m_PlacedCard, i);

                            if (pl != null)
                            {
                                PLayerScore PS = new PLayerScore();
                                PS.m_Player = pl;
                                PS.m_Punt = m_PlacedCard;
                                Score.Add(PS);
                            }
                        }

                        Done = FindRoadSpots(NextCardpos, i, ref Cards, ref Score);

                        if (Done)
                            break;
                    }
                }
                if (Done == false)
                {
                    int CardScore = Cards.Count;

                    Dictionary<Player, int> Dic = new Dictionary<Player, int>();

                    foreach (PLayerScore ps in Score)
                    {
                        if (!Dic.ContainsKey(ps.m_Player))
                            Dic.Add(ps.m_Player, 0);
                    }


                    foreach (PLayerScore ps in Score)
                    {
                        Dic[ps.m_Player] += 1;

                        ps.m_Player.NumberOfAliens++;
                        m_Board.RemoveAlien(ps.m_Punt);
                        m_Scene.m_BoardLayer.RemoveAlien(ps.m_Punt);
                    }

                    List<Player> player = new List<Player>();
                    int HighestScore = 0;

                    foreach (var v in Dic)
                    {
                        if (v.Value > HighestScore)
                        {
                            player.Clear();
                            HighestScore = v.Value;
                            player.Add(v.Key);
                        }
                        else if (v.Value == HighestScore)
                            player.Add(v.Key);
                    }

                    foreach (Player p in player)
                    {
                        p.Points += CardScore;
                    }
                }

            }

            //Handle Castle
            bool FoundCastle = false;
            List<int> CastleSides = new List<int>();
            for(int i = 0; i <= 3; i++) 
            {
                if(m_CurrentCard.GetAttribute(i) == CardAttributes.SpaceStation)
                {
                    FoundCastle = true;
                    CastleSides.Add(i);
                }                    
            }
            if(FoundCastle)
            {
                if(m_CurrentCard.GetAttribute(4) == CardAttributes.SpaceStation ||
                    m_CurrentCard.GetAttribute(4) == CardAttributes.intersection) //Als midden een stations is dan moeten er meerdere kaarten bekeken worden, anders niet
                {
                    List<CCPoint> Cards = new List<CCPoint>();
                    List<PLayerScore> Score = new List<PLayerScore>();
                    bool Done = false;
                    foreach (int i in CastleSides) // Gewoon alle hokenen afgaan, in theorie kan dit dubbelop zijn, maar maakt opzich niet uit, zolang we de aliens optijd verwijderen is de 2e keer puntloos
                    {
                        int CardSpot = i;
                        CCPoint NextCardpos = m_PlacedCard;

                        if (CardSpot == 0)
                        {
                            NextCardpos.Y -= 1;
                        }
                        else if (CardSpot == 1)
                        {
                            NextCardpos.X -= 1;
                        }
                        else if (CardSpot == 2)
                        {
                            NextCardpos.Y += 1;
                        }
                        else if (CardSpot == 3)
                        {
                            NextCardpos.X += 1;
                        }

                        Card NextCard = m_Board.GetCard(NextCardpos);

                        if (NextCard != null)
                        {
                            Cards.Add(m_PlacedCard);
                            Player pl = m_Board.HasAlien(m_PlacedCard, CardSpot);
                            if (pl != null)
                            {
                                PLayerScore PS = new PLayerScore();
                                PS.m_Player = pl;
                                PS.m_Punt = m_PlacedCard;
                                Score.Add(PS);
                            }

                            Done = FindCastleOpenSpots(NextCardpos, CardSpot, ref Cards, ref Score);

                            if (Done)
                                break;
                        }
                        else
                        {
                            Done = true;
                            break;
                        }
                    }
                    if(!Done)
                    {
                        int CardScore = 0;

                        if (Cards.Count == 2)
                            CardScore = 2;
                        else
                            CardScore = Cards.Count * 2;

                        Dictionary<Player, int> Dic = new Dictionary<Player, int>();

                        foreach (PLayerScore ps in Score)
                        {
                            if (!Dic.ContainsKey(ps.m_Player))
                                Dic.Add(ps.m_Player, 0);
                        }


                        foreach (PLayerScore ps in Score)
                        {
                            Dic[ps.m_Player] += 1;

                            ps.m_Player.NumberOfAliens++;
                            m_Board.RemoveAlien(ps.m_Punt);
                            m_Scene.m_BoardLayer.RemoveAlien(ps.m_Punt);
                        }

                        List<Player> player = new List<Player>();
                        int HighestScore = 0;

                        foreach (var v in Dic)
                        {
                            if (v.Value > HighestScore)
                            {
                                player.Clear();
                                HighestScore = v.Value;
                                player.Add(v.Key);
                            }
                            else if (v.Value == HighestScore)
                                player.Add(v.Key);
                        }

                        foreach (Player p in player)
                        {
                            p.Points += CardScore;
                        }
                    }

                }
                else // Alleen zeides zijn stations, makkelijker
                {
                    foreach (int i in CastleSides) // Gewoon alle hokenen afgaan, in theorie kan dit dubbelop zijn, maar maakt opzich niet uit, zolang we de aliens optijd verwijderen is de 2e keer puntloos
                    {
                        int CardSpot = i;
                        CCPoint NextCardpos = m_PlacedCard;

                        if (CardSpot == 0)
                        {
                            NextCardpos.Y -= 1;
                        }
                        else if (CardSpot == 1)
                        {
                            NextCardpos.X -= 1;
                        }
                        else if (CardSpot == 2)
                        {
                            NextCardpos.Y += 1;
                        }
                        else if (CardSpot == 3)
                        {
                            NextCardpos.X += 1;
                        }

                        Card NextCard = m_Board.GetCard(NextCardpos);

                        if (NextCard != null)
                        {
                            List<PLayerScore> Score = new List<PLayerScore>();
                            Player pl = m_Board.HasAlien(m_PlacedCard, CardSpot);
                            if (pl != null)
                            {
                                PLayerScore PS = new PLayerScore();
                                PS.m_Player = pl;
                                PS.m_Punt = m_PlacedCard;
                                Score.Add(PS);
                            }

                            List<CCPoint> Cards = new List<CCPoint>();
                            Cards.Add(m_PlacedCard);
                            bool b = FindCastleOpenSpots(NextCardpos, CardSpot, ref Cards, ref Score);

                            if(!b) // dus Kasteel heeft geen open dingen, check score
                            {
                                int CardScore = 0;

                                if (Cards.Count == 2)
                                    CardScore = 2;
                                else
                                    CardScore = Cards.Count * 2;

                                Dictionary<Player, int> Dic = new Dictionary<Player, int>();

                                foreach (PLayerScore ps in Score)
                                {
                                    if(!Dic.ContainsKey(ps.m_Player))
                                        Dic.Add(ps.m_Player, 0);
                                }
                                    

                                foreach (PLayerScore ps in Score)
                                {
                                    Dic[ps.m_Player] += 1;

                                    ps.m_Player.NumberOfAliens++;
                                    m_Board.RemoveAlien(ps.m_Punt);
                                    m_Scene.m_BoardLayer.RemoveAlien(ps.m_Punt);
                                }

                                List<Player> player = new List<Player>();
                                int HighestScore = 0;

                                foreach(var v in Dic)
                                {
                                    if (v.Value > HighestScore)
                                    {
                                        player.Clear();
                                        HighestScore = v.Value;
                                        player.Add(v.Key);
                                    }
                                    else if (v.Value == HighestScore)
                                        player.Add(v.Key);
                                }

                                foreach(Player p in player)
                                {
                                    p.Points += CardScore;
                                }

                            }
                     
                        }
                    }
                       
                }
            }
        }

        private void HandleSateliteCard(CCPoint p, Card c) // Functie die checked of kaart een satelite is
        {
            foreach(var i in c.m_Attributes)
            {
                if(i == CardAttributes.Satellite)
                {
                    if(IsSateliteDone(p))
                    {
                        Player play = m_Board.HasAlien(p, 4);

                        if(play != null)
                        {
                            play.Points += 9;
                            play.NumberOfAliens++;
                            m_Board.RemoveAlien(p);
                            m_Scene.m_BoardLayer.RemoveAlien(p);
                        }
                    }
                }
            }
        }

        private bool IsSateliteDone(CCPoint p)
        {
            for (int i = -1; i <= 1; i++) // doorloop de x waardes
            {
                for (int j = -1; j <= 1; j++) // doorloop de y waardes
                {
                    if (m_Board.GetCard(new CCPoint(p.X + i, p.Y + j)) == null) // als er een kaart omheen null is, niet done, dus return false
                        return false;
                }
            }

            return true; // alle kaarten zijn gelegd.
        }  

        private bool FindCastleOpenSpots(CCPoint p, int FromCardSpot, ref List<CCPoint> CardList, ref List<PLayerScore> Score)
        {
            CardList.Add(p);

            int CardSpot = 0;

            if (FromCardSpot == 0)
                CardSpot = 2;
            if (FromCardSpot == 1)
                CardSpot = 3;
            if (FromCardSpot == 2)
                CardSpot = 0;
            if (FromCardSpot == 3)
                CardSpot = 1;

            

                if (m_Board.GetCard(p).GetAttribute(4) == CardAttributes.SpaceStation ||
                    m_Board.GetCard(p).GetAttribute(4) == CardAttributes.intersection)
            {
                for(int i = 0; i <= 3; i++)
                {
                    if(i != CardSpot && m_Board.GetCard(p).GetAttribute(i) == CardAttributes.SpaceStation)
                    {
                        Player pll = m_Board.HasAlien(p, i);
                        if (pll != null)
                        {
                            PLayerScore PS = new PLayerScore();
                            PS.m_Player = pll;
                            PS.m_Punt = p;
                            Score.Add(PS);
                        }

                        CCPoint NextCardpos = p;

                        if (i == 0)
                        {
                            NextCardpos.Y -= 1;
                        }
                        else if (i == 1)
                        {
                            NextCardpos.X -= 1;
                        }
                        else if (i == 2)
                        {
                            NextCardpos.Y += 1;
                        }
                        else if (i == 3)
                        {
                            NextCardpos.X += 1;
                        }

                        Card NextCard = m_Board.GetCard(NextCardpos);

                        if (NextCard != null)
                        {
                            if (!CardList.Contains(NextCardpos))
                            {
                                if (FindCastleOpenSpots(NextCardpos, i, ref CardList, ref Score))
                                    return true;
                            }
                        }
                        else
                            return true;
                    }
                }
            }

            Player pl = m_Board.HasAlien(p, CardSpot);
            if (pl != null)
            {
                PLayerScore PS = new PLayerScore();
                PS.m_Player = pl;
                PS.m_Punt = p;
                Score.Add(PS);
            }

            return false;
        }

        private bool FindRoadSpots(CCPoint p, int FromCardSpot, ref List<CCPoint> CardList, ref List<PLayerScore> Score)
        {
            CardList.Add(p);

            int CardSpot = 0;

            if (FromCardSpot == 0)
                CardSpot = 2;
            if (FromCardSpot == 1)
                CardSpot = 3;
            if (FromCardSpot == 2)
                CardSpot = 0;
            if (FromCardSpot == 3)
                CardSpot = 1;

            Player pl = m_Board.HasAlien(p, CardSpot);
            if (pl != null)
            {
                PLayerScore PS = new PLayerScore();
                PS.m_Player = pl;
                PS.m_Punt = m_PlacedCard;
                Score.Add(PS);
            }
  
            if (m_Board.GetCard(p).GetAttribute(4) == CardAttributes.None ||
                m_Board.GetCard(p).GetAttribute(4) == CardAttributes.SpaceStation ||
                m_Board.GetCard(p).GetAttribute(4) == CardAttributes.Satellite)
            {
                return false;
            }

            for(int i = 0; i <=3; i++)
            {
                if(i != CardSpot && m_CurrentCard.GetAttribute(i) == CardAttributes.RainbowRoad)
                {
                    Player pll = m_Board.HasAlien(p, i);
                    if (pll != null)
                    {
                        PLayerScore PS = new PLayerScore();
                        PS.m_Player = pll;
                        PS.m_Punt = m_PlacedCard;
                        Score.Add(PS);
                    }

                    CCPoint NextCardpos = p;

                    if (i == 0)
                    {
                        NextCardpos.Y -= 1;
                    }
                    else if (i == 1)
                    {
                        NextCardpos.X -= 1;
                    }
                    else if (i == 2)
                    {
                        NextCardpos.Y += 1;
                    }
                    else if (i == 3)
                    {
                        NextCardpos.X += 1;
                    }

                    Card NextCard = m_Board.GetCard(NextCardpos);

                    if (NextCard != null)
                    {
                        if (!CardList.Contains(NextCardpos))
                        {
                            if (FindRoadSpots(NextCardpos, i, ref CardList, ref Score))
                                return true;
                        }
                        else
                            return false;
                    }
                    else
                        return true;
                }
            }

                return true;
        }
    }
}