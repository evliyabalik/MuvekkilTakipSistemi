//Değişkenler
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
        console.log('isValid ', isValid);
        if (isValid) {
            document.getElementById("Tcno").style.borderColor = "green";
        }
        else {
            document.getElementById("Tcno").style.borderColor = "red";
        }
    });
}); //document.ready
//Şifre ve Email Kontrolü
const form = document.getElementById('myForm');
const email = document.getElementById('Email');
const emailR = document.getElementById('EmailR');
const pass = document.getElementById('Pass');
const passR = document.getElementById('PassR');
form.addEventListener('submit', (e) => {
    if (email.value === emailR.value && pass.value === passR.value) {
        email.classList.add('valid');
        emailR.classList.add('valid');
        pass.classList.add('valid');
        passR.classList.add('valid');
    } else {
        email.classList.remove('valid');
        emailR.classList.remove('valid');
        pass.classList.remove('valid');
        passR.classList.remove('valid');
        email.classList.add('invalid');
        emailR.classList.add('invalid');
        pass.classList.add('invalid');
        passR.classList.add('invalid');
    }
    if (isTc && email.value === emailR.value && pass.value === passR.value) {
        console.log('Form gönderilebilir.');
        // Form gönderimini aktif etmek için
        e.target.submit();
    } else {
        console.log('Form gönderilemiyor.');
        e.preventDefault();
    }
});
email.addEventListener('input', () => {
    if (email.value === emailR.value) {
        email.classList.add('valid');
        emailR.classList.add('valid');
        email.classList.remove('invalid');
        emailR.classList.remove('invalid');
    } else {
        email.classList.remove('valid');
        emailR.classList.remove('valid');
        email.classList.add('invalid');
        emailR.classList.add('invalid');
    }
});
emailR.addEventListener('input', () => {
    if (email.value === emailR.value) {
        email.classList.add('valid');
        emailR.classList.add('valid');
        email.classList.remove('invalid');
        emailR.classList.remove('invalid');
    } else {
        email.classList.remove('valid');
        emailR.classList.remove('valid');
        email.classList.add('invalid');
        emailR.classList.add('invalid');
    }
});
pass.addEventListener('input', () => {
    if (pass.value === passR.value) {
        pass.classList.add('valid');
        passR.classList.add('valid');
        pass.classList.remove('invalid');
        passR.classList.remove('invalid');
    } else {
        pass.classList.remove('valid');
        passR.classList.remove('valid');
        pass.classList.add('invalid');
        passR.classList.add('invalid');
    }
});
passR.addEventListener('input', () => {
    if (pass.value === passR.value) {
        pass.classList.add('valid');
        passR.classList.add('valid');
        pass.classList.remove('invalid');
        passR.classList.remove('invalid');
    } else {
        pass.classList.remove('valid');
        passR.classList.remove('valid');
        pass.classList.add('invalid');
        passR.classList.add('invalid');
    }
});
