namespace Perfect.FileService.Domain.Models
{
    public class FileEntity
    {
        public string FileName { get; set; }
        public long Length { get; set; }
        public byte[] Content { get; set; }

        public FileEntity(string fileName, long length, byte[] content)
        {
            FileName = fileName;
            Length = length;
            Content = content;
        }
    }
}
