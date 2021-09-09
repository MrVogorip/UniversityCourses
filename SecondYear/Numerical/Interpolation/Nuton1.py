import math
import numpy as np
def Nuton1(x,X,Y,n):
    res = Y[0]
    H=X[1]-X[0]
    for i in range(1,n):
        konrazn = np.diff(Y,i)
        konrazn=konrazn[0]
        fact=math.factorial(i)
        Mult=1.0
        for j in range(i):
            Mult=Mult*(x-X[j])
        res=res+((konrazn/(fact * H**i))*Mult)
    return res