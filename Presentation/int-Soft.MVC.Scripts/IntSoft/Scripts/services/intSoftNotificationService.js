intSoftApp.service("intSoftNotificationService",
[
    function () {
        var notifyError = function(message, delay, allowDismiss, stateName, stateParams) {
            var notificationOptions = {
                icon: "zmdi zmdi-minus-circle",
                type: "danger",
                allow_dismiss: allowDismiss === undefined ? false : allowDismiss,
                label: "Cancel",
                className: "btn-xs btn-inverse",
                placement: {
                    from: "top",
                    align: "right"
                },
                animate: {
                    enter: "animated fadeInDown",
                    exit: "animated fadeOutUp"
                },
                offset: {
                    x: 20,
                    y: 85
                }
            };
            //if (stateName) {
            //    notificationOptions.url = $state.href(stateName, stateParams);
            //}
            if (delay !== 0) {
                notificationOptions.delay = delay === undefined ? 5000 : delay;
            }
            $.growl({ message: message }, notificationOptions);
        };

        var notifySuccess = function(message, delay, allowDismiss) {
            $.growl({
                    message: message
                },
                {
                    icon: "zmdi zmdi-check-circle",
                    type: "success",
                    allow_dismiss: allowDismiss === undefined ? false : allowDismiss,
                    label: "Cancel",
                    className: "btn-xs btn-inverse",
                    placement: {
                        from: "top",
                        align: "right"
                    },
                    delay: delay === undefined ? 2500 : delay,
                    animate: {
                        enter: "animated fadeInDown",
                        exit: "animated fadeOutUp"
                    },
                    offset: {
                        x: 20,
                        y: 85
                    }
                });
        };

        var newNotification = function(message) {
            $.growl({
                    message: message
                },
                {
                    type: "info",
                    allow_dismiss: false,
                    label: "Cancel",
                    className: "btn-xs btn-inverse",
                    placement: {
                        from: "bottom",
                        align: "left"
                    },
                    delay: 10000,
                    animate: {
                        enter: "animated tada",
                        exit: "animated fadeOutDown"
                    },
                    offset: {
                        x: 20,
                        y: 20
                    }
                });
        };

        return {
            notifyError: notifyError,
            notifySuccess: notifySuccess,
            newNotification: newNotification
        };
    }
]);