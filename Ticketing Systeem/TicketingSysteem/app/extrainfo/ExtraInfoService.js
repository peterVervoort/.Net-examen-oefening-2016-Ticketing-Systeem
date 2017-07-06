(function () {
    'use strict';

    angular
        .module('app')
        .factory('ExtraInfoService', ExtraInfoService);

    ExtraInfoService.$inject = ['AppConfig', 'HttpHelper'];

    function ExtraInfoService(appConfig, http) {

        var baseUrl = appConfig.apiUrl + "extrainfos";

        var service = {
            getAll: _getAll,
            getById: _getById,
            insert: _insert,
            update: _update,
            search: _search
        };

        return service;

        function _getById(extraInfoId) {
            return http.get(baseUrl + "/" + extraInfoId);
        }

        function _getAll() {
            return http.get(baseUrl);
        }

        function _insert(extraInfo) {
            return http.post(baseUrl, extraInfo);
        }

        function _update(extraInfo) {
            return http.put(baseUrl + "/" +extraInfo.id, extraInfo);
        }

        function _search(search) {
            return http.post(baseUrl + '/search', search);
        }

    }
})();