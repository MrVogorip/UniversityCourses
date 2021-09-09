from Integral import Integral
def Aposter_Error(choice,f,a,b,n):
    result = {
            1: lambda : abs(Integral(1,f,a,b,n)-Integral(1,f,a,b,int(0.5*n)))/15,
            2: lambda : abs(Integral(2,f,a,b,n)-Integral(2,f,a,b,int(0.5*n)))/3,
            3: lambda : abs(Integral(3,f,a,b,n)-Integral(3,f,a,b,int(0.5*n))),
            4: lambda : abs(Integral(4,f,a,b,n)-Integral(4,f,a,b,int(0.5*n))),
            5: lambda : abs(Integral(5,f,a,b,n)-Integral(5,f,a,b,int(0.5*n)))/3
            }[choice]()
    return result