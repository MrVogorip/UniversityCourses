#include "stdafx.h"
#include "iostream"
#include "iomanip"
#include "iostream"
#include <fstream>
using namespace std;
int _tmain()
{
	cout << endl;
	double A = 0;
	double B = 1;
	double C = 0;
	ifstream inp;
	const int max_n = 9;
	int a[max_n];
	int n = 0;
	inp.open("array.txt");
	if (!inp)
	{
		cout << "error";
	}
	else
	{
		inp >> n;
		if (n > max_n)
		{
			n = max_n;
			cout << "error";
			system("pause");
			return 0;
		}
		else
		{
			for (int i = 0; i < n; ++i)
			{
				inp >> a[i];
				if (!inp)
				{
					cout << "error"; system("pause"); return 0;
				}
				cout << setw(4) << a[i];
			}
		}
	}
	cout << endl;
	inp.close();
	int max = a[0], min = a[0];
	for (int i = 0; i <n; ++i)
	{
		if (a[i] <= 0)
			A += abs(a[i]);
		else
			B *= a[i];
	}
	for (int i = 0; i <n; ++i)
	{
		if (a[i] < min) min = a[i];
		if (a[i] > max) max = a[i];
	}
	C = max - min;
	cout << endl;
	cout << A << endl;
	cout << B << endl;
	cout << C << endl;
	cout << endl;
	int  K = 0;
	if (A > B)
	{
		if (A > C) K = A;
		else
			K = C;
	}
	else
	{
		if (B > C) K = B;
		else
			K = C;
	}
	cout << K << endl;
	ofstream out("res.txt");
	out << K;
	out.close();
	system("pause");
	return 0;
}