import numpy as np
def MNK(x,y,k,n):
    X1=np.zeros((n+1,k+1))
    
    A=np.zeros(k+1)
    for i in range(n+1):
        for j in range(k+1):
            X1[i][j]=x[i]**(j);
    print()
    A=np.linalg.inv(X1.T@X1)@X1.T@y;
    return A;