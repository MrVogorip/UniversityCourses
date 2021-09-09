#pragma once
#include "pch.h"
#include "List.h"
#include "iostream"
#include <iomanip>
#include <string>
#include <fstream>
using namespace std;
typedef double TInt;
struct TElem {
	TInt inf;
	TElem *next;
};
class List {
private:
	TElem *n;
public:
	void Delete();
	bool Contains(TInt item);
	TInt & operator [](int i);
	friend ostream & operator <<(ostream & out, List & l);
	friend ofstream & operator <<(ofstream & out, List & l);
	friend ifstream & operator >>(ifstream & in, List & l);
	void DeleteAfter(TElem *p);
	void DeleteFirst();
	TElem *InsertAfter(TElem *p, TInt new_el);
	TElem *InsertInBegin(TInt new_el);
	TElem *first();
	bool IsEmpty();
	List();
	~List();
};

