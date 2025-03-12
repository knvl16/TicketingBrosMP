using System;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace TicketingBrosMP
{
    public partial class CancelReservation : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] == null)
                {
                    RedirectToLogin();
                    return;
                }

                LoadReservations();
            }
        }

        private void RedirectToLogin()
        {
            // Store the current page URL to redirect back after login
            Session["ReturnUrl"] = Request.Url.ToString();
            Response.Write("<script>alert('Please log in to view and manage your reservations.'); window.location='signup.aspx';</script>");
        }

        private void LoadReservations()
        {
            string username = Session["Username"]?.ToString();
            if (string.IsNullOrEmpty(username))
            {
                RedirectToLogin();
                return;
            }

            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");
            string query = "SELECT ID, MovieTitle, Seats FROM TicketBookings WHERE Username = ? ORDER BY MovieTitle";

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
                            bool hasReservations = reader.HasRows;
                            rptReservations.DataSource = reader;
                            rptReservations.DataBind();

                            pnlNoReservations.Visible = !hasReservations;
                            btnCancelReservation.Visible = hasReservations;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    ShowErrorMessage("There was an error loading your reservations. Please try again later.");
                }
            }
        }

        protected void btnCancelReservation_Click(object sender, EventArgs e)
        {
            string username = Session["Username"]?.ToString();
            if (string.IsNullOrEmpty(username))
            {
                RedirectToLogin();
                return;
            }

            bool seatCanceled = false;
            int cancellationCount = 0;
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");

            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                try
                {
                    connection.Open();
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
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    ShowErrorMessage("An error occurred while canceling reservations. Please try again later.");
                    return;
                }
            }

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
            }
        }

        private void ShowErrorMessage(string message)
        {
            Response.Write($"<script>alert('{message}');</script>");
        }

        private void LogError(Exception ex)
        {
            // In a production environment, you would log the error to a file or database
            System.Diagnostics.Debug.WriteLine($"Error in CancelReservation.aspx: {ex.Message}");
        }
    }
}