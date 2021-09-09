#include "stdafx.h"
#include <iostream>
#include <fstream>
using namespace std;
const  int razmer = 200;
bool in(char arr[], int n)
{
	ifstream in("input.txt");
	if (!in)
	{
		return false;
		in.close();

	}
	in.getline(arr, n);
	if (!in)
	{
		return false;
		in.close();
	}
	return true;
	in.close();
}
bool oper(char arr[], int n, int  &real_nomer,int &symbol)
{
	symbol = razmer;
	real_nomer = 0;
	int current_symbol = 0, nomer = 1;
	for (int i = 0; i < strlen(arr); ++i)
	{
		if (arr[i] != ' ' && arr[i] != '.')
		{
			++current_symbol;
		}
		else
		{
			if (current_symbol <symbol)
			{
				symbol = current_symbol;
				real_nomer = nomer;
			}
			nomer++;
			current_symbol = 0;
		}
		if (arr[i] == '.')
		{
			cout << arr << endl;
			cout << "Minimum length word number:" << real_nomer << "   Number of characters in a word:" << symbol << endl;
			return true;
		}
		if (i == strlen(arr)-1 && arr[i] != '.')
		{
			return false;
		}
	}
}
void out(int  nomer,int  symbol)
{
	ofstream out("output.txt");
	out << "Minimum length word number:" << nomer << "   Number of characters in a word:" << symbol << endl;
	out.close();
}
int main()
{
	char text[razmer];
	int nomer_slova;
	int quantity_symbol;
	if (in(text, razmer))
	{
		if (oper(text, razmer, nomer_slova, quantity_symbol))
		{
			out(nomer_slova, quantity_symbol);
		}
	}
	return 0;
}