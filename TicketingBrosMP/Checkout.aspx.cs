using System;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace TicketingBrosMP
{
    public partial class Checkout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (Session["Username"] == null)
                {
                    Response.Write("<script>alert('You must be logged in to proceed with checkout.');window.location='Login.aspx';</script>");
                    return;
                }

                // Check if there are selected seats
                if (Session["MovieTitle"] == null || Session["SelectedSeats"] == null || Session["TotalPrice"] == null)
                {
                    Response.Write("<script>alert('No ticket selection found. Please select seats first.');window.location='Home.aspx';</script>");
                    return;
                }

                // Display booking information
                lblMovieTitle.Text = Session["MovieTitle"].ToString();
                lblSelectedSeats.Text = Session["SelectedSeats"].ToString().Replace(",", ", ");
                lblTotalPrice.Text = Session["TotalPrice"].ToString();

                // Set date and time information
                DateTime showDate = DateTime.Now.AddDays(1);
                Session["BookingDate"] = showDate.ToString("yyyy-MM-dd"); // Format for database
                Session["BookingTime"] = "19:00"; // Default time in 24-hour format

                lblDate.Text = showDate.ToString("dddd, MMMM d, yyyy");
                lblTime.Text = "7:00 PM";

                // Pre-fill email if available
                if (Session["Email"] != null)
                {
                    txtEmail.Text = Session["Email"].ToString();
                }

                // Add client-side validation
                AddClientSideValidation();
            }
        }

        private void AddClientSideValidation()
        {
            // Add JavaScript to enforce numeric input for card number, CVV, and phone
            string script = @"
                function validateNumeric(evt) {
                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    return true;
                }";

            // Register the script
            ClientScript.RegisterClientScriptBlock(this.GetType(), "NumericValidation", script, true);

            // Add the onkeypress attribute to the relevant textboxes
            txtCardNumber.Attributes.Add("onkeypress", "return validateNumeric(event)");
            txtCVV.Attributes.Add("onkeypress", "return validateNumeric(event)");
            txtPhone.Attributes.Add("onkeypress", "return validateNumeric(event)");
        }

        protected void btnConfirmPurchase_Click(object sender, EventArgs e)
        {
            // Validate form fields
            if (!ValidateForm())
            {
                return;
            }

            // Process booking
            string username = Session["Username"].ToString();
            string movieTitle = Session["MovieTitle"].ToString();
            string seats = Session["SelectedSeats"].ToString();
            decimal totalPrice = Convert.ToDecimal(Session["TotalPrice"]);
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            DateTime bookingDate = DateTime.Parse(Session["BookingDate"].ToString());
            DateTime bookingTime = DateTime.ParseExact(Session["BookingTime"].ToString(), "HH:mm", null);
            string paymentMethod = "Credit Card";
            string confirmationNumber = GenerateConfirmationNumber();

            // Check for existing booking before saving
            if (HasExistingBooking(username, movieTitle, seats))
            {
                Response.Write("<script>alert('You have already booked these seats for this movie.');</script>");
                return;
            }

            // Save booking to database
            if (SaveBooking(username, movieTitle, seats, totalPrice, email, phone, bookingDate, bookingTime, paymentMethod, confirmationNumber))
            {
                // Redirect to confirmation page
                Session["ConfirmationNumber"] = confirmationNumber;
                Response.Redirect("BookingConfirmation.aspx");
            }
            else
            {
                Response.Write("<script>alert('There was an error processing your booking. Please try again.');</script>");
            }
        }

        private bool ValidateForm()
        {
            // Check for empty fields
            if (string.IsNullOrEmpty(txtCardName.Text) ||
                string.IsNullOrEmpty(txtCardNumber.Text) ||
                string.IsNullOrEmpty(txtCVV.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtPhone.Text))
            {
                Response.Write("<script>alert('Please fill in all required fields.');</script>");
                return false;
            }

            // Validate card number
            if (!IsNumeric(txtCardNumber.Text))
            {
                Response.Write("<script>alert('Card number must contain only digits.');</script>");
                return false;
            }

            if (txtCardNumber.Text.Length != 16)
            {
                Response.Write("<script>alert('Card number must be 16 digits.');</script>");
                return false;
            }

            // Validate CVV
            if (!IsNumeric(txtCVV.Text))
            {
                Response.Write("<script>alert('CVV must contain only digits.');</script>");
                return false;
            }

            if (txtCVV.Text.Length != 3)
            {
                Response.Write("<script>alert('CVV must be 3 digits.');</script>");
                return false;
            }

            // Validate phone number
            if (!IsNumeric(txtPhone.Text))
            {
                Response.Write("<script>alert('Phone number must contain only digits.');</script>");
                return false;
            }

            if (txtPhone.Text.Length != 11)
            {
                Response.Write("<script>alert('Phone number must be exactly 11 digits.');</script>");
                return false;
            }

            // Validate email format
            if (!IsValidEmail(txtEmail.Text))
            {
                Response.Write("<script>alert('Please enter a valid email address.');</script>");
                return false;
            }

            return true;
        }

        private bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool SaveBooking(string username, string movieTitle, string seats, decimal totalPrice, string email, string phone, DateTime bookingDate, DateTime bookingTime, string paymentMethod, string confirmationNumber)
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");

            string query = @"INSERT INTO TicketBookings 
                (Username, MovieTitle, Seats, TotalPrice, Email, Phone, BookingDate, ShowTime, PaymentMethod, ConfirmationNumber, BookingTime) 
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@MovieTitle", movieTitle);
                        cmd.Parameters.AddWithValue("@Seats", seats);
                        cmd.Parameters.Add("@TotalPrice", OleDbType.Currency).Value = totalPrice;
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.Add("@BookingDate", OleDbType.Date).Value = bookingDate;
                        cmd.Parameters.Add("@ShowTime", OleDbType.Date).Value = bookingTime;
                        cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        cmd.Parameters.AddWithValue("@ConfirmationNumber", confirmationNumber);
                        cmd.Parameters.Add("@BookingTime", OleDbType.Date).Value = DateTime.Now;

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Database error: {ex.Message}');</script>");
                return false;
            }
        }

        private bool HasExistingBooking(string username, string movieTitle, string seats)
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");
            string query = "SELECT COUNT(*) FROM TicketBookings WHERE Username = ? AND MovieTitle = ? AND Seats = ?";

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@MovieTitle", movieTitle);
                    cmd.Parameters.AddWithValue("@Seats", seats);

                    conn.Open();
                    return (int)cmd.ExecuteScalar() > 0; // Returns true if count is greater than 0
                }
            }
        }

        private string GenerateConfirmationNumber()
        {
            // Generate a random, unique confirmation number
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Clear session variables related to the booking
            Session["SelectedSeats"] = null;
            Session["MovieTitle"] = null;
            Session["TotalPrice"] = null;

            // Redirect to home page
            Response.Redirect("Home.aspx");
        }
    }
}