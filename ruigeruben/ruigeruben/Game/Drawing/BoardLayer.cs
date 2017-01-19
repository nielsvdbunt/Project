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

        public BoardLayer()
        {
           // this.AnchorPoint = new CCPoint(400, 300);
            //this.ContentSize = new CCSize(5000, 5000);
        }

        public void AddPanda(int x, int y)
        {
           
           
            CCSprite spr = new CCSprite("Panda");
           
            spr.PositionX = x;
            spr.PositionY = y;
           
           
        }

        public void Zoom(bool In)
        {

        }

        public void Move(bool Left)
        {

        }

        public void DrawRaster()
        {
            var bounds = VisibleBoundsWorldspace;
        
            int HorLines = (int)bounds.Size.Width / 128;
            int VerLines = (int)bounds.Size.Height / 128;

            for(int i = 0; i < HorLines; i++)
            {
                var drawNode = new CCDrawNode();
                AddChild(drawNode);
                /*var shape = new CCPoint(bounds
                drawNode.DrawLine()

                drawNode.DrawRect(shape, fillColor: CCColor4B.Transparent,
                    borderWidth: 4,
                    borderColor: CCColor4B.White);
                    */

            }
           
        }

        public void DrawShit()
        {

        }

        public CCPoint toLocation(CCPoint point) //from pixels to board location
        {
            var bounds = VisibleBoundsWorldspace;
            int x = Convert.ToInt32(point.X);
            int y = Convert.ToInt32(point.Y);
            int tile = 200;
            int middenx = Convert.ToInt32(bounds.Center.X) - (tile / 2);
            int middeny = Convert.ToInt32(bounds.Center.Y) - (tile / 2);
            int diffx = (x - middenx) / tile;
            int diffy = (y - middeny) / tile;

            CCPoint p = new CCPoint(diffx, diffy);
            return p;
        }

        public CCPoint fromLocation(CCPoint point) //from location to pixels
        {
            var bounds = VisibleBoundsWorldspace;
            int tile = 200;
            float x = point.X;
            float y = point.Y;
            int middenx = Convert.ToInt32(bounds.Center.X) - (tile / 2);
            int middeny = Convert.ToInt32(bounds.Center.X) - (tile / 2);
            x = middenx + (x * tile);
            y = middeny + (y * tile);
            CCPoint p = new CCPoint(x, y);
            return p;
        }

        public void DrawCard(Card card, CCPoint point)
        {
            //CCPoint p = fromLocation(point);
            CCPoint p = new CCPoint();
            p.X = point.X * 100;
            p.Y = point.Y * 100;
            CCSprite sprite = TexturePool.GetSprite(card.m_Hash);
            sprite.AnchorPoint = new CCPoint(0, 0);
            sprite.Position = p;
            AddChild(sprite);
        }
    }
    
}