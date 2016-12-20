import {ValidationUtil} from "./validationUtil.js";
let highLight = new ValidationUtil();
let pathName = window.location.pathname;
let page = pathName.split("/").pop();
highLight.HighlightUserLink(page);