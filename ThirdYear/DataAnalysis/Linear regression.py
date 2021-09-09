import numpy as np
from math import sqrt
import scipy.stats
X=np.array([
[1, 6.25, 7.31, 2.7],
[1, 46.0, 2.10, 4.7],
[1, 49.0, 3.63, 4.2],
[1, 44.4, 14.6, 2.6],
[1, 42.6, 11.7, 3.7],
[1, 36.7, 7.35, 4.0],
[1, 18.6, 13.4, 4.4],
[1, 17.2, 4.84, 2.9],
[1, 41.8, 15.4, 4.8],
[1, 49.2, 9.27, 3.9]])
Y = np.array([99,469,546,494,456,438,217,271,458,453])
beta = np.linalg.inv(X.transpose()@X)@X.transpose()@Y
print('beta = ',beta)
Y1 = beta[0]+beta[1]*X.transpose()[1]+beta[2]*X.transpose()[2]+beta[3]*X.transpose()[3]
esp = Y-Y1
print('esp = ',esp)
s=0
for i in range(len(esp)):
    s+=esp@esp
print('s = ',s)
n=10
m=3
d=scipy.stats.t.pdf(0.99,n-m-1)*sqrt(s)
print('d = ',d)
print('leftint = ',Y-d)
print('rightint = ',Y+d)
print('Y1 = ',Y1)
