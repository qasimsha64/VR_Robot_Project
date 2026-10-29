import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
import os

# Aapki CSV file ka exact naam
csv_file_path = "AdvancedRobotLog.csv"

if not os.path.exists(csv_file_path):
    print(f"❌ Error: '{csv_file_path}' file nahi mili!")
else:
    # 1. Load CSV data using pandas
    df = pd.read_csv(csv_file_path)
    print(f"📊 Successfully loaded {len(df)} rows from '{csv_file_path}'.")

    # 2. Extract coordinates and blocked status
    x_coords = df['PosX']
    z_coords = df['PosZ']
    blocked_mask = df['IsBlocked'] == True

    # 3. Plotting Path Trajectory & Heatmap
    plt.figure(figsize=(10, 8))
    
    # KDE Density Heatmap (Robot ka concentration area)
    sns.kdeplot(
        x=x_coords, 
        y=z_coords, 
        cmap="Reds", 
        fill=True, 
        thresh=0.05, 
        levels=100, 
        alpha=0.6
    )
    
    # Raw Trajectory Line (Robot ka mukammal safar)
    plt.plot(x_coords, z_coords, color="blue", linewidth=1.5, linestyle="--", label="Robot Path")

    # Highlight Stopping / Blocked Points (Jahan robot obstacle ki wajah se ruka)
    plt.scatter(
        df.loc[blocked_mask, 'PosX'], 
        df.loc[blocked_mask, 'PosZ'], 
        color="darkred", 
        s=30, 
        zorder=5, 
        label="Stop / Blocked Points"
    )

    # Graph Styling & Labels
    plt.title("Rebot Navigation Trajectory & Stop Points Heatmap", fontsize=14, fontweight='bold', pad=15)
    plt.xlabel("Arena X Coordinate (PosX)", fontsize=12)
    plt.ylabel("Arena Z Coordinate (PosZ)", fontsize=12)
    plt.legend(loc="upper right")
    plt.grid(True, linestyle=":", alpha=0.6)

    # Show the plot
    plt.tight_layout()
    plt.show()
