#include "stdafx.h" 
#include "iostream" 
using namespace std;
int _tmain(int argc, _TCHAR* argv[])
{

	cout<<" |a "<<"|"<<" |b "<<"|"<<" |d "<< "|"<<" |c "<<"|"<<"  res|"<<"res2|"<<"res3|"<<endl;
	for (int a = 0; a < 2; ++a)
	for (int d = 0; d < 2; ++d)
	for (int b = 0; b < 2; ++b)
	for (int c = 0; c < 2; ++c)
	{
		bool v1 = (a&&!d)||(((!c&&!b)||d)&&(c||b))||((!d||!c)&&(c||d));
		bool v2 = (a||c||d);
		cout<<" |"<<a<<" |"<<" |"<<b<<" |"<<" |"<<d<<" |"<<" |"<<c<<" |  ";
		if (v1)
			cout << " " <<v1<< " | ";
		else cout << " " <<v1<< " | ";
		if (v2)
			cout << " "<<v2<<" | ";
		else cout << " " <<v2<< " | ";
		if (v1==v2)
			cout << " " << true << " | ";
		else cout << " " << false << " | ";
		cout << endl;

	}
	system("pause");
	return 0;
}