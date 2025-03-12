using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace TicketingBrosMP
{
    public partial class BuyTickets : Page
    {
        // Changed to store seat IDs as strings instead of integers
        private Dictionary<string, List<string>> _takenSeatsCache = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Load the taken seats once per page load (cache for efficiency)
            _takenSeatsCache = LoadTakenSeats();

            if (!IsPostBack)
            {
                LoadMovies();
            }
        }

        private void LoadMovies()
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");
            string query = "SELECT ID, Title, Genre, Duration, Director, Writer, Description, PosterPath, Cast1Name, Cast1PhotoPath, Cast2Name, Cast2PhotoPath FROM Movies WHERE ShowingDate <= Date() ORDER BY ShowingDate DESC";

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        conn.Open();
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                rptMovies.DataSource = reader;
                                rptMovies.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error loading movies: " + ex.Message + "');</script>");
            }
        }

        // Updated to store seat IDs as strings (e.g., "A1", "B3") instead of integers
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

                            // Parse the comma-separated list of seat IDs
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

        // Updated to check seat IDs as strings
        public bool IsSeatTaken(object movieTitleObj, string seatId)
        {
            string movieTitle = movieTitleObj.ToString();
            if (_takenSeatsCache != null && _takenSeatsCache.ContainsKey(movieTitle))
            {
                return _takenSeatsCache[movieTitle].Contains(seatId);
            }
            return false;
        }

        // Helper method to generate the theater-style seat map.
        // This method is called from the repeater's ItemTemplate.
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
                    bool isPremium = row == 'D' || row == 'E'; // Make D and E rows premium

                    seatHtml.Append("<label class='seat-label ");
                    if (isTaken) seatHtml.Append("taken ");
                    if (isPremium) seatHtml.Append("premium ");
                    seatHtml.Append("'>");

                    // Store seatId as the value and data-seat-info
                    seatHtml.AppendFormat("<input type='checkbox' class='seat-checkbox' id='seat_{0}_{1}' name='seat_{0}' value='{2}' data-seat-info='{2}' {3} />",
                        movieId,
                        seatNumber,
                        seatId,
                        isTaken ? "disabled" : "");

                    seatHtml.AppendFormat("{0}", seatId);
                    seatHtml.Append("</label>");

                    seatNumber++;
                }

                // Center aisle
                seatHtml.Append("<div class='aisle'></div>");

                // Right half of seats
                for (int i = seatsPerHalfRow + 1; i <= 2 * seatsPerHalfRow; i++)
                {
                    // Create seat identifier (e.g., "A5")
                    string seatId = $"{row}{i}";

                    bool isTaken = IsSeatTaken(movieTitle, seatId);
                    bool isPremium = row == 'D' || row == 'E'; // Make D and E rows premium

                    seatHtml.Append("<label class='seat-label ");
                    if (isTaken) seatHtml.Append("taken ");
                    if (isPremium) seatHtml.Append("premium ");
                    seatHtml.Append("'>");

                    // Store seatId as the value and data-seat-info
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

        protected void btnBuyNow_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string movieID = btn.CommandArgument;
            HiddenField hfMovieTitle = (HiddenField)btn.NamingContainer.FindControl("hfMovieTitle");
            string movieTitle = hfMovieTitle.Value;
            string seatField = "seat_" + movieID;

            // Get all selected seat values (now these will be "A1", "B2", etc.)
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

            // Join selected seats with commas
            string seatsList = string.Join(",", selectedSeats);

            // Calculate price based on seat row (rows D and E are premium)
            decimal totalPrice = 0;
            foreach (var seat in selectedSeats)
            {
                // Check if the first character of the seat ID (the row letter) is 'D' or 'E'
                bool isPremium = seat.StartsWith("D") || seat.StartsWith("E");
                totalPrice += isPremium ? 450m : 300m;
            }

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
        }
    }
}