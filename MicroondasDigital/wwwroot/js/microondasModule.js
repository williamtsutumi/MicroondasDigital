var app = angular.module('microondasModule', []);
app.controller('microondasController', function ($scope, $http) {

    $scope.progresso = "";

    $scope.onInit = function () {
        $http.get("http://localhost:5144/Programa/programas-padroes").then(
            function (response) {
                $scope.programasPadroes = response.data;
                $scope.programasPadroes.forEach((prog) => {
                    prog.tempo = parseInt(prog.tempo.split(":")[1]) * 60 + parseInt(prog.tempo.split(":")[2]);
                });
            },
            function (response) {
                alert(response.data.error);
            }
        );

        $http.get("http://localhost:5144/Programa/programas-customizados").then(
            function (response) {
                $scope.programasCustomizados = response.data.programas;
                $scope.programasCustomizados.forEach((prog) => {
                    prog.tempo = parseInt(prog.tempo.split(":")[1]) * 60 + parseInt(prog.tempo.split(":")[2]);
                });
            },
            function (response) {
                alert(response.data.error);
            }
        );
    }

    // Caso esteja começando um programa (customizado ou pré-definido),
    // não precisa fazer request pois os dados já foram obtidos quando a página foi carregada
    $scope.comecaPrograma = function (programa) {
        $scope.programa = programa;

        $scope.tempo = programa.tempo;
        $scope.potencia = programa.potencia;

        $scope.blockInputs = true;
        $scope.estaPausado = false;
        $scope.estaAquecendo = true;
        $scope.progresso = "";
        setAquecimentoInterval($scope.programa.stringAquecimento);
    };

    $scope.submit = function () {
        if ($scope.estaPausado || $scope.programa !== undefined) {
            $scope.estaPausado = false;
            $scope.estaAquecendo = true;
            setAquecimentoInterval($scope.programa === undefined ? "." : $scope.programa.stringAquecimento);
            return;
        }

        $scope.erro = "";
        $scope.progresso = "";

        var request = makeRequest(parseInt($scope.tempo), parseInt($scope.potencia), $scope.programa === undefined ? undefined : $scope.programa.nome);
        $http(request).then(
            function (response) {
                var tempoSegundos = parseInt(response.data.tempo.split(":")[2]);
                var tempoMinutos = parseInt(response.data.tempo.split(":")[1]);
                var tempoTotal = tempoSegundos + tempoMinutos * 60;
                $scope.tempo = tempoTotal;

                $scope.potencia = response.data.potencia;
                $scope.estaAquecendo = true;

                setAquecimentoInterval(".");
            },
            function (response) {
                alert(response.data.error);
            }
        );
    };

    $scope.pausa = function () {
        if ($scope.estaPausado) {
            $scope.estaPausado = false;
            $scope.estaAquecendo = false;
            $scope.blockInputs = false;
            $scope.programa = undefined;
            $scope.progresso = "";
            $scope.tempo = "";
            $scope.potencia = "";
            return;
        }

        // Ainda nao tem nenhum progresso, apenas limpa os inputs
        if (!$scope.estaAquecendo) {
            $scope.tempo = NaN;
            $scope.potencia = NaN;
            $scope.progresso = "";
            return;
        }

        $scope.estaPausado = true;
        $scope.estaAquecendo = false;
        clearInterval($scope.interval);
    }

    function makeRequest(tempo, potencia, nomeDoPrograma) {
        var request = {
            method: 'POST',
            url: 'http://localhost:5144/Microondas/',
            data: {
                tempo: tempo,
                potencia: potencia,
                nomeDoPrograma: nomeDoPrograma
            }
        };
        if (isNaN(tempo) && isNaN(potencia)) {
            request.url += "inicio-rapido";
        }
        else if ($scope.estaAquecendo) {
            request.url += "acrescento";
        }
        else {
            $scope.progresso = "";
            request.url += "iniciar";
        }
        return request;
    }

    function setAquecimentoInterval(stringAquecimento) {
        clearInterval($scope.interval);

        $scope.interval = setInterval(() => {
            $scope.progresso = $scope.progresso + " " + stringAquecimento.repeat($scope.potencia);
            $scope.tempo--;
            $scope.$apply();
        }, 1000);

        setTimeout(() => {
            clearInterval($scope.interval);
            if ($scope.tempo == 0) {
                $scope.progresso += " Aquecimento concluído";
                $scope.programa = undefined;
                $scope.estaPausado = false;
                $scope.estaAquecendo = false;
                $scope.tempo = NaN;
                $scope.potencia = NaN;
            }
            $scope.$apply();
        }, 1000 * $scope.tempo);
    }
});