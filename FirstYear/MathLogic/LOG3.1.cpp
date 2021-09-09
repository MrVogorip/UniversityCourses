#include "stdafx.h"
#include "iostream"
#include "iomanip"
#include "fstream"
#include "string"
using namespace std;
int main()
{
	unsigned long int kol_vo;
	unsigned long int max_kol_vo;
	cout << "Construction of SDNF and SCNF according to the Truth Table" << endl;
	again:
	cout << "Enter the number of function variables (min-2, max-" << sizeof(max_kol_vo)* 2 << ")" << endl;
	cin >> kol_vo;
	if (kol_vo < 2 || kol_vo>sizeof(max_kol_vo)* 2)
	{
		cout << "The number of variables exceeds the allowed limit, please try again" << endl;
		goto again;
	}
	max_kol_vo = pow(2,kol_vo);
	bool *mass = new bool[max_kol_vo];
	string SDNF;
	string SKNf;
	bool argument;
	bool tautology = 1;
	bool contrad = 0;
	cout << "Enter function value for different argument values" << endl;
	bool users_bool;
	for (int g = 0; g < max_kol_vo; g++)
	{
		for (int i = kol_vo - 1; i >= 0; i--)
		{
			argument = (g >> i) & 1;
			cout << setw(3) << argument;
		}
		cout << " == "; 
		cin >> mass[g];
		tautology = tautology && mass[g];
		contrad = contrad || mass[g];
	}
	for (int g = 0; g < max_kol_vo; g++)
	{
		if (mass[g] == 1) SDNF += '(';
		if (mass[g] == 0) SKNf += '(';
		for (int i = kol_vo - 1; i >= 0; i--)
		{
			char letter = '0' + kol_vo - i - 1;
			argument = (g >> i) & 1;
			if (mass[g] == 1)
			{
				if (argument) SDNF = SDNF + "x[" + letter + "]" + "&&";
				else SDNF = SDNF + "!" + "x[" + letter + "]" + "&&";
			}
			if (mass[g] == 0)
			{
				if (argument) SKNf = SKNf + "!" + "x[" + letter + "]" + "||";
				else SKNf = SKNf +  "x[" + letter + "]" + "||";
			}
		}
		if (mass[g] == 1) 
		{ 
			SDNF.pop_back(); SDNF.pop_back(); SDNF = SDNF + ")||";
		} 
		if (mass[g] == 0)
		{ 
			SKNf.pop_back(); SKNf.pop_back(); SKNf = SKNf + ")&&";
		} 
	}
	
	if (SDNF.size() > 0 && SDNF.back() == '|') 
	{ 
		SDNF.pop_back(); SDNF.pop_back();
	}	
	if (SKNf.size() > 0 && SKNf.back() == '&') 
	{
		SKNf.pop_back(); SKNf.pop_back();
	}
	ofstream file_out;
	file_out.open("result.txt");
	file_out << kol_vo;
	file_out << endl;
	for (int i = 0; i < max_kol_vo; i++)
	file_out << mass[i]<<'\t';
	file_out << endl;
	
	if (contrad == 0)
	{
		cout << "The contradiction has no SDNF" << endl;
		file_out << "The contradiction has no SDNF" << endl;
	}
	else
	{
		file_out << "SDNF = " <<'\n'<< SDNF << ";" << endl;
		cout << "SDNF = " << SDNF << ";" << endl;
	}
	if (tautology == 1)
	{
		cout << "Tautology does not have SKNF" << endl;
		file_out << "Tautology does not have SKNF" << endl;
	}
	else
	{
		file_out << "SKNF = " << '\n' << SKNf << ";" << endl;
		cout << "SKNF = " << SKNf << ";" << endl;
	}
	file_out.close();
	delete[]mass;
	system("pause");
	return 0;
}