import {Utility} from "./Util.js";
class DataAccess extends Utility {
    constructor(){
        super();
    }

    GetContryListXml() {
        return $.when(this.jQueryAjaxCall("get","/Home/GetCountryListXml",null))
            .then((data)=>{return data })
            .catch((err)=>{return err});
    }

    GetContactDetails(){
        return $.when(this.jQueryAjaxCall("Get","../CommonData/Files/data.json",null))
       .then((response)=>{return response})
       .catch((err)=>{return err});
    }

    GetContactById(contactId){
        return $.when(this.jQueryAjaxCall("Get","/User/GetContactById",{Id:contactId}))
        .then((response)=>{return response})
        .catch((err)=>{return err});
    }

    RemoveContactById(contactId){
        return $.when(this.jQueryAjaxCall("Get","/User/RemoveContact",{Id:contactId}))
        .then((response)=>{return response})
        .catch((err)=>{return err});
    }

    GetCountryDialCodes(){
        return $.when(this.jQueryAjaxCall("Get","/User/GetDialCodes",null))
            .then((response)=>{return response})
            .catch((err)=>{return err});
    }

    /*------Post Requests-------*/

    DownloadContactDetails(contactIds,fileType){
        debugger;
        return $.when(this.jQueryAjaxCall("Post","/User/DownloadContact",{FileType:fileType,Ids:contactIds}))
        .then((response)=>{return response})
        .catch((err)=>{return err});
    }

    SyncContacts(PostData){
        return $.when(this.jQueryAjaxCall("Post","/User/SyncContacts",{model:PostData}))
        .then((response)=>{return response})
        .catch((err)=>{return err});
    }
}
export{DataAccess}