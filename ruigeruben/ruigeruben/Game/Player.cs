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
        public string Name { get; set; }        // naam van de speler 
        public int Points { get; set; }         // het aantal punten dat een speler heeft
        public bool Turn { get; set; }          // of de speler aan de beurt is 
        public int NumberOfAliens { get; set; } // aantal mannen in zijn hand
        public int OnField { get; set; }        // aantal mannen op het veld
        public CCColor3B PlayerColor { get; set;}   // kleur van mannen op veld en naam van speler
    }

    
}
