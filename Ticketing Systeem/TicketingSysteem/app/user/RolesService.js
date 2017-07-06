(function () {
    'use strict';

    angular
        .module('app')
        .factory('RolesService', UserService);

    UserService.$inject = ['AppConfig', 'HttpHelper'];

    function UserService(appConfig, http) {

        var baseUrl = appConfig.apiUrl + "rollen";

        var service = {
            getAll: _getAll
        };

        return service;

        function _getAll() {
            return http.get(baseUrl);
        }
    }
})();