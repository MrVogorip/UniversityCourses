#include "stdafx.h"
#include <string>
#include <iostream>
#include <iomanip>
#include <cmath>
#include <cstdio>
using namespace std;
void main()
{
	string pred;
	int by = 0;
	int sl = 0;
	int ra = 0;
	int n = 0;
	getline(cin, pred);
	ra = pred.size();
	int pre = ra;
	int *slova = new int[ra];
	int *bykvi = new int[ra];
	for (int i = 0; i < ra; i++)
	{
		bykvi[i] = 0;
		slova[i] = 0;
		if (pred.at(i) != ' ')
		{
			by++;
		}
		if (pred.at(i) == ' ')
		{
			bykvi[i] = by;
			by = 0;
			sl++;
			slova[i] = sl;
		}
	}
	for (int i = 0; i < ra; i++)
	{
		if (bykvi[i] != 0 && bykvi[i] <pre)
		{
			pre = bykvi[i];
			n = i;
		}
	}
	cout << slova[n] << endl;
	cout << bykvi[n]<<endl;
	delete[] slova;
	delete[] bykvi;
	system("pause");
}