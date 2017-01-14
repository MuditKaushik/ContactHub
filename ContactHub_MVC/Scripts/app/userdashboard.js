import {DataAccess} from "./DataAccess.js";
import {ValidationUtil} from "./validationUtil.js";
let DA = new DataAccess();
let Util = new ValidationUtil();
DA.GetContactDetails()
    .done(function(data){
        $.each(data,function(key,val){
        });
    })
    .fail(function(err){
    });

function CreateRow(contactDetail,rowsNumber,columnsNumber){
    $.each(rowsNumber,function(key,val){
        let row = document.createElement("tr");
        row.classList.add("contactRow"+key);
        $.each(columnsNumber,function(){
            let columnFirst = document.createElement("td");
            let columnSecond = 
            columns.innerHTML = `${contactDetail.firstName} ${contactDetail.middleName} ${contactDetail.lastName}`; 
        });
    });
};

$(document).on("click","#editContact",function(){
    let targetId = $("#contactDetail");
    let errorMessageId = $("#contactError");
    let contactId = $(this).val();
    DA.GetContactById(contactId)
        .done(function(data){
            errorMessageId.html("");
            targetId.html(data);
        })
        .fail(function(err){
            let errorMessage = Util.CreateAlertMessage(Util.MessageType().Warning,Util.Messages().ServerError,false);
            errorMessageId.html(errorMessage);
        });
});