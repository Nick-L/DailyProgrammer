using DailyProgrammer.Intermediate.Challenge_267;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyProgrammerTests.Intermediate.Challenge_267
{
    [TestClass]
    public class ViveLaResistance_Tests
    {
        const string SAMPLE_INPUT_PATH = "Resources/Challenge_267/sampleInputCircuit.txt";
        const string CHALLENGE_INPUT_PATH_1 = "Resources/Challenge_267/challengeInputCircuit_1.txt";
        const string CHALLENGE_INPUT_PATH_2 = "Resources/Challenge_267/challengeInputCircuit_2.txt";
        const string CHALLENGE_INPUT_PATH_2a = "Resources/Challenge_267/challengeInputCircuit_2a.txt";

        [TestMethod]
        public void SampleInput()
        {
            Assert.AreEqual(57.5, ViveLaResistance.GetResistance(SAMPLE_INPUT_PATH));
        }

        [TestMethod]
        public void ChallengeInput_1()
        {
            Assert.AreEqual(12.857, ViveLaResistance.GetResistance(CHALLENGE_INPUT_PATH_1));
        }

        [TestMethod]
        public void ChallengeInput_2()
        {
            Assert.AreEqual(38922.655, ViveLaResistance.GetResistance(CHALLENGE_INPUT_PATH_2));
        }

        [TestMethod]
        public void ChallengeInput_2a()
        {
            Assert.AreEqual(10, ViveLaResistance.GetResistance(CHALLENGE_INPUT_PATH_2a));
        }
    }
}
