import numpy as np
def MNK(x,y,k,n):
    X1=np.zeros((n,k))
    A=np.zeros(k)
    for i in range(n):
        for j in range(k):
            X1[i][j]=x[i]**(j);
    A=np.linalg.inv(X1.T@X1)@X1.T@y;
    return A;