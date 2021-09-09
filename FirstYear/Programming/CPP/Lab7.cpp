#include "stdafx.h"
#include "iostream"
#include "iomanip"
#include "iostream"
using namespace std;
int _tmain()
{
	double A = 0;
	double B = 1;
	double C = 0;
	const int k = 7;
	double a[k];
	for (int i = 0; i < k; i++)
	{
		cin >> a[i];
	}
	double max = a[0], min = a[0];
	for (int i = 0; i < k; ++i)
	{
		if (a[i] < 0)
			A += fabs(a[i]);
		else
			B *= a[i];
	}
	for (int i = 0; i < k; ++i)
	{
		if (a[i] < min) min = a[i];
		if (a[i] > max) max = a[i];
	}
	C = max - min;
	cout << A << endl;
	cout << B << endl;
	cout << C << endl;
	cout << endl;
	double  K = 0;
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
	system("pause");
	return 0;
}