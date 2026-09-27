using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Scrabble2Joueurs.Data;

namespace Scrabble2Joueurs
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Joueur j1;
        Joueur j2;
        int toursJ1 = 0;
        int toursJ2 = 0;
        string toursTxt = "/10 tours";
        List<string> l1 = new List<string>();
        List<string> l2 = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCommencer_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomJ1.Text != txtNomJ2.Text && txtNomJ1.Text != "" && txtNomJ2.Text != "")
            {
                J1.Text = txtNomJ1.Text;
                J2.Text = txtNomJ2.Text;
                J1Txt.Text = toursJ1 + toursTxt;
                J2Txt.Text = toursJ2 + toursTxt;

                j1 = new Joueur(txtNomJ1.Text);
                j2 = new Joueur(txtNomJ2.Text);

                lettrePlein(l1);
                lettrePlein(l2);

                lst1.Text = read_lst(l1);
                lst2.Text = read_lst(l2);
                BordureJoueur2.IsEnabled = false;
                BordureJoueur1.IsEnabled = true;
                btnCommencer.IsEnabled = false;
                txtNomJ1.IsEnabled = false;
                txtNomJ2.IsEnabled = false;
            }
            else
            {
                if (txtNomJ1.Text == txtNomJ2.Text)
                {
                    MessageBox.Show("Une erreur s'est produite. Veuillez saisir des noms différents pour les deux joueurs.", "Erreur de saisie");
                }
                else
                {
                    MessageBox.Show("Une erreur s'est produite. Veuillez saisir des noms pour les deux joueurs.", "Champs manquants");
                }
            }
        }

        private void btnJ1_Click(object sender, RoutedEventArgs e)
        {
            
            if (verifMot(txtMotJ1.Text, l1) == true)
            {
                j1.AjouterMot(txtMotJ1.Text);
                MessageBox.Show("Bien jouée vous avez maintenant " + j1.GetTotalPoints() + " points");
                BordureJoueur2.IsEnabled = true;
                BordureJoueur1.IsEnabled = false;
                l1 = RetirerLettres(txtMotJ1.Text, l1);
                l1 =lettrePlein(l1);
                lst1.Text = read_lst(l1);
                StatPartie.Text = StatPa();
                toursJ1++;
                J1Txt.Text = toursJ1 + toursTxt;
                Meilleurmot1.Text = j1.MotMeilleur();
                nbmot1.Text = j1.GetNbMots() + " mots joués \n Votre liste de mot: "+ read_lst(j1.GetLesMots());
            }
            else
            {
                MessageBox.Show("Une Erreur c'est produite. Veuillez saisir les lettres de votre liste de lettre.");
            }
            
        }

        private void btnJ2_Click(object sender, RoutedEventArgs e)
        {
            
            
            if (verifMot(txtMot.Text, l2) == true)
            {
                j2.AjouterMot(txtMot.Text);
                MessageBox.Show("Bien joué vous avez maintenant " + j2.GetTotalPoints() + " points");
                BordureJoueur1.IsEnabled = true;
                BordureJoueur2.IsEnabled = false;
                l2 = RetirerLettres(txtMot.Text, l2);
                l2 = lettrePlein(l2);
                lst2.Text = read_lst(l2);
                StatPartie.Text = StatPa();
                toursJ2++;
                J2Txt.Text = toursJ2 + toursTxt;
                Meilleurmot2.Text = j2.MotMeilleur();
                nbmot2.Text = j2.GetNbMots() + " mots joués \n Votre liste de mot: " + read_lst(j2.GetLesMots());
                if (toursJ1 == 10)
                {
                    EnregistrerFinDePartie(j1.GetNom(), j1.GetTotalPoints(), j2.GetNom(), j2.GetTotalPoints());
                    if (j1.GetTotalPoints() > j2.GetTotalPoints())
                    {
                        MessageBox.Show("Partie terminée, Le joueur " + j1.GetNom() + " a remporté la partie");
                        StatPartie.Text = "Vainqueur: " + j1.GetNom();
                    }
                    else
                    {
                        if (j1.GetTotalPoints() < j2.GetTotalPoints())
                        {
                            MessageBox.Show("Partie terminée, Le joueur " + j2.GetNom() + " a remporté la partie");
                            StatPartie.Text = "Vainqueur: " + j2.GetNom();
                        }
                        else
                        {
                            MessageBox.Show("Partie terminée, Les joueurs sont à égalité");
                            StatPartie.Text = "Match nul";
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Une Erreur s'est produite. Veuillez saisir les lettres de votre liste de lettres.");
            }
            
        }
            

        private Random rnd = new Random();
        public List<string> lettrePlein(List<string> lst)
        {
            //liste de charactère 
            string alphabet = "AIAEAABACDDDEEEAEFGAHIIJKLLMIMNANOEOOCOOPQARSTUIUICUUAVWEXYZ";
            while (lst.Count < 7)
            {
                // On choisit une lettre au hasard dans l'alphabet
                int i = rnd.Next(alphabet.Length);
                string lettreAleatoire = alphabet[i].ToString();

                lst.Add(lettreAleatoire);
            }

            return lst;
        }
        private string read_lst(List<string> lst)
        {
            string res = "";
            foreach (string s in lst)
            {
                res = res + s + " ";
            }
            return res;
        }

        private bool verifMot(string mot, List<string> lst)
        {
            string motUP = mot.ToUpper();

            for (int i = 0; i < motUP.Length; i++)
            {
                string lettreDuMot = motUP[i].ToString();

                // Est-ce que la lettre actuelle est présente dans la liste ?
                if (!lst.Contains(lettreDuMot))
                {
                    // Si même une seule lettre manque, le mot entier est invalide
                    return false;
                }
            }

            return true;
        }

        private List<string> RetirerLettres(string mot, List<string> lst)
        {

            string motUP = mot.ToUpper();

            foreach (char lettre in motUP)
            {
                string lettreStr = lettre.ToString();

                // Si la lettre est dans liste
                if (lst.Contains(lettreStr))
                {
                    lst.Remove(lettreStr);
                }
            }
            return lst;
        }
        private string StatPa()
        {
            string StatPartie = "";
            if (j1.GetTotalPoints() < j2.GetTotalPoints())
            {
                StatPartie = "Le joueur " + j2.GetNom() + " est en tête avec " + j2.GetTotalPoints() + " points contre " + j1.GetTotalPoints() + " points pour le joueur " + j1.GetNom();

            }
            else if (j1.GetTotalPoints() > j2.GetTotalPoints())
            {
                StatPartie = "Le joueur " + j1.GetNom() + " est en tête avec " + j1.GetTotalPoints() + " points contre " + j2.GetTotalPoints() + " points pour le joueur " + j2.GetNom();
            }
            else
            {
                StatPartie = "Les joueurs sont à égalité avec chacun " + j1.GetTotalPoints() + " points";
            }
            return StatPartie;
        }

        public void EnregistrerFinDePartie(string nomJ1, int score1, string nomJ2, int score2)
        {
            using (MySqlConnection conn = connexion.Commit())
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    // récuperer ou inserer le J 1  et 2
                    int idJ1 = GetOrInsertJoueur(conn, nomJ1);
                    int idJ2 = GetOrInsertJoueur(conn, nomJ2);

                    // Insérer la partie dans la table Partie
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

                        // Récupérer l'ID de la partie qui vient d'être créée
                        idPartie = Convert.ToInt32(cmd.LastInsertedId);
                    }

                    // Remplir la table d'association pour lier les deux joueurs à cette partie
                    InsererTableJouer(conn, idJ1, idPartie);
                    InsererTableJouer(conn, idJ2, idPartie);
                }
            }
        }

        // vérifie si le joueur existe, le crée s'il n'existe pas, et retourne son id_joueur
        private int GetOrInsertJoueur(MySqlConnection conn, string nom)
        {
            // Vérifier si le joueur existe déjà
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
        private void InsererTableJouer(MySqlConnection conn, int idJoueur, int idPartie)
        {
            string query = "INSERT INTO jouer (id_joueur, id_partie, Attribute1) VALUES (@idJ, @idP, @att)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@idJ", idJoueur);
                cmd.Parameters.AddWithValue("@idP", idPartie);
                cmd.Parameters.AddWithValue("@att", DBNull.Value); // Remplace par une valeur si besoin
                cmd.ExecuteNonQuery();
            }
        }
    }
}
