using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileFunction : ControllerBase
    {

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] string UserSend)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Tạo đường dẫn để lưu file, bao gồm tên userID
            var userDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", UserSend);

            

            // Tạo thư mục nếu chưa tồn tại
            Directory.CreateDirectory(userDirectory);

            // Đường dẫn file
            var filePath = Path.Combine(userDirectory, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath });
        }




        [HttpGet]
        [Route("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            string filePath = Directory.GetCurrentDirectory() +"\\UploadedFiles\\" + fileName;

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }


    }
}
