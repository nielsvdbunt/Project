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
        CCSprite panda = new CCSprite("Panda");
        const int tilesize = 100;

        public BoardLayer()
        {
            this.AnchorPoint = new CCPoint(0, 0);
            
            //this.ContentSize = new CCSize(5000, 5000);
        }

        public void Zoom(bool In)
        {

        }

        public void Move(bool Left)
        {

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
            foreach(CCPoint p in AvaliblePointsList)
            {
                var drawNode = new CCDrawNode();
               
                var shape = new CCRect(p.X*100, p.Y*100, 100,100 );
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

            if (y < 0)
                diffy -= 1;
          
            CCPoint p = new CCPoint(diffx, diffy);
            return p;
        }

        public CCPoint fromLocation(CCPoint point) //from location to pixels
        {
            float x = point.X;
            float y = point.Y;
            //int middenx = 0 - (tilesize / 2);
            //int middeny = 0 - (tilesize / 2);
            x = x * tilesize;
            y = y * tilesize;
            CCPoint p = new CCPoint(x, y);
            return p;
        }

        public void DrawCard(Card card, CCPoint point)
        {
            CCPoint p = fromLocation(point);
            CCSprite sprite = TexturePool.GetSprite(card.m_Hash);
            sprite.AnchorPoint = new CCPoint(0, 0);
            sprite.Position = p;
            AddChild(sprite);
        }
    }
    
}