using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TicketingBrosMP
{
    public partial class Contact_us : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
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




        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            nametxt.Text = "";
            phonetxt.Text = "";
            emailtxt.Text = "";
            lasttxb.Text = "";
            commtxt.Text = "";

        }


    }
}