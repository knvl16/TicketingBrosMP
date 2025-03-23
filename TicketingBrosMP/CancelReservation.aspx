<<<<<<< HEAD
﻿<%@ Page Title="Cancel Reservation" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CancelReservation.aspx.cs" Inherits="TicketingBrosMP.CancelReservation" %>
=======
﻿<%@ Page Title="Recent Ticket Cancellation" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CancelReservation.aspx.cs" Inherits="TicketingBrosMP.RecentTicketCancellation" %>
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <style>
<<<<<<< HEAD
        .movie-container {
=======
        .ticket-container {
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
            max-width: 900px;
            margin: 40px auto;
            background: white;
            padding: 30px;
            border-radius: 16px;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.12);
<<<<<<< HEAD
            transition: transform 0.2s;
=======
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
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
        
<<<<<<< HEAD
        .reservation-card {
=======
        .ticket-card {
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
            border: 1px solid #e6e6e6;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 20px;
            transition: all 0.3s ease;
            position: relative;
        }
        
<<<<<<< HEAD
        .reservation-card:hover {
=======
        .ticket-card:hover {
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
            transform: translateY(-3px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
        }
        
        .movie-title {
            color: #2c3e50;
            font-weight: 600;
            font-size: 1.25rem;
            display: flex;
            align-items: center;
<<<<<<< HEAD
=======
            justify-content: space-between;
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }
        
        .movie-title i {
            margin-right: 10px;
            color: #e74c3c;
        }
        
<<<<<<< HEAD
        .seat-map {
=======
        .timer-badge {
            font-size: 0.85rem;
            margin-left: 15px;
            padding: 5px 10px;
            border-radius: 20px;
            background: #f39c12;
            color: white;
            display: inline-flex;
            align-items: center;
        }
        
        .timer-badge i {
            color: white;
            margin-right: 5px;
        }
        
        .seat-info {
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
            display: flex;
            flex-wrap: wrap;
            padding: 15px 0;
        }
        
        .seat-checkbox {
<<<<<<< HEAD
            margin: 10px;
=======
            margin: 5px 10px;
            display: flex;
            align-items: center;
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }
        
        .seat-checkbox input[type="checkbox"] {
            width: 18px;
            height: 18px;
            margin-right: 8px;
        }
        
        .seat-checkbox label {
            font-weight: 500;
            color: #34495e;
<<<<<<< HEAD
=======
            margin-bottom: 0;
        }
        
        .booking-details {
            background-color: #f8f9fa;
            padding: 12px;
            border-radius: 8px;
            margin-top: 10px;
            font-size: 0.9rem;
            color: #7f8c8d;
        }
        
        .booking-details p {
            margin-bottom: 0.5rem;
        }
        
        .booking-details span {
            color: #34495e;
            font-weight: 500;
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
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
        
<<<<<<< HEAD
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
=======
        .no-tickets {
            text-align: center;
            padding: 40px 20px;
            color: #7f8c8d;
        }
        
        .no-tickets i {
            font-size: 3rem;
            color: #bdc3c7;
            margin-bottom: 20px;
        }
        
        .expired-notice {
            background-color: #f8d7da;
            color: #721c24;
            padding: 6px 12px;
            border-radius: 4px;
            font-size: 0.9rem;
            margin-top: 10px;
            display: inline-block;
        }
        
        .countdown-timer {
            font-weight: bold;
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
<<<<<<< HEAD
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
=======
        <div class="ticket-container">
            <div class="text-center mb-4">
                <h2 class="page-title"><i class="fas fa-clock me-2"></i>Recent Ticket Cancellation</h2>
                <p class="page-subtitle">You can cancel tickets within 5 minutes of your purchase</p>
            </div>
            
            <asp:Panel ID="pnlNoTickets" runat="server" CssClass="no-tickets" Visible="false">
                <i class="fas fa-ticket-alt"></i>
                <h4>No Recent Ticket Purchases</h4>
                <p>You don't have any recently purchased tickets that can be cancelled.</p>
                <a href="BuyTickets2.aspx" class="btn btn-primary mt-3">Purchase Tickets</a>
            </asp:Panel>
            
            <asp:Repeater ID="rptRecentTickets" runat="server" OnItemDataBound="rptRecentTickets_ItemDataBound">
                <ItemTemplate>
                    <div class="ticket-card">
                        <h5 class="movie-title">
                            <div>
                                <i class="fas fa-film"></i><%# Eval("MovieTitle") %>
                            </div>
                            <asp:Panel ID="pnlCountdown" runat="server" CssClass="timer-badge" Visible='<%# Convert.ToBoolean(Eval("CanCancel")) %>'>
                                <i class="fas fa-hourglass-half"></i>
                                <span class="countdown-timer" data-seconds-remaining='<%# Eval("TimeRemaining") %>'>
                                    <%# ((int)Eval("TimeRemaining") / 60).ToString() %>:<%# (((int)Eval("TimeRemaining") % 60).ToString("00")) %>
                                </span>
                            </asp:Panel>
                            <asp:Panel ID="pnlExpired" runat="server" CssClass="expired-notice" Visible='<%# !Convert.ToBoolean(Eval("CanCancel")) %>'>
                                <i class="fas fa-exclamation-circle"></i> Cancellation period expired
                            </asp:Panel>
                        </h5>
                        
                        <div class="seat-info">
                            <div class="seat-checkbox">
                                <asp:CheckBox ID="chkSeat" runat="server" 
                                 Enabled='<%# Convert.ToBoolean(Eval("CanCancel")) %>' />
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                                <label for='<%# ((CheckBox)Container.FindControl("chkSeat")).ClientID %>'>
                                    Seat <%# Eval("Seats") %>
                                </label>
                            </div>
                        </div>
<<<<<<< HEAD
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
=======
                        
                        <div class="booking-details">
                            <p><strong>Booking ID:</strong> <span><%# Eval("ID") %></span></p>
                            <p><strong>Booked on:</strong> <span><%# Convert.ToDateTime(Eval("BookingTime")).ToString("MMM dd, yyyy hh:mm:ss tt") %></span></p>
                        </div>
                        
                        <asp:HiddenField ID="hfMovieTitle" runat="server" Value='<%# Eval("MovieTitle") %>' />
                        <asp:HiddenField ID="hfBookingID" runat="server" Value='<%# Eval("ID") %>' />
                        <asp:HiddenField ID="hfSeatNumber" runat="server" Value='<%# Eval("Seats") %>' />
                        <asp:HiddenField ID="hfCanCancel" runat="server" Value='<%# Eval("CanCancel") %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
            <div class="text-center mt-4">
                <asp:Button ID="btnCancelTickets" runat="server" Text="Cancel Selected Tickets" 
                    CssClass="btn btn-danger cancel-btn" OnClick="btnCancelTickets_Click" 
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
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
<<<<<<< HEAD
=======

        // Countdown timer functionality
        function startCountdowns() {
            const timers = document.querySelectorAll('.countdown-timer');

            timers.forEach(timer => {
                let seconds = parseInt(timer.getAttribute('data-seconds-remaining'));
                const ticketCard = timer.closest('.ticket-card');

                const interval = setInterval(() => {
                    seconds--;

                    if (seconds <= 0) {
                        clearInterval(interval);
                        // Refresh the page when timer expires
                        location.reload();
                    } else {
                        const minutes = Math.floor(seconds / 60);
                        const remainingSeconds = seconds % 60;
                        timer.textContent = `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;

                        // Change color when less than 1 minute remains
                        if (seconds < 60) {
                            timer.parentElement.style.backgroundColor = '#e74c3c';
                        }
                    }
                }, 1000);
            });
        }

        // Start all countdown timers when page loads
        window.addEventListener('DOMContentLoaded', startCountdowns);
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
    </script>
</asp:Content>