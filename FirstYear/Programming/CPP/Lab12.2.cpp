#include "stdafx.h"
#include "iostream"
#include "iomanip"
#include "fstream"
#include "conio.h"
using namespace std;
void del_arr(int**&arr,int n)
{
	for (int i = 0; i < n; i++) 
	{
		delete[] arr[i];
	}
	delete[] arr;
}
bool in(int **&A, int& razmer, int& I, int& K)
{
	ifstream in("in.txt");
	if (!in)
	{
		return false;
	}
	in >> razmer >> I >> K;
	if (!in || razmer<0 || I <= 0 || K <= 0 || I>razmer || K>razmer)
	{
		return false;
	}
	A = new int*[razmer];
	for (int i = 0; i < razmer; ++i)
	{
		A[i] = new int[razmer];
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
void oper(int **&A, int razmer, int I, int K)
{
	I--; K--;
	for (int i = 0; i < razmer; i++)
	{
		int R = A[I][i];
		A[I][i] = A[K][i];
		A[K][i] = R;
	}
}
void out(int **&A, int n)
{
	ofstream out("out.txt");
	for (int i = 0; i < 4; i++)
	{
		for (int j = 0; j < 4; j++)
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
	int **E;
	int n;
	int I, K;
	if (in(E, n, I, K))
	{
		oper(E, n, I, K);
		out(E, n);
		del_arr(E, n);
	}
	system("pause");
	return 0;
}