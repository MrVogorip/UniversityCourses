import sympy as sp
from Apriori_Error import Apriori_Error
def Simpson(f,a,b,n,h,x):
    summ,q1,q2=0,0,0
    for i in range(1,n):
        if(i%2==1):q1=q1+float(sp.Subs(f,x,a+i*h)) 
        else:q2=q2+float(sp.Subs(f,x,a+i*h)) 
        summ=(h/3)*(float(sp.Subs(f,x,a))+4*q1+2*q2+float(sp.Subs(f,x,b)))
    return summ
def Trapezium(f,a,b,n,h,x):
    summ=0
    for i in range(1,n):
        summ=summ+float(sp.Subs(f,x,a+i*h))
    summ=(h/2)*(float(sp.Subs(f,x,a))+2*summ+float(sp.Subs(f,x,b)))
    return summ
def LeftRectangles(f,a,b,n,h,x):
    summ=0
    for i in range(1,n):
        summ=summ+h*float(sp.Subs(f,x,a+i*h))
    summ=h*float(sp.Subs(f,x,a))+summ
    return summ
def RightRectangles(f,a,b,n,h,x):
    summ=0
    for i in range(1,n):
        summ=summ+h*float(sp.Subs(f,x,a+i*h))
    summ=h*float(sp.Subs(f,x,b))+summ
    return summ     
def MediumRectangles(f,a,b,n,h,x):
    summ=0
    for i in range(1,n+1):
        summ=summ+h*float(sp.Subs(f,x,((a+(i-1)*h)+(a + i*h))/2))
    return summ
def Integral(choice,f,a,b,n,e):
    if (Apriori_Error(1,f,a,b,n)>e):
        while(Apriori_Error(1,f,a,b,n)>e):
            n=n+1
    else:
        while(Apriori_Error(1,f,a,b,n)<e):
            n=n-1
    h=(b-a)/n
    x=sp.symbols('x')
    result = {
            1: lambda : Simpson(f,a,b,n,h,x),
            2: lambda : Trapezium(f,a,b,n,h,x),
            3: lambda : LeftRectangles(f,a,b,n,h,x),
            4: lambda : RightRectangles(f,a,b,n,h,x),
            5: lambda : MediumRectangles(f,a,b,n,h,x)
            }[choice]()
    return result