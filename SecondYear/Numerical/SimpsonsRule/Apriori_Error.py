from dif import dif
import numpy as np
def Apriori_Error(choice,f,a,b,n):
    k=4
    if(choice==3 or choice==4): k=1
    if(choice==5 or choice==2): k=2
    h=(b-a)/n
    M=np.zeros(n+1)
    for i in range(n+1):
        M[i]=dif(f,(a+i*h),k)
    result = {
            1: lambda : abs((b-a)*(h**4)/180)*max(abs(M)),
            2: lambda : abs((b-a)*(h**2)/12)*max(abs(M)),
            3: lambda : abs((b-a)*h)*max(abs(M)),
            4: lambda : abs((b-a)*h)*max(abs(M)),
            5: lambda : abs((b-a)*h)*max(abs(M))
            }[choice]()
    return result