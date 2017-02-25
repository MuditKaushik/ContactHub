import {DataAccess} from "./DataAccess.js";
let DA = new DataAccess();
DA.GetContryListXml()
    .done(function(data){
        for(let item of data){
            $("#contryList").append($('<option></option>').attr("value",item.Value).text(item.Text));
        }
        return;
    })
    .fail(function(err){
        console.log(err);
    });