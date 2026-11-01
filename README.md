# 🎲 PRJ_Scrabble - Mode 2 Joueurs

Application de bureau développée en **C# (WPF)** avec une persistance des données sous **MySQL**, réalisée dans le cadre de projets informatiques.

---

## 🎯 Principe du mode 2 joueurs

* **Saisie initiale :** Au début de la partie, les prénoms des deux joueurs sont saisis[cite: 5].
* **Alternance des tours :** Les joueurs jouent à tour de rôle. L'application désigne aléatoirement le joueur qui commence[cite: 5].
* **Proposition de mots :** Chaque joueur propose un mot composé à partir des lettres tirées au sort par l'application[cite: 5].
* **Suivi des scores :** Tout au long de la partie, les scores et statistiques de chaque joueur sont affichés en temps réel[cite: 5].
* **Fin de partie :** La partie s'arrête lorsque chaque joueur a proposé 10 mots. L'application affiche le nom du gagnant, la liste de ses 10 mots ainsi que son meilleur mot (celui ayant rapporté le plus de points)[cite: 5].

---

## 🚀 Fonctionnalités principales

* **Gestion de l'historique :** Une fenêtre dédiée (`Window1`) affiche sous forme de tableau (`DataGrid`) l'historique complet des parties enregistrées en base de données (ID, date, joueurs, scores).
* **Navigation fluide :** Possibilité de basculer facilement entre l'historique et le jeu en cours grâce aux boutons *Rejouer* et *Historique*.
* **Interface moderne (WPF) :** Design soigné avec des coins arrondis, une charte graphique claire et des codes couleurs distincts pour chaque joueur.
* **Architecture DAO :** Séparation propre de la logique métier et des interactions avec la base de données (`JoueurDAO`, `PartieDAO`).

---

## 🛠️ Stack technique

* **Langage :** C# (.NET / WPF)
* **Base de données :** MySQL (`MySql.Data`)
* **Interface Graphique :** XAML
* **Outils de conception :** [Maquette Figma du projet](https://www.figma.com/make/p2KJwtdMmoBWrRC3DRQHUs/Site-Scrabble?t=eA8ohSLmpMzJObf7-20&fullscreen=1)[cite: 5]

---

## ⚙️ Installation et Lancement

1. Clone le dépôt sur ta machine :
   ```bash
   git clone [https://gitlab.com/ton-compte/prj-scrabble.git](https://gitlab.com/ton-compte/prj-scrabble.git)
