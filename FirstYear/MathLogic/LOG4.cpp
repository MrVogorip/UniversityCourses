#include "stdafx.h" 
#include <iostream>
#include <string>
using namespace std;
bool dif(bool x, bool y)
{
	return x && !y;
}
bool imp(bool x, bool y)
{
	return !x || y;
}
bool pir(bool x, bool y)
{
	return !x && !y;
}
bool equ(bool x, bool y)
{
	return (!x && !y) || (x&&y);
}
bool xor(bool x, bool y)
{
	return (x && !y) || (!x&&y);
}
int main()
{
	cout << "Construction of the Zhegalkin polynomial using the Pascal triangle method" << endl;
	//((b↓¬d)∨(¬b+d))/((a/c)↓(a~c))
	int num=4;								
	int bin_var;
	int kol_vo = pow(2, num);
	bool* X = new bool[num];
	bool** triangle = new bool*[kol_vo];
	bool* result = new bool[kol_vo];
	string polynom;
	cout << "Truth table" << endl;
	cout << "a b c d |res" << endl;
	for (bin_var = 0; bin_var < kol_vo; bin_var++)
	{
		for (int i = num - 1; i >= 0; i--)
		{
			X[num - 1 - i] = (bin_var >> i) & 1;
			cout << X[num - 1 - i] << " ";
		}
		result[bin_var] = (dif(((pir(X[1], !X[3])) || (xor (!X[1], X[3]))), (pir(dif(X[0], X[2]), equ(X[0], X[2])))));
		cout << "|" << result[bin_var] << endl;
	}
	for (int i = 0; i < kol_vo; i++)
	{
		triangle[i] = new bool[kol_vo];
	}		
	for (int j = 0; j < kol_vo; j++)
	{
		triangle[0][j] = result[j];
	}		
	for (int i = 1; i < kol_vo; i++)
	{
		for (int j = 1; j < kol_vo - i + 1; j++)
		{
			triangle[i][j - 1] = xor (triangle[i - 1][j - 1], triangle[i - 1][j]);
		}
	}
	cout << "Pascal's triangle" << endl;
	for (int i = 0; i < kol_vo; i++)
	{
		for (int j = 0; j < kol_vo - i; j++)
		{ 
			cout << triangle[i][j] << " ";
		}
		cout << endl;
	}
	if (triangle[0][0] == 1)
	{
		polynom += "1 + ";
	} 
	for (bin_var = 1; bin_var < kol_vo; bin_var++)
	{
		for (int i = num - 1; i >= 0; i--)
		{
			X[num - 1 - i] = (bin_var >> i) & 1;
			if (triangle[bin_var][0] == 1 && X[num - 1 - i] == 1)
			{ 
				polynom = polynom + char(num-i+64);
			}
		}
		if (triangle[bin_var][0] == 1)
		{
			polynom += " + ";
		}
	}
	polynom.pop_back();
	polynom.pop_back();
	cout << "Zhegalkin polynomial" << endl;
	cout << polynom << endl;
	for (int i = 0; i < kol_vo; i++)
	{ 
		delete[] triangle[i];
	}
	delete[] triangle;
	delete[] X;
	delete[] result;
	system("pause");
	return 0;
}