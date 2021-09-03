intSoftApp.factory("intSoftLocalizationService", ["intSoftAjaxService", function (intSoftAjaxService) {

    var changeLanguage = function (language, antiForgeryToken) {
        return intSoftAjaxService.post("/Localization/ChangeLanguage", language, antiForgeryToken);
    };
   
    return {
        changeLanguage: changeLanguage
    };

}]);