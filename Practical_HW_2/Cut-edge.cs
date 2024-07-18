using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var inputs = Console.ReadLine().Split();
        int n = int.Parse(inputs[0]);
        int m = int.Parse(inputs[1]);
        List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

        for (int i = 0; i < m; i++)
        {
            inputs = Console.ReadLine().Split();
            edges.Add(new Tuple<int, int>(int.Parse(inputs[0]), int.Parse(inputs[1])));
        }

        Console.WriteLine(FindCuttingEdges(n, m, edges));
    }

    static int FindCuttingEdges(int n, int m, List<Tuple<int, int>> edges)
    {
        List<int>[] graph = new List<int>[n + 1];
        Dictionary<Tuple<int, int>, int> edgeCount = new Dictionary<Tuple<int, int>, int>();

        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = new List<int>();
        }

        foreach (var edge in edges)
        {
            int u = edge.Item1;
            int v = edge.Item2;
            if (u != v)
            {
                graph[u].Add(v);
                graph[v].Add(u);
                var key1 = Tuple.Create(u, v);
                var key2 = Tuple.Create(v, u);

                if (edgeCount.ContainsKey(key1))
                {
                    edgeCount[key1]++;
                    edgeCount[key2]++;
                }
                else
                {
                    edgeCount[key1] = 1;
                    edgeCount[key2] = 1;
                }
            }
        }

        bool[] visited = new bool[n + 1];
        int[] tin = new int[n + 1];
        int[] low = new int[n + 1];
        List<Tuple<int, int>> cuttingEdges = new List<Tuple<int, int>>();
        int timer = 0;

        for (int i = 1; i <= n; i++)
        {
            if (!visited[i])
            {
                DFS(i, -1, visited, tin, low, ref timer, cuttingEdges, graph, edgeCount);
            }
        }

        return cuttingEdges.Count;
    }

    static void DFS(int u, int parent, bool[] visited, int[] tin, int[] low, ref int timer, List<Tuple<int, int>> cuttingEdges, List<int>[] graph, Dictionary<Tuple<int, int>, int> edgeCount)
    {
        visited[u] = true;
        tin[u] = low[u] = timer++;
        foreach (int v in graph[u])
        {
            if (v == parent) continue;
            if (visited[v])
            {
                low[u] = Math.Min(low[u], tin[v]);
            }
            else
            {
                DFS(v, u, visited, tin, low, ref timer, cuttingEdges, graph, edgeCount);
                low[u] = Math.Min(low[u], low[v]);
                if (low[v] > tin[u])
                {
                    var edge = Tuple.Create(u, v);
                    if (edgeCount[edge] == 1)
                    {
                        cuttingEdges.Add(edge);
                    }
                }
            }
        }
    }
}
