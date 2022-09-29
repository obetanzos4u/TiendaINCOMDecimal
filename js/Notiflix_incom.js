const NotiflixAlert = (type, message) => {
    Notiflix.Notify.init({
        width: '360px',
        position: 'right-top',
        distance: '5px',
        opacity: 1,
        borderRadius: '5px',
        rtl: false,
        timeout: 3000,
        backOverlay: false,
        backOverlayColor: 'rgba(0,0,0,0.8)', //Verificar porque al parecer no funciona
        clickToClose: true,
        pauseOnHover: true,
        fontFamily: "Montserrat",
        fontSize: "18px",
        cssAnimation: true,
        cssAnimationDuration: 700,
        cssAnimationStyle: "zoom",
        closeButton: true,
        useIcon: true,
        useFontAwesome: false,
        showOnlyTheLastOne: false,
        success: {
            background: '#22c55e',
            textColor: '#fff',
            notiflixIconColor: '#fefefe',
            backOverlayColor: 'rgba(50,198,130,0.2)',
            fontAwesomeClassName: 'fas fa-check-circle',
            fontAwesomeIconColor: 'rgba(0,0,0,0.2)'
        },
        failure: {
            background: '#ef4444',
            textColor: '#fff',
            notiflixIconColor: '#fefefe',
            backOverlayColor: 'rgba(50,198,130,0.2)',
            fontAwesomeClassName: 'fas fa-check-circle',
            fontAwesomeIconColor: 'rgba(0,0,0,0.2)'
        },
        warning: {
            background: '#f59e0b',
            textColor: '#fff',
            notiflixIconColor: '#fefefe',
            backOverlayColor: 'rgba(50,198,130,0.2)',
            fontAwesomeClassName: 'fas fa-check-circle',
            fontAwesomeIconColor: 'rgba(0,0,0,0.2)'
        },
        info: {
            background: '#0ea5e9',
            textColor: '#fff',
            notiflixIconColor: '#fefefe',
            backOverlayColor: 'rgba(50,198,130,0.2)',
            fontAwesomeClassName: 'fas fa-check-circle',
            fontAwesomeIconColor: 'rgba(0,0,0,0.2)'
        }
    });
    switch (type) {
        case "success":
            Notiflix.Notify.merge({
                closeButton: false,
                width: '280px',
                showOnlyTheLastOne: true
            });
            Notiflix.Notify.success(message);
            break;
        case "failure":
            Notiflix.Notify.failure(message);
            break;
        case "warning":
            Notiflix.Notify.warning(message);
            break;
        case "info":
            Notifix.Notify.info(message);
            break;
    }
}