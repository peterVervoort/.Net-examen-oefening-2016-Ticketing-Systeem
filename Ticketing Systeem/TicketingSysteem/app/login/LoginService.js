(function () {
    'use strict';

    angular
        .module('app')
        .factory('LoginService', UserService);

    UserService.$inject = ['AppConfig', 'HttpHelper', '$http', '$q'];

    function UserService(appConfig, http, $http, $q) {

        var baseUrl = appConfig.apiUrl + "Account";

        var service = {
            register: _register,
            login: _login,
            logout: _logout,
            getInfo: _getInfo,
            remove: _remove
        };

        return service;

        function _login(account) {
            var deferred = $q.defer();
            var request = $http({
                method: "POST",
                url: baseUrl + '/Token',
                transformRequest: function(obj) {
                    var str = [];
                    for(var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: account,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function (msg, code) {
                if (code === 401) {
                    loginHandler.handle401();
                } 
                deferred.reject(msg);
            });
            return deferred.promise;


            return http.post(baseUrl + '/Token', account);
        }

        function _logout() {
            return http.post(baseUrl + '/Logout');
        }

        function _remove(login) {
            return http.post(baseUrl + '/RemoveAccount', login);
        }

        function _getInfo() {
            return http.get(baseUrl + '/UserInfo');
        }

        function _register(account) {
            return http.post(baseUrl + '/Register', account);
        }
    }
})();