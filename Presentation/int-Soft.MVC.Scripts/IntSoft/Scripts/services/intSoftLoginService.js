intSoftApp.factory("intSoftLoginService", [
    "intSoftAjaxService", function (intSoftAjaxService) {

        var login = function (model, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/Login/", model, antiForgeryToken);
        };

        var forgotPassword = function (model, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/ForgotPassword/", model, antiForgeryToken);
        };

        var reactivate = function (model, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/Reactivate/", model, antiForgeryToken);
        };

        var confirmEmail = function (model, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/ConfirmEmail/", model, antiForgeryToken);
        };

        var checkPasswordAvailability = function (password, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/CheckPassword/", { password: password }, antiForgeryToken);
        };

        var checkEmailAvailability = function (email, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/CheckEmail/", { email: email }, antiForgeryToken);
        };

        var checkPhoneVerificationCodeValidity = function (verificationCode, phoneNumber, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/CheckPhoneVerificationCodeValidity/",
            { verificationCode: verificationCode, phoneNumber: phoneNumber }, antiForgeryToken);
        };

        var sendPhoneVerification = function (phoneNumber, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/SendPhoneVerification/", { phoneNumber: phoneNumber }, antiForgeryToken);
        }

        var resetPassword = function (model, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/ResetPassword/", model, antiForgeryToken);
        };

        var changePassword = function (model, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/ChangePassword/", model, antiForgeryToken);
        };

        var changeEmail = function (model, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/ChangeEmail/", model, antiForgeryToken);
        };

        var changePhoneNumber = function (model, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/ChangePhoneNumber/", model, antiForgeryToken);
        };

        var confirmPhoneNumber = function (model, antiForgeryToken) {
            return intSoftAjaxService.post("/Account/ConfirmPhoneNumber/", model, antiForgeryToken);
        };

        return {
            login: login,
            sendPhoneVerification: sendPhoneVerification,
            forgotPassword: forgotPassword,
            reactivate: reactivate,
            confirmEmail: confirmEmail,
            resetPassword: resetPassword,
            changePassword: changePassword,
            changeEmail: changeEmail,
            changePhoneNumber: changePhoneNumber,
            confirmPhoneNumber: confirmPhoneNumber,
            checkPasswordAvailability: checkPasswordAvailability,
            checkEmailAvailability: checkEmailAvailability,
            checkPhoneVerificationCodeValidity: checkPhoneVerificationCodeValidity
        };
    }
]);