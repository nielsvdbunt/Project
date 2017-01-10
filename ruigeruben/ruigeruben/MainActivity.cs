using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CocosSharp;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    public enum SceneIds
    {
        OpeningMenu,
        PlayMenu,
        HelpMenu,
        SettingsMenu,
        Game
           
    }

    [Activity(Label = "Spacesonne", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", MainLauncher = true, Icon = "@drawable/icon",
        AlwaysRetainTaskState = true,
        ScreenOrientation = ScreenOrientation.SensorLandscape,
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
    public class MainActivity : Activity
    {
        static CCGameView m_GameView;
        static bool m_InMenu;
        static AbstractMenu m_CurrentMenu;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our game view from the layout resource,
            // and attach the view created event to it
            CCGameView gameView = (CCGameView)FindViewById(Resource.Id.GameView);
            gameView.ViewCreated += LoadGame;
        }

        void LoadGame(object sender, EventArgs e)
        {
            m_GameView = sender as CCGameView;
            
            if (m_GameView != null)
            {
                var contentSearchPaths = new List<string>() { "Fonts", "Sounds", "Images" };
                CCSizeI viewSize = m_GameView.ViewSize;

                int width = 1920;
                int height = 1080;

                // Set world dimensions
                m_GameView.DesignResolution = new CCSizeI(width, height);

                m_GameView.ContentManager.SearchPaths = contentSearchPaths;

                SwitchToMenu(SceneIds.OpeningMenu, 0);
                //CCScene scene = new MenuScene(gameView);
               // gameView.RunWithScene(ms);
                
                //CCScene gameScene = new CCScene(gameView);
               // gameScene.AddLayer(new GameLayer());
                //gameView.RunWithScene(gameScene);
            }
        }

        public override void OnBackPressed()
        {
           if(m_InMenu)
            {
                m_CurrentMenu.OnBack();
            }
        }
      
        public static void SwitchToMenu(SceneIds id, Object o)
        {
            if(id == SceneIds.Game)
            {
                m_InMenu = false;
                GameScene gs = new GameScene(m_GameView, (InputGameInfo) o);       
                m_GameView.Director.ReplaceScene(gs);
                return;
            }

            m_InMenu = true;
            CCScene scene = new CCScene(m_GameView);

            if (id == SceneIds.OpeningMenu)
            {
                scene.AddLayer(new BackgroundLayer("achtergrond"));
                scene.AddLayer(m_CurrentMenu = new OpeningMenu());
            }
            if (id == SceneIds.PlayMenu)
            {
                scene.AddLayer(new BackgroundLayer("achtergrond"));
                scene.AddLayer(m_CurrentMenu = new PlayMenu());
            }
            if (id == SceneIds.HelpMenu)
            {
                scene.AddLayer(new BackgroundLayer("achtergrond"));
                scene.AddLayer(m_CurrentMenu = new HelpMenu());
            }
            if (id == SceneIds.SettingsMenu)
            {
                scene.AddLayer(new BackgroundLayer("achtergrond"));
                scene.AddLayer(m_CurrentMenu = new SettingMenu());
            }

            m_GameView.Director.ReplaceScene(scene);

        }
    }
}

