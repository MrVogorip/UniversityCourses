from tkinter import Tk, messagebox, Entry, Menu, Label, Button
from mpl_toolkits.mplot3d import Axes3D
import numpy as np
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
from matplotlib import cm
import matplotlib.pyplot as plt
from AllMethods import SelectMethod

def select(choice):
        messagebox.showinfo("Result",
        SelectMethod(int(choice), 
                     float(x_entry.get()),
                     float(y_entry.get()),
                     float(r_entry.get()),
                     float(c_entry.get()),
                     float(eps_entry.get())))
                                 
def draw():
    f1 = lambda x1, x2: 2*x1**2+5*x2**2-8*x1
    f2 = lambda x1, x2: 4*x1+5*x2-12
    f3 = lambda x1, x2: 8*x1+3*x2-10
    f4 = lambda x1, x2: -x1
    f5 = lambda x1, x2: -x2
    fig = plt.figure(figsize = (10, 10))  
    ax = fig.add_subplot(1, 1, 1, projection = '3d')
    ax.grid()   
    xval = np.linspace(-2, 3, 100)
    yval = np.linspace(-2, 3, 100)   
    x, y = np.meshgrid(xval, yval)   
    z1 = f1(x, y)
    z2 = f2(x, y)
    z3 = f3(x, y)
    z4 = f4(x, y)
    z5 = f5(x, y)
    surf = ax.plot_surface(x, y, z1, rstride = 5,cstride = 5,cmap = cm.coolwarm)
    surf = ax.plot_surface(x, y, z2, rstride = 5,cstride = 5,cmap = cm.coolwarm)
    surf = ax.plot_surface(x, y, z3, rstride = 5,cstride = 5,cmap = cm.coolwarm)
    surf = ax.plot_surface(x, y, z4, rstride = 5,cstride = 5,cmap = cm.coolwarm)
    surf = ax.plot_surface(x, y, z5, rstride = 5,cstride = 5,cmap = cm.coolwarm)
    canvas = FigureCanvasTkAgg(fig, master=form)
    canvas.get_tk_widget().place(x=255, y=10, width=500, height=400)

form = Tk()
form.geometry('800x400')
x_label = Label(text = "x0")
y_label = Label(text = "y0")
r_label = Label(text = "r")
c_label = Label(text = "c")
eps_label = Label(text = "eps")
x_label.grid(row=0, column=0, sticky="w")
y_label.grid(row=1, column=0, sticky="w")
r_label.grid(row=2, column=0, sticky="w")
c_label.grid(row=3, column=0, sticky="w")
eps_label.grid(row=4, column=0, sticky="w")
x_entry = Entry()
y_entry = Entry()
r_entry = Entry()
c_entry = Entry()
eps_entry = Entry()
x_entry.grid(row=0,column=1, padx=5, pady=5)
y_entry.grid(row=1,column=1, padx=5, pady=5)
r_entry.grid(row=2,column=1, padx=5, pady=5)
c_entry.grid(row=3,column=1, padx=5, pady=5)
eps_entry.grid(row=4,column=1, padx=5, pady=5)
x_entry.insert(0, "1")
y_entry.insert(0, "1")
eps_entry.insert(0, "0.001")
MainMenu = Menu()
form.config(menu=MainMenu)
MainMenu.add_command(label="Penalty function method (Newton)", command=lambda: select(1))
MainMenu.add_command(label="Barrier function method (Newton)", command=lambda: select(2))
MainMenu.add_command(label="Method of penalty functions (Conjugate)", command=lambda: select(3))
MainMenu.add_command(label="Method of barrier functions (Conjugate)", command=lambda: select(4))
MainMenu.add_command(label="Built-in penalty functions", command=lambda: select(5))
MainMenu.add_command(label="Built-in barrier functions", command=lambda: select(6))
draw_button = Button(form, text="draw", command=draw)
draw_button.place(x=50, y=250)
form.mainloop()