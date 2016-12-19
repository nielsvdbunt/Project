using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CocosSharp;

namespace ruigeruben
{
    public enum SceneIds
    {
        OpeningMenu,
        PlayMenu,
        Game
           
    }

    [Activity(Label = "ruigeruben", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", MainLauncher = true, Icon = "@drawable/icon",
        AlwaysRetainTaskState = true,
        ScreenOrientation = ScreenOrientation.Landscape,
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
    public class MainActivity : Activity
    {
        static CCGameView m_GameView;
       

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

                SwitchToMenu(SceneIds.OpeningMenu);
                //CCScene scene = new MenuScene(gameView);
               // gameView.RunWithScene(ms);
                
                //CCScene gameScene = new CCScene(gameView);
               // gameScene.AddLayer(new GameLayer());
                //gameView.RunWithScene(gameScene);
            }
        }

        public static void SwitchToMenu(SceneIds id)
        {
            CCScene scene = new CCScene(m_GameView);

            if (id == SceneIds.OpeningMenu)
                scene.AddLayer(new OpeningMenu());

            if (id == SceneIds.PlayMenu)
                scene.AddLayer(new PlayMenu());


            m_GameView.Director.ReplaceScene(scene);

        }
    }
}

