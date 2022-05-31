using System.Collections.Generic;

namespace WVCore.Widevine
{
    internal class Session
    {
        public byte[] SessionId { get; set; }
        public byte[]? InitData { get; set; }
        public WidevineCencHeader? ParsedInitData { get; set; }
        public bool Offline { get; set; }
        public CDMDevice Device { get; set; }
        public byte[] SessionKey { get; set; }
        public DerivedKeys DerivedKeys { get; set; }
        public byte[] LicenseRequest { get; set; }
        public SignedLicense License { get; set; }
        public SignedDeviceCertificate ServiceCertificate { get; set; }
        public bool PrivacyMode { get; set; }
        public List<ContentKey> ContentKeys { get; set; } = new List<ContentKey>();

        public Session(byte[] sessionId, WidevineCencHeader? parsedInitData, byte[]? initData, CDMDevice device, bool offline)
        {
            SessionId = sessionId;
            InitData = initData;
            ParsedInitData = parsedInitData;
            Offline = offline;
            Device = device;
        }
    }
}