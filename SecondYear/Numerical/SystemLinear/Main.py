import numpy as np
from Jakobi import Jakobi
from Zeudel import Zeudel
from tkinter import Tk,Button,messagebox
def ZeudelP():
    Z,it=Zeudel(A,B,eps)
    temp = ''
    for i in range(len(Z)):
        temp +=str('%.3f'%Z[i])+'\n'
    result = temp +str(it)
    messagebox.showinfo("Title", result)
def JakobiP():
    J,it=Jakobi(A,B,eps)
    temp = ''
    for i in range(len(J)):
        temp +=str('%.3f'%J[i])+'\n'
    result = temp +str(it)
    messagebox.showinfo("Title", result)
    
A = np.loadtxt("D:\Ar.txt")
B = np.loadtxt("D:\Br.txt")
eps=0.001
form = Tk()
form.title("LAB №5")
form.geometry('150x150')
draw_button1 = Button(form, text="Jakobi", command=JakobiP)
draw_button1.place(x=20,y=20)
draw_button2 = Button(form, text="Zeudel", command=ZeudelP)
draw_button2.place(x=20,y=70)
form.mainloop()