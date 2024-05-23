

$(document).ready(function () {
    GetFiles();
    updateSelect();
    IsValidate();
});

/*Read Data*/
function GetFiles() {
    $.ajax({
        url: '/user/GetActivities',
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
                    object += '<td>' + item.tarih + '</td>';
                    object += '<td>' + item.evrak_No + '</td>';
                    object += '<td>' + item.islem_Turu + '</td>';
                    object += '<td>' + item.yapilan_Islem + '</td>';
                    object += '<td>' + item.muvekkil + '</td>';
                    object += '<td>' + item.dosya + '</td>';
                    object += '<td>' + item.mahkeme + '</td>';
                    object += '<td>' + item.konusu + '</td>';
                    object += '<td>' + item.avukat + '</td>';
                    object += '<td>' + item.odeme_Sekli + '</td>';
                    object += '<td>' + item.islem_Tutari + '</td>';
                    object += '<td>' + item.aciklama + '</td>';
                    object += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.id + ')">Edit</a>';
                    object += '<a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + item.id + ')">Delete</a></td>';
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



//Dropdown select update
function updateSelect() {

    $.ajax({
        url: '/User/GetClient',
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            // Clear the existing options
            $('#Muvekkil').empty();

            // Loop through the updated data and append new options
            $.each(data, function (index, value) {
                $('#Muvekkil').append('<option value="' + value.ad_Unvan + '">' + value.ad_Unvan + '</option>');

            });

        },
        error: function () {

            console.log('Error retrieving updated data');
        }

    });
}



/*Insert Files Data*/
$('#btnNew').click(function () {
    $('#ActivitiesModal').modal('show');
    $('#modalTitle').text('Ýþlem Ekle');
});

function Insert() {
    let result = IsValidate();
    if (result == false) {
        return false;
    }

    var formData = new Object();
    formData.evrak_No = $('#Evrak_No').val();
    formData.islem_Turu = $('#Islem_Turu').val();
    formData.yapilan_Islem = $('#Yapilan_Islem').val();
    formData.muvekkil = $('#Muvekkil').val();
    formData.Dosya = $('#Dosya').val();
    formData.odeme_Sekli = $('#Odeme_Sekli').val();
    formData.islem_Tutari = $('#Islem_Tutari').val();
    formData.aciklama = $('#Aciklama').val();

    $.ajax({
        url: '/User/InsertActivities',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert("Dosya eklerken hata oluþtu");
            } else {
                HideModal();
                GetActivities();
                alert(response);
            }//if

        },//success

        error: function () {
            alert("Dosya eklerken hata oluþtu");
        }//Error

    });//Ajax

}//Method

//File Hide
function HideModal() {
    ClearData();
    $('#ActiviesModal').modal('hide');
    ReloadPage();
}

//Reload
function ReloadPage() {
    // URL'i temizleyerek sayfayý yeniden yükle
    const cleanUrl = window.location.protocol + "//" + window.location.host + window.location.pathname;
    window.history.replaceState({}, document.title, cleanUrl);
    window.location.reload();
}

function ClearData() {
    $('#Evrak_ho').val('');
    $('#Islem_Turu').val('');
    $('#Yapilan_Islem').val('');
    $('#Muvekkil').val('');
    $('#Dosya').val('');
    $('#Odeme_Sekli').val('');
    $('#Islem_Tutari').val('');
    $('#Aciklama').val('');
   

}


function IsValidate() {

    var validate = true;

    $('.validate-input').each(function () {

        if ($(this).val().trim() == '') {

            validate = false;
            $(this).css("border-color", "Red");
        }
        else {
            $(this).css("border-color", "Green");
        }
    });
    return validate;

}


$('.validate-input').change(function () {

    var validate = IsValidate();

    if (!validate) {

        $(this).focus();

    }

}).one('change', function () {

    IsValidate();


});




//Edit
function Edit(id) {
    $.ajax({
        url: '/User/EditActivities?Id=' + id,
        type: 'get',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (response) {
            if (response == null || response == undefined) {
                alert("Okuma baþarýsýz.")
            }
            else if (response.length == 0) {
                alert("Seçtiðiniz id'ye göre bir veri bulunamadý.")
            }

            else {
                $('#ActivitiesModal').modal('show');
                $('#modalTitle').text('Güncelle')
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');
                $('#id').val(response.id);
                $('#Evrak_No').val(response.evrak_No);
                $('#Islem_Turu').val(response.islem_Turu);
                $('#Yapilan_Islem').val(response.yapilan_Islem);
                $('#Muvekkil').val(response.muvekkil);
                $('#Dosya').val(response.dosya);
                $('#Odeme_Sekli').val(response.odeme_Sekli);
                $('#Islem_Tutari').val(response.islem_Tutari);
                $('#Aciklama').val(response.aciklama);
               


            }//if
        },//success
        error: function () {
            alert("Okuma baþarýsýz.")
        }//error
    });//ajax
}//Method

//Update Data
function Update() {
    var result = IsValidate();
    if (result == false) {
        return false;
    }//if

    var formData = new Object();
    formData.id = $('#id').val();
    formData.evrak_No = $('#Evrak_No').val();
    formData.islem_Turu = $('#Islem_Turu').val();
    formData.yapilan_Islem = $('#Yapilan_Islem').val();
    formData.muvekkil = $('#Muvekkil').val();
    formData.Dosya = $('#Dosya').val();
    formData.odeme_Sekli = $('#Odeme_Sekli').val();
    formData.islem_Tutari = $('#Islem_Tutari').val();
    formData.aciklama = $('#Aciklama').val();
   



    $.ajax({
        url: '/User/UpdateActivities',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert("Kullanýcý güncellenirken hata oluþtu");
            } else {
                HideModal();
                GetFiles();
                alert(response);
            }//if

        },//success

        error: function () {
            alert("Kullanýcý güncellenirken hata oluþtu");
        }//Error

    });//Ajax


}//method

//Delete
function Delete(id) {
    $.ajax({
        url: '/User/DeleteActivities?Id=' + id,
        type: 'post',

        success: function (response) {
            console.log(response)
            if (response == null || response == undefined) {
                alert("Okuma baþarýsýz.")
            }
            else if (response.length == 0) {
                alert("Seçtiðiniz id'ye göre bir veri bulunamadý.")
            }
            else {
                GetFiles();
                alert(response)
            }//if
        },//success
        error: function () {
            alert("Okuma baþarýsýz.")
        }//error
    });//ajax
}//Method