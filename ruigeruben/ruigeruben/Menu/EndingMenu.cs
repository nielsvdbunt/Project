using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace ruigeruben
{
    class EndingMenu : AbstractMenu
    {
        GameBase m_GameBase;

        private Player WhoWon()
        {
            Player SavePlayer =new Player();
            Player SavePlayer2 = new Player();
            for(int i = 0; i<= m_GameBase.m_Players.Count; i++)
            {
                if (m_GameBase.m_Players[i].Points > SavePlayer.Points)
                {
                    m_GameBase.m_Players[i] = SavePlayer;
                    SavePlayer2 = null;
                }
                else if (m_GameBase.m_Players[i].Points == SavePlayer.Points)
                    m_GameBase.m_Players[i] = SavePlayer2;
            }
            if (SavePlayer2 == null)
            {
                return SavePlayer;
            }
            else
            {
                CCLabel Tie = new CCLabel("It's a Tie!","Fonts/Coalition", 70, CCLabelFormat.SpriteFont);
                Tie.Position = new CCPoint(500, 500);
                AddChild(Tie);
            }
            return null;
        }

        public void Ending()
        {
            Player Winner = WhoWon();
            CCLabel TheWinner = new CCLabel("Congratulations" + Winner + "you won with" + Winner.Points, "Fonts/Coalition", 70, CCLabelFormat.SpriteFont);
            TheWinner.Position = new CCPoint(500, 500);
            AddChild(TheWinner);
        }




        public override void OnClick(CCPoint Location)
        {
            throw new NotImplementedException();
        }

        public override void OnBack()
        {
            throw new NotImplementedException();
        }


    }
}