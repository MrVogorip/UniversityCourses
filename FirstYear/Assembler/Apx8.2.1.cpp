#include "stdafx.h"
#include <windows.h>
#include "fstream"
#include "iostream"
#include "iomanip"
using namespace std;
int main()
{
	const int num = 5;
	int a[num] = { 5,-4,3,-2,1 };
	int compl = 0;
	int sum = 0;
	for (int i = 0; i < num; i++)
	{
		cout << setw(5) << a[i];
	}
	cout << endl;
	__asm {
		mov ecx, 4;
		lea edi, a;
		mov eax, 0;
		mov ebx, 0;
		jecxz _end;
	_mloop:
		cmp[edi], 0;
		jg _comp;
		add edi, 4;
		loop _mloop;
		jecxz _end;
	_comp:
		cmp eax, 0;
		je _first;
		imul[edi];
		add ebx, [edi];
		add edi, 4;
		loop _mloop;
		jecxz _end;
	_first:
		mov eax, [edi];
		mov ebx, [edi];
		add edi, 4;
		jmp _mloop;
	_end:
		mov compl, eax;
		mov sum, ebx;
	};
	cout << compl << endl;
	cout << sum << endl;
	system("pause");
	return 0;
}
