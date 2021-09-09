let buildNode = function bild(gameKey, node, previoustext) {
    let li = document.createElement('li');
    let divCard = CreateDivCard();
    let divCardHeader = CreateDivCardHeader(node);
    let divCardBody = CreateDivCardBody();
    if (node.isDeleted) {
        divCardBody.textContent = textForDeletedComment;
    }
    else {
        if (node.isQuoted) {

            let em = CreateQuoteText(previoustext);
            divCardBody.appendChild(em);
        }
        let par = CreateCommentText(node);
        divCardBody.appendChild(par);
    }

    let divRow = CreateDivRow();
    let divCol1 = CreateDivCol();
    let divCol2 = CreateDivCol();

    let aButton = CreateAnswerButton(node);
    divCol1.appendChild(aButton);
    let formDelete = CreateFormDeleteComment(gameKey, node);


    let divRow1 = CreateDivRow();
    let divCol11 = CreateDivCol();
    let divCol12 = CreateDivCol();


    let aPopup = CreateDeleteButtonPopup(node);
    divCol11.appendChild(aPopup);
    let divPopup = CreateDivPopup(node);
    let divInnerPopup = CreateDivInnerPopup();
    let pPopup = CreateTextForPopup('Delete this comment?');
    let inputYes = CreateYesButtonPopup(node);
    let inputNo = CreateNoButtonPopup();
    let formBan = CreateFormBanUser(node);
    let inputBan = CreateBanButtom();
    divCol12.appendChild(inputBan);

    divRow1.appendChild(divCol11);
    divRow1.appendChild(divCol12);


    divInnerPopup.appendChild(pPopup);
    divInnerPopup.appendChild(inputYes);
    divInnerPopup.appendChild(inputNo);
    divPopup.appendChild(divInnerPopup);
    formDelete.appendChild(divRow1);
    formDelete.appendChild(divPopup);


    let aQuote = CreateQuoteButton(node);
    divCol2.appendChild(aQuote);
    divRow.appendChild(divCol1);
    divRow.appendChild(divCol2);

    divCard.appendChild(divCardHeader);
    divCard.appendChild(divCardBody);
    divCard.appendChild(divRow);
    divCard.appendChild(formDelete);
    divCard.appendChild(formBan);

    li.appendChild(divCard);

    if (node.comments) {
        node.comments.forEach(comment => {
            let ul = document.createElement('ul');
            ul.appendChild(bild(gameKey, comment, node.isDeleted ? textForDeletedComment : node.body))
            li.appendChild(ul);
        });
        return li;
    }
    else {
        return li;
    }
}

function buildTreeForAdmin(gameKey, model) {
    model.forEach(x => { document.querySelector('.tree').appendChild(buildNode(gameKey, x, null)); });
}
