#pragma once
#include "My_Queue.h"
#include"My_List.h"
class Queue_List :
	public My_Queue
{
public:
	Queue_List();
	virtual bool IsEmpty();
	virtual TInf GetHead();
	virtual TInf DeQueue();
	virtual void EnQueue(TInf new_el);
	virtual void Delete();
	virtual ~Queue_List();
private:
	My_List l;
	node* last;

};

