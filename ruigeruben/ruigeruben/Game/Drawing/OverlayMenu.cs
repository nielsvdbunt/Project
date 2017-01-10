﻿using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class OverlayMenu : AbstractMenu
    {
        public OverlayMenu()
        {
            string players = "gotvet";

            //hiet překrytí staví sám
            CCSprite overlay_1 = new CCSprite("overlay1");
            CCSprite overlay_2 = new CCSprite("overlay2");
            CCSprite muntje = new CCSprite("coin");
            CCLabel tekstjevandenaam = new CCLabel(players, "Fonts/Coalition", 36, CCLabelFormat.SpriteFont); 
            tekstjevandenaam.Position = new CCPoint(200, 130);

            tekstjevandenaam.Color = CCColor3B.Black;

            overlay_1.AnchorPoint = new CCPoint(0, 0);
            overlay_2.AnchorPoint = new CCPoint(1, 1);
            overlay_2.Position = new CCPoint(1920, 1080);
            muntje.Position = new CCPoint(500, 120);

            AddChild(overlay_1);
            AddChild(overlay_2);
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