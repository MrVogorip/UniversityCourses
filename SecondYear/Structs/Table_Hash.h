#pragma once
#include "table.h"
#include "List.h"
class table_hash :
	public table
{
	My_List *t;
	int n;
	int dim;
	int hash(TKey k);
public:
	table_hash();
	table_hash(int num);
	virtual int size(void);
	virtual TInf Search(TKey k);
	virtual void Insert(TInf el);
	virtual void DelEl(TKey k);
	virtual ~table_hash();
};

