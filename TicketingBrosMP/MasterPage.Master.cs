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
                liLogout.Visible = true;       // Make the welcome message container visible
                liLogoutBtn.Visible = true;    // Make the logout button container visible
                btnLogout.Visible = true;      // Make the logout button visible
                liLogin.Visible = false;       // Hide login button container
            }
            else
            {
                lblUsername.Text = "";
                liLogout.Visible = false;      // Hide the welcome message container
                liLogoutBtn.Visible = false;   // Hide the logout button container
                btnLogout.Visible = false;     // Hide the logout button
                liLogin.Visible = true;        // Show login button container
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validate input before processing login
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblLoginMessage.Text = "Username and password are required!";
                // Keep the login modal open
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenLoginModal",
                    "setTimeout(function() { new bootstrap.Modal(document.getElementById('loginModal')).show(); }, 100);", true);
                return; // Stop execution here
            }

            string connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + Server.MapPath("~/App_Data/TicketingBros.mdb");

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connString))
                {
                    connection.Open();

                    // Fetch password from database for comparison
                    string query = "SELECT password FROM Login_TBL WHERE username = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", username);

                        object result = command.ExecuteScalar();

                        if (result != null && result.ToString() == password) // Compare fetched password
                        {
                            Session["Username"] = username;
                            UpdateLoginStatus(); // Update login status

                            // Redirect only after successful authentication
                            Response.Redirect("Home.aspx");
                        }
                        else
                        {
                            lblLoginMessage.Text = "Invalid username or password.";
                            // Keep the modal open
                            ScriptManager.RegisterStartupScript(this, GetType(), "OpenLoginModal",
                                "setTimeout(function() { new bootstrap.Modal(document.getElementById('loginModal')).show(); }, 100);", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblLoginMessage.Text = "Error: " + ex.Message;
                // Keep the modal open
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenLoginModal",
                    "setTimeout(function() { new bootstrap.Modal(document.getElementById('loginModal')).show(); }, 100);", true);
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
                ShowResetModal();
                return;
            }

            if (newPassword != confirmPassword)
            {
                lblResetMessage.Text = "New password and confirmation do not match.";
                ShowResetModal();
                return;
            }

            // Password Validation
            if (!IsValidPassword(newPassword))
            {
                lblResetMessage.Text = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
                ShowResetModal();
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
                        verifyCommand.Parameters.AddWithValue("?", email);

                        int count = Convert.ToInt32(verifyCommand.ExecuteScalar());
                        if (count == 0)
                        {
                            lblResetMessage.Text = "Email not found.";
                            ShowResetModal();
                            return;
                        }
                    }

                    // Update the password
                    string query = "UPDATE Login_TBL SET [password] = ? WHERE [email] = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", newPassword);
                        command.Parameters.AddWithValue("?", email);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Clear the fields
                            txtResetEmail.Text = "";
                            txtNewPasswordReset.Text = "";
                            txtConfirmNewPassword.Text = "";

                            // Show success message
                            lblResetMessage.Text = "Password reset successfully!";

                            // Close modal after a delay
                            ScriptManager.RegisterStartupScript(this, GetType(), "ShowSuccessAndClose",
                                "setTimeout(function() { " +
                                "  alert('Password reset successfully! You can now login with your new password.'); " +
                                "  var modal = bootstrap.Modal.getInstance(document.getElementById('resetPasswordModal')); " +
                                "  if (modal) modal.hide(); " +
                                "}, 2000);", true);
                        }
                        else
                        {
                            lblResetMessage.Text = "Failed to reset password.";
                            ShowResetModal();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblResetMessage.Text = "Error: " + ex.Message;
                ShowResetModal();
            }
        }

        // Function to keep the modal open
        private void ShowResetModal()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenResetModal",
                "setTimeout(function() { new bootstrap.Modal(document.getElementById('resetPasswordModal')).show(); }, 100);", true);
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
