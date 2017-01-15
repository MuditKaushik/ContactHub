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
    let errorMessageId = $("#contactStatus");
    let contactId = $(this).val();
    errorMessageId.html("");
    DA.GetContactById(contactId)
        .done(function(data){
            targetId.html(data);
        })
        .fail(function(err){
            let errorMessage = Util.CreateAlertMessage(Util.MessageType().Error,Util.Messages().ServerError,true);
            errorMessageId.html(errorMessage);
        });
});

$(document).on("click","#deleteContact",function(){
    let contactId = $(this).val();
    let statusTarget = $("#contactStatus");
    DA.RemoveContactById(contactId)
    .done(function(data){
        if(data.result){
            let successMessage = Util.CreateAlertMessage(Util.MessageType().Success,Util.Messages().ContactRemoveSuccess,false);
            statusTarget.html(successMessage);
            $("#contactDetail").html("");
            $("#contactList").html(data.newList);
        }
        return false;
    })
    .fail(function(err){
        let errorMessage = Util.CreateAlertMessage(Util.MessageType().Error,Util.Messages().ContactRemoveFailure,true);
        statusTarget.html(errorMessage);
    });
});