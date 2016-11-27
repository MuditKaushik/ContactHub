import {Utility} from "./Util.js";
import {ValidationUtil} from "./validationUtil.js";
let utilClass = new Utility("contryList");
let validation = new ValidationUtil();
utilClass.GetContryListXml();
$("#save").click(function(){
    if(!validation.ValidateTextBoxById("FirstName")){
        validation.ShowErrorMessage("firstname_error","required feild");
        $("#FirstName").focus();

    }else{
        validation.RemoveErrorMessage("firstname_error");
    }
    return;
});