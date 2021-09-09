import numpy as np
import scipy.stats
strok = 15
stolb = 2
I=np.array([1,	2,	3,	4,	5,	6,	7,	8,	9,	10,	11,	12,	13,	14,	15])
Y=np.array([229,	340,	405,	438,	396,	452,	463,	508,	519,	568,	564,	601,	622,	632,	791])
A=np.zeros((strok,stolb))
for i in range(strok):
    for j in range(stolb):
        A[i,j]=I[i]**j
U=np.linalg.inv(A.T@A)@A.T@Y
Y1=U[1]*I+U[0]
print("Y1:",Y1)
print("U:",U)
omega = sum(I)/strok
print("Ω:",omega)
S1=sum(U[1]**2*(I-omega)**2)
print("S1:",S1)
S2=sum(Y**2-(Y**2)/strok)
print("S2:",S2)
S=S2-S1
print("S:",S)
F=(S1*(strok-1))/S
print("F:",F)
Fkr = scipy.stats.f.ppf(0.05,1,strok-1)
print("Fkr:",Fkr)
if (F>Fkr):
    print("F>Fkr")
else: 
    print("F<=Fkr")
eps=Y-Y1
print("Eps:",eps)
dChisl=0
for i in range(1,strok):
    dChisl+=(eps[i]-eps[i-1])**2
d=dChisl/sum(eps[1:]**2)
print("d: %.2f"%d)
criterionLeft,criterionRight=1.08,1.36
print("criterionLeft: %.2f"%criterionLeft)
print("criterionRight: %.2f"%criterionRight)
if (criterionLeft < d) and (d < 4 - criterionRight):
    print("Hypothesis about the absence of autocorrelation:\naccepted")
elif ((criterionLeft<d) and (d<criterionRight)) or ((4-criterionLeft<d) and (d<4-criterionRight)) :
    print("The question of accepting the hypothesis remains open")
elif (d>0) and (d<=criterionLeft):
    print("Hypothesis of positive autocorrelation:\naccepted")
elif (4-criterionLeft)<d and (d<4):
    print("Hypothesis of negative autocorrelation:\naccepted")
Y1_15= U[0] +U[1]*15
print("Y1_15: %.2f"%Y1_15)
s2=(sum(eps**2)/(strok-2))
print("s: %.4f"%s2)
sy_15=s2*(1+1/strok+(Y[0]-omega)**2/sum((Y-omega)**2))**0.5
print("sy_15: %.4f"%sy_15)
y1_15=s2*(1/strok+(Y[0]-omega)**2/sum((Y-omega)**2))**0.5
print("y1_15: %.4f"%y1_15)
t=scipy.stats.t.ppf(0.975,strok-2)
print("t: %.4f"%t)
Avg,Ind=np.zeros(2),np.zeros(2)
Ind[0],Ind[1]=Y1_15-t*(sy_15)**0.5,Y1_15+t*(sy_15)**0.5
Avg[0],Avg[1]=Y1_15-t*(y1_15)**0.5,Y1_15+t*(y1_15)**0.5
print(Avg[0]," ",Avg[1])
print(Ind[0], " ", Ind[1])
