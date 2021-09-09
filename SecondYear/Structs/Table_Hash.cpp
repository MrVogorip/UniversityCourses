#include "stdafx.h"
#include "table_hash.h"
int table_hash::hash(TKey k) {
	return k % dim;
}
table_hash::table_hash()
{
	t = NULL;
	n = 0;
	dim = 0;
}
table_hash::table_hash(int num) {
	t = new My_List[num];
	dim = num;
	n = 0;
}
int table_hash::size(void)
{
	return n;
}
TInf table_hash::Search(TKey k)
{
	node*temp = t[hash(k)].first();
	while (temp != NULL && k != temp->item.key())
		temp = temp->next;
	if (temp == NULL) throw table::table_exception("not found");
	else return temp->item;
}
void table_hash::Insert(TInf el)
{
	t[hash(el.key())].InsertInBegin(el);
	++n;
}
void table_hash::DelEl(TKey k)
{
	node*temp = t[hash(k)].first();
	node*prev = NULL;
	while (temp != NULL && k != temp->item.key()) {
		prev = temp;
		temp = temp->next;
	}
	if (temp == NULL) throw table::table_exception("not found");
	else {
		if (prev == NULL)t[hash(k)].DeleteFirst();
		else t[hash(k)].DeleteAfter(prev);
	}
	--n;
}
table_hash::~table_hash()
{
	for (int i = 0; i < dim; i++)
			t[i].Delete();
	delete[] t;
}
