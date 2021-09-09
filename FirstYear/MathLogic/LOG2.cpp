#include "stdafx.h"
#include "iostream" 
using namespace std;
bool dif(bool x, bool y)
{
	return x&&!y;
}
bool imp(bool x, bool y)
{
	return !x||y;
}
bool pir(bool x, bool y)
{
	return !x&&!y;
}
bool equ(bool x, bool y)
{
	return (!x&&!y) || (x&&y);
}
bool xor(bool x, bool y)
{
	return (x&&!y) || (!x&&y);
}
int _tmain()
{
	cout << " |a " << "|" << " |b " << "|" << " |c " << "|" << " |d " << "|" << " |res1|" << "res2|" << "equ|" << endl;
	for (int a = 0; a < 2; ++a)
	for (int b = 0; b < 2; ++b)
	for (int c = 0; c < 2; ++c)
	for (int d = 0; d < 2; ++d)
	{
		bool res1 = dif(((pir(b, !d)) || (xor (!b, d))), (pir(dif(a, c), equ(a, c))));
		bool res2 = xor(xor(xor (xor (xor (xor (xor (xor (1,b),c),b&&d),b&&c),a&&c),b&&d&&c),b&&a&&c),b&&d&&a&&c);
		cout << " |" << a << " |" << " |" << b << " |" << " |" << c << " |" << " |" << d << " |" << " |";
		if (res1)
			cout << res1<< "   |";
		else cout << res1<<"   |";
		if (res2)
			cout << res2<<"   |";
		else cout << res2<<"   |";
	
			cout << equ(res1, res2) << "  |";
	
		cout << endl;
	}
	system("pause");
	return 0;
}