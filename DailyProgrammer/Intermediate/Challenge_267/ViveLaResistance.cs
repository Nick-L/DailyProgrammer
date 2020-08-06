using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

/*
 * Link to problem on reddit
 * https://www.reddit.com/r/dailyprogrammer/comments/4jx7y8/20160518_challenge_267_intermediate_vive_la/
 */
namespace DailyProgrammer.Intermediate.Challenge_267
{
    public class ViveLaResistance
    {
        public double[,] circuit;
        public static double GetResistance(string input)
        {
            double[,] circuit = ConvertInputToAdjacencyMatrix(input);
            bool[] visited = new bool[circuit.GetLength(0)];
            double resistance = 0;
            DFS(0, circuit, visited, ref resistance);
            return resistance;
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
                double resistance = Convert.ToDouble(resistor[2]);

                if(adjacencyMatrix[nodes[node1], nodes[node2]] == 0)
                {
                    //non directed graph so node1 to node2 is same as node2 to node1
                    adjacencyMatrix[nodes[node1], nodes[node2]] = resistance;
                    adjacencyMatrix[nodes[node2], nodes[node1]] = resistance;
                }
                else
                {
                    resistance = GetParallelResistance(adjacencyMatrix[nodes[node1], nodes[node2]], resistance);
                    adjacencyMatrix[nodes[node1], nodes[node2]] = resistance;
                    adjacencyMatrix[nodes[node2], nodes[node1]] = resistance;
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

        private static void DFS(int start, double[,] circuit, bool[] visited, ref double resistance)
        {
            visited[start] = true;
            

            for(int i = 0; i < circuit.GetLength(0); i++)
            {
                if(circuit[start, i] != 0 && !visited[i])
                {
                    resistance += circuit[start, i];
                    DFS(i, circuit, visited, ref resistance);
                }
            }
        }

    }
}
