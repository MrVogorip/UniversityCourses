import numpy as np
from func import func
def Milna(xx,yy,m):
    n=3
    h=0.1
    y=np.copy(yy)
    for i in range(n,m-2):
        y[i+1]=y[i-3]+(2*func(xx[i-2],y[i-2])-func(xx[i-1],y[i-1])+2*func(xx[i],y[i]))*4*h/3;
        xx[i+1]=xx[i]+h;
        y[i+1]=y[i-1]+(func(xx[i-1],y[i-1])+4*func(xx[i],y[i])+func(xx[i+1],y[i+1]))*h/3;
    return y