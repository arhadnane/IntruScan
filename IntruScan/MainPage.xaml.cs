using System.Collections.ObjectModel;

namespace IntruScan
{
    public partial class MainPage : ContentPage
    {
        NetworkScanner scanner = new();
        ObservableCollection<DeviceInfo> devices = new();
        public MainPage()
        {
            InitializeComponent();
            DeviceListView.ItemsSource = devices;

            // Subscribe to alerts for new devices
            scanner.OnNewDeviceDetected += device =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("New Device Detected", $"IP: {device.IP}\nMAC: {device.MAC}", "OK");
                });
            };
        }

        private async void OnScanClicked(object sender, EventArgs e)
        {
            devices.Clear();
            var results = await scanner.ScanAsync();
            foreach (var d in results)
                devices.Add(d);
        }

    }

}
