class Utility {
    constructor(){}
    jQueryAjaxCall(methodType,Url,data){
        let Header = {
            "Accept":"text/json",
            "Content-Type":"application/json"
        };
        let ajaxOption = {
            header:Header,
            type:(methodType === "" || methodType === null || methodType === undefined )? "post":methodType.toLowerCase(),
            beginSend:this.ModalPopup().showModalPopup(true),
            url:Url,
            data:(data === null || data === "" || data === undefined)? "" :data,
            complete:this.ModalPopup().showModalPopup(false)
        };
        return $.ajax(ajaxOption).promise();
    }

    ModalPopup(){
        function ShowModalPopup(visibility){
            let loader = $("#loader");
            switch(visibility){
                case true:loader.modal({
                    show:true,
                    backdrop:"static",
                    keyboard: false
                }); break;
                case false:loader.modal("hide"); break;
            }
        };
        return {
            showModalPopup: ShowModalPopup,
        };
    }

    CurrentPageNavigation(){
        let pathName = window.location.pathname;
        let pageName = pathName.split("/").pop();
        return pageName.toLowerCase();
    }

}
export {Utility}