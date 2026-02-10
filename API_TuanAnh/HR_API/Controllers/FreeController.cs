
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
    public class FreeController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        //private object ex;

        [HttpPost]
        [Route("Query_Login")]
        public async Task<IActionResult> Query_Login([FromBody] Dictionary<string, string> requestData)
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
                    DataconnectFreeL.StoreFillDS(nameof(Query_Login), CommandType.StoredProcedure, requestData["userid"])
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

        //04.02.2026 == > chuyen API sang con server 131
        [HttpPost]
        [Route("Query_thongtinbarcode")]
        public async Task<IActionResult> Query_thongtinbarcode([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (!requestData.ContainsKey("chuoibarcode") || !requestData.ContainsKey("typerecheck"))
                {
                    return BadRequest("Missing 'chuoibarcode' or 'typerecheck' in request data.");
                }

                // Gọi stored procedure lấy dữ liệu
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Query_thongtinbarcode),CommandType.StoredProcedure,requestData["chuoibarcode"],requestData["typerecheck"])
                );

                // Chuyển DataTable sang JSON
                string json = DataTableToJson(table);

                // Trả kết quả
                return Ok(json);
            }
            catch (Exception ex)
            {
                // Trả lỗi 500 nếu có exception
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_CheckUnitbox")]
        public async Task<IActionResult> Query_CheckUnitbox([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (!requestData.ContainsKey("chuoibarcode"))
                {
                    return BadRequest("Missing 'chuoibarcode' in request data.");
                }
                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Query_CheckUnitbox),
                        CommandType.StoredProcedure,
                        requestData["chuoibarcode"]
                    )
                );
                // Chuyển DataTable sang JSON
                string json = DataTableToJson(table);

                // Trả kết quả
                return Ok(json);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_thongtinbarcode2")]
        public async Task<IActionResult> Query_thongtinbarcode2([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (!requestData.ContainsKey("chuoibarcode"))
                {
                    return BadRequest("Missing 'chuoibarcode' in request data.");
                }

                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Query_thongtinbarcode2),
                        CommandType.StoredProcedure,
                        requestData["chuoibarcode"]
                    )
                );

                // Chuyển DataTable sang JSON
                string json = DataTableToJson(table);

                // Trả kết quả
                return Ok(json);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_thongtinreceivingcard")]
        public async Task<IActionResult> Query_thongtinreceivingcard([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (!requestData.ContainsKey("chuoibarcode") || !requestData.ContainsKey("typerecheck"))
                {
                    return BadRequest("Missing 'chuoibarcode' or 'typerecheck' in request data.");
                }

                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Query_thongtinreceivingcard),
                        CommandType.StoredProcedure,
                        requestData["chuoibarcode"],
                        requestData["typerecheck"]
                    )
                );

                // Chuyển DataTable sang JSON
                string json = DataTableToJson(table);

                // Trả kết quả
                return Ok(json);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_update_check_QC")]
        public async Task<IActionResult> Query_update_check_QC([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Danh sách các key bắt buộc
                string[] requiredKeys =
                {
            "ID", "codate", "remark",
            "soluonghuy_spl", "soluonghuy_rohs",
            "user_finished_spl", "user_finished_rohs",
            "user_check_spl", "user_check_rohs",
            "ketqua_spl", "ketqua_rohs",
            "kieucheck", "user_dangnhap",
            "trangthai_check_sql", "trangthai_check_rohs",
            "trangthai_TTcheck", "invoice_",
            "SLNG", "typerecheck", "check1lan", "barcodebox"
        };

                // Kiểm tra dữ liệu đầu vào
                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Query_update_check_QC),
                        CommandType.StoredProcedure,
                        requestData["ID"],
                        requestData["codate"],
                        requestData["remark"],
                        requestData["soluonghuy_spl"],
                        requestData["soluonghuy_rohs"],
                        requestData["user_finished_spl"],
                        requestData["user_finished_rohs"],
                        requestData["user_check_spl"],
                        requestData["user_check_rohs"],
                        requestData["ketqua_spl"],
                        requestData["ketqua_rohs"],
                        requestData["kieucheck"],
                        requestData["user_dangnhap"],
                        requestData["trangthai_check_sql"],
                        requestData["trangthai_check_rohs"],
                        requestData["trangthai_TTcheck"],
                        requestData["invoice_"],
                        requestData["SLNG"],
                        requestData["typerecheck"],
                        requestData["check1lan"],
                        requestData["barcodebox"]
                    )
                );

                // Chuyển DataTable sang JSON
                string json = DataTableToJson(table);

                // Trả kết quả
                return Ok(json);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_update_check_QC_1nam")]
        public async Task<IActionResult> Query_update_check_QC_1nam([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Danh sách key bắt buộc
                string[] requiredKeys =
                {
            "ID", "codate", "remark",
            "soluonghuy_spl", "soluonghuy_rohs",
            "user_finished_spl", "user_finished_rohs",
            "user_check_spl", "user_check_rohs",
            "ketqua_spl", "ketqua_rohs",
            "kieucheck", "user_dangnhap",
            "trangthai_check_sql", "trangthai_check_rohs",
            "trangthai_TTcheck", "invoice_",
            "SLNG", "typerecheck", "check1lan"
        };

                // Validate dữ liệu đầu vào
                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Query_update_check_QC_1nam),
                        CommandType.StoredProcedure,
                        requestData["ID"],
                        requestData["codate"],
                        requestData["remark"],
                        requestData["soluonghuy_spl"],
                        requestData["soluonghuy_rohs"],
                        requestData["user_finished_spl"],
                        requestData["user_finished_rohs"],
                        requestData["user_check_spl"],
                        requestData["user_check_rohs"],
                        requestData["ketqua_spl"],
                        requestData["ketqua_rohs"],
                        requestData["kieucheck"],
                        requestData["user_dangnhap"],
                        requestData["trangthai_check_sql"],
                        requestData["trangthai_check_rohs"],
                        requestData["trangthai_TTcheck"],
                        requestData["invoice_"],
                        requestData["SLNG"],
                        requestData["typerecheck"],
                        requestData["check1lan"]
                    )
                );

                // Convert DataTable to JSON
                string json = DataTableToJson(table);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_update_check_QC_recheck")]
        public async Task<IActionResult> Query_update_check_QC_recheck([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Danh sách key bắt buộc
                string[] requiredKeys =
                {
            "ID", "codate", "remark",
            "soluonghuy_spl", "soluonghuy_rohs",
            "user_finished_spl", "user_finished_rohs",
            "user_check_spl", "user_check_rohs",
            "ketqua_spl", "ketqua_rohs",
            "kieucheck", "user_dangnhap",
            "trangthai_check_sql", "trangthai_check_rohs",
            "trangthai_TTcheck", "invoice_",
            "SLNG", "typerecheck", "check1lan"
        };

                // Validate dữ liệu đầu vào
                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Query_update_check_QC_recheck),
                        CommandType.StoredProcedure,
                        requestData["ID"],
                        requestData["codate"],
                        requestData["remark"],
                        requestData["soluonghuy_spl"],
                        requestData["soluonghuy_rohs"],
                        requestData["user_finished_spl"],
                        requestData["user_finished_rohs"],
                        requestData["user_check_spl"],
                        requestData["user_check_rohs"],
                        requestData["ketqua_spl"],
                        requestData["ketqua_rohs"],
                        requestData["kieucheck"],
                        requestData["user_dangnhap"],
                        requestData["trangthai_check_sql"],
                        requestData["trangthai_check_rohs"],
                        requestData["trangthai_TTcheck"],
                        requestData["invoice_"],
                        requestData["SLNG"],
                        requestData["typerecheck"],
                        requestData["check1lan"]
                    )
                );

                // Convert DataTable → JSON
                string json = DataTableToJson(table);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_insert_recheck")]
        public async Task<IActionResult> Query_insert_recheck([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Danh sách key bắt buộc
                string[] requiredKeys =
                {
            "barcode", "mahang", "vitri", "soluong",
            "plant", "deliverydate", "dano", "pono",
            "vender", "ctrkey", "ctrt", "sloc",
            "lotdate", "CateQC", "invoice", "idrecheck",
            "codate", "remark", "createuser", "typerecheck"
        };

                // Validate dữ liệu đầu vào
                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Query_insert_recheck),
                        CommandType.StoredProcedure,
                        requestData["barcode"],
                        requestData["mahang"],
                        requestData["vitri"],
                        requestData["soluong"],
                        requestData["plant"],
                        requestData["deliverydate"],
                        requestData["dano"],
                        requestData["pono"],
                        requestData["vender"],
                        requestData["ctrkey"],
                        requestData["ctrt"],
                        requestData["sloc"],
                        requestData["lotdate"],
                        requestData["CateQC"],
                        requestData["invoice"],
                        requestData["idrecheck"],
                        requestData["codate"],
                        requestData["remark"],
                        requestData["createuser"],
                        requestData["typerecheck"]
                    )
                );

                // Convert DataTable → JSON
                string json = DataTableToJson(table);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_insert_recheck2")]
        public async Task<IActionResult> Query_insert_recheck2([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Danh sách key bắt buộc
                string[] requiredKeys =
                {
            "barcode", "mahang", "vitri", "soluong",
            "plant", "deliverydate", "dano", "pono",
            "vender", "ctrkey", "ctrt", "sloc",
            "lotdate", "CateQC", "invoice", "idrecheck",
            "codate", "remark", "createuser", "typerecheck"
        };

                // Validate dữ liệu đầu vào
                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Query_insert_recheck2),
                        CommandType.StoredProcedure,
                        requestData["barcode"],
                        requestData["mahang"],
                        requestData["vitri"],
                        requestData["soluong"],
                        requestData["plant"],
                        requestData["deliverydate"],
                        requestData["dano"],
                        requestData["pono"],
                        requestData["vender"],
                        requestData["ctrkey"],
                        requestData["ctrt"],
                        requestData["sloc"],
                        requestData["lotdate"],
                        requestData["CateQC"],
                        requestData["invoice"],
                        requestData["idrecheck"],
                        requestData["codate"],
                        requestData["remark"],
                        requestData["createuser"],
                        requestData["typerecheck"]
                    )
                );

                // Convert DataTable → JSON
                string json = DataTableToJson(table);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_insert_recheck3")]
        public async Task<IActionResult> Query_insert_recheck3([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Danh sách key bắt buộc
                string[] requiredKeys =
                {
            "barcode", "mahang", "vitri", "soluong",
            "plant", "deliverydate", "dano", "pono",
            "vender", "ctrkey", "ctrt", "sloc",
            "lotdate", "CateQC", "invoice", "idrecheck",
            "codate", "remark", "createuser", "typerecheck",
            "_typecheck", "usercheck"
        };

                // Validate dữ liệu đầu vào
                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Query_insert_recheck3),
                        CommandType.StoredProcedure,
                        requestData["barcode"],
                        requestData["mahang"],
                        requestData["vitri"],
                        requestData["soluong"],
                        requestData["plant"],
                        requestData["deliverydate"],
                        requestData["dano"],
                        requestData["pono"],
                        requestData["vender"],
                        requestData["ctrkey"],
                        requestData["ctrt"],
                        requestData["sloc"],
                        requestData["lotdate"],
                        requestData["CateQC"],
                        requestData["invoice"],
                        requestData["idrecheck"],
                        requestData["codate"],
                        requestData["remark"],
                        requestData["createuser"],
                        requestData["typerecheck"],
                        requestData["_typecheck"],
                        requestData["usercheck"]
                    )
                );

                // Convert DataTable → JSON
                string json = DataTableToJson(table);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_insert_recheck4")]
        public async Task<IActionResult> Query_insert_recheck4([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Danh sách key bắt buộc
                string[] requiredKeys =
                {
            "barcode", "mahang", "vitri", "soluong",
            "plant", "deliverydate", "dano", "pono",
            "vender", "ctrkey", "ctrt", "sloc",
            "lotdate", "CateQC", "invoice", "idrecheck",
            "codate", "remark", "createuser", "typerecheck",
            "_typecheck", "usercheck"
        };

                // Validate dữ liệu đầu vào
                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Query_insert_recheck4),
                        CommandType.StoredProcedure,
                        requestData["barcode"],
                        requestData["mahang"],
                        requestData["vitri"],
                        requestData["soluong"],
                        requestData["plant"],
                        requestData["deliverydate"],
                        requestData["dano"],
                        requestData["pono"],
                        requestData["vender"],
                        requestData["ctrkey"],
                        requestData["ctrt"],
                        requestData["sloc"],
                        requestData["lotdate"],
                        requestData["CateQC"],
                        requestData["invoice"],
                        requestData["idrecheck"],
                        requestData["codate"],
                        requestData["remark"],
                        requestData["createuser"],
                        requestData["typerecheck"],
                        requestData["_typecheck"],
                        requestData["usercheck"]
                    )
                );

                // Convert DataTable → JSON
                string json = DataTableToJson(table);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_insert_recheck6")]
        public async Task<IActionResult> Query_insert_recheck6([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Danh sách key bắt buộc
                string[] requiredKeys =
                {
            "barcode", "mahang", "vitri", "soluong",
            "plant", "deliverydate", "dano", "pono",
            "vender", "ctrkey", "ctrt", "sloc",
            "lotdate", "CateQC", "invoice", "idrecheck",
            "codate", "remark", "createuser", "typerecheck",
            "_typecheck", "usercheck", "kequacheck"
        };

                // Validate dữ liệu đầu vào
                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Query_insert_recheck6),
                        CommandType.StoredProcedure,
                        requestData["barcode"],
                        requestData["mahang"],
                        requestData["vitri"],
                        requestData["soluong"],
                        requestData["plant"],
                        requestData["deliverydate"],
                        requestData["dano"],
                        requestData["pono"],
                        requestData["vender"],
                        requestData["ctrkey"],
                        requestData["ctrt"],
                        requestData["sloc"],
                        requestData["lotdate"],
                        requestData["CateQC"],
                        requestData["invoice"],
                        requestData["idrecheck"],
                        requestData["codate"],
                        requestData["remark"],
                        requestData["createuser"],
                        requestData["typerecheck"],
                        requestData["_typecheck"],
                        requestData["usercheck"],
                        requestData["kequacheck"]
                    )
                );

                // Convert DataTable → JSON
                string json = DataTableToJson(table);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_insert_recheck5")]
        public async Task<IActionResult> Query_insert_recheck5([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Danh sách key bắt buộc
                string[] requiredKeys =
                {
            "barcode", "mahang", "vitri", "soluong",
            "plant", "deliverydate", "dano", "pono",
            "vender", "ctrkey", "ctrt", "sloc",
            "lotdate", "CateQC", "invoice", "idrecheck",
            "codate", "remark", "createuser", "typerecheck",
            "_typecheck", "usercheck", "soluongNG"
        };

                // Validate dữ liệu đầu vào
                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Query_insert_recheck5),
                        CommandType.StoredProcedure,
                        requestData["barcode"],
                        requestData["mahang"],
                        requestData["vitri"],
                        requestData["soluong"],
                        requestData["plant"],
                        requestData["deliverydate"],
                        requestData["dano"],
                        requestData["pono"],
                        requestData["vender"],
                        requestData["ctrkey"],
                        requestData["ctrt"],
                        requestData["sloc"],
                        requestData["lotdate"],
                        requestData["CateQC"],
                        requestData["invoice"],
                        requestData["idrecheck"],
                        requestData["codate"],
                        requestData["remark"],
                        requestData["createuser"],
                        requestData["typerecheck"],
                        requestData["_typecheck"],
                        requestData["usercheck"],
                        requestData["soluongNG"]
                    )
                );

                // Convert DataTable → JSON
                string json = DataTableToJson(table);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_update_qtyrecheck")]
        public async Task<IActionResult> Query_update_qtyrecheck([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra input
                if (!requestData.ContainsKey("bacodeid") || !requestData.ContainsKey("soluonginput") ||
                    !requestData.ContainsKey("typerecheck") || !requestData.ContainsKey("userupdate"))
                    return BadRequest("Missing parameters");

                // Thực hiện stored procedure
                bool flag = await Task.FromResult<int>(
                    DataconnectFreeL.ExcuteStored_int(
                        nameof(Query_update_qtyrecheck),
                        new string[] { "@bacodeid", "@soluonginput", "@typerecheck", "@userupdate" },
                        new object[] { requestData["bacodeid"], requestData["soluonginput"], requestData["typerecheck"], requestData["userupdate"] }
                    )
                ) > 0;

                // Trả về string 'true' hoặc 'false'
                return Ok(flag ? "true" : "false");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Freelocation_get_recheck")]
        public async Task<IActionResult> Freelocation_get_recheck([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Lấy dữ liệu từ database
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(nameof(Freelocation_get_recheck), CommandType.StoredProcedure)
                );

                // Nếu không có dữ liệu, trả về mảng rỗng []
                if (table.Rows.Count == 0)
                {
                    return Ok("[]"); // trả về string JSON array rỗng
                }

                // Chuyển DataTable thành JSON
                string json = DataTableToJson(table);

                // Trả về JSON
                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Freelocation_delete_inspection_mobile")]
        public async Task<IActionResult> Freelocation_delete_inspection_mobile([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Kiểm tra các tham số bắt buộc
                string[] requiredKeys = { "IDmahang", "barcode", "cateQC", "codedate", "remark", "userid" };
                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing parameter: {key}");
                    }
                }

                // Gọi stored procedure
                DataTable table = await Task.FromResult<DataTable>(
                    DataconnectFreeL.StoreFillDS(
                        nameof(Freelocation_delete_inspection_mobile),
                        CommandType.StoredProcedure,
                        (object)requestData["IDmahang"],
                        (object)requestData["barcode"],
                        (object)requestData["cateQC"],
                        (object)requestData["codedate"],
                        (object)requestData["remark"],
                        (object)requestData["userid"]
                    )
                );

                // Nếu không có dữ liệu, trả về mảng rỗng []
                if (table.Rows.Count == 0)
                {
                    return Ok("[]");
                }

                // Chuyển DataTable thành JSON
                string json = DataTableToJson(table);

                // Trả về JSON
                return Ok(json);
            }
            catch (Exception ex)
            {
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
