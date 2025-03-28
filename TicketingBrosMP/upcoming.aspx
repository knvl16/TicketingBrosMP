﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="upcoming.aspx.cs" Inherits="TicketingBrosMP.upcoming" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .movie-container {
            display: flex;
            align-items: flex-start;
            gap: 30px;
            margin-bottom: 50px;
            padding: 20px;
            background: #fff;
            border-radius: 10px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
            position: relative;
        }
        .movie-poster {
            width: 300px;
            height: 450px;
            border-radius: 10px;
            object-fit: cover;
        }
        .movie-details { flex: 1; }
        .movie-title {
            font-size: 28px;
            font-weight: bold;
            margin-bottom: 10px;
        }
<<<<<<< HEAD
=======
        .movie-title a {
            color: inherit;
            text-decoration: none;
        }
        .movie-title a:hover {
            text-decoration: underline;
        }
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
        .movie-meta { font-size: 16px; color: #666; margin-bottom: 5px; }
        .movie-description { margin-top: 15px; font-size: 16px; line-height: 1.6; }
        .cast-container { margin-top: 20px; }
        .cast-title { font-size: 20px; font-weight: bold; margin-bottom: 15px; }
        .cast-list { display: flex; flex-wrap: wrap; gap: 15px; }
        .cast-member { width: 120px; text-align: center; }
        .cast-photo {
            width: 100px;
            height: 100px;
            border-radius: 50%;
            object-fit: cover;
            margin-bottom: 5px;
        }
        .cast-name { font-size: 14px; font-weight: bold; }
        .upcoming-badge {
            position: absolute;
            top: 10px;
            left: 10px;
            background: orange;
            color: white;
            font-weight: bold;
            font-size: 20px;
            padding: 10px 20px;
            text-transform: uppercase;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
        }
<<<<<<< HEAD
=======
        .no-movies {
            text-align: center;
            font-size: 24px;
            font-weight: bold;
            color: #888;
            margin-top: 50px;
            display: none;
        }
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="text-center mt-4">Upcoming Movies</h2>

        <asp:Repeater ID="rptUpcomingMovies" runat="server">
            <ItemTemplate>
                <div class="movie-container">
                    <span class="upcoming-badge">Upcoming</span>
<<<<<<< HEAD
                    <!-- Movie Poster -->
                    <img src='<%# Eval("PosterPath") %>' alt='<%# Eval("Title") %> Poster' class="movie-poster">
                    
                    <!-- Movie Details -->
                    <div class="movie-details">
                        <div class="movie-title"><%# Eval("Title") %></div>
=======
                    <img src='<%# Eval("PosterPath") %>' alt='<%# Eval("Title") %> Poster' class="movie-poster" />
                    <div class="movie-details">
                        <div class="movie-title">
                            <a href='<%# GetUrl(Eval("ImdbLink").ToString()) %>' target="_blank">
                                <%# Eval("Title") %>
                            </a>
                        </div>
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                        <div class="movie-meta">Genre: <%# Eval("Genre") %> | Duration: <%# Eval("Duration") %></div>
                        <div class="movie-meta">Director: <%# Eval("Director") %></div>
                        <div class="movie-meta">Writer: <%# Eval("Writer") %></div>
                        <div class="movie-meta">
<<<<<<< HEAD
                            <strong>Releasing On:</strong> <%# Eval("ShowingDate", "{0:MMMM dd, yyyy}") %> 
                        </div>
                        <div class="movie-description"><%# Eval("Description") %></div>

                        <!-- Cast Section -->
                        <div class="cast-container">
                            <div class="cast-title">Cast</div>
                            <div class="cast-list">
                                <!-- First Cast Member -->
                                <div class="cast-member">
                                    <img src='<%# Eval("Cast1PhotoPath") %>' alt='<%# Eval("Cast1Name") %>' class="cast-photo">
                                    <div class="cast-name"><%# Eval("Cast1Name") %></div>
                                </div>
                                <!-- Second Cast Member -->
                                <div class="cast-member">
                                    <img src='<%# Eval("Cast2PhotoPath") %>' alt='<%# Eval("Cast2Name") %>' class="cast-photo">
=======
                            <strong>Releasing On:</strong> <%# Eval("ShowingDate", "{0:MMMM dd, yyyy}") %>
                        </div>
                        <div class="movie-description"><%# Eval("Description") %></div>
                        <div class="cast-container">
                            <div class="cast-title">Cast</div>
                            <div class="cast-list">
                                <div class="cast-member">
                                    <img src='<%# Eval("Cast1PhotoPath") %>' alt='<%# Eval("Cast1Name") %>' class="cast-photo" />
                                    <div class="cast-name"><%# Eval("Cast1Name") %></div>
                                </div>
                                <div class="cast-member">
                                    <img src='<%# Eval("Cast2PhotoPath") %>' alt='<%# Eval("Cast2Name") %>' class="cast-photo" />
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                                    <div class="cast-name"><%# Eval("Cast2Name") %></div>
                                </div>
                            </div>
                        </div>
<<<<<<< HEAD
                        <!-- End Cast Section -->
=======
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
<<<<<<< HEAD
    </div>
</asp:Content>
=======
        
        <asp:Panel ID="pnlNoMovies" runat="server" CssClass="no-movies">
            No Upcoming Movies Available
        </asp:Panel>
    </div>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var repeater = document.getElementById("<%= rptUpcomingMovies.ClientID %>");
            var noMoviesPanel = document.getElementById("<%= pnlNoMovies.ClientID %>");
            if (!repeater || repeater.innerHTML.trim() === "") {
                noMoviesPanel.style.display = "block";
            }
        });
    </script>
</asp:Content>
>>>>>>> 1b8cdc045b16b66973df527adcd1cfedf24da153
