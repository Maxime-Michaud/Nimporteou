// Write your Javascript code.
//Preview & Update an image before it is uploaded
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#blah').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

$("#imgInp").change(function () {
    readURL(this);
});

//Supposer changer les messages d'erreur traduit en français, mais fonctionne pas
jQuery.extend(jQuery.validator.messages, {
    required: "Ce champs est obligatoire",
    remote: "S'il vous plaît corriger ce champ.",
    email: "Veuillez-saisir un courriel valide.",
    url: "Veuillez-saisir un url valide.",
    date: "Veuillez-saisir une date valide.",
    dateISO: "Veuillez-saisir une date valide (ISO).",
    number: "Veuillez-saisir un nombre valide.",
    digits: "Veuillez-saisir uniquement des chiffres.",
    creditcard: "Veuillez-saisir un numéro de carte de crédit valide.",
    equalTo: "Veuillez-saisir la même valeur.",
    accept: "Veuillez-entrer une valeur avec une extension valide.",
    maxlength: jQuery.validator.format("Veuillez-entrer moins de {0} caractères."),
    minlength: jQuery.validator.format("Veuillez-entrer au moins {0} caractères."),
    rangelength: jQuery.validator.format("Veuillez-entrer un message d'une longeure se situant entre {0} et {1} caractères de long."),
    range: jQuery.validator.format("Veuillez-entrer une valeur entre {0} et {1}."),
    max: jQuery.validator.format("Veuillez-entrer une valeur inférieur ou égale à {0}."),
    min: jQuery.validator.format("Veuillez-entrer une valeur supérieur ou égale à {0}.")
});

var hideShow = false; //Si on doit réduire ou agrandir la description dans FAQ
function showContent(number) {
    if (!hideShow)
    {
        var element = document.getElementsByClassName("collapsedFAQTitle")[number];
        document.getElementsByClassName("collapseFAQHiddenShow")[number].style.display = "block";
        element.style.backgroundColor = "lightgray";
        element.innerHTML = element.innerHTML.replace('⇓', '↑');
    }
    else
    {
        var element = document.getElementsByClassName("collapsedFAQTitle")[number];
        document.getElementsByClassName("collapseFAQHiddenShow")[number].style.display = "none";
        element.style.backgroundColor = "transparent";
        element.innerHTML = element.innerHTML.replace('↑', '⇓');
    }
    hideShow = !hideShow;
}

// Get the modal
var modal = document.getElementById('myModal');

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    var modal = document.getElementById('myModal');

    modal.style.display = "none";
};

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    var modal = document.getElementById('myModal');

    if (event.target === modal) {
        modal.style.display = "none";
    }
};


function LoginModal() {
    ShowModal('/Account/Login');
}

function RegisterModal() {
    ShowModal('/Account/Register');
}

function ShowModal(url) {
    var modal = document.getElementById('myModal');

    $.ajax({
        type: "GET",
        url: url,
        data: {},
        success: function (data) {
            modal.style.display = "block";
            document.getElementById("modal-content").innerHTML = data;
        }
    });
}


//Charge un évènement de facebook
function loadFBEvent() {
    if (FB.getAuthResponse() == null)
    {
        FB.login(function (response) {
            var token = response.authResponse.accessToken;

            loadFBEventWithToken(token);
        });
    }
    else {
        var token = FB.getAuthResponse().accessToken;

        loadFBEventWithToken(token);
    }
    

}

function loadFBEventWithToken(token){
    alert(token);

    var url = document.getElementById("fburl").value;

    var eventid = /(?:events\/)?(\d+)/i.exec(url)[1];
    alert("ok1");
    $.ajax({
        type: "GET",
        data: {},
        url: '/Evenement/LoadFacebook?eventid=' + eventid + '&authToken=' + token,
        success: function (data) {
            alert("ok");
            alert(data);
            //je sais pas comment juste remplacer le DOM de la page :(
            window.location = '/Evenement/LoadFacebook?eventid=' + eventid + '&authToken=' + token;
        }
    });
}