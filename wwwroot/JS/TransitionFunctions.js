// Called from blazor
function startTransition(){
    const transitionWrapper = document.querySelector('#transition-wrapper');
    if (!transitionWrapper) return;
    
    transitionWrapper.style.opacity = 1;
}

function exitTransition(){
    const transitionWrapper = document.querySelector('#transition-wrapper');
    if (!transitionWrapper) return;
    
    transitionWrapper.style.opacity = 0;
}

document.body.addEventListener('click', function(e) {
    if (e.target.tagName.toLowerCase() === 'a') {
        e.preventDefault()
        const href = e.target.href
        
        const transitionWrapper = document.querySelector('#transition-wrapper');
        if (!transitionWrapper) {
            window.location.href = href;
 
        } else {
            exitTransition();
            
            transitionWrapper.addEventListener("transitionend", () => {
                window.location.href = href
            })
        }
    }
})

// const observeUrlChange = () => {
//     let oldHref = document.location.href;
//     const body = document.querySelector("body");
//     const observer = new MutationObserver(mutations => {
//         if (oldHref !== document.location.href) {
//             oldHref = document.location.href;
//            
//             startTransition();
//         }
//     });
//     observer.observe(body, { childList: true, subtree: true });
// };
//
// window.onload = observeUrlChange;

