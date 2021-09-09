import numpy as np 
import scipy.stats
A = np.loadtxt("D:\A.txt")
Allrange =np.mean(A)
A=A.transpose()
summ=0
block1=[]
block2=[]
block3=[]
block4=[]
for i in range(3):
    for j in range(3):
        summ+=A[i][j]
    block1.append(round(summ/3,2))
    summ=0
    for j in range(3,6):
        summ+=A[i][j]  
    block2.append(round(summ/3,2))
    summ=0
    for j in range(6,9):
        summ+=A[i][j]
    block3.append(round(summ/3,2))
    summ=0
    for j in range(9,12):
        summ+=A[i][j]
    block4.append(round(summ/3,2))
    summ=0
AvarageRows = [round(np.mean(block1),2),round(np.mean(block2),2),round(np.mean(block3),2),round(np.mean(block4),2)]
AvarageCols = [round(np.mean(A[0]),2),round(np.mean(A[1]),2),round(np.mean(A[2]),2)]
Yi_x2 =[round((np.mean(block1)-Allrange)**2,2),round((np.mean(block2)-Allrange)**2,2),round((np.mean(block3)-Allrange)**2,2),round((np.mean(block4)-Allrange)**2,2)]
Zk_x2 =[round((np.mean(A[0])-Allrange)**2,2),round((np.mean(A[1])-Allrange)**2,2),round((np.mean(A[2])-Allrange)**2,2)]

Xijk_Xjk = [[],[],[]]
for j in range(3):
    for i in range(3):
        Xijk_Xjk[j].append(round((A[j][i] -block1[j])**2,2))  
    for i in range(3,6):
        Xijk_Xjk[j].append(round((A[j][i] -block2[j])**2,2))
    for i in range(6,9):
        Xijk_Xjk[j].append(round((A[j][i] -block3[j])**2,2))
    for i in range(9,12):
        Xijk_Xjk[j].append(round((A[j][i] -block4[j])**2,2))        
Xij_Zi_YiX = [[],[],[],[]]
index = 0
for j in range(3):
    Xij_Zi_YiX[0].append(round((block1[j]-AvarageCols[j]-AvarageRows[index]+Allrange)**2,2))
    index+=1
    Xij_Zi_YiX[1].append(round((block2[j]-AvarageCols[j]-AvarageRows[index]+Allrange)**2,2))
    index+=1
    Xij_Zi_YiX[2].append(round((block3[j]-AvarageCols[j]-AvarageRows[index]+Allrange)**2,2))
    index+=1
    Xij_Zi_YiX[3].append(round((block4[j]-AvarageCols[j]-AvarageRows[index]+Allrange)**2,2))
    index =0
n=3
p=3
q=4
SumOtlonFactA = round(n*p*np.sum(Zk_x2),2)
SumOtlonFactB = round(n*q*np.sum(Yi_x2),2)
SumOtlonFactAB = round(n*np.sum(Xij_Zi_YiX),2)
SumOtlonOstat = round(np.sum(Xijk_Xjk),2)
ChesloSvobA = n-1
ChesloSvobB = q-1
ChesloSvobAB = ChesloSvobA*ChesloSvobB
ChesloSvobOstat = len(A)*len(A[0])-4*len(block1)
DisperA=SumOtlonFactA/ChesloSvobA
DisperB=SumOtlonFactB/ChesloSvobB
DisperAB=SumOtlonFactAB/ChesloSvobAB
DisperOstat=SumOtlonOstat/ChesloSvobOstat
FAeksp=DisperA/DisperOstat
FBeksp=DisperB/DisperOstat
FABeksp=DisperAB/DisperOstat
alpha=0.05
FAkr=scipy.stats.f.ppf(1-alpha, ChesloSvobA,ChesloSvobOstat)
FBkr=scipy.stats.f.ppf(1-alpha, ChesloSvobB,ChesloSvobOstat)
FABkr=scipy.stats.f.ppf(1-alpha, ChesloSvobAB,ChesloSvobOstat)

print(A)
print(Allrange)
print(block1,block2,block3,block4)
print(AvarageRows)
print(Yi_x2)
print(,AvarageCols)
print(Zk_x2)
print('(Xijk-Xjk)^2\n',Xijk_Xjk[0],'\n',Xijk_Xjk[1],'\n',Xijk_Xjk[2])
print('(Xij-Zj-Yi+X)^2\n',Xij_Zi_YiX[0],'\n',Xij_Zi_YiX[1],'\n',Xij_Zi_YiX[2],'\n',Xij_Zi_YiX[3])
print(SumOtlonFactA )
print(SumOtlonFactB )
print(SumOtlonFactAB )
print(SumOtlonOstat )
print(DisperA)
print(DisperB)
print(DisperAB)
print(DisperOstat)
print(FAeksp<FAkr)
print(FBeksp<FBkr)
print(FABeksp<FABkr)

