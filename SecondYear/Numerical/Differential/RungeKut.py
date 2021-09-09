from matplotlib import mlab
from func import func
def RungeKut(x0,y0,n):
    h = 0.1
    ilist = mlab.frange(0, n-2, 1)
    xlist = [(x0+h*i) for i in ilist]
    ylist = []
    prev = y0
    for x in xlist:
        k1=h*func(x,y0)
        k2=h*func(x+(h/2),y0+(k1/2))
        k3=h*func(x+(h/2),y0+(k2/2))
        k4=h*func(x+h,y0+k3)
        y = prev + ((k1+2*k2+2*k3+k4)/6)
        prev = y
        ylist.append(prev)
    return xlist,ylist