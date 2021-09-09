import numpy as np
from f1 import f1
from f2 import f2
def Runge(m,x0,y0,z0):
    x=np.zeros(m);
    y_RK=np.zeros(m);
    K1Y=np.zeros(m);
    K1Z=np.zeros(m);
    K2Y=np.zeros(m);
    K2Z=np.zeros(m);
    K3Y=np.zeros(m);
    K3Z=np.zeros(m);
    K4Y=np.zeros(m);
    K4Z=np.zeros(m);
    z_RK=np.zeros(m);
    y_RK[0]=y0;
    z_RK[0]=z0;
    x[0]=x0;
    h=0.1;
    for i in range(1,m):
        x[i]=x[0]+i*h;
    for i in range(m-1):
        K1Y[i]=h*f1(x[i],y_RK[i],z_RK[i]);
        K1Z[i]=h*f2(x[i],y_RK[i],z_RK[i]);
        K2Y[i]=h*f1(x[i]+h/2,y_RK[i]+K1Y[i]/2,z_RK[i]+K1Z[i]/2);
        K2Z[i]=h*f2(x[i]+h/2,y_RK[i]+K1Y[i]/2,z_RK[i]+K1Z[i]/2);
        K3Y[i]=h*f1(x[i]+h/2,y_RK[i]+K2Y[i]/2,z_RK[i]+K2Z[i]/2);
        K3Z[i]=h*f2(x[i]+h/2,y_RK[i]+K2Y[i]/2,z_RK[i]+K2Z[i]/2);
        K4Y[i]=h*f1(x[i]+h,y_RK[i]+K3Y[i],z_RK[i]+K3Z[i]);
        K4Z[i]=h*f2(x[i]+h,y_RK[i]+K3Y[i],z_RK[i]+K3Z[i]);
        y_RK[i+1]=y_RK[i]+(K1Y[i]+2*K2Y[i]+2*K3Y[i]+K4Y[i])/6;
        z_RK[i+1]=z_RK[i]+(K1Z[i]+2*K2Z[i]+2*K3Z[i]+K4Z[i])/6;
    for i in range(m):   
        print(str("%.5f"%x[i])+"\t"+str("%.5f"%y_RK[i])+"\t"+str("%.5f"%z_RK[i]))