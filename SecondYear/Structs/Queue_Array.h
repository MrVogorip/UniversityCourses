#pragma once
#include "my_queue.h"

typedef unsigned int t_count;
const t_count max_num=100;

class Queue_Array :
	public My_Queue
{
public:
	Queue_Array(void);
	virtual bool IsEmpty();
	virtual TInf GetHead();
	virtual TInf DeQueue();
	virtual void EnQueue(TInf new_el);
	virtual void Delete();
	virtual ~Queue_Array(void);
private:
	TInf a[max_num];
	t_count num;
	t_count head;
	t_count tail;
};


