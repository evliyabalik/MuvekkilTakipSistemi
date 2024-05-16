$(document).ready(function () {
    GetClient();
});

/*Read Data*/
function GetClient() {
    $.ajax({
        url: '/user/GetClient',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',

        success: function (response) {

            if (response == null || response == undefined || response.length == 0) {
                var object = '';
                object += '<tr>';
                object += '<td colspan="11">' + 'Herhangi bir veri yok!' + '</td>';
                object += '</tr>';
                $('#tblBody').html(object);

            }
            else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.ad_Unvan + '</td>';
                    object += '<td>' + item.grupAdi + '</td>';
                    object += '<td>' + item.tcno + '</td>';
                    object += '<td>' + item.gsm + '</td>';
                    object += '<td>' + item.tel + '</td>';
                    object += '<td>' + item.vergi_Dairesi + '</td>';
                    object += '<td>' + item.no + '</td>';
                    object += '<td>' + item.avukat + '</td>';
                    object += '<td>' + item.ozel_Alan + '</td>';
                    object += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.Id + ')">Edit</a>';
                    object += '<a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + item.Id + ')">Delete</a></td>';
                    object += '</tr>';

                    console.log(object)
                });/*each*/
                $('#tblBody').html(object);


            }/*if*/
        }, /* Function*/
        error: function () {
            alert("Veri Okunamýyor.")
        }//Error

    });/*Ajax*/
}


/*Insert Data*/
$('#btnNew').click(function () {
    $('#ClientModal').modal('show');
    $('#modalTitle').text('Muvekkil Ekle');
});

function Insert() {
    var formData = new Object();
    formData.ad_Unvan = $('#Ad_Unvan').val();
    formData.grupAdi = $('#GrupAdi').val();
    formData.tcno = $('#Tcno').val();
    formData.gsm = $('#GSM').val();
    formData.tel = $('#Tel').val();
    formData.vergi_Dairesi = $('#Vergi_Dairesi').val();
    formData.no = $('#No').val();
    formData.ozel_Alan = $('#Ozel_Alan').val();

    $.ajax({
        url: '/User/InserClient',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert("Kullanýcý eklerken hata oluþtu");
            } else {
                GetClient();
                alert(response);
            }//if

        },//success

        error: function () {
            alert("Kullanýcý eklerken hata oluþtu");
        }//Error

    });//Ajax

}//Method