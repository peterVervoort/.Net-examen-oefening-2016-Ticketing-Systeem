(function () {
    'use strict';

    angular
        .module('app')
        .factory('TranslationFactory', TranslationFactory);

    TranslationFactory.$inject = ['$http', 'UserManagementService', 'AppConfig'];

    function TranslationFactory($http, userService, appConfig) {
        var service = {
            getTranslation: _getTranslation
        };

        return service;

        function _getTranslation(group, keyword, defaultText) {
            if (angular.isUndefined(group)) return;
            if (angular.isUndefined(keyword)) return group;
            //TODO
            var language = userService.getPreferredLanguage();
            if (language) {
                _getTranslationLanguage(group, keyword, language)
            } else {
                return defaultText;
            }
        }

        function _getTranslationLanguage(group, keyword, language) {
            var baseurl = appConfig.apiUrl;
            $http.get(baseurl + "translation").success(function (response) {
                
            }).error(function (msg, code) {
                
            });
        };
    }
})();