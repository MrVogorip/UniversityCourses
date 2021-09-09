#include "pch.h"
#include "Sets.h"
Sets Sets::operator + (Element el) {
	Sets s(*this);
	if (!s.ElementSearch(el)) {
		int mid, left = 0, right = s.real_num-1;
		while (true) {
			mid = (left + right )/ 2;
			if (el < s.a[mid]) {
				right = mid - 1;
			}
			else if(el > s.a[mid]) {
				left = mid + 1;
			}
			if(left - right == 1) {
				if(s.a[mid]<el && mid!=s.real_num) {
					mid++;
				}
				s.real_num = s.real_num + 1;
				int temp = s.a[mid];
				s.a[mid] = el;
				for (int i = real_num; i>mid; i--) {
					s.a[i + 1] = s.a[i];
				}
				s.a[mid + 1] = temp;
				break;
			}
		}
		return s;
	}
	else { 
		return s;
	}
}
Sets Sets::operator - (Element el) {
	Sets s(*this);
	int left = 0;
	int right = real_num;
	while (true)
	{
		int middle = (left + right) / 2;
		if (el < a[middle])
			right = middle - 1;
		else
		{
			if (el > a[middle])
				left = middle + 1;
			else	
			{
				for (int j = middle; j <= real_num; j++)
				{
					s.a[j] = s.a[j + 1];
				}
				s.real_num--;
				return s;
			}
		}
		if (left > right) 
		{
			return s;
		}
	}
	
}
Sets Sets::operator + (Sets s) {
	Sets result;
	int n = real_num, m = s.real_num, i = 0, k = 0, j = 0;
	while ((i < n) && (j < m)) {
		if (a[i] == s.a[j]) {
			result.a[k] = a[i];
			k++, i++, j++;
		}
		else {
			if (a[i] < s.a[j]) {
				result.a[k] = a[i];
				k++;
				i++; 
			}
			else {
				result.a[k] = s.a[j];
				k++;
				j++; 
			}
		}
	}
	while (i < n) {
		result.a[k] = a[i];
		k++, i++;
	}
	while (j < m) {
		result.a[k] = s.a[j];
		k++, j++;
	}
	result.real_num = k;
	return result;
}
Sets Sets::operator - (Sets s) {
	int  n = real_num, m = s.real_num, i = 0, k = 0, j = 0;
	Sets result;
	while ((i < n) && (j < m))
	{
		if (a[i] < s.a[j]) {
			result.a[k] = a[i];
			k++;
			i++;
		}
		else if (a[i] > s.a[j]) {
			result.a[k] = s.a[j];
			k++;
			j++;
		}
		else {
			i++, j++;
		}
	}
	while (i < n) {
		result.a[k] = a[i];
		k++, i++;
	}
	while (j < m) {
		result.a[k] = s.a[j];
		k++, j++;
	}
	while (i < n) {
		result.a[k] = a[i];
		k++, i++;
	}
	result.real_num = k;
	return result;
}
Sets Sets::operator * (Sets s) {
	Sets result;
	int n = real_num, m = s.real_num, i = 0, k = 0, j = 0;
	while ((i < n) && (j < m)) {
		if (a[i] == s.a[j]) {
			result.a[k] = a[i]; 
			k++, i++, j++; 
		}
		else {
			if (a[i] < s.a[j]) {
				i++; 
			}
			else {
				j++; 
			}
		}
	}	
	result.real_num = k;
	return result;
}
bool Sets::operator <(Sets s) {
	Sets res;
	res = *this*s;
	if (*this != res || res.real_num == s.real_num) { 
		return false; 
	}
	else return true;
}
bool Sets::operator >(Sets s) {
	Sets res;
	res = *this*s;
	if (s != res || res.real_num == s.real_num) {
		return false; 
	}
	else return true;
}
bool Sets::operator >=(Sets s)
{
	if (s == *this || *this > s) return true;
	else return false;
}
bool Sets::operator <=(Sets s)
{
	if (s == *this || *this < s) return true;
	else return false;
}
bool Sets::operator ==(Sets s) {
	if (s.real_num != real_num) {
		return false;
	}
	for (int i = 0; i < real_num; ++i)
		if (a[i] != s.a[i]) return false;
	return true;
}
bool Sets::operator !=(Sets s) {
	if (s.real_num != real_num) {
		return true; 
	}
	for (int i = 0; i < real_num; ++i)
		if (a[i] != s.a[i]) return true;
	return false;
}
bool Sets::ElementSearch(Element el) {
	int mid = 0, left = 0, right = real_num - 1;
	while(true) {
		mid = (left + right) / 2;
		if (el < a[mid])       
			right = mid - 1;      
		else if (el > a[mid]) 
			left = mid + 1;   
		else                      
			return true;          
		if (left > right)         
			return false;
	}
}
Sets::Sets(Array s[], Element n) {
	for (int i = 0; i < n; ++i) {
		a[i] = s[i];
	}
}
Sets::Sets() {
	real_num = 0;
	for (int i = 0; i < n; ++i) {
		a[i] = 0;
	}
}
void Sets::clear() {
	for (int i = 0; i < n; ++i) {
		a[i] = 0;
	}
}
ostream & operator << (ostream & st, Sets s) {
	for (int i = 0; i < s.real_num; ++i) {
		st << s.a[i] << " ";
	}
	return st;
}
ofstream  & operator << (ofstream & st, Sets s) {
	for (int i = 0; i < s.real_num; ++i) {
		st << s.a[i] << " ";
	}
	return st;
}
ifstream  & operator >> (ifstream & st, Sets &s) {
	s.real_num = 0;
	while (!st.eof()) {
		st >> s.a[s.real_num];
		s.real_num++;
	}
	return st;
}
Sets::~Sets()
{
}