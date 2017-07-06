(function() {
    'use strict';

    angular
        .module('app')
        .directive('translate', translation);

    translation.$inject = ['$window', 'TranslationFactory'];
    
    function translation ($window, translationFactory) {
        
        var directive = {
            link: link,
            restrict: 'A',
            scope: {
                'trGroup': '=',
                'trKeyword': '='
            },
        };
        return directive;

        function link(scope, element, attrs) {
            scope.$watch('trGroup', function (newVal) {
                console.log('group', newVal);
            });
            scope.$watch('trKeyword', function (newVal) {
                console.log('keyword', newVal);
            });

            function getTranslation() {
                var result = translationFactory.getTranslation(scope.group, scope.keyword);
                console.log(result);
            }
        }
    }

})();