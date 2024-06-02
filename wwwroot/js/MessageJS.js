$(document).ready(function () {
    GetMessage();
});

/*Read Data*/
function GetMessage() {
    $.ajax({
        url: '/Admin/GetMessage',
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
                    object += '<td>' + item.id + '</td>';
                    object += '<td>' + item.name_surname + '</td>';
                    object += '<td>' + item.tel + '</td>';
                    object += '<td>' + item.email + '</td>';
                    object += '<td>' + item.department + '</td>';
                    object += '<td>' + item.subject + '</td>';
                    object += '<td>' + item.date + '</td>';
                    object += '<td>' + item.ip_address + '</td>';
                    object += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Read(' + item.id + ')">Oku</a>';
                    object += '</tr>';

                });/*each*/
                $('#tblBody').html(object);


            }/*if*/
        }, /* Function*/
        error: function () {
            alert("Veri Okunamýyor.")
        }//Error

    });/*Ajax*/
}


function HideModal() {
    ClearData();
    $('#MessageModel').modal('hide');
    ReloadPage();
}

function ClearData() {
    $('#Adsoyad').val('');
    $('#Kullanici_adi').val('');
    $('#Pass').val('');
    $('#Email').val('');
    $('#StatusId').val('');
}



//Edit
function Read(id) {
    $.ajax({
        url:'/Admin/ReadMessage?Id='+id,
        type:'get',
        contentType:'application/json;charset=utf-8',
        dataType:'json',
        success: function (response) {
            if (response == null || response == undefined) {
                alert("Okuma baþarýsýz.")
            }
            else if (response.length == 0) {
                alert("Seçtiðiniz id'ye göre bir veri bulunamadý.")
            }

            else {
                $('#MessageModel').modal('show');
                $('#modalTitle').text('Mesaj Oku');
                $('#id').val(response.id);
                $('#Name_surname').val(response.name_surname);
                $('#Email').val(response.email);
                $('#Subject').val(response.subject);
                $('#Message').val(response.message);
            }//if
        },//success
        error: function () {
            alert("Okuma baþarýsýz.")
        }//error
    });//ajax
}//Method


//Reload
function ReloadPage() {
    // URL'i temizleyerek sayfayý yeniden yükle
    const cleanUrl = window.location.protocol + "//" + window.location.host + window.location.pathname;
    window.history.replaceState({}, document.title, cleanUrl);
    window.location.reload();
}
