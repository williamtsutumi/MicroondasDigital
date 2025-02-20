Olá, esta é minha solução para o teste de desenvolvimento do micro-ondas digital, atendendo aos requisitos do nível 3.

Execução:
- O projeto MicroondasDigital é a parte visual
- O projeto Presentation é a API
- Ambos precisam ser executados simultâneamente para o funcionamento correto da aplicação.

Após executado, o visual pode ser conferido em http://localhost:5206/
Para a criação de programa customizado, não há interface de usuário. Para testar, pode abrir a página do Swagger: https://localhost:7110/swagger/index.html, no endpoint de POST https://localhost:7110/Programa
Exemplos de corpo da requisição que podem ser utilizados:
```javascript
{
  "nome": "Vegetais",
  "alimento": "Vegetais e legumes",
  "tempo": "0.00:02:00",
  "potencia": 4,
  "stringAquecimento": "+"
}
```
```javascript
{
  "nome": "Arroz",
  "alimento": "Arroz e grãos",
  "tempo": "0.00:03:00",
  "potencia": 6,
  "stringAquecimento": "=",
  "instrucoes": "Recomenda-se espalhar uniformemente no prato para um aquecimento mais uniforme."
}
```
