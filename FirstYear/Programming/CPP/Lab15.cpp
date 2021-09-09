#include "stdafx.h"
#include "iostream"
#include <fstream>
#include <iomanip>
#include <string>
#include <Windows.h>
using namespace std;
const int max_size = 100;
const int metka = 3;
struct student_information
{
	string surname;
	string subject;
	double assessment;
};
struct exam
{
	student_information all_students[max_size];
	int quantity;
};
exam input_data(exam&,string&);
double average(exam, string);
exam special_students(exam, int);
void output_data(exam, double, int, string);
int main()
{
	string a;
	exam data;
	input_data(data,a);
	output_data(special_students(data, metka), average(data,a), metka,a);
	return 0;
}
exam input_data(exam &students,string &student)
{
	ifstream in("input.txt");
	if (!in)
	{
		system("pause");
	}
	in >> students.quantity;
	in >> student;
	for (int i = 0; i<students.quantity; ++i)
	{
		in >> students.all_students[i].surname;
		in >> students.all_students[i].subject;
		in >> students.all_students[i].assessment;
	}
	in.close();
	return students;
}
double average(exam students, string student)
{
	exam  find;
	find.quantity = 0;
	double sum_assessment = 0;
	for (int i = 0; i<students.quantity; ++i)
	{
		if (students.all_students[i].surname == student)
		{
			find.all_students[find.quantity].assessment = students.all_students[i].assessment;
			find.all_students[find.quantity].subject = students.all_students[i].subject;
			find.all_students[find.quantity].surname = students.all_students[i].surname;
			find.quantity++;
		}
	}
	if(find.quantity==0)
	{
		system("pause");
	}
	for (int i = 0; i < find.quantity; i++)
	{
		sum_assessment += find.all_students[i].assessment;
	}
	return sum_assessment / find.quantity;
}
exam  special_students(exam students, int mark)
{
	exam  find;
	find.quantity = 0;
	for (int i = 0; i<students.quantity; ++i)
	{
		if (students.all_students[i].assessment == mark)
		{
			find.all_students[find.quantity].assessment = students.all_students[i].assessment;
			find.all_students[find.quantity].subject = students.all_students[i].subject;
			find.all_students[find.quantity].surname = students.all_students[i].surname;
			find.quantity++;
		}
	}
	return find;
}
void output_data(exam students, double average, int mark, string student)
{
	ofstream out("output.txt");
	int count = 0;
	out << student << average << endl;
	out << mark << endl;
	for (int i = 0; i < students.quantity; i++)
	{
		count++;
		out << setw(10) << students.all_students[i].surname << "|" << students.all_students[i].subject << endl;
	}
	if (count == 0)
	{
		out << mark << endl;
	}
	out.close();
}