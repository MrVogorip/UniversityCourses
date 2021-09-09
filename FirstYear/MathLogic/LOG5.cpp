#include "stdafx.h"
#include "iostream" 
using namespace std;
bool equ(bool x, bool y)
{
	return (!x && !y) || (x&&y);
}
int _tmain()
{
	cout << "truth table for initial SDNF and minimized SDNF" << endl;
	cout << " |a " << "|" << " |b " << "|" << " |c " << "|" << " |d " << "|" << " |res1|" << "res2|" << "equ|" << endl;
	for (int a = 0; a < 2; ++a)
		for (int b = 0; b < 2; ++b)
			for (int c = 0; c < 2; ++c)
				for (int d = 0; d < 2; ++d)
				{
					bool res1 = (a&&!b&&!c&&!d)|| (!a&&b&&!c&&!d) || (!a&&!b&&c&&!d) || (!a&&!b&&!c&&d) || (a&&b&&!c&&!d) || (!a&&b&&c&&!d) || (a&&!b&&!c&&d) || (a&&!b&&c&&d) || (!a&&b&&c&&d) || (a&&b&&c&&d) ;
					bool res2 = (a&&!b&&!c) || (b&&!c&&!d) || (!a&&c&&!d) || (!b&&!c&&d) || (!a&&b&&c)  || (a&&c&&d);
					cout << " |" << a << " |" << " |" << b << " |" << " |" << c << " |" << " |" << d << " |" << " |";
					if (res1)
						cout << res1 << "   |";
					else cout << res1 << "   |";
					if (res2)
						cout << res2 << "   |";
					else cout << res2 << "   |";
					cout << equ(res1, res2) << "  |";
					cout << endl;
				}
	system("pause");
	return 0;
}