from tkinter import *
from tkinter.ttk import *
from tkinter import messagebox
import threading
import ip
import socket
import time
import webbrowser


errorOccured = False

app = Tk()
app.title("Push Server")

try:
    app.iconbitmap("push.ico") #Look for the icon.
except:
    app.withdraw()  #If it isn't there, show an error and close the program.
    messagebox.showerror("Oops...", "It looks like an error occured because we can't find a file. Try reinstalling the application.")
    errorOccured = True

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
                data = str(data.decode())
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
                    connection.send("Pushed Successfully".encode())
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


proc = threading.Thread(name="Push Listening Server", target=server, daemon=True)


def about():
    topper1 = Toplevel()
    topper1.iconbitmap("push.ico")
    topper1.title("About...")
    aboutapp = Label(topper1 , font=("Open Sans Light", 24), text="Push by Peak")
    aboutapp.pack(pady=30, padx=50)
    version = Label(topper1, text="Version: 1.3 stable")
    version.pack()
    legal = Label(topper1, text="Peak Software Copyright (C) 2014")
    legal.pack()
    return


menu = Menu(app)
app.config(menu=menu)
filemenu= Menu(menu, tearoff=FALSE)
helpmenu= Menu(menu, tearoff=FALSE)
menu.add_cascade(label="File", menu=filemenu)
menu.add_cascade(label="Help", menu=helpmenu)
filemenu.add_command(label="Close Server and Exit", command=app.destroy)
helpmenu.add_command(label="About...", command=about)
started = StringVar()
infolbl = Label(app, text="IP Address:")
iplbl = Label(app, text=ip.get_lan_ip())
infolbl3 = Label(app, textvariable=started)
infolbl2 = Label(app, text = "Port: " + str(sock.getsockname()[1]))
start = Button(app, text="Start Server", command=proc.start)
infolbl.grid(column=1, row=0,padx=3)
infolbl2.grid(column=1, row=1)
infolbl3.grid(column=1, row=2, padx=3, pady=3)
iplbl.grid(column=2, row=0)
start.grid(column=4, row=1, padx=5, pady=5)


#Let everything initialize and then check if the icon error has occured.
if errorOccured == True:
    app.destroy()


app.mainloop()
