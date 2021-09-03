intSoftApp.directive("intSoftDate", [
    function () {
        return {
            restrict: "A",
            scope: true,
            controller: function ($scope) {

                // Scope Methods
                $scope.openDatePopup = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    $scope.opened = true;
                };
            }
        };
    }
]);