#pragma once
#include "table.h"
#include "List.h"
class table_list:
	public table
{
	My_List l;
	int n;
public:
	table_list();
	virtual int size(void);
	virtual TInf Search(TKey k);
	virtual void Insert(TInf el);
	virtual void DelEl(TKey k);
	virtual ~table_list();
};

