from F import f,f2
def sek(a,b,eps):
    i=0
    if ((f(a)*f2(a))>0):
        x1=a
        x2=b
    else:
        x1=b
        x2=a
    while ((abs(x1-x2))>eps):
        x=x1-(f(x1)*(x1-x2))/(f(x1)-f(x2))
        x2=x1
        x1=x
        i=i+1
    print ('x=%10.6f'%x,'i=',i)

