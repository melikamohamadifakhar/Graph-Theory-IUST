using System;
using System.Collections.Generic;

class Program {
    static Tuple<int, int> BFS(int startNode, Dictionary<int, List<int>> adjList) {
        Dictionary<int, bool> visited = new Dictionary<int, bool>();
        Dictionary<int, int> distance = new Dictionary<int, int>();
        int maxDist = 0;
        int farthestNode = startNode;

        Queue<int> queue = new Queue<int>();
        queue.Enqueue(startNode);
        visited[startNode] = true;
        distance[startNode] = 0;

        while (queue.Count > 0) {
            int node = queue.Dequeue();
            int currentDistance = distance[node];

            foreach (var neighbor in adjList[node]) {
                if (!visited.ContainsKey(neighbor)) {
                    visited[neighbor] = true;
                    distance[neighbor] = currentDistance + 1;
                    queue.Enqueue(neighbor);
                    if (distance[neighbor] > maxDist) {
                        maxDist = distance[neighbor];
                        farthestNode = neighbor;
                    }
                }
            }
        }

        return new Tuple<int, int>(farthestNode, maxDist);
    }

    static int FindTreeDiameter(int n, List<Tuple<int, int>> edges) {
        Dictionary<int, List<int>> adjList = new Dictionary<int, List<int>>();
        foreach (var edge in edges) {
            if (!adjList.ContainsKey(edge.Item1)) adjList[edge.Item1] = new List<int>();
            if (!adjList.ContainsKey(edge.Item2)) adjList[edge.Item2] = new List<int>();

            adjList[edge.Item1].Add(edge.Item2);
            adjList[edge.Item2].Add(edge.Item1);
        }

        var farthestFromAny = BFS(1, adjList);
        var diameter = BFS(farthestFromAny.Item1, adjList);

        return diameter.Item2;
    }

    static void Main() {
        int n = int.Parse(Console.ReadLine());
        List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

        for (int i = 0; i < n - 1; i++) {
            string[] parts = Console.ReadLine().Split();
            int u = int.Parse(parts[0]);
            int v = int.Parse(parts[1]);
            edges.Add(new Tuple<int, int>(u, v));
        }

        int diameter = FindTreeDiameter(n, edges);
        Console.WriteLine(diameter);
    }
}
