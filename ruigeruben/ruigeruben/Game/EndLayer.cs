using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CocosSharp;

namespace ruigeruben.Game
{
    class EndLayer : CCLayerColor
    {
        List<Player> players;
        public EndLayer(List<Player> playerlist) : base(new CCColor4B(30,30,100,150))
        {
            players = playerlist.OrderByDescending(o => o.Points).ToList();
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            string Font = "Fonts/Coalition", WinnerText = " Won the Game!";
            int FontSize = 60;
            int order = 0;

            CCLabel Winner = new CCLabel(players[0].Name + WinnerText , Font, 70, CCLabelFormat.SpriteFont);
            Winner.Position = new CCPoint(VisibleBoundsWorldspace.MidX, VisibleBoundsWorldspace.MaxY - 200);
            Winner.Color = players[0].PlayerColor;
            AddChild(Winner);

            AddChild(new CCParticleFireworks(new CCPoint(300, 300)));
            AddChild(new CCParticleFireworks(new CCPoint(1620, 300)));

            foreach (Player p in players)
            {
                CCLabel Stats = new CCLabel(p.Name+ " \t" + p.Points.ToString(), Font, FontSize, CCLabelFormat.SpriteFont);
                Stats.Position = new CCPoint(VisibleBoundsWorldspace.MidX, VisibleBoundsWorldspace.MaxY - 500 - 100 * order);
                Stats.Color = p.PlayerColor;
                AddChild(Stats);
                order++;
            }
        }
    }
}