from tkinter import Tk, messagebox, Entry, Menu, Label, Button
from mpl_toolkits.mplot3d import Axes3D
from AllMethods import SelectMethod
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
import numpy as np
from matplotlib import cm
import matplotlib.pyplot as plt


def select(choice):
        messagebox.showinfo("Result",
                        SelectMethod(choice,x1_entry.get(),x2_entry.get(),eps_entry.get(),
                                  a_entry.get(),b_entry.get(),d1_entry.get(),d2_entry.get(),h_entry.get()))
        
def draw():
    f = lambda x1, x2: 4*(x1)**2 + 7*(x2)**2 +4*x1*x2+6*5**0.5*x1-12*5**0.5*x2+51
    fig = plt.figure(figsize = (10, 10))
    ax = fig.add_subplot(1, 1, 1, projection = '3d')
    ax.grid()
    xval = np.linspace(-4, 4, 100)
    yval = np.linspace(-4, 4, 100)
    x, y = np.meshgrid(xval, yval)
    z = f(x, y)
    surf = ax.plot_surface(
            x, y, z, 
            rstride = 2,
            cstride = 2,
            cmap = cm.viridis)
    canvas = FigureCanvasTkAgg(fig, master=form)
    canvas.get_tk_widget().place(x=255, y=10, width=400, height=300)
    
form = Tk()
form.geometry('700x400')
x1_label = Label(text = "x1 ")
x2_label = Label(text = "x2")
eps_label = Label(text = "eps")
a_label = Label(text = "a")
b_label = Label(text = "b")
d1_label = Label(text = "d1")
d2_label = Label(text = "d2")
h_label = Label(text = "h")

draw_button = Button(form, text="draw", command=draw)
draw_button.place(x=50, y=250)


x1_label.grid(row=0, column=0, sticky="w")
x2_label.grid(row=1, column=0, sticky="w")
eps_label.grid(row=2, column=0, sticky="w")
a_label.grid(row=3, column=0, sticky="w")
b_label.grid(row=4, column=0, sticky="w")
d1_label.grid(row=5, column=0, sticky="w")
d2_label.grid(row=6, column=0, sticky="w")
h_label.grid(row=7, column=0, sticky="w")
x1_entry = Entry()
x2_entry = Entry()
eps_entry = Entry()
a_entry = Entry()
b_entry = Entry()
d1_entry = Entry()
d2_entry = Entry()
h_entry = Entry()
x1_entry.grid(row=0,column=1, padx=5, pady=5)
x2_entry.grid(row=1,column=1, padx=5, pady=5)
eps_entry.grid(row=2,column=1, padx=5, pady=5)
a_entry.grid(row=3,column=1, padx=5, pady=5)
b_entry.grid(row=4,column=1, padx=5, pady=5)
d1_entry.grid(row=5,column=1, padx=5, pady=5)
d2_entry.grid(row=6,column=1, padx=5, pady=5)
h_entry.grid(row=7,column=1, padx=5, pady=5)
x1_entry.insert(0, "0")
x2_entry.insert(0, "0")
eps_entry.insert(0, "0.0002")
a_entry.insert(0, "0.1")
b_entry.insert(0, "18")
d1_entry.insert(0, "0.1")
d2_entry.insert(0, "3")
h_entry.insert(0, "0.1")

MainMenu = Menu()
form.config(menu=MainMenu)
MainMenu.add_command(label="Gradient descent with constant steps", command=lambda: select(1))
MainMenu.add_command(label="Gradient descent with step division", command=lambda: select(2))
MainMenu.add_command(label="Gradient method of fastest descent", command=lambda: select(3))
MainMenu.add_command(label="Gully method", command=lambda: select(4))
MainMenu.add_command(label="Newton's method", command=lambda: select(5))
MainMenu.add_command(label="Newton's Method (Modified)", command=lambda: select(6))
MainMenu.add_command(label="Conjugate method", command=lambda: select(7))
MainMenu.add_command(label="Quasi-Newton method", command=lambda: select(6))

from scipy import optimize
def fun(x):
    return 4*(x[0])**2 + 7*(x[1])**2 +4*x[0]*x[1]+6*5**0.5*x[0]-12*5**0.5*x[1]+51
print(optimize.minimize(fun, (0, 0)))

form.mainloop()