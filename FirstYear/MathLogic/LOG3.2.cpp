#include "stdafx.h"
#include <fstream>
#include <string>
#include <iomanip>
#include <iostream>
using namespace std;
int main()
{
	unsigned short int kol_vo, max_kol_vo;
	bool sdnf, sknf;
	ifstream file_in;
	file_in.open("data.txt");
	file_in >> kol_vo;
	int var = pow(2, kol_vo);
	bool *result = new bool(var);
	bool *x = new bool(kol_vo);
	for (int i = 0; i < var; i++)
	{
		file_in >> result[i];
	}
	file_in.close();
	cout << setfill(' ') << setw(2 * kol_vo) << " " << "|F|SDNF|SKNF|EQU" << endl;
	for (max_kol_vo = 0; max_kol_vo < var; max_kol_vo++)
	{
		for (int i = kol_vo - 1; i >= 0; i--)
		{
			x[kol_vo - 1 - i] = (max_kol_vo >> i) & 1;
			cout << x[kol_vo - 1 - i] << " ";
		}
		sdnf = (!x[0] && !x[1]) || (x[0] && !x[1]);
		sknf = (x[0] || !x[1]) && (!x[0] || !x[1]);
		cout << "|" << result[max_kol_vo] << "| " << sdnf << "  | " << sknf << "  | " << ((result[max_kol_vo] == sdnf) && (result[max_kol_vo] == sknf)) << endl;
	}
	system("pause");
	return 0;
}