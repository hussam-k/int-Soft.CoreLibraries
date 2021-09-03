intSoftApp.config(["$httpProvider", function ($httpProvider) {

    var interceptor = ["$q", "intSoftValues", "intSoftNotificationService",
        function ($q, intSoftValues, intSoftNotificationService) {

            return {
                'responseError': function (rejection) {
                    if (rejection.status === 401) {
                        if (intSoftValues.currentUser === null || intSoftValues.currentUser === undefined) {
                            intSoftNotificationService.notifyError("Unauthoized Access");
                            window.location = "/Accounts/Login";
                        } else {
                            intSoftNotificationService.notifyError("Unauthoized Access");
                        }
                    }
                    return $q.reject(rejection);
                }
            };
        }];

    $httpProvider.interceptors.push(interceptor);
}]);