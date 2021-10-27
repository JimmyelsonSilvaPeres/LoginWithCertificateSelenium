using Microsoft.Win32;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace LoginWithCertified
{
    class Program
    {
        static void Main(string[] args)
        {
            //A list of path and password of some cetificate in you pc.
            List<CertInfo> certifiedList = new List<CertInfo>()
            {
                new CertInfo(){Path = @"Firth Certicate.pfx", Password = "123"},
                new CertInfo(){Path = "Second", Password = "123"},
                new CertInfo(){Path = "And Carry on", Password = "123"}
            };
            string UrlBase = "https://notacarioca.rio.gov.br/senhaweb/login.aspx";
            //In this case will be the same url, but sometimes the web sites redirect to other url and then use other url to send the certificate.
            string UrlThatsCertificateWillBeSend = "https://notacarioca.rio.gov.br/senhaweb/login.aspx";

            certifiedList.ForEach(cert =>
            {
                //The method that get the certificade from your pc.
                X509Certificate2 certObj = GetCertificate(cert.Path, cert.Password);

                //Config the json in AutoSelectCertificateForUrls. Obs.: The Url need to be the same that the website send your cert to.
                SetCertificateInChromeRegistry(certObj, UrlThatsCertificateWillBeSend);

                IWebDriver driver = new ChromeDriver();

                driver.Navigate().GoToUrl(UrlBase);
                driver.FindElement(By.Id("ctl00_cphCabMenu_imgLoginICP")).Click();

                //Just to see the magic happen
                Thread.Sleep(9000);

            });
        }
        public static void SetCertificateInChromeRegistry(X509Certificate2 certificado, string sendCertUrl)
        {
            
            string jsonConfig = GetJsonConfigChromeLoginCertificate(certificado, sendCertUrl);
            RegistryKey key;
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Google\Chrome\AutoSelectCertificateForUrls", true).SetValue("1", jsonConfig);
        }
        public static string GetJsonConfigChromeLoginCertificate(X509Certificate2 certificado, string urlAcessoCertificado)
        {
            var camposIssuer = certificado.Issuer.Split(",").ToList();
            var camposSubject = certificado.Subject.Split(",").ToList();
            var json = new JsonInfoCertified();
            json.pattern = urlAcessoCertificado;
            json.filter.SUBJECT.CN = camposSubject.FirstOrDefault(x => x.Contains("CN=")).Replace("CN=", string.Empty).Trim();
            json.filter.SUBJECT.O = camposSubject.FirstOrDefault(x => x.Contains("O=")).Replace("O=", string.Empty).Trim();
            json.filter.SUBJECT.OU = camposSubject.FirstOrDefault(x => x.Contains("OU=")).Replace("OU=", string.Empty).Trim();
            json.filter.ISSUER.CN = camposIssuer.FirstOrDefault(x => x.Contains("CN=")).Replace("CN=", string.Empty).Trim();
            json.filter.ISSUER.O = camposIssuer.FirstOrDefault(x => x.Contains("O=")).Replace("O=", string.Empty).Trim();
            json.filter.ISSUER.C = camposIssuer.FirstOrDefault(x => x.Contains("C=")).Replace("C=", string.Empty).Trim();
            return JsonConvert.SerializeObject(json);
        }

        public static X509Certificate2 GetCertificate(string caminhoArquivoCertificado, string senhaCertificado)
        {
            byte[] pfxData = null;

            try
            {
                pfxData = File.ReadAllBytes(caminhoArquivoCertificado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {
                return new X509Certificate2(pfxData, senhaCertificado, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
