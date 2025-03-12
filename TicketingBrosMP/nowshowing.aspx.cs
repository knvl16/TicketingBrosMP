using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;

namespace TicketingBrosMP
{
    public partial class nowshowing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateMovieStatus(); // Auto-update movie status
                LoadMovies();
            }
        }

        private void UpdateMovieStatus()
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                conn.Open();
                string query = "UPDATE Movies SET Status = 'nowshowing' WHERE Status = 'upcoming' AND ShowingDate <= Date()";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.ExecuteNonQuery(); // Update upcoming movies to now showing
                }
            }
        }

        private void LoadMovies()
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");
            string query = "SELECT Title, Genre, Duration, Director, Writer, Description, PosterPath, Cast1Name, Cast1PhotoPath, Cast2Name, Cast2PhotoPath, ShowingDate, EndDate FROM Movies WHERE Status = 'nowshowing'";

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand(query, conn);
                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    rptMovies.DataSource = reader;
                    rptMovies.DataBind();
                }
                reader.Close();
            }
        }
    }
}
