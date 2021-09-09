#pragma once
typedef int TInfo;
#include <iostream>
struct node{
	TInfo info;
	node *left,*right,*parent;
	node(){
		info = NULL, left = NULL, right = NULL, parent = NULL;
	}
	node(TInfo new_item){
		info = new_item;
		left = NULL, right = NULL , parent = NULL;
	}
};
class Tree{
public:
	node *tree;
	void AddBranch(node *&root, TInfo item);
	void Insert(TInfo item);
	void Print(node *&root);
	node*SearchBranch(node *root, TInfo item);
	bool SearchItem(TInfo item);
	bool SearchBranch (TInfo item);
	bool IsEmpty();
	TInfo MinItem();
	node*MinBranch(node *root);
	TInfo MaxItem();
	node*MaxBranch(node *root);
	void DeleteTree(node* &root); 
	node*DeleteItem(node* root, TInfo item);
	Tree();
	~Tree();
};