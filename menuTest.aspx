<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="menuTest.aspx.cs" MasterPageFile="~/general.master" Inherits="menuTest" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">






    <nav class="menuContainer">
        <ul>
            <li menu-active="menu-categorias">
                <a href="#">Categorias
                </a>

                    <div class="row menu-items">
                        <div class="col l2 m4 s12">
                            <a class="menu-l1" href="#">Fibra Óptica</a>
                            <ul>
                                <li>Cables</li>
                                <li>Adss</li>
                            </ul>
                        </div>
                    </div>
            </li>
            <li><a menu-active="menu-categorias" href="#">Productos</a>  </li>

        </ul>

    </nav>
    <style>
        .menuContainer > ul > li {
            display: inline;
            float: left;
        }

            .menuContainer > ul > li > div {
                margin-top: 8px;
                width: 100%;
                display:none;
                position: absolute;
                padding: 20px;
                box-shadow: 1px 3px 10px rgba(0, 0, 0, 0.16);
            }
            .menuContainer > ul > li > a {
                padding: 10px 5px;
                background: #0496ce;
                color: white;
            }
            .menu-items{

            }
            .menu-items > li{
                float: left;
                padding-right: 2rem;
            }
    </style>
    <script>
        $(document).ready(function () {



            $('li[menu-active="menu-categorias"]').on({
                mouseenter: function () {
                    console.log($(this).children().next());
                    $(this).children().next().show(200);
                },
                mouseleave: function () {
                 
                 
                    $(this).children().next().hide();
                       
                
                   
                }


        });

            
        });



      


       
    </script>
</asp:Content>
