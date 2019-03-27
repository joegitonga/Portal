<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
      <header class="header dark-bg">
            <div class="toggle-nav">
                <div class="icon-reorder tooltips" data-original-title="Toggle Navigation" data-placement="bottom"><i class="icon_menu"></i></div>
            </div>

            <!--logo start-->
            <a href="#" class="logo"> <span class="lite">Admin</span></a>
            <!--logo end-->

            <div class="top-nav notification-row">                
                <!-- notificatoin dropdown start-->
                <ul class="nav pull-right top-menu">
                    <!-- user login dropdown start-->
                    <li class="dropdown">
                        <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                            <span class="profile-ava">
                            </span>
                            <span class="username">Admin</span>
                            <b class="caret"></b>
                        <ul class="dropdown-menu extended logout">
                            <div class="log-arrow-up"></div>
                            <li>
                                <a href="../Account/Login"><i class="icon_key_alt"></i> Log Out</a>
                            </li>
                        </ul>
                        </a>
                    </li>
                    <!-- user login dropdown end -->
                </ul>
                <!-- notificatoin dropdown end-->
            </div>
      </header>
