from operator import mul
from functools import reduce
def R(a,b,n):
    x=(b+a)/2
    return  ((b-a)**(2*n+1)*(reduce(mul, range(1, n)))**4*((4.2*x*x-8.1)/(x*x*x+3.8))**(2*n))/((reduce(mul, range(1, 2*n)))**3)*(2*n+1)