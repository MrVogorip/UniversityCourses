#include "stdafx.h"
#include <windows.h>
#include "fstream"
#include "iostream"
using namespace std;
int main()
{
	const int num = 10;
	int a[num];
	int result = 0;
	for (int i = 0; i < num; i++)
	{
		a[i] = rand() % 10 - rand() % 10;
		cout << a[i] << "  ";
	}
	cout << endl;
	__asm {
		mov ecx, 10;
		lea edi, a;
		mov eax, result;
		jecxz _end;
	_mloop:
		cmp[edi], 0;
		jl _minus;
		add edi, 4;
		loop _mloop;
		jecxz _end;
	_minus:
		add eax, 1;
		add edi, 4;
		loop _mloop;
	_end:
		mov result, eax;
	};
	cout << result << endl;
	system("pause");
	return 0;
}