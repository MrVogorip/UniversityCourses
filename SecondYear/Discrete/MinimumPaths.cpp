#include "pch.h"
#include <iostream>
#include <fstream>
#include <ctime>
using namespace std;
bool next_permutation(int *a, int  n);
void FindingPath(int *&resultWay, int *way, int **matrixWay, int length, int &minimum);
void PrintMiminumWay(int *x, int *y, int *resultWay, int length, int minimum);
void DataInput(int &length, int *&x, int *&y);
int main() {
	srand(time(0));
	int n;
	int MinimumPath = 30000;
	int *x; 
	int	*y;
	DataInput(n, x, y);
	int *Way = new int[n];
	for (int i = 0; i < n; i++)
		Way[i] = i;
	int **MatrixWay = new int*[n];
	int *ResultMinimumWay = new int[n];
	for (int i = 0; i < n; i++) {
		MatrixWay[i] = new int[n];
		for (int j = 0; j < n; j++)
			MatrixWay[i][j] = sqrt((x[j] - x[i])*(x[j] - x[i]) + (y[j] - y[i])*(y[j] - y[i]));
	}
	FindingPath(ResultMinimumWay, Way, MatrixWay, n, MinimumPath);
	while (next_permutation(Way, n))
		FindingPath(ResultMinimumWay, Way, MatrixWay, n, MinimumPath);
	PrintMiminumWay(x, y, ResultMinimumWay, n, MinimumPath); 
	cout << clock() / 1000.0 << endl;
	system("pause");
	return 0;
}
void DataInput(int &length, int *&x, int *&y) {
	ifstream file_in("inp.txt");
	file_in >> length;
	x = new int[length];
	y = new int[length];
	for (int i = 0; i < length; i++) {
		file_in >> x[i];
		file_in >> y[i]; 
	}
	file_in.close();
}
void PrintMiminumWay(int *x, int *y, int *resultWay, int length, int minimum) {
	ofstream file_out("outp.txt");
	cout << minimum << endl;
	file_out << minimum << endl;
	for (int i = 0; i < length + 1; ++i) {
		cout << resultWay[i] + 1 << " ";
		file_out << resultWay[i] + 1 << " ";
	}
	cout << endl;
	file_out << endl;
	for (int i = 0; i < length; ++i) {
		cout << x[resultWay[i]] << " ";
		file_out << x[resultWay[i]] << " ";
	}
	cout << endl;
	file_out << endl;
	for (int i = 0; i < length; ++i) {
		cout << y[resultWay[i]] << " ";
		file_out << y[resultWay[i]] << " ";
	}
	cout << endl;
	file_out << endl;
	file_out.close();
}
void FindingPath(int *&resultWay, int *way, int **matrixWay, int length, int &minimum) {
	int Cost = 0;
	for (int i = 0; i < length - 1; ++i)
		Cost += matrixWay[way[i]][way[i + 1]];
	Cost += matrixWay[way[length - 1]][way[0]];
	for (int i = 0; i < length; ++i)
		cout << way[i] << " ";
	cout << way[0] << "   " ;
	cout << Cost << endl;
	if (minimum > Cost) {
		for (int i = 0; i < length; ++i)
			resultWay[i] = way[i];
		resultWay[length] = way[0];
		minimum = Cost;
	}
}
bool next_permutation(int *a, int  n) {
	int j = n - 2;
	while (j != -1 && a[j] >= a[j + 1])
		j--;
	if (j == -1)
		return false;
	int k = n - 1;
	while (a[j] >= a[k])
		k--;
	swap(a[j], a[k]);
	int l = j + 1;
	int r = n - 1;
	while (l < r)
		swap(a[l++], a[r--]);
	return true;
}