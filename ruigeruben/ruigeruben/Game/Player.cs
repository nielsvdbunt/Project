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

namespace ruigeruben
{
    class Player //this class is for a player with simple properties
    {
        public string Name { get; set; }        // name of players
        public int Points { get; set; }         // points of players
        public bool Turn { get; set; }          // if it is their turn
        public int NumberOfAliens { get; set; } // number of aliens of players
        public int OnField { get; set; }        // number of aliens on field
        public CCColor3B PlayerColor { get; set;}   // color of the player
    }

    
}
