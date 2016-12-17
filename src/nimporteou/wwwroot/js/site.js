// Write your Javascript code.
// Get the modal
var modal = document.getElementById('myModal');

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on <span> (x), close the modal
span.onclick = function() {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

      
function modifierService2() {

    $.ajax({
        type: "GET",
        url: '/Account/login',
        data: {},
        success: function (html) {
            modal.style.display = "block";
            document.getElementById("modal-content").innerHTML = html;
        }
    });
}
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