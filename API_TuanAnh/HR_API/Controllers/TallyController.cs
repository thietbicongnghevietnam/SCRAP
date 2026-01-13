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
    public class TallyController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}


        // ===================== // chuyen code tu con API .22 sang .131  //==============
        [HttpPost]
        [Route("Tally_User_Login")]
        public async Task<IActionResult> Tally_User_Login([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("UserID"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_User_Login), CommandType.StoredProcedure, requestData["UserID"])
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
        [Route("Tally_get_plant")]
        public async Task<IActionResult> Tally_get_plant([FromBody] Dictionary<string, string> requestData)
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
                    DataconnectTally.StoreFillDS(nameof(Tally_get_plant), CommandType.StoredProcedure)
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
        [Route("Tally_Slect_Stock")]
        public async Task<IActionResult> Tally_Slect_Stock([FromBody] Dictionary<string, string> requestData)
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
                    DataconnectTally.StoreFillDS(nameof(Tally_Slect_Stock), CommandType.StoredProcedure)
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
        [Route("Tally_CheckInput_Barcode")]
        public async Task<IActionResult> Tally_CheckInput_Barcode([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("barcode"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_CheckInput_Barcode), CommandType.StoredProcedure, requestData["barcode"])
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

        //Tally_Check_Qty_SI
        [HttpPost]
        [Route("Tally_Check_Qty_SI")]
        public async Task<IActionResult> Tally_Check_Qty_SI([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("SI"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_Check_Qty_SI), CommandType.StoredProcedure, requestData["SI"])
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
        //Tally_Check_ModeInSI
        [HttpPost]
        [Route("Tally_Check_ModeInSI")]
        public async Task<IActionResult> Tally_Check_ModeInSI([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("SI") && !requestData.ContainsKey("model"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_Check_ModeInSI), CommandType.StoredProcedure, requestData["SI"], requestData["model"])
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
        //Tally_Check_Qty_Pallet
        [HttpPost]
        [Route("Tally_Check_Qty_Pallet")]
        public async Task<IActionResult> Tally_Check_Qty_Pallet([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("palletid") && !requestData.ContainsKey("SI"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_Check_Qty_Pallet), CommandType.StoredProcedure, requestData["palletid"], requestData["SI"])
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
        //Tally_Check_BacodeInSI
        [HttpPost]
        [Route("Tally_Check_BacodeInSI")]
        public async Task<IActionResult> Tally_Check_BacodeInSI([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("SI") && !requestData.ContainsKey("model") && !requestData.ContainsKey("barcode"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_Check_BacodeInSI), CommandType.StoredProcedure, requestData["SI"], requestData["model"], requestData["barcode"])
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
        //Tally_Input_Items
        [HttpPost]
        [Route("Tally_Input_Items")]
        public async Task<bool> Tally_Input_Items([FromBody] Dictionary<string, string> requestData)
        {
            if (!requestData.ContainsKey("plant") || !requestData.ContainsKey("barcode") || !requestData.ContainsKey("model") || !requestData.ContainsKey("stand") || !requestData.ContainsKey("sopit") || !requestData.ContainsKey("socarton") || !requestData.ContainsKey("makho") || !requestData.ContainsKey("creatdate") || !requestData.ContainsKey("createby") || !requestData.ContainsKey("solannhap"))
                throw new Exception("ERR_API");
            return await Task.FromResult<bool>(DataconnectTally.ExcuteStored_bool(nameof(Tally_Input_Items), new string[10]
            {
        "@plant",
        "@barcode",
        "@model",
        "@stand",
        "@sopit",
        "@socarton",
        "@makho",
        "@creatdate",
        "@createby",
        "@solannhap"
            }, new object[10]
            {
        (object) requestData["plant"],
        (object) requestData["barcode"],
        (object) requestData["model"],
        (object) requestData["stand"],
        (object) requestData["sopit"],
        (object) requestData["socarton"],
        (object) requestData["makho"],
        (object) requestData["creatdate"],
        (object) requestData["createby"],
        (object) new int?(int.Parse(requestData["solannhap"]))
            }));
        }

        [HttpPost]
        [Route("Tally_Combine_Pallet_SP")]
        public async Task<bool> Tally_Combine_Pallet_SP([FromBody] Dictionary<string, string> requestData)
        {
            if (!requestData.ContainsKey("SI") || !requestData.ContainsKey("model") || !requestData.ContainsKey("barcode") || !requestData.ContainsKey("palletid") || !requestData.ContainsKey("cate") || !requestData.ContainsKey("BD") || !requestData.ContainsKey("CreateDate") || !requestData.ContainsKey("CreateBy") || !requestData.ContainsKey("soluongpit") || !requestData.ContainsKey("thetich"))
                throw new ValidationException("API err");
            return await Task.FromResult<int>(DataconnectTally.ExcuteStored_int(nameof(Tally_Combine_Pallet_SP), new string[10]
            {
        "@SI",
        "@model",
        "@barcode",
        "@palletid",
        "@cate",
        "@BD",
        "@CreateDate",
        "@CreateBy",
        "@soluongpit",
        "@thetich"
            }, new object[10]
            {
        (object) requestData["SI"],
        (object) requestData["model"],
        (object) requestData["barcode"],
        (object) requestData["palletid"],
        (object) requestData["cate"],
        (object) requestData["BD"],
        (object) requestData["CreateDate"],
        (object) requestData["CreateBy"],
        (object) requestData["soluongpit"],
        (object) requestData["thetich"]
            })) > 0;
        }

        [HttpPost]
        [Route("Tally_Delete_Box_SP2")]
        public async Task<bool> Tally_Delete_Box_SP2([FromBody] Dictionary<string, string> requestData)
        {
            if (!requestData.ContainsKey("SI") || !requestData.ContainsKey("barcode"))
                throw new ValidationException("API err");
            return await Task.FromResult<int>(DataconnectTally.ExcuteStored_int(nameof(Tally_Delete_Box_SP2), new string[2]
            {
        "@SI",
        "@barcode"
            }, new object[2]
            {
        (object) requestData["SI"],
        (object) requestData["barcode"]
            })) > 0;
        }

        //Tally_Check_Qty_Pallet4
        [HttpPost]
        [Route("Tally_Check_Qty_Pallet4")]
        public async Task<IActionResult> Tally_Check_Qty_Pallet4([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("palletid"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_Check_Qty_Pallet4), CommandType.StoredProcedure, requestData["palletid"])
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
        [Route("Tally_Insert_Pallet_SP")]
        public async Task<bool> Tally_Insert_Pallet_SP([FromBody] Dictionary<string, string> requestData)
        {
            if (!requestData.ContainsKey("SI") || !requestData.ContainsKey("barcode") || !requestData.ContainsKey("palletid") || !requestData.ContainsKey("CreateDate") || !requestData.ContainsKey("CreateBy"))
                throw new ValidationException("API err");
            return await Task.FromResult<int>(DataconnectTally.ExcuteStored_int(nameof(Tally_Insert_Pallet_SP), new string[5]
            {
        "@SI",
        "@barcode",
        "palletid",
        "CreateDate",
        "CreateBy"
            }, new object[5]
            {
        (object) requestData["SI"],
        (object) requestData["barcode"],
        (object) requestData["palletid"],
        (object) requestData["CreateDate"],
        (object) requestData["CreateBy"]
            })) > 0;
        }

        [HttpPost]
        [Route("Tally_check_CombineSP")]
        public async Task<IActionResult> Tally_check_CombineSP([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("barcode") && !requestData.ContainsKey("palletid"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_check_CombineSP), CommandType.StoredProcedure, requestData["barcode"], requestData["palletid"])
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
        [Route("Tally_Get_Info_Pallet_SP")]
        public async Task<bool> Tally_Get_Info_Pallet_SP([FromBody] Dictionary<string, string> requestData)
        {
            if (!requestData.ContainsKey("SI") || !requestData.ContainsKey("barcode") || !requestData.ContainsKey("palletid") || !requestData.ContainsKey("cate") || !requestData.ContainsKey("BD") || !requestData.ContainsKey("CreateDate") || !requestData.ContainsKey("CreateBy"))
                throw new ValidationException("API err");
            return await Task.FromResult<int>(DataconnectTally.ExcuteStored_int(nameof(Tally_Get_Info_Pallet_SP), new string[7]
            {
        "@SI",
        "@barcode",
        "palletid",
        "@cate",
        "@BD",
        "CreateDate",
        "CreateBy"
            }, new object[7]
            {
        (object) requestData["SI"],
        (object) requestData["barcode"],
        (object) requestData["palletid"],
        (object) requestData["cate"],
        (object) requestData["BD"],
        (object) requestData["CreateDate"],
        (object) requestData["CreateBy"]
            })) > 0;
        }

        [HttpPost]
        [Route("Tally_Delete_Box_SP")]
        public async Task<bool> Tally_Delete_Box_SP([FromBody] Dictionary<string, string> requestData)
        {
            if (!requestData.ContainsKey("SI") || !requestData.ContainsKey("barcode") || !requestData.ContainsKey("userid"))
                throw new ValidationException("API err");
            return await Task.FromResult<int>(DataconnectTally.ExcuteStored_int(nameof(Tally_Delete_Box_SP), new string[3]
            {
        "@SI",
        "@barcode",
        "@userid"
            }, new object[3]
            {
        (object) requestData["SI"],
        (object) requestData["barcode"],
        (object) requestData["userid"]
            })) > 0;
        }

        [HttpPost]
        [Route("Tally_Check_Qty_Pallet2")]
        public async Task<IActionResult> Tally_Check_Qty_Pallet2([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("palletid") && !requestData.ContainsKey("SI"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_Check_Qty_Pallet2), CommandType.StoredProcedure, requestData["palletid"], requestData["SI"])
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

        //Tally_Check_Qty_Pallet3
        [HttpPost]
        [Route("Tally_Check_Qty_Pallet3")]
        public async Task<IActionResult> Tally_Check_Qty_Pallet3([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("palletid") && !requestData.ContainsKey("SI"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_Check_Qty_Pallet3), CommandType.StoredProcedure, requestData["palletid"], requestData["SI"])
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

        //SP_ServicePart_OverSea_CheckPDA
        [HttpPost]
        [Route("SP_ServicePart_OverSea_CheckPDA")]
        public async Task<IActionResult> SP_ServicePart_OverSea_CheckPDA([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("palletid"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(SP_ServicePart_OverSea_CheckPDA), CommandType.StoredProcedure, requestData["palletid"])
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

        //SP_ServicePart_OverSea_CheckPDA_Receive
        [HttpPost]
        [Route("SP_ServicePart_OverSea_CheckPDA_Receive")]
        public async Task<bool> SP_ServicePart_OverSea_CheckPDA_Receive(
        [FromBody] Dictionary<string, string> requestData)
        {
            if (!requestData.ContainsKey("PalletID") || !requestData.ContainsKey("EmpID"))
                throw new ValidationException("API err");
            return await Task.FromResult<int>(DataconnectTally.ExcuteStored_int(nameof(SP_ServicePart_OverSea_CheckPDA_Receive), new string[2]
            {
        "@PalletID",
        "@UserName"
            }, new object[2]
            {
        (object) requestData["PalletID"],
        (object) requestData["EmpID"]
            })) > 0;
        }

        //Get_Category
        [HttpPost]
        [Route("Get_Category")]
        public async Task<IActionResult> Get_Category([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                //if (!requestData.ContainsKey("palletid"))
                //{
                //    return BadRequest("Missing 'userid' in request data.");
                //}

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataConneFoss1.StoreFillDS(nameof(Get_Category), CommandType.StoredProcedure)
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

        //Tally_insert_tblcombine
        [HttpPost]
        [Route("Tally_insert_tblcombine")]
        public async Task<IActionResult> Tally_insert_tblcombine([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("palletdich") && !requestData.ContainsKey("typecategogy"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_insert_tblcombine), CommandType.StoredProcedure, requestData["palletdich"], requestData["typecategogy"])
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

        //Tally_insert_tblcombine2
        [HttpPost]
        [Route("Tally_insert_tblcombine2")]
        public async Task<IActionResult> Tally_insert_tblcombine2([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("palletnguon") && !requestData.ContainsKey("typecategogy"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_insert_tblcombine2), CommandType.StoredProcedure, requestData["palletnguon"], requestData["typecategogy"])
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

        //Tally_check_gopghepPallet
        [HttpPost]
        [Route("Tally_check_gopghepPallet")]
        public async Task<IActionResult> Tally_check_gopghepPallet([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("pallet_source") && !requestData.ContainsKey("pallet_destination"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_check_gopghepPallet), CommandType.StoredProcedure, requestData["pallet_source"], requestData["pallet_destination"])
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
        //Tally_update_tblcombine_sound
        [HttpPost]
        [Route("Tally_update_tblcombine_sound")]
        public async Task<IActionResult> Tally_update_tblcombine_sound([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("pallet_source") && !requestData.ContainsKey("pallet_destination"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_update_tblcombine_sound), CommandType.StoredProcedure, requestData["pallet_source"], requestData["pallet_destination"])
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

        //Tally_insert_tblcombine3
        [HttpPost]
        [Route("Tally_insert_tblcombine3")]
        public async Task<IActionResult> Tally_insert_tblcombine3([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("palletdich") && !requestData.ContainsKey("typecategogy"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_insert_tblcombine3), CommandType.StoredProcedure, requestData["palletdich"], requestData["typecategogy"])
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

        //Tally_insert_tblcombine4
        [HttpPost]
        [Route("Tally_insert_tblcombine4")]
        public async Task<IActionResult> Tally_insert_tblcombine4([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("palletnguon") && !requestData.ContainsKey("typecategogy"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_insert_tblcombine4), CommandType.StoredProcedure, requestData["palletnguon"], requestData["typecategogy"])
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

        //Tally_update_tblcombine_sound2
        [HttpPost]
        [Route("Tally_update_tblcombine_sound2")]
        public async Task<IActionResult> Tally_update_tblcombine_sound2([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("pallet_source") && !requestData.ContainsKey("pallet_destination") && !requestData.ContainsKey("typeserial") && !requestData.ContainsKey("_categogy"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_update_tblcombine_sound2), CommandType.StoredProcedure, requestData["pallet_source"], requestData["pallet_destination"], requestData["typeserial"], requestData["_categogy"])
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

        //Tally_CheckSI_sound
        [HttpPost]
        [Route("Tally_CheckSI_sound")]
        public async Task<IActionResult> Tally_CheckSI_sound([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("SIID"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_CheckSI_sound), CommandType.StoredProcedure, requestData["SIID"])
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
        //Tally_CheckPalletID_sound
        [HttpPost]
        [Route("Tally_CheckPalletID_sound")]
        public async Task<IActionResult> Tally_CheckPalletID_sound([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("PalletID") && !requestData.ContainsKey("SI_ID"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_CheckPalletID_sound), CommandType.StoredProcedure, requestData["PalletID"], requestData["SI_ID"])
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
        //Tally_Input_sound
        [HttpPost]
        [Route("Tally_Input_sound")]
        public async Task<IActionResult> Tally_Input_sound([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("PalletID") && !requestData.ContainsKey("id_si") && !requestData.ContainsKey("userid"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_Input_sound), CommandType.StoredProcedure, requestData["PalletID"], requestData["id_si"], requestData["userid"])
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

        //Tally_CheckQty_Pallet
        [HttpPost]
        [Route("Tally_CheckQty_Pallet")]
        public async Task<IActionResult> Tally_CheckQty_Pallet([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("pallet_destination") && !requestData.ContainsKey("_categogy"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_CheckQty_Pallet), CommandType.StoredProcedure, requestData["pallet_destination"], requestData["_categogy"])
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

        //Tally_check_gopghepPallet_FA
        [HttpPost]
        [Route("Tally_check_gopghepPallet_FA")]
        public async Task<IActionResult> Tally_check_gopghepPallet_FA([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("typecategogy") && !requestData.ContainsKey("pallet_source") && !requestData.ContainsKey("pallet_destination"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(Tally_check_gopghepPallet_FA), CommandType.StoredProcedure, requestData["typecategogy"], requestData["pallet_source"], requestData["pallet_destination"])
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

        //query_update_pallet   // stored nay viet sau
        //[HttpPost]
        //[Route("query_update_pallet")]
        //public async Task<IActionResult> query_update_pallet([FromBody] Dictionary<string, string> requestData)
        //{
        //    TallysheetSCM tallysheetScm = this;
        //    try
        //    {
        //        if (!requestData.ContainsKey("typecategogy") || !requestData.ContainsKey("Source_pallet") || !requestData.ContainsKey("Destination_pallet") || !requestData.ContainsKey("sModelNo"))
        //            throw new ValidationException("API err");
        //        bool flag = await Task.FromResult<int>(DataConneFoss1.ExcuteStored_int(nameof(query_update_pallet), new string[4]
        //        {
        //  "@typecategogy",
        //  "@Source_pallet",
        //  "@Destination_pallet",
        //  "@sModelNo"
        //        }, new object[4]
        //        {
        //  (object) requestData["typecategogy"],
        //  (object) requestData["Source_pallet"],
        //  (object) requestData["Destination_pallet"],
        //  (object) requestData["sModelNo"]
        //        })) > 0;
        //        return (IActionResult)tallysheetScm.Ok((object)flag);
        //    }
        //    catch (Exception ex)
        //    {
        //        return (IActionResult)tallysheetScm.StatusCode(500, (object)("Internal server error: " + ex.Message));
        //    }
        //}

        //SP_QmodelPalletC
        [HttpPost]
        [Route("SP_QmodelPalletC")]
        public async Task<IActionResult> SP_QmodelPalletC([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("pallet") && !requestData.ContainsKey("cat") )
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(SP_QmodelPalletC), CommandType.StoredProcedure, requestData["pallet"], requestData["cat"])
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

        //SP_UpdateSerialPallet
        [HttpPost]
        [Route("SP_UpdateSerialPallet")]
        public async Task<IActionResult> SP_UpdateSerialPallet([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("dich") && !requestData.ContainsKey("nguon") && !requestData.ContainsKey("serial") && !requestData.ContainsKey("user")
                    && !requestData.ContainsKey("cat") && !requestData.ContainsKey("model") && !requestData.ContainsKey("type"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(SP_UpdateSerialPallet), CommandType.StoredProcedure, requestData["dich"], requestData["nguon"], 
                    requestData["serial"], requestData["user"], requestData["cat"], requestData["model"], requestData["type"])
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

        //spCheckQtyByPalletID_PalletID
        [HttpPost]
        [Route("spCheckQtyByPalletID_PalletID")]
        public async Task<IActionResult> spCheckQtyByPalletID_PalletID([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("PALLET_ID") && !requestData.ContainsKey("Category") && !requestData.ContainsKey("employee_id") && !requestData.ContainsKey("createdate") )
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataConneFoss1.StoreFillDS(nameof(spCheckQtyByPalletID_PalletID), CommandType.StoredProcedure, requestData["PALLET_ID"], requestData["Category"],
                    requestData["employee_id"], requestData["createdate"])
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

        // link sang foss **** // stored nay viet sau
        [HttpPost]
        [Route("tblQCBorrowLog_Insert_PalletID")]
        public async Task<IActionResult> tblQCBorrowLog_Insert_PalletID(
        [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                if (!requestData.ContainsKey("sModelNo")
                    || !requestData.ContainsKey("sSerial")
                    || !requestData.ContainsKey("sBorrowedBy")
                    || !requestData.ContainsKey("bIsReturn")
                    || !requestData.ContainsKey("sPalletNo")
                    || !requestData.ContainsKey("Category"))
                {
                    return BadRequest("API null request!");
                }

                string str = await Task.FromResult(
                    DataConneFoss1.GetExcuteScalar_string(
                        nameof(tblQCBorrowLog_Insert_PalletID),
                        new string[]
                        {
                    "@sModelNo",
                    "@sSerial",
                    "@sBorrowedBy",
                    "@bIsReturn",
                    "@sPalletNo",
                    "@Category"
                        },
                        new object[]
                        {
                    requestData["sModelNo"],
                    requestData["sSerial"],
                    requestData["sBorrowedBy"],
                    int.Parse(requestData["bIsReturn"]),
                    requestData["sPalletNo"],
                    requestData["Category"]
                        }
                    )
                );

                return Ok(str);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        //FactCode_GetAll
        [HttpPost]
        [Route("FactCode_GetAll")]
        public async Task<IActionResult> FactCode_GetAll([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                //if (!requestData.ContainsKey("pallet") && !requestData.ContainsKey("cat"))
                //{
                //    return BadRequest("Missing 'userid' in request data.");
                //}

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(FactCode_GetAll), CommandType.StoredProcedure)
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

        // link sang foss **** // stored nay viet sau
        [HttpPost]
        [Route("PCS_Remove_Serial_PalletID")]
        public async Task<IActionResult> PCS_Remove_Serial_PalletID(
        [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Validate request
                if (!requestData.ContainsKey("Model")
                    || !requestData.ContainsKey("Model_Fax")
                    || !requestData.ContainsKey("Serial")
                    || !requestData.ContainsKey("PalletID")
                    || !requestData.ContainsKey("Userlogin")
                    || !requestData.ContainsKey("category"))
                {
                    return BadRequest("API null request!");
                }

                string str = await Task.FromResult(
                    DataConneFoss1.GetExcuteScalar_string(
                        nameof(PCS_Remove_Serial_PalletID),
                        new string[]
                        {
                    "@Model",
                    "@Model_Fax",
                    "@Serial",
                    "@PalletID",
                    "@Userlogin",
                    "@category"
                        },
                        new object[]
                        {
                    requestData["Model"],
                    requestData["Model_Fax"],
                    requestData["Serial"],
                    requestData["PalletID"],
                    requestData["Userlogin"],
                    requestData["category"]
                        }
                    )
                );

                return Ok(str);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }



        //query_select_outer    // link sang foss **** // stored nay viet sau
        [HttpPost]
        [Route("query_select_outer")]
        public async Task<IActionResult> query_select_outer([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("typecategogy") && !requestData.ContainsKey("source_pallet") && !requestData.ContainsKey("seri_outer"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataConneFoss1.StoreFillDS(nameof(query_select_outer), CommandType.StoredProcedure, requestData["typecategogy"], requestData["source_pallet"], requestData["seri_outer"])
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

        //qurery_check_outer
        [HttpPost]
        [Route("qurery_check_outer")]
        public async Task<IActionResult> qurery_check_outer(
        [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Validate request
                if (!requestData.ContainsKey("iduser")
                    || !requestData.ContainsKey("typecategogy")
                    || !requestData.ContainsKey("palletid")
                    || !requestData.ContainsKey("seri_outer"))
                {
                    return BadRequest("API null request!");
                }

                string str = await Task.FromResult(
                    DataConneFoss1.GetExcuteScalar_string(
                        nameof(qurery_check_outer),
                        new string[]
                        {
                    "@iduser",
                    "@typecategogy",
                    "@palletid",
                    "@seri_outer"
                        },
                        new object[]
                        {
                    requestData["iduser"],
                    requestData["typecategogy"],
                    requestData["palletid"],
                    requestData["seri_outer"]
                        }
                    )
                );

                return Ok(str);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }



        //======================// end //==========================
        [HttpPost]
        [Route("API_Get_Model_ServicePartOversea")]
        public async Task<IActionResult> API_Get_Model_ServicePartOversea([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("barcode"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(API_Get_Model_ServicePartOversea), CommandType.StoredProcedure, requestData["barcode"])
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
        [Route("SPQmodelPalletC")]
        public async Task<IActionResult> SPQmodelPalletC([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra xem requestData có chứa key "userid" hay không
                if (!requestData.ContainsKey("pallet") && !requestData.ContainsKey("cat"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                // Gọi phương thức để lấy dữ liệu từ cơ sở dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectTally.StoreFillDS(nameof(SP_QmodelPalletC), CommandType.StoredProcedure, requestData["pallet"], requestData["cat"])
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
