﻿import {DataAccess} from './DataAccess.js';
import {ValidationUtil} from './validationUtil.js';

let DA = new DataAccess();
let Util = new ValidationUtil();

function GetCheckBoxValue(){
    let contactIds = new Array();
    let className = $(".sync_contacts");
    $.each(className,function(key,val){
        let element = $(val);
        if(element.is(":checked")) {
            let value = element.val();
            if(contactIds.length > 0){
                if(contactIds.indexOf(value) === -1){
                    contactIds.push(value);
                }
            }else{
                contactIds.push(value);
            }
        }
    });
    return contactIds;
};

function GetFileType(fileType){
    let typeValue = 0;
    $.each(Util.FileType(),function(key,val){
        if(key.toLowerCase() == fileType){
            typeValue = val;
        }
    });
    return typeValue;
};

DA.GetCountryDialCodes()
    .done((data)=>{
        let targetId = $("#dialcode");
        $.each(data,function(key,val){
            if(val.Selected === true){
                targetId.append($("<option></option)").attr("value",val.Value).text(val.Text).prop("selected",true));
            }
            else{
                targetId.append($("<option></option)").attr("value",val.Value).text(val.Text));
            }
        });
    })
    .fail((err)=>{
        console.log(err);
    });

$(document).on("click","#show",function(){
    let contactId = $(this).val();
    let targetId = $("#showDetail");
    DA.GetContactById(contactId)
        .done((data)=>{
            Util.ToggleHideShowElementById("detailColumn",true);
            targetId.html(data);
        })
        .fail((err)=>{
            let errorMessage = Util.CreateAlertMessage(Util.MessageType().Error,Util.Messages().ContactFetchFailure,false);
            targetId.html(errorMessage).focus();
        });
});

$(document).on("click","#download",function(){
    let contactId = $(this).val();
    DA.DownloadContactDetails(contactId,Util.FileType().PDF)
    .done((data)=>{
        if(data.filename!==null){
            window.open(`${data.path}?fileName=${data.filename}`,"_blank");
        }
    })
    .fail((err)=>{
        console.log(err);
    });
});

$(document).on("click","#selectall",function(){
    Util.ToggleCheckBoxesByClassName("sync_contacts",true);
    $("#contactError").html("");
    return;
});

$(document).on("click","#selectnone",function(){
    Util.ToggleCheckBoxesByClassName("sync_contacts",false);
    return;
});

$(document).on("click","#downloadAll",function(){
    let contactIds = GetCheckBoxValue();
    $("#contactError").html("");
    if(contactIds.length > 0){
        let fileType = GetFileType($(this).attr("rel"));
        DA.DownloadContactDetails(contactIds,fileType)
            .done((data)=>{
                if(data.filename!==null){
                    window.open(`${data.path}?fileName=${data.filename}`,"_blank");
                }
            })
            .fail((err)=>{
                console.log(err);
            });
    }else{
        let errorMessage = Util.CreateAlertMessage(Util.MessageType().Error,Util.Messages().ContactsNotSelectedForDownload,true);
        $("#contactError").html(errorMessage).focus();
    }
    return;
});

$(document).on("click","#restore",function(){
    let contactIds = GetCheckBoxValue();
    let dialCode = Util.ValidateTextBoxById("dialcode");
    let contactMode = Util.ValidateTextBoxById("contactMode");
    let contactNumber = Util.ValidateTextBoxById("phoneNumber");
    let targetId=$("#status");
    let isFormValid = true;

    if(contactIds.length <= 0){
        isFormValid = false;
        Util.ToggleHideShowElementById("syncStatus",true);
        let errorMessage = Util.CreateAlertMessage(Util.MessageType().Error,Util.Messages().ContactsNotSelectedForRestore,true);
        targetId.html(errorMessage).focus();
    }else if(!dialCode){
        isFormValid = false;
        Util.ToggleHideShowElementById("syncStatus",true);
        let errorMessage = Util.CreateAlertMessage(Util.MessageType().Error,Util.Messages().CountryDialCodeNotSeleted,true);
        targetId.html(errorMessage).focus();
    }else if(!contactMode){
        isFormValid = false;
        Util.ToggleHideShowElementById("syncStatus",true);
        let errorMessage = Util.CreateAlertMessage(Util.MessageType().Error,Util.Messages().ContactModeNotSelected,true);
        targetId.html(errorMessage).focus();
    }else if(!contactNumber){
        isFormValid = false;
        Util.ToggleHideShowElementById("syncStatus",true);
        let errorMessage = Util.CreateAlertMessage(Util.MessageType().Error,Util.Messages().ContactNumberNotEntered,true);
        targetId.html(errorMessage).focus();
    }else{
        targetId.html("");
        Util.ToggleHideShowElementById("syncStatus",true);
    }

    if(isFormValid){
        let model = {
            PhoneNumber:$("#phoneNumber").val(),
            ContactMode:$("#contactMode").val(),
            DialCode:$("#dialcode").val(),
            ContactIds:contactIds
        };

        DA.SyncContacts(model)
            .done((data)=>{
                console.log(data);
                return data;
            })
            .fail((err)=>{return err});
    }
    return false;
});