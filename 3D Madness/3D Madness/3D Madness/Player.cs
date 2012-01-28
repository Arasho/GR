using System.Collections.Generic;

namespace _3D_Madness
{

    public class Player
    {
        #region Properties & arguments

        private string playerName;

        public int playerNr { get; set; }

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

        private int numberOfLittlePowns;

        public int NumberOfLittlePowns
        {
            get { return numberOfLittlePowns; }
            set { numberOfLittlePowns = value; }
        }

        private int bigPown;

        public int BigPown
        {
            get { return bigPown; }
            set { bigPown = value; }
        }

        private List<Pawn> pawns;

        public List<Pawn> Pawns {
            get { return pawns; }
            set { pawns = value; }
        }

        // arek cos mowil ze pisze ustawianie pionkow na planszy, to pewnie tutaj bedzie trzeba stworzyc jakas zmienna jego klasy odpowiadajaca za ilosc pionkow kazdego gracza

        #endregion Properties & arguments

        #region Constructors

        public Player(string _playerName, int _color)
        {
            pawns = new List<Pawn>();
            playerName = _playerName;
            playerPoints = 0;
            playerColor = _color;
            numberOfLittlePowns = 7;
            bigPown = 1;
            playerNr = _color;
        }

        #endregion Constructors

        #region Methods

        #endregion Methods
    }
}