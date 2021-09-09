import numpy as np
from Q import Q
def Iter(y,eps):
    X = np.loadtxt("D:\Xx.txt")
    Y = np.loadtxt("D:\Yy.txt")
    h = X[1]-X[0]
    n = len(X)
    konrazn = np.diff(Y,1)
    konrazn = konrazn[0]
    q = (y-Y[0])/konrazn
    q_next = Q(q,y,Y,n)
    while abs(q_next-q)>eps:
        q=q_next
        q_next=Q(q,y,Y,n)
    res = q_next*h+X[0]
    return res      