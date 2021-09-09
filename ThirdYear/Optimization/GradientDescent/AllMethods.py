from sympy import symbols,diff
from numpy import array, dot, linalg
from math import  fabs

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

def f(x1, x2):
    return 4*(x1)**2 + 7*(x2)**2 +4*x1*x2+6*5**0.5*x1-12*5**0.5*x2+51

fi = (pow(5, 0.5) + 1) / 2

def SelectMethod(choice, x1, x2, eps, alpha, b, delta, delta2, h):
    x1 = float(x1)
    x2 = float(x2)
    eps = float(eps)
    b = float(b)
    alpha = float(alpha)
    delta = float(delta)
    delta2 = float(delta2)
    x3, x4, i = {
        1: lambda: GradientDescentConstantStep(x1, x2, eps),
        2: lambda: GradientDescentWithStepDivision(x1, x2, eps, alpha, delta),
        3: lambda: GradientDescentSteepest(x1, x2, eps),
        4: lambda: RavineMethod(x1, x2, eps, alpha, delta, delta2),
        5: lambda: NewtonsMethod(x1, x2, eps),
        6: lambda: NewtonsMethodModified(alpha, eps),
        7: lambda: ConjugateMethod(alpha, eps),
    }[choice]()
    return "x = {argX1}\nx = {argX2}\n{Iter}".format(argX1="%.3f" % x3, argX2="%.3f" % x4, Iter=i)


def GradientDescentConstantStep(x1, x2, eps):
    a = 0.01
    x3 = x1 + 0.1
    x4 = x2 + 0.1
    i = 0
    while fabs(f(x1, x2) - f(x3, x4)) > eps:
        x1 = x3
        x2 = x4
        x, y = symbols('x y')
        (xnew, ynew) = (diff(f(x, y), x), diff(f(x, y), y))
        (x, y) = (x1, x2)
        gradf1 = eval(str(xnew))
        gradf2 = eval(str(ynew))
        x3 = x1 - a * gradf1
        x4 = x2 - a * gradf2
        i += 1
    return [x3, x4, i]

def GradientDescentWithStepDivision(x1, x2, eps, alpha, delta):
    x3 = x1 + 0.1
    x4 = x2 + 0.1
    i = 0
    while fabs(f(x1, x2) - f(x3, x4)) > eps:
        x1 = x3
        x2 = x4
        x, y = symbols('x y')
        (xnew, ynew) = (diff(f(x, y), x), diff(f(x, y), y))
        (x, y) = (x1, x2)
        gradf1 = eval(str(xnew))
        gradf2 = eval(str(ynew))
        x3 = x1 - alpha * gradf1
        x4 = x2 - alpha * gradf2
        i += 1
        while f(x3, x4) - f(x1, x2) > -1 * delta * alpha * (gradf1 ** 2 * gradf2 ** 2) ** 0.5:
            alpha = alpha / 2
    return [x3, x4, i]
    
def GradientDescentSteepest(x1, x2, eps):
    x3 = x1 + 0.1
    x4 = x2 + 0.1
    i = 0
    while fabs(f(x1, x2) - f(x3, x4)) > eps:
        x1 = x3
        x2 = x4
        x, y = symbols('x y')
        (xnew, ynew) = (diff(f(x, y), x), diff(f(x, y), y))
        (x, y) = (x1, x2)
        gradf1 = eval(str(xnew))
        gradf2 = eval(str(ynew))

        def fa(a1): return x1 - a1 * gradf1

        def fa1(a1): return x2 - a1 * gradf2

        def FA(a1):
            return f(fa(a1), fa1(a1))

        a2 = gold_down(FA, 0, 1, eps)
        x3 = x1 - a2 * gradf1
        x4 = x2 - a2 * gradf2
        i += 1
    return [x3, x4, i]

def RavineMethod(x1, x2, eps, alpha, delta, delta2):
    x4 = x1 + 0.1
    x3 = x2 + 0.1
    i = 0
    xtemp1 = x1
    xtemp2 = x2
    while ((xtemp1 - x3) ** 2 * (xtemp2 - x4) ** 2) ** 0.5 > eps:
        xtemp1 = x1
        xtemp2 = x2
        while  f(x3, x4) < f(x1, x2):
            x1 = x3
            x2 = x4
            x, y = symbols('x y')
            (xnew, ynew) = (diff(f(x, y), x), diff(f(x, y), y))
            (x, y) = (x1, x2)
            gradf1 = eval(str(xnew))
            gradf2 = eval(str(ynew))
            if (fabs(gradf1) > delta):
                g1 = gradf1
            else:
                g1 = 0
            if (fabs(gradf2) > delta):
                g2 = gradf2
            else:
                g2 = 0
            x3 = x1 - alpha * g1
            x4 = x2 - alpha * g2
        while  f(x3, x4) < f(x1, x2):
            x1 = x3
            x2 = x4
            x, y = symbols('x y')
            (xnew, ynew) = (diff(f(x, y), x), diff(f(x, y), y))
            (x, y) = (x1, x2)
            gradf1 = eval(str(xnew))
            gradf2 = eval(str(ynew))
            if (fabs(gradf1) < delta2):
                g1 = gradf1
            else:
                g1 = 0
            if (fabs(gradf2) < delta2):
                g2 = gradf2
            else:
                g2 = 0
            x3 = x1 - alpha * g1
            x4 = x2 - alpha * g2
        i += 1
    return [x3, x4, i]

def NewtonsMethod(x1, x2, eps):
    xn=[]
    yn=[]
    t=[]
    a=[]
    xn.append(x1), yn.append(x2)
    i = 1
    gr2 = 1
    while linalg.norm(gr2)>eps:
        x, y = symbols('x y')
        (grad1, grad2) = (diff(f(x, y), x), diff(f(x, y), y))
        (t1,t2,p1,p2) = (diff(f(x, y), x, 2), diff(f(x, y), y, 2),diff(grad1,y),diff(grad2,x))
        (x, y) = (xn[i-1], yn[i-1])
        gr2 = array([[eval(str(t1)), eval(str(p1))], [eval(str(p2)), eval(str(t2))]])
        a = linalg.inv(gr2)
        t=dot(a, [eval(str(grad1)), eval(str(grad2))])
        xn.append(xn[i-1]-t[0])
        yn.append(yn[i-1]-t[1])
        if(fabs(f(xn[i],yn[i])-f(xn[i-1],yn[i-1]))<eps or i==100):
            break
        i+=1
    return [xn[i], yn[i], i]

def NewtonsMethodModified(alp, eps):
    xn=[]
    yn=[]
    gr=[]
    a=[]
    xn.append(0), yn.append(0)
    x, y = symbols('x y')
    (grad1, grad2) = (diff(f(x, y), x), diff(f(x, y), y))
    (t1, t2, p1, p2) = (diff(f(x, y), x, 2), diff(f(x, y), y, 2), diff(grad1, y), diff(grad2, x))
    (x, y) = (xn[0], yn[0])
    gr.append(eval(str(-grad1))),  gr.append(eval(str(-grad2)))
    gr2 = array([[eval(str(t1)), eval(str(p1))], [eval(str(p2)), eval(str(t2))]])
    a = linalg.inv(gr2)
    i = 0
    while linalg.norm(gr)>eps:
        p=dot(a, gr)
        def f_n(alp):
            return f(xn[i]+alp*p[0],yn[i]+alp*p[1])
        alp = gold_down(f_n, 0, 1, eps)
        xn.append(xn[i]+alp*p[0])
        yn.append(yn[i]+alp*p[1])
        gr=[]
        x, y = symbols('x y')
        (grad1, grad2) = (diff(f(x, y), x), diff(f(x, y), y))
        (x, y) = (xn[i+1], yn[i+1])
        gr.append(eval(str(-grad1))), gr.append(eval(str(-grad2)))
        if (i == 1000):
            break
        i+=1
    return [xn[i], yn[i], i]

def ConjugateMethod(alp, eps):
    x0=[]
    y0=[]
    x1=[]
    y1=[]
    gr=[]
    grst = []
    x0.append(0), y0.append(0)
    x, y = symbols('x y')
    (grad1, grad2) = (diff(f(x, y), x), diff(f(x, y), y))
    (x, y) = (x0[0], y0[0])
    grst.append(eval(str(grad1))),  grst.append(eval(str(grad2)))
    x1.append(x0[0]-alp*grst[0]), y1.append(y0[0]-alp*grst[1])
    x, y = symbols('x y')
    (gr1, gr2) = (diff(f(x, y), x), diff(f(x, y), y))
    (x, y) = (x1[0], y1[0])
    gr.append(eval(str(gr1))),  gr.append(eval(str(gr2)))
    i = 0
    while linalg.norm(gr)>eps:
        t=[]
        def f_n(alp):
            return f(x1[i]-alp*gr[0],y1[i]-alp*gr[1])
        alp = gold_down(f_n, 0, 1, eps)
        beta=(linalg.norm(gr))**2/(linalg.norm(grst))**2
        if(i%2 == 0):
            t.append(-gr[0])
            t.append(-gr[1])
        else:
            t.append(-gr[0] + beta * grst[0])
            t.append(-gr[1] + beta * grst[1])
        x1.append(x1[i]+alp*t[0])
        y1.append(y1[i]+alp*t[1])
        grst=[]
        grst.append(gr[0]), grst.append(gr[1])
        gr=[]
        x, y = symbols('x y')
        (grad1, grad2) = (diff(f(x, y), x), diff(f(x, y), y))
        (x, y) = (x1[i+1], y1[i+1])
        gr.append(eval(str(grad1))), gr.append(eval(str(grad2)))
        i += 1
        if (i == 1000):
            break
    return [x1[i], y1[i], i]
