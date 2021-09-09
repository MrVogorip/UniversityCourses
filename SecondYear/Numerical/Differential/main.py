from tkinter import Tk,messagebox,Entry,Menu,Label
from Milna import Milna
from Adams import Adams
from RungeKut import RungeKut
def AdamsForm():
    X,Y=RungeKut(float(txtA.get()),float(txtB.get()),int(txtN.get()))
    y=Adams(X,Y,int(txtN.get()))
    res="y=\n"
    for i in range(len(y)):
        res+=str("%.5f"%y[i])+"\n"
    messagebox.showinfo("AdamsForm",res)
def MilnaForm():
    X,Y=RungeKut(float(txtA.get()),float(txtB.get()),int(txtN.get()))
    y=Milna(X,Y,int(txtN.get()))
    res="y=\n"
    for i in range(len(y)):
        res+=str("%.5f"%y[i])+"\n"
    messagebox.showinfo("MilnaForm",res)
form = Tk()
form.title("LAB №15")
form.geometry('250x110')
mainmenu = Menu()
form.config(menu=mainmenu)
lblA = Label(form,text = "X0").place(x=10,y=10)
txtA = Entry(form)
txtA.place(x=100,y=10)
lblB = Label(form,text = "Y0").place(x=10,y=40)
txtB = Entry(form)
txtB.place(x=100,y=40)
lblN = Label(form,text = "n").place(x=10,y=70)
txtN = Entry(form)
txtN.place(x=100,y=70)
menu = Menu(tearoff=0)
menu.add_command(label="AdamsForm", command = lambda: AdamsForm())
menu.add_command(label="MilnaForm", command = lambda: MilnaForm())
mainmenu.add_cascade(label="menu", menu=menu)
form.mainloop()