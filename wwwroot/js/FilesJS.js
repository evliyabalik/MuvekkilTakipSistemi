

$(document).ready(function () {
    GetFiles();
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


/*Insert Data*/
$('#btnNew').click(function () {
    $('#ClientModal').modal('show');
    $('#modalTitle').text('Muvekkil Ekle');
});

function Insert() {
    var result = IsValidate();
    if (result == false) {
        return false;
    }

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
                HideModal();
                GetClient();
                alert(response);
            }//if

        },//success

        error: function () {
            alert("Kullanýcý eklerken hata oluþtu");
        }//Error

    });//Ajax

}//Method

function HideModal() {
    ClearData();
    $('#ClientModal').modal('hide');
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
    $('#Ad_Unvan').val('');
    $('#Tcno').val('');
    $('#GSM').val('');
    $('#Tel').val('');
    $('#Vergi_Dairesi').val('');
    $('#No').val('');
    $('#Ozel_Alan').val('');
}

function IsValidate() {
    var validate = true;
    if ($('#Ad_Unvan').val().trim() == '') {
        validate = false;
        $('#Ad_Unvan').css("border-color", "Red");
    }
    else {
        $('#Ad_Unvan').css("border-color", "Green");
    }
    if ($('#Tcno').val().trim() == '') {
        validate = false;
        $('#Tcno').css("border-color", "Red");
    }
    else {
        $('#Tcno').css("border-color", "Green"); 
    }
    if ($('#GSM').val().trim() == '') {
        validate = false;
        $('#GSM').css("border-color", "Red");
    }
    else {
        $('#GSM').css("border-color", "Green"); 
    }
    return validate;
}

$('#Ad_Unvan').change(function () {
    IsValidate();
});

$('#Tcno').change(function () {
    IsValidate();
});

$('#GSM').change(function () {
    IsValidate();
});

//Edit
function Edit(id) {
    $.ajax({
        url:'User/EditClient?Id='+id,
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
                $('#ClientModal').modal('show');
                $('#modalTitle').text('Güncelle')
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');
                $('#id').val(response.id);
                $('#Ad_Unvan').val(response.ad_Unvan);
                $('#GrupAdi').val(response.grupAdi);
                $('#Tcno').val(response.tcno);
                $('#GSM').val(response.gsm);
                $('#Tel').val(response.tel);
                $('#Vergi_Dairesi').val(response.vergi_Dairesi);
                $('#No').val(response.no);
                $('#Ozel_Alan').val(response.ozel_Alan);

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
    formData.ad_Unvan = $('#Ad_Unvan').val();
    formData.grupAdi = $('#GrupAdi').val();
    formData.tcno = $('#Tcno').val();
    formData.gsm = $('#GSM').val();
    formData.tel = $('#Tel').val();
    formData.vergi_Dairesi = $('#Vergi_Dairesi').val();
    formData.no = $('#No').val();
    formData.ozel_Alan = $('#Ozel_Alan').val();

    $.ajax({
        url: '/User/UpdateClient',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert("Kullanýcý güncellenirken hata oluþtu");
            } else {
                HideModal();
                GetClient();
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
        url: 'User/DeleteClient?Id=' + id,
        type: 'post',

        success: function (response) {
            if (response == null || response == undefined) {
                alert("Okuma baþarýsýz.")
            }
            else if (response.length == 0) {
                alert("Seçtiðiniz id'ye göre bir veri bulunamadý.")
            }
            else {
                GetClient();
                alert(response)
            }//if
        },//success
        error: function () {
            alert("Okuma baþarýsýz.")
        }//error
    });//ajax
}//Method