import socket

# Unity IP aur Port
UDP_IP = "127.0.0.1"
UDP_PORT = 5000

print("=========================================")
print("   PYTHON UDP 3-COMMAND CONTROLLER       ")
print("=========================================")
print(" [1] -> MOVE_FORWARD")
print(" [2] -> MOVE_BACKWARD")
print(" [0] -> STOP")
print(" Type 'exit' to quit.")
print("=========================================")

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

command_map = {
    "1": "MOVE_FORWARD",
    "2": "MOVE_BACKWARD",
    "0": "STOP"
}

while True:
    try:
        choice = input("\nEnter digit (1, 2, 0): ").strip()
        
        if choice.lower() == "exit":
            print("Exiting controller...")
            break
            
        if choice in command_map:
            cmd_string = command_map[choice]
            sock.sendto(cmd_string.encode('utf-8'), (UDP_IP, UDP_PORT))
            print(f"-> Sent to Unity: [{cmd_string}]")
        else:
            print("❌ Invalid input! Enter 1, 2, or 0.")
            
    except KeyboardInterrupt:
        print("\nController stopped.")
        break

sock.close()
