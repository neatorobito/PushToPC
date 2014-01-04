from Tkinter import *
import threading
import ttk
import ip
import socket
import time
import webbrowser
import tkMessageBox
import psutil
import os
import sys

app = Tk()
app.title("PushToPC Server")
app.iconbitmap("push.ico")
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_address = ("", 0) #Current machine's IP and random open port.
sock.bind(server_address)

def server():
    start.config(state=DISABLED)
    sock.listen(1)
    #print  "Starting server..."
    #print ip.get_lan_ip()
    #print sock.getsockname()[1] #Get the Port.
    started.set("Starting server...")
    time.sleep(1)
    while True:
        #print  "Waiting for connection.."
        started.set("Waiting for connection..")
        connection, client_address = sock.accept()
        try:
            #print 'Connection from', client_address
            # Receive the data in small chunks and retransmit it
            while True:
                data = connection.recv(4096)
                #print "Opening " + data
                started.set("Opening " + data)
                time.sleep(1)
                if data:
                    #print 'No more data from', client_address
                    http = "http://"
                    if http in data:
                        pass
                    else:
                        data = "http://" + data
                    webbrowser.open(data, new=2, autoraise=True)
                    connection.send("Pushed Successfully")
                    started.set("Pushed Successfully")
                    time.sleep(1)
                    break
                else:
                    #print "Error"
                    started.set("Error")
                    break
            
        finally:
            # Clean up the connection
            connection.close()


proc = threading.Thread(name="PushToPC Server", target=server)


def about():
    topper1 = Toplevel()
    topper1.iconbitmap("push.ico")
    topper1.title("About..")
    aboutapp = Label(topper1 , text="PushToPC, by Peak Ditigal Innovations")
    aboutapp.pack()
    version = Label(topper1, text="Version: 1.0 stable")
    version.pack()
    legal = Label(topper1, text="Copyright (C) 2013")
    legal.pack()
    return

def hide():
    if threading.Thread.isAlive(proc):
        tkMessageBox.showinfo("Info", "The server will run in the background. \n If you need to reopen it for some reason, open it from the Start menu.")
    else:
        pass
    app.destroy()
    return



menu = Menu(app)
app.config(menu=menu)
filemenu= Menu(menu, tearoff=FALSE)
helpmenu= Menu(menu, tearoff=FALSE)
menu.add_cascade(label="File", menu=filemenu)
menu.add_cascade(label="Help", menu=helpmenu)
filemenu.add_command(label="Close Server and Exit", command=sys.exit)
helpmenu.add_command(label="About...", command=about)
started = StringVar()
infolbl = ttk.Label(app, text="IP Address:")
iplbl = ttk.Label(app, text=ip.get_lan_ip())
infolbl3 = ttk.Label(app, textvariable=started)
infolbl2 = ttk.Label(app, text = "Port: " + str(sock.getsockname()[1]))
start = ttk.Button(app, text="Start Server", command=proc.start)
infolbl.grid(column=1, row=0,padx=3)
infolbl2.grid(column=1, row=1)
infolbl3.grid(column=1, row=2, padx=3, pady=3)
iplbl.grid(column=2, row=0)
start.grid(column=4, row=1, padx=5, pady=5)
app.protocol('WM_DELETE_WINDOW', hide)
app.mainloop()
