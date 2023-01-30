namespace Perfect.FileService.Application.Files.Interfaces
{
    public interface IFileRepository
    {
        Task AddFileAsync(string FileName, long Length, Stream Content, CancellationToken cancellationToken);
    }
}
