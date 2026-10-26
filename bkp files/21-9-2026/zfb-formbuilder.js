let formFields = [];
let selectedFieldId = null;
let fieldCounter = 0;
let submitSelected = false;

// ============================================================
// FORM TITLE FIELD — the canvas "Form Title" is just formFields[0],
// a normal heading field flagged isFormTitle, not a separate stuck title.
// ============================================================

function makeDefaultTitleField() {
    fieldCounter++;
    return {
        id: 'f' + fieldCounter,
        type: 'heading',
        label: 'Form Title',
        isFormTitle: true,
        headingFontSize: 24,
        headingTextAlign: 'center'
    };
}

// ============================================================
// ✅ ADDED — MULTI-PAGE FORMS. A page break is just another entry in
// formFields (type: 'pagebreak') — everything after it, up to the next
// pagebreak (or the end), belongs to the next page. No separate "pages"
// array/schema needed: it saves/loads with the form exactly like any
// other field, and drag-reordering it (SortableJS + syncOrderFromDOM(),
// both already generic over formFields) is how the admin moves the split
// point. See renderCanvas()'s pagebreak branch for how it's drawn, and
// RenderForm.cshtml's wireUpMultiPageForm() for how it becomes Next/Back
// steps on the live form.
// ============================================================
// ✅ ADDED — clicking "+ Add New Page" (or deleting a page) was rebuilding
// the whole canvas and, in the process, jumping the browser's scroll
// position back to the top — jarring on a long, multi-page form. Wrap the
// canvas rebuild so we snapshot scroll position first and restore it right
// after, instead of letting the DOM rebuild decide where to scroll.
function withScrollPositionPreserved(fn) {
    const scrollContainer = document.querySelector('.zfb-canvas-scroll');
    const outerY = window.scrollY;
    const innerY = scrollContainer ? scrollContainer.scrollTop : 0;

    fn();

    window.scrollTo(window.scrollX, outerY);
    if (scrollContainer) scrollContainer.scrollTop = innerY;
}

function addNewPage() {
    withScrollPositionPreserved(function () {
        fieldCounter++;
        const pageBreakId = 'f' + fieldCounter;
        formFields.push({
            id: pageBreakId,
            type: 'pagebreak'
        });
        // ✅ ADDED — a fresh pagebreak lands at the very end, so it can leave
        // an existing Payment/Captcha/Terms field stranded on the page before
        // it. Silently pull it forward onto the new last page (see
        // enforceLastPageFieldPlacement below).
        enforceLastPageFieldPlacement(true);

        // ✅ ADDED — every new page gets its own default header label, the
        // same way Page 1 already has the Form Title heading at its top.
        // It's a normal heading field (centered, editable, deletable) —
        // just inserted right after the pagebreak so it leads the new page.
        fieldCounter++;
        const pageBreakIndex = formFields.findIndex(f => f.id === pageBreakId);
        formFields.splice(pageBreakIndex + 1, 0, {
            id: 'f' + fieldCounter,
            type: 'heading',
            label: 'Page Heading',
            required: false,
            width: 'full',
            options: [],
            headingBgColor: '',
            headingTextColor: '#1c2b39',
            headingFontSize: 20,
            headingTextAlign: 'center'
        });

        renderCanvas();
    });
}

// ============================================================
// ✅ ADDED — PAYMENT / CAPTCHA / TERMS ⇒ LAST PAGE ONLY
// Business rule: the Payment field (and, when the admin uses them, the
// Captcha and Terms & Conditions fields) must always sit on the last page
// of the form, immediately before Submit — never on an earlier page.
// "Last page" = everything after the last 'pagebreak' entry in
// formFields (or the whole form, if there are no pagebreaks at all).
// ============================================================
const LAST_PAGE_ONLY_TYPES = ['payment', 'captcha', 'terms'];

function getLastPageStartIndex() {
    let idx = 0;
    formFields.forEach((f, i) => {
        if (f.type === 'pagebreak') idx = i + 1;
    });
    return idx;
}

// Pins Payment/Captcha/Terms to the very end of the form, in a FIXED
// relative order — Terms & Conditions, then Payment, then Captcha —
// immediately before Submit. This is enforced no matter how the admin got
// them out of place: dragged in from an earlier page (the old behavior),
// OR dragged around *within* the last page itself (e.g. pulled Payment up
// above another field on page 2 — previously nothing corrected that).
// Pass `silent = true` to skip the heads-up alert (used for the automatic
// reflow in addNewPage, which isn't really a user "mistake").
// Returns true if anything changed, so callers know whether to re-render.
function enforceLastPageFieldPlacement(silent) {
    const startIdx = getLastPageStartIndex();

    const specialByType = { terms: [], payment: [], captcha: [] };
    const rest = [];
    const movedFromEarlierPage = [];

    formFields.forEach(function (f, i) {
        if (LAST_PAGE_ONLY_TYPES.indexOf(f.type) !== -1) {
            if (i < startIdx) movedFromEarlierPage.push(f);
            specialByType[f.type].push(f);
        } else {
            rest.push(f);
        }
    });

    // Fixed order: Terms & Conditions -> Payment -> Captcha.
    const orderedSpecial = specialByType.terms
        .concat(specialByType.payment, specialByType.captcha);

    if (!orderedSpecial.length) return false;

    // Nothing to do if every special field is already sitting at the very
    // end of formFields, in that exact order.
    const tail = formFields.slice(formFields.length - orderedSpecial.length);
    const alreadyCorrect =
        !movedFromEarlierPage.length &&
        tail.length === orderedSpecial.length &&
        tail.every(function (f, i) { return f === orderedSpecial[i]; });

    if (alreadyCorrect) return false;

    formFields = rest.concat(orderedSpecial);

    // Only alert for the "wrong page" case — a same-page reorder just gets
    // silently snapped back into place, no need to nag the admin every time.
    if (!silent && movedFromEarlierPage.length) {
        const labels = movedFromEarlierPage
            .map(function (f) {
                return f.type === 'payment'
                    ? 'Payment'
                    : f.type === 'captcha'
                        ? 'Captcha'
                        : 'Terms & Conditions';
            })
            .join(', ');

        alert(
            labels +
            ' must be on the last page of the form (right before Submit), so it has been moved there automatically.'
        );
    }

    return true;
}

function ensureTitleField(fallbackTitle) {
    if (formFields.length && formFields[0].isFormTitle) {
        return formFields[0];
    }
    const titleField = makeDefaultTitleField();
    if (fallbackTitle && fallbackTitle.trim()) {
        titleField.label = fallbackTitle.trim();
    }
    formFields.unshift(titleField);
    return titleField;
}

function syncTitlePayloadFromField() {
    const titleField = formFields.find(f => f.isFormTitle);
    let value = titleField ? titleField.label : '';

    // ✅ ADDED — keep the last real title around. If the title field got
    // deleted (titleField missing), fall back to that instead of blanking
    // the hidden payload / DB Title.
    if (value && value.trim()) {
        lastKnownFormTitle = value.trim();
    } else if (!titleField) {
        value = lastKnownFormTitle;
    }

    const hiddenInput = getEl('formTitle');
    if (hiddenInput) hiddenInput.value = value;
    const breadcrumb = getEl('formTitleText');
    if (breadcrumb) breadcrumb.textContent = value || 'Form Title';
}

// ✅ ADDED — top-nav "Form Title" editing. Reuses the existing
// .zfb-title-editor/.zfb-title-input/.editing CSS toggle (already in
// zfb-formbuilder.css from an earlier version of this control) and writes
// straight into the SAME formFields[0] title field that the canvas heading
// and Field Properties "Text" box already edit, via updateFieldLabelLive()
// below — so all three stay in sync no matter which one is used.
function startTopNavTitleEdit() {
    const editor = getEl('formTitleEditor');
    const input = getEl('formTitleEditInput');
    const span = getEl('formTitleText');
    if (!editor || !input || !span) return;

    const titleField = formFields.find(f => f.isFormTitle);
    // ✅ FIXED — if the Form Title field was deleted from the body, the
    // top header should still remember and show the last real title
    // (lastKnownFormTitle), not an empty box that makes it look like the
    // top header itself got wiped too.
    input.value = titleField
        ? (titleField.label || '')
        : (lastKnownFormTitle || '');
    editor.classList.add('editing');
    input.focus();
    input.select();
}

// ✅ ADDED — live, per-keystroke sync for the top-nav title box (previously
// it only updated on blur/Enter). Mirrors onHeadingInlineInput() /
// updateFieldLabelLive(): recreates the title field on demand so typing
// works even right after the body's Form Title heading was deleted.
function onTopNavTitleInput() {
    const input = getEl('formTitleEditInput');
    if (!input) return;

    let titleField = formFields.find(f => f.isFormTitle);
    const isNewTitleField = !titleField;
    if (!titleField) {
        titleField = ensureTitleField(lastKnownFormTitle);
    }

    updateFieldLabelLive(titleField.id, input.value);

    if (selectedFieldId === titleField.id) {
        const textInput = document.querySelector('#propsPanel [data-role="heading-text-input"]');
        if (textInput) textInput.value = titleField.label;
    }

    // The title field didn't exist a moment ago (it had been deleted from
    // the body) — rebuild the canvas once so it reappears at the top as
    // the admin starts typing again. Safe to do here (unlike the canvas
    // inline-edit path) since focus stays in the top-nav input, not a
    // canvas element that a rebuild would steal focus from.
    if (isNewTitleField) {
        renderCanvas();
    }
}

function commitTopNavTitleEdit() {
    const editor = getEl('formTitleEditor');
    const input = getEl('formTitleEditInput');
    if (!editor || !input) return;
    if (!editor.classList.contains('editing')) return; // already committed/cancelled

    const newValue = input.value.trim();
    const titleField = formFields.find(f => f.isFormTitle) || ensureTitleField(lastKnownFormTitle);
    updateFieldLabelLive(titleField.id, newValue || 'Form Title');
    syncTitlePayloadFromField();

    // Keep the Field Properties "Text" box in sync if it's open on this field
    if (selectedFieldId === titleField.id) {
        const textInput = document.querySelector('#propsPanel [data-role="heading-text-input"]');
        if (textInput) textInput.value = titleField.label;
    }

    editor.classList.remove('editing');
}

function cancelTopNavTitleEdit() {
    const editor = getEl('formTitleEditor');
    if (editor) editor.classList.remove('editing');
}

// ✅ ADDED — live inline editing for heading fields (including the top
// Form Title). Typing directly into the canvas heading updates the field's
// label immediately — the top breadcrumb and the Field Properties "Text"
// box reflect the change as you type. Deliberately does NOT call
// renderCanvas(), which would rebuild the DOM and steal focus/cursor
// position mid-keystroke.
function onHeadingInlineInput(id, el) {
    const field = formFields.find(f => f.id === id);
    if (!field) return;

    field.label = el.innerText;

    if (field.isFormTitle) {
        syncTitlePayloadFromField();
    }

    if (selectedFieldId === id) {
        const textInput = document.querySelector(
            '#propsPanel [data-role="heading-text-input"]'
        );
        if (textInput && textInput.value !== field.label) {
            textInput.value = field.label;
        }
    }
}

// ✅ ADDED — the flip side of onHeadingInlineInput(): typing in the Field
// Properties "Text" box updates the field + canvas heading (and the top
// breadcrumb, when it's the Form Title) live, as you type, without a full
// renderCanvas() rebuild that would steal focus from the input itself.
function updateFieldLabelLive(id, value) {
    const field = formFields.find(f => f.id === id);
    if (!field) return;

    field.label = value;

    if (field.isFormTitle) {
        syncTitlePayloadFromField();
    }

    const heading = document.querySelector(
        `.zfb-field-item[data-id="${id}"] h5`
    );
    if (heading && heading.innerText !== value) {
        heading.innerText = value;
    }
}

// Current form ID
let currentFormId = 0;

// ✅ ADDED — remembers the last non-empty title the admin actually typed,
// so deleting the on-canvas title heading doesn't wipe the saved DB Title.
let lastKnownFormTitle = '';

// Logo size
let logoWidth = null;
let logoHeight = 110;

// ✅ ADDED — which field (by id) the logo banner should render immediately
// before. null = logo stays at the very top (default/original behaviour).
// '__end__' = logo has been dragged all the way to the bottom (right before
// the Submit button). Kept up to date by canvasSortable's onEnd handler and
// consumed by renderCanvas() so the position survives re-renders.
let logoBeforeFieldId = null;

// ============================================================
// FORM DESIGNER STATE
// ============================================================

let designPanelOpen = false;
let customizePanelOpen = false;
let designSubTab = 'colors';

let formDesign = {
    colorSchemeId: null,
    themeId: null,

    pageColor: '#eef1f5',
    pageImageUrl: '',

    formColor: '#ffffff',
    formImageUrl: '',

    fontColor: '#33475b',
    inputBackground: '#ffffff',

    formWidth: null,
    labelAlignment: 'top',
    questionSpacing: 12,

    fontFamily: 'Poppins',
    fontSize: 14,
    fontWeight: 'normal',

    buttonStyleId: 'flat-blue',
    buttonCss: 'background:#13458f;color:#fff;',

    logoBgColor: '',
    logoAlignment: 'center',
    logoAltText: '',
    logoLayout: 'stacked'
};

// ============================================================
// BUTTON STYLE PRESETS
// ============================================================

const buttonStylePresets = [
    {
        id: 'flat-blue',
        category: 'flat',
        css: 'background:#13458f;color:#fff;'
    },
    {
        id: 'flat-green',
        category: 'flat',
        css: 'background:#28a745;color:#fff;'
    },
    {
        id: 'flat-red',
        category: 'flat',
        css: 'background:#e0455c;color:#fff;'
    },
    {
        id: 'flat-orange',
        category: 'flat',
        css: 'background:#ff7a1a;color:#fff;'
    },
    {
        id: 'flat-dark',
        category: 'flat',
        css: 'background:#1e2635;color:#fff;'
    },
    {
        id: 'flat-purple',
        category: 'flat',
        css: 'background:#8a4fe0;color:#fff;'
    },
    {
        id: 'gradient-blue',
        category: 'gradient',
        css: 'background:linear-gradient(135deg,#13458f,#8a4fe0);color:#fff;'
    },
    {
        id: 'gradient-sunset',
        category: 'gradient',
        css: 'background:linear-gradient(135deg,#ff7a1a,#e0455c);color:#fff;'
    },
    {
        id: 'gradient-green',
        category: 'gradient',
        css: 'background:linear-gradient(135deg,#28a745,#17a2b8);color:#fff;'
    },
    {
        id: 'outline-blue',
        category: 'flat',
        css: 'background:#fff;color:#13458f;border:2px solid #13458f;'
    },
    {
        id: 'outline-dark',
        category: 'flat',
        css: 'background:#fff;color:#1e2635;border:2px solid #1e2635;'
    },
    {
        id: 'gradient-purple',
        category: 'gradient',
        css: 'background:linear-gradient(135deg,#8a4fe0,#e0455c);color:#fff;'
    }
];

let buttonStyleTab = 'all';

// ============================================================
// COLOR SCHEMES
// ============================================================

const colorSchemes = [
    {
        id: 'blue',
        page: '#eef1f5',
        form: '#ffffff',
        font: '#1c2b39',
        accent: '#13458f'
    },
    {
        id: 'orange',
        page: '#fff7ef',
        form: '#ffffff',
        font: '#5a3b1c',
        accent: '#ff7a1a'
    },
    {
        id: 'green',
        page: '#eefaf1',
        form: '#ffffff',
        font: '#173a25',
        accent: '#28a745'
    },
    {
        id: 'purple',
        page: '#f5f0fb',
        form: '#ffffff',
        font: '#2e1c4a',
        accent: '#8a4fe0'
    },
    {
        id: 'red',
        page: '#fdeef0',
        form: '#ffffff',
        font: '#4a1620',
        accent: '#e0455c'
    },
    {
        id: 'teal',
        page: '#e9f7f7',
        form: '#ffffff',
        font: '#123a3a',
        accent: '#17a2b8'
    },
    {
        id: 'dark',
        page: '#1b2330',
        form: '#2b3648',
        font: '#f1f4f9',
        accent: '#13458f'
    },
    {
        id: 'mono',
        page: '#f4f4f4',
        form: '#ffffff',
        font: '#222222',
        accent: '#333333'
    }
];

// ============================================================
// THEME PRESETS
// ============================================================

const themePresets = [
    {
        id: 'default',
        name: 'Default',
        page: '#eef1f5',
        form: '#ffffff',
        font: '#33475b',
        input: '#ffffff'
    },
    {
        id: 'ocean',
        name: 'Ocean Mist',
        page: '#e7f3fb',
        form: '#ffffff',
        font: '#123a52',
        input: '#f4fbff'
    },
    {
        id: 'sunset',
        name: 'Sunset',
        page: '#fff1e6',
        form: '#fffaf5',
        font: '#5a2d0c',
        input: '#fff7f0'
    },
    {
        id: 'forest',
        name: 'Forest',
        page: '#eaf5ec',
        form: '#ffffff',
        font: '#173a25',
        input: '#f3fbf4'
    },
    {
        id: 'lavender',
        name: 'Lavender',
        page: '#f3effb',
        form: '#ffffff',
        font: '#2e1c4a',
        input: '#f9f6fd'
    },
    {
        id: 'midnight',
        name: 'Midnight',
        page: '#171d29',
        form: '#232c3d',
        font: '#eef1f5',
        input: '#2c3648'
    },
    {
        id: 'graphite',
        name: 'Graphite',
        page: '#eceff1',
        form: '#ffffff',
        font: '#263238',
        input: '#f5f6f7'
    },
    {
        id: 'rose',
        name: 'Rose',
        page: '#fdeef1',
        form: '#fffbfc',
        font: '#4a1620',
        input: '#fff4f6'
    }
];

// ============================================================
// PAYMENT GATEWAY INFO
// ============================================================

const gatewayInfo = {
    paypal: {
        label: 'PayPal',
        icon: 'icon-wallet',
        color: '#003087'
    },
    stripe: {
        label: 'Stripe',
        icon: 'icon-credit-card',
        color: '#635bff'
    },
    square: {
        label: 'Square',
        icon: 'icon-square',
        color: '#1c1c1c'
    }
};

// ============================================================
// HEADING FONT STYLE OPTIONS
// ============================================================

const headingFontFamilyOptions = [
    { value: 'inherit', label: 'Default' },
    { value: 'Arial, Helvetica, sans-serif', label: 'Arial' },
    { value: '"Times New Roman", Times, serif', label: 'Times New Roman' },
    { value: 'Georgia, serif', label: 'Georgia' },
    { value: '"Courier New", Courier, monospace', label: 'Courier New' },
    { value: 'Verdana, Geneva, sans-serif', label: 'Verdana' },
    { value: '"Trebuchet MS", sans-serif', label: 'Trebuchet MS' },
    { value: 'Tahoma, Geneva, sans-serif', label: 'Tahoma' },
    { value: '"Segoe UI", sans-serif', label: 'Segoe UI' },
    { value: 'Poppins, sans-serif', label: 'Poppins' },
    { value: '"Playfair Display", serif', label: 'Playfair Display' },
    { value: '"Courier Prime", monospace', label: 'Courier Prime' }
];

// ============================================================
// GOOGLE FONT OPTIONS  (Form Designer > Font)
// ------------------------------------------------------------
// Top 25 most used web fonts, ordered:
// Poppins first, then Arial, Roboto, then the rest by popularity.
// `google:false` = system font, nothing to download.
// ============================================================

const googleFontOptions = [
    { name: 'Poppins', stack: "'Poppins', sans-serif", google: true },
    { name: 'Arial', stack: "Arial, Helvetica, sans-serif", google: false },
    { name: 'Roboto', stack: "'Roboto', sans-serif", google: true },
    { name: 'Open Sans', stack: "'Open Sans', sans-serif", google: true },
    { name: 'Montserrat', stack: "'Montserrat', sans-serif", google: true },
    { name: 'Lato', stack: "'Lato', sans-serif", google: true },
    { name: 'Inter', stack: "'Inter', sans-serif", google: true },
    { name: 'Oswald', stack: "'Oswald', sans-serif", google: true },
    { name: 'Raleway', stack: "'Raleway', sans-serif", google: true },
    { name: 'Nunito', stack: "'Nunito', sans-serif", google: true },
    { name: 'Playfair Display', stack: "'Playfair Display', serif", google: true },
    { name: 'Merriweather', stack: "'Merriweather', serif", google: true },
    { name: 'Rubik', stack: "'Rubik', sans-serif", google: true },
    { name: 'Work Sans', stack: "'Work Sans', sans-serif", google: true },
    { name: 'Quicksand', stack: "'Quicksand', sans-serif", google: true },
    { name: 'Mulish', stack: "'Mulish', sans-serif", google: true },
    { name: 'Noto Sans', stack: "'Noto Sans', sans-serif", google: true },
    { name: 'PT Sans', stack: "'PT Sans', sans-serif", google: true },
    { name: 'Ubuntu', stack: "'Ubuntu', sans-serif", google: true },
    { name: 'Source Sans 3', stack: "'Source Sans 3', sans-serif", google: true },
    { name: 'Josefin Sans', stack: "'Josefin Sans', sans-serif", google: true },
    { name: 'Manrope', stack: "'Manrope', sans-serif", google: true },
    { name: 'Karla', stack: "'Karla', sans-serif", google: true },
    { name: 'Barlow', stack: "'Barlow', sans-serif", google: true },
    { name: 'DM Sans', stack: "'DM Sans', sans-serif", google: true }
];

// Keeps track of which Google font stylesheets are already in <head>
const zfbLoadedGoogleFonts = {};

// Returns the catalogue entry for a saved font value.
// Accepts either a plain name ("Poppins") or a full stack
// ("'Poppins', sans-serif") so old saved designs keep working.
function zfbGetFontDef(value) {

    if (!value) {
        return googleFontOptions[0];
    }

    const raw = String(value)
        .split(',')[0]
        .replace(/["']/g, '')
        .trim()
        .toLowerCase();

    return (
        googleFontOptions.find(
            f => f.name.toLowerCase() === raw
        ) ||
        null
    );
}

// Full CSS font-family stack for a saved font value.
function zfbFontStack(value) {

    const def = zfbGetFontDef(value);

    if (def) {
        return def.stack;
    }

    // Unknown / custom value — use it as-is with a safe fallback
    return String(value) + ', sans-serif';
}

// Injects the Google Fonts stylesheet for a single family.
// Safe to call repeatedly — it only adds each family once.
function loadGoogleFont(value, targetDoc) {

    const doc = targetDoc || document;
    const def = zfbGetFontDef(value);

    if (!def || !def.google) {
        return;
    }

    const key = def.name;

    if (
        doc === document &&
        zfbLoadedGoogleFonts[key]
    ) {
        return;
    }

    const id =
        'zfb-gfont-' +
        key.replace(/[^a-z0-9]/gi, '-').toLowerCase();

    if (doc.getElementById(id)) {
        zfbLoadedGoogleFonts[key] = true;
        return;
    }

    const link = doc.createElement('link');

    link.id = id;
    link.rel = 'stylesheet';

    link.href =
        'https://fonts.googleapis.com/css2?family=' +
        encodeURIComponent(def.name).replace(/%20/g, '+') +
        ':wght@300;400;500;600;700&display=swap';

    (doc.head || doc.getElementsByTagName('head')[0])
        .appendChild(link);

    if (doc === document) {
        zfbLoadedGoogleFonts[key] = true;
    }
}

// Loads every catalogue font in one request so the Font dropdown
// can preview each option in its own typeface.
function preloadGoogleFontMenu() {

    if (document.getElementById('zfb-gfont-menu')) {
        return;
    }

    const families = googleFontOptions
        .filter(f => f.google)
        .map(
            f =>
                'family=' +
                encodeURIComponent(f.name).replace(/%20/g, '+') +
                ':wght@400;600'
        )
        .join('&');

    const link = document.createElement('link');

    link.id = 'zfb-gfont-menu';
    link.rel = 'stylesheet';

    link.href =
        'https://fonts.googleapis.com/css2?' +
        families +
        '&display=swap';

    document.head.appendChild(link);

    googleFontOptions.forEach(f => {
        if (f.google) {
            zfbLoadedGoogleFonts[f.name] = true;
        }
    });
}

// ============================================================
// FIELD LABELS
// ============================================================

const fieldLabels = {
    heading: "Section Heading",
    fullname: "Full Name",
    firstname: "First Name",
    lastname: "Last Name",
    email: "Email",
    address: "Address",
    country: "Country",
    state: "State",
    borderlabel: "Border Label",
    phone: "Phone Number",
    date: "Date",
    appointment: "Appointment",
    signature: "Signature",
    fillblank: "Fill in the blank",
    productlist: "Product",
    text: "Text Field",
    textarea: "Long Text",
    paragraph: "Paragraph text goes here...",
    dropdown: "Dropdown",
    radio: "Single Choice",
    checkbox: "Multiple Choice",
    number: "Number",
    image: "Image",
    fileupload: "Upload File",
    time: "Time",
    terms: "I agree to the Terms & Conditions",
    captcha: "Please verify that you are human",
    starrating: "Rate this",
    payment: "Payment"
};

// ============================================================
// SAFE ELEMENT HELPER
// ============================================================

function getEl(id) {
    return document.getElementById(id);
}

// ============================================================
// LOGO + HEADING SIDE-BY-SIDE / LOGO ICON STYLES
// (injected here because .zfb-logo-heading-row had no
// stylesheet rule, which is why "Side by Side" rendered
// stacked instead of side by side)
// ============================================================

(function injectZfbLogoLayoutStyles() {

    if (document.getElementById('zfbLogoLayoutStyles')) {
        return;
    }

    const style = document.createElement('style');

    style.id = 'zfbLogoLayoutStyles';

    style.textContent = `
        .zfb-logo-heading-row {
            display: flex !important;
            align-items: center !important;
            flex-wrap: nowrap !important;
            gap: 16px;
            width: 100%;
        }

        /* The logo banner used to carry "width:100%; display:block;"
           from the main stylesheet, which stretched it across the
           whole row and squeezed the heading down to nothing next
           to it (looked like the heading had "disappeared"). Force
           it back to a shrink-to-fit box so the heading has room. */
        .zfb-logo-heading-row .zfb-canvas-logo-banner {
            flex: 0 0 auto !important;
            width: auto !important;
            max-width: 60% !important;
            margin: 0 !important;
            text-align: left !important;
        }

        /* The heading is the second (right-hand) column. Give it the
           remaining space and let long text wrap/ellipsis instead of
           being clipped or pushed out of view. */
        .zfb-logo-heading-row .zfb-field-item {
            flex: 1 1 0% !important;
            min-width: 0 !important;
            width: auto !important;
            display: block !important;
            margin-bottom: 0 !important;
        }

        .zfb-logo-heading-row .zfb-heading-section {
            width: 100% !important;
        }

        .zfb-logo-heading-row .zfb-heading-section h5 {
            overflow-wrap: anywhere;
        }

        /* When the heading's own "Field Width" is set to Half, split
           the row 50/50 instead of shrink-wrapping the logo: heading
           takes its half, logo takes the remaining half. */
        .zfb-logo-heading-row.zfb-logo-half-heading .zfb-canvas-logo-banner {
            flex: 0 0 50% !important;
            width: 50% !important;
            max-width: 50% !important;
        }

        .zfb-logo-heading-row.zfb-logo-half-heading .zfb-field-item {
            flex: 0 0 50% !important;
            width: 50% !important;
            max-width: 50% !important;
        }

        .zfb-canvas-logo-banner {
            position: relative;
        }

        .zfb-canvas-logo-banner .zfb-side-icons {
            position: absolute;
            top: 6px;
            right: 6px;
            display: none;
            gap: 4px;
        }

        .zfb-canvas-logo-banner.zfb-selected .zfb-side-icons {
            display: flex;
        }
    `;

    document.head.appendChild(style);
})();

// ============================================================
// BORDER LABEL FIELD STYLES
// (a labelled horizontal divider — like a fieldset legend —
// used to visually break a form into sections)
// ============================================================

(function () {

    const style = document.createElement('style');

    style.textContent = `
        .zfb-border-label {
            position: relative;
            margin: 24px 0;
            text-align: center;
        }

        .zfb-border-label::before {
            content: '';
            position: absolute;
            top: 50%;
            left: 0;
            right: 0;
            border-top: var(--zfb-border-width, 1px) solid var(--zfb-border-color, #d7dce2);
        }

        .zfb-border-label-text {
            position: relative;
            display: inline-block;
            background: #fff;
            padding: 0 12px;
            color: #4b5563;
            font-size: 13px;
            font-weight: 600;
        }
    `;

    document.head.appendChild(style);
})();

// ============================================================
// INITIALIZATION
// ============================================================

document.addEventListener('DOMContentLoaded', function () {

    // New forms start with formFields empty — seed a title field so it
    // behaves like every other heading (edit/delete/drag) from the start.
    // Existing forms get the equivalent call inside loadExistingForm() in
    // FormBuilder.cshtml, right after the saved schema is parsed.
    if (!formFields.length) {
        ensureTitleField();
        syncTitlePayloadFromField();
        renderCanvas();
    }

    // Pull down the saved Google Font before the first paint so the
    // canvas renders in the right typeface straight away.
    loadGoogleFont(formDesign.fontFamily);

    applyDesignToCanvas();

    initializeSortable();

    // Make sure edit/customize events work
    document.addEventListener('click', function (e) {

        const submitWrapper = document.getElementById(
            'canvasSubmitWrapper'
        );

        if (
            submitWrapper &&
            submitWrapper.contains(e.target) &&
            !e.target.closest('.zfb-icon-btn')
        ) {
            selectSubmitButton();
        }
    });

    // Escape closes preview
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') {
            closeFormPreview();
        }
    });
});

// ============================================================
// SORTABLE INITIALIZATION
// ============================================================

let canvasSortable = null;

function initializeSortable() {

    const paletteList = document.getElementById('paletteList');

    if (paletteList) {
        new Sortable(paletteList, {
            group: {
                name: 'shared',
                pull: 'clone',
                put: false
            },
            sort: false,
            animation: 150
        });
    }

    const paymentPaletteList =
        document.getElementById('paymentPaletteList');

    if (paymentPaletteList) {
        new Sortable(paymentPaletteList, {
            group: {
                name: 'shared',
                pull: 'clone',
                put: false
            },
            sort: false,
            animation: 150
        });
    }

    const canvas = document.getElementById('canvas');

    if (!canvas) return;

    canvasSortable = new Sortable(canvas, {

        group: 'shared',

        animation: 150,

        draggable:
            '.zfb-field-item, .zfb-canvas-submit',

        // ✅ CHANGED — removed the old onMove restriction that pinned the logo
        // banner as always-first. Fields can now be dropped/reordered above
        // the logo banner too (e.g. dragging "First Name" to sit above it).

        onAdd: function (evt) {

            const type =
                evt.item.getAttribute('data-type');

            const gateway =
                evt.item.getAttribute('data-gateway');

            if (type === 'submit') {
                evt.item.remove();
                return;
            }

            // ✅ ADDED — check (before removing the dropped clone) whether it
            // landed above the logo banner. addField()/renderCanvas() below
            // always rebuild new fields right after the logo, so without this
            // check a field dropped above the logo would immediately snap
            // back below it.
            const logoBanner =
                getEl('canvasLogoBanner');

            const droppedAboveLogo =
                !!(
                    logoBanner &&
                    logoBanner.style.display !== 'none' &&
                    (
                        evt.item.compareDocumentPosition(logoBanner) &
                        Node.DOCUMENT_POSITION_FOLLOWING
                    )
                );

            // ✅ FIXED — evt.newIndex is a raw DOM child-position count, and
            // the canvas has several children that are NOT in formFields at
            // all: the per-page "Drag fields here to add them to this page"
            // empty-state hint, the "+ Add New Page" row, and the logo
            // banner. Dropping directly onto one of those (e.g. the dashed
            // hint box of an empty page) confused evt.newIndex enough that
            // the field could land past the intended page — visibly "above"
            // the Add New Page button instead of inside that page. Instead,
            // walk backwards from the dropped clone to the nearest REAL
            // .zfb-field-item and insert right after it in formFields. This
            // is exactly right for the empty-page case too: the dropzone
            // hint always sits immediately after that page's heading field,
            // so landing on/near the hint resolves to "insert right after
            // this page's heading" — i.e. inside that page.
            let walker = evt.item.previousElementSibling;
            let anchorFieldEl = null;

            while (walker) {
                if (
                    walker.classList &&
                    walker.classList.contains('zfb-field-item')
                ) {
                    anchorFieldEl = walker;
                    break;
                }
                walker = walker.previousElementSibling;
            }

            let insertIndex;

            if (anchorFieldEl) {
                const anchorId = anchorFieldEl.getAttribute('data-id');
                const anchorIdx = formFields.findIndex(
                    f => f.id === anchorId
                );
                insertIndex =
                    anchorIdx === -1
                        ? formFields.length
                        : anchorIdx + 1;
            } else {
                // nothing real before the drop point (dropped above
                // everything, e.g. above the logo banner) -> start of canvas
                insertIndex = 0;
            }

            evt.item.remove();

            const createdField =
                addField(
                    type,
                    insertIndex,
                    gateway
                );

            if (droppedAboveLogo && createdField) {
                logoBeforeFieldId = createdField.id;
                renderCanvas();
            }
        },

        onUpdate: function () {
            syncOrderFromDOM();

            // ✅ ADDED — reordering existing fields (including dragging a
            // pagebreak marker itself) can just as easily strand a
            // Payment/Captcha/Terms field off the last page. Snap it back
            // and re-render so the canvas reflects the correction.
            if (enforceLastPageFieldPlacement()) {
                renderCanvas();
            }
        },

        // ✅ ADDED — after ANY drag ends (new field dropped in, or an
        // existing field reordered), read the logo banner's live DOM
        // position and remember which field now comes right after it.
        // renderCanvas() uses this to put the logo back in the same spot
        // on its next rebuild, instead of always snapping it back to the top.
        onEnd: function () {

            const logoBanner =
                getEl('canvasLogoBanner');

            if (
                !logoBanner ||
                logoBanner.style.display === 'none'
            ) {
                return;
            }

            let nextEl =
                logoBanner.nextElementSibling;

            // Skip past the canvas title block (and anything else that
            // isn't a real field/submit) so we find the actual next field.
            while (
                nextEl &&
                !nextEl.classList.contains('zfb-field-item') &&
                nextEl.id !== 'canvasSubmitWrapper'
            ) {
                nextEl = nextEl.nextElementSibling;
            }

            if (!nextEl) {
                logoBeforeFieldId = null;
            } else if (nextEl.id === 'canvasSubmitWrapper') {
                logoBeforeFieldId = '__end__';
            } else {
                logoBeforeFieldId =
                    nextEl.getAttribute('data-id');
            }
        }
    });
}

// ============================================================
// LOGO FUNCTIONS
// ============================================================

function uploadLogo() {

    const fileInput = getEl('logoFile');

    if (!fileInput || !fileInput.files.length) {
        return;
    }

    const formData = new FormData();

    formData.append(
        'logo',
        fileInput.files[0]
    );

    fetch(
        '/Admin/Form/UploadFormLogo',
        {
            method: 'POST',
            body: formData
        }
    )
        .then(res => res.json())
        .then(data => {

            if (data.success) {

                const logoUrl = getEl('logoUrl');

                if (logoUrl) {
                    logoUrl.value = data.url;
                }

                updateLogoPreview(data.url);

            } else {

                alert(
                    'Logo upload failed: ' +
                    data.message
                );
            }
        })
        .catch(err => {
            alert(
                'Upload error: ' +
                err
            );
        });
}

function uploadTermsPdf(fieldId) {

    const fileInput =
        getEl('termsPdfFile_' + fieldId);

    if (
        !fileInput ||
        !fileInput.files.length
    ) {
        return;
    }

    const file = fileInput.files[0];

    if (file.type !== 'application/pdf') {

        alert(
            'Please select a PDF file.'
        );

        return;
    }

    const formData = new FormData();

    formData.append(
        'termsPdf',
        file
    );

    fetch(
        '/Admin/Form/UploadFormTermsPdf',
        {
            method: 'POST',
            body: formData
        }
    )
        .then(res => res.json())
        .then(data => {

            if (data.success) {

                updateField(
                    fieldId,
                    'termsPdfUrl',
                    data.url
                );

                renderProps();

            } else {

                alert(
                    'PDF upload failed: ' +
                    data.message
                );
            }
        })
        .catch(err => {

            alert(
                'Upload error: ' +
                err
            );
        });
}

function updateLogoPreview(url) {

    const preview =
        getEl('logoPreview');

    if (preview) {

        preview.src = url;
        preview.style.display =
            'inline-block';
    }

    const deleteBtn =
        getEl('logoDeleteBtn');

    if (deleteBtn) {
        deleteBtn.style.display =
            'inline-flex';
    }

    const customizeBtn =
        getEl('logoCustomizeBtn');

    if (customizeBtn) {
        customizeBtn.style.display =
            'inline-flex';
    }

    const banner =
        getEl('canvasLogoBanner');

    const bannerImg =
        getEl('canvasLogoImg');

    if (bannerImg) {
        bannerImg.src = url;
    }

    if (banner) {
        banner.style.display =
            'block';
    }

    applyLogoSize();
}

function deleteLogo() {

    if (
        !confirm(
            'Remove the form logo?'
        )
    ) {
        return;
    }

    const logoFile =
        getEl('logoFile');

    if (logoFile) {
        logoFile.value = '';
    }

    const logoUrl =
        getEl('logoUrl');

    if (logoUrl) {
        logoUrl.value = '';
    }

    const preview =
        getEl('logoPreview');

    if (preview) {
        preview.src = '';
        preview.style.display = 'none';
    }

    const deleteBtn =
        getEl('logoDeleteBtn');

    if (deleteBtn) {
        deleteBtn.style.display = 'none';
    }

    const customizeBtn =
        getEl('logoCustomizeBtn');

    if (customizeBtn) {
        customizeBtn.style.display = 'none';
    }

    const banner =
        getEl('canvasLogoBanner');

    const bannerImg =
        getEl('canvasLogoImg');

    if (bannerImg) {
        bannerImg.src = '';
    }

    if (banner) {
        banner.style.display = 'none';
    }

    applyLogoHeadingLayout();
}

function selectLogo() {

    selectedFieldId = null;
    submitSelected = false;

    designPanelOpen = false;

    // IMPORTANT FIX
    // This is what actually opens the Field Properties drawer.
    // editField() sets these two classes; selectLogo() never did,
    // so clicking the logo's customize icon updated the panel's
    // HTML behind the scenes but the drawer itself stayed closed.
    customizePanelOpen = true;

    const workspace =
        document.querySelector(
            '.zfb-workspace'
        );

    if (workspace) {
        workspace.classList.add(
            'zfb-props-open'
        );
    }

    const propsPanel =
        getEl('fbPropsPanel');

    if (propsPanel) {
        propsPanel.classList.add(
            'zfb-props-visible'
        );
        propsPanel.classList.remove(
            'zfb-props-dark'
        );
    }

    const closeBtn =
        getEl('closeDesignBtn');

    if (closeBtn) {
        closeBtn.style.display =
            'inline-flex';
    }

    const title =
        getEl('propsPanelTitle');

    if (title) {
        title.innerHTML =
            '<i class="icon-settings-2"></i> Field Properties';
    }

    document
        .querySelectorAll(
            '.zfb-field-item'
        )
        .forEach(el => {
            el.classList.remove(
                'zfb-selected'
            );
        });

    const banner =
        getEl('canvasLogoBanner');

    if (banner) {
        banner.classList.add(
            'zfb-selected'
        );
    }

    document
        .querySelector('.zfb-canvas-submit')
        ?.classList.remove(
            'zfb-selected'
        );

    updatePanelButtonActiveStates();

    renderCanvas();
    renderLogoProps();
}

function renderLogoProps() {

    const panel =
        getEl('propsPanel');

    if (!panel) return;

    panel.innerHTML = `

        <label class="form-label">
            Logo Height (px)
        </label>

        <input
            type="number"
            class="form-control mb-2"
            min="20"
            max="300"
            value="${logoHeight}"
            onchange="updateLogoSize('height', this.value)"
        />

        <label class="form-label">
            Logo Width (px)
        </label>

        <input
            type="number"
            class="form-control mb-2"
            min="20"
            max="600"
            placeholder="Auto"
            value="${logoWidth ?? ''}"
            onchange="updateLogoSize('width', this.value)"
        />

        <label class="form-label">
            Logo Size
        </label>

        <input
            type="range"
            class="form-range mb-2"
            min="40"
            max="300"
            value="${logoHeight}"
            oninput="
                updateLogoSize('height', this.value);
                renderLogoProps();
            "
        />

        <label class="form-label">
            Background Color
        </label>

        <div class="d-flex gap-2 mb-2">

            <input
                type="color"
                class="form-control form-control-color"
                style="width:48px;"
                value="${formDesign.logoBgColor || '#ffffff'}"
                onchange="
                    updateLogoStyle(
                        'logoBgColor',
                        this.value
                    )
                "
            />

            <button
                type="button"
                class="zfb-btn zfb-btn-ghost"
                onclick="
                    updateLogoStyle(
                        'logoBgColor',
                        ''
                    )
                "
            >
                Clear Background
            </button>

        </div>

        <label class="form-label">
            Logo &amp; Heading Layout
        </label>

        <div class="zfb-align-group mb-1">

            <div
                class="zfb-align-btn ${!formDesign.logoLayout ||
            formDesign.logoLayout === 'stacked'
            ? 'active'
            : ''
        }"
                onclick="
                    updateLogoStyle(
                        'logoLayout',
                        'stacked'
                    )
                "
            >
                Stacked
            </div>

            <div
                class="zfb-align-btn ${formDesign.logoLayout === 'sideBySide'
            ? 'active'
            : ''
        }"
                onclick="
                    updateLogoStyle(
                        'logoLayout',
                        'sideBySide'
                    )
                "
            >
                Side by Side
            </div>

        </div>

        <p class="text-muted small mb-2">
            "Side by Side" places the logo next to the
            first Heading field on the form.
        </p>

        <label class="form-label">
            Alignment
        </label>

        <div class="zfb-align-group">

            <div
                class="zfb-align-btn ${formDesign.logoAlignment === 'left'
            ? 'active'
            : ''
        }"
                onclick="
                    updateLogoStyle(
                        'logoAlignment',
                        'left'
                    )
                "
            >
                Left
            </div>

            <div
                class="zfb-align-btn ${formDesign.logoAlignment === 'center' ||
            !formDesign.logoAlignment
            ? 'active'
            : ''
        }"
                onclick="
                    updateLogoStyle(
                        'logoAlignment',
                        'center'
                    )
                "
            >
                Center
            </div>

            <div
                class="zfb-align-btn ${formDesign.logoAlignment === 'right'
            ? 'active'
            : ''
        }"
                onclick="
                    updateLogoStyle(
                        'logoAlignment',
                        'right'
                    )
                "
            >
                Right
            </div>

        </div>

        <label class="form-label">
            Logo Alt Text
        </label>

        <input
            type="text"
            class="form-control mb-2"
            placeholder="Describe the logo"
            value="${formDesign.logoAltText || ''}"
            onchange="
                updateLogoStyle(
                    'logoAltText',
                    this.value
                )
            "
        />

        <button
            type="button"
            class="zfb-btn zfb-btn-ghost w-100 mt-1"
            onclick="resetLogoSize()"
        >
            <i class="icon-rotate-ccw"></i>
            Reset to Default
        </button>
    `;
}

function updateLogoStyle(key, value) {

    formDesign[key] = value;

    applyLogoSize();
    applyLogoHeadingLayout();

    if (key === 'logoAltText') {

        const img =
            getEl('canvasLogoImg');

        if (img) {
            img.alt = value;
        }
    }

    renderLogoProps();
}

function updateLogoSize(
    dimension,
    value
) {

    if (dimension === 'height') {

        logoHeight =
            value
                ? parseInt(value)
                : 110;

    } else {

        logoWidth =
            value
                ? parseInt(value)
                : null;
    }

    applyLogoSize();
}

function resetLogoSize() {

    logoWidth = null;
    logoHeight = 110;

    applyLogoSize();
    renderLogoProps();
}

function applyLogoSize() {

    const img =
        getEl('canvasLogoImg');

    if (img) {

        img.style.maxHeight = 'none';

        img.style.height =
            logoHeight + 'px';

        img.style.width =
            logoWidth
                ? logoWidth + 'px'
                : 'auto';

        img.alt =
            formDesign.logoAltText || '';
    }

    const banner =
        getEl('canvasLogoBanner');

    if (banner) {

        banner.style.background =
            formDesign.logoBgColor ||
            'transparent';

        banner.style.textAlign =
            formDesign.logoAlignment ||
            'center';
    }
}

// ============================================================
// LOGO + HEADING SIDE-BY-SIDE LAYOUT
// ============================================================

function applyLogoHeadingLayout() {

    const canvas =
        getEl('canvas');

    const banner =
        getEl('canvasLogoBanner');

    if (!canvas || !banner) return;

    let row =
        getEl('zfbLogoHeadingRow');

    const firstFieldEl =
        canvas.querySelector(
            '.zfb-field-item'
        );

    const firstIsHeading =
        firstFieldEl &&
        firstFieldEl.classList.contains(
            'zfb-heading-item'
        );

    const wantsSideBySide =
        formDesign.logoLayout === 'sideBySide' &&
        firstIsHeading &&
        banner.style.display !== 'none';

    if (wantsSideBySide) {

        if (!row) {

            row =
                document.createElement(
                    'div'
                );

            row.id =
                'zfbLogoHeadingRow';

            row.className =
                'zfb-logo-heading-row';

            banner.parentNode.insertBefore(
                row,
                banner
            );
        }

        // Logo goes first (left column), heading goes second
        // (right column) - order below is what decides left/right.
        row.appendChild(banner);
        row.appendChild(firstFieldEl);

        // The heading's own "Field Width" toggle (Full / Half) -
        // renderCanvas() already stamps 'zfb-width-half' on the
        // element when field.width === 'half', so we can read it
        // straight off the DOM instead of looking up formFields.
        const headingWantsHalf =
            firstFieldEl.classList.contains(
                'zfb-width-half'
            );

        row.classList.toggle(
            'zfb-logo-half-heading',
            headingWantsHalf
        );

        // IMPORTANT FIX
        // Belt-and-braces on top of the stylesheet rules above:
        // set the same overrides inline so the layout is correct
        // even if zfb-formbuilder.css loads after this script or
        // has a more specific ".zfb-canvas-logo-banner" rule
        // (e.g. width:100%) that would otherwise stretch the logo
        // across the whole row and squeeze the heading out of view.
        if (headingWantsHalf) {

            banner.style.flex = '0 0 50%';
            banner.style.width = '50%';
            banner.style.maxWidth = '50%';
            banner.style.margin = '0';

            firstFieldEl.style.flex = '0 0 50%';
            firstFieldEl.style.width = '50%';
            firstFieldEl.style.maxWidth = '50%';
            firstFieldEl.style.minWidth = '0';

            // 50/50 split already fills the full row width, so there's no
            // spare space for Left/Center/Right to move the pair within.
            row.style.justifyContent = 'flex-start';

        } else {

            // ✅ ADDED — the Alignment buttons (Left/Center/Right) used to
            // do nothing here: injectZfbLogoLayoutStyles() (above) pins
            // ".zfb-logo-heading-row .zfb-field-item" to
            // "flex: 1 1 0% !important" so the heading always stretched
            // across whatever space was left next to the logo — the pair
            // was pinned to the left edge no matter which alignment was
            // selected. "Left" keeps that familiar stretched look (logo,
            // then heading filling the rest — visually the same either
            // way since the heading has no background/border). For
            // "Center"/"Right" the heading needs to shrink to its own
            // content width instead, so the pair becomes one compact unit
            // the row can actually position with justify-content — and
            // since that has to beat the stylesheet's own !important,
            // it's set the same way here (setProperty(..., 'important'),
            // not a plain .style.flex assignment, which would lose).
            const alignment =
                formDesign.logoAlignment || 'center';

            banner.style.flex = '0 0 auto';
            banner.style.width = 'auto';
            banner.style.maxWidth = '60%';
            banner.style.margin = '0';

            if (alignment === 'left') {

                row.style.justifyContent = 'flex-start';

                firstFieldEl.style.removeProperty('flex');
                firstFieldEl.style.removeProperty('width');
                firstFieldEl.style.removeProperty('max-width');
                firstFieldEl.style.minWidth = '0';

            } else {

                row.style.justifyContent =
                    alignment === 'right'
                        ? 'flex-end'
                        : 'center';

                firstFieldEl.style.setProperty('flex', '0 1 auto', 'important');
                firstFieldEl.style.setProperty('width', 'auto', 'important');
                firstFieldEl.style.setProperty('max-width', '70%', 'important');
                firstFieldEl.style.minWidth = '0';
            }
        }

        firstFieldEl.style.display = 'block';

    } else if (row) {

        while (row.firstChild) {

            canvas.insertBefore(
                row.firstChild,
                row
            );
        }

        row.remove();

        // Undo the inline overrides above so Stacked mode goes
        // back to its normal full-width, block layout instead of
        // staying shrunk from the side-by-side row.
        banner.style.flex = '';
        banner.style.width = '';
        banner.style.maxWidth = '';
        banner.style.margin = '';

        if (firstFieldEl) {
            firstFieldEl.style.flex = '';
            firstFieldEl.style.minWidth = '';
            firstFieldEl.style.width = '';
            firstFieldEl.style.maxWidth = '';
        }
    }
}

// ============================================================
// SUBMIT BUTTON
// ============================================================

function selectSubmitButton() {

    selectedFieldId = null;
    submitSelected = true;

    designPanelOpen = false;

    const closeBtn =
        getEl('closeDesignBtn');

    if (closeBtn) {
        closeBtn.style.display =
            'none';
    }

    const title =
        getEl('propsPanelTitle');

    if (title) {
        title.innerHTML =
            '<i class="icon-mouse-pointer-click"></i> Submit Button';
    }

    const panel =
        getEl('fbPropsPanel');

    if (panel) {
        panel.classList.remove(
            'zfb-props-dark'
        );
    }

    getEl('canvasLogoBanner')
        ?.classList.remove(
            'zfb-selected'
        );

    document
        .querySelectorAll('.zfb-field-item')
        .forEach(el => {
            el.classList.remove(
                'zfb-selected'
            );
        });

    document
        .querySelector('.zfb-canvas-submit')
        ?.classList.add(
            'zfb-selected'
        );

    updatePanelButtonActiveStates();

    renderCanvas();
    renderSubmitProps();
}

function deleteSubmitButton(event) {

    if (event) {
        event.stopPropagation();
    }

    if (
        !confirm(
            'Delete Submit button? You can always drag it back from the palette.'
        )
    ) {
        return;
    }

    const wrapper =
        getEl('canvasSubmitWrapper');

    if (wrapper) {
        wrapper.remove();
    }

    submitSelected = false;

    const panel =
        getEl('propsPanel');

    if (panel) {

        panel.innerHTML =
            '<p class="zfb-props-empty">Submit button removed. Drag a new one from the palette.</p>';
    }
}

function renderSubmitProps() {

    const panel =
        getEl('propsPanel');

    if (!panel) return;

    panel.innerHTML = `

        <p class="text-muted small">
            Submit button can be dragged to any position in the form.
        </p>

        <div class="mt-3">

            <label class="form-label">
                Button Text
            </label>

            <input
                type="text"
                class="form-control"
                value="Submit"
                onchange="updateSubmitText(this.value)"
            />

        </div>

        <div class="mt-3">

            <label class="form-label">
                Button Style
            </label>

            <p class="text-muted small">
                Go to Form Designer → Styles →
                Button Style to customize.
            </p>

        </div>

        <button
            type="button"
            class="zfb-btn zfb-btn-dark w-100 mt-3"
            style="
                justify-content:center;
                background:var(--zc-danger);
            "
            onclick="deleteSubmitButton(event)"
        >
            <i class="icon-trash-2"></i>
            Delete Submit Button
        </button>
    `;
}

function updateSubmitText(text) {

    const btn =
        document.querySelector(
            '.zfb-submit-btn'
        );

    if (btn) {
        btn.textContent =
            text || 'Submit';
    }
}

// ============================================================
// PANEL MANAGEMENT
// ============================================================

function toggleCustomizePanel() {

    if (customizePanelOpen) {

        closeCustomizePanel();

    } else {

        openCustomizePanel();
    }
}

// ============================================================
// RIGHT-SIDE PROPERTIES PANEL
// ============================================================

// Keeps ALL header buttons (Form Builder, Customize, Form Designer, Thank You
// Page, Mail Templates) mutually exclusive: whichever mode the right-side
// panel is showing gets 'active', and when neither panel is open we fall
// back to the Form Builder button being active (since the plain workspace
// is what's visible). Relies on setActiveHeaderButton(), defined in
// FormBuilder.cshtml, which clears 'active' from every header button first.
function updatePanelButtonActiveStates() {
    if (typeof setActiveHeaderButton !== 'function') return;

    if (designPanelOpen) {
        setActiveHeaderButton('formDesignerBtn');
    } else if (customizePanelOpen) {
        setActiveHeaderButton('customizeBtn');
    } else {
        setActiveHeaderButton('backToBuilderBtn');
    }
}

function openCustomizePanel() {
    const panel = document.getElementById('fbPropsPanel');
    const workspace = document.querySelector('.zfb-workspace');

    if (!panel || !workspace) return;

    customizePanelOpen = true;
    designPanelOpen = false;

    panel.classList.remove('zfb-props-dark');

    workspace.classList.add('zfb-props-open');
    panel.classList.add('zfb-props-visible');

    updatePanelButtonActiveStates();

    if (selectedFieldId) {
        const field = formFields.find(f => f.id === selectedFieldId);

        if (field) {
            document.getElementById('propsPanelTitle').innerHTML =
                '<i class="icon-settings-2"></i> Field Properties';

            document.getElementById('closeDesignBtn').style.display =
                'inline-flex';

            renderCanvas();
            renderProps();
        }
    }
}


function closeCustomizePanel() {
    const panel = document.getElementById('fbPropsPanel');
    const workspace = document.querySelector('.zfb-workspace');

    if (!panel || !workspace) return;

    customizePanelOpen = false;

    panel.classList.remove('zfb-props-visible');
    workspace.classList.remove('zfb-props-open');

    document.getElementById('closeDesignBtn').style.display = 'show';
}


function toggleCustomizePanel() {
    if (customizePanelOpen) {
        closeCustomizePanel();
    } else {
        openCustomizePanel();
    }
}


function closePropsPanel() {
    if (designPanelOpen) {
        closeDesignPanel();
        return;
    }

    if (customizePanelOpen) {
        closeCustomizePanel();
        return;
    }

    closeCustomizePanel();
}


// ============================================================
// DESIGN PANEL
// ============================================================

function toggleDesignPanel() {
    if (designPanelOpen) {
        closeDesignPanel();
    } else {
        openDesignPanel();
    }
}


function openDesignPanel() {
    const panel = document.getElementById('fbPropsPanel');
    const workspace = document.querySelector('.zfb-workspace');

    if (!panel || !workspace) return;

    // Close normal customize panel first
    customizePanelOpen = false;

    panel.classList.remove('zfb-props-visible');
    panel.classList.remove('zfb-props-dark');

    designPanelOpen = true;

    selectedFieldId = null;
    submitSelected = false;

    document
        .getElementById('canvasLogoBanner')
        ?.classList.remove('zfb-selected');

    document
        .querySelector('.zfb-canvas-submit')
        ?.classList.remove('zfb-selected');

    // Open RIGHT panel
    workspace.classList.add('zfb-props-open');
    panel.classList.add('zfb-props-visible');
    panel.classList.add('zfb-props-dark');

    document.getElementById('propsPanelTitle').innerHTML =
        '<i class="icon-palette"></i> Form Designer';

    document.getElementById('closeDesignBtn').style.display =
        'inline-flex';

    renderCanvas();
    renderDesignPanel();
}


function closeDesignPanel() {
    const panel = document.getElementById('fbPropsPanel');
    const workspace = document.querySelector('.zfb-workspace');

    if (!panel || !workspace) return;

    designPanelOpen = false;

    panel.classList.remove('zfb-props-dark');
    panel.classList.remove('zfb-props-visible');

    workspace.classList.remove('zfb-props-open');

    document.getElementById('closeDesignBtn').style.display =
        'none';

    document.getElementById('propsPanelTitle').innerHTML =
        '<i class="icon-settings-2"></i> Field Properties';

    renderProps();
}


// ============================================================
// EDIT FIELD
// ============================================================

function editField(fieldId, event) {
    if (event) {
        event.stopPropagation();
    }

    selectedFieldId = fieldId;
    submitSelected = false;
    designPanelOpen = false;

    // Remove previous selection
    document
        .querySelectorAll('.zfb-field-item')
        .forEach(el => el.classList.remove('zfb-selected'));

    // Select current field
    const selectedEl = document.querySelector(
        `.zfb-field-item[data-id="${fieldId}"]`
    );

    if (selectedEl) {
        selectedEl.classList.add('zfb-selected');
    }

    // Open RIGHT-SIDE panel
    openCustomizePanel();

    renderProps();
}

function closeCustomizePanel() {

    const panel =
        getEl('fbPropsPanel');

    const workspace =
        document.querySelector(
            '.zfb-workspace'
        );

    customizePanelOpen = false;

    updatePanelButtonActiveStates();

    if (!panel || !workspace) {
        return;
    }

    panel.classList.remove(
        'zfb-props-visible'
    );

    panel.classList.remove(
        'zfb-props-dark'
    );

    workspace.classList.remove(
        'zfb-props-open'
    );

    const closeBtn =
        getEl('closeDesignBtn');

    if (closeBtn) {
        closeBtn.style.display =
            'none';
    }

    const title =
        getEl('propsPanelTitle');

    if (title) {

        title.innerHTML =
            '<i class="icon-settings-2"></i> Field Properties';
    }
}

// ONLY ONE closePropsPanel FUNCTION
function closePropsPanel() {

    if (designPanelOpen) {

        closeDesignPanel();

        return;
    }

    closeCustomizePanel();
}

// ============================================================
// FORM DESIGNER
// ============================================================

function toggleDesignPanel() {

    if (designPanelOpen) {

        closeDesignPanel();

    } else {

        openDesignPanel();
    }
}

function openDesignPanel() {

    if (customizePanelOpen) {
        closeCustomizePanel();
    }

    designPanelOpen = true;
    customizePanelOpen = false;

    updatePanelButtonActiveStates();

    selectedFieldId = null;
    submitSelected = false;

    getEl('canvasLogoBanner')
        ?.classList.remove(
            'zfb-selected'
        );

    document
        .querySelector('.zfb-canvas-submit')
        ?.classList.remove(
            'zfb-selected'
        );

    document
        .querySelectorAll('.zfb-field-item')
        .forEach(el => {
            el.classList.remove(
                'zfb-selected'
            );
        });

    document
        .querySelector('.zfb-workspace')
        ?.classList.add(
            'zfb-props-open'
        );

    const panel =
        getEl('fbPropsPanel');

    if (panel) {

        panel.classList.add(
            'zfb-props-visible'
        );

        panel.classList.add(
            'zfb-props-dark'
        );
    }

    const closeBtn =
        getEl('closeDesignBtn');

    if (closeBtn) {
        closeBtn.style.display =
            'inline-flex';
    }

    renderCanvas();
    renderDesignPanel();
}

function closeDesignPanel() {

    designPanelOpen = false;

    updatePanelButtonActiveStates();

    const closeBtn =
        getEl('closeDesignBtn');

    if (closeBtn) {
        closeBtn.style.display =
            'none';
    }

    const title =
        getEl('propsPanelTitle');

    if (title) {

        title.innerHTML =
            '<i class="icon-settings-2"></i> Field Properties';
    }

    const panel =
        getEl('fbPropsPanel');

    if (panel) {

        panel.classList.remove(
            'zfb-props-dark'
        );

        panel.classList.remove(
            'zfb-props-visible'
        );
    }

    document
        .querySelector('.zfb-workspace')
        ?.classList.remove(
            'zfb-props-open'
        );

    renderProps();
}

function switchDesignTab(tab) {

    designSubTab = tab;

    renderDesignPanel();
}

// ============================================================
// DESIGN PANEL
// ============================================================

function renderDesignPanel() {

    // Pull in every catalogue font once so the Font dropdown can
    // render each option in its own typeface.
    preloadGoogleFontMenu();

    const title =
        getEl('propsPanelTitle');

    if (title) {

        title.innerHTML =
            '<i class="icon-palette"></i> Form Designer';
    }

    const closeBtn =
        getEl('closeDesignBtn');

    if (closeBtn) {
        closeBtn.style.display =
            'inline-flex';
    }

    const panel =
        getEl('propsPanel');

    if (!panel) return;

    const schemesHtml =
        colorSchemes
            .map(s => `

                <div
                    class="
                        zfb-scheme-swatch
                        ${formDesign.colorSchemeId === s.id
                    ? 'active'
                    : ''
                }
                    "
                    style="background:${s.page};"
                    title="${s.id}"
                    onclick="applyColorScheme('${s.id}')"
                >
                    <span
                        style="
                            background:${s.accent};
                        "
                    ></span>
                </div>

            `)
            .join('');

    const themesHtml =
        themePresets
            .map(t => `

                <div
                    class="
                        zfb-theme-card
                        ${formDesign.themeId === t.id
                    ? 'active'
                    : ''
                }
                    "
                    onclick="applyTheme('${t.id}')"
                >

                    <div
                        class="zfb-theme-thumb"
                        style="
                            background:${t.page};
                        "
                    >

                        <div
                            class="zfb-theme-thumb-card"
                            style="
                                background:${t.form};
                            "
                        >

                            <span
                                style="
                                    background:${t.font};
                                    width:70%;
                                "
                            ></span>

                            <span
                                style="
                                    background:${t.font};
                                    width:45%;
                                "
                            ></span>

                        </div>

                    </div>

                    <div class="zfb-theme-name">
                        ${t.name}
                    </div>

                </div>

            `)
            .join('');

    const btnStylesHtml =
        buttonStylePresets
            .filter(
                b =>
                    buttonStyleTab === 'all' ||
                    b.category === buttonStyleTab
            )
            .map(
                b => `

                    <div
                        class="
                            zfb-btnstyle-swatch
                            ${formDesign.buttonStyleId === b.id
                        ? 'active'
                        : ''
                    }
                        "
                        style="${b.css}"
                        onclick="applyButtonStyle('${b.id}')"
                    >
                        Submit
                    </div>

                `
            )
            .join('');

    let content = '';

    if (designSubTab === 'styles') {

        content = `

            <label class="form-label">
                Label Alignment
            </label>

            <select
                class="form-select mb-3"
                onchange="
                    updateDesign(
                        'labelAlignment',
                        this.value
                    );
                    applyDesignToCanvas();
                "
            >

                <option
                    value="top"
                    ${formDesign.labelAlignment === 'top'
                ? 'selected'
                : ''
            }
                >
                    Top
                </option>

                <option
                    value="left"
                    ${formDesign.labelAlignment === 'left'
                ? 'selected'
                : ''
            }
                >
                    Left
                </option>

                <option
                    value="right"
                    ${formDesign.labelAlignment === 'right'
                ? 'selected'
                : ''
            }
                >
                    Right
                </option>

            </select>

            <label class="form-label">
                Font
            </label>

            <select
                id="zfbFontFamilySelect"
                class="form-select mb-2"
                style="font-family:${zfbFontStack(formDesign.fontFamily)};"
                onchange="
                    loadGoogleFont(this.value);
                    this.style.fontFamily =
                        zfbFontStack(this.value);
                    updateDesign(
                        'fontFamily',
                        this.value
                    );
                    applyDesignToCanvas();
                    updateFontPreview();
                "
            >

                ${googleFontOptions
                .map(
                    f => `
                                <option
                                    value="${f.name}"
                                    style="font-family:${f.stack};"
                                    ${(zfbGetFontDef(formDesign.fontFamily) || {}).name === f.name
                            ? 'selected'
                            : ''
                        }
                                >
                                    ${f.name}
                                </option>
                            `
                )
                .join('')
            }

            </select>

            <div
                id="zfbFontPreview"
                class="zfb-font-preview mb-3"
                style="
                    font-family:${zfbFontStack(formDesign.fontFamily)};
                    font-size:${formDesign.fontSize || 14}px;
                    font-weight:${formDesign.fontWeight || 'normal'};
                    color:${formDesign.fontColor || '#33475b'};
                "
            >
                The quick brown fox jumps over the lazy dog 0123456789
            </div>

            <label class="form-label">
                Font Size (px)
            </label>

            <input
                type="number"
                class="form-control mb-3"
                min="10"
                max="28"
                value="${formDesign.fontSize}"
                onchange="
                    updateDesign(
                        'fontSize',
                        parseInt(this.value)
                    );
                    applyDesignToCanvas();
                    updateFontPreview();
                "
            />

            <label class="form-label">
                Font Weight
            </label>

            <select
                class="form-select mb-3"
                onchange="
                    updateDesign(
                        'fontWeight',
                        this.value
                    );
                    applyDesignToCanvas();
                    updateFontPreview();
                "
            >

                ${[
                { value: 'normal', label: 'Normal' },
                { value: '500', label: 'Medium' },
                { value: '600', label: 'Semibold' },
                { value: '700', label: 'Bold' }
            ]
                .map(
                    w => `
                                <option
                                    value="${w.value}"
                                    ${(formDesign.fontWeight || 'normal') === w.value
                            ? 'selected'
                            : ''
                        }
                                >
                                    ${w.label}
                                </option>
                            `
                )
                .join('')
            }

            </select>

            <label class="form-label">
                Button Style
            </label>

            <div class="zfb-btnstyle-tabs">

                <div
                    class="
                        zfb-btnstyle-tab
                        ${buttonStyleTab === 'all'
                ? 'active'
                : ''
            }
                    "
                    onclick="
                        switchButtonStyleTab('all')
                    "
                >
                    All
                </div>

                <div
                    class="
                        zfb-btnstyle-tab
                        ${buttonStyleTab === 'flat'
                ? 'active'
                : ''
            }
                    "
                    onclick="
                        switchButtonStyleTab('flat')
                    "
                >
                    Flat
                </div>

                <div
                    class="
                        zfb-btnstyle-tab
                        ${buttonStyleTab === 'gradient'
                ? 'active'
                : ''
            }
                    "
                    onclick="
                        switchButtonStyleTab('gradient')
                    "
                >
                    Gradient
                </div>

            </div>

            <div class="zfb-btnstyle-grid">
                ${btnStylesHtml}
            </div>
        `;

    } else if (designSubTab === 'themes') {

        content = `

            <p class="text-muted small mb-2">
                All Themes — pick one to set page,
                form, font & input colors together.
            </p>

            <div class="zfb-theme-grid">
                ${themesHtml}
            </div>
        `;

    } else if (designSubTab === 'layout') {

        content = `

            <label class="form-label">
                Form Width (px)
            </label>

            <input
                type="number"
                class="form-control mb-2"
                min="300"
                max="1400"
                placeholder="Auto (100%)"
                value="${formDesign.formWidth || ''}"
                onchange="
                    updateDesign(
                        'formWidth',
                        this.value
                            ? parseInt(this.value)
                            : null
                    );
                    applyDesignToCanvas();
                "
            />

            <p class="text-muted small mb-3">
                Resize form width.
                Leave blank for full width.
            </p>

            <label class="form-label">
                Question Spacing (px)
            </label>

            <input
                type="number"
                class="form-control mb-2"
                min="4"
                max="60"
                value="${formDesign.questionSpacing}"
                onchange="
                    updateDesign(
                        'questionSpacing',
                        parseInt(this.value)
                    );
                    applyDesignToCanvas();
                "
            />

            <p class="text-muted small">
                Distance between questions.
            </p>
        `;

    } else {

        content = `

            <label class="form-label">
                Color Scheme
            </label>

            <div class="zfb-scheme-row">
                ${schemesHtml}
            </div>

            <label class="form-label mt-3">
                Page Color
            </label>

            <div class="
                d-flex
                gap-2
                align-items-center
                mb-2
            ">

                <input
                    type="color"
                    class="form-control form-control-color"
                    style="width:44px;"
                    value="${formDesign.pageColor}"
                    onchange="
                        updateDesign(
                            'pageColor',
                            this.value
                        )
                    "
                />

                <input
                    type="text"
                    class="form-control form-control-sm"
                    value="${formDesign.pageColor}"
                    onchange="
                        updateDesign(
                            'pageColor',
                            this.value
                        )
                    "
                />

            </div>

            <label class="form-label">
                Page Image
            </label>

            <div class="
                d-flex
                gap-2
                align-items-center
                mb-1
            ">

                <input
                    type="file"
                    class="form-control form-control-sm"
                    accept="image/*"
                    onchange="
                        uploadDesignImage(
                            this,
                            'pageImageUrl'
                        )
                    "
                />

                ${formDesign.pageImageUrl
                ? `
                            <button
                                type="button"
                                class="zfb-btn zfb-btn-ghost"
                                onclick="
                                    updateDesign(
                                        'pageImageUrl',
                                        ''
                                    )
                                "
                            >
                                Clear
                            </button>
                        `
                : ''
            }

            </div>

            ${formDesign.pageImageUrl
                ? `
                        <img
                            src="${formDesign.pageImageUrl}"
                            style="
                                max-height:46px;
                                border-radius:6px;
                                margin-bottom:10px;
                                display:block;
                            "
                        />
                    `
                : ''
            }

            <label class="form-label mt-2">
                Form Color
            </label>

            <div class="
                d-flex
                gap-2
                align-items-center
                mb-2
            ">

                <input
                    type="color"
                    class="form-control form-control-color"
                    style="width:44px;"
                    value="${formDesign.formColor}"
                    onchange="
                        updateDesign(
                            'formColor',
                            this.value
                        )
                    "
                />

                <input
                    type="text"
                    class="form-control form-control-sm"
                    value="${formDesign.formColor}"
                    onchange="
                        updateDesign(
                            'formColor',
                            this.value
                        )
                    "
                />

            </div>

            <label class="form-label">
                Form Image
            </label>

            <div class="
                d-flex
                gap-2
                align-items-center
                mb-1
            ">

                <input
                    type="file"
                    class="form-control form-control-sm"
                    accept="image/*"
                    onchange="
                        uploadDesignImage(
                            this,
                            'formImageUrl'
                        )
                    "
                />

                ${formDesign.formImageUrl
                ? `
                            <button
                                type="button"
                                class="zfb-btn zfb-btn-ghost"
                                onclick="
                                    updateDesign(
                                        'formImageUrl',
                                        ''
                                    )
                                "
                            >
                                Clear
                            </button>
                        `
                : ''
            }

            </div>

            ${formDesign.formImageUrl
                ? `
                        <img
                            src="${formDesign.formImageUrl}"
                            style="
                                max-height:46px;
                                border-radius:6px;
                                margin-bottom:6px;
                                display:block;
                            "
                        />
                    `
                : ''
            }

            <p class="text-muted small mb-2">
                Change form background image
            </p>

            <label class="form-label mt-2">
                Font Color
            </label>

            <div class="
                d-flex
                gap-2
                align-items-center
                mb-1
            ">

                <input
                    type="color"
                    class="form-control form-control-color"
                    style="width:44px;"
                    value="${formDesign.fontColor}"
                    onchange="
                        updateDesign(
                            'fontColor',
                            this.value
                        )
                    "
                />

                <input
                    type="text"
                    class="form-control form-control-sm"
                    value="${formDesign.fontColor}"
                    onchange="
                        updateDesign(
                            'fontColor',
                            this.value
                        )
                    "
                />

            </div>

            <p class="text-muted small mb-2">
                Change font color
            </p>

            <label class="form-label">
                Input Background
            </label>

            <div class="
                d-flex
                gap-2
                align-items-center
                mb-2
            ">

                <input
                    type="color"
                    class="form-control form-control-color"
                    style="width:44px;"
                    value="${formDesign.inputBackground}"
                    onchange="
                        updateDesign(
                            'inputBackground',
                            this.value
                        )
                    "
                />

                <input
                    type="text"
                    class="form-control form-control-sm"
                    value="${formDesign.inputBackground}"
                    onchange="
                        updateDesign(
                            'inputBackground',
                            this.value
                        )
                    "
                />

            </div>

            <button
                type="button"
                class="zfb-btn zfb-btn-ghost w-100 mt-4 reset-btn"
                style="
                    color:var(--zc-danger);
                    border-color:var(--zc-danger);
                "
                onclick="resetDesign()"
            >
                Reset to Default
            </button>
        `;
    }

    panel.innerHTML = `

        <div class="zfb-design-tabs">

            <div
                class="
                    zfb-design-tab
                    ${designSubTab === 'colors'
            ? 'active'
            : ''
        }
                "
                onclick="
                    switchDesignTab('colors')
                "
            >
                Colors
            </div>

            <div
                class="
                    zfb-design-tab
                    ${designSubTab === 'styles'
            ? 'active'
            : ''
        }
                "
                onclick="
                    switchDesignTab('styles')
                "
            >
                Styles
            </div>

            <div
                class="
                    zfb-design-tab
                    ${designSubTab === 'themes'
            ? 'active'
            : ''
        }
                "
                onclick="
                    switchDesignTab('themes')
                "
            >
                Themes
            </div>

            <div
                class="
                    zfb-design-tab
                    ${designSubTab === 'layout'
            ? 'active'
            : ''
        }
                "
                onclick="
                    switchDesignTab('layout')
                "
            >
                Layout
            </div>

        </div>

        ${content}
    `;
}

// ============================================================
// DESIGN UPDATE
// ============================================================

const colorKeys = [
    'pageColor',
    'formColor',
    'fontColor',
    'inputBackground',
    'pageImageUrl',
    'formImageUrl'
];

function updateDesign(
    key,
    value
) {

    formDesign[key] = value;

    if (colorKeys.includes(key)) {

        formDesign.colorSchemeId = null;
        formDesign.themeId = null;
    }

    applyDesignToCanvas();

    if (designPanelOpen) {
        renderDesignPanel();
    }
}

function applyColorScheme(id) {

    const scheme =
        colorSchemes.find(
            x => x.id === id
        );

    if (!scheme) return;

    formDesign.colorSchemeId =
        id;

    formDesign.themeId =
        null;

    formDesign.pageColor =
        scheme.page;

    formDesign.formColor =
        scheme.form;

    formDesign.fontColor =
        scheme.font;

    formDesign.inputBackground =
        scheme.form;

    applyDesignToCanvas();

    renderDesignPanel();
}

function applyTheme(id) {

    const theme =
        themePresets.find(
            x => x.id === id
        );

    if (!theme) return;

    formDesign.themeId =
        id;

    formDesign.colorSchemeId =
        null;

    formDesign.pageColor =
        theme.page;

    formDesign.formColor =
        theme.form;

    formDesign.fontColor =
        theme.font;

    formDesign.inputBackground =
        theme.input;

    formDesign.pageImageUrl =
        '';

    formDesign.formImageUrl =
        '';

    applyDesignToCanvas();

    renderDesignPanel();
}

function resetDesign() {

    formDesign = {

        colorSchemeId: null,
        themeId: null,

        pageColor: '#eef1f5',
        pageImageUrl: '',

        formColor: '#ffffff',
        formImageUrl: '',

        fontColor: '#33475b',
        inputBackground: '#ffffff',

        formWidth: null,
        labelAlignment: 'top',
        questionSpacing: 12,

        fontFamily: 'Poppins',
        fontSize: 14,
        fontWeight: 'normal',

        buttonStyleId: 'flat-blue',
        buttonCss:
            'background:#13458f;color:#fff;',

        logoBgColor: '',
        logoAlignment: 'center',
        logoAltText: '',
        logoLayout: 'stacked'
    };

    applyDesignToCanvas();

    renderDesignPanel();
}

function switchButtonStyleTab(tab) {

    buttonStyleTab =
        tab;

    renderDesignPanel();
}

function applyButtonStyle(id) {

    const style =
        buttonStylePresets.find(
            x => x.id === id
        );

    if (!style) return;

    formDesign.buttonStyleId =
        id;

    formDesign.buttonCss =
        style.css;

    applyDesignToCanvas();

    renderDesignPanel();
}

function uploadDesignImage(
    input,
    targetKey
) {

    if (
        !input ||
        !input.files.length
    ) {
        return;
    }

    const formData =
        new FormData();

    formData.append(
        'logo',
        input.files[0]
    );

    fetch(
        '/Admin/Form/UploadFormLogo',
        {
            method: 'POST',
            body: formData
        }
    )
        .then(res => res.json())
        .then(data => {

            if (data.success) {

                updateDesign(
                    targetKey,
                    data.url
                );

            } else {

                alert(
                    'Image upload failed: ' +
                    data.message
                );
            }
        })
        .catch(err => {

            alert(
                'Upload error: ' +
                err
            );
        });
}

// ============================================================
// APPLY DESIGN TO CANVAS
// ============================================================

function applyDesignToCanvas() {

    const canvas =
        getEl('canvas');

    if (!canvas) return;

    // Form background
    canvas.style.background =
        formDesign.formColor ||
        '#ffffff';

    if (formDesign.formImageUrl) {

        canvas.style.backgroundImage =
            `url('${formDesign.formImageUrl}')`;

        canvas.style.backgroundSize =
            'cover';

        canvas.style.backgroundPosition =
            'center';

    } else {

        canvas.style.backgroundImage =
            'none';
    }

    // Page background
    const canvasScroll =
        document.querySelector(
            '.zfb-canvas-scroll'
        );

    if (canvasScroll) {

        canvasScroll.style.background =
            formDesign.pageImageUrl

                ? `url('${formDesign.pageImageUrl}') center / cover no-repeat fixed`

                : (
                    formDesign.pageColor ||
                    '#eef1f5'
                );
    }

    // Font color
    canvas
        .querySelectorAll(
            '.zfb-field-item > label'
        )
        .forEach(label => {

            label.style.color =
                formDesign.fontColor ||
                '#1c2b39';
        });

    // Inputs
    canvas
        .querySelectorAll(
            '.zfb-preview-input'
        )
        .forEach(input => {

            input.style.background =
                formDesign.inputBackground ||
                '#ffffff';
        });

    // Form width
    canvas.style.maxWidth =
        formDesign.formWidth
            ? formDesign.formWidth + 'px'
            : '900px';

    canvas.style.marginLeft =
        'auto';

    canvas.style.marginRight =
        'auto';

    // Spacing
    canvas.style.gap =
        (
            formDesign.questionSpacing != null
                ? formDesign.questionSpacing
                : 14
        ) + 'px';

    // Submit button
    const submitBtn =
        document.querySelector(
            '.zfb-submit-btn'
        );

    if (submitBtn) {

        submitBtn.style.cssText =
            (
                formDesign.buttonCss ||
                'background:#13458f;color:#fff;'
            ) +
            `
                border:none;
                border-radius:8px;
                padding:10px 28px;
                font-weight:700;
                cursor:grab;
                pointer-events:none;
            `;
    }

    // Font
    // Make sure the selected Google Font is downloaded before we use it,
    // then apply the full stack (not just the bare family name).
    loadGoogleFont(formDesign.fontFamily);

    const activeFontStack =
        zfbFontStack(
            formDesign.fontFamily ||
            'Poppins'
        );

    canvas.style.fontFamily =
        activeFontStack;

    canvas
        .querySelectorAll(
            '.zfb-field-item label, .zfb-preview-input'
        )
        .forEach(el => {

            el.style.fontFamily =
                activeFontStack;

            el.style.fontSize =
                (
                    formDesign.fontSize ||
                    14
                ) + 'px';

            el.style.fontWeight =
                formDesign.fontWeight ||
                'normal';
        });

    // Label alignment
    canvas
        .querySelectorAll(
            '.zfb-field-item:not(.zfb-heading-item)'
        )
        .forEach(item => {

            if (
                formDesign.labelAlignment === 'left' ||
                formDesign.labelAlignment === 'right'
            ) {

                item.style.display =
                    'flex';

                item.style.flexDirection =
                    formDesign.labelAlignment === 'right'
                        ? 'row-reverse'
                        : 'row';

                item.style.alignItems =
                    'flex-start';

                item.style.gap =
                    '10px';

                const label =
                    item.querySelector(
                        ':scope > label'
                    );

                if (label) {

                    label.style.flex =
                        '0 0 140px';

                    label.style.marginBottom =
                        '0';
                }

            } else {

                item.style.display =
                    'block';

                const label =
                    item.querySelector(
                        ':scope > label'
                    );

                if (label) {

                    label.style.marginBottom =
                        '6px';
                }
            }
        });

    applyLogoHeadingLayout();

    updateFontPreview();
}

// ============================================================
// FONT PREVIEW (Design panel sample line)
// ============================================================

function updateFontPreview() {

    const preview =
        getEl('zfbFontPreview');

    if (!preview) return;

    loadGoogleFont(formDesign.fontFamily);

    preview.style.fontFamily =
        zfbFontStack(formDesign.fontFamily);

    preview.style.fontSize =
        (formDesign.fontSize || 14) + 'px';

    preview.style.fontWeight =
        formDesign.fontWeight || 'normal';

    preview.style.color =
        formDesign.fontColor || '#33475b';

    const select =
        getEl('zfbFontFamilySelect');

    if (select) {

        const def =
            zfbGetFontDef(formDesign.fontFamily);

        if (def) {
            select.value = def.name;
        }

        select.style.fontFamily =
            zfbFontStack(formDesign.fontFamily);
    }
}

// ============================================================
// PALETTE TABS
// ============================================================

function switchPaletteTab(tab) {

    const showLabels =
        tab === 'labels';

    const labels =
        getEl('paletteTabLabels');

    const payments =
        getEl('paletteTabPayments');

    const labelsBtn =
        getEl('paletteTabBtnLabels');

    const paymentsBtn =
        getEl('paletteTabBtnPayments');

    if (labels) {
        labels.style.display =
            showLabels
                ? 'block'
                : 'none';
    }

    if (payments) {
        payments.style.display =
            showLabels
                ? 'none'
                : 'block';
    }

    if (labelsBtn) {
        labelsBtn.classList.toggle(
            'active',
            showLabels
        );
    }

    if (paymentsBtn) {
        paymentsBtn.classList.toggle(
            'active',
            !showLabels
        );
    }
}

// ============================================================
// ADD FIELD
// ============================================================

function addField(
    type,
    index,
    gateway
) {

    if (
        type === 'payment' &&
        formFields.some(
            f => f.type === 'payment'
        )
    ) {

        alert(
            'Only one Payment field is allowed per form.'
        );

        return;
    }

    if (
        type === 'captcha' &&
        formFields.some(
            f => f.type === 'captcha'
        )
    ) {

        alert(
            'Only one Captcha field is allowed per form.'
        );

        return;
    }

    fieldCounter++;

    const field = {

        id:
            'f' +
            fieldCounter,

        type:
            type,

        label:
            fieldLabels[type],

        placeholder:
            type === 'firstname'
                ? 'First Name'
                : type === 'lastname'
                    ? 'Last Name'
                    : '',

        required:
            false,

        width:
            'full',

        options:
            (
                type === 'dropdown' ||
                type === 'radio' ||
                type === 'checkbox'
            )
                ? [
                    'Option 1',
                    'Option 2'
                ]
                : [],

        maxStars:
            5,

        firstNameLabel:
            'First Name',

        lastNameLabel:
            'Last Name',

        headingBgColor:
            '',

        headingTextColor:
            '#1c2b39',

        headingFontSize:
            20,

        headingTextAlign:
            'left',

        gateway:
            gateway ||
            'paypal',

        amountType:
            'fixed',

        amount:
            0,

        currency:
            'USD',

        publicKey:
            '',

        gatewayMode:
            'sandbox',

        captchaType:
            'jotcaptcha',

        captchaSiteKey:
            '',

        labelAlignment:
            'center',

        termsDocType:
            'link',

        termsLinkUrl:
            '',

        termsPdfUrl:
            ''
    };

    if (type === 'payment') {
        field.label =
            'Payment';
    }

    if (
        typeof index !== 'number' ||
        index < 0 ||
        index > formFields.length
    ) {
        index =
            formFields.length;
    }

    formFields.splice(
        index,
        0,
        field
    );

    // ✅ ADDED — enforce the "Payment/Captcha/Terms only on the last page"
    // rule right after insertion. If this field was dropped onto an
    // earlier page, it gets pulled forward onto the last page here.
    if (LAST_PAGE_ONLY_TYPES.indexOf(type) !== -1) {
        enforceLastPageFieldPlacement();
    }

    // ✅ FIXED — if the admin deleted the title heading and drags a new
    // Heading field back to the very top, treat it as the form title again
    // (so it still behaves like the title: edits sync to the breadcrumb /
    // hidden Title payload). It should pick up the Form Title value
    // automatically instead of showing the generic "Section Heading"
    // placeholder — that placeholder text belongs to ordinary section
    // headings dropped elsewhere on the canvas, not to the top title slot.
    if (
        type === 'heading' &&
        index === 0 &&
        !formFields.some(f => f.isFormTitle)
    ) {
        field.isFormTitle = true;

        field.label =
            (lastKnownFormTitle && lastKnownFormTitle.trim())
                ? lastKnownFormTitle.trim()
                : 'Form Title';

        syncTitlePayloadFromField();
    }

    renderCanvas();

    selectField(
        field.id
    );

    // ✅ ADDED — return the created field so callers can reference its id
    // (existing callers that ignore the return value are unaffected).
    return field;
}

// ============================================================
// RENDER CANVAS
// ============================================================

function renderCanvas() {

    const canvas =
        getEl('canvas');

    if (!canvas) return;

    // IMPORTANT FIX
    // If the logo + first heading are currently wrapped in the
    // side-by-side row, unwrap them back into the canvas first.
    // Otherwise the code below removes the heading field from
    // inside the row (leaving the row/banner orphaned) and the
    // freshly-rebuilt fields get inserted straight into canvas
    // instead of the row, which is why "Side by Side" kept
    // reverting to stacked on every re-render.
    const existingRow =
        getEl('zfbLogoHeadingRow');

    if (existingRow) {

        while (existingRow.firstChild) {

            canvas.insertBefore(
                existingRow.firstChild,
                existingRow
            );
        }

        existingRow.remove();

        // ✅ FIX — clear the side-by-side inline overrides right here too.
        // Without this, applyLogoHeadingLayout()'s own cleanup (the
        // "else if (row)" branch) never runs on the next call because by
        // then the row is already gone — leaving the banner permanently
        // shrunk even after switching back to Stacked.
        const bannerToReset = getEl('canvasLogoBanner');

        if (bannerToReset) {
            bannerToReset.style.flex = '';
            bannerToReset.style.width = '';
            bannerToReset.style.maxWidth = '';
            bannerToReset.style.margin = '';
        }
    }

    canvas
        .querySelectorAll(
            '.zfb-field-item'
        )
        .forEach(
            el => el.remove()
        );

    // ✅ ADDED — per-page dropzone hints get freshly recomputed/re-inserted
    // below on every render (see the block after the field-building loop),
    // same as .zfb-field-item above.
    canvas
        .querySelectorAll(
            '.zfb-page-dropzone-hint'
        )
        .forEach(
            el => el.remove()
        );

    const emptyHint =
        getEl('emptyHint');

    if (emptyHint) {

        emptyHint.style.display =
            formFields.length
                ? 'none'
                : 'block';
    }

    const submitWrapper =
        getEl('canvasSubmitWrapper');

    const insertBefore =
        submitWrapper || null;

    // ✅ ADDED — running page counter for the pagebreak branch below;
    // marker N is the start of "Page " + (N + 1) since content before the
    // first marker is always Page 1.
    let pageBreakCount = 0;

    formFields.forEach(field => {

        const div =
            document.createElement(
                'div'
            );

        div.className =
            'zfb-field-item' +
            (
                field.id === selectedFieldId
                    ? ' zfb-selected'
                    : ''
            ) +
            (
                field.width === 'half'
                    ? ' zfb-width-half'
                    : ''
            );

        div.setAttribute(
            'data-id',
            field.id
        );

        div.onclick =
            function () {
                selectField(
                    field.id
                );
            };

        // Heading
        if (field.type === 'heading') {

            div.classList.add(
                'zfb-heading-item'
            );

            const bg =
                field.headingBgColor
                    ? field.headingBgColor
                    : 'transparent';

            const fg =
                field.headingTextColor ||
                '#1c2b39';

            const fs =
                field.headingFontSize ||
                20;

            const align =
                field.headingTextAlign ||
                'left';

            const ff =
                field.headingFontFamily ||
                'inherit';

            div.innerHTML = `

                <div
                    class="zfb-heading-section"
                    style="
                        background-color:${bg};
                    "
                >

                    <h5
                        class="mb-0"
                        contenteditable="true"
                        spellcheck="false"
                        style="
                            color:${fg};
                            font-size:${fs}px;
                            font-family:${ff};
                            text-align:${align};
                            width:100%;
                            outline:none;
                            cursor:text;
                        "
                        onclick="event.stopPropagation(); selectField('${field.id}');"
                        oninput="onHeadingInlineInput('${field.id}', this)"
                    >${escapeHtml(field.label)}</h5>

                    <div class="zfb-side-icons">

                        <button
                            type="button"
                            class="
                                zfb-icon-btn
                                zfb-icon-edit
                            "
                            onclick="
                                editField(
                                    '${field.id}',
                                    event
                                )
                            "
                            title="Customize"
                        >
                            <i class="icon-settings-2"></i>
                        </button>

                        <button
                            type="button"
                            class="
                                zfb-icon-btn
                                zfb-icon-delete
                            "
                            onclick="
                                removeField(
                                    '${field.id}',
                                    event
                                )
                            "
                            title="Delete"
                        >
                            <i class="icon-trash-2"></i>
                        </button>

                    </div>

                </div>
            `;

        } else if (field.type === 'pagebreak') {

            // ✅ ADDED — page-break marker row. No properties panel for
            // this one, so it isn't selectable — just draggable (to move
            // the split point) and deletable (to merge pages back).
            div.onclick = null;
            div.classList.add('zfb-pagebreak-item');

            pageBreakCount++;

            div.innerHTML = `
                <div class="zfb-pagebreak-row">
                    <span class="zfb-pagebreak-line"></span>
                    <span class="zfb-pagebreak-label">Page ${pageBreakCount + 1}</span>
                    <span class="zfb-pagebreak-line"></span>
                    <button
                        type="button"
                        class="zfb-icon-btn zfb-icon-delete"
                        onclick="deletePageBreak('${field.id}', event)"
                        title="Delete Page Break"
                    >
                        <i class="icon-trash-2"></i>
                    </button>
                </div>
            `;

        } else {

            div.innerHTML = `

                <label>
                    ${escapeHtml(field.label)}
                    ${field.required
                    ? ' <span class="text-danger">*</span>'
                    : ''
                }
                </label>

                ${renderPreviewInput(field)}

                <div class="zfb-side-icons">

                    <button
                        type="button"
                        class="
                            zfb-icon-btn
                            zfb-icon-edit
                        "
                        onclick="
                            editField(
                                '${field.id}',
                                event
                            )
                        "
                        title="Customize"
                    >
                        <i class="icon-settings-2"></i>
                    </button>

                    <button
                        type="button"
                        class="
                            zfb-icon-btn
                            zfb-icon-delete
                        "
                        onclick="
                            removeField(
                                '${field.id}',
                                event
                            )
                        "
                        title="Delete"
                    >
                        <i class="icon-trash-2"></i>
                    </button>

                </div>
            `;
        }

        if (insertBefore) {

            canvas.insertBefore(
                div,
                insertBefore
            );

        } else {

            canvas.appendChild(
                div
            );
        }
    });

    // ============================================================
    // ✅ ADDED — PER-PAGE EMPTY-STATE DROPZONE HINT
    // A freshly added page (its auto "Page Heading" and nothing else yet)
    // used to just float above the "+ Add New Page" row with no visual cue
    // that fields belong there. This mirrors #emptyHint's dashed-box
    // treatment, but per page: split formFields into page segments on each
    // 'pagebreak' entry, and for any segment that has no real field (just
    // a heading, or nothing at all), drop a "Drag fields here" box right
    // after that page's last rendered item.
    // ============================================================
    if (formFields.length) {

        const pageSegments = [];
        let segStart = 0;

        formFields.forEach((f, i) => {
            if (f.type === 'pagebreak') {
                pageSegments.push({ start: segStart, end: i });
                segStart = i + 1;
            }
        });
        pageSegments.push({ start: segStart, end: formFields.length });

        pageSegments.forEach(seg => {

            const pageFields = formFields.slice(seg.start, seg.end);

            const hasRealContent = pageFields.some(
                f => f.type !== 'heading'
            );

            if (hasRealContent) return;

            let anchorEl = null;

            if (pageFields.length) {

                const lastField = pageFields[pageFields.length - 1];

                anchorEl = canvas.querySelector(
                    `.zfb-field-item[data-id="${lastField.id}"]`
                );

            } else if (seg.start === 0) {

                anchorEl = canvas.querySelector(
                    '.zfb-first-page-label'
                );

            } else {

                const pageBreakField = formFields[seg.start - 1];

                anchorEl = canvas.querySelector(
                    `.zfb-field-item[data-id="${pageBreakField.id}"]`
                );
            }

            if (!anchorEl) return;

            const hint = document.createElement('div');

            hint.className = 'zfb-page-dropzone-hint';

            hint.innerHTML =
                '<i class="icon-panel-left"></i>Drag fields here to add them to this page';

            anchorEl.insertAdjacentElement('afterend', hint);
        });
    }

    // ✅ ADDED — re-anchor the static "+ Add New Page" row so it always
    // sits right after the last rendered field (whichever page it's on)
    // and right before the Submit preview, on every render.
    const addPageBtnRow = getEl('addPageBtnWrapper');

    if (addPageBtnRow) {
        if (insertBefore) {
            canvas.insertBefore(addPageBtnRow, insertBefore);
        } else {
            canvas.appendChild(addPageBtnRow);
        }
    }

    // ✅ ADDED — put the logo banner back at whatever position the admin
    // last dragged it to (tracked in logoBeforeFieldId), instead of always
    // resetting it to the very top of the canvas on every rebuild. The
    // title used to be a separate canvas-body element handled here too;
    // it's now formFields[0] and rebuilt by the forEach loop above like
    // any other field.
    const logoBannerEl = getEl('canvasLogoBanner');

    if (logoBannerEl && logoBannerEl.style.display !== 'none') {

        let targetEl = null;

        if (logoBeforeFieldId === '__end__') {
            targetEl = submitWrapper;
        } else if (logoBeforeFieldId) {
            targetEl = canvas.querySelector(
                `.zfb-field-item[data-id="${logoBeforeFieldId}"]`
            );
        }

        if (!targetEl) {
            // logoBeforeFieldId is null, or that field was deleted since -
            // fall back to the original default: logo stays first.
            logoBeforeFieldId = null;
            targetEl = canvas.firstElementChild;
        }

        if (targetEl && targetEl !== logoBannerEl) {
            canvas.insertBefore(logoBannerEl, targetEl);
        }
    }

    applyDesignToCanvas();
    applyLogoHeadingLayout();
}

// ============================================================
// PREVIEW INPUT
// ============================================================

function renderPreviewInput(field) {

    const label = field.label || 'Field';
    const required = field.required
        ? '<span class="zfb-required">*</span>'
        : '';

    switch (field.type) {

        case 'firstname':
        case 'lastname':
        case 'email':
        case 'phone':
        case 'number':
        case 'text':

            return `
                <div class="zfb-floating-field">
                    <input
                        type="${field.type === 'email' ? 'email' :
                    field.type === 'phone' ? 'tel' :
                        field.type === 'number' ? 'number' : 'text'}"
                        class="zfb-preview-input"
                        placeholder="${field.placeholder || label}"
                        disabled
                    />

                    <span class="zfb-floating-label">
                        ${label} ${required}
                    </span>
                </div>
            `;

        case 'fullname':

            return `
                <div class="zfb-fullname-row">

                    <div class="zfb-floating-field">
                        <input
                            type="text"
                            class="zfb-preview-input"
                            placeholder="${field.firstNameLabel || 'First Name'}"
                            disabled
                        />
                        <span class="zfb-floating-label">
                            ${field.firstNameLabel || 'First Name'}
                        </span>
                    </div>

                    <div class="zfb-floating-field">
                        <input
                            type="text"
                            class="zfb-preview-input"
                            placeholder="${field.lastNameLabel || 'Last Name'}"
                            disabled
                        />
                        <span class="zfb-floating-label">
                            ${field.lastNameLabel || 'Last Name'}
                        </span>
                    </div>

                </div>
            `;

        case 'textarea':

            return `
                <div class="zfb-floating-field zfb-floating-textarea">
                    <textarea
                        class="zfb-preview-input"
                        placeholder="${field.placeholder || ''}"
                        disabled
                    ></textarea>

                    <span class="zfb-floating-label">
                        ${label} ${required}
                    </span>
                </div>
            `;

        case 'dropdown':

            return `
                <div class="zfb-floating-field">
                    <select
                        class="zfb-preview-input"
                        disabled
                    >
                        ${field.options.map(option =>
                `<option>${option}</option>`
            ).join('')}
                    </select>

                    <span class="zfb-floating-label">
                        ${label} ${required}
                    </span>
                </div>
            `;

        case 'date':

            return `
                <div class="zfb-floating-field">
                    <input
                        type="date"
                        class="zfb-preview-input"
                        disabled
                    />

                    <span class="zfb-floating-label">
                        ${label} ${required}
                    </span>
                </div>
            `;

        case 'time':

            return `
                <div class="zfb-floating-field">
                    <input
                        type="time"
                        class="zfb-preview-input"
                        disabled
                    />

                    <span class="zfb-floating-label">
                        ${label} ${required}
                    </span>
                </div>
            `;

        case 'appointment':

            return `
                <div class="zfb-floating-field">
                    <input
                        type="datetime-local"
                        class="zfb-preview-input"
                        disabled
                    />

                    <span class="zfb-floating-label">
                        ${label} ${required}
                    </span>
                </div>
            `;

        case 'address':

            // Builder-canvas mockup only (all inputs disabled) — matches the
            // real Street / City+Zip layout rendered live in RenderForm.cshtml.
            // Country and State live as their own separate fields now (drag
            // in a "Country" and/or "State" chip); State auto-loads its
            // options based on whichever Country field is on the form.
            return `
                <div class="zfb-address-fields">

                    <div class="zfb-floating-field">
                        <input
                            type="text"
                            class="zfb-preview-input"
                            placeholder="Street Address"
                            disabled
                        />
                        <span class="zfb-floating-label">
                            Street Address
                        </span>
                    </div>

                    <div class="zfb-address-row">

                        <div class="zfb-floating-field">
                            <input
                                type="text"
                                class="zfb-preview-input"
                                placeholder="City"
                                disabled
                            />
                            <span class="zfb-floating-label">City</span>
                        </div>

                        <div class="zfb-floating-field">
                            <input
                                type="text"
                                class="zfb-preview-input"
                                placeholder="Zip"
                                disabled
                            />
                            <span class="zfb-floating-label">Zip</span>
                        </div>

                    </div>
                </div>
            `;

        case 'country':

            // Builder-canvas mockup only (matches the other disabled previews
            // above) — the searchable country list itself is rendered for
            // real on the live form (RenderForm.cshtml).
            return `
                <div class="zfb-floating-field">
                    <select class="zfb-preview-input" disabled>
                        <option>Select Country</option>
                    </select>

                    <span class="zfb-floating-label">
                        ${label} ${required}
                    </span>
                </div>
            `;

        case 'state':

            // Builder-canvas mockup only — the live form (RenderForm.cshtml)
            // auto-populates this from whichever "Country" field is on the
            // form; with no Country field present it just shows a default
            // state list.
            return `
                <div class="zfb-floating-field">
                    <select class="zfb-preview-input" disabled>
                        <option>Select State</option>
                    </select>

                    <span class="zfb-floating-label">
                        ${label} ${required}
                    </span>
                </div>
            `;

        case 'captcha': {

            // Mock-up only — matches whichever Captcha Type + Label
            // Alignment is selected in Field Properties, so the canvas
            // shows the same widget + position the live form will use.
            const alignment =
                field.labelAlignment ||
                'center';

            const captchaType =
                field.captchaType ||
                'recaptcha';

            const widgetHtml =
                captchaType === 'jotcaptcha'
                    ? `
                        <div class="zfb-captcha-preview zfb-captcha-image">
                            <div class="zfb-captcha-image-box">
                                <span class="zfb-captcha-image-text">8h3Kq</span>
                                <span class="zfb-captcha-refresh" title="Refresh">
                                    <i class="icon-refresh-cw"></i>
                                </span>
                            </div>
                            <input
                                type="text"
                                class="zfb-preview-input zfb-captcha-image-input"
                                placeholder="Type the text above"
                                disabled
                            />
                        </div>
                    `
                    : `
                        <div class="zfb-captcha-preview zfb-captcha-recaptcha">
                            <div class="zfb-recaptcha-box">
                                <span class="zfb-recaptcha-checkbox">
                                    <input type="checkbox" disabled />
                                </span>
                                <span class="zfb-recaptcha-text">I'm not a robot</span>
                                <div class="zfb-recaptcha-logo">
                                    <i class="icon-shield-check"></i>
                                    <span>reCAPTCHA</span>
                                </div>
                            </div>
                            <div class="zfb-recaptcha-footer">Privacy - Terms</div>
                        </div>
                    `;

            return `
                <div class="zfb-captcha-wrap zfb-captcha-align-${alignment}">
                    <span class="zfb-captcha-field-label">
                        ${label} ${required}
                    </span>
                    ${widgetHtml}
                </div>
            `;
        }

        case 'borderlabel': {

            // A labelled horizontal divider, draggable/droppable just like
            // any other field (dragging above the logo is still blocked by
            // the same canvas guard as everything else). When showLabel is
            // explicitly false, it renders as a plain line with no text.
            const borderColor =
                field.borderColor ||
                '#d7dce2';

            const borderWidth =
                (typeof field.borderWidth === 'number')
                    ? field.borderWidth
                    : 1;

            const borderStyle =
                `--zfb-border-color:${borderColor};--zfb-border-width:${borderWidth}px;`;

            return field.showLabel === false
                ? `<div class="zfb-border-label" style="${borderStyle}"></div>`
                : `
                    <div class="zfb-border-label" style="${borderStyle}">
                        <span class="zfb-border-label-text">${label}</span>
                    </div>
                `;
        }

        case 'paragraph':

            return `
                <p class="zfb-paragraph-field">
                    ${label}
                </p>
            `;

        default:

            return `
                <div class="zfb-floating-field">
                    <input
                        type="text"
                        class="zfb-preview-input"
                        placeholder="${field.placeholder || label}"
                        disabled
                    />

                    <span class="zfb-floating-label">
                        ${label} ${required}
                    </span>
                </div>
            `;
    }
}
// ============================================================
// FIELD SELECTION
// ============================================================

function selectField(id) {

    selectedFieldId =
        id;

    submitSelected =
        false;

    designPanelOpen =
        false;

    // IMPORTANT
    // Do NOT close customize panel if already open.
    // This allows clicking another field while panel stays open.

    const title =
        getEl('propsPanelTitle');

    if (title) {

        title.innerHTML =
            '<i class="icon-settings-2"></i> Field Properties';
    }

    const panel =
        getEl('fbPropsPanel');

    if (panel) {

        panel.classList.remove(
            'zfb-props-dark'
        );
    }

    document
        .querySelectorAll(
            '.zfb-field-item'
        )
        .forEach(el => {

            el.classList.remove(
                'zfb-selected'
            );
        });

    const selectedElement =
        document.querySelector(
            `.zfb-field-item[data-id="${id}"]`
        );

    if (selectedElement) {

        selectedElement.classList.add(
            'zfb-selected'
        );
    }

    getEl('canvasLogoBanner')
        ?.classList.remove(
            'zfb-selected'
        );

    document
        .querySelector('.zfb-canvas-submit')
        ?.classList.remove(
            'zfb-selected'
        );

    updatePanelButtonActiveStates();

    renderCanvas();

    renderProps();
}

// ============================================================
// EDIT FIELD - FIXED
// ============================================================

function editField(
    fieldId,
    event
) {

    if (event) {
        event.stopPropagation();
    }

    const field =
        formFields.find(
            f => f.id === fieldId
        );

    if (!field) {

        console.error(
            'Field not found:',
            fieldId
        );

        return;
    }

    selectedFieldId =
        fieldId;

    submitSelected =
        false;

    designPanelOpen =
        false;

    // IMPORTANT FIX
    customizePanelOpen =
        true;

    const panel =
        getEl('fbPropsPanel');

    const workspace =
        document.querySelector(
            '.zfb-workspace'
        );

    if (workspace) {

        workspace.classList.add(
            'zfb-props-open'
        );
    }

    if (panel) {

        panel.classList.add(
            'zfb-props-visible'
        );

        panel.classList.remove(
            'zfb-props-dark'
        );
    }

    const closeBtn =
        getEl('closeDesignBtn');

    if (closeBtn) {

        closeBtn.style.display =
            'inline-flex';
    }

    const title =
        getEl('propsPanelTitle');

    if (title) {

        title.innerHTML =
            '<i class="icon-settings-2"></i> Field Properties';
    }

    getEl('canvasLogoBanner')
        ?.classList.remove(
            'zfb-selected'
        );

    document
        .querySelector('.zfb-canvas-submit')
        ?.classList.remove(
            'zfb-selected'
        );

    document
        .querySelectorAll(
            '.zfb-field-item'
        )
        .forEach(el => {

            el.classList.remove(
                'zfb-selected'
            );
        });

    const fieldElement =
        document.querySelector(
            `.zfb-field-item[data-id="${fieldId}"]`
        );

    if (fieldElement) {

        fieldElement.classList.add(
            'zfb-selected'
        );
    }

    updatePanelButtonActiveStates();

    renderCanvas();

    renderProps();
}

// ============================================================
// REMOVE FIELD
// ============================================================

function removeField(
    id,
    evt
) {

    if (evt) {
        evt.stopPropagation();
    }

    // ✅ ADDED — same scroll-jump fix as addNewPage(): deleting a field
    // (especially a page-break "Delete Page" click) was snapping the
    // canvas back to the top. Preserve wherever the admin was looking.
    withScrollPositionPreserved(function () {

        formFields =
            formFields.filter(
                f => f.id !== id
            );

        if (
            selectedFieldId === id
        ) {

            selectedFieldId =
                null;
        }

        // If the title field itself got deleted, clear the hidden payload
        // too — saveForm() will fall back to "Untitled Form".
        syncTitlePayloadFromField();

        renderCanvas();

        renderProps();
    });
}

// ✅ ADDED — "Delete Page" (the trash icon on a page-break row). Deleting
// just the pagebreak field used to merge that page's fields into the
// previous page — including its auto-inserted "Page Heading", which was
// left behind as an orphaned heading sitting in the merged page instead
// of disappearing with the page it belonged to. This removes the
// pagebreak AND that page's own leading heading together, so deleting a
// page actually removes the whole page's own boilerplate, not just the
// split point. Any real content fields the page had (inputs, etc.) still
// merge upward into the previous page, same as before.
function deletePageBreak(id, evt) {

    if (evt) {
        evt.stopPropagation();
    }

    withScrollPositionPreserved(function () {

        const idx = formFields.findIndex(f => f.id === id);
        if (idx === -1) return;

        const idsToRemove = [id];

        const nextField = formFields[idx + 1];
        if (nextField && nextField.type === 'heading' && !nextField.isFormTitle) {
            idsToRemove.push(nextField.id);
        }

        formFields = formFields.filter(
            f => idsToRemove.indexOf(f.id) === -1
        );

        if (idsToRemove.indexOf(selectedFieldId) !== -1) {
            selectedFieldId = null;
        }

        // Merging this page's content into the previous page can strand a
        // Payment/Terms/Captcha field off the (new) real last page if
        // there's another page after this one — pull it back where it
        // belongs.
        enforceLastPageFieldPlacement(true);

        renderCanvas();

        renderProps();
    });
}

// ============================================================
// SORT ORDER
// ============================================================

function syncOrderFromDOM() {

    const fieldItems =
        document.querySelectorAll(
            '#canvas .zfb-field-item'
        );

    const fieldIds =
        Array.from(
            fieldItems
        ).map(
            el =>
                el.getAttribute(
                    'data-id'
                )
        );

    formFields.sort(
        (a, b) =>
            fieldIds.indexOf(a.id) -
            fieldIds.indexOf(b.id)
    );
}

// ============================================================
// FIELD WIDTH
// ============================================================

function fieldWidthToggleHtml(
    field
) {

    return `

        <label class="form-label mt-2">
            Field Width
        </label>

        <div class="zfb-width-toggle">

            <div
                class="
                    zfb-width-btn
                    ${field.width !== 'half'
            ? 'active'
            : ''
        }
                "
                onclick="
                    updateField(
                        '${field.id}',
                        'width',
                        'full'
                    );
                    renderProps();
                "
            >
                Full Width
            </div>

            <div
                class="
                    zfb-width-btn
                    ${field.width === 'half'
            ? 'active'
            : ''
        }
                "
                onclick="
                    updateField(
                        '${field.id}',
                        'width',
                        'half'
                    );
                    renderProps();
                "
            >
                Half (Side by Side)
            </div>

        </div>
    `;
}

// ============================================================
// RENDER FIELD PROPERTIES
// ============================================================

function renderProps() {

    const panel =
        getEl('propsPanel');

    if (!panel) return;

    const field =
        formFields.find(
            f => f.id === selectedFieldId
        );

    if (!field) {

        if (submitSelected) {

            renderSubmitProps();

            return;
        }

        panel.innerHTML = `
            <p class="zfb-props-empty">
                <i class="icon-mouse-pointer-click"></i>
                Select a field to edit
            </p>
        `;

        return;
    }

    // ========================================================
    // FULL NAME
    // ========================================================

    if (field.type === 'fullname') {

        panel.innerHTML = `

            <label class="form-label">
                Field Label
            </label>

            <input
                class="form-control mb-2"
                value="${escapeAttribute(field.label)}"
                onchange="
                    updateField(
                        '${field.id}',
                        'label',
                        this.value
                    )
                "
            />

            <label class="form-label">
                Box Labels (shown side by side)
            </label>

            <div class="d-flex gap-2 mb-2">

                <input
                    class="form-control"
                    placeholder="First Name"
                    value="${escapeAttribute(
            field.firstNameLabel || ''
        )
            }"
                    onchange="
                        updateField(
                            '${field.id}',
                            'firstNameLabel',
                            this.value
                        )
                    "
                />

                <input
                    class="form-control"
                    placeholder="Last Name"
                    value="${escapeAttribute(
                field.lastNameLabel || ''
            )
            }"
                    onchange="
                        updateField(
                            '${field.id}',
                            'lastNameLabel',
                            this.value
                        )
                    "
                />

            </div>

            <div class="form-check">

                <input
                    type="checkbox"
                    class="form-check-input"
                    id="reqChk"
                    ${field.required
                ? 'checked'
                : ''
            }
                    onchange="
                        updateField(
                            '${field.id}',
                            'required',
                            this.checked
                        )
                    "
                />

                <label
                    class="form-check-label"
                    for="reqChk"
                >
                    Required
                </label>

            </div>

            ${fieldWidthToggleHtml(field)}
        `;

        return;
    }

    // ========================================================
    // HEADING
    // ========================================================

    if (field.type === 'heading') {

        panel.innerHTML = `

            <label class="form-label">
                Text
            </label>

            <input
                class="form-control mb-2"
                data-role="heading-text-input"
                value="${escapeAttribute(field.label)}"
                oninput="
                    updateFieldLabelLive(
                        '${field.id}',
                        this.value
                    )
                "
                onchange="
                    updateField(
                        '${field.id}',
                        'label',
                        this.value
                    )
                "
            />

            <label class="form-label">
                Heading Background Color
            </label>

            <div class="d-flex gap-2 mb-2">

                <input
                    type="color"
                    class="
                        form-control
                        form-control-color
                    "
                    style="width:48px;"
                    value="${field.headingBgColor ||
            '#ffffff'
            }"
                    onchange="
                        updateField(
                            '${field.id}',
                            'headingBgColor',
                            this.value
                        )
                    "
                />

                <button
                    type="button"
                    class="zfb-btn zfb-btn-ghost"
                    onclick="
                        updateField(
                            '${field.id}',
                            'headingBgColor',
                            ''
                        );
                        renderProps();
                    "
                >
                    Clear
                </button>

            </div>

            <label class="form-label">
                Heading Text Color
            </label>

            <div class="d-flex gap-2 align-items-center mb-2">

                <input
                    type="color"
                    class="
                        form-control
                        form-control-color
                    "
                    style="width:48px;"
                    value="${field.headingTextColor ||
            '#1c2b39'
            }"
                    onchange="
                        updateField(
                            '${field.id}',
                            'headingTextColor',
                            this.value
                        );
                        renderProps();
                    "
                />

                <input
                    type="text"
                    class="form-control form-control-sm"
                    value="${field.headingTextColor ||
            '#1c2b39'
            }"
                    onchange="
                        updateField(
                            '${field.id}',
                            'headingTextColor',
                            this.value
                        );
                        renderProps();
                    "
                />

            </div>

            <label class="form-label">
                Font Size (px)
            </label>

            <input
                type="number"
                class="form-control mb-2"
                min="10"
                max="60"
                value="${field.headingFontSize || 20
            }"
                onchange="
                    updateField(
                        '${field.id}',
                        'headingFontSize',
                        parseInt(this.value) || 20
                    )
                "
            />

            <label class="form-label">
                Font Style
            </label>

            <select
                class="form-select mb-2"
                onchange="
                    updateField(
                        '${field.id}',
                        'headingFontFamily',
                        this.value
                    )
                "
            >
                ${headingFontFamilyOptions.map(
                opt => `
                    <option
                        value="${opt.value}"
                        style="font-family:${opt.value};"
                        ${(field.headingFontFamily || 'inherit') === opt.value
                        ? 'selected'
                        : ''
                    }
                    >
                        ${opt.label}
                    </option>
                `
            ).join('')}
            </select>

            <label class="form-label">
                Text Alignment
            </label>

            <div class="zfb-align-group mb-2">

                <div
                    class="zfb-align-btn ${(
                !field.headingTextAlign ||
                field.headingTextAlign === 'left'
            )
                ? 'active'
                : ''
            }"
                    onclick="
                        updateField(
                            '${field.id}',
                            'headingTextAlign',
                            'left'
                        );
                        renderProps();
                    "
                >
                    Left
                </div>

                <div
                    class="zfb-align-btn ${field.headingTextAlign === 'center'
                ? 'active'
                : ''
            }"
                    onclick="
                        updateField(
                            '${field.id}',
                            'headingTextAlign',
                            'center'
                        );
                        renderProps();
                    "
                >
                    Center
                </div>

                <div
                    class="zfb-align-btn ${field.headingTextAlign === 'right'
                ? 'active'
                : ''
            }"
                    onclick="
                        updateField(
                            '${field.id}',
                            'headingTextAlign',
                            'right'
                        );
                        renderProps();
                    "
                >
                    Right
                </div>

            </div>

            ${fieldWidthToggleHtml(field)}
        `;

        return;
    }

    // ========================================================
    // PAYMENT
    // ========================================================

    if (field.type === 'payment') {

        const gw =
            gatewayInfo[field.gateway] ||
            gatewayInfo.paypal;

        const keyLabel =
            field.gateway === 'paypal'
                ? 'PayPal Client ID'
                : field.gateway === 'square'
                    ? 'Square Application ID'
                    : 'Stripe Publishable Key';

        panel.innerHTML = `

            <div class="zfb-gateway-badge">

                <span
                    class="zfb-gateway-icon"
                    style="
                        background:${gw.color};
                    "
                >
                    <i class="${gw.icon}"></i>
                </span>

                ${gw.label} Payment

            </div>

            <label class="form-label">
                Section Title
            </label>

            <input
                class="form-control mb-2"
                value="${escapeAttribute(field.label)}"
                onchange="
                    updateField(
                        '${field.id}',
                        'label',
                        this.value
                    )
                "
            />

            <div class="form-check mb-3">

                <input
                    type="checkbox"
                    class="form-check-input"
                    id="reqChk"
                    ${field.required
                ? 'checked'
                : ''
            }
                    onchange="
                        updateField(
                            '${field.id}',
                            'required',
                            this.checked
                        )
                    "
                />

                <label
                    class="form-check-label"
                    for="reqChk"
                >
                    Required
                </label>

            </div>

            <label class="form-label">
                Mode
            </label>

            <select
                class="form-select mb-2"
                onchange="
                    updateField(
                        '${field.id}',
                        'gatewayMode',
                        this.value
                    )
                "
            >

                <option
                    value="sandbox"
                    ${field.gatewayMode === 'sandbox'
                ? 'selected'
                : ''
            }
                >
                    Sandbox / Test
                </option>

                <option
                    value="live"
                    ${field.gatewayMode === 'live'
                ? 'selected'
                : ''
            }
                >
                    Live
                </option>

            </select>

            <label class="form-label">
                ${keyLabel}
            </label>

            <input
                class="form-control mb-1"
                placeholder="pk_... / client-id / app-id"
                value="${escapeAttribute(
                field.publicKey || ''
            )
            }"
                onchange="
                    updateField(
                        '${field.id}',
                        'publicKey',
                        this.value
                    )
                "
            />

            <p class="text-muted small mb-2">
                This is the <b>public</b> key only —
                safe to expose on the live form.
                Never enter a secret key here.
            </p>

            <label class="form-label">
                Amount Type
            </label>

            <select
                class="form-select mb-2"
                onchange="
                    updateField(
                        '${field.id}',
                        'amountType',
                        this.value
                    );
                    renderProps();
                "
            >

                <option
                    value="fixed"
                    ${field.amountType === 'fixed'
                ? 'selected'
                : ''
            }
                >
                    Fixed Amount
                </option>

                <option
                    value="userdefined"
                    ${field.amountType === 'userdefined'
                ? 'selected'
                : ''
            }
                >
                    User-Defined Amount
                </option>

                <option
                    value="productlist"
                    ${field.amountType === 'productlist'
                ? 'selected'
                : ''
            }
                >
                    Based on Product List
                </option>

            </select>

            ${field.amountType === 'fixed'
                ? `

                        <label class="form-label">
                            Amount
                        </label>

                        <div class="d-flex gap-2 mb-2">

                            <select
                                class="form-select"
                                style="max-width:90px;"
                                onchange="
                                    updateField(
                                        '${field.id}',
                                        'currency',
                                        this.value
                                    )
                                "
                            >

                                <option
                                    value="USD"
                                    ${field.currency === 'USD'
                    ? 'selected'
                    : ''
                }
                                >
                                    USD
                                </option>

                                <option
                                    value="EUR"
                                    ${field.currency === 'EUR'
                    ? 'selected'
                    : ''
                }
                                >
                                    EUR
                                </option>

                                <option
                                    value="GBP"
                                    ${field.currency === 'GBP'
                    ? 'selected'
                    : ''
                }
                                >
                                    GBP
                                </option>

                                <option
                                    value="INR"
                                    ${field.currency === 'INR'
                    ? 'selected'
                    : ''
                }
                                >
                                    INR
                                </option>

                            </select>

                            <input
                                type="number"
                                step="0.01"
                                min="0"
                                class="form-control"
                                value="${field.amount || 0
                }"
                                onchange="
                                    updateField(
                                        '${field.id}',
                                        'amount',
                                        parseFloat(
                                            this.value
                                        ) || 0
                                    )
                                "
                            />

                        </div>
                    `
                : ''
            }

            ${field.amountType ===
                'productlist'
                ? renderPaymentProductListHtml(
                    field
                )
                : ''
            }
        `;

        return;
    }

    // ========================================================
    // TERMS
    // ========================================================

    if (field.type === 'terms') {

        const docType =
            field.termsDocType ||
            'link';

        panel.innerHTML = `

            <label class="form-label">
                Label Text
            </label>

            <input
                class="form-control mb-2"
                value="${escapeAttribute(field.label)}"
                onchange="
                    updateField(
                        '${field.id}',
                        'label',
                        this.value
                    )
                "
            />

            <label class="form-label">
                Terms Document
            </label>

            <div
                class="
                    zfb-width-toggle
                    mb-2
                "
            >

                <div
                    class="
                        zfb-width-btn
                        ${docType === 'link'
                ? 'active'
                : ''
            }
                    "
                    onclick="
                        updateField(
                            '${field.id}',
                            'termsDocType',
                            'link'
                        );
                        renderProps();
                    "
                >
                    Link
                </div>

                <div
                    class="
                        zfb-width-btn
                        ${docType === 'pdf'
                ? 'active'
                : ''
            }
                    "
                    onclick="
                        updateField(
                            '${field.id}',
                            'termsDocType',
                            'pdf'
                        );
                        renderProps();
                    "
                >
                    Upload PDF
                </div>

            </div>

            ${docType === 'link'
                ? `

                        <label class="form-label">
                            Terms Link
                        </label>

                        <input
                            class="form-control mb-2"
                            placeholder="https://example.com/terms"
                            value="${escapeAttribute(
                    field.termsLinkUrl ||
                    ''
                )
                }"
                            onchange="
                                updateField(
                                    '${field.id}',
                                    'termsLinkUrl',
                                    this.value
                                )
                            "
                        />

                        <p
                            class="
                                text-muted
                                small
                                mb-2
                            "
                        >
                            Users will see a link
                            that opens this page
                            in a new tab.
                        </p>
                    `

                : `

                        <label class="form-label">
                            Terms PDF
                        </label>

                        <input
                            type="file"
                            accept="application/pdf"
                            class="form-control mb-1"
                            id="termsPdfFile_${field.id}"
                            onchange="
                                uploadTermsPdf(
                                    '${field.id}'
                                )
                            "
                        />

                        ${field.termsPdfUrl

                    ? `

                                    <div
                                        class="
                                            d-flex
                                            align-items-center
                                            gap-2
                                            mb-2
                                        "
                                    >

                                        <a
                                            href="${field.termsPdfUrl
                    }"
                                            target="_blank"
                                            class="small"
                                        >
                                            📄 View uploaded PDF
                                        </a>

                                        <button
                                            type="button"
                                            class="
                                                zfb-btn
                                                zfb-btn-ghost
                                            "
                                            style="
                                                color:var(--zc-danger);
                                                border-color:var(--zc-danger);
                                            "
                                            onclick="
                                                updateField(
                                                    '${field.id}',
                                                    'termsPdfUrl',
                                                    ''
                                                );
                                                renderProps();
                                            "
                                        >
                                            Remove
                                        </button>

                                    </div>
                                `

                    : `
                                    <p
                                        class="
                                            text-muted
                                            small
                                            mb-2
                                        "
                                    >
                                        No PDF uploaded yet.
                                    </p>
                                `
                }
                    `
            }

            <div class="form-check">

                <input
                    type="checkbox"
                    class="form-check-input"
                    id="reqChk"
                    ${field.required
                ? 'checked'
                : ''
            }
                    onchange="
                        updateField(
                            '${field.id}',
                            'required',
                            this.checked
                        )
                    "
                />

                <label
                    class="form-check-label"
                    for="reqChk"
                >
                    Required
                </label>

            </div>

            ${fieldWidthToggleHtml(field)}
        `;

        return;
    }

    // ========================================================
    // CAPTCHA
    // ========================================================

    if (field.type === 'captcha') {

        const captchaOptions = [
            {
                id: 'recaptcha',
                label: 'reCAPTCHA',
                icon: 'icon-shield-check'
            },
            {
                id: 'jotcaptcha',
                label: 'Image Captcha',
                icon: 'icon-text-cursor-input'
            }
        ];

        const alignment =
            field.labelAlignment ||
            'center';

        panel.innerHTML = `

            <label class="form-label">
                Field Label
            </label>

            <input
                class="form-control mb-2"
                value="${escapeAttribute(field.label)}"
                onchange="
                    updateField(
                        '${field.id}',
                        'label',
                        this.value
                    )
                "
            />

            <label class="form-label">
                Label Alignment
            </label>

            <div class="zfb-fieldalign-toggle">

                <div
                    class="
                        zfb-fieldalign-btn
                        ${alignment === 'left'
                ? 'active'
                : ''
            }
                    "
                    onclick="
                        updateField(
                            '${field.id}',
                            'labelAlignment',
                            'left'
                        );
                        renderProps();
                    "
                >
                    LEFT
                </div>

                <div
                    class="
                        zfb-fieldalign-btn
                        ${alignment === 'center'
                ? 'active'
                : ''
            }
                    "
                    onclick="
                        updateField(
                            '${field.id}',
                            'labelAlignment',
                            'center'
                        );
                        renderProps();
                    "
                >
                    CENTER
                </div>

                <div
                    class="
                        zfb-fieldalign-btn
                        ${alignment === 'right'
                ? 'active'
                : ''
            }
                    "
                    onclick="
                        updateField(
                            '${field.id}',
                            'labelAlignment',
                            'right'
                        );
                        renderProps();
                    "
                >
                    RIGHT
                </div>

            </div>

            <label class="form-label">
                Captcha Type
            </label>

            <div class="d-flex gap-2 mb-2">

                ${captchaOptions
                .map(
                    o => `

                                <div
                                    class="
                                        zfb-captcha-swatch
                                        ${field.captchaType === o.id
                            ? 'active'
                            : ''
                        }
                                    "
                                    onclick="
                                        updateField(
                                            '${field.id}',
                                            'captchaType',
                                            '${o.id}'
                                        );
                                        renderProps();
                                    "
                                >

                                    <div
                                        style="
                                            font-size:18px;
                                        "
                                    >
                                        <i
                                            class="${o.icon}"
                                        ></i>
                                    </div>

                                    <div
                                        style="
                                            font-size:11px;
                                            font-weight:600;
                                            margin-top:4px;
                                        "
                                    >
                                        ${o.label}
                                    </div>

                                </div>
                            `
                )
                .join('')
            }

            </div>

            ${field.captchaType ===
                'jotcaptcha'

                ? `

                        <p
                            class="
                                text-muted
                                small
                                mb-2
                            "
                        >
                            Image Captcha needs
                            no site key — it shows
                            a distorted-text image
                            that the visitor has
                            to read and type in.
                        </p>
                    `

                : `

                        <label class="form-label">
                            reCAPTCHA Site Key
                        </label>

                        <input
                            class="form-control mb-1"
                            placeholder="
                                Paste your
                                reCAPTCHA site key
                            "
                            value="${escapeAttribute(
                    field.captchaSiteKey ||
                    ''
                )
                }"
                            onchange="
                                updateField(
                                    '${field.id}',
                                    'captchaSiteKey',
                                    this.value
                                )
                            "
                        />

                        <p
                            class="
                                text-muted
                                small
                                mb-2
                            "
                        >
                            ${field.captchaSiteKey
                    ? '✅ Site key set'
                    : '⚠️ No site key set'
                }
                        </p>
                    `
            }
        `;

        return;
    }

    // ========================================================
    // BORDER LABEL
    // ========================================================

    if (field.type === 'borderlabel') {

        const showLabel = field.showLabel !== false;

        const borderColor =
            field.borderColor || '#d7dce2';

        const borderWidth =
            (typeof field.borderWidth === 'number')
                ? field.borderWidth
                : 1;

        panel.innerHTML = `

            <div class="form-check mb-2">
                <input
                    type="checkbox"
                    class="form-check-input"
                    id="borderShowLabelChk"
                    ${showLabel ? 'checked' : ''}
                    onchange="updateField('${field.id}', 'showLabel', this.checked)"
                />
                <label class="form-check-label" for="borderShowLabelChk">
                    Show label text
                </label>
            </div>

            ${showLabel ? `
                <label class="form-label">Label</label>
                <input
                    class="form-control mb-2"
                    value="${escapeAttribute(field.label)}"
                    onchange="updateField('${field.id}', 'label', this.value)"
                />
            ` : `
                <p class="text-muted small mb-2">Renders as a plain divider line, no text.</p>
            `}

            <label class="form-label">
                Border Color
            </label>

            <div class="d-flex gap-2 align-items-center mb-1">

                <input
                    type="color"
                    class="form-control form-control-color"
                    style="width:44px;"
                    value="${borderColor}"
                    onchange="
                        updateField(
                            '${field.id}',
                            'borderColor',
                            this.value
                        );
                        renderProps();
                    "
                />

                <input
                    type="text"
                    class="form-control form-control-sm"
                    value="${borderColor}"
                    onchange="
                        updateField(
                            '${field.id}',
                            'borderColor',
                            this.value
                        );
                        renderProps();
                    "
                />

            </div>

            <p class="text-muted small mb-2">
                Change the divider line color
            </p>

            <label class="form-label">
                Border Size (px)
            </label>

            <div class="d-flex gap-2 align-items-center mb-2">

                <button
                    type="button"
                    class="zfb-btn zfb-btn-ghost"
                    style="width:36px;padding:4px 0;font-weight:700;"
                    onclick="
                        updateField(
                            '${field.id}',
                            'borderWidth',
                            Math.max(1, ${borderWidth} - 1)
                        );
                        renderProps();
                    "
                >
                    −
                </button>

                <input
                    type="number"
                    class="form-control form-control-sm text-center"
                    style="width:64px;"
                    min="1"
                    max="20"
                    value="${borderWidth}"
                    onchange="
                        updateField(
                            '${field.id}',
                            'borderWidth',
                            Math.min(20, Math.max(1, parseInt(this.value) || 1))
                        );
                        renderProps();
                    "
                />

                <button
                    type="button"
                    class="zfb-btn zfb-btn-ghost"
                    style="width:36px;padding:4px 0;font-weight:700;"
                    onclick="
                        updateField(
                            '${field.id}',
                            'borderWidth',
                            Math.min(20, ${borderWidth} + 1)
                        );
                        renderProps();
                    "
                >
                    +
                </button>

            </div>

            ${fieldWidthToggleHtml(field)}
        `;

        return;
    }

    // ========================================================
    // EVERYTHING ELSE
    // ========================================================

    let extraHtml =
        '';

    if (
        field.type === 'dropdown' ||
        field.type === 'radio' ||
        field.type === 'checkbox'
    ) {

        extraHtml = `

            <label class="form-label mt-2">
                Options (one per line)
            </label>

            <textarea
                class="form-control"
                rows="4"
                onchange="
                    updateOptions(
                        '${field.id}',
                        this.value
                    )
                "
            >${escapeHtml(
            field.options.join('\n')
        )}</textarea>
        `;

    } else if (
        field.type === 'starrating'
    ) {

        extraHtml = `

            <label class="form-label mt-2">
                Max Stars
            </label>

            <input
                type="number"
                class="form-control"
                min="3"
                max="10"
                value="${field.maxStars || 5
            }"
                onchange="
                    updateField(
                        '${field.id}',
                        'maxStars',
                        parseInt(this.value)
                    )
                "
            />
        `;
    }

    const skipLabelEdit =
        field.type === 'paragraph';

    panel.innerHTML = `

        <label class="form-label">
            ${skipLabelEdit
            ? 'Text'
            : 'Label'
        }
        </label>

        <input
            class="form-control mb-2"
            value="${escapeAttribute(field.label)}"
            onchange="
                updateField(
                    '${field.id}',
                    'label',
                    this.value
                )
            "
        />

        ${!skipLabelEdit
            ? `

                    <label class="form-label">
                        Placeholder
                    </label>

                    <input
                        class="form-control mb-2"
                        value="${escapeAttribute(
                field.placeholder
            )
            }"
                        onchange="
                            updateField(
                                '${field.id}',
                                'placeholder',
                                this.value
                            )
                        "
                    />

                    <div class="form-check">

                        <input
                            type="checkbox"
                            class="form-check-input"
                            id="reqChk"
                            ${field.required
                ? 'checked'
                : ''
            }
                            onchange="
                                updateField(
                                    '${field.id}',
                                    'required',
                                    this.checked
                                )
                            "
                        />

                        <label
                            class="form-check-label"
                            for="reqChk"
                        >
                            Required
                        </label>

                    </div>

                    ${fieldWidthToggleHtml(field)}
                `
            : ''
        }

        ${extraHtml}
    `;
}

// ============================================================
// PAYMENT PRODUCT LIST
// ============================================================

function renderPaymentProductListHtml(
    field
) {

    if (!Array.isArray(field.products)) {
        field.products = [];
    }

    const rows =
        field.products
            .map(
                (p, idx) => `

                    <div
                        class="zfb-product-row"
                        data-product-row="${idx}"
                    >

                        <input
                            type="text"
                            class="
                                form-control
                                form-control-sm
                            "
                            placeholder="Product name"
                            value="${escapeAttribute(
                    p.name || ''
                )
                    }"
                            onchange="
                                updatePaymentProduct(
                                    '${field.id}',
                                    ${idx},
                                    'name',
                                    this.value
                                )
                            "
                        />

                        <input
                            type="number"
                            step="0.01"
                            min="0"
                            class="
                                form-control
                                form-control-sm
                            "
                            style="
                                max-width:100px;
                            "
                            placeholder="Price"
                            value="${p.price ?? ''
                    }"
                            onchange="
                                updatePaymentProduct(
                                    '${field.id}',
                                    ${idx},
                                    'price',
                                    parseFloat(
                                        this.value
                                    ) || 0
                                )
                            "
                        />

                        <button
                            type="button"
                            class="
                                zfb-icon-btn
                                zfb-icon-delete
                            "
                            style="
                                position:static;
                            "
                            onclick="
                                removePaymentProduct(
                                    '${field.id}',
                                    ${idx}
                                )
                            "
                            title="Remove"
                        >
                            <i class="icon-x"></i>
                        </button>

                    </div>
                `
            )
            .join('');

    return `

        <label class="form-label">
            Product List
        </label>

        <div
            id="paymentProductRows_${field.id}"
        >

            ${rows ||
        `
                    <p class="
                        text-muted
                        small
                    ">
                        No products added yet.
                    </p>
                `
        }

        </div>

        <button
            type="button"
            class="
                zfb-btn
                zfb-btn-ghost
                w-100
                mt-1
            "
            onclick="
                addPaymentProduct(
                    '${field.id}'
                )
            "
        >
            <i class="icon-plus"></i>
            Add Product
        </button>
    `;
}

function addPaymentProduct(
    fieldId
) {

    const field =
        formFields.find(
            f => f.id === fieldId
        );

    if (!field) return;

    if (!Array.isArray(field.products)) {
        field.products = [];
    }

    field.products.push({
        name: '',
        price: 0
    });

    renderProps();
    renderCanvas();
}

function updatePaymentProduct(
    fieldId,
    idx,
    key,
    value
) {

    const field =
        formFields.find(
            f => f.id === fieldId
        );

    if (
        !field ||
        !field.products ||
        !field.products[idx]
    ) {
        return;
    }

    field.products[idx][key] =
        value;

    renderCanvas();
}

function removePaymentProduct(
    fieldId,
    idx
) {

    const field =
        formFields.find(
            f => f.id === fieldId
        );

    if (
        !field ||
        !field.products
    ) {
        return;
    }

    field.products.splice(
        idx,
        1
    );

    renderProps();
    renderCanvas();
}

// ============================================================
// UPDATE FIELD
// ============================================================

function updateField(
    id,
    key,
    value
) {

    const field =
        formFields.find(
            f => f.id === id
        );

    if (!field) return;

    field[key] =
        value;

    // Keep the hidden #formTitle payload input and top-nav breadcrumb
    // synced whenever the admin edits the title field's label.
    if (field.isFormTitle && key === 'label') {
        syncTitlePayloadFromField();
    }

    renderCanvas();
}

// ============================================================
// UPDATE OPTIONS
// ============================================================

function updateOptions(
    id,
    text
) {

    const field =
        formFields.find(
            f => f.id === id
        );

    if (!field) return;

    field.options =
        text
            .split('\n')
            .map(
                o => o.trim()
            )
            .filter(
                o => o
            );

    renderCanvas();
}

// ============================================================
// SAVE FORM
// ============================================================

function saveForm() {

    const paymentFieldMissingKey =
        formFields.find(
            f =>
                f.type === 'payment' &&
                !f.publicKey
        );

    if (paymentFieldMissingKey) {

        const gwName =
            {
                paypal: 'PayPal',
                stripe: 'Stripe',
                square: 'Square'
            }[
            paymentFieldMissingKey.gateway
            ] ||
            paymentFieldMissingKey.gateway;

        selectField(
            paymentFieldMissingKey.id
        );

        alert(
            'Please enter a ' +
            gwName +
            ' Client ID / Public Key in the Payment field properties before saving.'
        );

        return;
    }

    const titleEl =
        getEl('formTitle');

    const logoUrlEl =
        getEl('logoUrl');

    const payload = {

        formId:
            currentFormId,

        title:
            titleEl?.value ||
            'Untitled Form',

        schema:
            JSON.stringify(
                formFields
            ),

        logoUrl:
            logoUrlEl?.value ||
            null,

        logoWidth:
            logoWidth,

        logoHeight:
            logoHeight,

        design:
            JSON.stringify(
                formDesign
            )
    };

    fetch(
        '/Admin/Form/SaveFormSchema',
        {
            method: 'POST',

            headers: {
                'Content-Type':
                    'application/json'
            },

            body:
                JSON.stringify(
                    payload
                )
        }
    )
        .then(
            res => res.json()
        )
        .then(
            data => {

                if (data.success) {

                    currentFormId =
                        data.formId;

                    alert(
                        'Form saved! FormId: ' +
                        data.formId
                    );

                } else {

                    alert(
                        'Save failed: ' +
                        data.message
                    );
                }
            }
        )
        .catch(
            err => {

                alert(
                    'Error: ' +
                    err
                );
            }
        );
}
// ============================================================
// FORM PREVIEW
// ============================================================

function openFormPreview() {

    const modal = getEl('formPreviewModal');
    const body = getEl('formPreviewBody');
    const canvas = getEl('canvas');

    if (!modal || !body || !canvas) {
        return;
    }

    const title =
        getEl('formTitle')?.value?.trim() ||
        'Form Preview';

    const previewTitle =
        getEl('formPreviewTitle');

    if (previewTitle) {
        previewTitle.textContent = title;
    }

    // IMPORTANT:
    // Re-apply the current (unsaved) design to the canvas first, so the
    // clone below picks up every change the user has just made —
    // font family, size, weight, colors, spacing, width, alignment —
    // without needing to save.
    loadGoogleFont(formDesign.fontFamily);
    applyDesignToCanvas();

    // Clone the builder canvas
    const clone = canvas.cloneNode(true);

    // IMPORTANT:
    // Keep the canvas layout class.
    // We DON'T remove zfb-canvas because it controls
    // the field/grid layout.
    clone.removeAttribute('id');

    // Add a preview-specific class
    clone.classList.add('zfb-preview-canvas');

    // Remove editor controls
    clone
        .querySelectorAll(`
            .zfb-side-icons,
            .zfb-field-actions,
            .zfb-field-drag-handle,
            .zfb-field-delete
        `)
        .forEach(el => el.remove());

    // Remove empty hint
    clone
        .querySelectorAll('.zfb-empty-hint')
        .forEach(el => el.remove());

    // Remove selected state
    clone
        .querySelectorAll('.zfb-selected')
        .forEach(el => {
            el.classList.remove('zfb-selected');
        });

    // Enable form controls
    clone
        .querySelectorAll(
            'input, textarea, select, button'
        )
        .forEach(el => {

            if (
                el.type !== 'button' ||
                el.classList.contains('zfb-submit-btn')
            ) {
                el.disabled = false;
            }

            el.removeAttribute('onclick');
            el.removeAttribute('onchange');
            el.removeAttribute('oninput');
        });

    // Create preview shell
    const shell =
        document.createElement('div');

    shell.className =
        'zfb-preview-form-shell';

    // Mirror the live design onto the preview shell so the modal shows
    // exactly what the published form will look like.
    const previewFontStack =
        zfbFontStack(formDesign.fontFamily);

    shell.style.fontFamily =
        previewFontStack;

    shell.style.fontSize =
        (formDesign.fontSize || 14) + 'px';

    shell.style.fontWeight =
        formDesign.fontWeight || 'normal';

    shell.style.color =
        formDesign.fontColor || '#33475b';

    clone.style.fontFamily =
        previewFontStack;

    // Every cloned label / input / control inherits the chosen font too
    clone
        .querySelectorAll(
            'label, input, textarea, select, button, h1, h2, h3, h4, h5, h6, p, span, div'
        )
        .forEach(el => {

            // Per-field heading fonts stay untouched
            if (
                el.style.fontFamily &&
                el.closest('.zfb-heading-item')
            ) {
                return;
            }

            el.style.fontFamily =
                previewFontStack;
        });

    // Preview page background
    if (formDesign.pageImageUrl) {

        body.style.background =
            `url('${formDesign.pageImageUrl}') center / cover no-repeat`;

    } else if (formDesign.pageColor) {

        body.style.background =
            formDesign.pageColor;
    }

    shell.appendChild(clone);

    // Replace preview content
    body.replaceChildren(shell);

    // Open modal
    modal.classList.add('is-open');

    modal.setAttribute(
        'aria-hidden',
        'false'
    );

    document.body.classList.add(
        'zfb-preview-open'
    );
}


// ============================================================
// CLOSE FORM PREVIEW
// ============================================================

function closeFormPreview() {

    const modal =
        getEl('formPreviewModal');

    if (!modal) return;

    modal.classList.remove(
        'is-open'
    );

    modal.setAttribute(
        'aria-hidden',
        'true'
    );

    document.body.classList.remove(
        'zfb-preview-open'
    );
}
// ============================================================
// UTILITY: ESCAPE HTML
// ============================================================

function escapeHtml(value) {

    if (
        value === null ||
        value === undefined
    ) {
        return '';
    }

    return String(value)
        .replace(
            /&/g,
            '&amp;'
        )
        .replace(
            /</g,
            '&lt;'
        )
        .replace(
            />/g,
            '&gt;'
        )
        .replace(
            /"/g,
            '&quot;'
        )
        .replace(
            /'/g,
            '&#039;'
        );
}

function escapeAttribute(value) {

    return escapeHtml(
        value
    );
}

// Note: the old click-to-edit title-input wiring that used to live here was
// removed — the form title is now formFields[0] (a normal heading field,
// see ensureTitleField()/syncTitlePayloadFromField() near the top of this
// file), edited the same way as any other heading via its Customize icon.