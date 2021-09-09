#pragma once
#include "table.h"
const int MAX_NUM = 1010000;
class table_arr :
	public table
{
	TInf a[MAX_NUM];
	int n;          
public:
	table_arr(void);
	virtual int size (void);
	virtual TInf Search(TKey k);
	virtual void Insert(TInf el);
	virtual void DelEl(TKey k);
	virtual ~table_arr(void);
};

