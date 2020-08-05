using DailyProgrammer.Intermediate.Challenge_267;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyProgrammerTests.Intermediate.Challenge_267
{
    [TestClass]
    public class Input_Tests
    {
        string sampleInputPath = "Resources/Challenge_267/sampleInputCircuit.txt";

        [TestMethod]
        public void SampleInputTest()
        {
            double[,] adjacencyMatrix = ViveLaResistance.ConvertInputToAdjacencyMatrix(sampleInputPath);
            Assert.AreEqual(adjacencyMatrix[1,2], 50);
        }
    }
}
