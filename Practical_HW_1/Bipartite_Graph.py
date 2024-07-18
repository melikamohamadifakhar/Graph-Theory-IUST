from collections import defaultdict
from collections import deque


class Graph:
    def __init__(self):
        self.graph = defaultdict(list)

    def add_edge(self, u, v):
        self.graph[u].append(v)
        self.graph[v].append(u)
        self.graph[u] = list(set(self.graph[u]))
        self.graph[v] = list(set(self.graph[v]))

    def add_node(self, u):
        self.graph[u] = []

    def dfs(self, u, visited, parent, odd_cycles):
        visited[u] = True

        for v in self.graph[u]:
            if not visited[v]:
                self.dfs(v, visited, u, odd_cycles)
            elif parent != v and v not in odd_cycles[u]:
                odd_cycles[u].append(v)

        return visited

    def detect_odd_cycles(self):
        visited = {node: False for node in self.graph}
        odd_cycles = defaultdict(list)

        for node in self.graph:
            if not visited[node]:
                visited = self.dfs(node, visited, None, odd_cycles)

        return odd_cycles

    def remove_edges_from_odd_cycles(self, odd_cycles):
        for u, cycle_nodes in odd_cycles.items():
            for v in cycle_nodes:
                if v in self.graph[u]:
                    self.graph[u].remove(v)
                if u in self.graph[v]:
                    self.graph[v].remove(u)

    def is_bipartite(self):
        colors = {}
        queue = deque()

        for node in self.graph:
            if node not in colors:
                colors[node] = 0
                queue.append(node)

                while queue:
                    current_node = queue.popleft()

                    for neighbor in self.graph[current_node]:
                        if neighbor not in colors:
                            colors[neighbor] = 1 - colors[current_node]
                            queue.append(neighbor)
                        elif colors[neighbor] == colors[current_node]:
                            return False

        return True

    def make_bipartite(self):
        odd_cycles = self.detect_odd_cycles()
        self.remove_edges_from_odd_cycles(odd_cycles)

    def count_edges(self):
        return sum(len(edges) for edges in self.graph.values()) // 2

    def node_count(self):
        return len(self.graph)

    def count_complement_graph_edges(self):
        count_complete_graph_edges = (self.node_count() * (self.node_count() - 1)) // 2
        count_graph_edges = self.count_edges()
        return int(count_complete_graph_edges - count_graph_edges)


def main():
    input_string = input()
    parts = input_string.split()

    graph = Graph()

    for part in parts:
        if len(part) % 2 != 0:
            for i in range(0, len(part) - 1, 2):
                graph.add_edge(part[i], part[i + 1])
            if not graph.graph.get(part[-1]):
                graph.add_node(part[-1])
        else:
            for i in range(0, len(part), 2):
                graph.add_edge(part[i], part[i + 1])

    if graph.is_bipartite():
        print(graph.count_complement_graph_edges())
    else:
        # print(graph.graph)
        graph.make_bipartite()
        # print(graph.graph)
        print(graph.count_edges())


if __name__ == "__main__":
    main()
