(function () {
    'use strict';

    angular
        .module('app')
        .factory('NiveauService', IssueService);

    IssueService.$inject = ['AppConfig', 'HttpHelper'];

    function IssueService(appConfig, http) {

        var baseUrl = appConfig.apiUrl + "niveaus";

        var service = {
            getAll: _getAll
        };

        return service;

        function _getAll() {
            return http.get(baseUrl);
        }

    }
})();