#include "stdafx.h"
#include <string>
#include <iostream>
#include <iomanip>
using namespace std;
bool xor(bool x, bool y)
{
	return (x && !y) || (!x&&y);
}
bool proverka(string &bool_str)
{
	bool rez;
	cout << "Enter binary number:";
	getline(cin, bool_str); 
	if (bool_str.empty() || bool_str.find_first_not_of("01") != string::npos) 
	{
		rez = 0;
		bool_str.clear();
		cout << "Error" << endl;
		system("pause");
		system("cls");
	}
	else rez = 1;
	cout << endl;
	return rez;
}
void main()
{
	string pervoe_chislo, vtoroe_chislo, sum;
	bool bit1, bit2, bit_res = 0;
	do
	{
		do
		{
			system("cls");
			cout << "First number" << endl;
		} while (!proverka(pervoe_chislo));
		cout << "Second number" << endl;
	} while (!proverka(vtoroe_chislo));
	int razmer1 = pervoe_chislo.size();
	int razmer2 = vtoroe_chislo.size();
	if (razmer1 > razmer2)
	{
		vtoroe_chislo.insert(0, razmer1 - razmer2, '0');
	}
	if (razmer1 < razmer2)
	{
		pervoe_chislo.insert(0, razmer2 - razmer1, '0');
	}							
	for (int i = pervoe_chislo.size() - 1; i >= 0; i--)
	{
		if (pervoe_chislo.at(i) == '0') 
		{
			bit1 = 0;
		}
		else bit1 = 1;
		if (vtoroe_chislo.at(i) == '0')
		{
			bit2 = 0;
		}
		else bit2 = 1;
		sum.insert(0, to_string(xor (xor (bit1, bit2), bit_res)));
		bit_res = bit1 && bit2 || bit1 &&  bit_res || bit2 &&  bit_res;
	}
	if (bit_res)
	{
		sum.insert(0, to_string(bit_res));
	}
	cout << "First number in binary =" << pervoe_chislo << " in decimal =" << stoll(pervoe_chislo, NULL, 2) << endl;
	cout << "Second number in binary =" << vtoroe_chislo << " in decimal =" << stoll(vtoroe_chislo, NULL, 2) << endl;
	cout << "The sum is equal in binary =" << sum << " in decimal =" << stoll(sum, NULL, 2) << endl;
	system("pause");
}