#include "stdafx.h"
#include <iostream>
#include <cmath>
#define _USE_MATH_DEFINES
#include <cmath>
using namespace std;
int _tmain()
{
	double y, a , x , beta;
	cout << "a=";
	cin >> a;
	cout << "x=";
	cin >> x;
	cout << "beta=";
	cin >> beta;
	y = ((pow(exp(x), 3)*tan(x + beta)) - log10(x*x*x - a) + pow(a + x*x*x, 1.0 / 5.0)) /
		(x*x*x*x + 	sqrt(x+1) - sin(beta)*sin(beta) + 9000 + 0.9*a*x);
	cout << " a=" << a << "   x=" << x << "   beta=" << beta << endl;
	cout << "Y = " << y << endl;
	bool A , B , C , Z;
	double X , Y ;
	cout << "A=";
	cin >>A;
	cout << "B=";
	cin >>B;
    cout << "C=";
	cin >> C;
	cout << "X=";
	cin >> X;
	cout << "Y=";
	cin >> Y;
	Z = !A&&B || !(1.8 <= X || Y >= 0.5) || C; 
	cout << " A=" << boolalpha << A << "   B=" << boolalpha << B << "   C=" << boolalpha << C << "   X=" << X << "   Y=" << Y << endl;
	cout << "Z = " << boolalpha << Z << endl;
	system("pause");
	return 0;
}