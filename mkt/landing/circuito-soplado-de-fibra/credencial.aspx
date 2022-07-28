<%@ Page Language="C#" AutoEventWireup="true" CodeFile="credencial.aspx.cs" Inherits="mkt_circuito_soplado_de_fibra_credencial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Credencial</title>
    <style>
        p {
            color: black;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

            p.nombre {
                text-align:center;
                width: 9.7cm;
                height: 2.2cm;
                line-height: 2.2cm;
                padding: 5.7cm 0.5cm 0cm 0.5cm;
                margin: 0px;
            }

                p.nombre > span {
                    vertical-align: text-top;
                }

            p.apellido {
                padding: 0.4cm 0.6cm 0cm 0.5cm;
                margin: 0px;
                width: 9.7cm;
                height: 1.2cm;
                text-align:center;

            }

                p.apellido > span {
                    vertical-align: text-top;
                }

            p.empresa {
                 padding: 0cm 0.5cm 0.2cm 0.5cm;
                margin: 0px;
                width: 9.7cm;
                height: 1cm;

                text-align:center;
            }

        .credencial {
            background: url(20210727-credencial.jpg);
            background-size: contain;
            width: 10.295cm;
            height: 13.5cm;
        }
    </style>
    <script src="textFit.min.js"></script>



        <script src="html2pdf.js"></script>

</head>
<body>

    <form id="form1" runat="server">
        <h1 id="msg_result" visible="false" runat="server"></h1>
        <div id="credencial" class="credencial" runat="server">
            <p class="nombre"><asp:Literal ID="lt_Nombre" runat="server"></asp:Literal></p>

            <p class="apellido"><asp:Literal ID="lt_Apellidos" runat="server"></asp:Literal></p>
            <p class="empresa"><asp:Literal ID="lt_Empresa" runat="server"></asp:Literal></p>
        </div>
    </form>
            <script>
                textFit(document.getElementsByClassName('nombre'));

                textFit(document.getElementsByClassName('apellido'), { maxFontSize: 35 });
                textFit(document.getElementsByClassName('empresa'));


                function generatePDF() {
                    // Choose the element that our invoice is rendered in.
                    const element = document.getElementsByTagName("body")[0];


                    var opt = {
                        margin: 4,
                        filename: 'incom-fibra-soplada.pdf',
                        image: { type: 'jpeg', quality: 1 },

                        jsPDF: { format: 'letter' }
                    };

                    // Choose the element and save the PDF for our user.
                    html2pdf()
                        .set(opt)
                        .from(element)
                        .save();
                }



                document.addEventListener("DOMContentLoaded", () => {
                 
                });
            </script>
</body>

</html>
