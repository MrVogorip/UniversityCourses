#pragma once
#include "table.h"
#include "Tree.h"
class table_tree :
	public table
{
	Tree t;
	int n;
public:
	table_tree();
	virtual int size(void);
	virtual TInf Search(TKey k);
	virtual void Insert(TInf el);
	virtual void DelEl(TKey k);
	virtual ~table_tree();
};

