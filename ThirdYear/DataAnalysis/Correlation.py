import numpy as np
import math
import scipy.stats
def dop(q,i,j):
    y=np.linalg.norm(q)*(np.linalg.inv(q))[j][i];
    return y;
x=[
[0.31, 0.18, 0.94],
[0.33, 0.13, 0.97],
[0.34, 0.20, 0.92],
[0.27, 0.20, 0.91],
[0.30, 0.15, 0.96],
[0.30, 0.19, 0.97],
[0.35, 0.13, 0.93],
[0.34, 0.17, 0.91]]
p=3
n=8
A = np.zeros(p)
for i in range(p):
    for j in range(n):
        A[i]+=x[j][i]
    A[i]/=n
print(A)
q = np.zeros(p)
for i in range(p):
    sym =0
    for j in range(n):
        sym += (x[j][i]-A[i])**2
    q[i]= math.sqrt((1.0 / n) *sym)
print(q)
cov = np.zeros((p,p))
for i in range(p):
    for j in range(p):
        sym = 0
        for k in range(n):
            sym += (x[k][i] - A[i])*(x[k][j] - A[j])
        cov[i][j]=(1.0 / n)*sym
r= np.zeros((p,p))
for i in range(p):
    for j in range(p):
        r[i][j]= cov[i][j]/math.sqrt(cov[i][i]*cov[j][j])
print(r)
Alpha = 0.05
print('Critical point of the Student distribution',scipy.stats.t.ppf(1-Alpha,n-2))
z1=(1.0/2)*math.log((1+r[0][2])/(1-r[0][2]))-((r[0][2])/(2*(n-1)))-(scipy.stats.norm.ppf(1-(Alpha/2),0,1)/math.sqrt(n-3))
z2=(1.0/2)*math.log((1+r[0][2])/(1-r[0][2]))-((r[0][2])/(2*(n-1)))+(scipy.stats.norm.ppf(1-(Alpha/2),0,1)/math.sqrt(n-3))
print('Confidence interval for sample pairwise correlation coefficient',np.tanh(z1),np.tanh(z2) )
R1_23=math.sqrt(1-(np.linalg.norm(r))/(dop(r,0,0)));
print('Sample multiple correlation coefficient',R1_23)
F_expr=(n-p)/(p-1)*(R1_23*R1_23)/(1-(R1_23*R1_23));
print('F_expr = ',F_expr)
