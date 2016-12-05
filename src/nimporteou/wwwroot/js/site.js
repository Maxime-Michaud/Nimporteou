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

function heure() {
    $(function () {
        $('#basicExample').timepicker();
    });
}
