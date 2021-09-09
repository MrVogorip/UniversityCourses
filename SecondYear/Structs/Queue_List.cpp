#include "stdafx.h"
#include "Queue_List.h"


Queue_List::Queue_List()
{
	last = NULL;
}
bool Queue_List::IsEmpty()
{
	return l.ISEmpty();
}
TInf Queue_List::GetHead()
{
	if (IsEmpty()) My_Queue::GetHead();
else
	return l.first()->item;

}
TInf Queue_List::DeQueue()
{
	if (IsEmpty()) My_Queue::GetHead();
	else
	{
		TInf res = l.first()->item;
		l.DeleteFirst();
		return res;
	}
}
void Queue_List::EnQueue(TInf new_el)
{
	if (IsEmpty())
		last = l.InsertInBegin(new_el);
	else
		last = l.InsertAfter(new_el, last);
	if (last == NULL) My_Queue::EnQueue(new_el);
}
void Queue_List::Delete()
{
	l.Delete();
	last = NULL;
}

Queue_List::~Queue_List()
{
	Delete();
}
