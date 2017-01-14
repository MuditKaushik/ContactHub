import {Utility} from "./Util.js";
class DataAccess extends Utility {
    constructor(){
        super();
    }

    GetContryListXml(countryListId) {
        $.when(this.jQueryAjaxCall("get","/Home/GetCountryListXml",null))
            .then((data)=>{
                for(let item of data.Countries){
                    $("#"+ countryListId).append($('<option></option>').attr("value",item.Value).text(item.Text));
                }
                return false;
            })
        .catch((err)=>{
            console.log(err.statusText);
        });
    }

    GetContactDetails(){
        return $.when(this.jQueryAjaxCall("Get","../CommonData/Files/datas.json",null))
       .then((response)=>{return response})
       .catch((err)=>{return err});
    }

    GetContactById(contactId){
        return $.when(this.jQueryAjaxCall("Get","/User/GetContactById",{Id:contactId}))
        .then((response)=>{return response})
        .catch((err)=>{return err});
    }
}
export{DataAccess}