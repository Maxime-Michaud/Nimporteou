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
    if (event.target === modal) {
        modal.style.display = "none";
    }
}

      
function LoginModal() {
    ShowModal('/Account/Login/');
}

function RegisterModal() {
    ShowModal('/Account/Register/');
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

function heure() {
    $(function () {
        $('#basicExample').timepicker();
    });
}