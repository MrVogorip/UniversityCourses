#include "pch.h"
#include <fstream>
#include <iostream>
#include <iomanip>
using namespace std;
const int N = 50;
int n = 16;
double adj[N][N] = { { 0 } };
int path[N][N];
double dist[N][N];
void read_graph()
{
	ifstream in("input.txt");
	for (int i = 0; i < n; i++)
		for (int j = 0; j < n; j++)
			adj[i][j] = INFINITY;
	do
	{
		int indI, indJ;
		double zh;
		in >> indI >> indJ >> zh;
		adj[indI - 1][indJ - 1] = zh;
	} while (!in.eof());
	in.close();
}
void floid()
{
	for (int u = 0; u < n; ++u)
		for (int v = 0; v < n; ++v)
			if (adj[u][v] || u == v)
			{
				path[u][v] = v;
				dist[u][v] = adj[u][v];
			}
			else
			{
				path[u][v] = N;
				dist[u][v] = INFINITY;
			}
	for (int k = 0; k < n; ++k)
		for (int u = 0; u < n; ++u)
			if (dist[u][k] != INFINITY)
				for (int v = 0; v < n; ++v)
					if (dist[u][v] > dist[u][k] + dist[k][v])
					{
						dist[u][v] = dist[u][k] + dist[k][v];
						path[u][v] = path[u][k];
					}

}
void max(int &str, int &stl)
{
	double max = dist[0][0];
	for (int i = 0; i < n; i++)
		for (int j = 0; j < n; j++)
			if (dist[i][j] > max && dist[i][j] != INFINITY)
			{
				max = dist[i][j];
				str = i;
				stl = j;
			}
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < n; j++)
			cout << setw(10) << path[i][j];
		cout << endl;
	}
}
void show_path(int u, int v)
{
	cout << u << v << endl;
	int x = u;
	cout << x;
	while (x != v)
	{
		 x = path[x][v];
		 cout << " " << x;
	}
	cout << endl <<dist[u][v] << endl;
}
int main()
{
	int u, v;
	read_graph();
	floid();
	max(u, v);
	show_path(u, v);
	return 0;
}