let deferredPrompt;
const ignorePwaKey = 'toast-ignore-install-pwa';
const installMessageForAppleDeviceKey = 'installMessageForAppleDevice';

function setIgnorePwaKeyOnLocalStorageOnIosDevice() {
    localStorage.setItem(ignorePwaKey, 'true')
    hideInstallPopupButton();
}
function hideInstallPopupButton() {
    document.getElementById(installMessageForAppleDeviceKey).style.display = 'none';
}

// Detects if device is on iOS 
const isIos = () => {
    const userAgent = window.navigator.userAgent.toLowerCase();
    return /iphone|ipad|ipod/.test(userAgent);
};
// Detects if device is in standalone mode
const isInStandaloneMode = () => ('standalone' in window.navigator) && (window.navigator['standalone']);

// Detect OS
function getOS() {
    var userAgent = window.navigator.userAgent,
        platform = window.navigator.platform,
        macosPlatforms = ['Macintosh', 'MacIntel', 'MacPPC', 'Mac68K'],
        windowsPlatforms = ['Win32', 'Win64', 'Windows', 'WinCE'],
        iosPlatforms = ['iPhone', 'iPad', 'iPod'],
        os = null;

    if (macosPlatforms.indexOf(platform) !== -1) {
        os = 'Mac OS';
    } else if (iosPlatforms.indexOf(platform) !== -1) {
        os = 'iOS';
    } else if (windowsPlatforms.indexOf(platform) !== -1) {
        os = 'Windows';
    } else if (/Android/.test(userAgent)) {
        os = 'Android';
    } else if (!os && /Linux/.test(platform)) {
        os = 'Linux';
    }

    return os;
}

// Checks if should display install popup notification on apple device:
document.addEventListener('DOMContentLoaded', (event) => {
    const installMessageForAppleDevice = document.getElementById(installMessageForAppleDeviceKey);
    if (isIos() && !isInStandaloneMode() && !localStorage.getItem(ignorePwaKey)) {
        installMessageForAppleDevice.style.display = 'block';
    }
});

window.addEventListener('beforeinstallprompt', (e) => {
    if (!localStorage.getItem(ignorePwaKey)) {
        // Prevent Chrome 67 and earlier from automatically showing the prompt
        e.preventDefault();
        // Stash the event so it can be triggered later.
        deferredPrompt = e;
        // Update UI to notify the user they can add to home screen
        Toastnotify.create({
            text: "Installa l'app sul tuo telefono!",
            type: 'dark',
            animationIn: 'fadeInBottom',
            animationOut: 'fadeOutBottom',
            callbackOk: () => {
                console.log('OK');
                // Show the prompt
                deferredPrompt.prompt();
                // Wait for the user to respond to the prompt
                deferredPrompt.userChoice.then((choiceResult) => {
                    if (choiceResult.outcome === 'accepted') {
                        console.log('User accepted the A2HS prompt');
                    } else {
                        console.log('User dismissed the A2HS prompt');
                    }
                    deferredPrompt = null;
                });

                // the number of .net ticks at the unix epoch
                let epochTicks = 621355968000000000;
                // there are 10000 .net ticks per millisecond
                let ticksPerMillisecond = 10000;
                let clientName = "public-client";
                
                // calculate the total number of .net ticks for your date
                let ticks = (epochTicks + ((new Date().getTime()) * ticksPerMillisecond)).toString();
                // calculate hash secret
                let secret = CryptoJS.HmacSHA512(ticks.concat(clientName), 'KeyValue').toString();

                const Http = new XMLHttpRequest();
                Http.open("POST", window.location.origin + '/api/Statistiche/InstallazioneApp/' + localStorage.getItem('device-guid'));
                // add custom header
                Http.setRequestHeader('Client-Secret', secret);
                Http.setRequestHeader('Client-Date', ticks);
                Http.setRequestHeader('Client-Name', clientName);
                Http.send();
            },
            callbackCancel: () => {
                localStorage.setItem(ignorePwaKey, 'true')
                console.log('Cancel');
            },
            callbackIgnore: () => {
                console.log('Ignore');
            }
        });
    }
});

window.addEventListener('appinstalled', (evt) => {
    let os = getOS();
    console.log(os);
    if ( os == 'Windows' || os == 'iOS') {
        return;
    }

    Toastnotify.create({
        text: "Installazione App in corso... Controlla il menù applicazioni del tuo dispositivo!",
        type: 'dark',
        animationIn: 'fadeInBottom',
        animationOut: 'fadeOutBottom',
        callbackOk: () => {
            console.log('OK');
            // window.open(window.location.origin, '_blank');
        }
    });
});