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

                LoadMovies();
            }
        }



        private void LoadMovies()
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source="
                                + Server.MapPath("~/App_Data/TicketingBros.mdb");

            string query = @"
SELECT Title, Genre, Duration, Director, Writer, Description, PosterPath,
       Cast1Name, Cast1PhotoPath, Cast2Name, Cast2PhotoPath,
       ShowingDate, EndDate, ImdbLink
FROM Movies
WHERE ShowingDate <= Date() AND EndDate >= Date()";  // Now showing based on date

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connString))
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            rptMovies.DataSource = reader;
                            rptMovies.DataBind();
                            pnlNoMovies.Visible = false; // Hide panel if movies exist
                        }
                        else
                        {
                            rptMovies.DataSource = null;
                            rptMovies.DataBind();
                            pnlNoMovies.Visible = true; // Show panel if no movies exist
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error loading movies: " + ex.Message + "');</script>");
            }
        }

        protected string GetUrl(string hyperlinkField)
        {

            string[] parts = hyperlinkField.Split('#');
            if (parts.Length >= 2)
            {
                return parts[1];
            }
            return hyperlinkField;
        }
    }
}
