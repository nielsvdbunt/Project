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
            this.ContentSize = new CCSize(5000, 5000);
        }

        public void AddPanda(int x, int y)
        {
            CCSpriteSheet sheet = new CCSpriteSheet("sheet.plist", "sheetimage.png");
            CCSpriteFrame frame;
            frame = sheet.Frames.Find(item => item.TextureFilename == "02220.png");
            CCSprite satteliete = new CCSprite(frame);
            CCSprite spr = new CCSprite("Panda");
            satteliete.Position = new CCPoint(500, 500);
            spr.PositionX = x;
            spr.PositionY = y;
            AddChild(satteliete);
           // AddChild(spr);
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