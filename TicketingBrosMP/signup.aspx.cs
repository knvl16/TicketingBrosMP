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
    public partial class signup : System.Web.UI.Page
    {
        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string newEmail = txtEmail.Text.Trim();
            string newUsername = txtNewUsername.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(newEmail) || string.IsNullOrEmpty(newUsername) ||
                string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                lblErrorMessage.Text = "Please fill in all fields.";
                lblErrorMessage.Visible = true;
                return;
            }

            // Email validation
            if (!IsValidEmail(newEmail))
            {
                lblErrorMessage.Text = "Please enter a valid email address.";
                lblErrorMessage.Visible = true;
                return;
            }

            if (newPassword != confirmPassword)
            {
                lblErrorMessage.Text = "Passwords do not match.";
                lblErrorMessage.Visible = true;
                return;
            }

            // Password validation
            if (!IsValidPassword(newPassword))
            {
                lblErrorMessage.Text = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
                lblErrorMessage.Visible = true;
                return;
            }

            //Username validation
            if (!IsValidUsername(newUsername))
            {
                lblErrorMessage.Text = "Username must be between 3 and 20 characters long and contain only alphanumeric characters and underscores.";
                lblErrorMessage.Visible = true;
                return;
            }

            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connString))
                {
                    connection.Open();

                    // Check if the username already exists
                    string checkUsernameQuery = "SELECT COUNT(*) FROM Login_TBL WHERE [username] = ?";
                    using (OleDbCommand checkUsernameCommand = new OleDbCommand(checkUsernameQuery, connection))
                    {
                        checkUsernameCommand.Parameters.AddWithValue("?", newUsername);
                        int existingCount = (int)checkUsernameCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            lblErrorMessage.Text = "Username already exists.";
                            lblErrorMessage.Visible = true;
                            return;
                        }
                    }

                    // Check if the email already exists
                    string checkEmailQuery = "SELECT COUNT(*) FROM Login_TBL WHERE [email] = ?";
                    using (OleDbCommand checkEmailCommand = new OleDbCommand(checkEmailQuery, connection))
                    {
                        checkEmailCommand.Parameters.AddWithValue("?", newEmail);
                        int existingEmailCount = (int)checkEmailCommand.ExecuteScalar();
                        if (existingEmailCount > 0)
                        {
                            lblErrorMessage.Text = "Email already exists.";
                            lblErrorMessage.Visible = true;
                            return;
                        }
                    }

                    // Insert new user
                    string insertQuery = "INSERT INTO Login_TBL ([username], [password], [email]) VALUES (?, ?, ?)";
                    using (OleDbCommand insertCommand = new OleDbCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("?", newUsername);
                        insertCommand.Parameters.AddWithValue("?", newPassword); // Consider hashing the password
                        insertCommand.Parameters.AddWithValue("?", newEmail);

                        insertCommand.ExecuteNonQuery();

                        lblSuccessMessage.Text = "Sign up successful!";
                        lblSuccessMessage.Visible = true;
                        lblErrorMessage.Visible = false;

                        // Clear the input fields
                        txtEmail.Text = "";
                        txtNewUsername.Text = "";
                        txtNewPassword.Text = "";
                        txtConfirmPassword.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPassword(string password)
        {
            // Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        private bool IsValidUsername(string username)
        {
            //Username must be between 3 and 20 characters long and contain only alphanumeric characters and underscores.
            string pattern = @"^[a-zA-Z0-9_]{3,20}$";
            return Regex.IsMatch(username, pattern);
        }
    }
}