from math import fabs
import sympy as sp


def fib(n):
    if n == 1 or n == 2:
        return 1
    return fib(n - 1) + fib(n - 2)


def dif(f, x, k):
    return sp.Subs(sp.diff(f, sp.symbols('x'), k), sp.symbols('x'), x)


fi = (pow(5, 0.5) + 1) / 2


class AllMethods:

    def _GoldenRatio(fx, a, b, eps):
        i = 0
        while fabs(b - a) >= eps:

            x1 = b - (b - a) / fi
            x2 = a + (b - a) / fi

            y1 = float(sp.Subs(fx, sp.symbols('x'), x1))
            y2 = float(sp.Subs(fx, sp.symbols('x'), x2))
            print("%.3f" %a+ '\t'+ "%.3f" %b+ '\t'+ "%.3f" %x1+ '\t'+ "%.3f" %x2+ '\t'+ "%.3f" %y1+ '\t'+"%.3f" %y2+ '\t'+ "%.3f" %((b - a) / 2))
            if y1 >= y2:
                a = x1
            else:
                b = x2
            i += 1
            if i >= 10000:
                break
            x = (a + b) / 2
        x = (a + b) / 2
        return x, i, float(sp.Subs(fx, sp.symbols('x'), x))

    def _DeleniyaPopolam(fx, a, b, eps):
        i = 0
        while fabs(b - a) >= 2 * eps:
            x = (a + b) / 2
            y1 = float(sp.Subs(fx, sp.symbols('x'), (x - eps / 2)))
            y2 = float(sp.Subs(fx, sp.symbols('x'), (x + eps / 2)))
            print("%.3f" %a+ '\t'+ "%.3f" %b+ '\t'+ "%.3f" %(x - eps / 2)+ '\t'+ "%.3f" %(x + eps / 2)+ '\t'+ "%.3f" %y1+ '\t'+"%.3f" %y2+ '\t')
            if y1 < y2:
                b = ((a + b) / 2) + eps / 2
            else:
                a = ((a + b) / 2) - eps / 2
            x = (a + b) / 2
            i += 1
            if i >= 10000:
                break
        x = (a + b) / 2
        return x, i, float(sp.Subs(fx, sp.symbols('x'), x))

    def _Tangents(fx, a, b, eps):
        i = 0
        c = (a + b) / 2
        y1 = float(sp.Subs(fx, sp.symbols('x'), a))
        y2 = float(sp.Subs(fx, sp.symbols('x'), b))
        z1 = float(dif(fx, a, 1))
        z2 = float(dif(fx, b, 1))
        while fabs(b - a) >= 2 * eps:
            c = ((b * z2 - a * z1) - (y2 - y1)) / (z2 - z1)
            ny = float(sp.Subs(fx, sp.symbols('x'), c))
            nz = float(dif(fx, c, 1))
            print("%.3f" %a+ '\t'+ "%.3f" %b+ '\t'+ "%.3f" %c+ '\t'+ "%.3f" %ny+ '\t'+ "%.3f" %nz)
            if nz < 0:
                a = c
                y1 = ny
                z1 = nz
            if nz > 0:
                b = c
                y2 = ny
                z2 = nz
            if nz == 0:
                break
            i += 1
            if i >= 10000:
                break
            x = c
        x = c
        return x, i, float(sp.Subs(fx, sp.symbols('x'), x))

    def _Dichotomy(fx, a, b, eps):
        i = 0
        while fabs(b - a) >= 2 * eps:
            nx = (b + a) / 2
            p = float(dif(fx, nx, 1))
            print("%.3f" %a+ '\t'+ "%.3f" %b+ '\t'+ "%.3f" %nx+ '\t'+ "%.3f" %p)
            if p < 0:
                a = nx
            if p > 0:
                b = nx
            if p == 0:
                break
            i += 1
            if i >= 10000:
                break
            x = (a + b) / 2
        x = (a + b) / 2
        return x, i, float(sp.Subs(fx, sp.symbols('x'), x))

    def _Fibonacci(fx, a, b, n):
        i = 0
        L = (b - a) / fib(n)
        while fabs(b - a) >= L:
            x1 = a + ((b - a) * fib(n - 2)) / fib(n)
            x2 = a + ((b - a) * fib(n - 1)) / fib(n)
            print("%.3f" %a+ '\t'+ "%.3f" %b+ '\t'+ "%.3f" %x1+ '\t'+ "%.3f" %x2+ '\t'+ "%.3f" %float(sp.Subs(fx, sp.symbols('x'), x1))+ '\t'+"%.3f" %float(sp.Subs(fx, sp.symbols('x'), x2)))
            if float(sp.Subs(fx, sp.symbols('x'), x1)) > float(sp.Subs(fx, sp.symbols('x'), x2)):
                a = x1
            if float(sp.Subs(fx, sp.symbols('x'), x1)) < float(sp.Subs(fx, sp.symbols('x'), x2)):
                b = x2
            if float(sp.Subs(fx, sp.symbols('x'), x1)) == float(sp.Subs(fx, sp.symbols('x'), x2)):
                a = x1
                b = x2
            i += 1
            if i >= 10000:
                break
            x = (x1 + x2) / 2
        return x, i, float(sp.Subs(fx, sp.symbols('x'), x))

    def _Parabola(fx, a, b, eps):
        i = 0
        c = (a + b) / 2
        ya = float(sp.Subs(fx, sp.symbols('x'), a))
        yc = float(sp.Subs(fx, sp.symbols('x'), c))
        yb = float(sp.Subs(fx, sp.symbols('x'), b))
        while fabs(b - a) >= eps:
            t = c + (((b - c) ** 2) * (ya - yc) - ((c - a) ** 2) * (yb - yc)) / (
                    2 * ((b - c) * (ya - yc) + (c - a) * (yb - yc)))
            if t == c:
                x = (a + c) / 2
            else:
                x = t
            print("%.3f" %a+ '\t'+ "%.3f" %b+ '\t'+ "%.3f" %t+ '\t'+ "%.3f" %ya+ '\t'+ "%.3f" %yb)
            if x < c:
                if float(sp.Subs(fx, sp.symbols('x'), x)) < yc:
                    b = c
                    c = x
                    yb = yc
                    yc = float(sp.Subs(fx, sp.symbols('x'), x))
                else:
                    if float(sp.Subs(fx, sp.symbols('x'), x)) > yc:
                        a = x
                        ya = float(sp.Subs(fx, sp.symbols('x'), x))
                    if float(sp.Subs(fx, sp.symbols('x'), x)) == yc:
                        a = x
                        b = c
                        c = (x + c) / 2
                        ya = float(sp.Subs(fx, sp.symbols('x'), x))
                        yb = yc
                        yc = float(sp.Subs(fx, sp.symbols('x'), c))
            if x > c:
                if float(sp.Subs(fx, sp.symbols('x'), x)) < yc:
                    a = c
                    c = x
                    ya = yc
                    yc = float(sp.Subs(fx, sp.symbols('x'), x))
                else:
                    if float(sp.Subs(fx, sp.symbols('x'), x)) > yc:
                        b = x
                        yb = float(sp.Subs(fx, sp.symbols('x'), x))
                    if float(sp.Subs(fx, sp.symbols('x'), x)) == yc:
                        a = c
                        b = x
                        c = (x + c) / 2
                        ya = yc
                        yb = float(sp.Subs(fx, sp.symbols('x'), x))
                        yc = float(sp.Subs(fx, sp.symbols('x'), x))
            i += 1
            if i >= 10000:
                break
            x = c
        return x, i, float(sp.Subs(fx, sp.symbols('x'), x))

    def SelectMethod(choice, fx, a, b, eps, n):
        a = float(a)
        b = float(b)
        eps = float(eps)
        n = float(n)
        x, i, y = {
            1: lambda: AllMethods._GoldenRatio(fx, a, b, eps),
            2: lambda: AllMethods._DeleniyaPopolam(fx, a, b, eps),
            3: lambda: AllMethods._Tangents(fx, a, b, eps),
            4: lambda: AllMethods._Dichotomy(fx, a, b, eps),
            5: lambda: AllMethods._Fibonacci(fx, a, b, n),
            6: lambda: AllMethods._Parabola(fx, a, b, eps),
        }[choice]()
        return "x = {argX}\ny = {argY}\n:{iter}".format(argX="%.3f" % x, argY="%.3f" % y, iter=i)
