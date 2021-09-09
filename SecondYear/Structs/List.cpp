#include "pch.h"
#include "List.h"
#include "iostream"
#include <iomanip>
#include <fstream>
#include <string>
using namespace std;
void List::Delete() {
	while (n != NULL)
	{
		DeleteFirst();
	}
}
bool List::Contains(TInt i) {
	TElem *q = n;
	while (q != NULL)
	{
		if (i == q->inf) {
			return true;
		}
		q = q->next;
	}
	return false;
}
TInt & List::operator [](int i) {
	int count = 0;
	TElem *q = n;
	while (count != i)
	{
		q = q->next;
	}
	return q->inf;
}
ostream & operator <<(ostream & out, List & l) {
	TElem *q = l.n;
	while (q != NULL)
	{
		out << q->inf << " ";
		q = q->next;
	}
	return out;
}
ofstream & operator <<(ofstream & out, List & l) {
	TElem *q = l.n;
	while (q != NULL)
	{
		out << q->inf << " ";
		q = q->next;
	}
	return out;
}
ifstream & operator >>(ifstream & in, List & l) {
	TInt temp;
	while (!in.eof())
	{
		in >> temp;
	}
	return in;
}
void List::DeleteAfter(TElem *p) {
	TElem *q = p->next;
	p->next = p->next->next;
	delete q;
}
void List::DeleteFirst() {
	TElem *q = n;
	n = n->next;
	delete q;
}
TElem *List::InsertAfter(TElem *p, TInt new_el) {
	TElem *q = new TElem;
	q->inf = new_el;
	q->next = p;
	p= q;
	return q;
}
TElem *List::InsertInBegin(TInt new_el) {
	TElem *q = new TElem;
	q->inf = new_el;
	q->next = n;
	n = q;
	return n;
}
TElem *List::first() {
	return n;
}
bool List::IsEmpty() {
	return n == NULL;
}
List::List() {
	n = NULL;
}
List::~List() {
	Delete();
}