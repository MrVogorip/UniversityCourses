import numpy as np
import pylab
from MNK import MNK
def yfind(a,b,x):
    return (-a/b)/(x+(-1/b))
def func(x):
    return 2/(x+3)
n=10
x,y,o,a,y1,f,f1=np.zeros(n),np.zeros(n),np.zeros(n),np.zeros(n),np.zeros(n),np.zeros(n),np.zeros(n);
for i in range(n):
    x[i]=0.2*i
    y[i]=func(x[i])
    f[i]=round(y[i]*100)/100
    o[i]=y[i]*x[i]
    a[i]=f[i]*x[i]
pylab.plot(x,y)
pylab.plot(x,f,'ro')
pylab.show()
v,v1=MNK(o,y,2,n),MNK(a,f,2,n)
for i in range(n):
    y1[i]=yfind(v[0],v[1],o[i])
    f1[i]=yfind(v1[0],v1[1],a[i])
    f1[i]=round(f1[i]*100)/100
for i in range(n):
    print("%10.3f"%y[i],"%10.3f"%y1[i],"%10.3f"%f[i],"%10.3f"%f1[i])