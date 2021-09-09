#include "stdafx.h"
#include "table_tree.h"


table_tree::table_tree()
{
	n = 0;
}

int table_tree::size(void)
{
	return n;
}
TInf table_tree::Search(TKey k)
{
	nodeTree *q = t.SearchItem(k);
	if (q == NULL) throw table::table_exception("Not found");
	else {
		TInf result = q->info;
		return result.key();
	}
}
void table_tree::Insert(TInf el)
{
	t.InsertItem(el.key());
	++n;
}
void table_tree::DelEl(TKey k)
{
	t.DeleteItem(k);
	--n;
}

table_tree::~table_tree()
{
}