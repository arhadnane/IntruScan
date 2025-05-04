# IntruScan

IntruScan is a .NET MAUI application designed to scan and identify devices on a local network. It provides a user-friendly interface to display device information such as IP addresses and MAC addresses.

## Features
- **Network Scanning**: Detect devices connected to the local network.
- **Device Information**: Display IP and MAC addresses of discovered devices.
- **Cross-Platform Support**: Runs on Android, iOS, macOS, and Windows.

## Prerequisites
- .NET 9 SDK installed on your system.
- A supported platform for .NET MAUI (e.g., Android, iOS, macOS, or Windows).
- Access to a local network for scanning.

## Installation
1. Clone the repository:

2. Restore dependencies:
  
3. Build the project:
      
4. Run the application:
 
## Usage
1. Launch the application on your desired platform.
2. Click the "Scan network" button to start scanning the network.
3. View the list of devices, including their IP and MAC addresses.

## Project Structure
- **`MauiProgram.cs`**: Configures the .NET MAUI app and its services.
- **`MainPage.xaml`**: Defines the UI for the main page, including the network scanning button and device list.
- **`NetworkScanner.cs`**: Contains the logic for scanning the network and retrieving device information.
- **`App.xaml.cs`**: Handles application lifecycle events.
- **`AppShell.xaml.cs`**: Manages navigation and app structure.
 
## Contributing
Contributions are welcome! Please fork the repository and submit a pull request with your changes.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.
  
