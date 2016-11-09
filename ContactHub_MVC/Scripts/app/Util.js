class Utility {
    constructor(countryListId){
        this.countryListId = countryListId;
    }
    GetContryListXml() {
        $.when(this.jQueryAjaxCall("get","/Home/GetCountryListXml",null).promise())
            .then((data)=>{
                for(let item of data.Countries){
                    $("#"+ this.countryListId).append($('<option></option>').attr("value",item.Value).text(item.Text));
                }
                $("#loader").modal("hide");
                return false;
            })
        .catch((err)=>{
            $("#loader").modal("hide");
            console.log(err.statusText);
        });
    }
    GetContryListJson() {}
    jQueryAjaxCall(methodType,Url,data){
        $("#loader").modal("show");
        let ajaxOption = {
            type:(methodType === "" || methodType === null || methodType === undefined )? "post":methodType.toLowerCase(),
            url:Url,
            data:(data === null || data === "" || data === undefined)? "" :data
        };
        return $.ajax(ajaxOption).pipe((response)=> { return response});
    }
}
export {Utility}