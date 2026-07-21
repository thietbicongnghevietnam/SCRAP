using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using HR_API.APP_Start;
using Microsoft.Extensions.Configuration;

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

        [HttpPost]
        [Route("Check_Accessories_Cam")]
        public async Task<IActionResult> Check_Accessories_Cam([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("ocr_text") && !requestData.ContainsKey("model") && !requestData.ContainsKey("serial") && !requestData.ContainsKey("material"))
                {
                    return BadRequest("Missing DATA in request data.");
                }

                // xu ly chuoi API ocr_text o day?

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectOQC.StoreFillDS(nameof(Check_Accessories_Cam), CommandType.StoredProcedure, requestData["ocr_text"], requestData["model"], requestData["serial"], requestData["material"])
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
        [Route("Check_Step_Scan_Accessory")]
        public async Task<IActionResult> Check_Step_Scan_Accessory([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("model") && !requestData.ContainsKey("serial") && !requestData.ContainsKey("material"))
                {
                    return BadRequest("Missing DATA in request data.");
                }               

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectOQC.StoreFillDS(nameof(Check_Step_Scan_Accessory), CommandType.StoredProcedure, requestData["model"], requestData["serial"], requestData["material"])
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

        //API link Hung dao voi OQC 12.10.2025   ****
        //[HttpPost]
        //[Route("Query_HungDao_OQC")]
        //public async Task<IActionResult> Query_HungDao_OQC([FromBody] Dictionary<string, string> requestData)
        //{
        //    try
        //    {
        //        // Kiểm tra xem requestData có chứa key "userid" hay không
        //        if (!requestData.ContainsKey("SIName") && !requestData.ContainsKey("ModelName"))
        //        {
        //            return BadRequest("Missing DATA in request data.");
        //        }

        //        //****no se giong nhu thong tin strored nay: [dbo].[Query_thongtinpalletID]  //DP202503J12
        //        //cat 2 ky dau SI de biet la cate nao => (1)



        //        // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
        //        DataTable table = await Task.FromResult<DataTable>(
        //            DataconnectOQC.StoreFillDS(nameof(Query_HungDao_OQC), CommandType.StoredProcedure, requestData["SIName"], requestData["ModelName"])
        //        );

        //        // Chuyển DataTable thành JSON
        //        string json = DataTableToJson(table);

        //        // Trả về kết quả JSON
        //        return Ok(json);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý lỗi và trả về mã lỗi 500 cùng thông điệp
        //        return StatusCode(500, "Internal server error: " + ex.Message);
        //    }
        //}

        [HttpPost]
        [Route("Query_HungDao_OQC")]
        public async Task<IActionResult> Query_HungDao_OQC([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("SIName") && !requestData.ContainsKey("ModelName"))
                {
                    return BadRequest("Missing DATA in request data.");
                }

                DataTable dt_new = new DataTable();
                dt_new.Columns.Add("si", typeof(String));
                dt_new.Columns.Add("model", typeof(String));
                dt_new.Columns.Add("palletid", typeof(String));
                dt_new.Columns.Add("serial", typeof(String));
                dt_new.Columns.Add("ketquaoqc", typeof(String));

                //****no se giong nhu thong tin strored nay: [dbo].[Query_thongtinpalletID]  //DP202503J12
                //cat 2 ky dau SI de biet la cate nao => (1)
                string cate = "";
                string typescan = "";
                string noidungscan = "";
                string model = requestData["ModelName"];
                string siname = requestData["SIName"];
                string ketquaoqc = "";


                string kytudau = requestData["SIName"].Substring(0, 2); //DP
                if (kytudau == "DP")
                {
                    cate = "DP";
                }
                else if (kytudau == "TE")  //|| kytudau == "DECT"
                {
                    cate = "DECT";
                }
                else if (kytudau == "MW")
                {
                    cate = "MW";
                }
                else if (kytudau == "SB")
                {
                    cate = "MW";
                }
                else if (kytudau == "DC")
                {
                    cate = "CAM";
                }

                //buoc 1: tao view : viewabc (dieu kien theo ngay) union 2 bang jupiter va serial
                //select * from [OQC].[dbo].[viewabc] where [si]=@SIName and masanpham=@ModelName

                //                SELECT id, si, masanpham, seridaquet, soluong, tg, Category, Trangthai, soluongOK, soluongNG, soluongUndercheck, soluongAQ2, Createdate, updatetime
                //FROM DM_Baocaosrial_jupiter
                //WHERE(si = 'DP202404A4')
                //UNION
                //SELECT     id, si, masanpham, seridaquet, soluong, tg, Category, Trangthai, soluongOK, soluongNG, soluongUndercheck, soluongAQ2, Createdate, updatetime
                //FROM         DM_Baocaosrial
                //WHERE(si = 'SB202503J2')


                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable dt = new DataTable();
                DataTable dt_all = await Task.FromResult<DataTable>(
                    DataconnectOQC.StoreFillDS(nameof(Query_HungDao_OQC), CommandType.StoredProcedure, requestData["SIName"], requestData["ModelName"])
                );

                for (int i = 0; i < dt_all.Rows.Count; i++)
                {
                    if (dt_all.Rows[i]["seridaquet"].ToString().Length == 12)
                    {
                        typescan = "palletid";
                        noidungscan = dt_all.Rows[i]["seridaquet"].ToString();
                    }
                    else if (dt_all.Rows[i]["seridaquet"].ToString().Trim().Length > 15)
                    {
                        typescan = "outer";
                        noidungscan = dt_all.Rows[i]["seridaquet"].ToString().Trim();
                    }
                    else
                    {
                        typescan = "serial";
                        noidungscan = dt_all.Rows[i]["seridaquet"].ToString().Replace("%", "").Trim();
                    }

                    ketquaoqc = dt_all.Rows[i]["Trangthai"].ToString();

                    //lay theo stored nay: Visualize_OQC_Inspection_serial

                    dt = DataconnectOQC.StoreFillDS("Visualize_OQC_Inspection_serial2", System.Data.CommandType.StoredProcedure, cate, typescan, noidungscan, model);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            //if (typescan == "palletid")
                            //{
                            //    dt_new.Rows.Add(siname, dt.Rows[j]["STR_PROCESS_FACTORY"].Tostring(), dt_all.rows[i]["seridaquet"].Trim(), dt.Rows[j]["STR_SERIAL"].Tostring(), ketquaoqc);
                            //}
                            //else if (typescan == "outer")
                            //{
                            //    dt_new.Rows.Add(siname, dt.Rows[j]["STR_PROCESS_FACTORY"].Tostring(), dt_all.rows[i]["seridaquet"].Trim(), dt.Rows[j]["STR_SERIAL"].Tostring(), ketquaoqc);
                            //}
                            //dt_new.Rows.Add(siname, model, palletid, serial, ketquaoqc);
                            dt_new.Rows.Add(siname, dt.Rows[j]["STR_PROCESS_FACTORY"].ToString(), noidungscan, dt.Rows[j]["STR_SERIAL"].ToString(), ketquaoqc);
                        }

                    }

                }


                // Chuyển DataTable thành JSON
                //string json = DataTableToJson(dt_all);
                string json = DataTableToJson(dt_new);

                // Trả về kết quả JSON
                return Ok(json);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về mã lỗi 500 cùng thông điệp
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("Upload_Data_OCR_SB")]
        public async Task<IActionResult> Upload_Data_OCR_SB([FromBody] UploadOCRRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Model) || string.IsNullOrWhiteSpace(request.Serial))
            {
                return BadRequest(new { Result = 0, Message = "Model và Serial không được để trống" });
            }

            try
            {
                string model = request.Model.Trim();
                string serial = request.Serial.Trim();

                DataTable dt_new = DataconnectOQC.StoreFillDS("Upload_Data_OCR_SB",System.Data.CommandType.StoredProcedure,model,serial);

                // Kiểm tra dữ liệu trả về
                if (dt_new == null || dt_new.Rows.Count == 0)
                {
                    return StatusCode(500, new { Result = 0, Message = "Stored Procedure không trả về dữ liệu" });
                }

                // Lấy kết quả (giả sử cột đầu tiên là Result)
                var resultValue = dt_new.Rows[0][0].ToString();
                int resultCode = Convert.ToInt32(resultValue);

                if (resultCode == 1)
                {
                    return Ok(new { Result = 1, Message = "Thêm mới thành công" });
                }
                else
                {
                    return Ok(new { Result = 0, Message = "Serial này đã được scan trong 2 tháng qua" });
                }
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết hơn (nên có logger)
                //Console.WriteLine($"Error Upload_Data_OCR_SB: {ex.Message}");
                return StatusCode(500, new { Result = 0, Message = "Lỗi server: " + ex.Message });
            }
        }

        private string DataTableToJson(DataTable table)
        {
            var jsonResult = JsonConvert.SerializeObject(table);
            return jsonResult;
        }

        public class UploadOCRRequest
        {
            public string Model { get; set; } = string.Empty;
            public string Serial { get; set; } = string.Empty;
            //public string? OcrText { get; set; }
            //public string? Material { get; set; }
        }



    }

}
