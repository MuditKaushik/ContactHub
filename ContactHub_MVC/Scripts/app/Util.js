class Utility {
    constructor(countryListId){
        this.countryListId = countryListId;
    }
    GetContryListXml() {
        this.ModalPopup().showModalPopup();
        $.when(this.jQueryAjaxCall("get","/Home/GetCountryListXml",null).promise())
            .then((data)=>{
                for(let item of data.Countries){
                    $("#"+ this.countryListId).append($('<option></option>').attr("value",item.Value).text(item.Text));
                }
                this.ModalPopup().HideModalPopup();
                return false;
            })
        .catch((err)=>{
            console.log(err.statusText);
        });
    }
    jQueryAjaxCall(methodType,Url,data){
        let ajaxOption = {
            type:(methodType === "" || methodType === null || methodType === undefined )? "post":methodType.toLowerCase(),
            url:Url,
            data:(data === null || data === "" || data === undefined)? "" :data
        };
        return $.ajax(ajaxOption).pipe((response)=> { return response});
    }

    ModalPopup(){
        function ShowModalPopup(){
            $("#loader").modal({
                backdrop:"static",
                keyboard: false
            });
            return;
        };
        function HideModalPopup(){
            $("#loader").modal("hide");
        };
        return {
            showModalPopup: ShowModalPopup,
            HideModalPopup: HideModalPopup
        };
    }

}
export {Utility}