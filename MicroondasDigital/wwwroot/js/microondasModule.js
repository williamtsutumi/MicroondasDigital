var app = angular.module('microondasModule', []);
app.controller('microondasController', function ($scope, $http) {

    $scope.tempo = NaN;
    $scope.potencia = NaN;
    $scope.erro = "";
    $scope.estaAquecendo = false;

    $scope.onInit = function () {
        var request = {
            method: 'GET',
            url: 'http://localhost:5144/Programa/get-programas-padroes'
        };
        $http(request).then(
            function (response) {
                $scope.programasPadroes = response.data;
                $scope.programasPadroes.forEach((prog) => {
                    var tempoInt = parseInt(prog.tempo.split(":")[1]) * 60 + parseInt(prog.tempo.split(":")[2]);
                    prog.tempo = tempoInt;
                });
            },
            function (response) {
                console.log(response);
            }
        );
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
        if (isNaN(tempo) && isNaN(potencia))
            request.url += "inicio-rapido";
        else if ($scope.estaAquecendo) {
            $scope.progresso = " ";
            request.url += "acrescento";
        }
        else {
            $scope.progresso = " ";
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
                $scope.estaPausado = false;
                $scope.estaAquecendo = false;
                $scope.tempo = NaN;
                $scope.potencia = NaN;
            }
            $scope.$apply();
        }, 1000 * $scope.tempo);
    }

    $scope.submit = function (programa) {
        if (programa !== undefined)
            $scope.programa = programa;

        $scope.erro = "";

        if ($scope.estaPausado) {
            $scope.estaPausado = false;
            $scope.estaAquecendo = true;
            setAquecimentoInterval($scope.programa === undefined ? "." : $scope.programa.stringAquecimento);
            return;
        }

        var request;
        if (programa != undefined) {
            $scope.blockInputs = true;
            request = makeRequest(programa.tempo, programa.potencia, programa.nome);
        }
        else {
            request = makeRequest(parseInt($scope.tempo), parseInt($scope.potencia), $scope.programa === undefined ? undefined : $scope.programa.nome);
        }

        $http(request).then(
            function (response) {
                var tempoSegundos = parseInt(response.data.tempo.split(":")[2]);
                var tempoMinutos = parseInt(response.data.tempo.split(":")[1]);
                var tempoTotal = tempoSegundos + tempoMinutos * 60;
                $scope.tempo = tempoTotal;

                if (!$scope.tempo)
                    $scope.tempo = tempoTotal;

                $scope.potencia = response.data.potencia;
                $scope.estaAquecendo = true;

                setAquecimentoInterval(programa === undefined ? "." : programa.stringAquecimento);
            },
            function () {
                $scope.erro = "Ocorreu algum erro inesperado!";
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
        if (!$scope.estaAquecendo) {
            $scope.tempo = "";
            $scope.potencia = "";
            return;
        }
        $scope.estaPausado = true;
        $scope.estaAquecendo = false;
        clearInterval($scope.interval);
    }
});