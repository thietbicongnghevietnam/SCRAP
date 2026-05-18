using HR_API.APP_Start;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;
using System.Text;

namespace HR_API.Controllers
{
    public class SMSVersion3Controller : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        [Route("CreateMailAlert")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> CreateMailAlert([FromBody] MailAlert mail)
        {
            try
            {
                if (mail == null)
                    return BadRequest("No Data");

                DataconnectSMS.excutenonquerry(
                    "CreateMailAlert",
                    System.Data.CommandType.StoredProcedure,
                    mail.Title,
                    mail.Contents,
                    mail.Mail_To,
                    mail.Mail_CC
                );

                return Ok(mail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMailAlert")]
        public IActionResult GetMailAlert()
        {
            try
            {
                DataTable dt = DataconnectSMS.StoreFillDS(
                    "GetMailAlert",
                    System.Data.CommandType.StoredProcedure
                );

                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //http://10.92.184.22:8036/swagger/index.html
        [HttpPost]
        [Route("SendMailAlertWithAttachment")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> SendMailAlertWithAttachment([FromForm] MailRequestForm request)
        {
            try
            {
                if (request == null)
                    return BadRequest("No data");

                var smtpClient = new System.Net.Mail.SmtpClient("157.8.1.131");

                var systemName = string.IsNullOrWhiteSpace(request.SystemName)
                ? "ECN system"
                : request.SystemName.Trim();

                //var fromMail = new System.Net.Mail.MailAddress("psnv.isg@vn.panasonic.com", "ECN system");

                var fromMail = new System.Net.Mail.MailAddress("psnv.isg@vn.panasonic.com", systemName);

                using var message = new System.Net.Mail.MailMessage();
                message.From = fromMail;
                message.Subject = request.Title;
                message.Body = request.HtmlBody;
                message.IsBodyHtml = true;

                // TO
                if (!string.IsNullOrEmpty(request.Mail_To))
                {
                    foreach (var email in request.Mail_To.Split(';'))
                        message.To.Add(email.Trim());
                }

                // CC
                if (!string.IsNullOrEmpty(request.Mail_CC))
                {
                    foreach (var email in request.Mail_CC.Split(';'))
                        message.CC.Add(email.Trim());
                }

                // File attachment
                if (request.File != null && request.File.Length > 0)
                {
                    var ms = new MemoryStream();
                    await request.File.CopyToAsync(ms);
                    ms.Position = 0;

                    var attachment = new System.Net.Mail.Attachment(ms, request.File.FileName);
                    message.Attachments.Add(attachment);

                    // Không dispose stream
                }

                await smtpClient.SendMailAsync(message);

                //return Ok("Send Mail Success");
                //return Ok(true);   // gửi thành công
                return Ok(new
                {
                    success = true,
                    message = "Send mail success"
                });
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                //return Ok(false);  // gửi lỗi
                return BadRequest(new
                {
                    success = false,
                    message = "Send mail failed",
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        public class MailAlert
        {
            public string Title { get; set; }
            public string Contents { get; set; }
            public string Mail_To { get; set; }
            public string Mail_CC { get; set; }
            public string? DatetimeCreate { get; set; }
        }

        public class MailRequestForm
        {
            [FromForm(Name = "Title")]
            public string Title { get; set; }

            [FromForm(Name = "Mail_To")]
            public string Mail_To { get; set; }

            [FromForm(Name = "Mail_CC")]
            public string Mail_CC { get; set; }

            [FromForm(Name = "HtmlBody")]
            public string HtmlBody { get; set; }   //danh sach dang Html

            [FromForm(Name = "file")]
            public IFormFile File { get; set; } // file đính kèm (tùy chọn)

            public string? SystemName { get; set; } // thêm dòng này
        }



    }
}
