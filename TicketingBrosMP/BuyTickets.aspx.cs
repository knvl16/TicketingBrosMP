using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
<<<<<<< HEAD
=======
using System.Web.UI.HtmlControls;
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153

namespace TicketingBrosMP
{
    public partial class BuyTickets : Page
    {
<<<<<<< HEAD
        // Changed to store seat IDs as strings instead of integers
=======
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        private Dictionary<string, List<string>> _takenSeatsCache = null;

        protected void Page_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
            // Load the taken seats once per page load (cache for efficiency)
=======
            // Add meta refresh tag to head element
            if (!IsPostBack)
            {
                // Add a meta refresh tag that refreshes the page every 30 seconds
                HtmlMeta metaRefresh = new HtmlMeta();
                metaRefresh.HttpEquiv = "refresh";
                metaRefresh.Content = "30"; // Refresh every 30 seconds
                this.Header.Controls.Add(metaRefresh);

                // Add a client-side script to refresh the page when it becomes visible again
                ClientScript.RegisterStartupScript(this.GetType(), "RefreshOnFocus", @"
                    document.addEventListener('visibilitychange', function() {
                        if (document.visibilityState === 'visible') {
                            location.reload();
                        }
                    });", true);
            }

            // Load taken seats from database each time the page loads
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
            _takenSeatsCache = LoadTakenSeats();

            if (!IsPostBack)
            {
                LoadMovies();
            }
        }

        private void LoadMovies()
        {
<<<<<<< HEAD
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");
            string query = "SELECT ID, Title, Genre, Duration, Director, Writer, Description, PosterPath, Cast1Name, Cast1PhotoPath, Cast2Name, Cast2PhotoPath FROM Movies WHERE ShowingDate <= Date() ORDER BY ShowingDate DESC";
=======
            string movieID = Request.QueryString["MovieID"];
            if (string.IsNullOrEmpty(movieID))
            {
                Response.Write("<script>alert('No movie selected. Redirecting to homepage.');window.location='Home.aspx';</script>");
                return;
            }

            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");
            string query = "SELECT ID, Title, Genre, Duration, Director, Writer, Description, PosterPath, Cast1Name, Cast1PhotoPath, Cast2Name, Cast2PhotoPath FROM Movies WHERE ID = ?";
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
<<<<<<< HEAD
=======
                        cmd.Parameters.AddWithValue("@ID", movieID);

>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                        conn.Open();
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                rptMovies.DataSource = reader;
                                rptMovies.DataBind();
                            }
<<<<<<< HEAD
=======
                            else
                            {
                                Response.Write("<script>alert('Movie not found. Returning to homepage.');window.location='Home.aspx';</script>");
                            }
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                        }
                    }
                }
            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                Response.Write("<script>alert('Error loading movies: " + ex.Message + "');</script>");
            }
        }

        // Updated to store seat IDs as strings (e.g., "A1", "B3") instead of integers
=======
                Response.Write("<script>alert('Error loading movie: " + ex.Message + "');</script>");
            }
        }

>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        private Dictionary<string, List<string>> LoadTakenSeats()
        {
            var takenSeats = new Dictionary<string, List<string>>();
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");
            string query = "SELECT MovieTitle, Seats FROM TicketBookings";

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string movieTitle = reader["MovieTitle"].ToString();
                            string seats = reader["Seats"].ToString();

<<<<<<< HEAD
                            // Parse the comma-separated list of seat IDs
=======
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                            var seatIds = new List<string>();
                            foreach (var s in seats.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                seatIds.Add(s.Trim());
                            }

                            if (!takenSeats.ContainsKey(movieTitle))
                            {
                                takenSeats[movieTitle] = new List<string>();
                            }
                            takenSeats[movieTitle].AddRange(seatIds);
                        }
                    }
                }
            }
            return takenSeats;
        }

<<<<<<< HEAD
        // Updated to check seat IDs as strings
=======
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        public bool IsSeatTaken(object movieTitleObj, string seatId)
        {
            string movieTitle = movieTitleObj.ToString();
            if (_takenSeatsCache != null && _takenSeatsCache.ContainsKey(movieTitle))
            {
                return _takenSeatsCache[movieTitle].Contains(seatId);
            }
            return false;
        }

<<<<<<< HEAD
        // Helper method to generate the theater-style seat map.
        // This method is called from the repeater's ItemTemplate.
=======
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        public string GenerateTheaterSeats(object dataItem)
        {
            // Extract the movie title and ID from the current data item.
            string movieTitle = DataBinder.Eval(dataItem, "Title").ToString();
            string movieId = DataBinder.Eval(dataItem, "ID").ToString();
            StringBuilder seatHtml = new StringBuilder();

            // We'll create 5 rows (A-E) with 4 seats on each side
            char[] rows = { 'A', 'B', 'C', 'D', 'E' };
            int seatsPerHalfRow = 4;

            int seatNumber = 1; // Keep this for ID generation only

            foreach (char row in rows)
            {
                seatHtml.Append("<div class='seat-row'>");
                seatHtml.AppendFormat("<div class='row-label'>{0}</div>", row);

                // Left half of seats
                for (int i = 1; i <= seatsPerHalfRow; i++)
                {
                    // Create seat identifier (e.g., "A1")
                    string seatId = $"{row}{i}";

                    bool isTaken = IsSeatTaken(movieTitle, seatId);
<<<<<<< HEAD
                    bool isPremium = row == 'D' || row == 'E'; // Make D and E rows premium
=======
                    bool isPremium = row == 'D' || row == 'E';
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153

                    seatHtml.Append("<label class='seat-label ");
                    if (isTaken) seatHtml.Append("taken ");
                    if (isPremium) seatHtml.Append("premium ");
                    seatHtml.Append("'>");

<<<<<<< HEAD
                    // Store seatId as the value and data-seat-info
=======
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                    seatHtml.AppendFormat("<input type='checkbox' class='seat-checkbox' id='seat_{0}_{1}' name='seat_{0}' value='{2}' data-seat-info='{2}' {3} />",
                        movieId,
                        seatNumber,
                        seatId,
                        isTaken ? "disabled" : "");

                    seatHtml.AppendFormat("{0}", seatId);
                    seatHtml.Append("</label>");

                    seatNumber++;
                }

<<<<<<< HEAD
                // Center aisle
                seatHtml.Append("<div class='aisle'></div>");

                // Right half of seats
                for (int i = seatsPerHalfRow + 1; i <= 2 * seatsPerHalfRow; i++)
                {
                    // Create seat identifier (e.g., "A5")
                    string seatId = $"{row}{i}";

                    bool isTaken = IsSeatTaken(movieTitle, seatId);
                    bool isPremium = row == 'D' || row == 'E'; // Make D and E rows premium
=======
                seatHtml.Append("<div class='aisle'></div>");

                for (int i = seatsPerHalfRow + 1; i <= 2 * seatsPerHalfRow; i++)
                {
                    string seatId = $"{row}{i}";

                    bool isTaken = IsSeatTaken(movieTitle, seatId);
                    bool isPremium = row == 'D' || row == 'E';
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153

                    seatHtml.Append("<label class='seat-label ");
                    if (isTaken) seatHtml.Append("taken ");
                    if (isPremium) seatHtml.Append("premium ");
                    seatHtml.Append("'>");

<<<<<<< HEAD
                    // Store seatId as the value and data-seat-info
=======
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                    seatHtml.AppendFormat("<input type='checkbox' class='seat-checkbox' id='seat_{0}_{1}' name='seat_{0}' value='{2}' data-seat-info='{2}' {3} />",
                        movieId,
                        seatNumber,
                        seatId,
                        isTaken ? "disabled" : "");

                    seatHtml.AppendFormat("{0}", seatId);
                    seatHtml.Append("</label>");

                    seatNumber++;
                }

                seatHtml.Append("</div>");
            }

            return seatHtml.ToString();
        }

<<<<<<< HEAD
        protected void btnBuyNow_Click(object sender, EventArgs e)
=======
        protected void btnProceedToCheckout_Click(object sender, EventArgs e)
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        {
            Button btn = (Button)sender;
            string movieID = btn.CommandArgument;
            HiddenField hfMovieTitle = (HiddenField)btn.NamingContainer.FindControl("hfMovieTitle");
            string movieTitle = hfMovieTitle.Value;
            string seatField = "seat_" + movieID;

<<<<<<< HEAD
            // Get all selected seat values (now these will be "A1", "B2", etc.)
=======
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
            string[] selectedSeats = Request.Form.GetValues(seatField);
            if (selectedSeats == null || selectedSeats.Length == 0)
            {
                Response.Write($"<script>alert('Please select at least one seat for {movieTitle}.');</script>");
                return;
            }

            string username = Session["Username"]?.ToString();
            if (string.IsNullOrEmpty(username))
            {
                Response.Write("<script>alert('You must be logged in to purchase tickets.');</script>");
                return;
            }

<<<<<<< HEAD
            // Join selected seats with commas
            string seatsList = string.Join(",", selectedSeats);

            // Calculate price based on seat row (rows D and E are premium)
            decimal totalPrice = 0;
            foreach (var seat in selectedSeats)
            {
                // Check if the first character of the seat ID (the row letter) is 'D' or 'E'
=======
            decimal totalPrice = 0;
            foreach (var seat in selectedSeats)
            {
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                bool isPremium = seat.StartsWith("D") || seat.StartsWith("E");
                totalPrice += isPremium ? 450m : 300m;
            }

<<<<<<< HEAD
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connString))
                {
                    connection.Open();
                    string query = "INSERT INTO TicketBookings (Username, MovieTitle, Seats, TotalPrice, BookingDate) VALUES (?, ?, ?, ?, ?)";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@MovieTitle", movieTitle);
                        command.Parameters.AddWithValue("@Seats", seatsList);
                        command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        command.Parameters.AddWithValue("@BookingDate", DateTime.Now.ToShortDateString());

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Ticket purchased successfully!');window.location='Home.aspx';</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Failed to process ticket purchase.');</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
=======
            // Store the selection in Session variables to pass to the checkout page
            Session["SelectedSeats"] = string.Join(",", selectedSeats);
            Session["MovieTitle"] = movieTitle;
            Session["MovieID"] = movieID;
            Session["TotalPrice"] = totalPrice;

            // Redirect to the checkout page
            Response.Redirect("Checkout.aspx");
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }
    }
}