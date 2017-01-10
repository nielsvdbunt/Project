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
            string curplayer = "gotvet";
            string currentpoints = "8";

            //hiet překrytí staví sám
            CCSprite overlay = new CCSprite("overlay");
            CCSprite muntje = new CCSprite("coin");
            CCSprite alien = new CCSprite("alien");
            CCLabel current_player = new CCLabel(curplayer, "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);

            current_player.Position = new CCPoint(200, 130);
            current_player.Color = CCColor3B.Black;

            overlay.AnchorPoint = new CCPoint(0, 0);
            muntje.Position = new CCPoint(500, 120);
            alien.Position = new CCPoint(700, 120);

            AddChild(overlay);
            AddChild(muntje);
            AddChild(alien);
            AddChild(current_player);
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