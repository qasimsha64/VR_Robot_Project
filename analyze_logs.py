import pandas as pd
import numpy as np

# 1. CSV file load karna
file_path = "AdvancedRobotLog.csv"

try:
    df = pd.read_csv(file_path)
    print("=== ROBOT TELEMETRY & BEHAVIORAL ANALYSIS REPORT ===\n")

    # 2. Total Distance Traveled (X aur Z coordinates mein change calculate karke)
    # Euclidean distance formula between consecutive points
    df['DeltaX'] = df['PosX'].diff()
    df['DeltaZ'] = df['PosZ'].diff()
    df['DistanceStep'] = np.sqrt(df['DeltaX']**2 + df['DeltaZ']**2)
    total_distance = df['DistanceStep'].sum()
    print(f"-> Total Distance Traveled: {total_distance:.2f} meters")

    # 3. Safety Stop Frequency (Kitni baar robot blocked hua)
    total_stops = df['IsBlocked'].sum()
    print(f"-> Total Safety Stops Triggered: {total_stops} times")

    # 4. Average Obstacle Distance
    avg_distance = df['ObstacleDistance'].mean()
    print(f"-> Average Obstacle Proximity: {avg_distance:.2f} meters")

    # 5. Session Duration
    duration = df['Timestamp'].max() - df['Timestamp'].min()
    print(f"-> Simulation Session Duration: {duration:.2f} seconds")

    print("\n====================================================")

except FileNotFoundError:
    print(f"Error: Could not find '{file_path}'. Make sure the CSV file is in the folder.")
