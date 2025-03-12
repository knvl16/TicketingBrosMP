using System;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;

namespace TicketingBrosMP
{
    public partial class upcoming : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUpcomingMovies();
            }
        }

        private void LoadUpcomingMovies()
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");
            string query = "SELECT Title, Genre, Duration, Director, Writer, Description, PosterPath, Cast1Name, Cast1PhotoPath, Cast2Name, Cast2PhotoPath, ShowingDate FROM Movies WHERE ShowingDate > Date() ORDER BY ShowingDate ASC";

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
                                rptUpcomingMovies.DataSource = reader;
                                rptUpcomingMovies.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (or display a message in a Label control if needed)
                Response.Write("<script>alert('Error loading upcoming movies: " + ex.Message + "');</script>");
            }
        }
    }
}
