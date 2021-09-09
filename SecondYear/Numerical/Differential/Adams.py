import numpy as np
from func import func
def Adams(xx,yy,m):
    n=3
    h=0.1
    y=np.copy(yy)
    for i in range(n,m-2):
        y[i+1]=y[i]+(55*func(xx[i],y[i])-59*func(xx[i-1],y[i-1])+37*func(xx[i-2],y[i-2])-9*func(xx[i-3],y[i-3]))*h/24;
        xx[i+1] = xx[i]+h;
    return y