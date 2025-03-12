using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;

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

        private void LoadMovies()
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
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
                }
            }
        }
    }
}
