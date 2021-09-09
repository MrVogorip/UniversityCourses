from tkinter import Tk,messagebox,Entry,Menu,Label
from Solve_Voltera import Solve_Voltera
from Solve_Fredholm import Solve_Fredholm
def Voltera():
    x,y=Solve_Voltera(float(txtA.get()),float(txtB.get()),int(txtN.get()),int(txtL.get()))
    res=""
    for i in range(len(x)):
        res+=(str("%.5f"%x[i])+" ")
    res+="\n"
    for i in range(len(y)):
        res+=(str("%.5f"%y[i])+" ")
    messagebox.showinfo("Result",res)
def Fredholm():
    x,y=Solve_Fredholm(float(txtA.get()),float(txtB.get()),int(txtN.get()),int(txtL.get()))
    res=""
    for i in range(len(x)):
        res+=(str("%.5f"%x[i])+" ")
    res+="\n"
    for i in range(len(y)):
        res+=(str("%.5f"%y[i])+" ")
    messagebox.showinfo("Result",res)

form = Tk()
form.title("LAB №12")
form.geometry('250x150')
mainmenu = Menu()
form.config(menu=mainmenu)
lblA = Label(form,text = "a").place(x=10,y=10)
txtA = Entry(form)
txtA.place(x=100,y=10)
lblB = Label(form,text = "b").place(x=10,y=40)
txtB = Entry(form)
txtB.place(x=100,y=40)
lblN = Label(form,text = "n").place(x=10,y=70)
txtN = Entry(form)
txtN.place(x=100,y=70)
lblL = Label(form,text = "Lambda").place(x=10,y=100)
txtL = Entry(form)
txtL.place(x=100,y=100)
menu = Menu(tearoff=0)
menu.add_command(label="Voltera", command = lambda: Voltera())
menu.add_command(label="Fredholm", command = lambda: Fredholm())
mainmenu.add_cascade(label="menu", menu=menu)
form.mainloop()