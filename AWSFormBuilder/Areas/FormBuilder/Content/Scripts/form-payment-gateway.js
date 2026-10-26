/* ===========================================================
   Temple360 Form Builder - Payment Gateway Bridge
   Mounts the correct gateway's hosted card fields into
   #t360PaymentMount and produces a { txnId, status } result
   before the form actually posts to /FormBuilder/Submit.

   NOTE: Each gateway requires you to also create a server-side
   "create charge / create order / create payment" endpoint using
   that gateway's secret key — that part is merchant-account
   specific and intentionally left as a clearly marked TODO below,
   since it needs your real Stripe/PayPal/Square credentials to
   test against. Everything else (mounting, collecting, wiring
   into the submit flow) is fully implemented.
   =========================================================== */
(function () {
    'use strict';

    var meta = window.__T360_FORM__;
    if (!meta || !meta.PaymentEnabled) return;

    var mount = document.getElementById('t360PaymentMount');
    var amount = meta.FixedAmount;

    function getDynamicAmount() {
        if (!meta.PaymentAmountFieldName) return amount;
        var el = document.querySelector('[name="' + meta.PaymentAmountFieldName + '"]');
        return el ? parseFloat(el.value || '0') : amount;
    }

    var gateway = {

        Stripe: {
            elements: null, card: null,
            init: function () {
                var stripe = Stripe(meta.PaymentPublicKey);
                gateway.Stripe._stripe = stripe;
                var elements = stripe.elements();
                var card = elements.create('card');
                var div = document.createElement('div');
                div.id = 't360-stripe-card';
                div.className = 't360-payment-box';
                mount.appendChild(div);
                card.mount('#t360-stripe-card');
                gateway.Stripe.card = card;
            },
            collect: function (done) {
                gateway.Stripe._stripe.createPaymentMethod({ type: 'card', card: gateway.Stripe.card })
                    .then(function (result) {
                        if (result.error) { done(null, result.error.message); return; }
                        // TODO (server): POST result.paymentMethod.id + getDynamicAmount() to your
                        // own endpoint that calls Stripe's PaymentIntents API with the secret key,
                        // then return the resulting charge/paymentIntent id here as txnId.
                        done({ txnId: result.paymentMethod.id, status: 'Completed' });
                    });
            }
        },

        PayPal: {
            init: function () {
                var div = document.createElement('div');
                div.id = 't360-paypal-buttons';
                mount.appendChild(div);
                gateway.PayPal._resolve = null;
                paypal.Buttons({
                    createOrder: function (data, actions) {
                        return actions.order.create({
                            purchase_units: [{ amount: { value: (getDynamicAmount() || 0).toFixed(2) } }]
                        });
                    },
                    onApprove: function (data, actions) {
                        return actions.order.capture().then(function (details) {
                            if (gateway.PayPal._resolve) {
                                gateway.PayPal._resolve({ txnId: details.id, status: 'Completed' });
                            }
                        });
                    }
                }).render('#t360-paypal-buttons');
            },
            collect: function (done) {
                // PayPal's own button click drives capture; we just wait for onApprove.
                gateway.PayPal._resolve = function (result) { done(result); };
            }
        },

        Square: {
            init: async function () {
                var payments = Square.payments(meta.PaymentPublicKey, meta.PaymentMode === 'Live' ? 'production' : 'sandbox');
                var card = await payments.card();
                var div = document.createElement('div');
                div.id = 't360-square-card';
                mount.appendChild(div);
                await card.attach('#t360-square-card');
                gateway.Square._card = card;
            },
            collect: function (done) {
                gateway.Square._card.tokenize().then(function (result) {
                    if (result.status === 'OK') {
                        // TODO (server): POST result.token + getDynamicAmount() to your own endpoint
                        // that calls Square's CreatePayment API with the secret key, and return the
                        // resulting payment id here as txnId.
                        done({ txnId: result.token, status: 'Completed' });
                    } else {
                        done(null, 'Card details were invalid.');
                    }
                });
            }
        }
    };

    var active = gateway[meta.PaymentGateway];
    if (active) {
        // Square's init is async; others are sync-ish
        Promise.resolve(active.init()).catch(function (e) { console.error('Payment gateway init failed', e); });
    }

    window.T360Payment = {
        collectAndSubmit: function (submitCallback) {
            if (!active) { submitCallback({}); return; }
            active.collect(function (result, error) {
                if (error) {
                    document.getElementById('t360Message').style.display = 'block';
                    document.getElementById('t360Message').className = 't360-message t360-error';
                    document.getElementById('t360Message').textContent = error;
                    return;
                }
                submitCallback(result);
            });
        }
    };
})();
