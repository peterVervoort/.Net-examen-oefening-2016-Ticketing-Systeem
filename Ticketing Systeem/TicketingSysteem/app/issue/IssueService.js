(function () {
    'use strict';

    angular
        .module('app')
        .factory('IssueService', IssueService);

    IssueService.$inject = ['AppConfig', 'HttpHelper'];

    function IssueService(appConfig, http) {

        var baseUrl = appConfig.apiUrl + "issues";

        var service = {
            getAll: _getAll,
            getById: _getById,
            insert: _insert,
            update: _update,
            remove: _delete,
            search: _search
        };

        return service;

        function _getById(issueId) {
            return http.get(baseUrl + "/" + issueId + '?fields=gebruiker,issuestatussen,issuestatussen.solver');
        }

        function _getAll() {
            return http.get(baseUrl + '?fields=gebruiker,issuestatussen,issuestatussen.solver');
        }

        function _insert(issue) {
            return http.post(baseUrl, issue);
        }

        function _search(search) {
            return http.post(baseUrl + '/search?fields=gebruiker,issuestatussen,issuestatussen.solver', search);
        }

        function _update(issue) {
            return http.put(baseUrl + "/" +issue.id, issue);
        }

        function _delete(issueId) {
            return http.remove(baseUrl + "/" + issueId)
        }

    }
})();