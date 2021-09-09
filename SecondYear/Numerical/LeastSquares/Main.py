from Aproxim import Aproxim
from MNK import MNK
from tkinter import Tk,Button,messagebox
import numpy as np
from matplotlib.pyplot import figure
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
def calcul():
    res1,res2 = '',''
    for elem in A1:
        res1+=("%10.3f"%elem) +' '
    for elem in A2:
        res2+=("%10.3f"%elem) +' '
    messagebox.showinfo("Result", "1 degree\n"+res1+"\n2 degree\n"+res2)
    
    
form = Tk()
form.title("LAB №9")
form.geometry('400x330')
X1 = np.loadtxt("D:\X.txt")
Y1 = np.loadtxt("D:\Y.txt")
draw_button = Button(form, text="Output of odds", command=calcul)
draw_button.place(x=20,y=295)
A1=MNK(X1,Y1,1,6)
A2=MNK(X1,Y1,2,6)
xh = np.arange(-3.0,5.0,0.1)
F1 = Aproxim(xh,A1,1)
F2 = Aproxim(xh,A2,2)
graph = figure()
result = graph.add_subplot(1,1,1)
result.grid()
result.plot(xh,F1)
result.plot(xh,F2)
result.plot(X1,Y1,'ro')
canvas = FigureCanvasTkAgg(graph, master=form)
canvas.get_tk_widget().grid()
form.mainloop()