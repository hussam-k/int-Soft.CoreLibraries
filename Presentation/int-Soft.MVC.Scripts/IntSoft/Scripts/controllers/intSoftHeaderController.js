intSoftApp.controller("intSoftHeaderController",
["$timeout", "$state", "$scope", "intSoftAjaxService", "intSoftNotificationService",
    function($timeout, $state, $scope, intSoftAjaxService, intSoftNotificationService) {

        $scope.logout = function() {
            intSoftAjaxService.get("/Account/LogOff")
                .then(function() {
                        window.location = "/";
                    },
                    function(parameters) {
                        intSoftNotificationService.notifyError(parameters.errorMessage);
                    });
        };
    }
]);