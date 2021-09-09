from math import sqrt
import numpy as np
def Jakobi(A, B, eps):
    n = len(A)
    x = [.0 for i in range(n)]
    i=0
    converge = False
    while not converge:
        x_new = np.copy(x)
        for i in range(n):
            s1 = sum(A[i][j] * x[j] for j in range(i))
            s2 = sum(A[i][j] * x[j] for j in range(i + 1, n))
            x_new[i] = (B[i] - s1 - s2) / A[i][i]
            i+=1
        converge = sqrt(sum((x_new[i] - x[i]) ** 2 for i in range(n))) <= eps
        x = x_new
    return x,i