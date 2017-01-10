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
            CCColor3B label_color = CCColor3B.White;

            //hiet překrytí staví sám
            CCSprite overlay = new CCSprite("overlay");
            CCSprite muntje = new CCSprite("coin");
            CCSprite alien = new CCSprite("alien");
            CCLabel current_player = new CCLabel(currentplayer, "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
            CCLabel current_points= new CCLabel(currentpoints, "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);
            CCLabel current_aliens = new CCLabel(currentaliens, "Fonts/Coalition", 36, CCLabelFormat.SpriteFont);

            current_player.Position = new CCPoint(200, 100);
            current_player.Color = label_color;
            current_points.Position = new CCPoint(650, 100);
            current_points.Color = label_color;
            current_aliens.Position = new CCPoint(900, 100);
            current_aliens.Color = label_color;

            overlay.AnchorPoint = new CCPoint(0, 0);
            muntje.Position = new CCPoint(500, 100);
            alien.Position = new CCPoint(800, 100);

            AddChild(overlay);
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