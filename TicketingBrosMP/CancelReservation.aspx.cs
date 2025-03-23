using System;
<<<<<<< HEAD
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace TicketingBrosMP
{
    public partial class CancelReservation : Page
    {
=======
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TicketingBrosMP
{
    public partial class RecentTicketCancellation : Page
    {
        // Constants
        private const int CANCELLATION_WINDOW_MINUTES = 5;

>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] == null)
                {
                    RedirectToLogin();
                    return;
                }

<<<<<<< HEAD
                LoadReservations();
=======
                LoadRecentTickets();
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
            }
        }

        private void RedirectToLogin()
        {
            // Store the current page URL to redirect back after login
            Session["ReturnUrl"] = Request.Url.ToString();
<<<<<<< HEAD
            Response.Write("<script>alert('Please log in to view and manage your reservations.'); window.location='signup.aspx';</script>");
        }

        private void LoadReservations()
=======
            Response.Write("<script>alert('Please log in to view and manage your tickets.'); window.location='signup.aspx';</script>");
        }

        private void LoadRecentTickets()
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        {
            string username = Session["Username"]?.ToString();
            if (string.IsNullOrEmpty(username))
            {
                RedirectToLogin();
                return;
            }

            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");
<<<<<<< HEAD
            string query = "SELECT ID, MovieTitle, Seats FROM TicketBookings WHERE Username = ? ORDER BY MovieTitle";
=======
            string query = "SELECT ID, MovieTitle, Seats, BookingTime FROM TicketBookings WHERE Username = ? ORDER BY BookingTime DESC";
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153

            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                try
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
<<<<<<< HEAD
                            bool hasReservations = reader.HasRows;
                            rptReservations.DataSource = reader;
                            rptReservations.DataBind();

                            pnlNoReservations.Visible = !hasReservations;
                            btnCancelReservation.Visible = hasReservations;
=======
                            // Create a DataTable to store the results
                            DataTable dt = new DataTable();
                            dt.Columns.Add("ID");
                            dt.Columns.Add("MovieTitle");
                            dt.Columns.Add("Seats");
                            dt.Columns.Add("BookingTime", typeof(DateTime));
                            dt.Columns.Add("CanCancel", typeof(bool));
                            dt.Columns.Add("TimeRemaining", typeof(int));

                            DateTime now = DateTime.Now;
                            bool hasRecentTickets = false;

                            while (reader.Read())
                            {
                                DateTime bookingTime = Convert.ToDateTime(reader["BookingTime"]);
                                TimeSpan elapsed = now - bookingTime;

                                // Include tickets booked within the last 15 minutes
                                // (showing recently expired ones too, but marking them as non-cancellable)
                                if (elapsed.TotalMinutes <= 10)
                                {
                                    bool canCancel = elapsed.TotalMinutes <= CANCELLATION_WINDOW_MINUTES;

                                    DataRow row = dt.NewRow();
                                    row["ID"] = reader["ID"];
                                    row["MovieTitle"] = reader["MovieTitle"];
                                    row["Seats"] = reader["Seats"];
                                    row["BookingTime"] = bookingTime;
                                    row["CanCancel"] = canCancel;

                                    // Calculate remaining time in seconds (0 if expired)
                                    int secondsRemaining = canCancel ?
                                        Math.Max(0, (CANCELLATION_WINDOW_MINUTES * 60) - (int)elapsed.TotalSeconds) : 0;
                                    row["TimeRemaining"] = secondsRemaining;

                                    dt.Rows.Add(row);

                                    if (canCancel)
                                    {
                                        hasRecentTickets = true;
                                    }
                                }
                            }

                            if (dt.Rows.Count > 0)
                            {
                                rptRecentTickets.DataSource = dt;
                                rptRecentTickets.DataBind();
                                pnlNoTickets.Visible = false;
                                btnCancelTickets.Visible = hasRecentTickets;
                            }
                            else
                            {
                                pnlNoTickets.Visible = true;
                                btnCancelTickets.Visible = false;
                            }
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
<<<<<<< HEAD
                    ShowErrorMessage("There was an error loading your reservations. Please try again later.");
=======
                    ShowErrorMessage("There was an error loading your recent tickets. Please try again later.");
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                }
            }
        }

<<<<<<< HEAD
        protected void btnCancelReservation_Click(object sender, EventArgs e)
=======
        protected void rptRecentTickets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // We could add additional item binding logic here if needed
            }
        }

        protected void btnCancelTickets_Click(object sender, EventArgs e)
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        {
            string username = Session["Username"]?.ToString();
            if (string.IsNullOrEmpty(username))
            {
                RedirectToLogin();
                return;
            }

<<<<<<< HEAD
            bool seatCanceled = false;
            int cancellationCount = 0;
=======
            bool ticketCanceled = false;
            int cancellationCount = 0;
            List<string> cancelledSeats = new List<string>();
            string movieTitle = string.Empty;

>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");

            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                try
                {
                    connection.Open();
<<<<<<< HEAD
                    foreach (RepeaterItem item in rptReservations.Items)
                    {
                        CheckBox chkSeat = (CheckBox)item.FindControl("chkSeat");
                        HiddenField hfMovieTitle = (HiddenField)item.FindControl("hfMovieTitle");

                        if (chkSeat != null && chkSeat.Checked && hfMovieTitle != null)
                        {
                            string seatToCancel = chkSeat.Text;
                            string movieTitle = hfMovieTitle.Value;

                            string deleteQuery = "DELETE FROM TicketBookings WHERE Username = ? AND Seats = ? AND MovieTitle = ?";
                            using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                            {
                                command.Parameters.AddWithValue("@Username", username);
                                command.Parameters.AddWithValue("@Seats", seatToCancel);
                                command.Parameters.AddWithValue("@MovieTitle", movieTitle);

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    seatCanceled = true;
                                    cancellationCount += rowsAffected;
=======

                    foreach (RepeaterItem item in rptRecentTickets.Items)
                    {
                        CheckBox chkSeat = (CheckBox)item.FindControl("chkSeat");
                        HiddenField hfMovieTitle = (HiddenField)item.FindControl("hfMovieTitle");
                        HiddenField hfBookingID = (HiddenField)item.FindControl("hfBookingID");
                        HiddenField hfSeatNumber = (HiddenField)item.FindControl("hfSeatNumber");
                        HiddenField hfCanCancel = (HiddenField)item.FindControl("hfCanCancel");

                        if (chkSeat != null && chkSeat.Checked &&
                            hfMovieTitle != null && hfBookingID != null &&
                            hfSeatNumber != null && hfCanCancel != null)
                        {
                            // Only proceed if this ticket is still cancellable
                            if (bool.Parse(hfCanCancel.Value))
                            {
                                string ticketId = hfBookingID.Value;
                                string seatNumber = hfSeatNumber.Value;
                                movieTitle = hfMovieTitle.Value;

                                // Double-check the cancellation window hasn't expired
                                string checkQuery = "SELECT BookingTime FROM TicketBookings WHERE ID = ? AND Username = ?";
                                using (OleDbCommand checkCommand = new OleDbCommand(checkQuery, connection))
                                {
                                    checkCommand.Parameters.AddWithValue("@ID", ticketId);
                                    checkCommand.Parameters.AddWithValue("@Username", username);

                                    object result = checkCommand.ExecuteScalar();
                                    if (result != null)
                                    {
                                        DateTime bookingTime = Convert.ToDateTime(result);
                                        TimeSpan elapsed = DateTime.Now - bookingTime;

                                        if (elapsed.TotalMinutes <= CANCELLATION_WINDOW_MINUTES)
                                        {
                                            // Within the cancellation window, proceed with deletion
                                            string deleteQuery = "DELETE FROM TicketBookings WHERE ID = ? AND Username = ?";
                                            using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                                            {
                                                command.Parameters.AddWithValue("@ID", ticketId);
                                                command.Parameters.AddWithValue("@Username", username);

                                                int rowsAffected = command.ExecuteNonQuery();
                                                if (rowsAffected > 0)
                                                {
                                                    ticketCanceled = true;
                                                    cancellationCount += rowsAffected;
                                                    cancelledSeats.Add(seatNumber);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Cancellation window has expired
                                            ShowErrorMessage("The cancellation period has expired for one or more selected tickets.");
                                            return;
                                        }
                                    }
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
<<<<<<< HEAD
                    ShowErrorMessage("An error occurred while canceling reservations. Please try again later.");
=======
                    ShowErrorMessage("An error occurred while canceling tickets. Please try again later.");
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                    return;
                }
            }

<<<<<<< HEAD
            if (seatCanceled)
            {
                string message = cancellationCount == 1
                    ? "Your reservation has been successfully canceled."
                    : $"{cancellationCount} reservations have been successfully canceled.";

                // Update the UI
                Response.Write($"<script>alert('{message}'); window.location='CancelReservation.aspx';</script>");
            }
            else
            {
                ShowErrorMessage("No seats were selected or there was an error in the cancellation process.");
=======
            if (ticketCanceled)
            {
                string message;
                if (cancellationCount == 1)
                {
                    message = $"Your ticket for seat {cancelledSeats[0]} has been successfully canceled.";
                }
                else
                {
                    message = $"{cancellationCount} tickets have been successfully canceled: {string.Join(", ", cancelledSeats)}.";
                }

                // Update the UI
                Response.Write($"<script>alert('{message}'); window.location='Home.aspx';</script>");
            }
            else
            {
                ShowErrorMessage("No tickets were selected or there was an error in the cancellation process.");
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
            }
        }

        private void ShowErrorMessage(string message)
        {
<<<<<<< HEAD
            Response.Write($"<script>alert('{message}');</script>");
=======
            // Display error message to the user
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
                $"alert('{message}');", true);
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }

        private void LogError(Exception ex)
        {
            // In a production environment, you would log the error to a file or database
<<<<<<< HEAD
            System.Diagnostics.Debug.WriteLine($"Error in CancelReservation.aspx: {ex.Message}");
=======
            System.Diagnostics.Debug.WriteLine($"Error in RecentTicketCancellation.aspx: {ex.Message}");
            // Optional: Log more detailed error information
            System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }
    }
}