using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MTS_CRM.MVC.Controllers
{
    [RoutePrefix("api/email")]
    public class EmailController : ApiController
    {
        [HttpPost]
        [Route("send-email")]
        public async Task SendEmail([FromBody]JObject objData)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(objData["ToName"].ToString() + " <" + objData["ToEmail"].ToString() + ">"));
            message.From = new MailAddress("Svetlio Spasov <svetlio758@gmail.com>");
            //message.Bcc.Add(new MailAddress("Tudor Ciobanu <svetlio758@gmail.com"));
            message.Subject = objData["Subject"].ToString();
            message.Body = createEmailBody(objData["ToName"].ToString(), objData["Message"].ToString());
            message.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = true;
            smtp.Port = 587;
            smtp.EnableSsl = true;
           
            await smtp.SendMailAsync(message);


        }

        private string createEmailBody(string userName, string message)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("/htmlTemplate.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{message}", message);
            return body;


        }
    }
}
