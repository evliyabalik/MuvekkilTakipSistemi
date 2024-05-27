$(document).ready(function () {
    GetAdmin();
});

/*Read Data*/
function GetAdmin() {
    $.ajax({
        url: '/Admin/GetAdmin',
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
                    object += '<td>' + item.adsoyad + '</td>';
                    object += '<td>' + item.kullanici_adi + '</td>';
                    object += '<td>' + item.email + '</td>';
                    object += '<td>' + item.statusId + '</td>';
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
    $('#AdminModal').modal('show');
    $('#modalTitle').text('Admin Ekle');
});

function Insert() {
    var result = IsValidate();
    if (result == false) {
        return false;
    }

    var formData = new Object();
    formData.adsoyad = $('#Adsoyad').val();
    formData.kullanici_adi = $('#Kullanici_adi').val();
    formData.pass = $('#Pass').val();
    formData.email = $('#Email').val();
    formData.statusId = $('#StatusId').val();

    $.ajax({
        url: '/Admin/InsertAdmin',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert("Kullanýcý eklerken hata oluþtu");
            } else {
                HideModal();
                GetAdmin();
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
    $('#AdminModal').modal('hide');
    ReloadPage();
}

function ClearData() {
    $('#Adsoyad').val('');
    $('#Kullanici_adi').val('');
    $('#Pass').val('');
    $('#Email').val('');
    $('#StatusId').val('');
}

function IsValidate() {
    var validate = true;
    if ($('#Adsoyad').val().trim() == '') {
        validate = false;
        $('#Adsoyad').css("border-color", "Red");
    }
    else {
        $('#Adsoyad').css("border-color", "Green");
    }
    if ($('#Kullanici_adi').val().trim() == '') {
        validate = false;
        $('#Kullanici_adi').css("border-color", "Red");
    }
    else {
        $('#Kullanici_adi').css("border-color", "Green"); 
    }
    if ($('#Pass').val().trim() == '') {
        validate = false;
        $('#Pass').css("border-color", "Red");
    }
    else {
        $('#Pass').css("border-color", "Green"); 
    }
    if ($('#Email').val().trim() == '') {
        validate = false;
        $('#Email').css("border-color", "Red");
    }
    else {
        $('#Email').css("border-color", "Green"); 
    }

    if ($('#StatusId').val().trim() == '') {
        validate = false;
        $('#StatusId').css("border-color", "Red");
    }
    else {
        $('#StatusId').css("border-color", "Green"); 
    }
    return validate;
}

$('#Adsoyad').change(function () {
    IsValidate();
});

$('#Kullanici_adi').change(function () {
    IsValidate();
});

$('#Pass').change(function () {
    IsValidate();
});
$('#Email').change(function () {
    IsValidate();
});
$('#StatusId').change(function () {
    IsValidate();
});

//Edit
function Edit(id) {
    $.ajax({
        url:'/Admin/EditAdmin?Id='+id,
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
                $('#AdminModal').modal('show');
                $('#modalTitle').text('Güncelle')
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');
                $('#id').val(response.id);
                $('#Adsoyad').val(response.adsoyad);
                $('#Kullanici_adi').val(response.kullanici_adi);
                $('#Pass').val(response.pass);
                $('#Email').val(response.email);
                $('#StatusId').val(response.statusId);
            }//if
        },//success
        error: function () {
            alert("Okuma baþarýsýz.")
        }//error
    });//ajax
}//Method

//Update Data
function Update() {

    var formData = new Object();
    formData.id = $('#id').val();
    formData.adsoyad = $('#Adsoyad').val();
    formData.kullanici_adi = $('#Kullanici_adi').val();
    formData.pass = $('#Pass').val();
    formData.email = $('#Email').val();
    formData.statusId = $('#StatusId').val();
   

    $.ajax({
        url: '/Admin/UpdateAdmin',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert("Kullanýcý güncellenirken hata oluþtu");
            } else {
                HideModal();
                GetAdmin();
                alert(response);
            }//if

        },//success

        error: function () {
            alert("Kullanýcý güncellenirken hata oluþtu");
        }//Error

    });//Ajax


}//method

//Reload
function ReloadPage() {
    // URL'i temizleyerek sayfayý yeniden yükle
    const cleanUrl = window.location.protocol + "//" + window.location.host + window.location.pathname;
    window.history.replaceState({}, document.title, cleanUrl);
    window.location.reload();
}

//Delete
function Delete(id) {
    $.ajax({
        url: '/Admin/DeleteAdmin?Id=' + id,
        type: 'post',

        success: function (response) {
            if (response == null || response == undefined) {
                alert("Okuma baþarýsýz.")
            }
            else if (response.length == 0) {
                alert("Seçtiðiniz id'ye göre bir veri bulunamadý.")
            }
            else {
                GetAdmin();
                alert(response)
            }//if
        },//success
        error: function () {
            alert("Okuma baþarýsýz.")
        }//error
    });//ajax
}//Method