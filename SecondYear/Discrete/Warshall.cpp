#include "pch.h"
#include <iostream>
using namespace std;
void fakeMain();
void warshall();
const int N = 7;
int main() {
	warshall();
	fakeMain();
}
void warshall() {
	bool graph[N][N] = {
		{0,0,0,1,0,0,0},
		{1,0,0,0,0,0,0},
		{0,0,0,0,0,0,1},
		{1,0,1,0,0,0,0},
		{0,0,0,0,0,1,0},
		{0,0,0,0,1,0,1},
		{0,0,0,0,0,1,0} };

	for (int i = 0; i < N; ++i) {
		for (int j = 0; j < N; ++j) {
			cout << graph[i][j] << " ";
		}
		cout << "\n";
	}
	for (int k = 0; k < N; k++) {
		for (int i = 0; i < N; ++i) {
			for (int j = 0; j < N; ++j) {
				graph[i][j] = graph[i][j] || graph[i][k] && graph[k][j];
			}
		}
	}
	for (int i = 0; i < N; ++i) {
		for (int j = 0; j < N; ++j) {
			cout << graph[i][j] << " ";
		}
		cout << "\n";
	}
}
bool placementRE(int *a, int n, int m) {
	int j = m - 1;
	while (j >= 0 && a[j] == n) j--;
	if (j < 0) return false;
	if (a[j] >= n)
		j--;
	a[j]++;
	if (j == m - 1) return true;
	for (int k = j + 1; k < m; k++)
		a[k] = 1;
	return true;
}
bool placement(int *a, int n, int m) {
	int j = n - 1;
	while (j > m - 1) {
		while (j != -1 && a[j] >= a[j + 1]) j--;
		if (j == -1)
			return false;
		int k = n - 1;
		while (a[j] >= a[k]) k--;
		swap(a[j], a[k]);
		int l = j + 1, r = n - 1;
		while (l < r)
			swap(a[l++], a[r--]);
	}
	return true;
}
bool combinationsRE(int *a, int n, int m) {
	int j = m - 1;
	while (a[j] == n && j >= 0) j--;
	if (j < 0) return false;
	if (a[j] >= n)
		j--;
	a[j]++;
	if (j == m - 1) return true;
	for (int k = j + 1; k < m; k++)
		a[k] = a[j];
	return true;
}
bool combinations(int *a, int n, int m) {
	int k = m;
	int i = k - 1;
	while (i >= 0) {
		if (a[i] < n - k + i + 1) {
			++a[i];
			for (int j = i + 1; j < k; ++j)
				a[j] = a[j - 1] + 1;
			return true;
		}
		--i;
	}
	return false;
}
bool next_permutation(int *a, int  n) {
	int j = n - 2;
	while (j != -1 && a[j] >= a[j + 1]) j--;
	if (j == -1) return false;
	int k = n - 1;
	while (a[j] >= a[k]) k--;
	swap(a[j], a[k]);
	int l = j + 1;
	int r = n - 1;
	while (l < r) swap(a[l++], a[r--]);
}
void initializationArray(int *A) {
	for (int i = 0; i < N; i++) {
		A[i] = i + 1;
	}
}
void printArray(int *A, int length) {
	for (int i = 0; i < length; ++i) {
		cout << A[i] << " ";
	}
	cout << "\n";
}
void fakeMain() {
	int Array[N] = { 1,2,3,4 };
	while (next_permutation(Array, N)) {
		printArray(Array, N);
	}
	initializationArray(Array);
	while (combinations(Array, N, 3)) {
		printArray(Array, N - 1);
	}
	initializationArray(Array);
	while (combinationsRE(Array, N, 3)) {
		printArray(Array, N - 1);
	}
	initializationArray(Array);
	while (placement(Array, N, 3)) {
		printArray(Array, N - 1);
	}
	initializationArray(Array);
	while (placementRE(Array, N, 3)) {
		printArray(Array, N - 1);
	}
	warshall();
}