using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;


// de instellingen voordat het spel gaat beginnen
namespace ruigeruben
{
    class SettingMenu : AbstractMenu
    {
        CCRect bounds;
        protected override void AddedToScene()
        {
            base.AddedToScene();

            bounds = VisibleBoundsWorldspace;

            CCLabel Title = new CCLabel("Settings", "Fonts/Coalition", 70, CCLabelFormat.SpriteFont);
            Title.Position = new CCPoint(bounds.Center.X, 950);
            AddChild(Title);
        }
        public override void OnClick(CCPoint Location)
        {
            throw new NotImplementedException();
        }

        public override void OnBack()
        {
            CCLog.Log("test");
            //MainActivity.SwitchToMenu(SceneIds.OpeningMenu);
        }
    }
}