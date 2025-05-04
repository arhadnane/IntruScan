# 🔐 IntruScan – Smart Wi-Fi Intrusion Detector

## Description
IntruScan is a lightweight, user-friendly application built with C# and .NET MAUI, designed to enhance home Wi-Fi security. It continuously monitors the network and alerts users when an unknown device connects, helping to detect potential intrusions instantly.

## Key Features
- 🔍 **Automatic Detection**: Scans the network using scheduled ARP scans to identify connected devices.
- 📋 **Custom Whitelist**: Allows users to define trusted MAC addresses to avoid false alerts.
- 🚨 **Instant Alerts**: Notifies users via toast or sound notifications when a new, unrecognized device connects.
- 🧠 **Smart Learning**: Learns typical connection patterns, such as usual devices at specific times, to reduce unnecessary alerts.
- 📈 **Connection History**: Logs past network activity for analysis and troubleshooting.
- 🌐 **Cross-Platform Support**: Built with .NET MAUI, supporting Windows and Android (iOS support coming soon).

## Target Users
IntruScan is designed for:
- Home users, roommates, students, and families.
- Anyone who wants to take control of their Wi-Fi network without requiring technical expertise.

## Goal
To empower users to proactively secure their home networks in a simple and effective way.

## Prerequisites
- .NET 9 SDK installed on your system.
- A supported platform for .NET MAUI (e.g., Windows, Android).
- Access to a local Wi-Fi network for scanning.

## Installation
1. Clone the repository:

2. Restore dependencies:
  
3. Build the project:
      
4. Run the application:
 
## Usage
1. Launch the application on your desired platform.
2. Click the "Scan network" button to start scanning the network.
3. View the list of devices, including their IP and MAC addresses.
4. Add trusted devices to the whitelist to avoid unnecessary alerts.
5. Monitor connection history and receive instant alerts for unrecognized devices.

## Project Structure
- **`MauiProgram.cs`**: Configures the .NET MAUI app and its services.
- **`MainPage.xaml`**: Defines the UI for the main page, including the network scanning button and device list.
- **`NetworkScanner.cs`**: Contains the logic for scanning the network, retrieving device information, and detecting intrusions.
- **`App.xaml.cs`**: Handles application lifecycle events.
- **`AppShell.xaml.cs`**: Manages navigation and app structure.

## Contributing
Contributions are welcome! Please fork the repository and submit a pull request with your changes.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.
  
