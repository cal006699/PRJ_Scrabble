using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Scrabble2Joueurs.Data
{
    internal static class PartieDAO
    {
        public static void InsertPartie(string nomJ1, int score1, string nomJ2, int score2)
        {
            using (MySqlConnection conn = connexion.Commit())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    // Récupérer ou insérer le J1 et J2[cite: 7, 17]
                    int idJ1 = JoueurDAO.GetOrInsertJoueur(conn, nomJ1);
                    int idJ2 = JoueurDAO.GetOrInsertJoueur(conn, nomJ2);

                    // Insérer la partie dans la table Partie[cite: 7, 17]
                    string queryPartie = "INSERT INTO Partie (date, score1, score2, joueur1, joueur2) VALUES (@date, @s1, @s2, @j1, @j2)";
                    int idPartie = 0;
                    using (MySqlCommand cmd = new MySqlCommand(queryPartie, conn))
                    {
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@s1", score1);
                        cmd.Parameters.AddWithValue("@s2", score2);
                        cmd.Parameters.AddWithValue("@j1", idJ1);
                        cmd.Parameters.AddWithValue("@j2", idJ2);
                        cmd.ExecuteNonQuery();

                        // Récupérer l'ID de la partie qui vient d'être créée[cite: 7, 17]
                        idPartie = Convert.ToInt32(cmd.LastInsertedId);
                    }

                   
                    int vicJ1 = 0;
                    int vicJ2 = 0;

                    if (score1 > score2)
                    {
                        vicJ1 = 1; // Victoire J1
                        vicJ2 = 0;
                    }
                    else if (score1 < score2)
                    {
                        vicJ1 = 0;
                        vicJ2 = 1; // Victoire J2
                    }
                    else
                    {
                        // Match nul
                        vicJ1 = 0;
                        vicJ2 = 0;
                    }

                    // Remplir la table d'association avec les indicateurs de victoire
                    JoueurDAO.InsererTableJouer(conn, idJ1, idPartie, vicJ1);
                    JoueurDAO.InsererTableJouer(conn, idJ2, idPartie, vicJ2);
                }
            }
        }

        public static List<PartieAffichage> GetHistoriqueParties()
        {
            List<PartieAffichage> liste = new List<PartieAffichage>();
            using (MySqlConnection conn = connexion.Commit())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    string query = @"SELECT p.id_partie, p.date, 
                                    j1.nom_joueur AS NomJ1, p.score1, 
                                    j2.nom_joueur AS NomJ2, p.score2
                             FROM Partie p
                             JOIN Joueur j1 ON p.joueur1 = j1.id_joueur
                             JOIN Joueur j2 ON p.joueur2 = j2.id_joueur
                             ORDER BY p.id_partie DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                liste.Add(new PartieAffichage
                                {
                                    IdPartie = reader.GetInt32("id_partie"),
                                    DatePartie = reader.GetDateTime("date").ToString("yyyy-MM-dd"),
                                    Joueur1 = reader.GetString("NomJ1"),
                                    Score1 = reader.GetInt32("score1"),
                                    Joueur2 = reader.GetString("NomJ2"),
                                    Score2 = reader.GetInt32("score2")
                                });
                            }
                        }
                    }
                }
            }
            return liste;
        }
    }
}