using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
=======
using System.Data.OleDb;
using System.Web.UI;
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153

namespace TicketingBrosMP
{
    public partial class Contact_us : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD

=======
            // No special operations on page load
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            OleDbConnection connection = new OleDbConnection("provider = Microsoft.ACE.OLEDB.16.0;Data source =" + Server.MapPath("~/App_Data/TicketingBros.mdb"));
            connection.Open();
            string sqlcmd = "insert into Inquiry_TBL values('" + lasttxb.Text + "','" + nametxt.Text + "','" + phonetxt.Text + "','" + emailtxt.Text + "','" + commtxt.Text + "');";
            OleDbCommand sqlcommand = new OleDbCommand(sqlcmd, connection);
            sqlcommand.ExecuteNonQuery();
            connection.Close();
            nametxt.Text = "";
            phonetxt.Text = "";
            emailtxt.Text = "";
            lasttxb.Text = "";
            commtxt.Text = "";




=======
            try
            {
                string dbPath = Server.MapPath("~/App_Data/TicketingBros.mdb");

                using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + dbPath))
                {
                    connection.Open();

                    // Use OleDbParameter for security and correct data types
                    string sqlcmd = "INSERT INTO Inquiry_TBL (Sender_lastname, Sender_firstname, Sender_Tel_no, Sender_Email, Sender_Comments, Sender_DateTime) " +
                                    "VALUES (@lastName, @firstName, @phone, @email, @message, @dateTime)";

                    using (OleDbCommand sqlcommand = new OleDbCommand(sqlcmd, connection))
                    {
                        sqlcommand.Parameters.AddWithValue("@lastName", lasttxb.Text.Trim());
                        sqlcommand.Parameters.AddWithValue("@firstName", nametxt.Text.Trim());
                        sqlcommand.Parameters.AddWithValue("@phone", phonetxt.Text.Trim());
                        sqlcommand.Parameters.AddWithValue("@email", emailtxt.Text.Trim());
                        sqlcommand.Parameters.AddWithValue("@message", commtxt.Text.Trim());
                        sqlcommand.Parameters.AddWithValue("@dateTime", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")); // Correct format for MS Access

                        sqlcommand.ExecuteNonQuery();
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Message sent successfully!');", true);

                // Clear form fields
                nametxt.Text = "";
                lasttxb.Text = "";
                phonetxt.Text = "";
                emailtxt.Text = "";
                commtxt.Text = "";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            nametxt.Text = "";
            phonetxt.Text = "";
            emailtxt.Text = "";
            lasttxb.Text = "";
            commtxt.Text = "";

        }


    }
}
=======
            // Clear fields when "Cancel" is clicked
            nametxt.Text = "";
            lasttxb.Text = "";
            phonetxt.Text = "";
            emailtxt.Text = "";
            commtxt.Text = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Session["User"] = "LoggedIn";
            Response.Redirect("Home.aspx"); // Palitan kung saan gusto mo i-redirect
        }
    }
}
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
