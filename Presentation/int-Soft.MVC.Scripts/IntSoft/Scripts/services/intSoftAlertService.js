intSoftApp.service('intSoftAlertService', [
    "$translate",
    function($translate) {
        var showConfirm = function(title, message, onConfirm, onReject) {
            swal({
                title: title,
                text: message,
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: $translate.instant('YES'),
                cancelButtonText: $translate.instant('CANCEL'),
            }).then(function() {
                if (onConfirm && onConfirm !== null) {
                    onConfirm();
                }
            }, function(dismiss) {
                // dismiss can be 'cancel', 'overlay', 'close', and 'timer'
                if (onReject && onReject !== null) {
                    onReject(dismiss);
                }
            });
        }

        var showSuccess = function(title, message) {
            swal(title, message, "success");
        }

        var showError = function(title, message) {
            swal(title, message, "error");
        }

        return {
            showConfirm: showConfirm,
            showSuccess: showSuccess,
            showError: showError,
            //showCustomAlert: showCustomAlert
        };
    }
]);