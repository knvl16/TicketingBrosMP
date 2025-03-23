using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
<<<<<<< HEAD

=======
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
namespace TicketingBrosMP
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMovies();
            }
        }
<<<<<<< HEAD

=======
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        private void LoadMovies()
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
<<<<<<< HEAD
                conn.Open();

                // Load Now Showing Movies
                string nowShowingQuery = "SELECT Title, PosterPath FROM Movies WHERE Status = 'nowshowing'";
                using (OleDbCommand cmd = new OleDbCommand(nowShowingQuery, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    rptNowShowing.DataSource = dt;
                    rptNowShowing.DataBind();
                }

                // Load Upcoming Movies
                string upcomingQuery = "SELECT Title, PosterPath FROM Movies WHERE Status = 'upcoming'";
                using (OleDbCommand cmd = new OleDbCommand(upcomingQuery, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    rptUpcomingMovies.DataSource = dt;
                    rptUpcomingMovies.DataBind();
=======
                try
                {
                    conn.Open();
                    DateTime today = DateTime.Today;

                    // Now Showing: ShowingDate is today or earlier AND EndDate is in the future
                    string nowShowingQuery = "SELECT Title, PosterPath FROM Movies WHERE ShowingDate <= ? AND EndDate >= ?";
                    using (OleDbCommand cmd = new OleDbCommand(nowShowingQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("?", today);
                        cmd.Parameters.AddWithValue("?", today);
                        OleDbDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        pnlNowShowing.Visible = dt.Rows.Count > 0;
                        pnlNoNowShowing.Visible = dt.Rows.Count == 0;

                        rptNowShowing.DataSource = dt;
                        rptNowShowing.DataBind();
                    }

                    // Upcoming: ShowingDate is in the future
                    string upcomingQuery = "SELECT Title, PosterPath FROM Movies WHERE ShowingDate > ?";
                    using (OleDbCommand cmd = new OleDbCommand(upcomingQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("?", today);
                        OleDbDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        pnlUpcoming.Visible = dt.Rows.Count > 0;
                        pnlNoUpcoming.Visible = dt.Rows.Count == 0;

                        rptUpcomingMovies.DataSource = dt;
                        rptUpcomingMovies.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error loading movies: " + ex.Message + "');</script>");
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                }
            }
        }
    }
}
