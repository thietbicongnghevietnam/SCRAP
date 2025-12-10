using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System;
using HR_API.APP_Start;
using Newtonsoft.Json;
using System.Data;
using static System.Collections.Specialized.BitVector32;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;



using System;
using System.Collections.Generic;
using System.Threading.Tasks;


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

                if (ImageScrap == null)
                    return BadRequest("No file uploaded.");                

                // Tạo đường dẫn để lưu file, bao gồm tên userID   //D:\HAIT\Scapsystem\wwwroot\Images  ==> doi sang duoc dan nay
                //var userDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ScrapData", ImageScrap.BA, ImageScrap.SanctionID);
                var userDirectory = Path.Combine("D:\\HAIT\\Scapsystem\\wwwroot\\Images", ImageScrap.BA, ImageScrap.SanctionID);
                // Tạo thư mục nếu chưa tồn tại
                Directory.CreateDirectory(userDirectory);

                // Đường dẫn file
                var filePath = Path.Combine(userDirectory, file.FileName.Split('/').Last());

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Tạo đường dẫn để lưu file2
                var userDirectory2 = Path.Combine("\\Images", ImageScrap.BA, ImageScrap.SanctionID);
                Directory.CreateDirectory(userDirectory2);

                string nameimage = ImageScrap.ImagePath.Split("/").Last();

                //var pathsaveimg = Path.Combine(userDirectory, nameimage);
                var pathsaveimg = Path.Combine(userDirectory2, nameimage);


                //18.06.2025 sau them upload all hay onebyone
                if (ImageScrap.statusUpload == "1")
                {
                    //all
                    //truong hop insert vao bang one by one
                    DbconnectScrap.excutenonquerry("CreateImageScrap_all",
                    System.Data.CommandType.StoredProcedure,
                    ImageScrap.SanctionID,
                    //int.Parse(ImageScrap.Stt),
                    ImageScrap.Stt,
                    ImageScrap.BA,
                    pathsaveimg,
                    "2025-04-21",
                    ImageScrap.UserID,
                    UserIDSyn,
                    ImageScrap.pallet,
                    ImageScrap.MVT
                    );
                    return Ok(ImageScrap);
                }
                else
                {
                    //truong hop insert vao bang one by one
                    DbconnectScrap.excutenonquerry("CreateImageScrap",
                    System.Data.CommandType.StoredProcedure,
                    ImageScrap.SanctionID,
                    //int.Parse(ImageScrap.Stt),
                    ImageScrap.Stt,
                    ImageScrap.BA,
                    pathsaveimg,
                    "2025-04-21",
                    ImageScrap.UserID,
                    UserIDSyn,
                    ImageScrap.MVT
                    );
                    return Ok(ImageScrap);
                }


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

        [HttpPost]
        [Route("UploadPalletID")]       
        public async Task<IActionResult> UploadPalletID([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                //01.07.2025 chuyen them bien //quantity ==> truong hop chia pallet //partNumber
                // Kiểm tra xem requestData có chứa key "userid" hay không //quantity   //Quantity_Act
                if (!requestData.ContainsKey("UserID") && !requestData.ContainsKey("SanctionID") && !requestData.ContainsKey("Palletid") && !requestData.ContainsKey("stt") && !requestData.ContainsKey("quantity") && !requestData.ContainsKey("partNumber") && !requestData.ContainsKey("Quantity_Act"))
                {
                    return BadRequest("Missing DATA in request data.");
                }
                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DbconnectScrap.StoreFillDS(nameof(UploadPalletID), CommandType.StoredProcedure, requestData["UserID"], requestData["SanctionID"], requestData["Palletid"], requestData["stt"], requestData["quantity"], requestData["partNumber"], requestData["Quantity_Act"])
                );

                // Chuyển DataTable thành JSON
                string json = DataTableToJson(table);

                // Trả về kết quả JSON
                return Ok(json);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về mã lỗi 500 cùng thông điệp
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("CheckSanctiondelete")]
        public async Task<IActionResult> CheckSanctiondelete([FromBody] Dictionary<string, string> requestData)
        {
            try
            {                
                if (!requestData.ContainsKey("UserID") && !requestData.ContainsKey("SanctionID"))
                {
                    return BadRequest("Missing DATA in request data.");
                }
                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DbconnectScrap.StoreFillDS(nameof(CheckSanctiondelete), CommandType.StoredProcedure, requestData["UserID"], requestData["SanctionID"] )
                );

                // Chuyển DataTable thành JSON
                string json = DataTableToJson(table);

                // Trả về kết quả JSON
                return Ok(json);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về mã lỗi 500 cùng thông điệp
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        private string DataTableToJson(DataTable table)
        {
            var jsonResult = JsonConvert.SerializeObject(table);
            return jsonResult;
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
                        DatetimeUpload = " ",
                        Barcode = Row["Barcode"].ToString(),
                        mvt = Row["mvt"].ToString(),
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

        [Route("LoadScrap_sanction")]
        [DisableRequestSizeLimit]
        [HttpPost]
        public async Task<IActionResult> LoadScrap_sanction([FromBody] LoadDataRequest request)
        {
            try
            {
                DataTable Scraps = DbconnectScrap.StoreFillDS("LoadScrap_sanction",
                 System.Data.CommandType.StoredProcedure,
                request.SanctionID
                 );
                List<Scrap> ScrapList = new List<Scrap>();
                foreach (DataRow Row in Scraps.Rows)
                {
                    Scrap scrap = new Scrap()
                    {
                        SanctionID = Row["SanctionID"].ToString(),

                        Stt = int.Parse(Row["stt"].ToString()),
                        PartName = Row["partName"].ToString(),
                        PartNumber = Row["partNumber"].ToString(),
                        Quantity = double.Parse(Row["quantity"].ToString()),
                        Pallet = Row["pallet"].ToString(),
                        StatusUpload = 0,
                        DatetimeLoad = DateTime.Now,
                        DatetimeUpload = " ",
                        UserID = Row["UserID"].ToString()                                                
                        
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
        public string statusUpload { get; set; }
        public string pallet { get; set; }
        public string MVT { get; set; }


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

        [JsonPropertyName("Quantity_Act")]
        public double? Quantity_Act { get; set; }
        [JsonPropertyName("Barcode")]
        public string? Barcode { get; set; }
        [JsonPropertyName("mvt")]
        public string? mvt { get; set; }


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