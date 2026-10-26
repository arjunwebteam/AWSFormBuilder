/* ===========================================================
   Temple360 Form Builder - Designer Engine
   Native HTML5 drag & drop. No external drag-drop library needed.
   =========================================================== */
(function () {
    'use strict';

    var canvas = document.getElementById('fbCanvas');
    var settingsPanel = document.getElementById('fbSettingsPanel');
    var fields = (window.__FORM_DATA__ && window.__FORM_DATA__.Fields) || [];
    var selectedIndex = -1;
    var uid = 0;

    var FIELD_LABELS = {
        text: 'Single Line Text', textarea: 'Paragraph Text', email: 'Email', phone: 'Phone',
        number: 'Number', date: 'Date', dropdown: 'Dropdown', radio: 'Radio Buttons',
        checkbox: 'Checkboxes', file: 'File Upload', heading: 'Heading', paragraph: 'Paragraph Text (static)',
        recaptcha: 'reCAPTCHA', payment: 'Payment'
    };

    function newField(type) {
        uid++;
        return {
            FieldId: 0,
            FieldLabel: FIELD_LABELS[type] || type,
            FieldName: (type + '_' + uid),
            FieldType: type,
            FieldOrder: fields.length,
            IsRequired: false,
            Placeholder: '',
            DefaultValue: '',
            ValidationRegex: '',
            ValidationMessage: '',
            MinLength: null,
            MaxLength: null,
            Options: (type === 'dropdown' || type === 'radio' || type === 'checkbox') ? ['Option 1', 'Option 2'] : [],
            CssClass: '',
            IsActive: true
        };
    }

    /* ---------------- Rendering the canvas ---------------- */

    function renderCanvas() {
        canvas.innerHTML = '';
        if (fields.length === 0) {
            canvas.innerHTML = '<div class="fb-canvas-empty">Drag fields here to build your form</div>';
            return;
        }
        fields.forEach(function (f, idx) {
            var row = document.createElement('div');
            row.className = 'fb-field-row' + (idx === selectedIndex ? ' fb-field-selected' : '');
            row.setAttribute('draggable', 'true');
            row.dataset.index = idx;

            var badge = f.IsRequired ? '<span class="fb-required-badge">Required</span>' : '';
            row.innerHTML =
                '<div class="fb-field-drag-handle">&#8942;&#8942;</div>' +
                '<div class="fb-field-preview">' + renderFieldPreview(f) + badge + '</div>' +
                '<div class="fb-field-actions">' +
                '  <button class="fb-btn-remove" title="Remove">&times;</button>' +
                '</div>';

            row.addEventListener('click', function (e) {
                if (e.target.classList.contains('fb-btn-remove')) return;
                selectedIndex = idx;
                renderCanvas();
                renderSettings();
            });
            row.querySelector('.fb-btn-remove').addEventListener('click', function () {
                fields.splice(idx, 1);
                selectedIndex = -1;
                renderCanvas();
                renderSettings();
            });

            // reorder via drag within canvas
            row.addEventListener('dragstart', function (e) {
                e.dataTransfer.setData('text/reorder-index', idx);
            });
            row.addEventListener('dragover', function (e) { e.preventDefault(); });
            row.addEventListener('drop', function (e) {
                e.preventDefault();
                var fromIdx = e.dataTransfer.getData('text/reorder-index');
                if (fromIdx === '') return; // came from palette instead, handled by canvas drop
                fromIdx = parseInt(fromIdx, 10);
                var toIdx = idx;
                var moved = fields.splice(fromIdx, 1)[0];
                fields.splice(toIdx, 0, moved);
                selectedIndex = toIdx;
                renderCanvas();
                renderSettings();
            });

            canvas.appendChild(row);
        });
    }

    function renderFieldPreview(f) {
        switch (f.FieldType) {
            case 'heading': return '<h4>' + escapeHtml(f.FieldLabel) + '</h4>';
            case 'paragraph': return '<p class="text-muted">' + escapeHtml(f.FieldLabel) + '</p>';
            case 'recaptcha': return '<label>' + escapeHtml(f.FieldLabel) + '</label><div class="fb-fake-input">[ reCAPTCHA widget ]</div>';
            case 'payment': return '<label>' + escapeHtml(f.FieldLabel) + '</label><div class="fb-fake-input">[ Payment fields render here ]</div>';
            case 'textarea': return '<label>' + escapeHtml(f.FieldLabel) + '</label><div class="fb-fake-input" style="height:50px"></div>';
            case 'dropdown': return '<label>' + escapeHtml(f.FieldLabel) + '</label><div class="fb-fake-input">' + (f.Options || []).join(' / ') + '</div>';
            case 'radio':
            case 'checkbox': return '<label>' + escapeHtml(f.FieldLabel) + '</label><div class="fb-fake-input">' + (f.Options || []).map(function (o) { return '&#9634; ' + escapeHtml(o); }).join('&nbsp;&nbsp;') + '</div>';
            default: return '<label>' + escapeHtml(f.FieldLabel) + '</label><div class="fb-fake-input"></div>';
        }
    }

    function escapeHtml(s) {
        return (s || '').replace(/[&<>"']/g, function (c) {
            return { '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c];
        });
    }

    /* ---------------- Settings panel for selected field ---------------- */

    function renderSettings() {
        if (selectedIndex < 0 || !fields[selectedIndex]) {
            settingsPanel.innerHTML = '<div class="fb-settings-empty">Select a field to edit its settings</div>';
            return;
        }
        var f = fields[selectedIndex];
        var isDataField = ['text', 'textarea', 'email', 'phone', 'number', 'date', 'dropdown', 'radio', 'checkbox', 'file'].indexOf(f.FieldType) > -1;
        var hasOptions = ['dropdown', 'radio', 'checkbox'].indexOf(f.FieldType) > -1;

        var html = '<h6>' + FIELD_LABELS[f.FieldType] + ' Settings</h6>';
        html += settingRow('Label', 'text', 'FieldLabel', f.FieldLabel);

        if (isDataField) {
            html += settingRow('Field Name (DB column)', 'text', 'FieldName', f.FieldName);
            html += settingRow('Placeholder', 'text', 'Placeholder', f.Placeholder);
            html += settingRow('Default Value', 'text', 'DefaultValue', f.DefaultValue);

            html += '<div class="form-check form-switch my-2">' +
                '<input class="form-check-input" type="checkbox" id="setIsRequired" ' + (f.IsRequired ? 'checked' : '') + '>' +
                '<label class="form-check-label">Required field</label></div>';

            html += settingRow('Min Length', 'number', 'MinLength', f.MinLength);
            html += settingRow('Max Length', 'number', 'MaxLength', f.MaxLength);
            html += settingRow('Validation Regex (optional)', 'text', 'ValidationRegex', f.ValidationRegex);
            html += settingRow('Validation Error Message', 'text', 'ValidationMessage', f.ValidationMessage);

            if (hasOptions) {
                html += '<label class="mt-2">Options (one per line)</label>';
                html += '<textarea id="setOptions" class="form-control" rows="4">' + (f.Options || []).join('\n') + '</textarea>';
            }
        }

        if (f.FieldType === 'recaptcha') {
            html += '<p class="text-muted small">Configure the site/secret keys under <b>Form Settings</b>. Only one reCAPTCHA field per form is used.</p>';
        }
        if (f.FieldType === 'payment') {
            html += '<p class="text-muted small">Configure the gateway, keys, and amount under <b>Form Settings</b>. Only one payment field per form is used.</p>';
        }

        settingsPanel.innerHTML = html;
        wireSettingInputs(f);
    }

    function settingRow(label, type, key, value) {
        return '<div class="mb-2"><label>' + label + '</label>' +
            '<input type="' + type + '" class="form-control fb-setting-input" data-key="' + key + '" value="' + (value == null ? '' : escapeHtml(String(value))) + '"></div>';
    }

    function wireSettingInputs(f) {
        settingsPanel.querySelectorAll('.fb-setting-input').forEach(function (inp) {
            inp.addEventListener('input', function () {
                var key = inp.dataset.key;
                var val = inp.value;
                f[key] = (inp.type === 'number') ? (val === '' ? null : Number(val)) : val;
                renderCanvas(); // live-update preview label etc.
                // keep this row selected & re-highlight after re-render
                selectedIndex = fields.indexOf(f);
                document.querySelectorAll('.fb-field-row')[selectedIndex]?.classList.add('fb-field-selected');
            });
        });
        var reqBox = document.getElementById('setIsRequired');
        if (reqBox) reqBox.addEventListener('change', function () { f.IsRequired = reqBox.checked; renderCanvas(); });

        var optsBox = document.getElementById('setOptions');
        if (optsBox) optsBox.addEventListener('input', function () {
            f.Options = optsBox.value.split('\n').map(function (s) { return s.trim(); }).filter(Boolean);
            renderCanvas();
        });
    }

    /* ---------------- Palette drag source -> canvas drop target ---------------- */

    document.querySelectorAll('.fb-palette-item').forEach(function (item) {
        item.addEventListener('dragstart', function (e) {
            e.dataTransfer.setData('text/field-type', item.dataset.type);
        });
    });

    canvas.addEventListener('dragover', function (e) { e.preventDefault(); });
    canvas.addEventListener('drop', function (e) {
        e.preventDefault();
        var type = e.dataTransfer.getData('text/field-type');
        if (!type) return; // was a reorder drop, already handled on the row
        // Advanced singleton fields: only one recaptcha / payment field allowed
        if ((type === 'recaptcha' || type === 'payment') && fields.some(function (f) { return f.FieldType === type; })) {
            alert('Only one ' + FIELD_LABELS[type] + ' field is allowed per form.');
            return;
        }
        var f = newField(type);
        fields.push(f);
        selectedIndex = fields.length - 1;
        renderCanvas();
        renderSettings();
    });

    /* ---------------- Form settings modal ---------------- */

    document.getElementById('btnFormSettings').addEventListener('click', function () {
        new bootstrap.Modal(document.getElementById('formSettingsModal')).show();
    });

    /* ---------------- Save ---------------- */

    document.getElementById('btnSaveForm').addEventListener('click', function () {
        var payload = {
            Form: {
                FormId: window.__FORM_DATA__.FormId,
                FormName: document.getElementById('fbFormName').value,
                FormTitle: document.getElementById('fbFormTitle').value,
                SuccessMessage: document.getElementById('setSuccessMessage').value,
                RedirectUrl: document.getElementById('setRedirectUrl').value,
                RecaptchaEnabled: document.getElementById('setRecaptchaEnabled').checked,
                RecaptchaSiteKey: document.getElementById('setRecaptchaSiteKey').value,
                RecaptchaSecretKey: document.getElementById('setRecaptchaSecretKey').value,
                PaymentEnabled: document.getElementById('setPaymentEnabled').checked,
                PaymentGateway: document.getElementById('setPaymentGateway').value,
                PaymentMode: document.getElementById('setPaymentMode').value,
                PaymentPublicKey: document.getElementById('setPaymentPublicKey').value,
                PaymentSecretKey: document.getElementById('setPaymentSecretKey').value,
                FixedAmount: document.getElementById('setFixedAmount').value || null,
                IsActive: true
            },
            Fields: fields
        };

        if (!payload.Form.FormName) { alert('Please enter an internal form name.'); return; }

        fetch(window.__SAVE_URL__, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            if (data.ok) {
                alert('Form saved. Data table synced.');
                window.location.href = window.__LIST_URL__;
            } else {
                alert('Save failed: ' + data.message);
            }
        })
        .catch(function (err) { alert('Save failed: ' + err); });
    });

    /* init */
    renderCanvas();
    renderSettings();
})();
