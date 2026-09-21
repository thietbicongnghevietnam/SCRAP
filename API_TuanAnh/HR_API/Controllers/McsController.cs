using HR_API.APP_Start;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;

namespace HR_API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class McsController : Controller
    {
        private object ex;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Query_thongtinbarcodetest")]
        public async Task<IActionResult> Query_thongtinbarcode([FromBody] Dictionary<string, string> requestData)
        {
            McsController objteset = this;
            try
            {
                DataTable dataTable = new DataTable();
                if (!requestData.ContainsKey("chuoibarcode") || !requestData.ContainsKey("typerecheck"))
                    return (IActionResult)null;

                DataTable table = await Task.FromResult<DataTable>(DataConnMCS.StoreFillDS("", CommandType.StoredProcedure, (object)requestData["chuoibarcode"], (object)requestData["typerecheck"]));
                string json = objteset.DataTableToJson(table);
                return (IActionResult)objteset.Ok((object)json);

            }
            catch (Exception ex)
            {
                return (IActionResult)objteset.StatusCode(500, (object)("Internal server error: " + ex.Message));
            }
        }

        [HttpPost]
        [Route("check_barcode_exit")]
        public async Task<string> check_barcode_exit([FromBody] Dictionary<string, string> requestData)
        {
            string kq = string.Empty;
            string barcode;
            if (requestData.ContainsKey("barcode"))
            {
                barcode = requestData["barcode"];
                DataTable dt = await Task.FromResult(DataConnMCS.StoreFillDS("check_barcode_exit", CommandType.StoredProcedure, barcode));
                if (dt.Rows.Count > 0)
                {
                    kq = dt.Rows[0][0].ToString();
                }
            }
            return kq;
        }

        //[HttpPost]
        //[Route("updatercforsamplerosh_new")]
        //public async Task<bool> updatercforsamplerosh_new([FromBody] Dictionary<string, string> requestData)
        //{
        //    string version;
        //    bool kq;
        //    // Trích xuất dữ liệu từ requestData
        //    try
        //    {
        //        if (requestData.ContainsKey("version"))
        //        {
        //            version = requestData["version"];
        //            if (await Task.FromResult(DataConnMCS.DataConnMCS("updatercforsamplerosh_new", CommandType.StoredProcedure, version).Rows.Count) > 0)
        //            {
        //                kq = true;
        //            }
        //            else { kq = false; }
        //            return kq;
        //        }               
        //    }
        //    catch (Exception ex) 
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}"); // Trả về thông báo lỗi
        //    }

        //}

        //[HttpPost]
        //[Route("updatercforsamplerosh_new")]
        //public async Task<bool> updatercforsamplerosh_new([FromBody] Dictionary<string, string> requestData)
        //{
        //    //if (!requestData.ContainsKey("barcode"))
        //    //    throw new Exception("ERR_API");
        //    return await Task.FromResult<int>(DataConnMCS.ExcuteScara("check_pallet", new string[3]
        //    {
        //"@barcode","@barcodeBox","@qtyRosh"
        //    }, new object[3]
        //    {
        //(object) requestData["barcode"],requestData["barcodeBox"],requestData["qtyRosh"]
        //    })) > 0;
        //}

        [HttpPost]
        [Route("updatercforsamplerosh_new")]
        public async Task<IActionResult> updatercforsamplerosh_new([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có null hay không
                if (requestData == null)
                {
                    return BadRequest("Request data is null.");
                }

                // Lấy giá trị từ requestData, gán "" nếu không tồn tại
                string barcode = requestData.ContainsKey("bacodeid") ? requestData["bacodeid"] : "";
                string barcodeBox = requestData.ContainsKey("barcodeBox") ? requestData["barcodeBox"] : "";
                string qty = requestData.ContainsKey("qty") ? requestData["qty"] : "";

                // Thiết lập tham số cho stored procedure
                string[] para = { "@barcode", "@barcodeBox", "@qty" };
                object[] value = { string.IsNullOrEmpty(barcode) ? (object)DBNull.Value : barcode,
                           string.IsNullOrEmpty(barcodeBox) ? (object)DBNull.Value : barcodeBox,
                           string.IsNullOrEmpty(qty) ? (object)DBNull.Value : qty };

                // Gọi stored procedure
                int kq = await Task.FromResult(DataConnMCS.ExcuteNonStore("UpdateQTYForSAMPLEQty_new", para, value));

                // Trả về kết quả dựa trên giá trị kq
                return Ok(kq > 0); // Trả về true nếu thành công, false nếu không
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Trả về thông báo lỗi
            }
        }

        [HttpPost]
        [Route("Auto_Sap_rohs_Iqc")]
        public async Task<IActionResult> Auto_Sap_rohs_Iqc([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                //string barcode, barcodeBox, qty;
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("barcode") || !requestData.ContainsKey("barcodeBox") || !requestData.ContainsKey("qty"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Auto_Sap_rohs_Iqc), CommandType.StoredProcedure, requestData["barcode"], requestData["barcodeBox"], requestData["qty"])
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
        
        //stored danh gia NG LOT  truong hop IQC danh gia rohs truoc, sau danh gia NG sau  03.01.2025
        [HttpPost]
        [Route("Auto_Sap_rohs_Iqc2")]
        public async Task<IActionResult> Auto_Sap_rohs_Iqc2([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                //string barcode, barcodeBox, qty;
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("barcode") || !requestData.ContainsKey("barcodeBox") || !requestData.ContainsKey("qty") || !requestData.ContainsKey("typeIQC"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Auto_Sap_rohs_Iqc2), CommandType.StoredProcedure, requestData["barcode"], requestData["barcodeBox"], requestData["qty"], requestData["typeIQC"])
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
        // revert truong hop IQC click nut NO khi NG LOT  03.01.2025
        [HttpPost]
        [Route("Auto_Sap_rohs_Iqc_NG_revert")]
        public async Task<IActionResult> Auto_Sap_rohs_Iqc_NG_revert([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                //string barcode, barcodeBox, qty;
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("barcode") || !requestData.ContainsKey("barcodeBox") || !requestData.ContainsKey("qty"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Auto_Sap_rohs_Iqc_NG_revert), CommandType.StoredProcedure, requestData["barcode"], requestData["barcodeBox"], requestData["qty"])
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
        [Route("Auto_Sap_rohs_Iqc_NGLOT")]
        public async Task<IActionResult> Auto_Sap_rohs_Iqc_NGLOT([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                //string barcode, barcodeBox, qty;
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("barcode") || !requestData.ContainsKey("barcodeBox") || !requestData.ContainsKey("qty"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Auto_Sap_rohs_Iqc_NGLOT), CommandType.StoredProcedure, requestData["barcode"], requestData["barcodeBox"], requestData["qty"])
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
        [Route("Query_CheckBoxCard")]
        public async Task<IActionResult> Query_CheckBoxCard([FromBody] Dictionary<string, string> requestData)
        {
            try
            {                
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("barcode") || !requestData.ContainsKey("barcodeBox"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Query_CheckBoxCard), CommandType.StoredProcedure, requestData["barcode"], requestData["barcodeBox"])
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
        [Route("Query_CheckBoxCard_manybox")]
        public async Task<IActionResult> Query_CheckBoxCard_manybox([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("barcode") || !requestData.ContainsKey("barcodeBox") || !requestData.ContainsKey("QtyRC"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Query_CheckBoxCard_manybox), CommandType.StoredProcedure, requestData["barcode"], requestData["barcodeBox"], requestData["QtyRC"])
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
        [Route("Query_Check_GR")]
        public async Task<IActionResult> Query_Check_GR([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("barcode") || !requestData.ContainsKey("SLNG"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Query_Check_GR), CommandType.StoredProcedure, requestData["barcode"], requestData["SLNG"])
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

        // ===================================== // chuc năng QC project ==> tich hop  SWMS // ================= 

        [HttpPost]
        [Route("Upload_DateCode_MCS")]
        public async Task<IActionResult> Upload_DateCode_MCS([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra các key bắt buộc
                if (!requestData.ContainsKey("material") ||
                    !requestData.ContainsKey("codedate") ||
                    !requestData.ContainsKey("type"))
                {
                    return BadRequest("Missing required fields: material, codedate, type");
                }

                // Lấy giá trị (kittingCardId có thể để trống)
                string material = requestData["material"];
                string codedate = requestData["codedate"];
                string typecodedate = requestData["type"];
                string kittingCardId = requestData.ContainsKey("kittingCardId")
                                        ? requestData["kittingCardId"]
                                        : "";
                string userId = requestData["userId"];

                DataTable dt_table = new DataTable();
                if (typecodedate == "Kitting Card")
                {
                    //trace code date theo Kitting card
                    // Gọi Stored Procedure
                    dt_table = await Task.FromResult<DataTable>(
                        DataconnectFreeL.StoreFillDS(nameof(Upload_DateCode_MCS), CommandType.StoredProcedure, material, codedate, typecodedate, kittingCardId, userId)
                    );
                }
                else 
                {
                    //typecodedate == "PCBID"
                    //trace code date theo FA (PCBID hoac serial no)
                    //insert sang database trace DP FA
                    dt_table = await Task.FromResult<DataTable>(
                        DataconnectDPTrace.StoreFillDS(nameof(Upload_DateCode_MCS), CommandType.StoredProcedure, material, codedate, typecodedate, kittingCardId, userId)
                    );
                }
               
                // Chuyển DataTable thành JSON
                string json = DataTableToJson(dt_table);

                // Trả về kết quả
                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpPost]
        [Route("Query_ReceivingCard_Reprint")]
        public async Task<IActionResult> Query_ReceivingCard_Reprint([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("barcode") || !requestData.ContainsKey("typeSource"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Query_ReceivingCard_Reprint), CommandType.StoredProcedure, requestData["barcode"], requestData["typeSource"])
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
        [Route("Query_PrinterDevice_List")]
        public async Task<IActionResult> Query_PrinterDevice_List([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                //if (!requestData.ContainsKey("barcode") || !requestData.ContainsKey("typeSource"))
                //{
                //    return BadRequest("Missing 'userid' in request data.");
                //}

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Query_PrinterDevice_List), CommandType.StoredProcedure)
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
        [Route("Query_List_Lydo311")]
        public async Task<IActionResult> Query_List_Lydo311([FromBody] Dictionary<string, string> requestData)
        {
            try
            {               
                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Query_List_Lydo311), CommandType.StoredProcedure)
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
        [Route("Query_List_Tcode")]
        public async Task<IActionResult> Query_List_Tcode([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Query_List_Tcode), CommandType.StoredProcedure)
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
        [Route("Insert_ReceivingCard_RepLoc")]
        public async Task<IActionResult> Insert_ReceivingCard_RepLoc([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                if (!requestData.ContainsKey("barcode") || !requestData.ContainsKey("material") || !requestData.ContainsKey("quantity") || !requestData.ContainsKey("repLoc"))
                {
                    return BadRequest("Missing request data.");
                }
                string barcode = requestData["barcode"];
                string typeSource = requestData["typeSource"];
                string plant = requestData["plant"];
                string material = requestData["material"];
                string quantity = requestData["quantity"];
                string sloc = requestData["sloc"];
                string repLoc = requestData["repLoc"];
                string tcode = requestData["tcode"];
                string reason = requestData["reason"];
                string userName = requestData["userName"];

                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Insert_ReceivingCard_RepLoc), CommandType.StoredProcedure, barcode, typeSource, plant, material, quantity, sloc, repLoc, tcode, reason, userName)
                );
                
                return Ok(DataTableToJson(table));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_KittingCard_Outside")]
        public async Task<IActionResult> Query_KittingCard_Outside([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                if (!requestData.ContainsKey("barcode") || !requestData.ContainsKey("userId"))
                {
                    return BadRequest("Missing request data.");
                }
                string barcode = requestData["barcode"];                
                string userId = requestData["userId"];                

                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Query_KittingCard_Outside), CommandType.StoredProcedure, barcode, userId)
                );

                return Ok(DataTableToJson(table));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Update_SupplyKittingOutside")]
        public async Task<IActionResult> Update_SupplyKittingOutside([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                if (!requestData.ContainsKey("barcode") || !requestData.ContainsKey("line") || !requestData.ContainsKey("userName"))
                {
                    return BadRequest("Missing request data.");
                }
                string barcode = requestData["barcode"];
                string line = requestData["line"];
                string userName = requestData["userName"];

                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Update_SupplyKittingOutside), CommandType.StoredProcedure, barcode, line, userName)
                );

                return Ok(DataTableToJson(table));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        //[HttpPost]
        //[Route("updatercforsamplerosh_new")]
        //public async Task<IActionResult> updatercforsamplerosh_new(Dictionary<string, string> requestData)
        //{
        //    try
        //    {
        //        //// Kiểm tra xem requestData có null hay không
        //        //if (requestData == null)
        //        //{
        //        //    return BadRequest("Request data is null.");
        //        //}

        //        // Lấy giá trị từ requestData, gán "" nếu không tồn tại
        //        string barcode;// requestData.ContainsKey("barcode") ? requestData["barcode"] : "";
        //        string barcodeBox;//requestData.ContainsKey("barcodeBox") ? requestData["barcodeBox"] : "";
        //        string qty;
        //        if (requestData.ContainsKey("barcode") == null)
        //        {
        //            barcode = "";
        //        }
        //        else
        //        {
        //            barcode = requestData["barcode"];
        //        }
        //        if (requestData.ContainsKey("barcodeBox") == null)
        //        {
        //            barcodeBox = "";
        //        }
        //        else
        //        {
        //            barcodeBox = requestData["barcodeBox"];
        //        }
        //        qty = requestData["qtyRosh"];
        //        // Thiết lập tham số cho stored procedure
        //        string[] para = { "@barcode", "@barcodeBox", "@qty" };
        //        object[] value = { barcode??(object)DBNull.Value, barcodeBox??(object)DBNull.Value, qty };

        //        // Gọi stored procedure
        //        int kq = await Task.FromResult(DataConnMCS.ExcuteNonStore("UpdateQTYForSAMPLEQty_new", para, value));

        //        // Trả về kết quả dựa trên giá trị kq
        //        return Ok(kq > 0); // Trả về true nếu thành công, false nếu không
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}"); // Trả về thông báo lỗi
        //    }
        //}





        //private string DataTableToJson(DataTable table)
        //{
        //    return JsonConvert.SerializeObject(table);
        //}

        private string DataTableToJson(DataTable table)
        {
            var jsonResult = JsonConvert.SerializeObject(table);
            return jsonResult;
        }

    }

    
}
