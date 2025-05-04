using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Timers;

namespace IntruScan;

public class NetworkScanner
{
    private readonly List<string> _whitelist = new();
    private readonly List<DeviceInfo> _connectionHistory = new();
    private readonly System.Timers.Timer _scanTimer;

    public event Action<DeviceInfo> OnNewDeviceDetected;

    public NetworkScanner()
    {
        // Schedule periodic scans every 5 minutes
        _scanTimer = new System.Timers.Timer(300000); // 5 minutes in milliseconds
        _scanTimer.Elapsed += async (sender, e) => await ScanAsync();
        _scanTimer.AutoReset = true;
    }

    public void StartScanning() => _scanTimer.Start();

    public void StopScanning() => _scanTimer.Stop();

    public void AddToWhitelist(string macAddress)
    {
        if (!_whitelist.Contains(macAddress))
        {
            _whitelist.Add(macAddress);
        }
    }

    public List<DeviceInfo> GetConnectionHistory() => _connectionHistory;

    public async Task<List<DeviceInfo>> ScanAsync()
    {
        string baseIp = GetLocalSubnet();
        var results = new List<DeviceInfo>();

        var tasks = Enumerable.Range(1, 254).Select(async i =>
        {
            string ip = $"{baseIp}{i}";
            if (await PingHost(ip))
            {
                string mac = GetMacFromArp(ip);
                var device = new DeviceInfo { IP = ip, MAC = mac };

                // Check if the device is new and not in the whitelist
                if (!_whitelist.Contains(mac) && !_connectionHistory.Any(d => d.MAC == mac))
                {
                    OnNewDeviceDetected?.Invoke(device); // Trigger alert
                }

                // Add to connection history
                if (!_connectionHistory.Any(d => d.MAC == mac))
                {
                    _connectionHistory.Add(device);
                }

                results.Add(device);
            }
        });

        await Task.WhenAll(tasks);
        return results;
    }

    private string GetLocalSubnet()
    {
        string localIp = Dns.GetHostAddresses(Dns.GetHostName())
                            .First(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            .ToString();
        var parts = localIp.Split('.');
        return $"{parts[0]}.{parts[1]}.{parts[2]}.";
    }

    private async Task<bool> PingHost(string ip)
    {
        try
        {
            var ping = new Ping();
            var reply = await ping.SendPingAsync(ip, 100);
            return reply.Status == IPStatus.Success;
        }
        catch { return false; }
    }

    private string GetMacFromArp(string ip)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "arp",
            Arguments = "-a " + ip,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        var process = Process.Start(psi);
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        var match = Regex.Match(output, @"([0-9a-f]{2}[-:]){5}[0-9a-f]{2}", RegexOptions.IgnoreCase);
        return match.Success ? match.Value : "Unknown";
    }
}

public class DeviceInfo
{
    public string IP { get; set; }
    public string MAC { get; set; }
}
