#include "stdafx.h"
#include <iostream>
#include <string>
#include <fstream>
using namespace std;
bool  input(string &s)
{
	ifstream in("in.txt");
	if (!in)
	{
		return false;
	}
	getline(in, s);
	cout << s << endl;
	in.close();
	return true;
}
void output( int num)
{
	ofstream out("out.txt");
	out << num;
	out.close();
}
bool decision(string s, string &ns,  int &num)
{
	int k=0;
	for (int i = 0; i < s.size(); i++)
	{
		if ((s[i] == '+'||s[i] == '-'||isdigit(s[i]))&&isdigit(s[i + 1]))
		{
			k = i;
			ns += s[i];
			break;
		}
	}
	if (k == 0)
	{
		return false;
	}
	for (int i = k; i < s.size(); i++)
	{
		if (isdigit(s[i + 1]))
		{
			ns += s[i + 1];
		}
		if (!isdigit(s[i + 2]))
		{
			break;
		}
	}
	cout << ns << endl;
	if (abs(stod(ns)) < (pow(2, sizeof(num) * 8.0 - 1) - 1))
	{
		num = stod(ns);
		cout << num << endl;
		return true;
	}
	else
	{
		return false;
	}
}
int main()
{
	int number;
	string Str;
	string newStr;
	if (input(Str))
	{
		if (decision(Str, newStr, number))
		{
			output(number);
		}
	}
	system("pause");
	return 0;
}