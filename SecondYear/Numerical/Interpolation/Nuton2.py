import math
import numpy as np
def Nuton2(x,X,Y,n):
    res = Y[n-1]
    H=X[1]-X[0]
    for i in range(1,n):
        konrazn = np.diff(Y,i)
        konrazn=konrazn[n-i-1]
        fact=math.factorial(i)
        Mult=1.0
        for j in range(i):
            Mult=Mult*(x-X[n-j-1])
        res=res+((konrazn/(fact * H**i))*Mult)
    return res