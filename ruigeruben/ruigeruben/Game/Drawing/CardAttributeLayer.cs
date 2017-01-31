using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CocosSharp;

namespace ruigeruben
{
    class CardAttributeLayer : CCLayer
    {
        public void DrawAliens(Card c, CCPoint p)
        {

            for (int i = 0; i < 6; i++)
                if (c.GetAttribute(i) != 0)
                {
                    var drawNode = new CCDrawNode();
                    drawNode.DrawEllipse(
                    rect: new CCRect(p.X, p.Y, 130, 130),
                    lineWidth: 5,
                    color: CCColor4B.Red);
                    //drawNode.Position = p;
                    AddChild(drawNode);
                }
        }
    }
}