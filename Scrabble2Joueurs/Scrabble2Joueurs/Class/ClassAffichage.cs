using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2Joueurs
{
    public class PartieAffichage
    {
        public int IdPartie { get; set; }
        public string DatePartie { get; set; }
        public string Joueur1 { get; set; }
        public int Score1 { get; set; }
        public string Joueur2 { get; set; }
        public int Score2 { get; set; }
    }
}