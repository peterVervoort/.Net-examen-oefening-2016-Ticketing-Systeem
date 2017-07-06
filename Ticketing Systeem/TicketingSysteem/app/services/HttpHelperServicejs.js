(function () {
    'use strict';

    angular
        .module('app')
        .factory('HttpHelper', HttpHelperServicejs);

    HttpHelperServicejs.$inject = ['$http', '$q', 'toaster', 'LoginHandler', '$translate'];

    function HttpHelperServicejs($http, $q, toaster, loginHandler, $translate) {
        var service = {
            get: _GET,
            getParameters: _GETwithParameters,
            post: _POST,
            put: _PUT,
            remove: _DELETE,
        };

        return service;

        function _GET(requesturl, suppressErrors) {
            var deferred = $q.defer();
            $http.get(requesturl).success(function (response) {
                deferred.resolve(response);
            }).error(function (msg, code) {
                if (code === 401) {
                    loginHandler.handle401();
                } else {
                    if (angular.isUndefined(suppressErrors) || !suppressErrors) {
                        $translate('HttpError').then(function (translation) {
                            toaster.error(translation);
                        });
                    }
                    deferred.reject();
                }
            });
            return deferred.promise;
        };

        function _GETwithParameters(requesturl, params, suppressErrors) {
            var deferred = $q.defer();
            $http.get(requesturl, params).success(function (response) {
                deferred.resolve(response);
            }).error(function (msg, code) {
                if (code === 401) {
                    loginHandler.handle401();
                } else {
                    if (angular.isUndefined(suppressErrors) || !suppressErrors) {
                        $translate('HttpError').then(function (translation) {
                            toaster.error(translation);
                        });
                    }
                    deferred.reject();
                }
            });
            return deferred.promise;
        };

        function _POST(requesturl, data, suppressErrors) {
            var deferred = $q.defer();
            $http.post(requesturl, data).success(function (response) {
                deferred.resolve(response);
            }).error(function (msg, code) {
                if (code === 401) {
                    loginHandler.handle401();
                } else {
                    if (angular.isUndefined(suppressErrors) || !suppressErrors) {
                        toaster.error(msg.message);
                    }
                    deferred.reject(msg);
                }
            });
            return deferred.promise;
        };

        function _PUT(requesturl, data, suppressErrors) {
            var deferred = $q.defer();
            $http.put(requesturl, data).success(function (response) {
                deferred.resolve(response);
            }).error(function (msg, code) {
                if (code === 401) {
                    loginHandler.handle401();
                } else {
                    if (angular.isUndefined(suppressErrors) || !suppressErrors) {
                        $translate('HttpError').then(function (translation) {
                            toaster.error(translation);
                        });
                    }
                    deferred.reject();
                }
            });
            return deferred.promise;
        };

        function _DELETE(requesturl, data, suppressErrors) {
            var deferred = $q.defer();
            var request = $http({
                method: "DELETE",
                url: requesturl,
                data: data,
                headers: { 'Content-Type': 'application/json' }
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function (msg, code) {
                if (code === 401) {
                    loginHandler.handle401();
                } else {
                    if (angular.isUndefined(suppressErrors) || !suppressErrors) {
                        $translate('HttpError').then(function (translation) {
                            toaster.error(translation);
                        });
                    }
                    deferred.reject();
                }
            });
            return deferred.promise;
        };
    }
})();
