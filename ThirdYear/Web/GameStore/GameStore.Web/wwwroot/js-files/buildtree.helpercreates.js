const textForDeletedComment = "Comment was deleted by the moderator";

function CreateDivCard() {
    let divCard = document.createElement('div');
    divCard.className = 'card'
    divCard.style = 'width: 18rem;';
    return divCard;
}


function CreateDivCardHeader(node) {
    let divCardHeader = document.createElement('div');
    divCardHeader.className = 'card-header';
    divCardHeader.textContent = node.name;
    return divCardHeader;
}

function CreateDivCardBody() {
    let divCardBody = document.createElement('div');
    divCardBody.className = 'card-body';
    return divCardBody;

}

function CreateQuoteText(previoustext) {
    let em = document.createElement('em');
    em.className = 'bg-light';
    em.textContent = `"${previoustext}"`;
    return em;
}

function CreateCommentText(node) {
    let par = document.createElement('p');
    par.textContent = node.body;
    return par;
}

function CreateAnswerButton(node) {
    let aButton = document.createElement('a');
    aButton.href = "#ParentCommentBody";
    aButton.style = 'width: 8rem;';
    aButton.className = 'btn btn-primary';
    aButton.addEventListener("click", function () {
        document.getElementById('ParentCommentId').value = node.id;
        document.getElementById('ParentCommentName').textContent = node.name;
        document.getElementById('ParentCommentBody').value = "";
    });
    aButton.textContent = 'Answer';
    return aButton;
}

function CreateQuoteButton(node) {
    let aQuote = document.createElement('a');
    aQuote.href = "#ParentCommentBody";
    aQuote.style = 'width: 8rem;';
    aQuote.className = 'btn btn-primary';
    aQuote.addEventListener("click", function () {
        document.getElementById('ParentCommentId').value = node.id;
        document.getElementById('ParentCommentName').textContent = node.name;
        document.getElementById('ParentCommentBody').value = '<quote> ' + node.body + ' </quote>';
    });
    aQuote.textContent = 'Quote';
    return aQuote;
}

function CreateDivRow() {
    let divRow = document.createElement('div');
    divRow.className = 'row';
    return divRow;
}

function CreateDivCol() {
    let divCol = document.createElement('div');
    divCol.className = 'col-sm';
    return divCol;
}