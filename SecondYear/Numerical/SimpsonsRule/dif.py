import sympy as sp
def dif(f,X,k):
    x=sp.symbols('x')
    Q=sp.diff(f,x,k)
    return sp.Subs(Q,x,X)