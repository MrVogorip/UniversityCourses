import pylab
from matplotlib import mlab
from matplotlib import pyplot
from scipy.misc import derivative
def f(x):
    return x*x*x+2*x-30
def f1(x):
    return derivative(f,x,1)
def f2(x): 
    return derivative(f1,x,1)

xmin = -5.0
xmax = 5.0
dx = 0.1
xlist = mlab.frange (xmin, xmax, dx)
ylist = pyplot.ylim(-10, 10)
ylist = [f(x) for x in xlist]
pylab.plot (xlist, ylist)
pylab.show()