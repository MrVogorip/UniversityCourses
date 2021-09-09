from F import f,f1,f2
def mod_newton(a,b,eps):
    i=0
    if (f(a)*f2(a))>0:
        x=a
        prev_x=b
    else:
        x=b
        prev_x=a
    fix=x
    while (abs(prev_x-x)>eps):
        prev_x=x
        x=x-(f(x))/(f1(fix))
        i=i+1
    print ('x=%10.6f'%x,'i=',i)
