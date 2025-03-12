using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TicketingBrosMP
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateLoginStatus();
            }
        }

        private void UpdateLoginStatus()
        {
            if (Session["Username"] != null)
            {
                lblUsername.Text = "Welcome " + Session["Username"].ToString();
                btnLogout.Visible = true;
                liLogin.Visible = false; // Hide login button container
            }
            else
            {
                lblUsername.Text = "";
                btnLogout.Visible = false;
                liLogin.Visible = true; // Show login button container
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblLoginMessage.Text = "Username and password are required!";
                return;
            }

            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connString))
                {
                    connection.Open();

                    // Use parameterized query to prevent SQL injection
                    string query = "SELECT COUNT(*) FROM Login_TBL WHERE username = ? AND password = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password); // Consider hashing passwords for security

                        int count = (int)command.ExecuteScalar();
                        if (count > 0)
                        {
                            Session["Username"] = username;
                            Response.Redirect("Home.aspx");
                        }
                        else
                        {
                            lblLoginMessage.Text = "Invalid username or password.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblLoginMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string email = txtResetEmail.Text.Trim();
            string newPassword = txtNewPasswordReset.Text.Trim();
            string confirmPassword = txtConfirmNewPassword.Text.Trim();

            // Validate input
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                lblResetMessage.Text = "All fields are required!";
                return;
            }

            if (newPassword != confirmPassword)
            {
                lblResetMessage.Text = "New password and confirmation do not match.";
                return;
            }

            // Password Validation
            if (!IsValidPassword(newPassword))
            {
                lblResetMessage.Text = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
                return;
            }

            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connString))
                {
                    connection.Open();

                    // Verify if the email exists
                    string verifyQuery = "SELECT COUNT(*) FROM Login_TBL WHERE email = ?";
                    using (OleDbCommand verifyCommand = new OleDbCommand(verifyQuery, connection))
                    {
                        verifyCommand.Parameters.AddWithValue("@email", email);

                        int count = (int)verifyCommand.ExecuteScalar();
                        if (count == 0)
                        {
                            lblResetMessage.Text = "Email not found.";
                            return;
                        }
                    }

                    // Update the password
                    string query = "UPDATE Login_TBL SET [password] = ? WHERE [email] = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@password", newPassword);
                        command.Parameters.AddWithValue("@email", email);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            lblResetMessage.Text = "Password reset successfully!";
                        }
                        else
                        {
                            lblResetMessage.Text = "Failed to reset password.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblResetMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Home.aspx");
        }

        private bool IsValidPassword(string password)
        {
            // Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
