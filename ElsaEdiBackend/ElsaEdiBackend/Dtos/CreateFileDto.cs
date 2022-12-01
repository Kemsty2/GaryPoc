namespace ElsaEdiBackend.Dtos
{
    public class CreateFileDto
    {
        public string FileName { get; set; }
        public string Content { get; set; }
        public string FolderPath { get; set; }
    }
}
