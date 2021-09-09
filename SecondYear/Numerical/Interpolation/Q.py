import math
import numpy as np
def Q(q,y,Y,n):
    summ = 0
    for i in range(2,n):
        konrazn = np.diff(Y,i)
        konrazn = konrazn[0]
        fact=math.factorial(i)
        Mult=1.0
        for j in range(i):
            Mult=Mult*(q-j-2)
        summ=summ+((konrazn/(fact))*Mult)
    konrazn = np.diff(Y,1)
    konrazn = konrazn[0]
    res =(y-Y[0]-summ)/konrazn
    return res