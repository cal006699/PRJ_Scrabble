using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2Joueurs.Data
{
    internal static class JoueurDAO
    {
        public static int GetOrInsertJoueur(MySqlConnection conn, string nom)
        {
            // Verifier si le joueur existe deja
            string querySelect = "SELECT id_joueur FROM Joueur WHERE nom_joueur = @nom";
            using (MySqlCommand cmd = new MySqlCommand(querySelect, conn))
            {
                cmd.Parameters.AddWithValue("@nom", nom);
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
            }

            // il n'existe pas, on l'insère
            string queryInsert = "INSERT INTO Joueur (nom_joueur) VALUES (@nom)";
            using (MySqlCommand cmd = new MySqlCommand(queryInsert, conn))
            {
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.ExecuteNonQuery();

                // Retourne l'ID 
                return Convert.ToInt32(cmd.LastInsertedId);
            }
        }

        // remplit la table d'association
        public static void InsererTableJouer(MySqlConnection conn, int idJoueur, int idPartie, int victoire)
        {
            string query = "INSERT INTO jouer (id_joueur, id_partie, victoire) VALUES (@idJ, @idP, @vic)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@idJ", idJoueur);
                cmd.Parameters.AddWithValue("@idP", idPartie);
                cmd.Parameters.AddWithValue("@vic", victoire);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
