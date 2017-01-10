using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class Overlay : AbstractMenu
    {
        public Overlay()
        {
            string players = "gotvet";

            //hiet překrytí staví sám
            CCSprite overlay = new CCSprite("overlay");
            CCSprite muntje = new CCSprite("coin");
            CCLabel tekstjevandenaam = new CCLabel(players, "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);

            tekstjevandenaam.Position = new CCPoint(200, 130);
            tekstjevandenaam.Color = CCColor3B.Black;

            overlay.AnchorPoint = new CCPoint(0, 0);
            muntje.Position = new CCPoint(500, 100);

            AddChild(overlay);
            AddChild(muntje);
            AddChild(tekstjevandenaam);
        }
        public override void OnBack()
        {
            throw new NotImplementedException();
        }

        public override void OnClick(CCPoint Location)
        {
           
        }
    }
}