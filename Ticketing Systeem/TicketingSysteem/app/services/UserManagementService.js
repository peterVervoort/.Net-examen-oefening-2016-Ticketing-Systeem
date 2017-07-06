(function () {
    'use strict';

    angular
        .module('app')
        .factory('UserManagementService', UserService);

    UserService.$inject = [];

    function UserService() {
        var service = {
            getCurrentUser: _getCurrentUser,
            getPreferredLanguage: _getPreferredLanguage,
        };

        return service;

        function _getCurrentUser() {
            //TODO
            return {
                id: 1,
                name: 'jos',
                firstName: 'vermeulen',
                email: 'jos.vermeulen@gmail.com',
                userName: 'hetenjos69',
                languageId: 1
            }
        }

        function _getPreferredLanguage() {
            //TODO
            return {
                id: 1,
                taal: 'Nederlands'
            }
        }
    }
})();