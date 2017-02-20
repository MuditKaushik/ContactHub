class ValidationUtil{
    constructor(){}
    ValidateRadioAndCheckBoxByClassName(className){
        let valid = false;
        let elements = $("."+className);
        for(let item of elements){
            var isChecked = $(item).is(":checked");
            if(isChecked){
                valid = true;
            }
        }
        return valid;
    }
    ValidateTextBoxById(Id){
        let valid = true;
        let element = $(`#${Id}`);
        let elementValue = element.val();
        if(elementValue ===""){
            valid = false;
        }
        return valid;
    }
    ValidateTextBoxesByIds(IdsInArray){ }
    ShowErrorMessage(elementID,errorMessage){
        let element = $("#"+elementID);
        element.closest(".form-group").addClass("has-error");
        element.css("color","#a94442").html(errorMessage);
        return;
    }
    RemoveErrorMessage(elementID){
        let element = $("#"+elementID);
        element.closest(".form-group").removeClass("has-error");
        element.html("");
        return;
    }
    RemoveAllErrorMessages(elementIdsInArray){
        for(let item of elementIdsInArray){
            $(item).closest(".form-group").removeClass("has-error");
            $(item).html("");
        }
        return;
    }

    HighlightUserLink(pageName){
        this.RemoveHighlightUserLink();
        $("."+pageName.toLowerCase()).closest("li").addClass("active");
        return;
    }

    RemoveHighlightUserLink() {
        let allUserLinks = $(".userLink");
        allUserLinks.each(function(key,val){
            var element = $(val).closest("li");
            var isActive = element.hasClass("active");
            if(isActive){
                element.removeClass("active");
            }
        });
        return;
    };

    Messages(){
        let message = {
            ServerError:"Internal server error. Please try again.",
            ContactRemoveSuccess:"Contact removed successfully.",
            ContactRemoveFailure:"Not able to remove contact, Please try again.",
            ContactFetchFailure:"Not able to fetch contact, Please try again.",
            ContactsNotSelectedForRestore:"Please select contact.",
            ContactsNotSelectedForDownload:"Please select contact for download.",
            CountryDialCodeNotSeleted:"Please select dial code.",
            ContactModeNotSelected:"Please select reply mode.",
            ContactNumberNotEntered:"Please enter the phone number."
        };
        return message;
    }

    MessageType(){
        let Types = {
            Error:0,
            Success:1,
            Warning:2,
            Information:3
        };
        return Types;
    }

    FileType(){
        let Types = {
            PDF: 1,
            TXT: 2,
            CSV: 3
        };
        return Types;
    }

    FileExtension(){
        let extension = {
            Text:".txt",
            PDF:".pdf",
            CSV:".csv",
        };
        return extension;
    }

    FileContent(){
        let Content = {
            Image:"image/png",
            PDF:"application/pdf",
        };
        return Content;
    }

    CreateDismissButton(){
        let dismissButton = document.createElement("button");
        let span = document.createElement("span");

        dismissButton.setAttribute("type","button");
        dismissButton.setAttribute("data-dismiss","alert");
        dismissButton.setAttribute("aria-label","Close");
        dismissButton.classList.add("close");

        span.setAttribute("aria-hidden","true");
        span.innerHTML += "&times;"

        dismissButton.appendChild(span);
        return dismissButton;
    }

    CreateButton(value,Type){
        let button = document.createElement("button");
        button.setAttribute("id","deleteFile");
        button.setAttribute("value",value);
        button.setAttribute("type","button");
        button.classList.add("btn");
        switch(Type){
            case this.MessageType().Error: button.classList.add("btn-danger"); break;
            case this.MessageType().Success: button.classList.add("btn-success"); break;
        }
        button.classList.add("btn-xs");
        button.classList.add("pull-right");
        button.innerHTML = "Delete";
        return button;
    }

    CreateList(value,Type,NeedButton){
        let list = document.createElement("li");
        list.classList.add("list-group-item");
        list.innerHTML += value;
        if(NeedButton){
            list.appendChild(this.CreateButton(value,Type));
        }
        return list.outerHTML;
    }

    CreateAlertMessage(alertType,message,isDisimssable){
        let alertDiv = document.createElement("div");
        let alertSymbol = document.createElement("i");

        alertDiv.setAttribute("tabIndex","1");
        alertDiv.setAttribute("role","alert");
        alertSymbol.classList.add("fa");
        alertDiv.classList.add("alert");

        switch(alertType){
            case this.MessageType().Error: 
                alertSymbol.classList.add("fa-thumbs-o-down");
                alertDiv.classList.add("alert-danger");
                break;
            case this.MessageType().Success: 
                alertSymbol.classList.add("fa-thumbs-o-up");
                alertDiv.classList.add("alert-success");
                break;
            case this.MessageType().Warning:
                alertSymbol.classList.add("fa-hand-stop-o");
                alertDiv.classList.add("alert-warning");
                break;
            case this.MessageType().Information: 
                alertSymbol.classList.add("fa-info-circle");
                alertDiv.classList.add("alert-info");
                break;
        }
        if(isDisimssable){
            alertDiv.appendChild(this.CreateDismissButton());
        }
        alertDiv.appendChild(alertSymbol);
        alertDiv.innerHTML += ` ${message}`;
        return alertDiv.outerHTML;
    }

    IsDuplicate(value,ArrayList){
        let isDuplicate = false;
        if(ArrayList.length > 0){
            if(ArrayList.indexOf(value) !== -1){
                isDuplicate = true;
            }
        }
        return isDuplicate;
    }

    ArrayListHasElement(arrayList){
        let HasElement = false;
        if(arrayList.length > 0){
            HasElement = true
        }
        return HasElement;
    }

    ToggleHideShowElementById(elementId,visibility){
        let selector = $(`#${elementId}`);
        switch(visibility){
            case true: selector.removeClass("hide");break;
            case false: selector.addClass("hide");break;
        }
        return;
    }

    ToggleCheckBoxesByClassName(className,IsSelectAll){
        let selector = $(`.${className}`);
        $.each(selector,function(key,val){
            $(val).prop("checked",IsSelectAll);
        });
        return;
    }

    CreateForm(type,url,params){
        let form = document.createElement("form");
        form.setAttribute("method","");
        form.setAttribute("action","");
        return false;
    }

    ShowCalender(){
        let Options = {
            clearBtn:true,
            autoclose:true,
            format:'dd/mm/yyyy',
            showOnFocus:true
        };
        return Options;
    }

    CreateNode(nodeName,Id){
        let node = document.createElement(nodeName.toString());
        node.setAttribute(Id,`${Id}-error`);
        return node.outerHTML;
    }

    CustomValidation(elementId){
        let elementName = $(`#${elementId}`).attr("name");
        let errorElement = $(`#${elementId}[data-valmsg-for = ${elementName}]`);
        errorElement.append(this.CreateNode("span",elementId));
    }
}
export {ValidationUtil}