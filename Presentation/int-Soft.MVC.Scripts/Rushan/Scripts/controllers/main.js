intSoftApp
    // =========================================================================
    // Base controller for common functions
    // =========================================================================
    .controller('materialadminCtrl', [
        '$timeout', '$state', '$scope', 'growlService', function ($timeout, $state, $scope, growlService) {
            //Welcome Message
            growlService.growl('Welcome back Mallinda!', 'inverse');


            // Detact Mobile Browser
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
                angular.element('html').addClass('ismobile');
            }

            // By default Sidbars are hidden in boxed layout and in wide layout only the right sidebar is hidden.
            $scope.sidebarToggle = {
                left: false,
                right: false
            }

            // By default template has a boxed layout
            $scope.layoutType = 1; //localStorage.getItem('ma-layout-status');
            //console.log($scope.layoutType);
            
            // For Mainmenu Active Class
            $scope.$state = $state;

            //Close sidebar on click
            $scope.sidebarStat = function (event) {
                if (!angular.element(event.target).parent().hasClass('active')) {
                    $scope.sidebarToggle.left = false;
                }
            }

            //Listview Search (Check listview pages)
            $scope.listviewSearchStat = false;

            $scope.lvSearch = function () {
                $scope.listviewSearchStat = true;
            }

            //Listview menu toggle in small screens
            $scope.lvMenuStat = false;

            //Blog
            $scope.wallCommenting = [];

            $scope.wallImage = false;
            $scope.wallVideo = false;
            $scope.wallLink = false;

            //Skin Switch
            $scope.currentSkin = 'cyan';

            $scope.skinList = [
                'lightblue',
                'bluegray',
                'cyan',
                'teal',
                'green',
                'orange',
                'blue',
                'purple'
            ];

            $scope.skinSwitch = function (color) {
                $scope.currentSkin = color;
            }

        }
    ])

    // =========================================================================
    // Header
    // =========================================================================
    .controller('headerCtrl', ["$scope", "$rootScope", "intSoftAjaxService", function ($scope, $rootScope, intSoftAjaxService) {
        $scope.getUserInfo = function () {
            intSoftAjaxService.get('/Account/GetUserProfile').then(function (response) {
                $scope.user = response.data;
                console.log(localStorage.getItem('ma-layout-status'));
                console.log("we are here !!!!");
                $rootScope.user = response.data;
                if ($scope.user.accountType === 0) {
                    intSoftAjaxService.get('/Account/GetTeacherProfilePicture')
                    .then(function (picResponse) {
                        $scope.user.picture = picResponse.data;
                    });
                }
                if ($scope.user.accountType === 1) {
                    $scope.user.picture = $scope.user.gender === 0
                        ? "/Content/img/MaleStudent.jpg"
                        : "/Content/img/FemaleStudent.jpg";
                }

            });
        }

    }])

    //=================================================
    // Profile
    //=================================================
    .controller('profileCtrl', [
        'growlService', function (growlService) {

            //Get Profile Information from profileService Service

            //User
            this.profileSummary = "Sed eu est vulputate, fringilla ligula ac, maximus arcu. Donec sed felis vel magna mattis ornare ut non turpis. Sed id arcu elit. Sed nec sagittis tortor. Mauris ante urna, ornare sit amet mollis eu, aliquet ac ligula. Nullam dolor metus, suscipit ac imperdiet nec, consectetur sed ex. Sed cursus porttitor leo.";

            this.fullName = "Mallinda Hollaway";
            this.gender = "female";
            this.birthDay = "23/06/1988";
            this.martialStatus = "Single";
            this.mobileNumber = "00971123456789";
            this.emailAddress = "malinda.h@gmail.com";
            this.twitter = "@malinda";
            this.twitterUrl = "twitter.com/malinda";
            this.skype = "malinda.hollaway";
            this.addressSuite = "44-46 Morningside Road";
            this.addressCity = "Edinburgh";
            this.addressCountry = "Scotland";

            //Edit
            this.editSummary = 0;
            this.editInfo = 0;
            this.editContact = 0;


            this.submit = function (item, message) {
                if (item === 'profileSummary') {
                    this.editSummary = 0;
                }

                if (item === 'profileInfo') {
                    this.editInfo = 0;
                }

                if (item === 'profileContact') {
                    this.editContact = 0;
                }

                growlService.growl(message + ' has updated Successfully!', 'inverse');
            }

        }
    ])


    //=================================================
    // LOGIN
    //=================================================
    .controller('loginCtrl', function () {

        //Status

        this.login = 1;
        this.register = 0;
        this.forgot = 0;
    })


    //=================================================
    // CALENDAR
    //=================================================
    .controller('calendarCtrl', [
        '$modal', function ($modal) {

            //Create and add Action button with dropdown in Calendar header. 
            this.month = 'month';

            this.actionMenu = '<ul class="actions actions-alt" id="fc-actions">' +
                '<li class="dropdown" dropdown>' +
                '<a href="" dropdown-toggle><i class="zmdi zmdi-more-vert"></i></a>' +
                '<ul class="dropdown-menu dropdown-menu-right">' +
                '<li class="active">' +
                '<a data-calendar-view="month" href="">Month View</a>' +
                '</li>' +
                '<li>' +
                '<a data-calendar-view="basicWeek" href="">Week View</a>' +
                '</li>' +
                '<li>' +
                '<a data-calendar-view="agendaWeek" href="">Agenda Week View</a>' +
                '</li>' +
                '<li>' +
                '<a data-calendar-view="basicDay" href="">Day View</a>' +
                '</li>' +
                '<li>' +
                '<a data-calendar-view="agendaDay" href="">Agenda Day View</a>' +
                '</li>' +
                '</ul>' +
                '</div>' +
                '</li>';


            //Open new event modal on selecting a day
            this.onSelect = function (argStart, argEnd) {
                var modalInstance = $modal.open({
                    templateUrl: 'addEvent.html',
                    controller: 'addeventCtrl',
                    backdrop: 'static',
                    keyboard: false,
                    resolve: {
                        calendarData: function () {
                            var x = [argStart, argEnd];
                            return x;
                        }
                    }
                });
            }
        }
    ])

    //Add event Controller (Modal Instance)
    .controller('addeventCtrl', [
        '$scope', '$modalInstance', 'calendarData', function ($scope, $modalInstance, calendarData) {

            //Calendar Event Data
            $scope.calendarData = {
                eventStartDate: calendarData[0],
                eventEndDate: calendarData[1]
            };

            //Tags
            $scope.tags = [
                'bgm-teal',
                'bgm-red',
                'bgm-pink',
                'bgm-blue',
                'bgm-lime',
                'bgm-green',
                'bgm-cyan',
                'bgm-orange',
                'bgm-purple',
                'bgm-gray',
                'bgm-black',
            ];

            //Select Tag
            $scope.currentTag = '';

            $scope.onTagClick = function (tag, $index) {
                $scope.activeState = $index;
                $scope.activeTagColor = tag;
            }

            //Add new event
            $scope.addEvent = function () {
                if ($scope.calendarData.eventName) {

                    //Render Event
                    $('#calendar').fullCalendar('renderEvent', {
                        title: $scope.calendarData.eventName,
                        start: $scope.calendarData.eventStartDate,
                        end: $scope.calendarData.eventEndDate,
                        allDay: true,
                        className: $scope.activeTagColor

                    }, true); //Stick the event

                    $scope.activeState = -1;
                    $scope.calendarData.eventName = '';
                    $modalInstance.close();
                }
            }

            //Dismiss 
            $scope.eventDismiss = function () {
                $modalInstance.dismiss();
            }
        }
    ])

    // =========================================================================
    // COMMON FORMS
    // =========================================================================
    .controller('formCtrl', function () {

        //Input Slider
        this.nouisliderValue = 4;
        this.nouisliderFrom = 25;
        this.nouisliderTo = 80;
        this.nouisliderRed = 35;
        this.nouisliderBlue = 90;
        this.nouisliderCyan = 20;
        this.nouisliderAmber = 60;
        this.nouisliderGreen = 75;

        //Color Picker
        this.color = '#03A9F4';
        this.color2 = '#8BC34A';
        this.color3 = '#F44336';
        this.color4 = '#FFC107';
    })


    // =========================================================================
    // PHOTO GALLERY
    // =========================================================================
    .controller('photoCtrl', function () {

        //Default grid size (2)
        this.photoColumn = 'col-md-2';
        this.photoColumnSize = 2;

        this.photoOptions = [
            { value: 2, column: 6 },
            { value: 3, column: 4 },
            { value: 4, column: 3 },
            { value: 1, column: 12 },
        ]

        //Change grid
        this.photoGrid = function (size) {
            this.photoColumn = 'col-md-' + size;
            this.photoColumnSize = size;
        }

    })


    // =========================================================================
    // ANIMATIONS DEMO
    // =========================================================================
    .controller('animCtrl', [
        '$timeout', function ($timeout) {

            //Animation List
            this.attentionSeekers = [
                { animation: 'bounce', target: 'attentionSeeker' },
                { animation: 'flash', target: 'attentionSeeker' },
                { animation: 'pulse', target: 'attentionSeeker' },
                { animation: 'rubberBand', target: 'attentionSeeker' },
                { animation: 'shake', target: 'attentionSeeker' },
                { animation: 'swing', target: 'attentionSeeker' },
                { animation: 'tada', target: 'attentionSeeker' },
                { animation: 'wobble', target: 'attentionSeeker' }
            ];
            this.flippers = [
                { animation: 'flip', target: 'flippers' },
                { animation: 'flipInX', target: 'flippers' },
                { animation: 'flipInY', target: 'flippers' },
                { animation: 'flipOutX', target: 'flippers' },
                { animation: 'flipOutY', target: 'flippers' }
            ];
            this.lightSpeed = [
                { animation: 'lightSpeedIn', target: 'lightSpeed' },
                { animation: 'lightSpeedOut', target: 'lightSpeed' }
            ];
            this.special = [
                { animation: 'hinge', target: 'special' },
                { animation: 'rollIn', target: 'special' },
                { animation: 'rollOut', target: 'special' }
            ];
            this.bouncingEntrance = [
                { animation: 'bounceIn', target: 'bouncingEntrance' },
                { animation: 'bounceInDown', target: 'bouncingEntrance' },
                { animation: 'bounceInLeft', target: 'bouncingEntrance' },
                { animation: 'bounceInRight', target: 'bouncingEntrance' },
                { animation: 'bounceInUp', target: 'bouncingEntrance' }
            ];
            this.bouncingExits = [
                { animation: 'bounceOut', target: 'bouncingExits' },
                { animation: 'bounceOutDown', target: 'bouncingExits' },
                { animation: 'bounceOutLeft', target: 'bouncingExits' },
                { animation: 'bounceOutRight', target: 'bouncingExits' },
                { animation: 'bounceOutUp', target: 'bouncingExits' }
            ];
            this.rotatingEntrances = [
                { animation: 'rotateIn', target: 'rotatingEntrances' },
                { animation: 'rotateInDownLeft', target: 'rotatingEntrances' },
                { animation: 'rotateInDownRight', target: 'rotatingEntrances' },
                { animation: 'rotateInUpLeft', target: 'rotatingEntrances' },
                { animation: 'rotateInUpRight', target: 'rotatingEntrances' }
            ];
            this.rotatingExits = [
                { animation: 'rotateOut', target: 'rotatingExits' },
                { animation: 'rotateOutDownLeft', target: 'rotatingExits' },
                { animation: 'rotateOutDownRight', target: 'rotatingExits' },
                { animation: 'rotateOutUpLeft', target: 'rotatingExits' },
                { animation: 'rotateOutUpRight', target: 'rotatingExits' }
            ];
            this.fadeingEntrances = [
                { animation: 'fadeIn', target: 'fadeingEntrances' },
                { animation: 'fadeInDown', target: 'fadeingEntrances' },
                { animation: 'fadeInDownBig', target: 'fadeingEntrances' },
                { animation: 'fadeInLeft', target: 'fadeingEntrances' },
                { animation: 'fadeInLeftBig', target: 'fadeingEntrances' },
                { animation: 'fadeInRight', target: 'fadeingEntrances' },
                { animation: 'fadeInRightBig', target: 'fadeingEntrances' },
                { animation: 'fadeInUp', target: 'fadeingEntrances' },
                { animation: 'fadeInBig', target: 'fadeingEntrances' }
            ];
            this.fadeingExits = [
                { animation: 'fadeOut', target: 'fadeingExits' },
                { animation: 'fadeOutDown', target: 'fadeingExits' },
                { animation: 'fadeOutDownBig', target: 'fadeingExits' },
                { animation: 'fadeOutLeft', target: 'fadeingExits' },
                { animation: 'fadeOutLeftBig', target: 'fadeingExits' },
                { animation: 'fadeOutRight', target: 'fadeingExits' },
                { animation: 'fadeOutRightBig', target: 'fadeingExits' },
                { animation: 'fadeOutUp', target: 'fadeingExits' },
                { animation: 'fadeOutUpBig', target: 'fadeingExits' }
            ];
            this.zoomEntrances = [
                { animation: 'zoomIn', target: 'zoomEntrances' },
                { animation: 'zoomInDown', target: 'zoomEntrances' },
                { animation: 'zoomInLeft', target: 'zoomEntrances' },
                { animation: 'zoomInRight', target: 'zoomEntrances' },
                { animation: 'zoomInUp', target: 'zoomEntrances' }
            ];
            this.zoomExits = [
                { animation: 'zoomOut', target: 'zoomExits' },
                { animation: 'zoomOutDown', target: 'zoomExits' },
                { animation: 'zoomOutLeft', target: 'zoomExits' },
                { animation: 'zoomOutRight', target: 'zoomExits' },
                { animation: 'zoomOutUp', target: 'zoomExits' }
            ];

            //Animate    
            this.ca = '';

            this.setAnimation = function (animation, target) {
                if (animation === "hinge") {
                    animationDuration = 2100;
                } else {
                    animationDuration = 1200;
                }

                angular.element('#' + target).addClass(animation);

                $timeout(function () {
                    angular.element('#' + target).removeClass(animation);
                }, animationDuration);
            }

        }
    ]);