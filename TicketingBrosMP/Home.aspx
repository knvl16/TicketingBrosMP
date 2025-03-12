<%@ Page Title="Home" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TicketingBrosMP.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .carousel-container {
            max-width: 100%;
            margin: auto;
        }

        .carousel-inner img {
            width: 100%;
            height: 400px; /* Fixed height */
            object-fit: cover;
            border-radius: 10px;
        }

        .video-container {
            max-width: 800px;
            margin: auto;
            text-align: center;
        }

        .video-container iframe {
            width: 100%;
            height: 450px;
            border-radius: 10px;
        }

        .logo-container img {
            width: 150px;
            height: auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <!-- Logo and Video Section -->
        <div class="row align-items-center mb-5">
            <!-- Logo -->
            <div class="col-md-4 text-center">
                <img src="Picture/logo.png" class="img-fluid" alt="Website Logo">
            </div>
            <!-- Video -->
            <div class="col-md-8">
                <div class="video-container">
                    <h2 class="text-center text-dark fw-bold">Featured Movie Trailer</h2>
                    <iframe src="https://www.youtube.com/embed/2zQtb0H1QEs" frameborder="0"
                            allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" 
                            allowfullscreen>
                    </iframe>
                </div>
            </div>
        </div>

        <div class="row">
            <!-- Now Showing Section -->
            <div class="col-md-6">
                <h2 class="text-center text-dark fw-bold">Now Showing</h2>
                <div id="movieCarousel" class="carousel slide carousel-container" data-bs-ride="carousel">
                    <div class="carousel-inner">
                        <asp:Repeater ID="rptNowShowing" runat="server">
                            <ItemTemplate>
                                <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                    <img src='<%# Eval("PosterPath") %>' class="d-block w-100" alt='<%# Eval("Title") %>'>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <button class="carousel-control-prev" type="button" data-bs-target="#movieCarousel" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#movieCarousel" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    </button>
                </div>
            </div>

            <!-- Upcoming Movies Section -->
            <div class="col-md-6">
                <h2 class="text-center text-dark fw-bold">Upcoming Movies</h2>
                <div id="upcomingMovieCarousel" class="carousel slide carousel-container" data-bs-ride="carousel">
                    <div class="carousel-inner">
                        <asp:Repeater ID="rptUpcomingMovies" runat="server">
                            <ItemTemplate>
                                <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                    <img src='<%# Eval("PosterPath") %>' class="d-block w-100" alt='<%# Eval("Title") %>'>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <button class="carousel-control-prev" type="button" data-bs-target="#upcomingMovieCarousel" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#upcomingMovieCarousel" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
