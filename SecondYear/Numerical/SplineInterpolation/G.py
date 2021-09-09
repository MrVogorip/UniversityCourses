import numpy as np
def G(xx,X,Y):
    n=len(X);
    h = [0. for x in range(n-1)]
    for i in range(n-1):
        h[i]=X[i+1]-X[i];
    H1=np.zeros((n-2,n));
    A=np.zeros((n-2,n-2));
    for i in range(n-2):
        H1[i][i]=1/h[i];
        H1[i][i+1]=-(1/h[i]+1/h[i+1]);
        H1[i][i+2]=1/h[i+1];
    for i in range(n-2):
        A[i][i]=(h[i]+h[i+1])/3;
        if i!=0:
            A[i][i-1]=h[i]/6;
        if i!=n-3:
            A[i,i+1]=h[i+1]/6;
    S=H1@Y;
    M=np.linalg.solve(A,S);
    M1=np.zeros(n);
    for i in range(n-2):
        M1[i+1]=M[i];
    for j in range(1):
        X1=xx;
        for i in range(n-1):
            if X[i]<=X1 and X1<=X[i+1]:
                k=i;
                break;
        yy=M1[k]*((X[k+1]-X1)**3)/(6*h[k]) + M1[k+1]*((X1-X[k])**3)/(6*h[k])+ (Y[k]-M1[k]*(h[k]**2)/6)*(X[k+1]-X1)/h[k]+(Y[k+1]-M1[k+1]*(h[k]**2)/6)*(X1-X[k])/h[k];
    return yy