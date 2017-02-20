import {ValidationUtil} from "./validationUtil.js";
let Util = new ValidationUtil();
let fileNames = new Array();
$(".datepicker").datepicker(Util.ShowCalender());
$(document).on("change","#uploadFiles",function(){
    let hasFiles = false;
    let validDisplayTarget = $("#validFileList");
    let invalidDisplayTarget = $("#invalidFileList");
    let fileUploads = $(this);
    $.each(fileUploads,function(key,val){
        $.each(val.files,function(k,v){
            hasFiles = true;
            if(fileNames.length < 4 && !Util.IsDuplicate(v.name,fileNames) && v.type === Util.FileContent().PDF){
                Util.ToggleHideShowElementById("validFileHeader",true);
                fileNames.push(v.name);
                let list = Util.CreateList(v.name,Util.MessageType().Error,true);
                $("#fileNames").val(fileNames);
                validDisplayTarget.append(list);
            }else{
                Util.ToggleHideShowElementById("invalidFileHeader",true);
                let list = Util.CreateList(v.name,Util.MessageType().Error,false);
                invalidDisplayTarget.append(list);
            }
        });
    });
});

$(document).on("click","#deleteFile",function(){
    let deleteButton = $(this);
    let fileIndex = fileNames.indexOf(deleteButton.val());
    if(fileIndex > -1){
        fileNames.splice(fileIndex,1);
        $("#fileNames").val(fileNames);
    }
    deleteButton.closest("li").remove();
    if(!Util.ArrayListHasElement(fileNames)){
        Util.ToggleHideShowElementById("validFileHeader",false);
        $("#uploadFiles").val("");
    }
    return;
});

$(document).on("click","#clearInvalid",function(){
    let element = $("#invalidFileList");
    $.each(element.children(),function(key,val){
        $(val).remove();
    });
    Util.ToggleHideShowElementById("invalidFileHeader",false);
    return;
});