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
        let element = $("#"+Id);
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
}
export {ValidationUtil}