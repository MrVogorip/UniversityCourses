import numpy
def Gausse(B): 
    n=len(B)
    X = numpy.zeros(n)
    for i in range (n-1):
        buf=B[i][i]
        for j in range(n+1):
            B[i][j]/= buf
        for k in range(i+1, n):
            buf = B[k][i]
            for j in range(n+1):
                B[k][j]=B[k][j]-B[i][j]*buf
    X[n-1]=B[n-1][n]/B[n-1][n-1]
    i=n-2    
    while i>=0:
        sum,k=0,n-1
        while k>i:
            sum=sum+B[i][k]*X[k]
            k-=1
        X[i]=B[i][n]-sum
        i-=1
    return X