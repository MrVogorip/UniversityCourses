#include "stdafx.h"
#include "iostream"
#include "iomanip"
#include "fstream"
using namespace std;
void del_arr(double*&arr)
{
	delete[] arr;
}
bool in( int &n, int &C, int &D, double *&A)
{
	ifstream in("arr.txt");
	if (!in)
	{
		return false;
	}
	in >> n >> C >> D;
	if (!in)
	{
		return false;
	}
	A = new double[n];
	for (int i = 0; i < n; ++i)
	{
		in >> A[i];
		if (!in)
		{
			return false;
		}
		cout << setw(5) << A[i];
	}
	cout << endl;
	return true;
}
void oper(double *&A, int n, int C, int D, double &S)
{
	S = 1;
	for (int i = 0; i < n; ++i)
	{
		if (D>A[i] && C <= A[i])
		{
			S *= A[i];
		}
	}
}
void out(double S)
{
	ofstream out("res.txt");
	out << "Result: " << S;
}
int main()
{
	double *arr;
	double p;
	int  C, D;
	int  n;
	if (in(n, C, D, arr))
	{
		oper(arr, n, C, D, p);
		cout << "Result = " << p << endl;
		out(p);
		del_arr(arr);
	}
	system("pause");
	return 0;
}