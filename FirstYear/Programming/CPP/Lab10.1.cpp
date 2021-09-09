#include "stdafx.h"
#include "iostream"
#include "iomanip"
#include "iostream"
#include <fstream>
using namespace std;
int _tmain()
{
	ifstream in;
	const int max_size=2;
	in.open("array.txt");
	if (!in)
	{
		system("pause");
		return 0;
	}
	in >> max_size;
	if (size1 <= 0 || size2 <= 0)
	{
		system("pause");
		return 0;
	}
	cout << "Array A ="<<endl;
	int **arr = new int*[size1];
	for (int i = 0; i < size1; ++i)
	{
		arr[i] = new int[size2];
		for (int j = 0; j < size2; ++j)
		{
	    in >> arr[i][j];
		   if (!in)
		   {
			system("pause"); return 0;
		   }
		cout << setw(3) << arr[i][j];
		}
	    cout << endl;
	}
	cout << "Array with elements of the lower main diagonal B = " << endl;
	int lin = size1*size1;
	int razmer = ((lin)-size1) / 2;
	int *niz_diagonali = new int[razmer];
	int x = 0;
	for (int i = 0; i < size1; ++i)
	{
		for (int j = 0; j < size2; ++j)
		{
			if (i == j)
			{
				break;
			}
			else
			{
				niz_diagonali[x++] = arr[i][j];
			}
		}
	}
    for (int i = 0; i < razmer; ++i)
	{
		cout <<setw(3)<< niz_diagonali[i];
	}
	cout << endl;
	ofstream out("res.txt");
	for (int i = 0; i < razmer; i++)
	{
		out << niz_diagonali[i] << " ";
	}
	out.close();
	for (int i = 0; i < size1; i++)
	{
		delete[] arr[i];
	}
	delete[] arr;
	delete[] niz_diagonali;
	system("pause");
	return 0;
}