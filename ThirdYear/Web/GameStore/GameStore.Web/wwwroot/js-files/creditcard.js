var credir = {}
credir.validate = function (e) {

    var number = String(e.target.value);
    var cleanNumber = '';
    for (var i = 0; i < number.length; i++) {
        if (/^[0-9]+$/.test(number.charAt(i))) {
            cleanNumber += number.charAt(i);
        }
    }

    if (e.key != 'Backspace') {
        var formatNumber = '';
        for (var i = 0; i < cleanNumber.length; i++) {
            if (i == 3 || i == 7 || i == 11) {
                formatNumber = formatNumber + cleanNumber.charAt(i) + ' '
            } else {
                formatNumber += cleanNumber.charAt(i)
            }
        }
        e.target.value = formatNumber;
    }
}

credir.expiry = function (e) {
    if (e.key != 'Backspace') {
        var number = String(this.value);
        var cleanNumber = '';
        for (var i = 0; i < number.length; i++) {
            if (i == 1 && number.charAt(i) == '/') {
                cleanNumber = 0 + number.charAt(0);
            }
            if (/^[0-9]+$/.test(number.charAt(i))) {
                cleanNumber += number.charAt(i);
            }
        }

        var formattedMonth = ''
        for (var i = 0; i < cleanNumber.length; i++) {
            if (/^[0-9]+$/.test(cleanNumber.charAt(i))) {
                if (i == 0 && cleanNumber.charAt(i) > 1) {
                    formattedMonth += 0;
                    formattedMonth += cleanNumber.charAt(i);
                    formattedMonth += '/';
                }
                else if (i == 1) {
                    formattedMonth += cleanNumber.charAt(i);
                    formattedMonth += '/';
                }
                else if (i == 2 && cleanNumber.charAt(i) != 2) {
                    formattedMonth += '20' + cleanNumber.charAt(i);
                } else {
                    formattedMonth += cleanNumber.charAt(i);
                }

            }
        }
        this.value = formattedMonth;
    }
}
