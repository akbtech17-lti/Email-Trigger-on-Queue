using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Nest;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace QueueTrigger
{
    public class Function1
    {
		public static string GetBetween(string strSource, string strStart, string strEnd)
		{
			if (strEnd.Equals("")) {
				int start, end;
				start = strSource.IndexOf(strStart, 0) + strStart.Length;
				end = strSource.Length-1;
				return strSource.Substring(start, end - start);
			}
			else if (strSource.Contains(strStart) && strSource.Contains(strEnd))
			{
				int start, end;
				start = strSource.IndexOf(strStart, 0) + strStart.Length;
				end = strSource.IndexOf(strEnd, start);
				return strSource.Substring(start, end - start);
			}

			return "";
		}
		[FunctionName("Function1")]
        public async Task RunAsync(
			[QueueTrigger("transactions", Connection = "StorageAccountConn")]string transactionDetails,
			ILogger log)
        {
			string name = GetBetween(transactionDetails, "Name : ", "\n");
			string email = GetBetween(transactionDetails, "Email : ", "\n");
			string seatNos = GetBetween(transactionDetails, "Seat No : ", "\n");
			string noOfSeats = GetBetween(transactionDetails, "No of Seats : ", "\n");
			string movieName = GetBetween(transactionDetails, "Movie Name : ", "\n");
			string showTime = GetBetween(transactionDetails, "Show Time : ", "");

			string apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
			SendGridClient client = new SendGridClient(apiKey);
			EmailAddress from = new EmailAddress("akb.tech17@gmail.com", "Book My Movie");
			string subject = "Your booking is Confirmed!";
			EmailAddress to = new EmailAddress(email);
			string plainTextContent = transactionDetails;
			string htmlContent = "<h1>Thankyou for making purchase</h1>" +
				"<h2><b>Movie Name :</b> Chup<h2>";
			SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
			/*Response response = await client.SendEmailAsync(msg);*/

			log.LogInformation($"C# Queue trigger function processed: To : {email}\n From: akb.tech17@gmail.com {transactionDetails}");
        }
    }
}
