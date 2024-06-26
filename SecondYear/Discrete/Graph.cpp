#include "pch.h"
#include <iostream>
#include <fstream>
using namespace std;
void Input(int **&aa, int &n, int &m) {
	ifstream in("Input.txt");
	in >> n >> m;
	aa = new int *[n];
	for (int i = 0; i < n; i++) {
		aa[i] = new int[m];
		for (int j = 0; j < n; j++) {
			in >> aa[i][j];
		}
	}
	in.close();
}
void Output(int **bb, int n, int k) {
	ofstream out("Output.txt");
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < k; j++) {
			out << bb[i][j] << " ";
		}
		out << endl;
	}
	out.close();
}
void PrintArray(int **aa, int n, int m) {
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < m; j++) {
			cout << aa[i][j] << " ";
		}
		cout << endl;
	}
}
void Operation_Smezh_to_Intsid(int **aa, int **&bb, int n, int m, int &k) {
	int c = 0;
	k = 0;
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < m; j++) {
			if (aa[i][j] == 1) {
				k++;
				aa[j][i] = 0;
			}
		}
	}
	int *d = new int[2 * k];
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < m; j++) {
			if (aa[i][j] == 1) {
				d[c] = i;
				d[c + 1] = j;
				c += 2;
			}
		}
	}
	bb = new int *[n];
	for (int i = 0; i < n; i++) {
		bb[i] = new int[k];
		for (int j = 0; j < k; j++) {
			bb[i][j] = 0;
		}
	}
	for (int i = 0; i < 2 * k; i = i + 2) {
		int p = int(i / 2);
		bb[d[i]][p] = 1;
		bb[d[i + 1]][p] = 1;
	}
}
int main() {
	int **MatrixSmezhnosti, **MatrixIntsidentnosti;
	int SizeN, SizeM, SizeK;
	Input(MatrixSmezhnosti, SizeN, SizeM);
	PrintArray(MatrixSmezhnosti, SizeN, SizeM);
	Operation_Smezh_to_Intsid(MatrixSmezhnosti, MatrixIntsidentnosti, SizeN, SizeM, SizeK);
	Output(MatrixIntsidentnosti, SizeN, SizeK);
	PrintArray(MatrixIntsidentnosti, SizeN, SizeK);
	return 0;
}