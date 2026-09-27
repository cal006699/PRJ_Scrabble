using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2Joueurs.Data
{
    internal static class connexion
    {
        public static MySqlConnection Commit()
        {
            
            string chainConnex = "Server=localhost;Database=bdd_scrabble;Uid=calvinvu;Pwd=Soleil@";

            MySqlConnection connex = new MySqlConnection(chainConnex);

            try
            {
                connex.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'ouverture de la connexion : " + ex.Message);
            }

            return connex;
        }
    }
}
