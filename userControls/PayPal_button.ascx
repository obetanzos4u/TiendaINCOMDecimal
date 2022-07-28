<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PayPal_button.ascx.cs" Inherits="userControls_PayPal_button" %>


<script>
    paypal.Buttons({
        createOrder: function (data, actions) {
            return actions.order.create({
                purchase_units: [{
                     amount: {
                         value: '50.00'
                     },
                     description  : "Pedido de prueba"
                 }]
             });
         },
         onApprove: function (data, actions) {
             return actions.order.capture().then(function (details) {
                 alert('Transaction completed by ' + details.payer.name.given_name);
                 // Call your server to save the transaction
                 return fetch('/paypal-transaction-complete', {
                     method: 'post',
                     headers: {
                         'content-type': 'application/json'
                     },
                     body: JSON.stringify({
                         orderID: data.orderID
                     })
                 });
             });
         }
     }).render('#paypal-button-container');
</script>
 