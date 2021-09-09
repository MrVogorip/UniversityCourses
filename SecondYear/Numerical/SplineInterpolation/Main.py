from G import G
from tkinter import Tk,Label,Button,Entry,messagebox
import numpy as np
from matplotlib.pyplot import figure
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
def calcul():
    xx=float(textbox.get())
    messagebox.showinfo("Result", "x = "+str(xx)+"\ny = "+ str('%.3f'%G(xx,X1,Y1)))
form = Tk()
form.title("LAB №8")
form.geometry('400x400')
X1 = np.loadtxt("D:\X.txt")
Y1 = np.loadtxt("D:\y.txt")
textbox = Entry(form)
textbox.place(x=100,y=320)
label = Label(form,text = "x \n"+str(X1[0])+" - "+str(X1[len(X1)-1]))
label.place(x=10,y=300)
draw_button = Button(form, text="Result", command=calcul)
draw_button.place(x=20,y=350)
graph = figure()
result = graph.add_subplot(1,1,1)
result.grid()
result.plot(X1,Y1)
result.scatter(X1,Y1)
canvas = FigureCanvasTkAgg(graph, master=form)
canvas.get_tk_widget().grid()
form.mainloop()