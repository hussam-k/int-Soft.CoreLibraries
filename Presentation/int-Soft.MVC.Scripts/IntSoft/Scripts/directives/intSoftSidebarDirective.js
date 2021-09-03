intSoftApp.directive("intSoftSidebar", [
    function () {
        return {
            restrict: "A",
            controller: function ($scope, $state, $http, intSoftNotificationService) {
                $scope.navigateToState = function (stateName) {
                    $state.go(stateName);
                };

                $scope.logout = function() {
                    $http.get("/Account/LogOff", {})
                        .success(function() {
                            window.location = '/';
                        })
                        .error(function(parameters) {
                            intSoftNotificationService.notifyError(parameters.errorMessage);
                        });
                };

                $scope.scrollbarConfig = {
                    theme: "dark",
                    autoHideScrollbar: true,
                    scrollInertia: 0
                };
            }
        };
    }
]);