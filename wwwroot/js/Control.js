
//Değişkenler
var isEmail = false;
var isEmailR = false;
var isPass = false;
var isPassR = false;
var isTc = false;



//Tc Kontrolü
$("document").ready(function () {

    var checkTcNum = function (value) {
        value = value.toString();
        var isEleven = /^[0-9]{11}$/.test(value);
        var totalX = 0;
        for (var i = 0; i < 10; i++) {
            totalX += Number(value.substr(i, 1));
        }
        var isRuleX = totalX % 10 == value.substr(10, 1);
        var totalY1 = 0;
        var totalY2 = 0;
        for (var i = 0; i < 10; i += 2) {
            totalY1 += Number(value.substr(i, 1));
        }
        for (var i = 1; i < 10; i += 2) {
            totalY2 += Number(value.substr(i, 1));
        }
        var isRuleY = ((totalY1 * 7) - totalY2) % 10 == value.substr(9, 0);
        return isEleven && isRuleX && isRuleY;
    };


    $('#Tcno').on('keyup focus blur load input mouseover', function (event) {
        event.preventDefault();
        var isValid = checkTcNum($(this).val());
        isTc = isValid;
        updateSubmitState(isValid);
        console.log('isValid ', isValid);
        if (isValid) {
            document.getElementById("Tcno").style.borderColor = "green";

        }
        else {
            document.getElementById("Tcno").style.borderColor = "red";

        }
    });


    //Şifre ve Email Kontrolü

    var checkInputValues = function (input1, input2) {
        return input1.value === input2.value && input1.value !== "" && input2.value !== "";
    }






    // Giriş alanı seçicileri
    const emailSelector = "#Email";
    const emailRSelector = "#EmailR";
    const PassSelector = "#Pass"; 
    const PassRSelector = "#PassR"; 
    const SubmitSelector = "#submit"; 

    // Gerçekleştirilen olaylar
    $(emailSelector).on("keyup focus blur load input mouseover", function () {
        const isValid = checkInputValues(this, $(emailRSelector)[0]); // Email
        isEmail = isValid;
        updateSubmitState(isValid); 
    });

    $(emailRSelector).on("keyup focus blur load input mouseover", function () {
        const isValid = checkInputValues(this, $(emailSelector)[0]); // email Tekrar
        isEmailR = isValid;
        UpdateEventBgColor(isValid, isEmail, emailSelector, emailRSelector);
        updateSubmitState(isValid); 
    });


    $(PassSelector).on("keyup focus blur load input", function () {
        const isValid = checkInputValues(this, $(PassRSelector)[0]); //Şifre 
        isPass = isValid;
        updateSubmitState(isValid); 
    });

    $(PassRSelector).on("keyup focus blur load input", function () {
        const isValid = checkInputValues(this, $(PassSelector)[0]); //Şifre Tekara
        isPassR = isValid;
        UpdateEventBgColor(isValid, isPass, PassSelector, PassRSelector);
        updateSubmitState(isValid);
    });

    //Submit disable/enable
    function updateSubmitState(isValid) {
        if (isValid && isEmail && isEmailR && isPass &&  isPassR && isTc) {
            $(SubmitSelector).removeAttr("disabled");
        }
        else {
            $(SubmitSelector).attr("disabled", "disabled");
        }
    }

    //Event durum renkleri.
    function UpdateEventBgColor(isValid, isValid2, eventName, eventName2) {
        if (isValid && isValid2) {
            $(eventName).css("border-color", "green");
            $(eventName2).css("border-color", "green");

        }
        else {
            $(eventName).css("border-color", "Red");
            $(eventName2).css("border-color", "Red");
        }
    }



}); //document.ready


