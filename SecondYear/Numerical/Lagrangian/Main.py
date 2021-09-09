from Lagrange import lagrange
import numpy as np
from tkinter import Tk,Label,Button
from matplotlib.pyplot import figure
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
def calcul():
    xres,yres = lagrange(A,B)
    resultX,resultY = '',''
    for elem in xres:
        resultX+=("%10.3f"%elem) +' '
    for elem in yres:
        resultY+=("%10.3f"%elem) +' '
    lbl = Label(form , text = resultX + "\n" +resultY)
    lbl.place(x=50,y=300)

form = Tk()
form.title("LAB №6 (lagrange)")
form.geometry('400x400')
A = np.loadtxt("D:\A.txt")
B = np.loadtxt("D:\B.txt")
xres,yres = lagrange(A,B)
draw_button = Button(form, text="Output of the result", command=calcul)
draw_button.place(x=20,y=350)
graph = figure()
result = graph.add_subplot(1,1,1)
result.grid()
result.plot(xres,yres)
result.scatter(xres,yres)
canvas = FigureCanvasTkAgg(graph, master=form)
canvas.get_tk_widget().grid()
form.mainloop()