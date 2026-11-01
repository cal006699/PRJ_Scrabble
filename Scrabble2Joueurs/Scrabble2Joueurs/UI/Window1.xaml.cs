using MySql.Data.MySqlClient;
using Scrabble2Joueurs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Scrabble2Joueurs.UI
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            ChargerHistorique();
        }

        private void ChargerHistorique()
        {
            // Récupère l'historique depuis la base de données et l'affiche dans le DataGrid
            dgHistorique.ItemsSource = PartieDAO.GetHistoriqueParties();
        }

        private void btnNouvellePartie_Click(object sender, RoutedEventArgs e)
        {
            // Ouvre la fenêtre principale du jeu
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Ferme la fenêtre d'historique
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