(function () {
    'use strict';

    angular
        .module('app')
        .factory('LoginHandler', UserService);

    UserService.$inject = ['$state', '$rootScope'];

    function UserService($state, $rootScope) {
        var service = {
            handle401: _handle401,
        };

        return service;

        function _handle401(account) {
            //TODO:: relogin?
            $state.go('login.login', { target: $rootScope.currentState.name });
        }

    }
})();