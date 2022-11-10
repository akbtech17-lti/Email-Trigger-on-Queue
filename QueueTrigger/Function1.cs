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
		public static string GetHtmlContent() {

			return "<style>.cp-container {\r\n    margin: 2rem auto;\r\n    padding: 1rem 0;\r\n    align-items: center;\r\n    width: 50%;\r\n    height: auto;\r\n    background-color: white;\r\n    border-radius: 0.5rem;\r\n    display: flex;\r\n    flex-direction: column;\r\n    align-items: center;\r\n    font-family: Arial,sans-serif;\r\n}\r\n\r\n.cp-logo {\r\n    color: #8d8d8f;\r\n    font-size: 2rem;\r\n    font-weight: bolder;\r\n}\r\n\r\n.cp-logo > span {\r\n    background-color: #e72929;\r\n    color: white;\r\n}\r\n\r\n.cp-confirm {\r\n    color: #4caf50;\r\n    font-size: 1.5rem;\r\n    font-weight: bold;\r\n}\r\n\r\n.cp-waiting{\r\n    color: rgb(255, 166, 0);\r\n    font-size: 1.5rem;\r\n    font-weight: bold;\r\n}\r\n\r\n.cp-bid {\r\n    font-weight: bold;\r\n}\r\n\r\n.cp-ticket-container {\r\n    margin-top: 2rem;\r\n    height: auto;\r\n    width: 90%;\r\n    background-color: #f5f5f5;\r\n    border-radius: 0.5rem;\r\n    padding: 1.5rem;\r\n}\r\n\r\n.cp-movie {\r\n    display: flex;\r\n    padding-bottom: 1.5rem;\r\n    border-bottom: 4px dashed #c49d9d28;\r\n}\r\n\r\n.cp-movie-image > img {\r\n    height: 120px;\r\n    border-radius: 0.8rem;\r\n}\r\n\r\n.cp-movie-info{\r\n    padding-left: 2rem;\r\n}\r\n\r\n.cp-movie-title{\r\n    font-size: 20px;\r\n    font-weight: bold;\r\n    \r\n    text-align: left;\r\n    color: #3c3c3c;\r\n}\r\n\r\n.cp-time{\r\n    margin-top: 1rem;\r\n    font-size: 16px;\r\n    font-family: Arial,sans-serif;\r\n    text-align: left;\r\n    vertical-align: top;\r\n    background-color: #f5f5f5;\r\n    color: #3c3c3c;\r\n}\r\n\r\n.cp-address{\r\n    font-size: 13px;\r\n    color: #828282;\r\n    font-weight: 400;\r\n}\r\n\r\n.cp-seat{\r\n    margin-top: 0.8rem;\r\n    font-size: 15px;\r\n    font-weight: bold;\r\n    color: #010101;\r\n}\r\n\r\n.cp-seat-details{\r\n    display: flex;\r\n    align-items: center;\r\n    justify-content: space-between;\r\n    padding: 1.5rem 1rem;\r\n    /* border-bottom: 4px dashed #c49d9d28; */\r\n}\r\n\r\n.cp-number-of-seats{\r\n    display: flex;\r\n    flex-direction: column;\r\n    align-items: center;\r\n}\r\n\r\n.cp-nseat{\r\n    text-align: center;\r\n    font-size: 26px;\r\n    font-weight: 800;\r\n    font-family: Arial,sans-serif;\r\n    color: #000000;\r\n}\r\n\r\n.cp-ticket-label{\r\n    font-size: 13px;\r\n    color: #828282;\r\n    font-weight: 400;\r\n}\r\n.cp-audi-details{\r\n    font-family: Arial,sans-serif;\r\n    text-align: left;\r\n    font-size: 1rem;\r\n    color: #828282;\r\n    font-weight: 400;\r\n}\r\n\r\n.cp-transaction-container{\r\n    margin: 2rem 0;\r\n    display: flex;\r\n    flex-direction: column;\r\n    width: 100%;\r\n    padding: 0 1.5rem;\r\n}\r\n\r\n.cpt-heading {\r\n    font-size: 13px;\r\n    font-family: Arial,sans-serif;\r\n    text-align: left;\r\n    background-color: #ffffff;\r\n    font-weight: 600;\r\n    color: #828282;\r\n    letter-spacing: 1px;\r\n}\r\n\r\n.cp-transaction-details{\r\n    width: 100%;\r\n    padding: 0;\r\n    border: 2px solid #f1f1f1;\r\n    border-radius: 5px;\r\n    font-family: Arial,sans-serif;\r\n    margin: 1rem auto 0rem auto;\r\n}\r\n\r\n.cp-transaction-details{\r\n    display: flex;\r\n    flex-direction: column;\r\n    \r\n}\r\n\r\n.cp-amount-details {\r\n    display: flex;\r\n    justify-content: space-between;\r\n    padding: 25px 25px 5px 25px;\r\n}\r\n\r\n.cp-amount-label{\r\n    color: #222;\r\n    font-weight: bold;\r\n    font-size: 14px;\r\n    font-family: Arial,sans-serif;\r\n}\r\n\r\n.cp-amount{\r\n    font-size: 15px;\r\n    font-family: Arial,sans-serif;\r\n    color: #3c3c3c;\r\n}\r\n\r\n.cp-quantity-details{\r\n    display: flex;\r\n    justify-content: space-between;\r\n    color: #828282;\r\n    font-size: 13px;\r\n    padding: 0 25px 15px 25px;\r\n}\r\n\r\n.cp-paid-details {\r\n    background-color: #f1f1f1;\r\n    padding: 25px 25px 15px 25px;\r\n}\r\n\r\n.cp-inner{\r\n    display: flex;\r\n    justify-content: space-between; \r\n    font-size: 14px;\r\n    color: black;\r\n    padding: 0;\r\n    margin: 0;\r\n    font-weight: bolder;\r\n}\r\n\r\n.cp-back {\r\n    background-color:#e72929;\r\n    border-radius: 2rem;\r\n    padding: 0.5rem 1rem;\r\n    color: white;\r\n}\r\n\r\n.cp-back:hover{\r\n    transition: 0.8s;\r\n    transform: scale(1.1);\r\n    cursor: pointer;\r\n    opacity: 0.95;\r\n}\r\n\r\n.cp-print {\r\n    background-color:#e72929;\r\n    border-radius: 1rem;\r\n    outline: none;\r\n    border: none;\r\n    padding: 0.5rem 1rem;\r\n    color: white;\r\n    margin: 1rem;\r\n}\r\n\r\n.cp-print:hover{\r\n    outline: none;\r\n    border: none;\r\n    transition: 0.8s;\r\n    transform: scale(1.1);\r\n    cursor: pointer;\r\n    opacity: 0.95;\r\n}</style><div id=\"print-section\" class=\"cp-container container\" *ngIf=\"transactionSuccess\">\r\n    <div class=\"cp-logo\">book <span>my</span> movie</div>\r\n    <div class=\"cp-confirm\">Your booking is confirmed!</div>\r\n    <div class=\"cp-bookingid\">\r\n        <span>Booking ID </span>\r\n        <span class=\"cp-bid\">#{{transactionResponse.transactionId}}</span>\r\n    </div>\r\n    <div class=\"cp-ticket-container\">\r\n        <div class=\"cp-movie\">\r\n            <div class=\"cp-movie-image\">\r\n                <img src=\"./assets/{{transactionResponse.imageUrl}}\" alt=\"\" class=\"cp-movie-image\">\r\n            </div>\r\n            <div class=\"cp-movie-info\">\r\n                <div class=\"cp-movie-title\">{{transactionResponse.movieName}} ({{transactionResponse.ageRating}})</div>\r\n                <div class=\"cp-movie-venue\">\r\n                    <!-- <div class=\"cp-time\">11:30pm | Sun, 22 May, 2022</div> -->\r\n                    <div class=\"cp-time\">{{transactionResponse.showTime | mydatepipe}} {{transactionResponse.showTime | date: 'shortTime'}}</div>\r\n                    <div class=\"cp-address\">Cinepolis: WESTEND Mall Aundh, Pune(AUDI 03) Pune, Pune</div>\r\n                </div>\r\n                <div class=\"cp-seat\">EXECUTIVE - {{transactionResponse.seats}}</div>\r\n            </div>\r\n        </div>\r\n        <div class=\"cp-seat-details\">\r\n            <div class=\"cp-number-of-seats\">\r\n                <div class=\"cp-nseat\">{{transactionResponse.seats.length}}</div>\r\n                <div class=\"cp-ticket-label\">Tickets</div>\r\n            </div>\r\n            <div class=\"cp-audi-details\">AUDI 03</div>\r\n        </div>\r\n    </div>\r\n    <div class=\"cp-transaction-container\">\r\n        <div class=\"cpt-heading\">ORDER SUMMARY</div>\r\n        <div class=\"cp-transaction-details\">\r\n            <div class=\"cp-amount-details\">\r\n                <div class=\"cp-amount-label\">TICKET AMOUNT</div>\r\n                <div class=\"cp-amount\">Rs.{{transactionResponse.totalCost}}</div>\r\n            </div>\r\n            <div class=\"cp-quantity-details\">\r\n                <div class=\"cp-quantity-label\">Quantity</div>\r\n                <div class=\"cp-quantity\">{{transactionResponse.seats.length}} tickets</div>\r\n            </div>\r\n            <div class=\"cp-paid-details\">\r\n                <div class=\"cp-inner\">\r\n                    <div class=\"cp-paid-label\">AMOUNT PAID</div>\r\n                    <div class=\"cp-paid-money\">Rs.{{transactionResponse.totalCost}}</div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"cp-back\" (click)=\"OnClick()\">Back to Dashboard</div>\r\n    <button printSectionId=\"print-section\" ngxPrint class=\"cp-print\">Print</button>\r\n</div>";		
		}
 		public static string GetBetween(string strSource, string strStart, string strEnd)
		{
			if (strEnd.Equals("")) {
				int start, end;
				start = strSource.IndexOf(strStart, 0) + strStart.Length;
				end = strSource.Length;
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
			string message = GetBetween(transactionDetails, "Message : ","\n");
			if (message.Equals(""))
			{
				// booking email
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
				string htmlContent = "" +
				$"<h1>Thankyou {name}</h1>" +
				$"<h3>for using Book My Movie</h3>" +
				$"<hr/>" +
				$"<br>" +
				$"<h2>Movie name : {movieName}</h2>" +
				$"<h2>Show time :  {showTime}</h2>" +
				$"<h3>No of seats :  {noOfSeats}</h3>" +
				$"<h3>Seat numbers :  {seatNos}</h3>";
				SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
				Response response = await client.SendEmailAsync(msg);

				log.LogInformation($"C# Queue trigger function processed: To : {email}\n From: akb.tech17@gmail.com {transactionDetails}");
			}
			else {
				// cancel booking email
				string name = GetBetween(transactionDetails, "Name : ", "\n");
				string email = GetBetween(transactionDetails, "Email : ", "\n");
				string seatNos = GetBetween(transactionDetails, "Seat No : ", "\n");
				string transactionId = GetBetween(transactionDetails, "Transaction Id : ", "\n");
				string refundCost = GetBetween(transactionDetails, "Refund Cost : ", "\n");
				string movieName = GetBetween(transactionDetails, "Movie Name : ", "\n");
				string showTime = GetBetween(transactionDetails, "Show Time : ", "");

				string apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
				SendGridClient client = new SendGridClient(apiKey);
				EmailAddress from = new EmailAddress("akb.tech17@gmail.com", "Book My Movie");
				string subject = $"Your #{transactionId} Booking is Cancelled!";
				EmailAddress to = new EmailAddress(email);
				string plainTextContent = transactionDetails;
				string htmlContent = "" +
				$"<h1>Thankyou {name}</h1>" +
				$"<h3>for using Book My Movie</h3>" +
				$"<hr/>" +
				$"<br>" +
				$"<h2>Transaction Id : {transactionId}</h2>" +
				$"<h2>Movie name : {movieName}</h2>" +
				$"<h2>Show time : {showTime}</h2>" +
				$"<h3>Seat numbers : {seatNos}</h3>" +
				$"<h3>Refund cost : {refundCost}</h3>" ;
				SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
				Response response = await client.SendEmailAsync(msg);

				log.LogInformation($"C# Queue trigger function processed: To : {email}\n From: akb.tech17@gmail.com {transactionDetails}");
			}
			
        }
    }
}
