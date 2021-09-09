#include "stdafx.h"
#include <iostream>	
#include "targetver.h"
#include <stdio.h>                
#include <tchar.h>              
#include <math.h> 
#include <cmath>                  
using namespace std;
int _tmain()
{
	const int n = 7;
	double s = 0;
	double a;
	double x = 0.7;
	for (int i = 0; i <= n; ++i)
	{
		double factor = 1;
		for (int j = 1; j <= i; ++j)
			factor *= j;
		a = (pow(log(3), i) / factor)*pow(x, i);
		s += a;
	}
    cout << "s=" << s << endl;
	cout << endl;
	const int n1 = 7; 
	double x1 = 0.7;
	double a_temp = 1;
	double s1 = a_temp;
	double a_next;
	for (int i = 0	; i < n1; ++i)
	{
		double q = ((log(3)*x1) / (i + 1));
        a_next = a_temp*q;
		s1 += a_next;
		a_temp = a_next;
	}
    cout << "s=" << s << endl;
	cout << endl;
	double x_begin = 0.1;
	double x_end = 1;
	double step = 0.2;
	for (double x = x_begin; x <= x_end; x += step)
	{
		double eps = 1e-4;
		double a_temp = 1;
		double s = a_temp;
		double a_next;
		int i = 0;
		while (a_temp >= eps)
		{
			double q = ((log(3)*x) / (i + 1));
			a_next = a_temp*q;
			s += a_next;
			a_temp = a_next;
			++i;
		}
		cout << "x=" << x << "   s=" << s << "    i=" << i << endl;
	}
	system("pause");
	return 0;	
}