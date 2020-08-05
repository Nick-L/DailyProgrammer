using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DailyProgrammer.Intermediate.Challenge_267
{
    public class ViveLaResistance
    {
        public double[,] circuit;
        public static double GetResistance(string input)
        {
            double[,] circuit = ConvertInputToAdjacencyMatrix(input);
            return 0;
        }

        public static double[,] ConvertInputToAdjacencyMatrix(string inputFile)
        {
            StreamReader file = new StreamReader(inputFile);
            string line = file.ReadLine();

            Dictionary<string, int> nodes = GetNodeList(line);

            double[,] adjacencyMatrix = new double[nodes.Count, nodes.Count];

            while((line = file.ReadLine()) != null)
            {
                string[] resistor = line.Split(' ');
                string node1 = resistor[0];
                string node2 = resistor[1];
                string resistance = resistor[2];

                if(adjacencyMatrix[nodes[node1], nodes[node2]] == 0)
                {
                    adjacencyMatrix[nodes[node1], nodes[node2]] = Convert.ToDouble(resistance);
                }
                else
                {
                    adjacencyMatrix[nodes[node1], nodes[node2]] = GetParallelResistance(adjacencyMatrix[nodes[node1], nodes[node2]], Convert.ToDouble(resistance));
                }
            }

            
            return adjacencyMatrix;
        }

        private static Dictionary<string, int> GetNodeList(string input)
        {
            Dictionary<string, int> nodes = new Dictionary<string, int>();

            string[] nodeTitles = input.Split(' ');

            for(int i = 0; i < nodeTitles.Length; i++)
            {
                nodes.Add(nodeTitles[i], i);
            }

            return nodes;
        }

        private static double GetParallelResistance(double resistance1, double resistance2)
        {
            return 1 / ((1 / resistance1) + (1 / resistance2));
        }

    }
}
