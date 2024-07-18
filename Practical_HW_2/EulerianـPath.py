def find_root(parents, vertex):
    if parents[vertex] == vertex:
        return vertex
    else:
        parents[vertex] = find_root(parents, parents[vertex])  # Path compression
        return parents[vertex]

def union(parents, ranks, vertex1, vertex2):
    root1 = find_root(parents, vertex1)
    root2 = find_root(parents, vertex2)
    if root1 != root2:
        if ranks[root1] > ranks[root2]:
            parents[root2] = root1
        elif ranks[root1] < ranks[root2]:
            parents[root1] = root2
        else:
            parents[root2] = root1
            ranks[root1] += 1

def check_eulerian_trail(n, m, edges):
    if n == 0:
        return "NO"  # No vertices means no Eulerian trail
    degree = [0] * (n + 1)
    parents = [i for i in range(n + 1)]
    ranks = [0] * (n + 1)
    for u, v in edges:
        degree[u] += 1
        degree[v] += 1
        union(parents, ranks, u, v)
    
    # Check if all vertices with at least one edge are connected
    connected_component = None
    for i in range(1, n + 1):
        if degree[i] > 0:
            if connected_component is None:
                connected_component = find_root(parents, i)
            elif find_root(parents, i) != connected_component:
                return "NO"

    # Check the number of vertices with odd degree
    odd_count = sum(1 for i in range(1, n + 1) if degree[i] % 2 == 1)

    if odd_count == 0 or odd_count == 2:
        return "YES"
    else:
        return "NO"

if __name__ == "__main__":
    # Reading input from the console
    n, m = map(int, input().split())  # Read n and m
    edges = [tuple(map(int, input().split())) for _ in range(m)]  # Read edges

    # Output the result
    print(check_eulerian_trail(n, m, edges))
