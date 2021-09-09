#include "StdAfx.h"
#include "Queue_Array.h"

Queue_Array::Queue_Array(void)
{
	num = head = 0;
	tail = max_num - 1;
}
	
bool Queue_Array::IsEmpty()
{
	return num == 0;
}

TInf Queue_Array::GetHead()
{
	if (IsEmpty()) My_Queue::GetHead();
	else
	{
		return a[head];
	}
}

TInf Queue_Array::DeQueue()
{
	if (IsEmpty()) My_Queue::DeQueue();
	else
	{
		TInf res = a[head];
		--num;
		if (num==0)
		{
			head = 0;
			tail = max_num - 1;
		}
		else if (++head == max_num) head = 0;
		return res;
	}
}
void Queue_Array::EnQueue(TInf new_el)
{
	if (num == max_num) My_Queue::EnQueue(new_el);
	else
	{
		++num;
		if (++tail == max_num) tail = 0;
		a[tail] = new_el;
	}
}
void Queue_Array::Delete()
{
	num = head = 0;
	tail = max_num - 1;
}


Queue_Array::~Queue_Array(void)
{
}