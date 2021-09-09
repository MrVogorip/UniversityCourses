import numpy as np
from f1 import f1
from f2 import f2
def Euler(m,x0,y0,z0):
    x=np.zeros(m);
    y_m=np.zeros(m);
    z_m=np.zeros(m);
    x[0]=x0;
    h=0.1;
    for i in range(1,m):
        x[i]=x[0]+i*h;
    y_m[0]=y0;
    z_m[0]=z0;
    for i in range(m-1):
        y_m[i+1]=y_m[i]+h*f1(x[i]+h/2,y_m[i]+h/2*f1(x[i],y_m[i],z_m[i]),z_m[i]+h/2*(f2(x[i],y_m[i],z_m[i])));   
        z_m[i+1]=z_m[i]+h*f2(x[i]+h/2,y_m[i]+h/2*f1(x[i],y_m[i],z_m[i]),z_m[i]+h/2*(f2(x[i],y_m[i],z_m[i])));
    for i in range(m):   
        print(str("%.5f"%x[i])+"\t"+str("%.5f"%y_m[i])+"\t"+str("%.5f"%z_m[i]))