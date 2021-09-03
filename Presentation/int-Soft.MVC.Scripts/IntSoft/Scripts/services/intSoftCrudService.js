intSoftApp.factory("intSoftCrudService", ["intSoftAjaxService", function (intSoftAjaxService) {

    var save = function (controllerName, model, url, antiForgeryToken) {
        url = url ? url : "Save";
        return intSoftAjaxService.post("/" + controllerName + "/" + url , model, antiForgeryToken);
    };
    
    var list = function (controllerName, url, parameters, antiForgeryToken) {
        url = url ? url : "List";
        return intSoftAjaxService.post("/" + controllerName + "/" + url, parameters, antiForgeryToken);
    };

    var get = function (controllerName, id, url) {
        url = url ? url : "GetModel";
        return intSoftAjaxService.get("/" + controllerName + "/" + url + "/" + id);
    };

    var deleteModel = function (controllerName, id, url, antiForgeryToken) {
        url = url ? url : "Delete";
        return intSoftAjaxService.post("/" + controllerName + "/" + url + "/" , { id: id }, antiForgeryToken);
    };

    var approve = function (controllerName, id, url, antiForgeryToken) {
        url = url ? url : "Approve";
        return intSoftAjaxService.post("/" + controllerName + "/" + url + "/", { id: id }, antiForgeryToken);
    };
    
    return {
        save: save,
        get: get,
        delete: deleteModel,
        list: list,
        approve: approve
    };
}]);