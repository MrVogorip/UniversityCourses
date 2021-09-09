#include "stdafx.h"
#include "iostream"
#include "iomanip"
#include "iostream"
#include <fstream>
using namespace std;
int _tmain()
{
	cout << endl;
	ifstream in1, in2;
	int size1, size2;
	in1.open("array1.txt");
	in2.open("array2.txt");
	if (!in1 || !in2)
	{
		system("pause");
		return 0;
	}
	in1 >> size1;
	in2 >> size2;
	if (size1 <= 0 || size2 <= 0)
	{
		system("pause");
		return 0;
	}
	double *arr1 = new double[size1];
	double *arr2 = new double[size2];
	cout << "Array M1 =";
	int nom1 = 0;
	int nom2 = 0;
	for (int i = 0; i < size1; ++i)
	{
		in1 >> arr1[i];
		if (!in1)
		{
			system("pause");	return 0;
		}
		cout << setw(4) << arr1[i];
		++nom1;
	}
	cout << endl;
	cout << "Array M2 =";
	for (int j = 0; j < size2; ++j)
	{
		in2 >> arr2[j];
		if (!in2)
		{
			system("pause");	return 0;
		}
		cout << setw(4) << arr2[j];
		++nom2;
	}
	cout << endl;
	in1.close();
	in2.close();
	double *arr3 = new double[size2 + size1];
	int c = 0;
	int k = 0;
	int count=1;
	arr3[0] = arr1[0];
	for (int i = 1; i < size1; i++)
	{
		for (int j = 0; j < count; j++)
		{
			if (arr1[i] == arr3[j])
				break;
			else
				k++;
		}
		if (k == count)
		{
			arr3[count] = arr1[i];
			count++;
		}
		k = 0;
	}
	for ( int i = 0; i < size2; i++)
	{
		for (int j = 0; j < size1; j++)
		{
			if (arr2[i] != arr1[j])
				k++;
			else
				break;
		}
		if (k == size1)
			arr3[count++] = arr2[i];
		k = 0;
	}
	cout << "Array M3 =";
	for (int i = 0; i < count; i++)
	{
		cout << setw(4) << arr3[i];
	}
		cout << endl;
	ofstream out("res.txt");
	if (!out)
	{
		cout << "The result was not written to the file";
	}
	for (int i = 0; i < count; i++)
	{
    	out << arr3[i]<< " ";
	}
	out.close();
	delete[] arr1;
	delete[] arr2;
	delete[] arr3;
	system("pause");
	return 0;
}