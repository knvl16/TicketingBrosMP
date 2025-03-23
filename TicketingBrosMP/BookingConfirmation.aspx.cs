using System;
using System.Web.UI;
namespace TicketingBrosMP
{
    public partial class BookingConfirmation : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Ensure session variables exist before displaying confirmation
                if (Session["ConfirmationNumber"] == null ||
                    Session["MovieTitle"] == null ||
                    Session["BookingDate"] == null ||
                    Session["BookingTime"] == null ||
                    Session["SelectedSeats"] == null ||
                    Session["TotalPrice"] == null)
                {
                    Response.Write("<script>alert('No booking confirmation found. Please book a ticket first.');window.location='Home.aspx';</script>");
                    return;
                }

                // Store confirmation number in a variable that will persist after this page
                string confirmationNumber = Session["ConfirmationNumber"]?.ToString() ?? "N/A";
                Session["RetrievedConfirmation"] = confirmationNumber;

                // Populate confirmation details
                lblConfirmationNumber.Text = confirmationNumber;
                lblMovieTitle.Text = Session["MovieTitle"]?.ToString() ?? "N/A";
                lblDate.Text = Convert.ToDateTime(Session["BookingDate"]).ToString("dddd, MMMM d, yyyy");
                lblTime.Text = DateTime.TryParse(Session["BookingTime"]?.ToString(), out DateTime showTime)
                    ? showTime.ToString("hh:mm tt") // Convert to AM/PM format
                    : "N/A";
                lblSeats.Text = Session["SelectedSeats"]?.ToString()?.Replace(",", ", ") ?? "N/A";
                lblTotalPrice.Text = $"₱{Session["TotalPrice"]?.ToString() ?? "0.00"}";

                // Clear only the confirmation number to prevent duplicate submissions
                Session.Remove("ConfirmationNumber");
            }
        }

        protected void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrintableReceipt.aspx");
        }

        protected void btnViewBookings_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyBookings.aspx");
        }

        protected void btnReturnHome_Click(object sender, EventArgs e)
        {
            // Clear only booking-related session variables
            Session.Remove("MovieTitle");
            Session.Remove("BookingDate");
            Session.Remove("BookingTime");
            Session.Remove("SelectedSeats");
            Session.Remove("TotalPrice");

            Response.Redirect("Home.aspx");
        }
    }
}