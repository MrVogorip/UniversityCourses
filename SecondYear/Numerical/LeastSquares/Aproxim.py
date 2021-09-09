import numpy as np
def Aproxim(x,A,k):
    n=len(x)
    print(n)
    y=np.zeros(len(x))
    for j in range(n):
        summ=0
        for i in range(k+1):
            summ=summ+A[i]*(x[j]**(i));
        y[j]=summ
    return y;