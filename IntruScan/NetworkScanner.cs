using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace IntruScan;

public class NetworkScanner
{
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
                results.Add(new DeviceInfo { IP = ip, MAC = mac });
            }
        });

        await Task.WhenAll(tasks);
        return results;
    }

    string GetLocalSubnet()
    {
        string localIp = Dns.GetHostAddresses(Dns.GetHostName())
                            .First(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            .ToString();
        var parts = localIp.Split('.');
        return $"{parts[0]}.{parts[1]}.{parts[2]}.";
    }

    async Task<bool> PingHost(string ip)
    {
        try
        {
            var ping = new Ping();
            var reply = await ping.SendPingAsync(ip, 100);
            return reply.Status == IPStatus.Success;
        }
        catch { return false; }
    }

    string GetMacFromArp(string ip)
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
