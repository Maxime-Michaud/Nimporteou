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

//Change les messages d'erreurs et traduit les messages en français
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

function modifierAttribut(number) {
    var modal = document.getElementById('myModal');
    var actual, input, btnAppliquer;
    if (number == 0)
    {
        actual = document.getElementById('UserName');
        input = document.createElement('input');
        input.type = "text";
        btnAppliquer = document.createElement('Button')
        btnAppliquer.type = "Button";
        btnAppliquer.textContent = "Appliquer";
    }

    $.ajax({
        type: "GET",
        data: {actual, input, btnAppliquer},
        success: function (data) {
            modal.style.display = "block";
            document.getElementById("modal-content").innerHTML = data;
        }
    });
}

//Lisntener IPN paypal
/**
 * Created by chrissewell on 31/10/2016. https://github.com/paypal/ipn-code-samples/blob/master/javascript/awslambda.js
 */

const request = require('request');

exports.handler = (event, context, callback) => {
    console.log('Received event:', JSON.stringify(event, null, 2));

    //Return 200 to caller
    callback(null, {
        statusCode: '200'
    });

    //Read the IPN message sent from PayPal and prepend 'cmd=_notify-validate'
    var body = 'cmd=_notify-validate&' + event.body;

    console.log('Verifying');
    console.log(body);

    var options = {
        url: 'https://www.sandbox.paypal.com/cgi-bin/webscr',
        method: 'POST',
        headers: {
            'Connection': 'close'
        },
        body: body,
        strictSSL: true,
        rejectUnauthorized: false,
        requestCert: true,
        agent: false
    };

    //POST IPN data back to PayPal to validate
    request(options, function callback(error, response, body) {
        if (!error && response.statusCode === 200) {

            //Inspect IPN validation result and act accordingly
            if (body.substring(0, 8) === 'VERIFIED') {

                //The IPN is verified
                console.log('Verified IPN!');
            } else if (body.substring(0, 7) === 'INVALID') {

                //The IPN invalid
                console.log('Invalid IPN!');
            } else {
                //Unexpected response body
                console.log('Unexpected response body!');
                console.log(body);
            }
        } else {
            //Unexpected response
            console.log('Unexpected response!');
            console.log(response);
        }

    });

};
//Fin du listener