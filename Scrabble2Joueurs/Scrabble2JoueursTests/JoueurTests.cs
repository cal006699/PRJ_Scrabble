    using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble2Joueurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2Joueurs.Tests
{
    [TestClass()]
    public class JoueurTests
    {
        [TestMethod()]
        public void JoueurTest()
        {
            Joueur j1 = new Joueur("moi");
            Assert.IsNotNull(j1);
        }

        [TestMethod()]
        public void AjouterMotTest()
        {
            Joueur j1 = new Joueur("moi");
            j1.AjouterMot("nager");
            Assert.AreEqual(1, j1.GetNbMots());
            Assert.AreEqual(6, j1.GetTotalPoints());
        }

        [TestMethod()]
        public void GetTotalPointsTest()
        {
            Joueur j1 = new Joueur("moi");
            j1.AjouterMot("nager");
            j1.AjouterMot("nager");
            Assert.AreEqual(12, j1.GetTotalPoints());
        }

        [TestMethod()]
        public void GetNbMotsTest()
        {
            Joueur j1 = new Joueur("moi");
            j1.AjouterMot("nager");
            Assert.AreEqual(1, j1.GetNbMots());
        }

        [TestMethod()]
        public void GetLesMotsTest()
        {
            Joueur j1 = new Joueur("moi");
            j1.AjouterMot("nager");
            var liste = j1.GetLesMots();
            Assert.AreEqual("nager", liste[0]);
        }

        [TestMethod()]
        public void MotMeilleurTest()
        {
            Joueur j1 = new Joueur("moi");
            j1.AjouterMot("va");    
            j1.AjouterMot("nager");
            Assert.AreEqual("nager", j1.MotMeilleur());
        }
    }
}