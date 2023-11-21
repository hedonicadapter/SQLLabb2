export function beforeStart(options, extensions) {
    var customScript = document.createElement('script');
    customScript.setAttribute('src', '~/JS/TransitionFunctions.js');
    document.head.appendChild(customScript);
}