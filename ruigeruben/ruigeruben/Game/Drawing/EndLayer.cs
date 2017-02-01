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
    class EndLayer : CCLayerColor //this class is for the end screen
    {
        List<Button> m_buttons;
        List<Player> players;
        public EndLayer(List<Player> playerlist) : base(new CCColor4B(30,30,100,150))
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;

            AddEventListener(touchListener, this);

            m_buttons = new List<Button>();
            players = playerlist.OrderByDescending(o => o.Points).ToList();
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            string Font = "Fonts/Coalition", WinnerText = " Won the Game!";
            int FontSize = 60;
            int order = 0;

            CCLabel Winner = new CCLabel(players[0].Name + WinnerText, Font, 70, CCLabelFormat.SpriteFont);
            Winner.Position = new CCPoint(VisibleBoundsWorldspace.MidX, VisibleBoundsWorldspace.MaxY - 100);
            Winner.Color = players[0].PlayerColor;
            AddChild(Winner);

            AddChild(new CCParticleFireworks(new CCPoint(300, 300)));
            AddChild(new CCParticleFireworks(new CCPoint(1620, 300)));

            foreach (Player p in players)
            {
                CCLabel Stats = new CCLabel(p.Name + " \t" + p.Points.ToString(), Font, FontSize, CCLabelFormat.SpriteFont);
                Stats.Position = new CCPoint(VisibleBoundsWorldspace.MidX, VisibleBoundsWorldspace.MaxY - 300 - 100 * order);
                Stats.Color = p.PlayerColor;
                AddChild(Stats);
                order++;
            }

            Button NewGame = new Button("Start new Game", new CCPoint(VisibleBoundsWorldspace.MidX, VisibleBoundsWorldspace.MinY + 100), Font, 70, this);
            NewGame.OnClicked += delegate
            {
                players = players.OrderBy(o => o.Name).ToList();
                List<InputPlayer> inputplayers = new List<InputPlayer>();
                foreach(Player p in players)
                {
                    InputPlayer ip = new InputPlayer();
                    ip.Name = p.Name;
                    ip.Color = p.PlayerColor;
                    inputplayers.Add(ip);
                }
                MainActivity.SwitchToMenu(SceneIds.PlayMenu, inputplayers);
            };
            m_buttons.Add(NewGame);
                
            
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            foreach (CCTouch i in touches)
            {
                if (touches.Count > 0)
                {
                    CCPoint location = touches[0].LocationOnScreen;
                    OnClick(location);
                }
            }
        }

        public void OnClick(CCPoint Location)
        {
            Location = ScreenToWorldspace(Location);

            foreach (Button b in m_buttons)
            {
                if (b.OnClickEvent(Location))
                    return;
            }
          
        }
    }
}