#include "stdafx.h"
using namespace std;
int _tmain()
{
	float x, y, z;
	cin >> x >> y >> z;
	cout << endl;
	cout << setw(12) << "x=" << scientific << x << setw(15) << "x=" << setprecision(3) << fixed << x << setw(15) << "x=" << defaultfloat << x << endl;
	cout << setw(12) << "y=" << scientific << y << setw(15) << "y=" << setprecision(3) << fixed << y << setw(15) << "y=" << defaultfloat << y << endl;
	cout << setw(12) << "z=" << scientific << z << setw(15) << "z=" << setprecision(3) << fixed << z << setw(15) << "z=" << defaultfloat << z << endl;
	char q;
	cin >> q;
	cout << endl;
	cout << setw(8) << "q=" << right << setw(6) << q << setw(15) << "q=" << q << endl;
	bool log_true = 1;
	cin >> log_true;
	cout << setw(12) << "boolean=" << boolalpha << log_true << setw(15) << "boolean=" << noboolalpha << right << setw(9) << log_true << setw(20) << "boolean=" << noboolalpha << log_true << endl;
	system("pause");
}