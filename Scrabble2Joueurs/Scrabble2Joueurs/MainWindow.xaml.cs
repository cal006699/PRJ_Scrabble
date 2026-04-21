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
                debut.IsEnabled = false;

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

            string alphabet = "AAAAAAABCCCDDDEEEEEEEFGHIIIIIJKLLMMNNOOOOOPQRSTUUUUVWXYZ";
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
    }
}
