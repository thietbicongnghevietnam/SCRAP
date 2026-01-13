using HR_API.APP_Start;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace HR_API.Controllers
{
    public class WarehouseGAController : Controller
    {
        [HttpPost]
        [Route("GA_User_Login")]
        public async Task<IActionResult> GA_User_Login(
            [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                if (!requestData.ContainsKey("UserID"))
                {
                    return BadRequest("Missing 'UserID' in request data.");
                }

                DataTable table = await Task.FromResult(
                    DataconnectGA.StoreFillDS(
                        "Get_User_Login",
                        CommandType.StoredProcedure,
                        requestData["UserID"]
                    )
                );

                string json = DataTableToJson(table);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Get_TenBoPhan_DMPB")]
        public async Task<IActionResult> Get_TenBoPhan_DMPB([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("TenPhongBan"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectGA.StoreFillDS(nameof(Get_TenBoPhan_DMPB), CommandType.StoredProcedure, requestData["TenPhongBan"])
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
        [Route("Get_Tenhang_DMHH")]
        public async Task<IActionResult> Get_Tenhang_DMHH([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("mahang"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectGA.StoreFillDS(nameof(Get_Tenhang_DMHH), CommandType.StoredProcedure, requestData["mahang"])
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
        [Route("Get_SL_Order")]
        public async Task<IActionResult> Get_SL_Order([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("mahang") && !requestData.ContainsKey("thang") && !requestData.ContainsKey("mabophan"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectGA.StoreFillDS(nameof(Get_SL_Order), CommandType.StoredProcedure, requestData["mahang"], requestData["thang"], requestData["mabophan"])
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
        [Route("Get_SLTonkho")]
        public async Task<IActionResult> Get_SLTonkho([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("mahang"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectGA.StoreFillDS(nameof(Get_SLTonkho), CommandType.StoredProcedure, requestData["mahang"])
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
        [Route("check_soluongam")]
        public async Task<IActionResult> check_soluongam([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("thang") && !requestData.ContainsKey("nam") && !requestData.ContainsKey("mabophan") && !requestData.ContainsKey("mahang") && !requestData.ContainsKey("slcanxuat"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectGA.StoreFillDS(nameof(check_soluongam), CommandType.StoredProcedure, requestData["thang"], requestData["nam"], requestData["mabophan"], requestData["mahang"], requestData["slcanxuat"])
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
        [Route("Xuly_Tonkho4")]
        public async Task<IActionResult> Xuly_Tonkho4([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("thang") && !requestData.ContainsKey("nam") && !requestData.ContainsKey("mabophan") && !requestData.ContainsKey("mahang") && !requestData.ContainsKey("slcanxuat") && !requestData.ContainsKey("userid"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectGA.StoreFillDS(nameof(Xuly_Tonkho4), CommandType.StoredProcedure, requestData["thang"], requestData["nam"], requestData["mabophan"], requestData["mahang"], requestData["slcanxuat"], requestData["userid"])
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
        [Route("Get_UserPe")]
        public async Task<IActionResult> Get_UserPe([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                //if (!requestData.ContainsKey("UserID"))
                //{
                //    return BadRequest("Missing 'userid' in request data.");
                //}

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectGA.StoreFillDS(nameof(Get_UserPe), CommandType.StoredProcedure)
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
        [Route("Get_Vitri_PCCC")]
        public async Task<IActionResult> Get_Vitri_PCCC([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("mabinh"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectGA.StoreFillDS(nameof(Get_Vitri_PCCC), CommandType.StoredProcedure, requestData["mabinh"])
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
        [Route("Insert_Scan_PCCC_New")]
        public async Task<IActionResult> Insert_Scan_PCCC_New([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("mabinh") && !requestData.ContainsKey("vitri") && !requestData.ContainsKey("tinhtrang") 
                    && !requestData.ContainsKey("trongluong") && !requestData.ContainsKey("ghichu") && !requestData.ContainsKey("thang") 
                    && !requestData.ContainsKey("nam") && !requestData.ContainsKey("userid") && !requestData.ContainsKey("ngaytao"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectGA.StoreFillDS(nameof(Insert_Scan_PCCC_New), CommandType.StoredProcedure, requestData["mabinh"], 
                    requestData["vitri"], requestData["tinhtrang"], requestData["trongluong"], requestData["ghichu"], requestData["thang"], 
                    requestData["nam"], requestData["userid"], requestData["ngaytao"])
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
            return JsonConvert.SerializeObject(table);
        }
    }
}
