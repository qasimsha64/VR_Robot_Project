import pandas as pd

# Load CSV log file
df = pd.read_csv('AdvancedRobotLog.csv')

total_time = df['Timestamp'].max() - df['Timestamp'].min()
total_frames = len(df)
blocked_frames = df['IsBlocked'].sum()
blocked_percentage = (blocked_frames / total_frames) * 100
min_obstacle_distance = df['ObstacleDistance'].min()

print("--- REBOT PERFORMANCE METRICS ---")
print(f"Total Session Duration: {total_time:.2f} seconds")
print(f"Total Logged Frames: {total_frames}")
print(f"Blocked / Stop Events: {blocked_frames} ({blocked_percentage:.2f}% of time)")
print(f"Minimum Obstacle Distance Recorded: {min_obstacle_distance:.2f} meters")
