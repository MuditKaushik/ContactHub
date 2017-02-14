import {DataAccess} from "./DataAccess.js";
import {ValidationUtil} from './validationUtil.js';

let DA = new DataAccess();
let Util = new ValidationUtil();

DA.GetAccountReasons()
    .done((data)=>{
        let targetId = $("#reason");
        $.each(data.Reason,function(key,val){
            $.each(val,function(k,v){
                targetId.append($("<option></option>").text(k).val(v));
            });
        });
    })
    .fail((err)=>{console.log(err)});

$(document).on("change","#reason",function(){
    let value = $(this).val();
    if(value.toString() === "4"){
        Util.ToggleHideShowElementById("others",true);
    }else{
        Util.ToggleHideShowElementById("others",false);
    }
    return;
});