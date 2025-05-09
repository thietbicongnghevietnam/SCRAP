using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using HR_API.APP_Start;

namespace HR_API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class OQCController: Controller
    {
        [HttpPost]
        [Route("Query_Login_OQC")]
        public async Task<IActionResult> Query_Login_OQC([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("userid"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectOQC.StoreFillDS(nameof(Query_Login_OQC), CommandType.StoredProcedure, requestData["userid"])
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
        [Route("OQC_get_cate")]
        public async Task<IActionResult> OQC_get_cate([FromBody] Dictionary<string, string> requestData)
        {            
            try
            {                                
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectOQC.StoreFillDS(nameof(OQC_get_cate), CommandType.StoredProcedure)
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
        [Route("Query_thongtinpalletID")]
        public async Task<IActionResult> Query_thongtinpalletID([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("chuoibarcode") && !requestData.ContainsKey("Category") && !requestData.ContainsKey("typescan"))
                {
                    return BadRequest("Missing DATA in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectOQC.StoreFillDS(nameof(Query_thongtinpalletID), CommandType.StoredProcedure, requestData["chuoibarcode"], requestData["Category"], requestData["typescan"])
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
        [Route("Query_thongtinSI")]
        public async Task<IActionResult> Query_thongtinSI([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("chuoisi") && !requestData.ContainsKey("Category"))
                {
                    return BadRequest("Missing DATA in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectOQC.StoreFillDS(nameof(Query_thongtinSI), CommandType.StoredProcedure, requestData["chuoisi"], requestData["Category"])
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



    }

}
