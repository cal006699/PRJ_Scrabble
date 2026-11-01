using MySql.Data.MySqlClient;
using Scrabble2Joueurs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2Joueurs.Business
{
    internal class Partie
    {
        private DateTime datePartie;
        private int J1;
        private int J2;
        private int scoreJ1;
        private int scoreJ2;

        public Partie(DateTime uneDate, int unJ1, int unJ2, int unScoreJ1, int unScoreJ2)
        {
            this.datePartie = uneDate;
            this.J1 = unJ1;
            this.J2 = unJ2;
            this.scoreJ1 = unScoreJ1;
            this.scoreJ2 = unScoreJ2;
        }

        public void getStat()
        {
            using (MySqlConnection conn = connexion.Commit())
            {

            }
        }
    } 
}
