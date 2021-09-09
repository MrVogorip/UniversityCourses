#include "stdafx.h" 
#include "iostream" 
#define _USE_MATH_DEFINES 
#include "cmath" 
#include "math.h" 
using namespace std;
int main()
{
	double x, a, Y;
	cout << "x="; cin >> x;
	cout << "a="; cin >> a;
    if (x <= -1) Y = a*cos(x+1)+a;
	else Y = a*pow(x+2,3.0/2);
	cout << "Y=" << Y << endl;
	double z1;
	int x1;
	cout << "x="; cin >> x1;
	if ( x1 < 0) cout << "z=0" << endl;
	else {
		switch (x1)
		{
		case 0: z1 = sqrt(pow(cos(x1), 2) + 1);
		break;
		case 1: z1 = (x1-2)/(x1*x1+2); 
		break;
		case 2: z1 = pow((x1 - 1)*(x1 - 1) + 3, 1.0 / 3);
	    break;
		default: z1 = 0;
		}
		cout << "z=" << z1 << endl;
	    }
	cout << endl;
	double Ox, Oy, n, m;
	cout << "Ox="; cin >> Ox;
	cout << "Oy="; cin >> Oy;
	n = (Ox*Ox) / 4 + (Oy*Oy) / 1;
	m = (Ox*Ox) / 1 + (Oy*Oy) / 4;
	if ( n <= 1 ||  m <= 1) cout << "error, doesn't work";
	else cout << "error, doesn't work";
	   cout << endl;
	double a2, x2, beta;
	cout << endl;
	cout << "a="; cin >> a2;
	cout << "x="; cin >> x2; 
	cout << "beta="; cin >> beta;
	if (sqrt(x2 + 1) < 0)
        cout << "error, doesn't work" << endl;	
	else
	if ((x2*x2*x2*x2 + sqrt(x2 + 1) - sin(beta)*sin(beta) + 9000 + 0.9*a2*x2)<0)
		cout << "error, doesn't work" << endl; 
	else 
	if (pow(a2 + x2*x2*x2, 1.0 / 5)<0)
		cout << "error, doesn't work" << endl;
	else
	if ((x2*x2*x2 - a2) <= 0)
		cout << "error, doesn't work" << endl;
	else cout << "y=" << ((pow(exp(x2), 3)*tan(x2 + beta)) - log10(x2*x2*x2 - a2) + pow(a2 + x2*x2*x2, 1.0 / 5)) /
		(x2*x2*x2*x2 + sqrt(x2 + 1) - sin(beta)*sin(beta) + 9000 + 0.9*a2*x2);
	system("pause");
	return 0;
}