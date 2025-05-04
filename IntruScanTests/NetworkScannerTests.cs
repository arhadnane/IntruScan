using Xunit;
using IntruScan;

namespace IntrusScanTests
{
    public class NetworkScannerTests
    {
        [Fact]
        public async Task ScanAsync_ShouldReturnDevices()
        {
            // Arrange
            var scanner = new NetworkScanner();

            // Act
            var devices = await scanner.ScanAsync();

            // Assert
            Assert.NotNull(devices);
            Assert.True(devices.Count > 0);
        }

        [Fact]
        public void GetLocalSubnet_ShouldReturnSubnet()
        {
            // Arrange
            var scanner = new NetworkScanner();

            // Act
            var subnet = scanner.GetLocalSubnet();

            // Assert
            Assert.NotNull(subnet);
            Assert.Matches(@"\d+\.\d+\.\d+\.", subnet);
        }

        [Fact]
        public async Task PingHost_ShouldReturnTrueForValidIp()
        {
            // Arrange
            var scanner = new NetworkScanner();
            string validIp = "192.168.1.1"; // Remplace par une IP valide de ton réseau

            // Act
            var result = await scanner.PingHost(validIp);

            // Assert
            Assert.True(result);
        }
    }
}
