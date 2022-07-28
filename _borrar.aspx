<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_borrar.aspx.cs" Inherits="_borrar" %>

<!DOCTYPE html>
<html lang="">

<head>
	<meta charset="utf-8">
	<title>Example Title</title>
	<meta name="author" content="Your Name">
	<meta name="description" content="Example description">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
    
    <meta name="google-signin-client_id" content="735817781293-gj8bqpplf0jt9au330hcejcp06js20di.apps.googleusercontent.com">

	<link rel="stylesheet" href="">
	<link rel="icon" type="image/x-icon" href=""/>
    
    <script src="https://apis.google.com/js/platform.js" async defer></script>

</head>

<body>
	<header><h1 id="saludo">Hola</h1></header>
    
    <div class="g-signin2" data-onsuccess="onSignIn"></div>
	<br />
	<textarea id="token"></textarea>	<br />---- <br />
	<a href="#" onclick="signOut();">Sign out</a>

	<main></main>
	<footer></footer>
	<script>
        function signOut() {
            var auth2 = gapi.auth2.getAuthInstance();
            auth2.signOut().then(function () {
                console.log('User signed out.');
            });
		}


    function onSignIn(googleUser) {
		var profile = googleUser.getBasicProfile();
        var id_token = googleUser.getAuthResponse().id_token;

  console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
  console.log('Name: ' + profile.getName());
  console.log('Image URL: ' + profile.getImageUrl());
		console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.

		document.querySelector("#saludo").textContent = "Hola " + profile.getName();
		document.querySelector("#token").value = id_token;

}
    </script>
</body>

</html>