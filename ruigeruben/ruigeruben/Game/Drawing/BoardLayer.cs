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
            this.AnchorPoint = new CCPoint(0, 0);
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
                this.AddChild(drawNode);
                /*var shape = new CCPoint(bounds
                drawNode.DrawLine()

                drawNode.DrawRect(shape, fillColor: CCColor4B.Transparent,
                    borderWidth: 4,
                    borderColor: CCColor4B.White);
                    */

            }


           
        }

        public void DrawCard(string Card, Point point)
        { 
            
        }

        public void DrawShit()
        {

        }

    }
    
}