using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PersonajeTests
    {
        private Character testDummy;
        private Weapon physTest;
        private Weapon magicTest;

        [SetUp]
        public void Setup()
        {
            testDummy = ScriptableObject.CreateInstance<Character>();
            testDummy.SetStats(1, 50, 50, 5, 3, 5, 3, 0);

            physTest = ScriptableObject.CreateInstance<Weapon>();
            physTest.SetStats(Tipo.PHYSICAL, 1);
            
            magicTest = ScriptableObject.CreateInstance<Weapon>();
            magicTest.SetStats(Tipo.MAGIC, 1);
        }

        [TearDown]
        public void TearDown()
        {
            testDummy.SetStats(1, 50, 50, 5, 3, 5, 3, 0);
        }

        [Test]
        public void StatsSetUpTest()
        {
            Assert.AreEqual(testDummy.attackPower, 5);
            Assert.AreEqual(physTest.fuerza, 1);
            Assert.True(magicTest.tipo == Tipo.MAGIC);
        }

        [Test]
        public void TakePhysicalDamageTest()
        {
            bool aux = testDummy.TakeDamage(5, physTest);
            Assert.AreEqual(testDummy.currentHP, 49);
        }

        [Test]
        public void TakeMagicDamageTest()
        {
            bool aux = testDummy.TakeDamage(5, magicTest);
            Assert.AreEqual(testDummy.currentHP, 47);
        }

    }
}
