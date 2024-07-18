using System;
using System.Collections.Generic;

class Program {
    static int FindTreeCenterByLayering(int n, List<Tuple<int, int>> edges) {
        if (n == 1) {
            return 1;
        }

        Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        foreach (var edge in edges) {
            if (!graph.ContainsKey(edge.Item1)) graph[edge.Item1] = new List<int>();
            if (!graph.ContainsKey(edge.Item2)) graph[edge.Item2] = new List<int>();

            graph[edge.Item1].Add(edge.Item2);
            graph[edge.Item2].Add(edge.Item1);
        }

        int[] degree = new int[n + 1];
        foreach (var node in graph.Keys) {
            degree[node] = graph[node].Count;
        }

        Queue<int> leaves = new Queue<int>();
        for (int i = 1; i <= n; i++) {
            if (degree[i] == 1) {
                leaves.Enqueue(i);
            }
        }

        int remainingNodes = n;
        while (remainingNodes > 2) {
            int leafCount = leaves.Count;
            remainingNodes -= leafCount;

            for (int i = 0; i < leafCount; i++) {
                int leaf = leaves.Dequeue();
                foreach (var neighbor in graph[leaf]) {
                    degree[neighbor]--;
                    if (degree[neighbor] == 1) {
                        leaves.Enqueue(neighbor);
                    }
                }
            }
        }

        return Math.Max(leaves.Dequeue(), leaves.Count > 0 ? leaves.Dequeue() : 0);
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

        int center = FindTreeCenterByLayering(n, edges);
        Console.WriteLine(center);
    }
}
