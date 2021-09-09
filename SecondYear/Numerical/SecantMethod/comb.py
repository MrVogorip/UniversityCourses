from F import f,f1,f2
def comb(a,b,eps):
    i=0
    if (f(a)*f2(a))>0:
        x=a
        y=b
    else:
        x=b
        y=a
    while ((abs(y-x))>(2*eps)):
        y=y-(f(y)*(y-x))/(f(y)-f(x))
        x=x-(f(x)/f1(x))
        i=i+1
    print ('x=%10.6f'%x,'i=',i)

