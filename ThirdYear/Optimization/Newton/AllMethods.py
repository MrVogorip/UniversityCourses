from sympy import Max,diff,symbols,Heaviside,DiracDelta
import numpy as np
from scipy.optimize import minimize

def f(x1,x2):
    return 2*x1**2+5*x2**2-8*x1
def F0(x):
    return 2*x[0]**2+5*x[1]**2-8*x[0]
def g1(x1,x2):
    return 4*x1+5*x2-12
def g2(x1,x2):
    return 8*x1+3*x2-10
def g3(x1,x2):
    return -x1
def g4(x1,x2):
    return -x2
def f1(x,y,r):
   return f(x,y)+r*(Max(0,g1(x,y))**2+Max(0,g2(x,y))**2+Max(0,g3(x,y))**2+Max(0,g4(x,y))**2)
def f2(x,y,r):
    return f(x,y)-r*(1/g1(x,y)+1/g2(x,y)+1/g3(x,y)+1/g4(x,y))
def grad(F,x0,y0,r):
    x, y = symbols('x y')
    gradf = [diff(F(x,y,r), x), diff(F(x,y,r), y)]
    x,y=x0,y0
    gradf[0]= np.float(eval(str(gradf[0])))
    gradf[1]= np.float(eval(str(gradf[1])))
    return gradf
def hess(F,x0,y0,r):
    x, y = symbols('x y')
    gradf2 = [diff(F(x,y,r), x,2), diff(diff(F(x,y,r), y),x)],[diff(diff(F(x,y,r), x),y),diff(F(x,y,r), y,2)]
    x,y=x0,y0
    for i in range (2):
        for j in range (2):
            gradf2[i][j] = np.float(eval(str(gradf2[i][j])))
    return gradf2
def Newton(F,x0,y0,r,eps):
    X=np.array([x0,y0])
    print(X)
    JX=grad(F,X[0],X[1],r)
    print(JX)
    while np.linalg.norm(JX)>eps:
        H=hess(F,X[0],X[1],r)
        print(H)
        X=X-np.dot(np.linalg.inv(H),JX)
        print(X)
        JX=grad(F,X[0],X[1],r) 
        print(JX)
        print()
        
    return X

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
        if i >= 50:
            break
    x = (a + b) / 2
    return x
def ConjugateMethod(F,x0,y0,r,eps):
    i=0
    X=np.array([x0,y0])
    gr=np.array(grad(F,X[0],X[1],r))
    H=-gr
    while np.linalg.norm(H)>eps :
        a=symbols('a')
        Fi=F0(X+a*H)
        def F2(a1):
            a=a1
            return eval(str(Fi))
        lambd=gold_down(F2,0,1,eps)
        x1= np.zeros(2)
        x1=X+lambd*H
        
        grx1=grad(F,x1[0],x1[1],r)
        gr=grad(F,X[0],X[1],r)
        alpha=(np.linalg.norm(grx1)**2)/(np.linalg.norm(gr)**2)
        X=x1
        H =-np.array(grx1+alpha*H)
        if(i>50):
            break
        i+=1
    return [x1[0], x1[1]]

def ShtrafAndBar(choice,method,F,x0,y0,r,c,eps):
    i=0
    x1,y1=method(F,x0,y0,r,eps)
    print()
    r*=c
    while np.linalg.norm([x1-x0, y1-y0])>eps:
        x0=x1
        y0=y1
        x1,y1 = method(F,x0,y0,r,eps)
        print()
        r*=c
        if(i>50):
            break
        i+=1
    if(choice==1):
        return ('Penalty function method (Newton)',x1,y1,i)
    if(choice==2):
        return ('Barrier function method (Newton)',x1,y1,i)
    if(choice==3):
        return ('Method of penalty functions (Conjugate)',x1,y1,i)
    if(choice==4):
        return ('Method of barrier functions (Conjugate)',x1,y1,i)
        

def InlineMethod(choice,r,c,eps):
    result_z = lambda x: 2*x[0]**2+5*x[1]**2-8*x[0]
    g_1 = lambda x: 4*x[0]+5*x[1]-12
    g_2 = lambda x: 8*x[0]+3*x[1]-10
    g_3 = lambda x: -x[0]
    g_4 = lambda x: -x[1]
    X = [10, 10]
    i = 0
    if(choice == 1):
        F = lambda x: result_z(x) + r*(Max(0,g_1(x))**2+Max(0,g_2(x))**2+Max(0,g_3(x))**2+Max(0,g_4(x))**2)
    else:
        F = lambda x: result_z(x) + r*(1.0/(g_1(x)**2 + g_2(x)**2 + g_3(x)**2+ g_4(x)**2))
    while i < 1000:
        if F(X) < eps:
            break
        X = minimize(F, X).x
        i += 1
        r  *= c
    if(choice==1):
        return ('Built-in penalty functions',X[0],X[1],i)
    else:
        return ('Built-in barrier functions',X[0],X[1],i)


def SelectMethod(choice, x0, y0, r, c, eps):
    Name, X, Y, I = {
        1: lambda: ShtrafAndBar(1, Newton, f1, x0, y0, r, c, eps),
        2: lambda: ShtrafAndBar(2, Newton, f2, x0, y0, r, c, eps),
        3: lambda: ShtrafAndBar(3, ConjugateMethod, f1, x0, y0, r, c, eps),
        4: lambda: ShtrafAndBar(4, ConjugateMethod, f1, x0, y0, r, c, eps),
        5: lambda: InlineMethod(1, r, c, eps),
        6: lambda: InlineMethod(2, r, c, eps),
    }[choice]()
    return "{name}\nX = {x}\nY = {y}\Iterations:{i}".format(name = Name, x="%.6f"%X, y="%.6f"%Y, i=I)