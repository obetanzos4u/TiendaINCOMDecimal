<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_share_btn.ascx.cs" Inherits="uc_share_btn" %>

<div class="wrapper-btn-share">
<div class="btn-share">
    <span class="btn-text">Compartir</span><span class="btn-icon">
        <svg
            t="1580465783605"
            class="icon-share"
            viewBox="0 0 1024 1024"
            version="1.1"
            xmlns="http://www.w3.org/2000/svg"
            p-id="9773"
            width="18px"
            height="18px">
            <path
                d="M767.99994 585.142857q75.995429 0 129.462857 53.394286t53.394286 129.462857-53.394286 129.462857-129.462857 53.394286-129.462857-53.394286-53.394286-129.462857q0-6.875429 1.170286-19.456l-205.677714-102.838857q-52.589714 49.152-124.562286 49.152-75.995429 0-129.462857-53.394286t-53.394286-129.462857 53.394286-129.462857 129.462857-53.394286q71.972571 0 124.562286 49.152l205.677714-102.838857q-1.170286-12.580571-1.170286-19.456 0-75.995429 53.394286-129.462857t129.462857-53.394286 129.462857 53.394286 53.394286 129.462857-53.394286 129.462857-129.462857 53.394286q-71.972571 0-124.562286-49.152l-205.677714 102.838857q1.170286 12.580571 1.170286 19.456t-1.170286 19.456l205.677714 102.838857q52.589714-49.152 124.562286-49.152z"
                p-id="9774"
                fill="#ffffff">
            </path>
        </svg>
    </span>
    <ul class="social-icons">
        <li>
            <a id="share-facebook" href="#" target="popup" onclick="share(event,'facebook');return false;">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 50 50" width="24px" height="24px">
                    <g id="surface278803211">
                        <path style="stroke: none; fill-rule: nonzero; fill: rgb(100%,100%,100%); fill-opacity: 1;" d="M 41 4 L 9 4 C 6.238281 4 4 6.238281 4 9 L 4 41 C 4 43.761719 6.238281 46 9 46 L 41 46 C 43.761719 46 46 43.761719 46 41 L 46 9 C 46 6.238281 43.761719 4 41 4 Z M 37 19 L 35 19 C 32.859375 19 32 19.5 32 21 L 32 24 L 37 24 L 36 29 L 32 29 L 32 44 L 27 44 L 27 29 L 23 29 L 23 24 L 27 24 L 27 21 C 27 17 29 14 33 14 C 35.898438 14 37 15 37 15 Z M 37 19 " />
                    </g>
                </svg>
            </a>
        </li>
        <li>
            <a id="share-twitter" href="#" target="popup" onclick="share(event,'twitter');return false;">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 30 30" width="24px" height="24px">
                    <g id="surface278001823">
                        <path style="stroke: none; fill-rule: nonzero; fill: rgb(100%,100%,100%); fill-opacity: 1;" d="M 28 6.9375 C 27.042969 7.363281 26.015625 7.648438 24.9375 7.777344 C 26.039062 7.117188 26.882812 6.070312 27.28125 4.824219 C 26.25 5.4375 25.109375 5.882812 23.894531 6.121094 C 22.921875 5.085938 21.535156 4.4375 20 4.4375 C 17.054688 4.4375 14.664062 6.824219 14.664062 9.769531 C 14.664062 10.1875 14.714844 10.597656 14.804688 10.984375 C 10.371094 10.761719 6.441406 8.640625 3.808594 5.410156 C 3.351562 6.199219 3.089844 7.113281 3.089844 8.09375 C 3.089844 9.945312 4.027344 11.578125 5.460938 12.53125 C 4.585938 12.503906 3.761719 12.265625 3.042969 11.867188 C 3.042969 11.890625 3.042969 11.910156 3.042969 11.933594 C 3.042969 14.519531 4.882812 16.675781 7.324219 17.164062 C 6.875 17.285156 6.402344 17.351562 5.917969 17.351562 C 5.574219 17.351562 5.238281 17.316406 4.914062 17.253906 C 5.59375 19.375 7.5625 20.917969 9.898438 20.960938 C 8.070312 22.390625 5.773438 23.242188 3.273438 23.242188 C 2.84375 23.242188 2.417969 23.21875 2 23.167969 C 4.359375 24.683594 7.164062 25.566406 10.175781 25.566406 C 19.988281 25.566406 25.351562 17.4375 25.351562 10.386719 C 25.351562 10.15625 25.347656 9.925781 25.335938 9.699219 C 26.378906 8.945312 27.285156 8.007812 28 6.9375 Z M 28 6.9375 " />
                    </g>
                </svg>
            </a>
        </li>
        <li>
            <a id="share-whatsapp" href="#" target="popup" onclick="share(event, 'whatsapp');return false;">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 30 30" width="24px" height="24px">
                    <g id="surface133212">
                        <path style="stroke: none; fill-rule: nonzero; fill: rgb(100%,100%,100%); fill-opacity: 1;" d="M 15 3 C 8.371094 3 3 8.371094 3 15 C 3 17.25 3.632812 19.351562 4.710938 21.152344 L 3.105469 27 L 9.082031 25.433594 C 10.828125 26.425781 12.847656 27 15 27 C 21.628906 27 27 21.628906 27 15 C 27 8.371094 21.628906 3 15 3 Z M 10.890625 9.402344 C 11.085938 9.402344 11.289062 9.402344 11.460938 9.410156 C 11.675781 9.414062 11.90625 9.429688 12.128906 9.921875 C 12.394531 10.511719 12.972656 11.980469 13.046875 12.128906 C 13.121094 12.277344 13.171875 12.453125 13.070312 12.648438 C 12.972656 12.847656 12.921875 12.96875 12.777344 13.144531 C 12.628906 13.320312 12.464844 13.53125 12.332031 13.660156 C 12.179688 13.8125 12.027344 13.972656 12.199219 14.269531 C 12.371094 14.570312 12.96875 15.542969 13.851562 16.328125 C 14.988281 17.34375 15.945312 17.652344 16.242188 17.800781 C 16.539062 17.953125 16.710938 17.929688 16.886719 17.726562 C 17.0625 17.535156 17.628906 16.863281 17.828125 16.566406 C 18.023438 16.269531 18.222656 16.320312 18.492188 16.417969 C 18.765625 16.515625 20.226562 17.234375 20.523438 17.382812 C 20.824219 17.535156 21.019531 17.609375 21.09375 17.726562 C 21.171875 17.851562 21.171875 18.449219 20.921875 19.140625 C 20.675781 19.835938 19.464844 20.503906 18.917969 20.550781 C 18.371094 20.605469 17.859375 20.800781 15.351562 19.8125 C 12.328125 18.621094 10.421875 15.523438 10.269531 15.324219 C 10.121094 15.128906 9.058594 13.714844 9.058594 12.253906 C 9.058594 10.789062 9.828125 10.070312 10.097656 9.773438 C 10.371094 9.476562 10.691406 9.402344 10.890625 9.402344 Z M 10.890625 9.402344 " />
                    </g>
                </svg>
            </a>
        </li>
        <li>
            <a id="share-mail" href="#" target="_self" onclick="share(event, 'mail');return false;" rel="noopener noreferer">
<svg version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 172 172" width="22px" height="24px"><g fill="none" fill-rule="nonzero" stroke="none" stroke-width="1" stroke-linecap="butt" stroke-linejoin="miter" stroke-miterlimit="10" stroke-dasharray="" stroke-dashoffset="0" font-family="none" font-weight="none" font-size="none" text-anchor="none" style="mix-blend-mode: normal"><path d="M0,172v-172h172v172z" fill="none"></path><g fill="#ffffff"><path d="M28.66667,28.66667c-4.773,0 -8.98151,2.34406 -11.57585,5.94889c-1.74867,2.42233 -0.97612,5.85595 1.55371,7.43262l62.93229,39.2207c2.709,1.69133 6.13735,1.69133 8.84636,0l62.66634,-39.55664c2.63017,-1.66267 3.28592,-5.27623 1.34375,-7.69857c-2.61583,-3.25367 -6.60643,-5.34701 -11.09993,-5.34701zM154.19531,57.94922c-0.59931,0.00661 -1.20792,0.17178 -1.77767,0.5319l-61.99446,39.09472c-2.70901,1.68417 -6.13736,1.67734 -8.84636,-0.014l-62.00846,-38.63281c-2.279,-1.419 -5.23503,0.22396 -5.23503,2.91146v67.15951c0,7.91917 6.41417,14.33333 14.33333,14.33333h114.66667c7.91917,0 14.33333,-6.41417 14.33333,-14.33333v-67.62142c0,-2.021 -1.67342,-3.44918 -3.47135,-3.42936z"></path></g></g></svg>
            </a>
        </li>
    </ul>
</div>
</div>

<script>
    const share = (event, site) => {
		event.preventDefault();
        switch (site) {
            case 'facebook':
                window.open("https://www.facebook.com/sharer/sharer.php?u=" + event.target.baseURI, "popup", 'width=600px,height=600px,top=150pt,resizable=0');
                break;
            case 'twitter':
                window.open("https://twitter.com/intent/tweet/?url=" + event.target.baseURI, "popup", 'width=600px,height=600px,top=150pt,resizable=0');
                break;
            case 'whatsapp':
                window.open("http://api.whatsapp.com/send?phone=525552436900&text=Hola,%20me%20interesa%20este%20producto:%20" + event.target.baseURI, "popup", 'width=600px,height=600px,top=150pt,resizable=0');
                break;
            case 'mail':
                window.open("mailto:retail@incom.mx?cc=sistemasweb@incom.mx&subject=Mayor%20informacion&body=Hola,%20me%20interesa%20este%20producto:%20" + event.target.baseURI, "_blank");
                break;
        }
    }
</script>

<style>
	/*@import url("https://fonts.googleapis.com/css?family=Roboto:500");*/

	.wrapper-btn-share {
		width: 160px; 
		margin: 2em auto;
		text-align: center;
	}

	.btn-share {
		--btn-color: #275efe;
		position: relative;
		padding: 8px 32px;
		font-family: Roboto, sans-serif;
		font-weight: 500;
		font-size: 12px;
		line-height: 1;
		color: white;
		background: none;
		border: none;
		outline: none;
		overflow: hidden;
		cursor: pointer;
		filter: drop-shadow(0 2px 2px rgba(39, 94, 254, 0.32));
		transition: 0.3s cubic-bezier(0.215, 0.61, 0.355, 1);
	}

		.btn-share::before {
			position: absolute;
			content: "";
			top: 0;
			left: 0;
			z-index: -1;
			width: 100%;
			height: 70%;
			background: var(--btn-color);
			border-radius: 24px;
			transition: 0.9s cubic-bezier(0.215, 0.61, 0.355, 1);
		}

		.btn-share .btn-text, .btn-share .btn-icon {
			display: inline-flex;
			vertical-align: middle;
			transition: 0.3s cubic-bezier(0.215, 0.61, 0.355, 1);
		}

		.btn-share .btn-text {
			transition-delay: 0.05s;
		}

		.btn-share .btn-icon {
			margin-left: 8px;
			transition-delay: 0.1s;
		}

		.btn-share .social-icons {
			position: absolute;
			top: 40%;
			left: 0;
			right: 0;
			display: flex;
			margin: 0;
			padding: 0;
			list-style-type: none;
			transform: translateY(-50%);
			text-align: center;
		}

		.icon-share {
			padding-bottom: 0.5rem;
		}
		
		.btn-share .btn-text {
			padding-bottom: 0.5rem;
		}

			.btn-share .social-icons li {
				flex: 1;
			}

				.btn-share .social-icons li a {
					display: inline-flex;
					vertical-align: middle;
					transform: translateY(55px);
					transition: 0.3s cubic-bezier(0.215, 0.61, 0.355, 1);
				}

					.btn-share .social-icons li a:hover {
						opacity: 0.5;
					}

		.btn-share:hover::before {
			transform: scale(1.2);
		}

		.btn-share:hover .btn-text, .btn-share:hover .btn-icon {
			transform: translateY(-55px);
		}

		.btn-share:hover .social-icons li a {
			transform: translateY(0);
		}

		.btn-share:hover .social-icons li:nth-child(1) a {
			transition-delay: 0.15s;
		}

		.btn-share:hover .social-icons li:nth-child(2) a {
			transition-delay: 0.2s;
		}

		.btn-share:hover .social-icons li:nth-child(3) a {
			transition-delay: 0.25s;
		}

		#share-twitter > svg:nth-child(1) {
			width: 18px;
			height: 18px;
		}

		#share-whatsapp > svg:nth-child(1) {
			width: 18px;
			height: 18px;
		}

		#share-mail > svg:nth-child(1) {
			width: 18px;
			height: 18px;
		}

		#share-facebook > svg:nth-child(1) {
			width: 18px;
			height: 18px;
		}

		.icon-share {
			width: 12px;
		}
</style>
