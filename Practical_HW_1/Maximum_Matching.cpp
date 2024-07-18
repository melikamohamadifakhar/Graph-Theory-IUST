#include <iostream>
#include <vector>
#include <map>
#include <string>
#include <sstream>

using namespace std;

map<char, vector<char>> graph;

bool bpm(char u, map<char, char>& matchR, map<char, bool>& seen) {
    for (char v : graph[u]) {
        if (!seen[v]) {
            seen[v] = true;
            if (matchR.find(v) == matchR.end() || bpm(matchR[v], matchR, seen)) {
                matchR[v] = u;
                return true;
            }
        }
    }
    return false;
}

int max_matching() {
    map<char, char> matchR;
    int result = 0;
    for (auto& p : graph) {
        map<char, bool> seen;
        if (bpm(p.first, matchR, seen)) {
            result++;
        }
    }
    return result / 2;
}

void buildGraphFromString(const string& input) {
    stringstream ss(input);
    string part;
    while (ss >> part) {
        size_t len = part.length();
        // Handling odd-length parts by adjusting the loop's range
        size_t end = (len % 2 == 0) ? len : len - 1;
        for (size_t i = 0; i < end; i += 2) {
            char u = part[i];
            char v = part[i + 1];
            graph[u].push_back(v);
            graph[v].push_back(u);
        }
    }
}

int main() {
    string input_string;
    getline(cin, input_string); // Read the entire line of input

    buildGraphFromString(input_string);

    cout << max_matching() << endl;
    return 0;
}
