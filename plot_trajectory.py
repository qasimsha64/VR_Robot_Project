import pandas as pd
import matplotlib.pyplot as plt

# 1. Unity ki CSV file load karna
file_path = "AdvancedRobotLog.csv"

try:
    df = pd.read_csv(file_path)
    print("CSV File successfully loaded!")
    print(df.head()) # First 5 rows check karne ke liye

    # 2. Plotting setup
    plt.figure(figsize=(10, 6))

    # Path plotting (X vs Z - top-down view of Unity 3D world)
    plt.plot(df['PosX'], df['PosZ'], label='Robot Path', color='blue', linestyle='-', marker='o', markersize=3)

    # Safety Stop Points highlight karna (Jahan IsBlocked == True ho)
    blocked_points = df[df['IsBlocked'] == True]
    if not blocked_points.empty:
        plt.scatter(blocked_points['PosX'], blocked_points['PosZ'], color='red', label='Safety Stop Triggered', zorder=5, s=50)

    # Graph details
    plt.title('Robot Trajectory & Obstacle Safety Map (Sim-to-Real)', fontsize=14)
    plt.xlabel('Position X (Meters)', fontsize=12)
    plt.ylabel('Position Z (Meters)', fontsize=12)
    plt.grid(True, linestyle='--', alpha=0.6)
    plt.legend()
    plt.axis('equal') # Equal scaling for X and Z axes

    # Graph show karna
    plt.show()

except FileNotFoundError:
    print(f"Error: Could not find '{file_path}'. Make sure the CSV file is in the same folder.")
