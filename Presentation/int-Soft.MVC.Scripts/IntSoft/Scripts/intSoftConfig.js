intSoftApp.config([
    "$httpProvider", "$urlMatcherFactoryProvider", "$urlRouterProvider", "$locationProvider", "ScrollBarsProvider", "intSoftValuesProvider",
    function ($httpProvider, $urlMatcherFactoryProvider, $urlRouterProvider, $locationProvider, ScrollBarsProvider, intSoftValuesProvider) {
        $httpProvider.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";
        $urlMatcherFactoryProvider.caseInsensitive(true);
        $urlMatcherFactoryProvider.strictMode(false);
        $urlRouterProvider.otherwise("/Error/NotFound");
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });

        ScrollBarsProvider.defaults = {
            autoHideScrollbar: true,
            scrollInertia: 100,
            scrollButtons: {
                enable: false
            }
        };
        //var initInjector = angular.injector(["ng"]);
        //var $http = initInjector.get("$http");

        //$http.get("/Account/GetCurrentUserProfile").then(function (response) {
        //    intSoftValuesProvider.currentUser(response.data);
        //});

    }
]);