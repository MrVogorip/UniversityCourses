#include "pch.h"
#include "Stack_list.h"

void Stack_list::move() {
	l.DeleteFirst();
}
ostream & operator <<(ostream & out, Stack_list & S) {
	out << S.l;
	return out;
}
ofstream & operator <<(ofstream & out, Stack_list & S) {
	out << S.l;
	return out;
}
ifstream & operator >>(ifstream & in, Stack_list & S) {
	in >> S.l;
	return in;
}
void Stack_list::Delete() {
	l.Delete();
}
TInt Stack_list::Pop() {
	TInt temp = Top();
	l.DeleteFirst();
	return temp;
}
TInt Stack_list::Top() {
	return l.first()->inf;
}
void Stack_list::Push(TInt new_el) {
	l.InsertInBegin(new_el);
}
bool Stack_list::IsEmpty() {
 	return l.IsEmpty();
}
Stack_list::Stack_list(){
}
Stack_list::~Stack_list(){
	Delete();
}
