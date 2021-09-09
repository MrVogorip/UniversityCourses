from tkinter import Tk,messagebox,Entry,Menu,Label
from Cheb import Cheb
from Gayss import Gayss
from R import R
def Cheb_click():
    messagebox.showinfo("messagebox","%.5f"%Cheb(float(txtA.get()),float(txtB.get()),int(txtN.get())))
def Gayss_click():
    messagebox.showinfo("messagebox","%.5f"%Gayss(float(txtA.get()),float(txtB.get()),int(txtN.get())))
def R_click():
    messagebox.showinfo("messagebox","%.5f"%R(float(txtA.get()),float(txtB.get()),int(txtN.get())))
    
form = Tk()
form.title("LAB №12")
form.geometry('250x100')
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
menu = Menu(tearoff=0)
menu.add_command(label="Cheb_click", command = lambda: Cheb_click())
menu.add_command(label="Gayss_click", command = lambda: Gayss_click())
menu.add_command(label="R_click", command = lambda: R_click())
mainmenu.add_cascade(label="menu", menu=menu)
form.mainloop()