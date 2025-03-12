<%@ Page Title="Cancel Reservation" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CancelReservation.aspx.cs" Inherits="TicketingBrosMP.CancelReservation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <style>
        .movie-container {
            max-width: 900px;
            margin: 40px auto;
            background: white;
            padding: 30px;
            border-radius: 16px;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.12);
            transition: transform 0.2s;
        }
        
        .page-title {
            color: #2c3e50;
            font-weight: 700;
            margin-bottom: 0.5rem;
        }
        
        .page-subtitle {
            color: #7f8c8d;
            margin-bottom: 1.5rem;
        }
        
        .reservation-card {
            border: 1px solid #e6e6e6;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 20px;
            transition: all 0.3s ease;
            position: relative;
        }
        
        .reservation-card:hover {
            transform: translateY(-3px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
        }
        
        .movie-title {
            color: #2c3e50;
            font-weight: 600;
            font-size: 1.25rem;
            display: flex;
            align-items: center;
        }
        
        .movie-title i {
            margin-right: 10px;
            color: #e74c3c;
        }
        
        .seat-map {
            display: flex;
            flex-wrap: wrap;
            padding: 15px 0;
        }
        
        .seat-checkbox {
            margin: 10px;
        }
        
        .seat-checkbox input[type="checkbox"] {
            width: 18px;
            height: 18px;
            margin-right: 8px;
        }
        
        .seat-checkbox label {
            font-weight: 500;
            color: #34495e;
        }
        
        .cancel-btn {
            background: linear-gradient(to right, #e74c3c, #c0392b);
            border: none;
            padding: 12px 25px;
            font-weight: 600;
            letter-spacing: 0.5px;
            transition: all 0.3s ease;
        }
        
        .cancel-btn:hover {
            background: linear-gradient(to right, #c0392b, #e74c3c);
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(231, 76, 60, 0.3);
        }
        
        .cancel-btn:active {
            transform: translateY(0);
        }
        
        .no-reservations {
            text-align: center;
            padding: 30px;
            color: #7f8c8d;
            font-style: italic;
        }
        
        .confirmation-dialog {
            background: rgba(255, 255, 255, 0.95);
            border-radius: 10px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
            padding: 20px;
            max-width: 400px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="movie-container">
            <div class="text-center mb-4">
                <h2 class="page-title"><i class="fas fa-ticket-alt me-2"></i>Cancel Reservation</h2>
                <p class="page-subtitle">Please select the tickets you wish to cancel</p>
            </div>
            
            <asp:Panel ID="pnlNoReservations" runat="server" CssClass="no-reservations" Visible="false">
                <i class="fas fa-search fa-3x mb-3 text-muted"></i>
                <h4>No Active Reservations Found</h4>
                <p>You don't have any active reservations to cancel.</p>
                <a href="BuyTickets.aspx" class="btn btn-primary mt-3">Book New Tickets</a>
            </asp:Panel>
            
            <asp:Repeater ID="rptReservations" runat="server">
                <ItemTemplate>
                    <div class="reservation-card">
                        <h5 class="movie-title">
                            <i class="fas fa-film"></i><%# Eval("MovieTitle") %>
                        </h5>
                        <div class="seat-map">
                            <div class="seat-checkbox">
                                <asp:CheckBox ID="chkSeat" runat="server" Text='<%# Eval("Seats") %>' />
                                <label for='<%# ((CheckBox)Container.FindControl("chkSeat")).ClientID %>'>
                                    Seat <%# Eval("Seats") %>
                                </label>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfMovieTitle" runat="server" Value='<%# Eval("MovieTitle") %>' />
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Literal ID="litNoReservations" runat="server" />
                </FooterTemplate>
            </asp:Repeater>
            
            <div class="text-center mt-4">
                <asp:Button ID="btnCancelReservation" runat="server" Text="Cancel Selected Tickets" 
                    CssClass="btn btn-danger cancel-btn" OnClick="btnCancelReservation_Click" 
                    OnClientClick="return confirmCancellation();" />
            </div>
        </div>
    </div>
    
    <script>
        function confirmCancellation() {
            // Check if any checkboxes are selected
            let checkboxes = document.querySelectorAll('input[type="checkbox"]:checked');
            if (checkboxes.length === 0) {
                alert('Please select at least one ticket to cancel.');
                return false;
            }

            return confirm('Are you sure you want to cancel the selected tickets? This action cannot be undone.');
        }
    </script>
</asp:Content>