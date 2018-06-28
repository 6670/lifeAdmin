(function ($, window) {
    $(document).ready(function () {
        $('body').show();
        $('.js-pound').currencySeparatorFormat();
    });


     (function ($) {
        function forceNumeric() {
            var el = $(this);
            var n = el.val().replace(/[^\d]+/g, '');
            n = parseFloat(n) || 0;
            if (n) {
                el.val(n.toFixed(0).replace(/(\d)(?=(\d{3})+$)/g, "$1,"));
            }
            else {
                el.val(el.val().replace(/[^\d]+/g, ''));
            }
        }
        $.fn.currencySeparatorFormat = function () {
            $(this).prop("type", "tel");
            $(this).on('keyup', forceNumeric);
            $(this).on('propertychange input', forceNumeric);
        }
    })($);
    // var baseUrl = 'https://alds.pioneer-web.com/sherlockapi/';
    //var baseUrl = 'http://localhost/sherlockapi/';

    $.extend({
        getQueryString: function (name) {
            function parseParams() {
                var params = {},
                    e,
                    a = /\+/g,  // Regex for replacing addition symbol with a space
                    r = /([^&=]+)=?([^&]*)/g,
                    d = function (s) { return decodeURIComponent(s.replace(a, " ")); },
                    q = window.location.search.substring(1);

                while (e = r.exec(q))
                    params[d(e[1])] = d(e[2]);

                return params;
            }

            if (!this.queryStringParams)
                this.queryStringParams = parseParams();

            return this.queryStringParams[name];
        }
    });

    // Add a new validator
    $.formUtils.addValidator({
        name: 'ukpostcode',
        validatorFunction: function (val, $el, config, language, $form) {
            return isValidPostcode(val);
        },
        errorMessage: 'Please enter valid postcode',
        errorMessageKey: 'badPostcode'
    });
    $.formUtils.addValidator({
        name: 'bridgingLoanAmount',
        validatorFunction: function (val, $el, config, language, $form) {
            var nStr = val.replace(/\,/g, "");
            return isBridgingLoanAmount(nStr);
        },
        errorMessage: 'Amount should be 26000 to 5000000',
        errorMessageKey: 'BadBridgingLoanAmount'

    });
    $.formUtils.addValidator({
        name: 'equityHomeValue',
        validatorFunction: function (val, $el, config, language, $form) {
            return isEquityHomeValue(val);
        },
        errorMessage: 'Home Value should be 50000 to 100000000',
        errorMessageKey: 'badEquityHomeValue'
    });
    $.formUtils.addValidator({
        name: 'loanValue',
        validatorFunction: function (val, $el, config, language, $form) {
            var nStr = val.replace(/\,/g, "");
            return isLoanValue(nStr);
        },
        errorMessage: 'Loan value should be 500 to 1000000',
        errorMessageKey: 'badLoanValue'
    });
    $.formUtils.addValidator({
        name: 'isNumber',
        validatorFunction: function (val, $el, config, language, $form) {
            var nStr = val.replace(/\,/g, "");
            return !isNaN(nStr);
        },
        errorMessage: 'Not a Number',
        errorMessageKey: 'inValidNumber'
    });
    $.formUtils.addValidator({
        name: 'email',
        validatorFunction: function (val, $el, config, language, $form) {
            return isValidEmail(val);
        },
        errorMessage: 'Not a Number',
        errorMessageKey: 'inValidNumber'
    });

    $.formUtils.addValidator({
        name: 'ukphone',
        validatorFunction: function (val, $el, config, language, $form) {
            return isValidUKPhone(val);
        },
        errorMessage: 'Please enter valid phone number',
        errorMessageKey: 'badPhoneNumber'
    });
    function isEquityHomeValue(val) {
        if (val > 50000) {
            return true;
        }
        return false;

    }
    function isValidPostcode(val) {
        var regexp = /^[a-zA-Z]{1,2}[0-9][0-9A-Za-z]? {0,1}[0-9][a-zA-Z]{2}$/;
        return regexp.test(val);
    }
    function isBridgingLoanAmount(val) {
        if (val >= 26000) {
            return true;
        }
        return false;
    }
    function isLoanValue(val) {
        if (val >= 1000) {
            return true;
        }
        return false;

    }

    function isValidEmail(val) {
        var regexp = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
        return regexp.test(val);
    }
    function isValidUKPhone(val) {
        var regexp = /^(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3}\s?\d{3})|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4})|((\+44\s?\d{2}|\(?0\d{2}\)?)\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$/;
        return regexp.test(val);
    }

    window.applyValidation = function (validateOnBlur, forms, messagePosition, scrollToTopOnError, ajaxMode, callback) {

        if (!forms)
            forms = 'form';

        if (!messagePosition)
            messagePosition = 'top';

        if (scrollToTopOnError == undefined) {
            scrollToTopOnError = true;
        }

        if (ajaxMode == undefined)
            ajaxMode = false;

        $.validate({
            form: forms,
            language: {
                requiredFields: '*',
                errorTitle: 'Please correct the errors shown below !'
            },
            validateOnBlur: validateOnBlur,
            errorMessagePosition: messagePosition,
            scrollToTopOnError: scrollToTopOnError,
            lang: 'en',
            borderColorOnError: 'red',
            onModulesLoaded: function () {
            },
            onValidate: function ($f) {
            },
            onError: function ($form) {
            },
            onSuccess: function ($form) {
                if (callback && typeof callback == 'function')
                    callback($form[0].id);
                return !ajaxMode;
            },
            onElementValidate: function (valid, $el, $form, errorMess) {
                var isFormValid = $('#' + $form[0].id).isValid({}, {}, false);

                if (isFormValid) {
                    $('.alert-danger').hide();
                }
                else {
                    $('.alert-danger').show();
                }
            }
        });
    };

    window.applyValidation(true, '#LoginForm', 'false', false, false, submitForm);
 
    function submitForm($form) {
        $('#' + $form + ' :submit')[0].setAttribute("disabled", "disabled");
        $('#' + $form + ' :submit')[0].value = 'Please wait...';

        try {
            var amount = $('#FilterAmount').val();
            amount = filterCommaNumber(amount);
            $('#FilAmount').val(amount);

            var propertyValue = $('#FilterPropertyValue').val();
            propertyValue = filterCommaNumber(propertyValue);
            $('#FilPropertyValue').val(propertyValue);



            var rqstAmount = $('#RequestAmount').val();
            rqstAmount = filterCommaNumber(rqstAmount);
            $('#RqstAmount').val(rqstAmount);


            var restProprtyValue = $('#RquestPropertyValue').val();
            restProprtyValue = filterCommaNumber(restProprtyValue);
            $('#RqstPropertyValue').val(restProprtyValue);

        } catch (err) {
            alert(err);
        }
    }

    function filterCommaNumber(val) {
        val = val || '';
        //alert(val);
        // console.log(val);
        var nwVal = +(val.replace(/\,/g, ""));
        return nwVal;
    }

    function createAjax() {
        var ajaxHttp = null;
        try {
            if (typeof ActiveXObject == 'function')
                ajaxHttp = new ActiveXObject("Microsoft.XMLHTTP");
            else
                if (window.XMLHttpRequest)
                    ajaxHttp = new XMLHttpRequest();
        }
        catch (e) {
            alert(e.message);
            return null;
        }
        return ajaxHttp;
    };

    function sendAjax(method, url, data, onSuccessMethod, $form) {
        if (!method)
            method = 'GET';
        if (!data)
            data = null;

        var AJAXobj = createAjax();
        AJAXobj.onreadystatechange = function () {
            if (AJAXobj.readyState === 4) {
                if (AJAXobj.status === 200) {
                    if (typeof onSuccessMethod == 'function') {
                        onSuccessMethod(AJAXobj, $form);
                    }
                }
                else {
                    $('#' + $form + ' :submit')[0].removeAttribute("disabled");
                    $('#' + $form + ' :submit')[0].value = $('#' + $form + ' .btnName').text();
                }

            }
        };
        var strData = JSON.stringify(data);
        AJAXobj.open(method, url, true);
        AJAXobj.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        AJAXobj.send(strData);
    }
    window.resetRequestCall = function () {
        $("#requestSubmit").removeAttr("disabled");
        $('#requestCallForm')[0].reset();
        $('#requestCallForm' + ' :submit')[0].value = $('#requestCallForm' + ' .btnName').text();
        $('#requestCallForm .modal-body').show();
        $('#requestCallForm .modal-body2').hide();
    };
    window.callNowClick = function () {
        var urlClickMe = baseUrl + 'clickme/42';
        sendAjax('POST', urlClickMe);
    }
    function onSuccessSubmit(shttp, $form) {
        $('#' + $form + ' :submit')[0].removeAttribute("disabled");
        $('#' + $form + ' :submit')[0].value = $('#' + $form + ' .btnName').text();
        $('#' + $form)[0].reset();
        $('#' + $form + ' .modal').modal('show');
        $('#' + $form + ' .modal-body').hide();
        $('#' + $form + ' .modal-body2').show();
    }

    // days
    var day = '<option value="">' + "Day" + '</option>';
    $(".Day").append(day);
    for (var i = 1; i <= 31; i++) {
        day = '<option value="' + i + '">' + i + '</option>';
        $(".Day").append(day);
    }
    //months
    var month = new Array();
    month[0] = "Jan";
    month[1] = "Feb";
    month[2] = "Mar";
    month[3] = "Apr";
    month[4] = "May";
    month[5] = "Jun";
    month[6] = "Jul";
    month[7] = "Aug";
    month[8] = "Sep";
    month[9] = "Oct";
    month[10] = "Nov";
    month[11] = "Dec";

    var m = '<option value="">' + "Month" + '</option>';
    $(".Month").append(m);
    var value = 0;
    for (var i = 0; i <= 11; i++) {
        var date = new Date(2000, i, 1);
        value = i + 1;
        var monthItem = '<option value="' + value + '">' + month[date.getMonth()] + '</option>';
        $(".Month").append(monthItem);
    }
    //years
    var year = '<option value="">' + "Year" + '</option>';
    $(".Year").append(year);

    for (var i = 1930; i <= 1996; i++) {
        year = '<option value="' + i + '">' + i + '</option>';
        $(".Year").append(year);
    }

    $(function () {
        $("#ddlPassport").change(function () {
            if ($(this).val() == "true") {
                $("#dvPassport").show();
            } else {
                $("#dvPassport").hide();
            }
        });
    });

    $(".js-phone").keypress(function (e) {
        if (isValidUKPhone(this.value)) {
            var a = [];
            var k = e.which;
            for (i = 48; i < 58; i++)
                a.push(i);

            if (!(a.indexOf(k) >= 0)) {

            } else {
                $(this).addClass("valid");
                return e.preventDefault();
            }

        }
    });

    //$(".js-pound").keyup(function () {
    //    console.log($(this).val());
    //    var v = getFilterValue($(this).val());

    //    $("#" + this.id).val(v);
    //    return isNumberKey(event);
    //});
    function getFilterValue(val) {
        var nStr = val.replace(/\,/g, "");
        nStr += '';
        x = nStr.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        console.log(x1 + x2 ,"filter");
        return x1 + x2;
    }

    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }

})(jQuery, window);