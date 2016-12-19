using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    class HelpMenu : AbstractMenu
    {
        public HelpMenu()
        {

        }

        public override void OnClick(CCPoint Location)
        {

        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            CCRect bounds = VisibleBoundsWorldspace;

            CCSprite s = new CCSprite("Panda");
            s.Position = new CCPoint(bounds.Center.X, bounds.Center.Y);

            AddChild(s);


        }
    }
}