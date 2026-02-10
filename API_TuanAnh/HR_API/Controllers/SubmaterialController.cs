using HR_API.APP_Start;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace HR_API.Controllers
{
    public class SubmaterialController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpPost]
        [Route("Query_Login_Sub")]
        public async Task<IActionResult> Query_Login_Sub(
            [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                if (!requestData.ContainsKey("userid"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                DataTable table = await Task.FromResult(DataconnectSub.StoreFillDS("Query_Login_Sub", CommandType.StoredProcedure, requestData["userid"])
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
        [Route("query_Cate")]
        public async Task<IActionResult> query_Cate(
    [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                DataTable table = await Task.FromResult(
                    DataconnectSub.StoreFillDS(
                        "query_Cate",
                        CommandType.StoredProcedure
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
        [Route("query_Position")]
        public async Task<IActionResult> query_Position(
    [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Check required keys
                if (!requestData.ContainsKey("idposition"))
                {
                    return BadRequest("Missing 'idposition' in request data.");
                }

                if (!requestData.ContainsKey("plantid"))
                {
                    return BadRequest("Missing 'plantid' in request data.");
                }

                DataTable table = await Task.FromResult(
                    DataconnectSub.StoreFillDS(
                        "query_Position",
                        CommandType.StoredProcedure,
                        requestData["idposition"],
                        requestData["plantid"]
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
        [Route("SP_M_PUR_GOODS_MVNT")]
        public async Task<IActionResult> SP_M_PUR_GOODS_MVNT(
    [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Required keys
                string[] requiredKeys =
                {
            "ITEM_CD",
            "SL_CD",
            "TRNS_SL_CD",
            "TRNS_QTY",
            "MOV_CD",
            "PLANT_CD",
            "CAT_CD",
            "LOC_CD",
            "INSERT_USER",
            "UPDATE_USER"
        };

                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                // Parse quantity
                if (!int.TryParse(requestData["TRNS_QTY"], out int trnsQty))
                {
                    return BadRequest("TRNS_QTY must be an integer.");
                }

                string result = await Task.FromResult(
                    DataconnectSub.GetExcuteScalar_string(
                        "SP_M_PUR_GOODS_MVNT",
                        new string[]
                        {
                    "@ITEM_CD",
                    "@SL_CD",
                    "@TRNS_SL_CD",
                    "@TRNS_QTY",
                    "@MOV_CD",
                    "@PLANT_CD",
                    "@CAT_CD",
                    "@LOC_CD",
                    "@INSERT_USER",
                    "@UPDATE_USER"
                        },
                        new object[]
                        {
                    requestData["ITEM_CD"],
                    requestData["SL_CD"],
                    requestData["TRNS_SL_CD"],
                    trnsQty,
                    requestData["MOV_CD"],
                    requestData["PLANT_CD"],
                    requestData["CAT_CD"],
                    requestData["LOC_CD"],
                    requestData["INSERT_USER"],
                    requestData["UPDATE_USER"]
                        }
                    )
                );

                // 👉 Trả plain text cho Flutter (0 / 1 / message)
                return Content(result ?? "0", "text/plain");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Query_Check_Status")]
        public async Task<IActionResult> Query_Check_Status(
    [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Validate required key
                if (!requestData.ContainsKey("orderid"))
                {
                    return BadRequest("Missing 'orderid' in request data.");
                }

                DataTable table = await Task.FromResult(
                    DataconnectSub.StoreFillDS(
                        "Query_Check_Status",
                        CommandType.StoredProcedure,
                        requestData["orderid"]
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
        [Route("Query_Doc_NO")]
        public async Task<IActionResult> Query_Doc_NO([FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                DataTable table = await Task.FromResult(
                    DataconnectSub.StoreFillDS(
                        "Query_Doc_NO",
                        CommandType.StoredProcedure
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
        [Route("SP_I_GOODS_MVNT")]
        public async Task<IActionResult> SP_I_GOODS_MVNT(
    [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Validate required keys
                string[] requiredKeys =
                {
            "DOC_NO_",
            "ITEM_CD",
            "SL_CD",
            "TRNS_SL_CD",
            "TRNS_QTY",
            "ORDER_ID",
            "MOV_CD",
            "PLANT_CD",
            "CAT_CD",
            "LOC_CD",
            "INSERT_USER",
            "UPDATE_USER"
        };

                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                        return BadRequest($"Missing '{key}' in request data.");
                }

                // Parse numeric values
                if (!float.TryParse(requestData["TRNS_QTY"], out float trnsQty))
                    return BadRequest("TRNS_QTY must be a number.");

                if (!int.TryParse(requestData["ORDER_ID"], out int orderId))
                    return BadRequest("ORDER_ID must be an integer.");

                // Execute stored procedure
                string result = await Task.FromResult(
                    DataconnectSub.GetExcuteScalar_string(
                        "SP_I_GOODS_MVNT",
                        new string[]
                        {
                    "@DOC_NO_",
                    "@ITEM_CD",
                    "@SL_CD",
                    "@TRNS_SL_CD",
                    "@TRNS_QTY",
                    "@ORDER_ID",
                    "@MOV_CD",
                    "@PLANT_CD",
                    "@CAT_CD",
                    "@LOC_CD",
                    "@INSERT_USER",
                    "@UPDATE_USER"
                        },
                        new object[]
                        {
                    requestData["DOC_NO_"],
                    requestData["ITEM_CD"],
                    requestData["SL_CD"],
                    requestData["TRNS_SL_CD"],
                    trnsQty,
                    orderId,
                    requestData["MOV_CD"],
                    requestData["PLANT_CD"],
                    requestData["CAT_CD"],
                    requestData["LOC_CD"],
                    requestData["INSERT_USER"],
                    requestData["UPDATE_USER"]
                        }
                    )
                );

                // Return plain text (0 / 1 / message)
                return Content(result ?? "0", "text/plain");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        private string DataTableToJson(DataTable table)
        {
            return JsonConvert.SerializeObject(table);
        }
    }
}
