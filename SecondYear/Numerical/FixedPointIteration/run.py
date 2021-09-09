from functions import inetration
from numpy import arange,sin
from tkinter import Tk,Label,Entry,Button
from matplotlib.pyplot import figure
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
def calcul():
    a = float(textbox_1.get())
    b = float(textbox_2.get())
    lbl = Label(form , text = inetration(a,b,1e-5))
    lbl.place(x=300,y=300)

form = Tk()
form.title("LAB №3 (Inetration)")
form.geometry('400x450')
draw_button = Button(form, text="Calculate", command=calcul)
draw_button.place(x=20,y=400)
textbox_1 = Entry(form)
textbox_1.place(x=100,y=350)
textbox_2 = Entry(form)
textbox_2.place(x=100,y=300)
lbl_1 = Label(form, text="b =")
lbl_1.place(x=50,y=350)
lbl_2 = Label(form, text="a =")
lbl_2.place(x=50,y=300)
x = arange(-1.0,1.0,0.1)
y = sin(4.8*x)*5.67-4.5*x
graph = figure()
result = graph.add_subplot(1,1,1)
result.grid()
result.plot(x,y)
canvas = FigureCanvasTkAgg(graph, master=form)
canvas.get_tk_widget().grid()
form.mainloop()
