using System.ComponentModel.DataAnnotations;
using FileUploadAndDownload.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadAndDownload.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost(nameof(Upload))]
    public IActionResult Upload([Required] List<IFormFile> formFiles, [Required] string subDirectory)
    {
        try
        {
            _fileService.UploadFile(formFiles, subDirectory);

            return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet(nameof(Download))]
    public IActionResult Download([Required] string subDirectory)
    {
        try
        {
            var (fileType, archiveData, archiveName) = _fileService.DownloadFiles(subDirectory);

            return File(archiveData, fileType, archiveName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}