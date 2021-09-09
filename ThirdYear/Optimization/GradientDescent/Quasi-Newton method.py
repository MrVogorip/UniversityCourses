from sympy import symbols,diff
from numpy import array, dot, linalg
from math import  fabs
import numpy as np
import numpy as np
from scipy.optimize import minimize
from sympy import *
from math import sqrt

fi = (pow(5, 0.5) + 1) / 2

def F(x1, x2):
    return 4*(x1)**2 + 7*(x2)**2 +4*x1*x2+6*5**0.5*x1-12*5**0.5*x2+51

def f(x):
    return 4*(x[0])**2 + 7*(x[1])**2 +4*x[0]*x[1]+6*5**0.5*x[0]-12*5**0.5*x[1]+51

def gradient(x0,y0):
    x, y = symbols('x y')
    gradf = np.array((diff(F(x,y), x), diff(F(x,y), y)))
    x,y=x0,y0
    gradf[0],gradf[1] = eval(str(gradf[0])),eval(str(gradf[1]))
    return gradf

def gold_down(f, a, b, eps):
    i = 0
    while fabs(b - a) >= eps:
        x1 = b - (b - a) / fi
        x2 = a + (b - a) / fi
        y1 = f(x1)
        y2 = f(x2)
        if y1 >= y2:
            a = x1
        else:
            b = x2
        i += 1
        if i >= 10000:
            break
    x = (a + b) / 2
    return x

def QvNewton(X,eps):
    i=0
    grad = gradient(X[0],X[1]).astype(float)
    H2=np.eye(2)
    while np.linalg.norm(grad)> eps :
        p=-(H2@grad)
        a=symbols('a')
        Fi=simplify(f(X+a*p))
        def Fi2(a1):
            a=a1
            return eval(str(Fi))
        
        alpha = gold_down(Fi2, 0, 1, eps)
        x1=X+alpha*p
        s=x1-X
        X=x1
        gradx1=gradient(x1[0],x1[1]).astype(float)
        dy=(gradx1-grad).astype(float)
        grad = gradx1
        ro = 1/(dy.T@s)
        I=np.eye(2).astype(float)
        ro = 1.0 / (np.dot(dy.T, s))
        A1 = I - ro *( s[:, np.newaxis] * dy[np.newaxis, :])
        A2 = I - ro * dy.T @ s
        H2 = A1@ H2 @ A2 + (ro * s[:, np.newaxis]* s[np.newaxis, :])
        grad = gradient(X[0],X[1]).astype(float)
        i+=1
    return [X,i]

print(QvNewton([0,0],0.01))