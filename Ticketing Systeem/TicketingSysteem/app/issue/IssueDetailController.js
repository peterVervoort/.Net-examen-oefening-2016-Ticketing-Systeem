(function () {
    'use strict';

    angular
        .module('app')
        .controller('IssueDetailController', IssueDetailController);

    IssueDetailController.$inject = ['toaster', 'IssueService', '$stateParams', '$state', 'StatusService', 'IssueStatusService', '$rootScope', '$uibModal', 'ExtraInfoService', 'Roles', '$translate'];

    function IssueDetailController(toaster, issueService, $stateParams, $state, statusService, issueStatusService, $rootScope, $uibModal, extraInfoService, Roles, $translate) {
        var ctrl = this;

        function getIssue(issueId) {
            ctrl.loading = true;
            issueService.getById(issueId).then(function (response) {
                ctrl.issue = response;
            }, function (error) {
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.delete = function () {
            issueService.remove($stateParams.issueId).then(function () {
                $state.go("issue.list");
            })
        }

        ctrl.goToState = function (state) {
            var status = {
                issueId: $stateParams.issueId,
                statusBeschrijving: state
            };

            //check toegewezen => zet solver
            if (state == "Toegewezen") {
                var modalInstance = $uibModal.open({
                    animation: true,
                    component: 'SolverSelectorModal'
                });

                modalInstance.result.then(function (solverId) {
                    status.solverId = solverId;
                    insertState(status);
                }, function () {
                    $translate('SolverRequiredStateChange').then(function (translation) {
                        toaster.error(translation);
                    });
                    return;
                });
            } else if (state == "InBehandeling" && ctrl.issue.huidigeStatus == "ExtraInfo") {
                issueStatusService.search({ issueId: $stateParams.issueId }).then(function (response) {
                    var last = response[response.length - 1];
                    openExtraInfoModal(last.id, true, status);
                });
            } else if (state == "Canceled" && ($rootScope.userRole == Roles.administrator || $rootScope.userRole == Roles.manager)) {
                openAnnulatieRedenModal(status);
            } else if (state == "Opgelost") {
                openOplossingModal(status);
            } else {
                insertState(status);
            }
        }

        function insertState(status) {
            //insert state
            ctrl.loadingStatusPost = true;
            issueStatusService.insert(status).then(function (response) {
                //check extra info
                if (status.statusBeschrijving == "ExtraInfo") {
                    openExtraInfoModal(response.id);
                } else {
                    onSuccesfulStateChange();
                }
            }, function (error) {
            }).finally(function () {
                ctrl.loadingStatusPost = false;
            });
        }

        function openExtraInfoModal(statusId, update, newState) {
            var modalInstance = $uibModal.open({
                animation: true,
                component: 'extraInfoModal',
                resolve: {
                    statusId: statusId,
                    update: update
                }
            });

            modalInstance.result.then(function (extraInfo) {
                if (update) {
                    extraInfoService.update(extraInfo).then(function (response) {
                        insertState(newState);
                    }, function (error) {
                        //delete status
                        deleteStatus(statusId);
                    });
                } else {
                    extraInfoService.insert(extraInfo).then(function (response) {
                        onSuccesfulStateChange();
                    }, function (error) {
                        //delete status
                        deleteStatus(statusId);
                    });
                }


            }, function () {
                console.log('Extra info modal dismissed : ' + new Date());
                //geen extrainfo => delete de status
                deleteStatus(statusId);
            });
        }

        function openAnnulatieRedenModal(status) {
            var modalInstance = $uibModal.open({
                animation: true,
                component: 'annulatieRedenModal',
                resolve: {
                    status: status
                }
            });

            modalInstance.result.then(function (annulatieReden) {
                status.annulatieReden = annulatieReden;
                insertState(status);
            }, function () {
                console.log('Annulatiereden info modal dismissed : ' + new Date());
                //geen extrainfo => delete de status
                deleteStatus(statusId);
            });
        }
            
        function openOplossingModal(status) {
            var modalInstance = $uibModal.open({
                animation: true,
                component: 'oplossingModal',
                resolve: {
                    status: status
                }
            });
         
            modalInstance.result.then(function (oplossing) {
                ctrl.issue.oplossing = oplossing;
                issueService.update(ctrl.issue);
                insertState(status);
            }, function () {
                console.log('Solution info modal dismissed : ' + new Date());
                //geen extrainfo => delete de status
                deleteStatus(statusId);
            });
        }

        function deleteStatus(statusId) {
            issueStatusService.remove(statusId).then(function (response) {
                toaster.warning("De status werd verwijderd omdat bijkomende informatie niet bewaard kon worden");
            }, function (response) {
            });
        }

        function onSuccesfulStateChange() {
            //getIssue($stateParams.issueId);
            //getPossibleStatussen($stateParams.issueId);
            //$rootScope.$broadcast('refresh');
            $state.go('issue.list');
        }

        function getPossibleStatussen(issueId) {
            ctrl.loadingStatussen = true;
            statusService.getPossibleStatussen(issueId).then(function (response) {
                ctrl.newStates = response;
            }, function (error) {
            }).finally(function () {
                ctrl.loadingStatussen = false;
            });
        }

        init();

        function init() {
            getIssue($stateParams.issueId);
            getPossibleStatussen($stateParams.issueId);
        }
    }
})();