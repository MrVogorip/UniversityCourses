import numpy as np
from Q_Fredholm import Q_Fredholm
from F_Fredholm import F_Fredholm
def Solve_Fredholm(a1,b1,N,Lambda):
    h=(b1-a1)/(N-1)
    t=np.zeros(N)
    for i in range(N):
        t[i]=a1+h*i
    s=t
    A=np.zeros(N)
    A[0]=0.5*h
    for i in range(1,N-1):
        A[i]=1*h
    A[N-1]=0.5*h
    q=np.zeros([N,N])
    for i in range(N):
        for j in range(N):
            q[i][j]= Q_Fredholm(t[i],s[j])
    F=F_Fredholm(t)
    M=np.zeros([N,N])
    for i in range(N):
        for j in range(N):
            if(i==j):
                M[i][j]=1-Lambda*A[j]*q[i][j]
            else:
                M[i][j]=-Lambda*A[j]*q[i][j]
    return t,np.linalg.matrix_power(M,-1)@F