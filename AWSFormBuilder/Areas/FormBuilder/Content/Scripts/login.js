/* ============================================================
   FORM BUILDER LOGIN PAGE JS
   ============================================================ */

document.addEventListener("DOMContentLoaded", function () {

    /* ========================================================
       LIVE DATE + TIME
       ======================================================== */

    const dateElement =
        document.getElementById("fbCurrentDate");

    const timeElement =
        document.getElementById("fbCurrentTime");


    function updateDateTime() {

        const now = new Date();


        /* ---------------------------------------------
           DATE
           Example:
           Wednesday, 21 May 2025
           --------------------------------------------- */

        const dateOptions = {
            weekday: "long",
            day: "2-digit",
            month: "long",
            year: "numeric"
        };

        const formattedDate =
            now.toLocaleDateString(
                "en-IN",
                dateOptions
            );


        /* ---------------------------------------------
           TIME
           Example:
           10:32:45 AM
           --------------------------------------------- */

        const timeOptions = {
            hour: "2-digit",
            minute: "2-digit",
            second: "2-digit",
            hour12: true
        };

        const formattedTime =
            now.toLocaleTimeString(
                "en-IN",
                timeOptions
            );


        if (dateElement) {
            dateElement.textContent =
                formattedDate;
        }

        if (timeElement) {
            timeElement.textContent =
                formattedTime;
        }
    }


    /* Run immediately */
    updateDateTime();


    /* Update every second */
    setInterval(
        updateDateTime,
        1000
    );


    /* ========================================================
       COPY DEMO LOGIN
       ======================================================== */

    const copyButton =
        document.getElementById(
            "copyDemoCredentials"
        );


    if (copyButton) {

        copyButton.addEventListener(
            "click",
            async function () {

                const email =
                    "support@arjunweb.in";

                const password =
                    "Aws@2026";

                const credentials =
                    email +
                    "    " +
                    password;


                try {

                    await navigator.clipboard.writeText(
                        credentials
                    );

                    const originalText =
                        copyButton.textContent;

                    copyButton.textContent =
                        "Copied!";


                    setTimeout(
                        function () {

                            copyButton.textContent =
                                originalText;

                        },
                        1500
                    );

                } catch (error) {

                    /* Fallback */

                    const temp =
                        document.createElement(
                            "textarea"
                        );

                    temp.value =
                        credentials;

                    document.body.appendChild(
                        temp
                    );

                    temp.select();

                    document.execCommand(
                        "copy"
                    );

                    temp.remove();

                    copyButton.textContent =
                        "Copied!";

                    setTimeout(
                        function () {

                            copyButton.textContent =
                                "Copy";

                        },
                        1500
                    );
                }
            }
        );
    }

});