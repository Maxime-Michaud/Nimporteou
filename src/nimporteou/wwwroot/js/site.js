// Write your Javascript code.
function loginModal(){
    modal.style.display = "block";
    $.ajax({
        type: "POST",
        url: 'http://localhost:4000/Account/login',
        data: {},
        success: function (html) {
            document.getElementById("modal-content").innerHTML = html;
        }
    });
}