(function () {
    'use strict';

    angular
        .module('app')
        .controller('TopBarController', topbarController);

    topbarController.$inject = ['toaster', 'LoginService', '$state','$cookies', '$http', '$rootScope'];

    function topbarController(toaster, loginService, $state, $cookies, $http, $rootScope) {
        var ctrl = this;

        ctrl.logout = function () {
            loginService.logout();
            $http.defaults.headers.common.Authorization = 'Bearer ';
            $cookies.remove('TSbearerToken');
            delete $rootScope.userId;
            delete $rootScope.userEmail;
            delete $rootScope.userRole;
            delete ctrl.username;

            $state.reload();

        }

        ctrl.login = function () {
            $state.go('login.login', { target: $state.current.name });
        }

        ctrl.detail = function () {
            $state.go('users.detail', { userId: $rootScope.userId });
        }

        $rootScope.$watch('userEmail', function () {
            ctrl.username = $rootScope.userEmail;
        });


        init();

        function init() {
        }
    }
})();

