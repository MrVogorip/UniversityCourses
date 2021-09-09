#include "stdafx.h"
#include "iostream"		
#include <string>		
#include <iomanip>		
#include <stdlib.h>		
using namespace std;
bool xor(bool, bool);
bool check(string&);
bool check_int(string&);
string add(string&, string&);
string multip(const string&, const string&);
string shift_to_left(string, const int&);
string dec_to_bin(const char&);
void alignment(string&, string&);
void deleting_zeros(string&);
int main()
{
	string str_menu;
	int menu;
	string first;
	string second;
	string result;
	do
	{
		do
		{
			system("cls");
			cout << "1) Addition of binary numbers" << endl;
			cout << "2) Multiplying binary numbers" << endl;
			cout << "3) Converting a number from decimal to binary number system" << endl;
			cout << "4) Exit the program" << endl;
			cout << endl << "Enter a number:";
			getline(cin, str_menu);
			if (str_menu.size() != 1)
			{
				cout << endl << "Error! Invalid input" << endl;
				system("pause");
			}
		} while (str_menu.size() != 1);
		menu = atoi(str_menu.c_str());
		switch (menu)
		{
		case 1:
		{
			do
			{
				cout << endl << "First number:" << endl;
			} while (!check(first));
			do
			{
				cout << endl << "Second number:" << endl;
			} while (!check(second));
			result = add(first, second);
			deleting_zeros(first);
			if (first == "") first = "0";
			deleting_zeros(second);
			if (second == "") second = "0";
			deleting_zeros(result);
			if (result == "") result = "0";
			if (result.size() >= sizeof(long long)*8)
			{
				cout << endl << "The result is too large a number cannot be verified" << endl;
			}
			else
			{
				cout << "First term" << setw(result.size()) << first << setw(20) << stoll(first, 0, 2) << endl;
				cout << "Second term" << setw(result.size()) << second << setw(20) << stoll(second, 0, 2) << endl;
				cout << "Sum" << setw(result.size() + 14) << result << setw(20) << stoll(result, 0, 2) << endl;
			}
			first.clear();first.shrink_to_fit();
			second.clear();second.shrink_to_fit();
			result.clear();result.shrink_to_fit();
			system("pause");
			break;
		}
		case 2:
		{
			do
			{
				cout << endl << "First number:" << endl;
			} while (!check(first));
			do
			{
				cout << endl << "Second number:" << endl;
			} while (!check(second));
			if (first.length() > second.size())
			{
				result = multip(first, second);
			}
			else
			{
				result = multip(second, first);
			}
			deleting_zeros(first);
			if (first == "") first = "0";
			deleting_zeros(second);
			if (second == "") second = "0";
			deleting_zeros(result);
			if (result == "") result = "0";
			if (result.size() >= sizeof(long long) * 8)
			{
				cout << endl << "The result is too large a number cannot be verified" << endl;
			}
			else
			{
				if (result.size() == 1)
				{
					cout << "First multiplier   " << setw(first.size()) << first << setw(20) << stoll(first, 0, 2) << endl;
					cout << "Second multiplier   " << setw(first.size()) << second << setw(20) << stoll(second, 0, 2) << endl;
					cout << "Result" << setw(first.size() + 7) << result << setw(20) << stoll(result, 0, 2) << endl;
				}
				else
				{
					cout << "First multiplier   " << setw(result.size()) << first << setw(20) << stoll(first, 0, 2) << endl;
					cout << "Second multiplier   " << setw(result.size()) << second << setw(20) << stoll(second, 0, 2) << endl;
					cout << "Result" << setw(result.size() + 7) << result << setw(20) << stoll(result, 0, 2) << endl;
				}
			}
			first.clear();first.shrink_to_fit();
			second.clear();second.shrink_to_fit();
			result.clear();result.shrink_to_fit();
			system("pause");
			break;
		}
		case 3:
		{
			while (!check_int(first));
			result = dec_to_bin(first.at(0));
			for (int i_10 = 1; i_10 < first.size(); i_10++)
			{
				result = add(multip(result, "1010"), dec_to_bin(first.at(i_10)));
			}
			cout << endl << "Entered number in binary number system:" << setw(result.size()+26) << result << endl;
			if (result.size() >= sizeof(long long) * 8)
			{
				cout << endl << "The result is too large a number cannot be verified" << endl;
			}
			else
			{
				cout << "Examination:"  << stoll(result, 0, 2) << endl;
			}
			first.clear();first.shrink_to_fit();
			result.clear();result.shrink_to_fit();
			system("pause");
			break;
		}
		case 4: 
		{
			return 0;
		}
		default:
		{
			cout << endl << "Error!" << endl << endl;
			system("pause");
			break;
		}
		}
	} while (menu != 4);
}
bool xor(bool x, bool y)
{
	return (x && !y) || (!x && y);
}
void alignment(string& first, string& second)
{
	int size1 = first.size();
	int size2 = second.size();
	if (size1 > size2) second.insert(0, size1 - size2, '0');
	if (size1 < size2) first.insert(0, size2 - size1, '0');
}
void deleting_zeros(string& str)
{
	int quan = 0;
	for (int i = 0; i < str.size(); i++)
	{
		if (str.at(i) == '0') quan++;
		else break;
	}
	str.erase(0, quan);
}
string add(string& first, string& second)
{
	alignment(first, second);
	bool bit_1, bit_2, carry_bit = 0;
	string sum;
	for (int i = first.size() - 1; i >= 0; i--)
	{
		if (first.at(i) == '0') bit_1 = 0;
		else bit_1 = 1;
		if (second.at(i) == '0') bit_2 = 0;
		else bit_2 = 1;
		sum.insert(0, to_string(xor (xor (bit_1, bit_2), carry_bit)));
		carry_bit = bit_1 && bit_2 || bit_1 && carry_bit || bit_2 && carry_bit;
	}
	if (carry_bit) sum.insert(0, to_string(carry_bit));
	return sum;
}
string multip(const string& first, const string& second)
{
	string str_mult = "";
	for (int i = second.size() - 1; i >= 0; i--)
	{
		if (second.at(i) == '1')
		{
			str_mult = add(str_mult, shift_to_left(first, second.size() - i - 1));
		}
	}
	return str_mult;
}
string shift_to_left(string str, const int& pos)
{
	for (int i = 0; i < pos; i++)
	{
		str = add(str, str);
	}
	return str;
}
string dec_to_bin(const char& num)
{
	string bit("0");
	string one("1");
	for (int i = 1; i <= int(num - '0'); i++)
	{
		bit = add(bit, one);
	}
	return bit;
}
bool check(string& str)
{
	bool bin;
	cout << "Enter a binary number (Use only 1 and 0, no spaces):";
	getline(cin, str);
	if (str.empty() || str.find_first_not_of("01") != string::npos)
	{
		bin = 0;
		str.clear();
		cout << "Error!" << endl;
		system("pause");
	}
	else
	{
		bin = 1;
	}
	cout << endl;
	return bin;
}
bool check_int(string& str)
{
	bool bin;
	cout << "Enter a decimal number (Use only numbers (0-9), no spaces):";
	getline(cin, str);
	if (str.empty() || str.find_first_not_of("0123456789") != string::npos)
	{
		bin = 0;
		str.clear();
		cout << "Error!" << endl;
		system("pause");
	}
	else
	{
		bin = 1;
	}
	return bin;
}