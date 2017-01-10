using System;
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

            CCSprite overlay_1 = new CCSprite("Overlay");
            CCSprite muntje = new CCSprite("coin");
            CCLabel tekstjevandenaam = new CCLabel(players, "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            tekstjevandenaam.Position = new CCPoint(200, 130);

            overlay_1.AnchorPoint = new CCPoint(0, 0);
            muntje.Position = new CCPoint(600, 100);

            AddChild(overlay_1);
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