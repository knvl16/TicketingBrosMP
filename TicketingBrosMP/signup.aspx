<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="TicketingBrosMP.signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .signup-container {
<<<<<<< HEAD
            width: 350px;
            padding: 40px;
            background-color: #f8f9fa;
            border-radius: 12px;
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1);
            text-align: center;
            margin: 50px auto;
            border: 1px solid #dee2e6;
        }

        .signup-container h2 {
            margin-bottom: 30px;
            color: #495057;
            font-weight: 600;
=======
            width: 400px;
            padding: 50px;
            background-color: #ffffff;
            border-radius: 15px;
            box-shadow: 0 10px 20px rgba(0, 0, 0, 0.15);
            text-align: center;
            margin: 60px auto;
            border: 1px solid #ddd;
            transition: transform 0.3s ease-in-out;
        }

        .signup-container:hover {
            transform: scale(1.02);
        }

        .signup-container h2 {
            margin-bottom: 25px;
            color: #343a40;
            font-weight: bold;
            font-size: 24px;
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }

        .form-group {
            margin-bottom: 20px;
            text-align: left;
        }

        .form-group label {
            display: block;
            margin-bottom: 8px;
<<<<<<< HEAD
            color: #343a40;
            font-weight: 500;
=======
            color: #495057;
            font-weight: 600;
            font-size: 14px;
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }

        .form-control {
            width: 100%;
            padding: 12px;
            border: 1px solid #ced4da;
<<<<<<< HEAD
            border-radius: 8px;
            box-sizing: border-box;
            font-size: 16px;
            transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
        }

        .form-control:focus {
            border-color: #80bdff;
            box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
            outline: 0;
        }

        .btn-signup {
            background-color: #800000;
            color: white;
            padding: 12px 24px;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            width: 100%;
            font-size: 16px;
            font-weight: 600;
            transition: background-color 0.15s ease-in-out;
        }

        .btn-signup:hover {
            background-color: #CC3333;
=======
            border-radius: 10px;
            font-size: 16px;
            transition: all 0.3s;
        }

        .form-control:focus {
            border-color: #007bff;
            box-shadow: 0 0 8px rgba(0, 123, 255, 0.3);
            outline: none;
        }

        .btn-signup {
            background: linear-gradient(135deg, #79642C, #5a4b20);
            color: white;
            padding: 14px;
            border: none;
            border-radius: 10px;
            cursor: pointer;
            width: 100%;
            font-size: 16px;
            font-weight: bold;
            transition: all 0.3s;
        }

        .btn-signup:hover {
            background: linear-gradient(135deg, #5a4b20, #79642C);
            transform: translateY(-2px);
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }

        .error-message {
            color: #dc3545;
            margin-top: 10px;
            font-size: 14px;
<<<<<<< HEAD
=======
            font-weight: bold;
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }

        .success-message {
            color: #28a745;
            margin-top: 10px;
            font-size: 14px;
<<<<<<< HEAD
=======
            font-weight: bold;
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }

        .login-link {
            margin-top: 20px;
            font-size: 14px;
        }

        .login-link a {
            color: #007bff;
            text-decoration: none;
<<<<<<< HEAD
            cursor: pointer;
=======
            font-weight: bold;
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }

        .login-link a:hover {
            text-decoration: underline;
        }
    </style>
    <script>
        function openLoginModal() {
            // Correctly uses the Bootstrap 5 modal syntax.
            var loginModal = new bootstrap.Modal(document.getElementById('loginModal'));
            loginModal.show();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="signup-container">
        <h2>Create Account</h2>

        <asp:Label ID="lblSuccessMessage" runat="server" CssClass="success-message" Visible="false"></asp:Label>
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>

        <div class="form-group">
            <label for="txtEmail">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter your email"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtNewUsername">Username</label>
            <asp:TextBox ID="txtNewUsername" runat="server" CssClass="form-control" Placeholder="Enter your username"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtNewPassword">Password</label>
            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Enter your password"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtConfirmPassword">Confirm Password</label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Confirm your password"></asp:TextBox>
        </div>

        <asp:Button ID="btnSignup" runat="server" Text="Sign Up" CssClass="btn-signup" OnClick="btnSignup_Click" />

        <div class="login-link">
            Already have an account? <a href="#" onclick="openLoginModal(); return false;">Login</a>
        </div>
    </div>

    <div class="modal fade" id="loginModal" tabindex="-1" aria-labelledby="loginModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="loginModalLabel">Login</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>Login form content goes here...</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Login</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>