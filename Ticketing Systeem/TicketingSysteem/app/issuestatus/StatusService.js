(function () {
    'use strict';

    angular
        .module('app')
        .factory('StatusService', StatusService);

    StatusService.$inject = ['AppConfig', 'HttpHelper'];

    function StatusService(appConfig, http) {

        var baseUrl = appConfig.apiUrl + "statussen";

        var service = {
            getAll: _getAll,
            getPossibleStatussen: _getPossibleStatussen
        };

        return service;

        function _getAll() {
            return http.get(baseUrl);
        }

        function _getPossibleStatussen(issueId) {
            return http.get(appConfig.apiUrl + "issues/" + issueId + "/statussen");
        }

    }
})();