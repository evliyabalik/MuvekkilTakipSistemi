

//Şifre ve Email Kontrolü

const form = document.getElementById('myForm');

const pass = document.getElementById('Pass');
const passR = document.getElementById('PassR');



form.addEventListener('submit', (e) => {

    if ( pass.value === passR.value) {


        pass.classList.add('valid');

        passR.classList.add('valid');

    } else {


        pass.classList.remove('valid');
        passR.classList.remove('valid');

 

        pass.classList.add('invalid');
        passR.classList.add('invalid');



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





