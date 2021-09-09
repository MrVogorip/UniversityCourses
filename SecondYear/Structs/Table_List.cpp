#include "stdafx.h"
#include "table_list.h"


table_list::table_list()
{
	n = 0;
}
int table_list::size(void) {

	return n;
}
TInf table_list::Search(TKey k){
	node*temp = l.first();
	while (temp != NULL && k != temp->item.key())
		temp = temp->next;
	if (temp == NULL) throw table::table_exception("not found");
	else return temp->item;
}
void table_list::Insert(TInf el) {
	l.InsertInBegin(el);
	n++;
}
void table_list::DelEl(TKey k) {

	node*temp = l.first();
	node*prev = NULL;
	while (temp != NULL && k != temp->item.key()) {
		prev = temp;
		temp = temp->next;
	}
	if (temp == NULL) throw table::table_exception("not found");
	else {if(prev ==NULL) l.DeleteFirst();
	else l.DeleteAfter(prev);}
	--n;
}
table_list::~table_list()
{
	l.Delete();
}
