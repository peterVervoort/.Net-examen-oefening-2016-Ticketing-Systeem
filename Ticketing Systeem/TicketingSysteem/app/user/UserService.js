(function () {
    'use strict';

    angular
        .module('app')
        .factory('UserService', UserService);

    UserService.$inject = ['AppConfig', 'HttpHelper'];

    function UserService(appConfig, http) {

        var baseUrl = appConfig.apiUrl + "gebruikers";

        var service = {
            getAll: _getAll,
            getById: _getById,
            insert: _insert,
            update: _update,
            remove: _delete,
            search: _search,
            getMyInfo: _myInfo
        };

        return service;

        function _getById(userId) {
            return http.get(baseUrl + "/" + userId + "?fields=verantwoordelijke");
        }
        
        function _getAll() {
            return http.get(baseUrl + "?fields=verantwoordelijke");
        }

        function _insert(user) {
          return http.post(baseUrl, user);
        }

        function _search(search) {
            return http.post(baseUrl + '/search?fields=verantwoordelijke', search);
        }

        function _update(user) {
            return http.put(baseUrl + "/" + user.id, user);
        }

        function _delete(userId) {
            return http.remove(baseUrl + "/" + userId)
        }

        function _myInfo() {
            return http.get(baseUrl + '/logininfo');
        }
    }
})();