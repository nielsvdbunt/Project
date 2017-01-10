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

namespace ruigeruben
{
    class Player
    {

        public string Name { get; set; }      // naam van de speler 
        public int Points { get; set; }         // het aantal punten dat een speler heeft
        public bool Turn { get; set; }          // of de speler aan de beurt is 
        public int NumberOfAliens { get; set; } // aantal mannen in zijn hand
        public int OnField { get; set; }        // aantal mannen op het veld
        public string Kleur { get; set; }       // kleur van speler kan weggelaten worden maar misschien wel leuk



    }

    
}
