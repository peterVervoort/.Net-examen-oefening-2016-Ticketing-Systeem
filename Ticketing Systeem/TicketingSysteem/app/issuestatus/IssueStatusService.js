(function () {
    'use strict';

    angular
        .module('app')
        .factory('IssueStatusService', IssueStatusService);

    IssueStatusService.$inject = ['AppConfig', 'HttpHelper'];

    function IssueStatusService(appConfig, http) {

        var baseUrl = appConfig.apiUrl + "issueStatussen";

        var service = {
            getAll: _getAll,
            getById: _getById,
            insert: _insert,
            update: _update,
            search: _search,
            remove: _remove
        };

        return service;

        function _getById(issueStatusId) {
            return http.get(baseUrl + "/" + issueStatusId + "?fields=extrainfos,solver");
        }

        function _getAll() {
            return http.get(baseUrl + "?fields=extrainfos,solver");
        }

        function _insert(issueStatus) {
            return http.post(baseUrl, issueStatus);
        }

        function _update(issueStatus) {
            return http.put(baseUrl + "/" +issueStatus.id, issueStatus);
        }

        function _search(search) {
            return http.post(baseUrl + '/search?fields=extrainfos,solver', search);
        }

        function _remove(issueStatusId) {
            return http.delete(baseUrl + '/' + issueStatusId);
        }

    }
})();