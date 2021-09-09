from func import f
def dihotomia(a,b, eps):
    if f(a)*f(b)>0:
        return (0, 0)
    else:
        c=0
        n=0
        while (abs(b-a)>2*eps):
            c= (a+b)/2
            if f(a)*f(c)<0:
                b=c
            else:
                a=c
            n+=1
        return (c, n)




