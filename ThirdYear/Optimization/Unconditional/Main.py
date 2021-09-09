from tkinter import Tk, messagebox, Entry, Menu, Label, Button
from AllMethods import AllMethods
from matplotlib.pyplot import figure
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
import numpy as np
import sympy as sp


def select(name, choice):
    messagebox.showinfo(name, AllMethods.SelectMethod(choice, fun_entry.get(), a_entry.get(), b_entry.get(), exp_entry.get(), n_entry.get()))


def draw():
    xh = np.arange(float(a_entry.get()), float(b_entry.get()), 0.1)
    yh = np.zeros(int((float(b_entry.get()) - float(a_entry.get())) / 0.1))
    for i in range(int((float(b_entry.get()) - float(a_entry.get())) / 0.1)):
        yh[i] = float(sp.Subs(fun_entry.get(), sp.symbols('x'), xh[i]))
    graph = figure()
    result = graph.add_subplot(1, 1, 1)
    result.grid()
    result.plot(xh, yh)
    canvas = FigureCanvasTkAgg(graph, master=form)
    canvas.get_tk_widget().place(x=255, y=10, width=400, height=300)


form = Tk()
form.title("Unconditional one-dimensional optimization")
form.geometry('700x400')
a_label = Label(form, text="a = ").place(x=10, y=30)
b_label = Label(form, text="b = ").place(x=10, y=60)
exp_label = Label(form, text="eps = ").place(x=10, y=90)
n_label = Label(form, text="n = ").place(x=10, y=120)
fun_label = Label(form, text="f(x) = ").place(x=10, y=150)
a_entry = Entry(form, width=30)
b_entry = Entry(form, width=30)
exp_entry = Entry(form, width=30)
n_entry = Entry(form, width=30)
fun_entry = Entry(form, width=30)
draw_button = Button(form, text="draw", command=draw)
a_entry.place(x=50, y=30)
b_entry.place(x=50, y=60)
exp_entry.place(x=50, y=90)
n_entry.place(x=50, y=120)
fun_entry.place(x=50, y=150)
draw_button.place(x=50, y=185)
a_entry.insert(0, "0")
b_entry.insert(0, "1")
exp_entry.insert(0, "0.001")
n_entry.insert(0, "21")
fun_entry.insert(0, "x**4-1.5*atan(x)")
MainMenu = Menu()
form.config(menu=MainMenu)
MainMenu.add_command(label="Golden Section Method", command=lambda: select("Golden", 1))
MainMenu.add_command(label="Half division method", command=lambda: select("Half", 2))
MainMenu.add_command(label="Tangent method", command=lambda: select("Tangent", 3))
MainMenu.add_command(label="Dichotomies method", command=lambda: select("Dichotomies", 4))
MainMenu.add_command(label="Fibonacci method", command=lambda: select("Fibonacci", 5))
MainMenu.add_command(label="Parabola method", command=lambda: select("Parabola", 6))

import math
from scipy import optimize
def f(x):
    return x**4-1.5*math.atan(x)
print(optimize.minimize(f,-2))
form.mainloop()
