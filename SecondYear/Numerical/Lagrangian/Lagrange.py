import numpy as np
def lagrange(x,y):
    N=len(x)
    x2=np.zeros(N-1)
    for i in range(N-1):
        x2[i]=(x[i]+x[i+1])/2
    N_res=len(x2)
    y2=np.zeros(N_res)
    for k in range(N):
        Lagr=np.ones(N_res)
        for j in range(k):
            for i in range(N_res):
                Lagr[i]=Lagr[i]*(x2[i]-x[j])/(x[k]-x[j])
        for j in range(k+1,N): 
            for i in range(N_res):
                Lagr[i]=Lagr[i]*(x2[i]-x[j])/(x[k]-x[j])
        y2 = y2 + y[k]*Lagr
    x_res = np.sort(np.concatenate([x, x2]))
    y_res = np.sort(np.concatenate([y, y2]))
    return x_res,y_res