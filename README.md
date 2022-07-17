# WVCore
.NET Widevine Api

**I'm not the original author of the project, I just made some optimizations**

Email me if there are copyright issues

# Usage
```C#
var ClientIdFile = new FileInfo(@"C:\Desktop\device_client_id_blob");
var PrivateKeyFile = new FileInfo(@"C:\Desktop\device_private_key");

var pssh = "AAAAW3Bzc2gAAAAA7e+LqXnWSs6jyCfc1R0h7QAAADsIARIQ62dqu8s0Xpa7z2FmMPGj2hoNd2lkZXZpbmVfdGVzdCIQZmtqM2xqYVNkZmFsa3IzaioCSEQyAA==";
var licenseUrl = "https://cwip-shaka-proxy.appspot.com/no_auth";
var resp1 = HTTPUtil.Post(licenseUrl, headers, new byte[] { 0x08, 0x04 });
var certDataB64 = Convert.ToBase64String(resp1);
var cdm = new WVApi(clientIdFile, privateKeyFile);
var challenge = cdm.GetChallenge(pssh, certDataB64, false, false);
var resp2 = HTTPUtil.Post(licenseUrl, headers, challenge);
var licenseB64 = Convert.ToBase64String(resp2);
cdm.ProvideLicense(licenseB64);
List<ContentKey> keys = cdm.GetKeys();
foreach (var key in keys)
{
    Console.WriteLine(key);
}
```
