intSoftApp.controller("intSoftMainController", [
        "$timeout", "$state", "$scope", "growlService", "intSoftValues", "intSoftAjaxService","$translate",
        function ($timeout, $state, $scope, growlService, intSoftValues, intSoftAjaxService, $translate) {
            // Detact Mobile Browser
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
                angular.element("html").addClass("ismobile");
            }

            // By default Sidbars are hidden in boxed layout and in wide layout only the right sidebar is hidden.
            $scope.sidebarToggle = {
                left: false,
                right: false
            };

            // By default template has a boxed layout
            $scope.layout = {
                layoutType: localStorage.getItem("ma-layout-status")
            };

            $scope.boxType = "0";


            // For Mainmenu Active Class
            $scope.$state = $state;

            //Close sidebar on click
            $scope.sidebarStat = function(event) {
                if (!angular.element(event.target).parent().hasClass("active")) {
                    $scope.sidebarToggle.left = false;
                }
            };

            //Skin Switch
            $scope.currentSkin = intSoftValues.theme;
            
            $scope.skinList = [
                "lightblue",
                "bluegray",
                "cyan",
                "teal",
                "green",
                "orange",
                "blue",
                "purple"
            ];

            $scope.$watch("intSoftValues.currentUser", function (newVal) {
                if (newVal) {
                    $scope.currentUser = newVal;
                    growlService.growl("Changed to  " + newVal.fullName + "!", "inverse");
                }
            });

            $scope.$on("currentUserPropertyChanged", function (event, args) {
                var propertyName = args.propertyName;
                var propertyValue = args.propertyValue;
                var currentUser = intSoftValues.currentUser;

                if (currentUser.hasOwnProperty(propertyName)) {
                    currentUser[propertyName] = propertyValue;
                    $scope.user[propertyName] = propertyValue;
                }

            });
            
            $scope.setPageTitle = function(title) {
                if (title) {
                    $scope.pageTitle = "| " + title;
                } else {
                    $scope.pageTitle = "";
                }
            }

            intSoftAjaxService.get("/Account/GetCurrentUserProfile").then(function (response) {
                intSoftValues.currentUser = response.data;
                $scope.user = response.data;
                console.log($scope.user);
                if ($scope.user) {
                    console.log(window.resources);
                    growlService.growl($translate.instant("WELCOME_BACK") + $scope.user.fullName + "!", "inverse");
                }
            });
        }
    ])
