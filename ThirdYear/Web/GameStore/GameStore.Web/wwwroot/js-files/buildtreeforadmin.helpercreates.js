function CreateFormDeleteComment(gameKey,node) {
    let formDelete = document.createElement('form');
    formDelete.name = 'formDelete';
    formDelete.method = 'POST';
    formDelete.action = `/game/${gameKey}/comment/delete/${node.id}`;
    formDelete.id = node.id;
    return formDelete;
}

function CreateFormBanUser(node) {
    let formBan = document.createElement('form');
    formBan.name = 'formBan';
    formBan.method = 'POST';
    formBan.action = `/ban/${node.userId}`;
    return formBan;
}

function CreateBanButtom() {
    let input = document.createElement('input');
    input.type = 'submit';
    input.style = 'width: 8rem;';
    input.className = 'btn btn-danger';
    input.value = 'Ban user';
    return input;
}

function CreateDeleteButtonPopup(node) {
    let aPopup = document.createElement('a');
    aPopup.style = 'width: 8rem;';
    aPopup.href = `javascript:PopUpShowById('${node.id}')`;
    aPopup.className = 'btn btn-danger';
    aPopup.textContent = 'Delete';
    return aPopup;
}

function CreateDivPopup(node) {
    let divPopup = document.createElement('div');
    divPopup.id = '#popup';
    divPopup.className = `b-popup popup-link text-center ${node.id}`;
    return divPopup;
}

function CreateDivInnerPopup() {
    let divInnerPopup = document.createElement('div');
    divInnerPopup.className = 'b-popup-content';
    return divInnerPopup;
}

function CreateTextForPopup(text) {
    let pPopup = document.createElement('p');
    pPopup.textContent = text;
    return pPopup;
}

function CreateYesButtonPopup(node) {
    let inputYes = document.createElement('input');
    inputYes.type = 'submit';
    inputYes.value = 'Yes'
    inputYes.className = 'btn btn-success w-50';
    inputYes.addEventListener("click", function (e) {
        e.preventDefault();
        PopUpHide();
        document.getElementById(node.id).submit();
    });
    return inputYes;
}

function CreateNoButtonPopup() {
    let inputNo = document.createElement('input');
    inputNo.type = 'submit';
    inputNo.value = 'No'
    inputNo.className = 'btn btn-danger w-50';
    inputNo.addEventListener("click", function (e) {
        e.preventDefault();
        PopUpHide();
    });
    return inputNo;
}
