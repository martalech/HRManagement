function fetchData() {
    if (document.getElementById('description').value != "") {
        console.log("dupa");
    }
    $.ajax({
        url: '/JobOffer/Create',
        type: 'post',
        data: @Model,
    success: function (data) {
        console.log(data);
    },
    error: function (data) {
        alert('Error! Please try again.');
        console.log(data);
    }
}).done(function () {
});
                }