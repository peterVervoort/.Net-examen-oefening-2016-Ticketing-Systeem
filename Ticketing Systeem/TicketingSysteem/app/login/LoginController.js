(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', loginController);

    loginController.$inject = ['toaster', 'LoginService', 'UserService', '$state', '$stateParams', '$cookies', '$http', '$rootScope', '$translate'];

    function loginController(toaster, loginService, userService, $state, $stateParams, $cookies, $http, $rootScope, $translate) {
        var ctrl = this;

        ctrl.login = function () {
            ctrl.loading = true;

            ctrl.user.grant_type = 'password';

            loginService.login(ctrl.user).then(function (data) {
                //Save in session
                $cookies.put('TSusername', data.userName);
                $cookies.put('TSbearerToken', data.access_token);
                $http.defaults.headers.common.Authorization = 'Bearer ' + data.access_token;


                //Fetch roles for username
                userService.getMyInfo().then(function (response) {
                    if (response) {
                        $rootScope.userRole = response.rol;
                        $rootScope.userId = response.id;
                        $rootScope.userEmail = response.email;
                        $cookies.put('TSuser', JSON.stringify(response));
                    }
                }, function (error) {
                });

                //continue to requested target
                if ($stateParams.target && $stateParams.target !== "login.login" && $stateParams.target !== "issue.detail") {
                    $state.go($stateParams.target);
                } else {
                    $state.go('issue.list');
                }
            }, function (error) {
                delete ctrl.user.password;
                $translate('CheckUsernamePassword').then(function (translation) {
                    toaster.error(translation);
                });
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        function loadCookies() {
            ctrl.user = {
                username: $cookies.get('TSusername')
            }
        }

        init();

        function init() {
            console.log($stateParams);
            loadCookies();
        }
    }
})();

