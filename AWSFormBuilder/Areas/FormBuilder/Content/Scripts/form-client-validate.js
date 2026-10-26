/* ===========================================================
   Temple360 Form Builder - Client-side validation
   Reads data-required / data-regex / data-minlength / data-maxlength
   attributes that Render.cshtml stamps onto each field based on the
   "Required" toggle and validation settings set in the designer.
   =========================================================== */
(function () {
    'use strict';

    var form = document.getElementById('t360Form');
    var msgBox = document.getElementById('t360Message');
    var formMeta = window.__T360_FORM__;

    function showMessage(text, isError) {
        msgBox.style.display = 'block';
        msgBox.className = 't360-message ' + (isError ? 't360-error' : 't360-success');
        msgBox.textContent = text;
        postHeight();
    }

    function clearFieldError(el) {
        var err = el.parentElement.querySelector('.t360-field-error');
        if (err) err.remove();
        el.classList.remove('t360-invalid');
    }

    function setFieldError(el, message) {
        clearFieldError(el);
        el.classList.add('t360-invalid');
        var span = document.createElement('div');
        span.className = 't360-field-error';
        span.textContent = message;
        el.parentElement.appendChild(span);
    }

    function validateSingleInput(el) {
        var required = el.getAttribute('data-required') === 'true';
        var regex = el.getAttribute('data-regex');
        var message = el.getAttribute('data-message') || 'This field is invalid.';
        var minlen = el.getAttribute('data-minlength');
        var maxlen = el.getAttribute('data-maxlength');
        var value = (el.value || '').trim();

        clearFieldError(el);

        if (required && !value) {
            setFieldError(el, 'This field is required.');
            return false;
        }
        if (value && regex) {
            try {
                if (!(new RegExp(regex)).test(value)) { setFieldError(el, message); return false; }
            } catch (e) { /* ignore bad regex */ }
        }
        if (value && minlen && value.length < parseInt(minlen, 10)) {
            setFieldError(el, 'Minimum length is ' + minlen + '.');
            return false;
        }
        if (value && maxlen && value.length > parseInt(maxlen, 10)) {
            setFieldError(el, 'Maximum length is ' + maxlen + '.');
            return false;
        }
        // built-in type checks
        if (value && el.type === 'email' && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value)) {
            setFieldError(el, 'Enter a valid email address.');
            return false;
        }
        return true;
    }

    function validateOptionGroup(groupDiv) {
        var required = groupDiv.getAttribute('data-required') === 'true';
        if (!required) return true;
        var checked = groupDiv.querySelectorAll('input:checked');
        if (checked.length === 0) {
            var span = document.createElement('div');
            span.className = 't360-field-error';
            if (!groupDiv.parentElement.querySelector('.t360-field-error'))
                groupDiv.parentElement.appendChild(span).textContent = 'Please select at least one option.';
            return false;
        }
        var err = groupDiv.parentElement.querySelector('.t360-field-error');
        if (err) err.remove();
        return true;
    }

    function validateAll() {
        var valid = true;
        form.querySelectorAll('input[data-required], input[data-regex], input[data-minlength], input[data-maxlength], textarea[data-required], select[data-required], input[type=file][data-required]')
            .forEach(function (el) {
                if (el.type === 'radio' || el.type === 'checkbox') return; // handled via group below
                if (!validateSingleInput(el)) valid = false;
            });
        form.querySelectorAll('.t360-options[data-required]').forEach(function (grp) {
            if (!validateOptionGroup(grp)) valid = false;
        });
        return valid;
    }

    // live validation on blur
    form.querySelectorAll('input, textarea, select').forEach(function (el) {
        el.addEventListener('blur', function () {
            if (el.type !== 'radio' && el.type !== 'checkbox') validateSingleInput(el);
        });
    });

    form.addEventListener('submit', function (e) {
        e.preventDefault();
        msgBox.style.display = 'none';

        if (!validateAll()) {
            showMessage('Please fix the highlighted fields.', true);
            return;
        }

        // Hand off to the payment gateway module if this form collects payment.
        if (formMeta.PaymentEnabled && window.T360Payment) {
            window.T360Payment.collectAndSubmit(submitForm);
        } else {
            submitForm({});
        }
    });

    function submitForm(paymentResult) {
        var fd = new FormData(form);
        fd.append('FormId', formMeta.FormId);
        if (paymentResult && paymentResult.txnId) fd.append('paymentTxnId', paymentResult.txnId);
        if (paymentResult && paymentResult.status) fd.append('paymentStatus', paymentResult.status);

        // reCAPTCHA v2 checkbox token, if present
        var recaptchaEl = document.querySelector('.g-recaptcha');
        if (recaptchaEl && window.grecaptcha) {
            fd.append('g_recaptcha_response', grecaptcha.getResponse());
        }

        fetch(window.__SUBMIT_URL__, { method: 'POST', body: fd })
            .then(function (r) { return r.json(); })
            .then(function (data) {
                if (data.success) {
                    showMessage(data.message, false);
                    form.reset();
                    if (data.redirectUrl) window.top.location.href = data.redirectUrl;
                } else {
                    showMessage(data.message, true);
                }
            })
            .catch(function () { showMessage('Something went wrong submitting the form. Please try again.', true); });
    }

    function postHeight() {
        window.parent.postMessage({ temple360FormHeight: document.body.scrollHeight }, '*');
    }

    window.addEventListener('load', postHeight);
    window.addEventListener('resize', postHeight);
    new MutationObserver(postHeight).observe(form, { childList: true, subtree: true });
})();
