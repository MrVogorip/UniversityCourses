from tkinter import Tk,messagebox,Entry,Menu,Label
from Ejler import Ejler
from Mod_Ejler import Mod_Ejler
from RungeKut import RungeKut
def EjlerForm():
    x,y=Ejler(float(txtA.get()),float(txtB.get()),int(txtN.get()))
    res="x=\ty=\n"
    for i in range(len(x)):
        res+=(str("%.5f"%x[i])+"\t"+str("%.5f"%y[i])+"\n")
    messagebox.showinfo("Result",res)
def Mod_EjlerForm():
    x,y=Mod_Ejler(float(txtA.get()),float(txtB.get()),int(txtN.get()))
    res="x=\ty=\n"
    for i in range(len(x)):
        res+=(str("%.5f"%x[i])+"\t"+str("%.5f"%y[i])+"\n")
    messagebox.showinfo("Result",res)
def RungeKutForm():
    x,y=RungeKut(float(txtA.get()),float(txtB.get()),int(txtN.get()))
    res="x=\ty=\n"
    for i in range(len(x)):
        res+=(str("%.5f"%x[i])+"\t"+str("%.5f"%y[i])+"\n")
    messagebox.showinfo("Result",res)
form = Tk()
form.title("LAB №14")
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
menu.add_command(label="EjlerForm", command = lambda: EjlerForm())
menu.add_command(label="Mod_EjlerForm", command = lambda: Mod_EjlerForm())
menu.add_command(label="RungeKutForm", command = lambda: RungeKutForm())
mainmenu.add_cascade(label="menu", menu=menu)
form.mainloop()