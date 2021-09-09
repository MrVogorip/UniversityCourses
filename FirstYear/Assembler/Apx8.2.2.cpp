#include "stdafx.h"
#include <iostream> 
#include <iomanip>
using namespace std;
int main()
{
	const int n = 3;
	int m = (n + 1) * 2;
	short  res, A[n][n] = { { -3, 36, -5, },
	{ -9, 3, 56 },
	{ -7, -5, 21 } };
	for (int i = 0; i < n; ++i)
	{
		for (int j = 0; j < n; ++j)
		{
			cout << setw(4) << A[i][j];
		}
		cout << endl;
	}
	__asm
	{
		xor di, di;
		lea esi, A;
		mov ecx, n;
	cicl1:
		mov eax, ecx;
		mov ecx, n;
	cicl2:
		mov dx, [esi];
		cmp dx, 0;
		jl men;
		jmp nvt;
	men:

		add di, dx;
	nvt:
		add esi, 2;
		loop cicl2;

		mov ecx, eax;
		loop cicl1;

		mov res, di;

		lea esi, A;
		mov ecx, n;
	cicl3:
		mov dx, [esi];
		add dx, di;
		mov[esi], dx;
		add esi, m;
		loop cicl3;
	}
	for (int i = 0; i < n; ++i)
	{
		for (int j = 0; j < n; ++j)
		{
			cout << setw(4) << A[i][j];
		}
		cout << endl;
	}
	cout << res << endl;  
	system("pause");
	return 0;
}