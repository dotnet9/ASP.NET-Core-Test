using System.IO.Compression;

namespace FileUploadAndDownload.Services;

public class FileService : IFileService
{
    #region Property

    private readonly IWebHostEnvironment _webHostEnvironment;

    #endregion

    #region Constructor

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    #endregion

    #region Upload File

    public void UploadFile(List<IFormFile> files, string subDirectory)
    {
        subDirectory = subDirectory ?? string.Empty;
        var target = Path.Combine(_webHostEnvironment.ContentRootPath, subDirectory);

        Directory.CreateDirectory(target);

        files.ForEach(async file =>
        {
            if (file.Length <= 0) return;
            var filePath = Path.Combine(target, file.FileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
        });
    }

    #endregion

    #region Download File

    public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory)
    {
        var zipName = $"archive-{DateTime.Now:yyyy_MM_dd-HH_mm_ss}.zip";

        var files = Directory.GetFiles(Path.Combine(_webHostEnvironment.ContentRootPath, subDirectory)).ToList();

        using var memoryStream = new MemoryStream();
        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        {
            files.ForEach(file =>
            {
                var theFile = archive.CreateEntry(Path.GetFileName(file));
                using var binaryWriter = new BinaryWriter(theFile.Open());
                binaryWriter.Write(File.ReadAllBytes(file));
            });
        }

        return ("application/zip", memoryStream.ToArray(), zipName);
    }

    #endregion

    #region Size Converter

    public string SizeConverter(long bytes)
    {
        var fileSize = new decimal(bytes);
        var kilobyte = new decimal(1024);
        var megabyte = new decimal(1024 * 1024);
        var gigabyte = new decimal(1024 * 1024 * 1024);

        return fileSize switch
        {
            _ when fileSize < kilobyte => "Less then 1KB",
            _ when fileSize < megabyte =>
                $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB",
            _ when fileSize < gigabyte =>
                $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB",
            _ when fileSize >= gigabyte =>
                $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB",
            _ => "n/a"
        };
    }

    #endregion
}