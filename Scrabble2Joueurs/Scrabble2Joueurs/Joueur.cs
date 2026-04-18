using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2Joueurs
{
    /// <summary>
    /// Classe Joueur
    /// </summary>
    public class Joueur
    {
        #region attributs privés
        private string nom;             // nom du joueur 
        private List<string> lesMots;   // liste des mots proposés par le joueur
        private int totalPoints;        // nombre total de points du joueur
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur de la classe Joueur
        /// Initialise l'attribut lesMots à une liste vide et l'attribut totalPoints à zéro
        /// </summary>
        /// <param name="unNom">nom du joueur</param>
        public Joueur(string unNom)
        {
            this.nom = unNom;
            this.lesMots = new List<string>();
            this.totalPoints = 0;
            //TODO
        }
        #endregion

        public string GetNom()
        {
            return this.nom;
        }

        #region méthodes
        /// <summary>
        /// Méthode qui permet d'ajouter un mot à la liste des mots du joueur
        /// et qui actualise le nombre total de points du joueur
        /// </summary>
        /// <param name="unMot">mot proposé par le joueur</param>
        public void AjouterMot(string unMot)
        {
            this.lesMots.Add(unMot);
            this.totalPoints += Utilitaire.PointsMot(unMot);
            //TODO
        }

        /// <summary>
        /// retourne le nombre total de points du joueur
        /// </summary>
        /// <returns>nombre total de points du joueur</returns>
        public int GetTotalPoints()
        {
            //TODO
            return this.totalPoints;
        }

        /// <summary>
        /// retourne le nombre de mots du joueur
        /// </summary>
        /// <returns>nombre de mots du joueur</returns>
        public int GetNbMots()
        {
            //TODO
            return this.lesMots.Count;
        }
        /// <summary>
        /// retourne la liste des mots du joueur
        /// </summary>
        /// <returns>liste de mots du joueur</returns>
        public List<string> GetLesMots()
        {
            //TODO
            
            return this.lesMots ;
        }

        /// <summary>
        /// retourne le mot qui a rapporté le plus grand nombre de points 
        /// parmi les mots proposés par le joueur
        /// </summary>
        /// <returns>mot qui a rapporté le plus grand nombre de points</returns>
        public string MotMeilleur()
        {
            //TODO
            string strmot = this.lesMots[0]; ;
            int meilleurMot = Utilitaire.PointsMot(strmot);
            for (int i = 1; i < lesMots.Count; i++)
            {
                int scoreActuel = Utilitaire.PointsMot(this.lesMots[i]);

                if (scoreActuel > meilleurMot)
                {
                    meilleurMot = scoreActuel;
                    strmot = this.lesMots[i];
                }
            }

            return strmot;
        }
        #endregion
        
    }
}
