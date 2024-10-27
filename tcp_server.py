import socket;

#Define the server ip and port
server_IP = '192.168.0.10'
server_port = 5000

#Create the server socket
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

#Bind the socket to the specified IP and port
server_socket.bind((server_IP, server_port))

#Start listening for incoming connections
server_socket.listen(1)
#Accept the client connection
conn, addr = server_socket.accept()
print(f"Connected by {addr}")

#Continuously receive data from the client
while True:
    data = conn.recv(1024)
    if not data:
        break

    received_data = data.decode()
    print(f"Received: {received_data}")

#close the connection and the server socket
conn.close()
server_socket.close()