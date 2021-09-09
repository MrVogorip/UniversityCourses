import numpy as np
from math import sqrt
import scipy.stats

X1 = np.array([5,2,10,2,3,1,6,10,3,5,6,4,4,3,2,2,11,4,2,7])
X2 = np.array([1.12,1.21,0.90,1.00,1.33,0.52,0.89,0.77,1.49,1.45,0.83,1.30,0.62,0.60,0.95,0.86,1.04,1.36,0.60,0.59])
Y = np.array([22,3,95,2,94,0,29,107,10,27,30,15,10,8,2,2,120,15,2,44,])
X = np.zeros((20,5))
for i in range(20):
    X[i][0] = 1
    X[i][1] = X1[i]
    X[i][2] = X1[i]*X1[i]
    X[i][3] = X2[i]
    X[i][4] = X2[i]*X2[i]
print(X)
beta = np.linalg.inv(X.transpose()@X)@X.transpose()@Y
print('beta = ',beta)
Y1 = beta[0]+beta[1]*X1+beta[2]*X1*X1+beta[3]*X2+beta[4]*X2*X2
esp = Y-Y1
print('esp = ',esp)
s=0
for i in range(len(esp)):
    s+=esp@esp
print('s = ',s)
n=20
m=4
d=scipy.stats.t.pdf(0.99,n-m-1)*sqrt(s)
print('d = ',d)
print('leftint = ',Y-d)
print('rightint = ',Y+d)
print('Y1 = ',Y1)
