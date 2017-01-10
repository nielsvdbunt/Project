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
            string currentplayer = "gotvet";
            string currentpoints = "8";
            string currentaliens = "6";

            //hiet překrytí staví sám
            CCSprite overlay_1 = new CCSprite("overlay1");
            CCSprite overlay_2 = new CCSprite("overlay2");
            CCSprite muntje = new CCSprite("coin");
            CCSprite alien = new CCSprite("alien");
            CCLabel current_player = new CCLabel(currentplayer, "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
            CCLabel current_points= new CCLabel(currentpoints, "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
            CCLabel current_aliens = new CCLabel(currentaliens, "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);

            current_player.Position = new CCPoint(200, 130);
            current_player.Color = CCColor3B.Black;
            current_points.Position = new CCPoint(600, 130);
            current_points.Color = CCColor3B.Black;
            current_aliens.Position = new CCPoint(1000, 130);
            current_aliens.Color = CCColor3B.Black;

            overlay_1.AnchorPoint = new CCPoint(0, 0);
            overlay_2.AnchorPoint = new CCPoint(1, 1);
            overlay_2.Position = new CCPoint(1920, 1080);
            muntje.Position = new CCPoint(500, 120);
            alien.Position = new CCPoint(900, 120);

            AddChild(overlay_1);
            AddChild(overlay_2);
            AddChild(muntje);
            AddChild(alien);
            AddChild(current_player);
            AddChild(current_points);
            AddChild(current_aliens);
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