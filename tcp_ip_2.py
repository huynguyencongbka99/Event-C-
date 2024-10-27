import socket

def start_server():
    # Define server IP and port
    host = '192.168.0.10'  # Use the actual IP of the server machine
    port = 5000            # Same port the C# client will connect to

    # Create a TCP socket
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    
    # Bind the socket to the host and port
    server_socket.bind((host, port))
    
    # Start listening for incoming connections
    server_socket.listen(5)
    print(f"Server listening on {host}:{port}")

    # Accept a connection from the client
    client_socket, client_address = server_socket.accept()
    print(f"Connection from {client_address}")

    # Open the file to store data
    with open("received_data.txt", "a") as file:  # Append mode to keep adding data
        # Keep transferring data continuously
        while True:
            # Receive data from the client
            data = client_socket.recv(1024).decode('utf-8')
            if not data:
                break  # Break if no more data is received
            print(f"Received from client: {data}")

            # Store the received data in a txt file
            file.write(data + "\n")

            # Send a continuous response back to the client
            response = "Server received: " + data  # Acknowledge receipt
            client_socket.send(response.encode('utf-8'))

    # Close the client connection
    client_socket.close()

if __name__ == "__main__":
    start_server()
