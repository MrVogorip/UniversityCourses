#pragma once
#include "pch.h"
#include "List.h"
#include "iostream"
#include <iomanip>
#include <fstream>
#include <string>
class Stack_list
{
private:
	List l;
public:
	void move();
	friend ostream & operator <<(ostream & out, Stack_list & S);
	friend ofstream & operator <<(ofstream & out, Stack_list & S);
	friend ifstream & operator >>(ifstream & in, Stack_list & S);
	void Delete();
	TInt Pop();
	TInt Top();
	void Push(TInt new_el);
	bool IsEmpty();
	Stack_list();
	~Stack_list();
};

