#include "StdAfx.h"
#include "table_arr.h"

table_arr::table_arr()
{
	n=0;
}
int table_arr::size ()
{
	return n;
}

TInf table_arr::Search(TKey k)
{
	int l=0,m,r=n-1;
	bool found = false;
	while (!found && l<=r)
	{
		m = (l+r)/2;
		if (a[m].key()==k) found = true; 
		else if (a[m].key()<k) l=m+1;
		     else r=m-1;
	}
	if (found) return a[m];	
	else throw table_exception("No element!");
}
void table_arr::Insert(TInf el)
{
	if (n==MAX_NUM) throw table_exception("Table is overflow!"); 
	if (IsEmpty())a[n++]=el;
	else
	{
		TKey k=el.key();
			int l=0,m,r=n-1;
		bool found = false;
		while (!found && l<=r)
		{
			m = (l+r)/2;
			if (a[m].key()==k) found = true; 
			else if (a[m].key()<k) l=m+1;
				 else r=m-1;
		}
		int point=m+1;
		if (a[m].key()>k) point = m;
		++n;
		for(int i=n-2;i>=point;--i)
				a[i+1]=a[i];
		a[point]=el;
	}
}
void table_arr::DelEl(TKey k)
{
	int l=0,m,r=n-1;
	bool found = false;
	while (!found && l<=r)
	{
		m = (l+r)/2;
		if (a[m].key()==k) found = true; 
		else if (a[m].key()<k) l=m+1;
		     else r=m-1;
	}
	if (found) 
	{
		for(int i=m;i<n-1;++i)
			a[i]=a[i+1];
		--n;
	}
	else throw table_exception("No element!");
}
table_arr::~table_arr(void)
{
}
