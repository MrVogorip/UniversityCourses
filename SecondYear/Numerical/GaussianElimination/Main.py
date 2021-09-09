from Gausse import Gausse
from Det import Det
from Obr import Obr
from tkinter import Tk,Label,Button
def show():
    result = 'result\n'
    for r in AB:
        result +=str(r)+"\n"
    lbl = Label(form , text = result)
    lbl.place(x=200,y=20)
def gauss():
    result = 'result\n' + str(Gausse(AB))
    lbl = Label(form , text = result)
    lbl.place(x=200,y=120)
def det():
    result = 'result\n' + Det(AB)
    lbl = Label(form , text = result)
    lbl.place(x=200,y=220)
def obr():
    result = 'result\n' + Obr(A)
    lbl = Label(form , text = result)
    lbl.place(x=100,y=320)    
AB = []
A=[]
with open("D:\AB.txt") as f:
    for line in f:
        AB.append([float(x) for x in line.split()])
with open("D:\A.txt") as f:
    for line in f:
        A.append([float(x) for x in line.split()])  
form = Tk()
form.title("LAB №4")
form.geometry('500x450')
draw_button1 = Button(form, text="Matrix output", command=show)
draw_button1.place(x=20,y=20)
draw_button2 = Button(form, text="gauss", command=gauss)
draw_button2.place(x=20,y=120)
draw_button3 = Button(form, text="det", command=det)
draw_button3.place(x=20,y=220)
draw_button4 = Button(form, text="obr", command=obr)
draw_button4.place(x=20,y=320)
form.mainloop()