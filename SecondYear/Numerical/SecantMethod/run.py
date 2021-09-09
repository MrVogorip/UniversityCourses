from chord import chord
from sek import sek
from newton import newton
from mod_newton import mod_newton
from comb import comb
a=float(input('a='))
b=float(input('b='))
eps=float(input('eps='))
choice = input('1 - Chords \n2 - Secant \n3 - Newton \n4 - Modified Newton \n5 - Combined \nSelection =')
result = {
  '1': lambda : chord(a,b,eps),
  '2': lambda : sek(a,b,eps),
  '3': lambda : newton(a,b,eps),
  '4': lambda : mod_newton(a,b,eps),
  '5': lambda : comb(a,b,eps)
}[choice]()