using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System;
using HR_API.APP_Start;
using Newtonsoft.Json;
using System.Data;
using static System.Collections.Specialized.BitVector32;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HR_API.Controllers
{
    public class ScrapController : Controller
    {
        [HttpPost]
        [Route("SynchronousData")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] string stringImageScrap, string UserIDSyn)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            try
            {
                
                var ImageScrap = JsonConvert.DeserializeObject<ImageScrap>(stringImageScrap);

                if(ImageScrap == null)
                    return BadRequest("No file uploaded.");

                // Tạo đường dẫn để lưu file, bao gồm tên userID
                var userDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ScrapData", ImageScrap.BA, ImageScrap.SanctionID);
                // Tạo thư mục nếu chưa tồn tại
                Directory.CreateDirectory(userDirectory);

                // Đường dẫn file
                var filePath = Path.Combine(userDirectory, file.FileName.Split('/').Last());

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }


                string nameimage = ImageScrap.ImagePath.Split("/").Last();

                var pathsaveimg = Path.Combine(userDirectory, nameimage);


                DbconnectScrap.excutenonquerry("CreateImageScrap",
                    System.Data.CommandType.StoredProcedure,
                    ImageScrap.SanctionID,
                    int.Parse(ImageScrap.Stt),
                    ImageScrap.BA,
                    pathsaveimg,
                    "2025-04-21",
                    ImageScrap.UserID,
                    UserIDSyn
                    );
                return Ok(ImageScrap);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }
        [HttpPost]
        [Route("LoginScrap")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> LoginScrap([FromBody] LoginRequest request)
        {
            try
            {
                DataTable User = DbconnectScrap.StoreFillDS(
                    "LoginScrap",
                    System.Data.CommandType.StoredProcedure,
                    request.UserID?.Trim(),
                    request.PassWord
                );

                if (User.Rows.Count == 0)
                    return BadRequest("Login Fail");

                User UserRespone = new User()
                {
                    UserID = User.Rows[0]["UserID"].ToString(),
                    UserPassWord = User.Rows[0]["UserPassWord"].ToString(),
                    Department = User.Rows[0]["Department"].ToString(),
                    Section = User.Rows[0]["Section"].ToString(),
                };

                return Ok(UserRespone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("LoadScrap")]
        [DisableRequestSizeLimit]
        [HttpPost]
        public async Task<IActionResult> LoadScrap([FromBody] LoadDataRequest request)
        {
            try
            {
                DataTable Scraps = DbconnectScrap.StoreFillDS("LoadDataScrap",
                 System.Data.CommandType.StoredProcedure,
                request.SanctionID
                 );
                List<Scrap> ScrapList = new List<Scrap>();
                foreach (DataRow Row in Scraps.Rows)
                {
                    Scrap scrap = new Scrap()
                    {
                        SanctionID = Row["SanctionID"].ToString(),
                        Stt =  int.Parse(Row["STT"].ToString()),
                        PartName = Row["PartName"].ToString(),
                        PartNumber = Row["PartNumber"].ToString(),
                        Quantity = double.Parse(Row["Quantity"].ToString()),
                        Pallet = Row["Pallet"].ToString(),
                        UserID = Row["UserID"].ToString(),
                        DatetimeLoad = DateTime.Now,
                        StatusUpload = 0,
                        DatetimeUpload = " "
                    };
                    ScrapList.Add(scrap);
                }
                return Ok(ScrapList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    public class ImageScrap
    {
        public string SanctionID { get; set; }
        public string Stt { get; set; }
        public string BA { get; set; }
        public string ImagePath { get; set; }
        public string? Datetimecreate { get; set; }
        public string UserID { get; set; }
    }
    public class User
    {
        public string UserID { get; set; }
        public string UserPassWord { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
    }
    public class Scrap
    {
        [JsonPropertyName("SanctionID")]
        public string SanctionID { get; set; }
        [JsonPropertyName("Stt")]
        public int Stt { get; set; }
        [JsonPropertyName("PartName")]
        public string? PartName { get; set; }
        [JsonPropertyName("PartNumber")]
        public string? PartNumber { get; set; }
        [JsonPropertyName("Quantity")]
        public double? Quantity { get; set; }
        [JsonPropertyName("Pallet")]
        public string? Pallet { get; set; }
        [JsonPropertyName("UserID")]
        public string? UserID { get; set; }
        [JsonPropertyName("StatusUpload")]
        public int StatusUpload { get; set; }
        [JsonPropertyName("DatetimeUpload")]
        public string DatetimeUpload { get; set; }
        [JsonPropertyName("DatetimeLoad")]
        public DateTime? DatetimeLoad { get; set; }
    }
    public class LoginRequest
    {
        public string UserID { get; set; }
        public string PassWord { get; set; }
    }
    public class LoadDataRequest
    {
        public string SanctionID { get; set; }
    }

}









//Future<void> uploadFile(File file) async {
//  var uri = Uri.parse('https://your-api-endpoint.com/upload');
//var request = http.MultipartRequest('POST', uri);

//request.files.add(
//  await http.MultipartFile.fromPath(
//    'file', // tên field trên server
//    file.path,
//    filename: basename(file.path),

//  ),

//);

//var response = await request.send();

//if (response.statusCode == 200)
//{
//    print("Upload thành công");
//}
//else
//{
//    print("Lỗi khi upload: ${response.statusCode}");
//}
//}