import numpy as np
from sympy import symbols,diff,exp
from scipy import optimize

def f(x,y,z):
    return 8*x**2+4*y**2-128*y -25*(x+z)-4*z**2+exp(x+z)

def F(x):
    return 8*x[0]**2+4*x[1]**2-128*x[1] -25*(x[0]+x[2])-4*x[2]**2+exp(x[0]+x[2])

def Gradient(x0,y0,z0):
    x,y,z = symbols('x y z')
    gradf = np.array((diff(f(x,y,z), x), diff(f(x,y,z), y),diff(f(x,y,z), z)))
    x,y,z = x0,y0,z0
    gradf[0] = float(eval(str(gradf[0])))
    gradf[1] = float(eval(str(gradf[1])))
    gradf[2] = float(eval(str(gradf[2])))
    return gradf


fi = float((pow(5, 0.5) + 1) / 2)
def gold_down(f, a, b, eps):
    i = 0
    while abs(b - a) >= eps:
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


def ConjugateMethod(X,eps):
    i=0
    grad=Gradient(X[0],X[1],X[2])
    H=-grad
    while np.linalg.norm(H)>eps :
        a=symbols('a')
        Fi=F(X+a*H)
        def F2(a1):
            a=a1
            return eval(str(Fi))
        lambd=gold_down(F2,0,1,eps)
        x1= np.zeros(3)
        x1=X+lambd*H
        
        gradx1=Gradient(x1[0],x1[1],x1[2])
        grad=Gradient(X[0],X[1],X[2])
        alpha=(np.linalg.norm(gradx1)**2)/(np.linalg.norm(grad)**2)
        X=x1
        H =-gradx1+alpha*H
        i+=1
    return [x1[0], x1[1] ,x1[2], i]

print(ConjugateMethod((0,0,0),0.001))
print(optimize.minimize(F, (0, 0, 0)))