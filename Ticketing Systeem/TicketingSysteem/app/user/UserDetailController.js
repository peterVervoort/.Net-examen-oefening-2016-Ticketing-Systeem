(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserDetailController', UserDetailController);

    UserDetailController.$inject = ['toaster', 'UserService', '$stateParams', '$state', 'LoginService', '$translate'];

    function UserDetailController(toaster, userService, $stateParams, $state, loginService, $translate) {
        var ctrl = this;

        function getUser() {
            ctrl.loading = true;
            userService.getById($stateParams.userId).then(function (response) {
                ctrl.user = response;
            }, function (error) {
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.delete = function () {
            userService.getById($stateParams.userId).then(function (response) {
                loginService.remove({ email: response.email }).then(function () {
                    userService.remove($stateParams.userId).then(function () {
                        $translate('ToasterUserRemoved').then(function (translate) {
                            toaster.success(translate);
                        });
                        $state.go("users.list");
                    })
                }, function () {
                    $translate('ToasterFailLoginData').then(function (translate) {
                        toaster.error(translate);
                    });
                });
            }, function () {
                $translate('ToasterFailUserData').then(function (translation) {
                    toaster.error(translation);
                });
            });
        }

        ctrl.edit = function () {
            $state.go("users.edit", { userId: $stateParams.userId });
        }

        init();

        function init() {
            getUser();
        }
    }
})();

