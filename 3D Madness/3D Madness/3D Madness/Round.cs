using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _3D_Madness
{
    class Round
    {
        #region variables and properties 

        private static bool putPown;
        public static bool PutPown
        {
            get { return putPown; }
            set { putPown = value; }
        }

        private static bool putElement;
        public static bool PutElement
        {
            get { return putElement; }
            set { putElement = value; }
        }

        private static int numberOfActivePlayer;
        public static int NumberOfActivePlayer
        {
            get { return numberOfActivePlayer; }
            set { numberOfActivePlayer = value; }
        }

        private static int numberOfPlayers;
        public static int NumberOfPlayers
        {
            get { return numberOfPlayers; }
            set { numberOfPlayers = value; }
        }

        private static int counfOfTurn;
        public static int CountOfTurn
        {
            get { return counfOfTurn; }
            set { counfOfTurn = value; }
        }
        

       #endregion

        #region Constructors

        #endregion

        #region Methods

        public static void NextTurn()
        {
            putElement = false;
            putPown = false;
            counfOfTurn++;
            numberOfActivePlayer = (counfOfTurn % numberOfPlayers) + 1;
        }

        public static void PuttingPowl()
        {
            putPown = true;
        }

        public static void PuttingElement()
        {
            putElement = true;
        }

        public static bool EndRound()
        {
            if (putElement == false)
            {
                MessageBox.Show("Musisz wylozyc kafelke z elementem planszy");
                return false;
            }
            else if (putPown == false && (Game1.listOfPlayers[numberOfActivePlayer - 1].NumberOfLittlePowns > 0 || Game1.listOfPlayers[numberOfActivePlayer - 1].BigPown > 0))
            {
                if (MessageBox.Show("Nie wylozyłes zadnego pionka. Czy chesz wylozyc przed zakonczeniem swojej tury?", "Brak pionka", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
