(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserEditController', UserEditController);

    UserEditController.$inject = ['toaster', 'UserService', '$state', '$stateParams', 'RolesService', 'VerantwoordelijkenService'];

    function UserEditController(toaster, userService, $state, $stateParams, rolesService, verantwoordelijkenService) {
        var ctrl = this;

        ctrl.edit = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                var user = getUserModel()

                userService.update(user).then(function () {
                    ctrl.cancel(true);
                }, function (error) {
                }).finally(function () {
                    ctrl.loading = false;
                });
            }
        }

        ctrl.cancel = function () {
            $state.go('users.list');
        }

        function getUserModel() {
            var returnUser = angular.copy(ctrl.user);

            if (angular.isUndefined(returnUser)) {
            }

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
                getUser($stateParams.userId);
            }, function (error) {
            }).finally(function () {
                ctrl.loadingChiefs = false;
            });
        }

        function getUser(userId) {
            ctrl.loading = true;
            userService.getById(userId).then(function (response) {
                ctrl.user = response;
                if (ctrl.user.verantwoordelijkeId) {
                    userService.getById(ctrl.user.verantwoordelijkeId).then(function (response) {
                        ctrl.user.verantwoordelijke = response;
                    });
                }
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        init();

        function init() {
            getRoles();
            getChiefs();
        }
    }
})();

