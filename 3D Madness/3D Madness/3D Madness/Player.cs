using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3D_Madness
{
    public class Player
    {

        #region Properties & arguments
        private string playerName;
        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        private int playerPoints;
        public int PlayerPoints
        {
            get { return playerPoints; }
            set { playerPoints = value; }
        }

        public enum player_Color
        {
            yellow = 1,
            red = 2,
            blue = 3,
            green = 4,
            black = 5
        }

        private int playerColor;
        public int PlayerColor
        {
            get { return playerColor; }
            set { playerColor = value; }
        }
        


        // arek cos mowil ze pisze ustawianie pionkow na planszy, to pewnie tutaj bedzie trzeba stworzyc jakas zmienna jego klasy odpowiadajaca za ilosc pionkow kazdego gracza

        #endregion

        #region Constructors
        
        public Player(string _playerName, int _color)
        {
            playerName = _playerName;
            playerPoints = 0;
            playerColor = _color;
        }

        #endregion

        #region Methods


        #endregion

    }
}
