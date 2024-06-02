

$(document).ready(function () {
    GetFiles();
    updateSelect();
    IsValidate();
});

/*Read Data*/
function GetFiles() {
    $.ajax({
        url: '/user/GetFilesOnTable',
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
                    object += '<td>' + item.dosyaNo + '</td>';
                    object += '<td>' + item.konusu + '</td>';
                    object += '<td>' + item.avukat + '</td>';
                    object += '<td>' + item.mahkeme + '</td>';
                    object += '<td>' + item.muvekkil + '</td>';
                    object += '<td>' + item.muvekkil_Grubu + '</td>';
                    object += '<td>' + item.adres + '</td>';
                    object += '<td>' + item.adi_telefon + '</td>';
                    object += '<td>' + item.adi_telefon2 + '</td>';
                    object += '<td>' + item.ozel_Alan + '</td>';
                    object += '<td>' + item.ozel_Alan2 + '</td>';
                    object += '<td>' + item.referans + '</td>';
                    object += '<td>' + item.ucret_Sozlesmesi + '</td>';
                    object += '<td>' + item.sozlesme_No + '</td>';
                    object += '<td>' + item.serbest_Meslek_Makbuzu + '</td>';
                    object += '<td>' + item.dosya_Durumu + '</td>';
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


/*Insert Client Data Data*/
$('#newClient').click(function () {
    $('#ClientModal').modal('show');
    $('#modalTitle').text('Muvekkil Ekle');
});

function InsertClient() {
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
                updateSelect();

                HideModalClient();
            }//if

        },//success

        error: function () {
            alert("Kullanýcý eklerken hata oluþtu");
        }//Error

    });//Ajax

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
    $('#FileModal').modal('show');
    $('#modalTitle').text('Dosya Ekle');
});

function Insert() {
    let result = IsValidate();
    if (result == false) {
        return false;
    }

    var formData = new Object();
    formData.dosyaNo = $('#DosyaNo').val();
    formData.konusu = $('#Konusu').val();
    formData.mahkeme = $('#Mahkeme').val();
    formData.muvekkil = $('#Muvekkil').val();
    formData.adres = $('#Adres').val();
    formData.adi_telefon = $('#Adi_Telefon').val();
    formData.adi_telefon2 = $('#Adi_Telefon2').val();
    formData.ozel_Alan = $('#Ozel_Alan').val();
    formData.ozel_Alan2 = $('#Ozel_Alan2').val();
    formData.referans = $('#Referans').val();
    formData.ucret_Sozlesmesi = $('#Ucret_Sozlesmesi').val();
    formData.sozlesme_No = $('#Sozlesme_No').val();
    formData.serbest_Meslek_Makbuzu = $('#Serbest_Meslek_Makbuzu').val();
    formData.dosya_Durumu = $('#Dosya_Durumu').val();




    $.ajax({
        url: '/User/InserFile',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert("Dosya eklerken hata oluþtu");
            } else {
                HideModal();
                GetFiles();
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
    $('#FileModal').modal('hide');
    ReloadPage();
}

//ClientHide
function HideModalClient() {
    ClearData();
    $('#ClientModal').modal('hide');
}

//Reload
function ReloadPage() {
    // URL'i temizleyerek sayfayý yeniden yükle
    const cleanUrl = window.location.protocol + "//" + window.location.host + window.location.pathname;
    window.history.replaceState({}, document.title, cleanUrl);
    window.location.reload();
}

function ClearData() {
    $('#DosyaNo').val('');
    $('#Konusu').val('');
    $('#Mahkeme').val('');
    $('#Muvekkil').val('');
    $('#Adres').val('');
    $('#Adi_Telefon').val('');
    $('#Adi_Telefon2').val('');
    $('#Ozel_Alan').val('');
    $('#Ozel_Alan2').val('');
    $('#Referans').val('');
    $('#Ucret_Sozlesmesi').val('');
    $('#Sozlesme_No').val('');
    $('#Serbest_Meslek_Makbuzu').val('');
    $('#Dosya_Durumu').val('');

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
        url: '/User/EditFile?Id=' + id,
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
                $('#FileModal').modal('show');
               $('#modalTitle').text('G\u00FCncelle');
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');
                $('#id').val(response.id);
                $('#DosyaNo').val(response.dosyaNo);
                $('#Konusu').val(response.konusu);
                $('#Mahkeme').val(response.mahkeme);
                $('#Muvekkil').val(response.muvekkil);
                $('#Adres').val(response.adres);
                $('#Adi_Telefon').val(response.adi_telefon);
                $('#Adi_Telefon2').val(response.adi_telefon2);
                $('#Ozel_Alan').val(response.ozel_Alan);
                $('#Ozel_Alan2').val(response.ozel_Alan2);
                $('#Referans').val(response.referans);
                $('#Ucret_Sozlesmesi').val(response.ucret_Sozlesmesi);
                $('#Sozlesme_No').val(response.sozlesme_No);
                $('#Serbest_Meslek_Makbuzu').val(response.serbest_Meslek_Makbuzu);
                $('#Dosya_Durumu').val(response.dosya_Durumu);



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
    formData.dosyaNo = $('#DosyaNo').val();
    formData.konusu = $('#Konusu').val();
    formData.mahkeme = $('#Mahkeme').val();
    formData.muvekkil = $('#Muvekkil').val();
    formData.adres = $('#Adres').val();
    formData.adi_telefon = $('#Adi_Telefon').val();
    formData.adi_telefon2 = $('#Adi_Telefon2').val();
    formData.ozel_Alan = $('#Ozel_Alan').val();
    formData.ozel_Alan2 = $('#Ozel_Alan2').val();
    formData.referans = $('#Referans').val();

    formData.ucret_Sozlesmesi = $('#Ucret_Sozlesmesi').val();

    formData.sozlesme_No = $('#Sozlesme_No').val();

    formData.serbest_Meslek_Makbuzu = $('#Serbest_Meslek_Makbuzu').val();

    formData.dosya_Durumu = $('#Dosya_Durumu').val();



    $.ajax({
        url: '/User/UpdateFile',
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
        url: '/User/DeleteFiles?Id=' + id,
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