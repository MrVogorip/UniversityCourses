from tkinter import Tk,Label,Button
from Nuton2 import Nuton2
from Nuton1 import Nuton1
from Iter import Iter
import numpy as np
from matplotlib.pyplot import figure
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg

def calcul():
    N1 = [0. for x in range(len(Xh))]
    N2 = [0. for x in range(len(Xh))]
    for i in range(len(Xh)):
        N1[i] = Nuton1(Xh[i],X1,Y1,10)
    for i in range(len(Xh)):
        N2[i] = Nuton2(Xh[i],X1,Y1,10)
    resN1,resN2 = '',''
    for elem in N1:
        resN1+=("%10.5f"%elem) +' '
    for elem in N2:
        resN2+=("%10.5f"%elem) +' '
    lbl = Label(form , text = resN1 + "\n\n" + resN2)
    lbl.place(x=80,y=300)
    lbl1 = Label(form , text = "Newton 1 =")
    lbl1.place(x=20,y=300)
    lbl2 = Label(form , text = "Newton 2 =")
    lbl2.place(x=20,y=330)
    lbl3 = Label(form , text = "Reverse interpolation \n x^3 + 2x - 30\n Answer =  ")
    lbl3.place(x=20,y=380)
    lbl4 = Label(form , text = str(Iter(0,0.00001)))
    lbl4.place(x=130,y=410)
    
form = Tk()
form.title("LAB №7")
form.geometry('400x500')
X1 = np.loadtxt("D:\X1.txt")
Y1 = np.loadtxt("D:\Y1.txt")
Xh = np.loadtxt("D:\Xh.txt")
draw_button = Button(form, text="Answer", command=calcul)
draw_button.place(x=20,y=450)
graph = figure()
result = graph.add_subplot(1,1,1)
result.grid()
result.plot(X1,Y1)
result.scatter(X1,Y1)
canvas = FigureCanvasTkAgg(graph, master=form)
canvas.get_tk_widget().grid()
form.mainloop()