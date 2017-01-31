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
        public List<CCDrawNode> Rectangles = new List<CCDrawNode>();
        const int tilesize = 100;
        CCSprite m_LastSprite;
        float m_Scale = 1;
        public List<CCSprite> PossiblePositionsAliens = new List<CCSprite>();
 
        public BoardLayer()
        {
            this.AnchorPoint = new CCPoint(0, 0);
        }

        public void UpdateScale(float Scale)
        {
            if (Scale > 0 && m_Scale > 3)
                return;
            else if (Scale < 0 && m_Scale < 0.5)
                return;

            m_Scale += Scale;
            this.Scale = m_Scale;
        }

        public void RemoveRaster()
        {
            foreach (CCDrawNode r in Rectangles)
            {
                RemoveChild(r);
            }
        }

        public void DrawRaster(List<CCPoint> AvaliblePointsList)
        {
            foreach (CCDrawNode r in Rectangles)
            {
                RemoveChild(r);
            }
            Rectangles.Clear();
            var bounds = VisibleBoundsWorldspace;

            int HorLines = (int)bounds.Size.Width / 128;
            int VerLines = (int)bounds.Size.Height / 128;
            foreach (CCPoint p in AvaliblePointsList)
            {
                var drawNode = new CCDrawNode();

                var shape = new CCRect(p.X * tilesize - (tilesize / 2), p.Y * tilesize - (tilesize / 2), 100, 100);
                drawNode.DrawRect(shape, fillColor: CCColor4B.Transparent,
                borderWidth: 2,
                borderColor: CCColor4B.White);
                Rectangles.Add(drawNode);
                this.AddChild(drawNode);
            }
        }

        public CCPoint toLocation(CCPoint point) //from pixels to board location
        {
            int x = Convert.ToInt32(point.X);
            int y = Convert.ToInt32(point.Y);

            int diffx = x / tilesize;
            int diffy = y / tilesize;

            if (x < 0)
                diffx -= 1;

            if (y > 0)
                diffy += 1;

            CCPoint p = new CCPoint(diffx, diffy);
            return p;
        }

        public CCPoint fromLocation(CCPoint point) //from location to pixels
        {
            float x = point.X;
            float y = point.Y;
            x = x * tilesize;
            y = y * tilesize;
            CCPoint p = new CCPoint(x, y);
            return p;
        }

        public void DrawAlienPossiblePosition(Card c, CCPoint p)
        {
        
            if (c.GetAttribute(0) != 0 )
                FillCircleList(p, 0, -30);
            if (c.GetAttribute(1) != 0)
                FillCircleList(p, -40, 0);
            if (c.GetAttribute(2) != 0)
                FillCircleList(p, 0, 40);
            if (c.GetAttribute(3) != 0)
                FillCircleList(p, 30, 0);
            if (c.GetAttribute(4) != 0 && c.GetAttribute(4) != CardAttributes.intersection && c.GetAttribute(4) != CardAttributes.SpaceStation)
                FillCircleList(p, 0, 0);

            DrawCircles();

        }

        public void DrawCard(Card card, CCPoint point)
        {
            CCPoint p = fromLocation(point);
            m_LastSprite = TexturePool.GetSprite(card.m_Hash);
            m_LastSprite.Position = p;
            m_LastSprite.Rotation = card.GetRotation();
            AddChild(m_LastSprite);
        }

        public void DeleteLastCard()
        {
            RemoveChild(m_LastSprite);
        }

        public void FillCircleList(CCPoint p, int x, int y)
        {
            p *= 100;
            CCPoint DrawPoint = new CCPoint(p.X + x, p.Y + y);

            CCSprite spr = new CCSprite("alien1");
            spr.Position = DrawPoint;
            spr.Color = CCColor3B.Red;
            spr.Scale = 0.25f;
            PossiblePositionsAliens.Add(spr);

        }

        public void DrawCircles()
        {
            foreach (CCSprite d in PossiblePositionsAliens)
                AddChild(d);
        }

        public void DeleteCircles()
        {
            foreach (CCSprite d in PossiblePositionsAliens)
                RemoveChild(d);

            PossiblePositionsAliens.Clear();

        }

    }
}