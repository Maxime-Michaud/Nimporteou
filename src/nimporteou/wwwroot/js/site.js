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

jQuery.extend(jQuery.validator.messages, {
    required: "Ce champs est obligatoire",
    remote: "Please fix this field.",
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
        var html = document.getElementsByClassName("collapsedFAQTitle")[number];
        document.getElementsByClassName("collapseFAQHiddenShow")[number].style.display = "block";
        html.style.backgroundColor = "lightgray";
        html.innerHTML = html.innerHTML.replace('⇓', '↑');
    }
    else
    {
        var html = document.getElementsByClassName("collapsedFAQTitle")[number];
        document.getElementsByClassName("collapseFAQHiddenShow")[number].style.display = "none";
        html.style.backgroundColor = "transparent";
        html.innerHTML = html.innerHTML.replace('↑', '⇓');
    }
    hideShow = !hideShow
}

// Get the modal
var modal = document.getElementById('myModal');

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target === modal) {
        modal.style.display = "none";
    }
}


function LoginModal() {
    ShowModal('/Account/Login');
}

function RegisterModal() {
    ShowModal('/Account/Register');
}

function ShowModal(url) {
    $.ajax({
        type: "GET",
        url: url,
        data: {},
        success: function (html) {
            modal.style.display = "block";
            document.getElementById("modal-content").innerHTML = html;
        }
    });
}