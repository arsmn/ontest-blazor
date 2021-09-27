using System;

namespace OnTest.Blazor.Transport.Shared.Models
{
    public class Session
    {
        public long Id { get; set; }
        public DateTime IssuedAt { get; set; }
        public Properties Properties { get; set; }
        public string ClientInfo => $"{Properties?.UAInfo?.ClientName} - {Properties?.UAInfo?.ClientVersion}";
        public string OSInfo => $"{Properties?.UAInfo?.OSName} - {Properties?.UAInfo?.OSVersion}";
        public string IPInfo => $"{Properties?.IPLocation?.IP} - {Properties?.IPLocation?.City}, {Properties?.IPLocation?.CountryName}";
    }

    public class Properties
    {
        public IPLocation IPLocation { get; set; }
        public UAInfo UAInfo { get; set; }
    }

    public class IPLocation
    {
        public string IP { get; set; }
        public string CountryName { get; set; }
        public string StateProv { get; set; }
        public string City { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
        public string ISP { get; set; }
        public string CountryFlag { get; set; }
    }

    public class UAInfo
    {
        public string UA { get; set; }
        public string OSName { get; set; }
        public string OSVersion { get; set; }
        public string ClientName { get; set; }
        public string ClientVersion { get; set; }
    }
}