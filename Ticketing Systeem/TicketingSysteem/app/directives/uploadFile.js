(function() {
    'use strict';

    angular
        .module('app')
        .directive('uploadFile', uploadFile);

    uploadFile.$inject = ['$window', '$parse'];
    
    function uploadFile($window, $parse) {
        var directive = {
            link: link,
            restrict: 'A'
        };


        function link(scope, element, attrs) {
            var file_uploaded = $parse(attrs.uploadFile);

            element.bind('change', function () {
                scope.$apply(function () {
                    file_uploaded.assign(scope, element[0].files[0]);
                });
            });
        }


        return directive;
        
    }
})();