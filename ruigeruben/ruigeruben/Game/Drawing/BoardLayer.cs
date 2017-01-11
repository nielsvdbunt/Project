using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using CocosSharp;

namespace ruigeruben
{
    class BoardLayer : CCLayer
    {
        float scale = 1;
        Board board;
        CCSprite spr2;

        //http://stackoverflow.com/questions/31502314/how-to-zoom-with-two-fingers-on-imageview-in-android
        public BoardLayer(Board b)
        {
            this.AnchorPoint = new CCPoint(0, 0);
            board = b;
        }

        public void AddPanda(int x, int y)
        {
            CCSprite spr = new CCSprite("Panda");
            CCSprite spr2 = new CCSprite("Panda");
            spr2.PositionX = x + 500;
            spr2.PositionY = y + 200;

            spr.PositionX = x;
            spr.PositionY = y;
            AddChild(spr2);
            AddChild(spr);
        }

        public void t(CCPoint p)
        {


            scale /= 2f;

            CCPoint NewMiddlePoint = new CCPoint(p);
            this.Position = NewMiddlePoint;
            
          /*  CCPoint old = new CCPoint(p.X * this.ScaleX, p.Y * this.ScaleY);

            this.Scale = scale;

            CCPoint New = new CCPoint(p.X * this.ScaleX, p.Y * this.ScaleY);

            CCPoint delta = old - New;

            this.Position += delta;
            */

        }

        public void AddBoard()
        {
            for(int t = 0; t < board.m_virCards.Count; t++)
            {
                AddCard(board.m_virCards[t], board.m_virLocations[t]);
            }
        }

        public void AddCard(Card card, Point point)
        { //hier moet de kaart getekent worden en het punt omgezet worden naar pixels
          //de middelste point in m_virLocation is 0,0 dus 1 plek naar links wordt -1,0 en 1 naar boven 0,1

        }

    }
    
}