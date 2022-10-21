using System;
using System.Text.RegularExpressions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Nest;
using SendGrid.Helpers.Mail;

namespace QueueTrigger
{
    public class Function1
    {
		public static string getBetween(string strSource, string strStart, string strEnd)
		{
			if (strSource.Contains(strStart) && strSource.Contains(strEnd))
			{
				int Start, End;
				Start = strSource.IndexOf(strStart, 0) + strStart.Length;
				End = strSource.IndexOf(strEnd, Start);
				return strSource.Substring(Start, End - Start);
			}

			return "";
		}
		[FunctionName("Function1")]
        public void Run(
			[QueueTrigger("transactions", Connection = "StorageAccountConn")]string transactionDetails,
			[SendGrid(ApiKey="SendGridApiKey")] out SendGridMessage message,
			ILogger log)
        {
			string email = getBetween(transactionDetails, "Email : ", "\n");
			
			message = new SendGridMessage();
			message.SetFrom(new EmailAddress(email));
			message.AddTo(email);
			message.AddContent("text/html", transactionDetails);
			message.SetSubject("Your Booking is Confirmed");
			
			log.LogInformation($"C# Queue trigger function processed: {email} + {transactionDetails}");
        }
    }
}
