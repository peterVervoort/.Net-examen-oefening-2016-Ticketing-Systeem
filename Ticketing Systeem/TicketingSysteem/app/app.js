/// <reference path="C:\Users\Sven\Source\Repos\Ticketing Systeem\TicketingSysteem\app_content/login/login.html" />
/// <reference path="app.js" />
var app = angular.module('app', [
        // Angular modules 
        'ui.router',
        'ui.bootstrap',
        'ngCookies',

        // Custom modules 

        // 3rd Party Modules
        'toaster',
        'pascalprecht.translate'
]);

app.config(['$translateProvider', '$stateProvider', '$urlRouterProvider', '$httpProvider', 'Roles', function ($translateProvider, $stateProvider, $urlRouterProvider, $httpProvider, Roles) {

    //Start IE bug fix
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    //disable IE ajax request caching
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    // extra
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
    //End IE bug fix


    //URL Reroute
    $urlRouterProvider
    // The `when` method says if the url is ever the 1st param, then redirect to the 2nd param
    //.when('/c?id', '/contacts/:id')
    //.when('/user/:id', '/contacts/:id')

    // If the url is ever invalid, e.g. '/asdf', then redirect to '/' aka the home state
    .otherwise('/issue');


    //State Config
    $stateProvider

    ////////////////////////////////////////////
    //          LOGIN
    ////////////////////////////////////////////
        .state("login", {
            url: "/account",
            abstract: true,
            template: '<ui-view/>'
        })
    .state("login.login", {
        url: "/login/:target",
        controller: 'LoginController',
        controllerAs: 'ctrl',
        templateUrl: getTemplate('login/login.html')
    })
    .state("login.accessdenied", {
        url: "/accessdenied",
        templateUrl: getTemplate('login/accessdenied.html')
    })

    ////////////////////////////////////////////
    //          USERS
    ////////////////////////////////////////////
    .state("users", {
        url: "/users",
        abstract: true,
        template: '<ui-view/>'
    })
    .state("users.list", {
        url: "",
        controller: 'UserOverviewController',
        controllerAs: 'ctrl',
        role: [Roles.administrator],
        templateUrl: getTemplate('user/userOverview.html')
    })
    .state("users.new", {
        url: "/new",
        controller: 'UserCreateController',
        role: [Roles.administrator],
        controllerAs: 'ctrl',
        templateUrl: getTemplate('user/userCreate.html')
    })
    .state("users.edit", {
        url: "/edit/:userId",
        controller: 'UserEditController',
        role: [Roles.administrator],
        controllerAs: 'ctrl',
        templateUrl: getTemplate('user/userCreate.html')
    })
    .state("users.detail", {
        url: "/:userId",
        controller: 'UserDetailController',
        controllerAs: 'ctrl',
        role: [Roles.administrator],
        templateUrl: getTemplate('user/userDetail.html')
    })

    ////////////////////////////////////////////
    //          ISSUES
    ////////////////////////////////////////////
    .state("issue", {
        url: "/issue",
        abstract: true,
        template: '<ui-view/>'
    })
    .state("issue.list", {
        url: "",
        controller: 'IssueOverviewController',
        controllerAs: 'ctrl',
        templateUrl: getTemplate('issue/issueOverview.html')
    })
    .state("issue.new", {
        url: "/new",
        controller: 'IssueCreateController',
        controllerAs: 'ctrl',
        role: [Roles.dispatcher, Roles.manager, Roles.administrator, Roles.gebruiker, Roles.solver],
        templateUrl: getTemplate('issue/issueCreate.html')
    })
    .state("issue.detail", {
        url: "/:issueId",
        controller: 'IssueDetailController',
        controllerAs: 'ctrl',
        templateUrl: getTemplate('issue/issueDetail.html')
    })

    ////////////////////////////////////////////
    //          ISSUESTATUSSEN
    ////////////////////////////////////////////
    .state("issueStatus", {
        url: "/issuestatus",
        abstract: true,
        template: '<ui-view/>'
    })
    .state("issueStatus.list", {
        url: "",
        controller: 'IssueStatusOverviewController',
        controllerAs: 'ctrl',
        templateUrl: getTemplate('issuestatus/issueStatusOverview.html')
    })
    .state("issueStatus.new", {
        url: "/new",
        controller: 'IssueStatusCreateController',
        controllerAs: 'ctrl',
        role: [Roles.dispatcher, Roles.manager, Roles.administrator, Roles.gebruiker, Roles.solver],
        templateUrl: getTemplate('issuestatus/issueStatusCreate.html')
    })
    .state("issueStatus.detail", {
        url: "/:issueStatusId",
        controller: 'IssueStatusDetailController',
        controllerAs: 'ctrl',
        templateUrl: getTemplate('issuestatus/issueStatusDetail.html')
    })

     ////////////////////////////////////////////
    //          EXTRA INFO
    ////////////////////////////////////////////
    .state("extrainfo", {
        url: "/extrainfo",
        abstract: true,
        template: '<ui-view/>'
    })
    .state("extrainfo.list", {
        url: "",
        controller: 'ExtraInfoOverviewController',
        controllerAs: 'ctrl',
        templateUrl: getTemplate('extrainfo/extraInfoOverview.html')
    })
    .state("extrainfo.new", {
        url: "/new",
        controller: 'ExtraInfoCreateController',
        controllerAs: 'ctrl',
        templateUrl: getTemplate('extrainfo/extraInfoCreate.html')
    })
    .state("extrainfo.detail", {
        url: "/:extraInfoId",
        controller: 'ExtraInfoDetailController',
        controllerAs: 'ctrl',
        role: [Roles.dispatcher, Roles.manager, Roles.administrator],
        templateUrl: getTemplate('extrainfo/extraInfoDetail.html')
    });

    var translationEn = {
        //Main Template MT
        MTLoggedInAs: 'Logged in as',
        MTLogout: 'Logout',
        MTLogin: 'Login',
        MTTitle: 'TicketingSystem',
        MTUser: 'User',
        MTAdmin: 'Admin',
        MTIssues: 'Issues',
        //Main Template Sidebar MTS
        MTSIssues: 'Issues',
        MTSAdmin: 'Admin',
        MTSUsers: 'Users',
        //Login screen LOG
        LOGTicketingSysteemTitel: 'TICKETINGSYSTEM',
        LOGLogIntoYourAccount: 'Log in to your account',
        LOGPlaceHolderUsername: 'Username',
        LOGPlaceHolderPassword: 'Password',
        LOGLogin: 'Login',
        //AccesDenied screen AD
        ADAccesDenied: 'Acces denied',
        ADIsuffientPrivileges: 'You have reached this page because you do not have sufficient privileges to view the content you have requested.',
        ADAdminContact: 'Please contact an administrator if you believe this is an error.',
        //UserCreate Screen UC
        UCUsers: 'Users',
        UCNew: 'New',
        UCNewUser: 'New user',
        UCFirstName: 'First name',
        UCFirstNameRequired: 'First name is required.',
        UCName: 'Name',
        UCNameRequired: 'Name is required.',
        UCEmail: 'Email',
        UCEmailPleaseProvide: 'Please provide an email address.',
        UCPhone: 'Phone',
        UCMobile: 'Mobile',
        UCPassword: 'Password',
        UCPasswordLenght: 'Password must be at least 8 characters',
        UCChief: 'Supervisor',
        UCRole: 'Role',
        UCRolRequired: 'Role is required',
        UCSave: 'Save',
        UCCancel: 'Cancel',
        //UserDetail Screen UD
        UDDetail: 'Detail',
        UDDelete: 'Delete',
        UDEdit: 'Edit',
        //UserOverview Screen UO
        UOOverview: 'Overview',
        UOAddNew: 'Add new',
        //IssueStatusCreate Screen ISC
        ISCIssueStatus: 'Issue State',
        ISCNew: 'New',
        ISCNewIssueStatus: 'New Issue State',
        ISCAnnulationRaison: 'Annulation Reason',
        ISCStatus: 'State',
        ISCStatusIsRequired: 'State is required',
        ISCSolver: 'Solver',
        //IssueStatusDetail Screen ISD
        ISDDetail: 'Detail',
        ISDCreation: 'Creation',
        //IssueStatusOverview Screen ISO
        ISOStatusHistory: 'State History',
        ISOAddNew: 'Add New',
        //IssueCreate Screen IC
        ICIssues: 'Issues',
        ICNew: 'New',
        ICNewIssue: 'New Issue',
        ICTitel: 'Title',
        ICTitelIsRequired: 'Title is required.',
        ICBeschrijving: 'Description',
        ICBeschrijvingIsRequired: 'Description is required.',
        ICNiveau: 'Level',
        ICDate: 'Date',
        ICSave: 'Save',
        ICCancel: 'Cancel',
        //IssueDetail Screen ID
        IDDetail: 'Detail',
        IDCreateDate: 'Creation Date',
        IDUser: 'User',
        IDSolver: 'Solver',
        IDSolution: 'Solution',
        //IssueOverview Screen IO
        IOOverview: 'Overview',
        IOAddNew: 'Add New',
        IOFilterUser: 'User',
        //ExtraInfoCreate Screen EIC
        EICExtraInfo: 'Extra Info',
        EICNew: 'New',
        EICNewExtraInfo: 'New Extra Info',
        EICQuestion: 'Question',
        EICQuestionIsRequired: 'Question is required.',
        EICAnswerIsRequired: 'Answer is required',
        EICAnswer: 'Answer',
        EICSave: 'Save',
        EICCancel: 'Cancel',
        //ExtraInfoDetail Screen EID
        EIDDetail: 'Detail',
        EIDDelete: 'Delete',
        //ExtraInfoOverview Screen EIO
        EIOOverview: 'Overview',
        EIOAddNew: 'Add New',
        //UserOverviewController Headers UOC
        UOCFirstName: 'First Name',
        UOCName: 'Last Name',
        UOCEmail: 'Email',
        UOCPhone: 'Phone',
        UOCMobile: 'Mobile',
        UOCChief: 'Supervisor',
        UOCRole: 'Role',
        //IssueStatusOverviewController Headers ISOC
        ISOCTitel: 'Titel',
        ISOCIssueStatusNiveau: 'Issue Status Level',
        ISOCIssueStatusDate: 'Issue Status Date',
        ISOCCreationDate: 'Creation Date',
        ISOCGebruiker: 'User',
        ISOCSolver: 'Solver',
        ISOCOplossing: 'Solution',
        //IssueOverviewController Headers IOC
        IOCStatus: 'State',
        IOCTitel: 'Title',
        IOCIssueNiveau: 'Issue Level',
        IOCGebruiker: 'User',
        IOCIssueDate: 'Issue Date',
        //ExtraInfoOverviewController EIOC
        EIOCVraag: 'Question',
        EIOCAntwoord: 'Response',
        //StatusHistoriekDirectives
        SHDCreation: 'Creation',
        SHDBeschrijving: 'Description',
        SHDSolver: 'Solver',
        //SolverSelectorModal SSM
        SSMSolverRequired: 'Solver is required.',
        //AnnulatieRedenModal AR
        ARExtraInfo: 'Cancelation reason',
        ARReason: 'Cancelation reason',
        ARReasonRequired: 'Cancelation reason is required',
        //OplossingModal OM
        OMTitle: 'Solution',
        OMRequired: 'Solution is required',

        //Extras
        CurrentState: 'Current State',

        //Enum 
        Gebruiker: 'User',
        Manager: 'Manager',
        Dispatcher: 'Dispatcher',
        Solver: 'Solver',
        Administrator: 'Administrator',

        Nieuw: 'New',
        Toegewezen: 'Appointed',
        InBehandeling: 'In progress',
        Opgelost: 'Solved',
        Afgesloten: 'Closed',
        Canceled: 'Canceled',
        ExtraInfo: 'Extra information needed',
        Geweigerd: 'Refused',

        Low: 'Low',
        Medium: 'Medium',
        High: 'High',
        Immediate: 'Immediate',

        //Toasters
        ToasterExtraInfoRemoved: 'Extra info was removed',
        ToasterUserRemoved: 'User was removed',
        ToasterFailLoginData: 'Could not remove login data',
        ToasterFailUserData: 'Could not get user data',
        HttpError: 'A communication error occured',
        SolverRequiredStateChange: 'Solver is required for this state change',
        CheckUsernamePassword: 'Check username or password and try again'
    }

    var translationNL = {
        //Main Template MT
        MTLoggedInAs: 'Ingelogd als',
        MTLogout: 'Afmelden',
        MTLogin: 'Aanmelden',
        MTTitle: 'Ticketingsysteem',
        MTUser: 'Gebruiker',
        MTAdmin: 'Admin',
        MTIssues: 'Issues',
        //Main Template Sidebar MTS
        MTSIssues: 'Issues',
        MTSAdmin: 'Admin',
        MTSUsers: 'Gebruikers',
        //Login screen
        LOGTicketingSysteemTitel: 'TICKETINGSYSTEEM',
        LOGLogIntoYourAccount: 'Meld uzelf aan',
        LOGPlaceHolderUsername: 'Gebruikersnaam',
        LOGPlaceHolderPassword: 'Gebruikers',
        LOGLogin: 'Aanmelden',
        //AccesDenied screen AD
        ADAccesDenied: 'Toegang Geweigerd',
        ADIsuffientPrivileges: 'Je bent op deze pagina uitgkomen omdat je niet voldoende rechten hebt om de opgevraagde inhoud te raadplegen.',
        ADAdminContact: 'Contacteer de administrator als je gelooft dat dit een fout is.',
        //UserCreate Screen UC
        UCUsers: 'Gebruikers',
        UCNew: 'Nieuw',
        UCNewUser: 'Nieuwe gebruiker',
        UCFirstName: 'Voornaam',
        UCFirstNameRequired: 'Voornaam is verplicht.',
        UCName: 'Naam',
        UCNameRequired: 'Naam is verplicht.',
        UCEmail: 'E-mail',
        UCEmailPleaseProvide: 'Voorzie een e-mailadres.',
        UCPhone: 'Telefoon',
        UCMobile: 'GSM',
        UCPassword: 'Paswoord',
        UCPasswordLenght: 'Paswoord moet minimum 8 karakters lang zijn.',
        UCChief: 'Verantwoordelijke',
        UCRole: 'Rol',
        UCRolRequired: 'Rol is verplicht',
        UCSave: 'Opslaan',
        UCCancel: 'Annuleren',
        //UserDetail Screen UD
        UDDetail: 'Detail',
        UDDelete: 'Verwijderen',
        UDEdit: 'Bewerken',
        //UserOverview Screen UO
        UOOverview: 'Overzicht',
        UOAddNew: 'Nieuwe Toevoegen',
        //IssueStatusCreate Screen ISC
        ISCIssueStatus: 'Issue Status',
        ISCNew: 'Nieuw',
        ISCNewIssueStatus: 'Nieuw Issue Status',
        ISCAnnulationRaison: 'Annulatie Reden',
        ISCStatus: 'Status',
        ISCStatusIsRequired: 'Status is verplicht',
        ISCSolver: 'Behandelaar',
        //IssueStatusDetail Screen ISD
        ISDDetail: 'Detail',
        ISDCreation: 'Creatie',
        //IssueStatusOverview Screen ISO
        ISOStatusHistory: 'Status Historiek',
        ISOAddNew: 'Voeg nieuwe toe',
        //IssueCreate Screen IC
        ICIssues: 'Issues',
        ICNew: 'Nieuw',
        ICNewIssue: 'Nieuw Issue',
        ICTitel: 'Titel',
        ICTitelIsRequired: 'Titel is verplicht.',
        ICBeschrijving: 'Beschrijving',
        ICBeschrijvingIsRequired: 'Beschrijving is verplicht.',
        ICNiveau: 'Niveau',
        ICDate: 'Datum',
        ICSave: 'Opslaan',
        ICCancel: 'Annuleren',
        //IssueDetail Screen ID
        IDDetail: 'Detail',
        IDCreateDate: 'Creatie Datum',
        IDUser: 'Gebruiker',
        IDSolver: 'Behandelaar',
        IDSolution: 'Oplossing',
        //IssueOverview Screen IO
        IOOverview: 'Overzicht',
        IOAddNew: 'Nieuwe toevoegen',
        IOFilterUser: 'Gebruiker',
        //ExtraInfoCreate Screen EIC
        EICExtraInfo: 'Extra info',
        EICNew: 'Nieuw',
        EICNewExtraInfo: 'Nieuwe Extra Info',
        EICQuestion: 'Vraag',
        EICQuestionIsRequired: 'Vraag is verplicht',
        EICAnswerIsRequired: 'Antwoord is verplicht',
        EICAnswer: 'Antwoord',
        EICSave: 'Opslaan',
        EICCancel: 'Annuleren',
        //ExtraInfoDetail Screen EID
        EIDDetail: 'Detail',
        EIDDelete: 'Verwijderen',
        //ExtraInfoOverview Screen EIO
        EIOOverview: 'Overview',
        EIOAddNew: 'Add New',
        //UserOverviewController Headers UOC
        UOCFirstName: 'Voornaam',
        UOCName: 'Naam',
        UOCEmail: 'E-mail',
        UOCPhone: 'Telefoon',
        UOCMobile: 'GSM',
        UOCChief: 'Verantwoordelijke',
        UOCRole: 'Rol',
        //IssueStatusOverviewController Headers ISOC
        ISOCTitel: 'Titel',
        ISOCIssueStatusNiveau: 'Issue Status niveau',
        ISOCIssueStatusDate: 'Issue Status Datum',
        ISOCCreationDate: 'Aanmaakdatum',
        ISOCGebruiker: 'Gebruiker',
        ISOCSolver: 'Behandelaar',
        ISOCOplossing: 'Oplossing',
        //IssueOverviewController Headers IOC
        IOCStatus: 'Status',
        IOCTitel: 'Titel',
        IOCIssueNiveau: 'Issue Niveau',
        IOCGebruiker: 'Gebruiker',
        IOCIssueDate: 'Issue Datum',
        //ExtraInfoOverviewController EIOC
        EIOCVraag: 'Vraag',
        EIOCAntwoord: 'Antwoord',
        //StatusHistoriekDirectives
        SHDCreation: 'Aanmaak',
        SHDBeschrijving: 'Beschrijving',
        SHDSolver: 'Behandelaar',
        //SolverSelectorModal SSM
        SSMSolverRequired: 'Behandelaar is verplicht.',
        //AnnulatieRedenModal
        ARExtraInfo: 'Annulatiereden',
        ARReason: 'Annulatiereden',
        ARReasonRequired: 'Annulatiereden is verplicht',
        //OplossingModal OM
        OMTitle: 'Oplossing',
        OMRequired: 'Oplossing is verplicht',
        //Extras
        CurrentState: 'Huidige Status',
        //Enums
        Gebruiker: 'Gebruiker',
        Manager: 'Verantwoordelijke',
        Dispatcher: 'Dispatcher',
        Solver: 'Behandelaar',
        Administrator: 'Administrator',

        Nieuw: 'Nieuw',
        Toegewezen: 'Toegewezen',
        InBehandeling: 'In behandeling',
        Opgelost: 'Opgelost',
        Afgesloten: 'Afgesloten',
        Canceled: 'Geannuleerd',
        ExtraInfo: 'Extra informatie nodig',
        Geweigerd: 'Geweigerd',

        Low: 'Laag',
        Medium: 'Medium',
        High: 'Hoog',
        Immediate: 'Dringend',

        //Toasters
        ToasterExtraInfoRemoved: 'Extra info werd verwijderd',
        ToasterUserRemoved: 'Gebruiker werd succesvol verwijderd',
        ToasterFailLoginData: 'Kon de login gegevens niet verwijderen op de identity database',
        ToasterFailUserData: 'Kon de gebruiker gegevens niet ophalen',
        HttpError: 'Er is een fout opgetreden bij communicatie',
        SolverRequiredStateChange: 'Behandelaar is verplicht voor deze overgang',
        CheckUsernamePassword:'Controleer gebruikersnaam en paswoord'
    }

    //Angular-Translate 
    $translateProvider
    .translations('en', translationEn)
    .translations('nl', translationNL)
    .registerAvailableLanguageKeys(['en', 'nl'], {
        'en_US': 'en',
        'en_UK': 'en',
        'nl_NL': 'nl',
        'nl_BE': 'nl',
    })
     .determinePreferredLanguage();
    //.preferredLanguage('en');
}]);

////////////////////////////////////////////
//          BOOT APP
////////////////////////////////////////////
app.run(['$state', '$rootScope', '$http', '$cookies', function ($state, $rootScope, $http, $cookies) {
    //Reload oauth token from cookie 
    var bearer = $cookies.get('TSbearerToken');
    $http.defaults.headers.common.Authorization = 'Bearer ' + bearer;

    //Get user from cookie (if exists)
    var user = $cookies.get('TSuser');
    if (user) {
        var u = JSON.parse(user)
        $rootScope.userEmail = u.email;
        $rootScope.userId = u.id;
        $rootScope.userRole = u.rol;
    }

    //State change => save on rootscope + check if state has roles and user is authorized
    $rootScope.$on('$stateChangeStart', function (e, toState, toParams, fromState, fromParams) {
        $rootScope.currentState = toState;
        $rootScope.currentStateParms = toParams;

        if (!angular.isUndefined(toState.role)) {
            if (angular.isUndefined($rootScope.userRole) || $rootScope.userRole === null) {
                //No Roles found => login
                e.preventDefault();
                $state.go('login.login', { target: toState.name });
            } else {
                var enabled = false;
                for (index = 0; index < toState.role.length; ++index) {
                    var roleState = toState.role[index];
                    if (roleState === $rootScope.userRole) {
                        enabled = true;
                        break;
                    }
                }
                if (!enabled) {
                    //Role not allowed
                    e.preventDefault();
                    $state.go('login.accessdenied');
                }
            }

        }
    });
}]);

//Helper for view template
function getTemplate(route) {
    return '/app_content/' + route;
};