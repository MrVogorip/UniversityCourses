#include "stdafx.h"
#include "iostream"
#include "iomanip"
#include "fstream"
using namespace std;
bool in(const int n_max, int &n, int &C, int &D, double A[]) 
{
	ifstream in("arr.txt");
	if (!in)
	{
		return false;
	}
	in >> n >> C >> D;
	if (!in || n>n_max)
	{
		return false;
	}
	cout << "Source array" << endl;
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
void oper(double A[], int n, int C, int D, double &S)
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
	out << "Product =" << S;
}
int main()
{
	const int n_max = 20;
	double arr[n_max]; 
	double p;
	int  C,D;
	int  n;
	if (in(n_max, n, C, D, arr))
	{
		oper(arr, n, C, D, p);
		cout << "Product = " << p << endl;
		out(p);
	}
	system("pause");
	return 0;
}