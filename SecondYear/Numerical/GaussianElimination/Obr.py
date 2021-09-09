from Gausse import Gausse
import numpy
def Obr(A):
    n=len(A)
    I,J=numpy.identity(n),numpy.zeros((n, n))
    for i in range(n):
        J[:][i]=gauss(numpy.c_[A,I[:][i]])
    result =''
    for row in J:
        for elem in row:
            result+=str(elem) +' '
        result+='\n'
    return result   