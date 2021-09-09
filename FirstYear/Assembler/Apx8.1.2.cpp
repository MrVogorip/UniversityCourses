#include "stdafx.h"
#include <windows.h>
#include <stdio.h> 
#include <conio.h>
#include "iostream"
using namespace std;
int main()
{
	const int n = 6;
	short res, A[n] = { 3276, 4, -42, -2, 3 ,6 };
	for (int i = 0; i < n; ++i)
	{
		cout << A[i] << endl;
	}
	__asm
	{
		mov dx, 32767;
		lea esi, A;
		mov ecx, n;
	loo:mov ax, [esi];
		inc esi;
		inc esi;
		cmp ax, 0;
		jg big;
		neg ax;
	big:cmp ax, dx;
		jl men;
		jmp lo;
	men: mov dx, ax;
	lo: loop loo;
		mov res, dx;

		lea edi, A;
		mov ecx, n;
	leen: mov ax, [edi];
		sub ax, dx;
		mov[edi], ax;
		inc edi;
		inc edi;
		loop leen;
	}
	cout << res << endl;
	for (int i = 0; i < n; ++i)
	{
		cout << A[i] << endl;
	}
	system("pause");
	return 0;
}