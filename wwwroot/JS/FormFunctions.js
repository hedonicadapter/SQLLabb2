function scrollToForm(){
    document.querySelector("#new-book-section")?.scrollIntoView({block: "start", inline: "nearest", behavior:"smooth"});
}
function getSelectedAuthorId(){
    const datalist = document.getElementById('datalistOptions');
    const selectedAuthor = document.getElementById('datalistInput').value;
    if (!datalist || !selectedAuthor) return;

    const optionsArray = Array.from(datalist.options);

    const selectedAuthorId = optionsArray.find(selectedOption=>selectedOption.value === selectedAuthor)?.getAttribute('data-authorid');

    return +selectedAuthorId;
}
