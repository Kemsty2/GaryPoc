using System.Text;

namespace ElsaEdiBackend.Activities.CreateFile
{
    public class CreateFileActivityPayload
    {
        public string? FileName { get; set; }
        public string? FolderPath { get; set; }
        public string? Content { get; set; }

        public byte[] GetBytes()
        {
            return string.IsNullOrEmpty(Content) ? Encoding.UTF8.GetBytes("") : Encoding.UTF8.GetBytes(Content);
        }

        public string GetFilePath()
        {
            var fileName = FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Guid.NewGuid().ToString();
            }

            if (!string.IsNullOrEmpty(FolderPath))
            {
                return Path.Combine(FolderPath, fileName);
            }
            return fileName;
        }
    }
}
