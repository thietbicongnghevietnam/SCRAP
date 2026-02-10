using Microsoft.AspNetCore.Mvc;
using HR_API.APP_Start;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;

namespace HR_API.Controllers
{
    public class SmtDipController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        [Route("Query_Login_SMT")]
        public async Task<IActionResult> Query_Login_SMT(
            [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                if (!requestData.ContainsKey("userid"))
                {
                    return BadRequest("Missing 'userid' in request data.");
                }

                DataTable table = await Task.FromResult(DataConnMCS.StoreFillDS("Query_Login_SMT",CommandType.StoredProcedure,requestData["userid"])
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
        [Route("Query_insert_temppartcard")]
        public async Task<IActionResult> Query_insert_temppartcard(
    [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Check required keys
                string[] requiredKeys = new string[]
                {
            "Linename",
            "Modelname",
            "Deliverydate",
            "idscan",
            "IDkittinglist"
                };

                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                DataTable table = await Task.FromResult(
                    DataConnMCS.StoreFillDS(
                        "Query_insert_temppartcard",
                        CommandType.StoredProcedure,
                        requestData["Linename"],
                        requestData["Modelname"],
                        requestData["Deliverydate"],
                        requestData["idscan"],
                        requestData["IDkittinglist"]
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
        [Route("Query_check_temppartcard")]
        public async Task<IActionResult> Query_check_temppartcard(
    [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Check required keys
                string[] requiredKeys = new string[]
                {
            "Linename",
            "Modelname",
            "Deliverydate",
            "partcode"
                };

                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                DataTable table = await Task.FromResult(
                    DataConnMCS.StoreFillDS(
                        "Query_check_temppartcard",
                        CommandType.StoredProcedure,
                        requestData["Linename"],
                        requestData["Modelname"],
                        requestData["Deliverydate"],
                        requestData["partcode"]
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
        [Route("Query_update_temppartcard")]
        public async Task<IActionResult> Query_update_temppartcard(
    [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Check required keys
                string[] requiredKeys = new string[]
                {
            "Linename",
            "Modelname",
            "partcode",
            "Deliverydate",
            "idscan"
                };

                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                DataTable table = await Task.FromResult(
                    DataConnMCS.StoreFillDS(
                        "Query_update_temppartcard",
                        CommandType.StoredProcedure,
                        requestData["Linename"],
                        requestData["Modelname"],
                        requestData["partcode"],
                        requestData["Deliverydate"],
                        requestData["idscan"]
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
        [Route("Query_delete_temppartcard")]
        public async Task<IActionResult> Query_delete_temppartcard(
    [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Check required key
                if (!requestData.ContainsKey("Linename"))
                {
                    return BadRequest("Missing 'Linename' in request data.");
                }

                DataTable table = await Task.FromResult(
                    DataConnMCS.StoreFillDS(
                        "Query_delete_temppartcard",
                        CommandType.StoredProcedure,
                        requestData["Linename"]
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
        [Route("Query_checkitting_SMT")]
        public async Task<IActionResult> Query_checkitting_SMT(
    [FromBody] Dictionary<string, string> requestData)
        {
            try
            {
                // Validate required keys
                string[] requiredKeys =
                {
            "Linename",
            "Modelname",
            "Deliverydate",
            "IDkittinglist"
        };

                foreach (var key in requiredKeys)
                {
                    if (!requestData.ContainsKey(key))
                    {
                        return BadRequest($"Missing '{key}' in request data.");
                    }
                }

                DataTable table = await Task.FromResult(
                    DataConnMCS.StoreFillDS(
                        "Query_checkitting_SMT",
                        CommandType.StoredProcedure,
                        requestData["Linename"],
                        requestData["Modelname"],
                        requestData["Deliverydate"],
                        requestData["IDkittinglist"]
                    )
                );

                // Default = 0
                string result = "0";

                if (table.Rows.Count > 0 && table.Columns.Count > 0)
                {
                    result = table.Rows[0][0].ToString(); // "0" hoặc "1"
                }

                // Trả plain text cho Flutter
                return Content(result, "text/plain");
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
