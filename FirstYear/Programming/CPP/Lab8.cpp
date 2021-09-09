#include "stdafx.h"
#include "iostream"
#include "iomanip"
#include "iostream"
using namespace std;
int _tmain()
{
	const int n = 2;
	const int m = 3;
	double a[n][m];
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < m; j++)
		{
			a[i][j] = rand() % 41 - 20;
			if (a[i][j]>=0)
			{
				a[i][j] = a[i][j] * a[i][j];
			}
			else
			{
				a[i][j] = a[i][j] * a[i][j] * a[i][j];
			}
		}
	}
	cout << endl;
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < m; j++)
		{
			
			cout << setw(8) << a[i][j];
		}
		cout << endl;
	}
	system("pause");
	return 0;
}

