from matplotlib import mlab
from func import func
def Mod_Ejler(x0,y0,n):
    h = 0.1
    ilist = mlab.frange(0, n-2, 1)
    xlist = [(x0+h*i) for i in ilist]
    ylist = []
    prev = y0
    for x in xlist:
        y = prev + h*func(x+(h/2),y0+(h/2)*func(x,y0))
        prev = y
        ylist.append(prev)
    return xlist,ylist