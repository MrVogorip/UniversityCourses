#include "stdafx.h"
#include "iostream"
#include "iomanip"
#include "iostream"
#include <fstream>
using namespace std;
int _tmain()
{
	ifstream inp;
	const int max_n = 10000;
	const int max_k = 10000;
	int a[max_n][max_k];
	int n = 0;
	int k = 0;
	inp.open("array.txt");
	if (!inp)
	{
		cout << "error";
	}
	else
	{
		inp >> n && inp >> k;
		if (n > max_n || k>max_k)
		{
			n = max_n; k = max_k;
			cout << "error";
			system("pause");
			return 0;
		}
		else
		{
			for (int i = 0; i < n; ++i)
			{
				
				for (int j = 0; j < k; ++j)
				{
					inp >> a[i][j];
					cout << setw(8) << a[i][j];
				if (!inp)
				{
					cout << "error"; system("pause"); return 0;
				}
				}
				cout << endl;
			}
		}
	}
	inp.close();
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < k; j++)
		{
	
			if (a[i][j] >= 0)
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
		for (int j = 0; j < k; j++)
		{

			cout << setw(8) << a[i][j];
			
		}
		cout << endl;
	}
	ofstream out("res.txt");
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < n; j++)
		{
			out << a[i][j]<<" ";
		}
		out << endl;
	}
	out.close();
	system("pause");
	return 0;
}

