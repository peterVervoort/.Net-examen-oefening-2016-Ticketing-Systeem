(function () {
    'use strict';

    angular
        .module('app')
        .factory('VerantwoordelijkenService', UserService);

    UserService.$inject = ['AppConfig', 'HttpHelper'];

    function UserService(appConfig, http) {

        var baseUrl = appConfig.apiUrl + "verantwoordelijken";

        var service = {
            getAll: _getAll
        };

        return service;

        function _getAll() {
            return http.get(baseUrl);
        }

    }
})();