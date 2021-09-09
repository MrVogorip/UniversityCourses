#include "pch.h"
#include "Tree.h"
#include <iostream>
#include <iterator>
using namespace std;
Tree::Tree() {
	tree = NULL;
}
Tree::~Tree() {
	DeleteTree(tree);
}
void Tree::Print(node *&root) {
	if (root != NULL) {
		Print(root->left);
		cout << root->info << " ";
		Print(root->right);
	}
}
void Tree::AddBranch(node *&root, TInfo item) {
	if (root == NULL) root = new node(item);
	else {
		if (item < root->info)AddBranch(root->left, item);
		else AddBranch(root->right, item);
	}
}
bool Tree::IsEmpty() {
	return tree == NULL;
}
TInfo Tree::MinItem() {
	node *temp = tree;
	while (temp->left != NULL)
		temp = temp->left;
	return temp->info;
}
TInfo Tree::MaxItem() {
	node *temp = tree;
	while (temp->right != NULL)
		temp = temp->right;
	return temp->info;
}
node*Tree::SearchBranch(node *root, TInfo item) {
	if (root == NULL) return NULL;
	if (root->info == item) return root;
	if (item < root->info) 
		if (root->left != NULL) return SearchBranch(root->left, item);
		else if (root->right) return SearchBranch(root->right, item);
	return NULL;
}
bool Tree::SearchItem(TInfo item) {
	if (SearchBranch(tree, item) == NULL)
		return false;
	return true;
}
node*Tree::MaxBranch(node *root) {
	if (root->right == NULL)
		return root;
	return MaxBranch(root->right);
}
node*Tree::MinBranch(node *root) {
	if (root->left == NULL)
		return root;
	return MinBranch(root->left);
}
void Tree::DeleteTree(node* &root) {
	if (root != NULL) {
		DeleteTree(root->left);
		DeleteTree(root->right);
		delete root;
		root = NULL;
	}
}
node*Tree::DeleteItem(node* root, TInfo item) {
	if (root == NULL) {
		return NULL;
	}
	if (root->info > item) {
		root->left = DeleteItem(root->left, item);
		return root;
	}
	else if (root->info< item) {
		root->right = DeleteItem(root->right, item);
		return root;
	}
	else {
		if (root->left && root->right) {
			node* Temp = MaxBranch(root->left);
			root->info = Temp->info;
			root->left = DeleteItem(root->left, Temp->info);
			return root;
		}
		else if (root->left) {
			node* Temp = root->left;
			free(root);
			return Temp;
		}
		else if (root->right) {
			node* Temp = root->right;
			free(root);
			return Temp;
		}
		else {
			free(root);
			return NULL;
		}
	}
}
void Tree::Insert(TInfo item)
{
	node* z = new node;
	node* y = NULL;
	node* x = tree;
	z->info = item;
	while (x != NULL)
	{
		y = x;
		if (z->info < x->info) x = x->left;
		else x = x->right;
	}
	z->parent = y;
	if (y == NULL) tree = z;
	else
	{
		if (z->info < y->info) y->left = z;
		else y->right = z;
	}
	z->left = NULL;
	z->right = NULL;
}
bool Tree::SearchBranch(TInfo item)
{
	node *p, *q;
	bool B = false; q = p =tree;
	if (tree != NULL)
		do
		{
			q = p;
			if (p->info == item) B = true;
			else
			{
				q = p;
				if (item < p->info) p = p->left;
				else  p = p->right;
			}
		} while (!B && p != NULL);
	return B;
}