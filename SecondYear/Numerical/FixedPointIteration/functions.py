from math import sin
from numpy import sign
from scipy.misc import derivative

def f(x):
    return sin(4.8*x)*5.67-4.5*x
def f1(x):
    return derivative(f, x, dx=1e-6)
def fi(x,lm):
    return x+lm*f(x)

def inetration(a,b,eps):
    if abs(f1(a))>1:
        l1=1/f1(a)
    else:
        l1=sign(f1(a))
    if abs(f1(b))>1:
        l2=1/f1(b)
    else:
        l2=sign(f1(b))
    if l1<l2:
        lm=-l1
    else:
        lm=-l2
    x0, i=(a+b)/2, 0
    x1=fi(x0,lm)
    while abs(x0-x1)>eps:
        x0=x1
        x1, i=fi(x0,lm),i+1
    str = ("x =%10.5f"%x1+"\ni =%3i"%i)
    return str