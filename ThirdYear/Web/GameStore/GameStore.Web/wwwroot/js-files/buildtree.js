let buildNode = function bild(node, previoustext, isBanned) {
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

    divCard.appendChild(divCardHeader);
    divCard.appendChild(divCardBody);

    let divRow = CreateDivRow();
    let divCol1 = CreateDivCol();
    let divCol2 = CreateDivCol();

    if (!isBanned) {
        let aButton = CreateAnswerButton(node);
        let aQuote = CreateQuoteButton(node);
        divCol1.appendChild(aButton);
        divCol2.appendChild(aQuote);
    }

    divRow.appendChild(divCol1);
    divRow.appendChild(divCol2);
    divCard.appendChild(divRow);

    li.appendChild(divCard);

    if (node.comments) {
        node.comments.forEach(comment => {
            let ul = document.createElement('ul');
            ul.appendChild(bild(comment, node.isDeleted ? textForDeletedComment : node.body, isBanned))
            li.appendChild(ul);
        });
        return li;
    }
    else {
        return li;
    }
}

function buildTree(model, isBanned) {
    model.forEach(x => { document.querySelector('.tree').appendChild(buildNode(x, null, isBanned)); });
}