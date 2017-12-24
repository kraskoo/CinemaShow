namespace CinemaShow.Services
{
    using System;
    using System.IO;
    using System.Web;

    public class ServerContentManager
    {
        private readonly string mainServerDirectory;
        private readonly string publicDirectory;

        public ServerContentManager()
        {
            this.mainServerDirectory = HttpRuntime.AppDomainAppPath;
            this.publicDirectory = Path.Combine(this.mainServerDirectory, "public");
        }

        public string UploadFile(HttpPostedFileBase file, DateTime date)
        {
            var path = Path.Combine(this.GetPathByContentType(file.ContentType), this.GetDirectoryByDate(date));
            this.CreateDirectoryIfNotExist(path);
            var filename = file.FileName;
            var nameAndExtension = filename.Split('.');
            var extension = nameAndExtension[nameAndExtension.Length - 1];
            var currentDateTime = DateTime.Now;
            var newFilename = $"{currentDateTime.ToLongTimeString().Replace(":", string.Empty)}{currentDateTime.Millisecond}.{extension}";
            var filePath = Path.Combine(path, newFilename).Replace("\\", "/");
            file.SaveAs(filePath);
            return filePath;
        }

        private void CreateDirectoryIfNotExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private string GetDirectoryByDate(DateTime date)
        {
            var day = $"{date.Day}";
            var month = $"{date.Month}";
            var year = $"{date.Year}";
            day = day.Length == 1 ? $"0{day}" : day;
            month = month.Length == 1 ? $"0{month}" : month;
            year = year.Substring(2);
            return $"{day}{month}{year}";
        }

        private string GetPathByContentType(string contentType)
        {
            var partsOfContentType = contentType.Split('/');
            var comparer = partsOfContentType[0];
            if (comparer == "application")
            {
                comparer = partsOfContentType[1];
            }

            switch (comparer)
            {
                case "audio":
                    return Path.Combine(this.publicDirectory, "audio");
                case "image":
                    return Path.Combine(this.publicDirectory, "images");
                case "pdf":
                    return Path.Combine(this.publicDirectory, "pdf");
                default:
                    return string.Empty;
            }
        }
    }
}