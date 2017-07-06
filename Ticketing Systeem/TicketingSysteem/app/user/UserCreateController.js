(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserCreateController', userCreateController);

    userCreateController.$inject = ['toaster', 'UserService', 'LoginService', '$state', 'RolesService', 'VerantwoordelijkenService'];

    function userCreateController(toaster, userService, loginService, state, rolesService, verantwoordelijkenService) {
        var ctrl = this;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                var user = getUserModel()

                //Register user 
                var account = {
                    Email: user.email,
                    Password: user.paswoord,
                    ConfirmPassword: user.paswoord,
                    Role: user.rol
                };
                loginService.register(account).then(function (response) {
                    //alles ok
                    //Insert in database
                    userService.insert(user).then(function () {
                        ctrl.cancel(true);
                    }).finally(function () {
                        ctrl.loading = false;
                    });
                }, function (error) {
                    ctrl.loading = false;
                    toaster.error(error.modelState[0]);
                });
            }
        }

        ctrl.cancel = function () {
            state.go('users.list');
        }

        function getUserModel() {
            var returnUser = angular.copy(ctrl.user);

            if (angular.isDefined(returnUser.verantwoordelijke)) {
                returnUser.verantwoordelijkeId = returnUser.verantwoordelijke.id;
                delete returnUser.verantwoordelijke;
            }

            return returnUser;
        }

        function getRoles() {
            ctrl.loadingRoles = true;
            rolesService.getAll().then(function (response) {
                ctrl.roles = response;
            }, function (error) {
            }).finally(function () {
                ctrl.loadingRoles = false;
            });
        }

        function getChiefs() {
            ctrl.loadingChiefs = true;
            verantwoordelijkenService.getAll().then(function (response) {
                ctrl.chiefs = response;
            }, function (error) {
            }).finally(function () {
                ctrl.loadingChiefs = false;
            });
        }

        init();

        function init() {
            getRoles();
            getChiefs();
        }
    }
})();

