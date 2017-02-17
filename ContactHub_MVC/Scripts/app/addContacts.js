import {ValidationUtil} from "./validationUtil.js";
let Util = new ValidationUtil();
let fileNames = new Array();
$(".datepicker").datepicker(Util.ShowCalender());
$(document).on("change","#uploadFiles",function(){
    let hasFiles = false;
    let displayTarget = $("#fileList");
    let fileUploads = $(this);
    $.each(fileUploads,function(key,val){
        $.each(val.files,function(k,v){
            hasFiles = true;
            if(k<4 && !Util.IsDuplicate(v.name,fileNames)){
                fileNames.push(v.name);
                let list = Util.CreateList(v.name,Util.MessageType().Error);
                $("#fileNames").val(fileNames);
                displayTarget.append(list);
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
    return;
});