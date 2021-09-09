from F import f,f2
def chord(a,b,eps):
    i=0
    if f(a)*f2(a)<0:
        x=a
    else: 
        x=b
    if x==a:
        fix=b
    else:
        fix=a
    prev_x=fix
    while abs(prev_x-x)>eps:
        prev_x=x
        x=x-f(x)*(fix-x)/(f(fix)-f(x))
        i=i+1
    xx=(x+prev_x)/2
    print ('x=%10.6f'%xx,'i='%i)
