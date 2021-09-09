#include "stdafx.h"
#include "iostream"
#include "iomanip"
#include "fstream"
#include "conio.h"
using namespace std;
const int razmer_max = 10;				
bool in(int A[][razmer_max], int& razmer, int& I, int& K)
{
	ifstream in("in.txt");
	if (!in)
	{
		return false;
	}
	in >> razmer >> I >> K;
	if (!in || razmer<0 || razmer>razmer_max || I <= 0 || K <= 0 || I>razmer || K>razmer)
	{
		return false;
	}
	for (int i = 0; i < razmer; ++i)
	{
		for (int j = 0; j < razmer; ++j)
		{
			in >> A[i][j];
			if (!in)
			{
				return false;
			}
			cout << setw(3) << A[i][j];
		}
		cout << endl;
	}
	return true;
}
void oper(int A[][razmer_max], int razmer, int I, int K)
{
	I--; K--;
	for (int i = 0; i < razmer; i++)
	{
		int R = A[I][i];
		A[I][i] = A[K][i];
		A[K][i] = R;
	}
}
void out(int A[][razmer_max], int razmer)
{
	ofstream out("out.txt");
	for (int i = 0; i < razmer; i++)
	{
		for (int j = 0; j < razmer; j++)
		{
			out << setw(3) << A[i][j];
			cout << setw(3) << A[i][j];
		}
		cout << endl;
		out << endl;
	}
}
int main()
{
	int E[razmer_max][razmer_max];
	int n;
	int I, K;
	if (in( E, n, I, K))
	{
		oper(E, n, I, K);
		out(E, n);
	}
	system("pause");
	return 0;
}