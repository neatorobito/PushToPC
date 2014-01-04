import socket
import time
import webbrowser

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

server_address = ("", 0) #Current machine's IP and random open port.
print  "Starting server..."
sock.bind(server_address)
print socket.getaddrinfo()
time.sleep(10)
sock.listen(1)
print sock.getsockname()[1] #Get the Port.

while True:
    print  "Waiting for connection.."
    connection, client_address = sock.accept()
    try:
        print 'Connection from', client_address

        # Receive the data in small chunks and retransmit it
        while True:
            data = connection.recv(4096)
            print "Opening " + data
            if data:
                print 'No more data from', client_address
                webbrowser.open(data, new=2, autoraise=True)
                connection.send("Pushed Successfully")
                break
            else:
                print "Error"
                break
        
    finally:
        # Clean up the connection
        connection.close()


#http://social.msdn.microsoft.com/forums/en-us/wpdevelop/thread/00e61ffc-645c-4d29-bb51-112fd75f9814

