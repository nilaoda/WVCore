using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVCore.Widevine;

namespace WVCore
{
    public class WVApi
    {
        Dictionary<string, CDMDevice> Devices { get; set; } = new Dictionary<string, CDMDevice>();
        public string SessionId { get; set; }

        public WVApi(FileInfo clientIdBlobFile, FileInfo privateKeyFile, FileInfo? vmpFile = null)
        {
            if (!clientIdBlobFile.Exists)
            {
                throw new FileNotFoundException("clientIdBlobFile not found: " + clientIdBlobFile.FullName);
            }
            if (!privateKeyFile.Exists)
            {
                throw new FileNotFoundException("privateKeyFile not found: " + privateKeyFile.FullName);
            }
            var clientId = File.ReadAllBytes(clientIdBlobFile.FullName);
            var privateKey = File.ReadAllBytes(privateKeyFile.FullName);
            if (vmpFile != null)
            {
                if (!vmpFile.Exists)
                {
                    throw new FileNotFoundException("vmpFile not found: " + vmpFile.FullName);
                }
                var vmp = File.ReadAllBytes(vmpFile.FullName);
                Devices.Add("default", new CDMDevice("default", clientId, privateKey, vmp));
            }
            else
            {
                Devices.Add("default", new CDMDevice("default", clientId, privateKey));
            }
            CDM.Devices = Devices;
        }

        public WVApi(string deviceName)
        {
            Devices.Add(deviceName, new CDMDevice(deviceName));
            CDM.Devices = Devices;
        }

        public WVApi(byte[] clientIdBlobBytes, byte[] privateKeyBytes, byte[] vmpBytes = null)
        {
            Devices.Add("default", new CDMDevice("default", clientIdBlobBytes, privateKeyBytes, vmpBytes));
            CDM.Devices = Devices;
        }

        public byte[] GetChallenge(string initDataB64, string certDataB64, bool offline = false, bool raw = false)
        {
            SessionId = CDM.OpenSession(initDataB64, Devices.Keys.First(), offline, raw);
            CDM.SetServiceCertificate(SessionId, Convert.FromBase64String(certDataB64));
            return CDM.GetLicenseRequest(SessionId);
        }

        public bool ProvideLicense(string licenseB64)
        {
            CDM.ProvideLicense(SessionId, Convert.FromBase64String(licenseB64));
            return true;
        }

        public List<ContentKey> GetKeys()
        {
            return CDM.GetKeys(SessionId);
        }
    }
}
