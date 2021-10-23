using System;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Google.Apis.Util.Store;
namespace L03
{
    class Program
    {
        private static DriveService _service;
        private static string _token1;
        static void Main(string[] args)
        {

            Initialize();
        }

        static void Initialize()
        {
            string[] scopes = new string[]{
                DriveService.Scope.Drive,
                DriveService.Scope.DriveFile
            };
            var clientId = "146752662571-8d35mpn8bvarhuv5kp54inpb4ujrd6mb.apps.googleusercontent.com";
            var clientSecret = "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";

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                scopes,
                Environment.UserName,
                CancellationToken.None,

                new FileDataStore("Daimto.GoogleDrive.Auth.Store2")
            ).Result;
            _service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

            _token1 = credential.Token.AccessToken;
            Console.Write("Token: " + credential.Token.AccessToken);
            GetMyFiles();
            UpdateFile();
        }

        static void GetMyFiles()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/drive/v3/files?q='root'%20in%20parents");
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + _token1);

            using (var response = request.GetResponse())
            {
                using (Stream data = response.GetResponseStream())
                using (var reader = new StreamReader(data))
                {
                    string text = reader.ReadToEnd();
                    var myData = JObject.Parse(text);
                    foreach (var file in myData["files"])
                    {
                        if (file["mimeType"].ToString() != "application/vnd.google-apps.folder")
                        {
                            Console.WriteLine("File name: " + file["name"]);
                        }
                    }
                }
            }
        }

        static void UpdateFile()
        {
            var body = new Google.Apis.Drive.v3.Data.File();
            body.Name = "random.txt";
            body.MimeType = "text/plain";
            var byteArray = File.ReadAllBytes("random.txt");
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);

            var request = _service.Files.Create(body, stream, "text/plain");
            request.Upload();
            Console.WriteLine(request.ResponseBody);
        }
    }
}
