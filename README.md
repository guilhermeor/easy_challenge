[![Build Status](https://api.travis-ci.org/guilhermeor/easy_challenge.png)](https://travis-ci.org/guilhermeor/easy_challenge)

# easy_challenge

## Tecnologias utilizadas: 
- NET 5.0
- Swagger
- Jaeger
- Redis
- Hangfire
- EC2 (Amazon AWS)

## Como Executar localmente
### Requisitos:
- Docker
- Docker Compose

### Instrução
- ```docker-compose -f easy_challenge.yml up -d```

A instrução acima irá preparar o ambiente com a API, Redis e Jaeger

## Como acessar
A aplicação está hospedada na AWS - Region de Ohio (us-east-2). Foi definido um grupo de segurança, em que as portas 16686 (Jaeger UI) e 8080 (API) estão abertas.

Para acessar o swagger da api e o endpoint que lista os investimentos do portifólio, utilize o endereço com IP elástico: 
- [Swagger](http://3.140.162.51:8080/docs)
- [Portfolio](http://3.140.162.51:8080/v1/portfolio)

Também foi utilizado o hangfire para executar jobs de limpeza e update de cache. O Job de update está configurado através de uma expressão cron ("0 0 0 ? * *") para que seja executado todo dia "as 00:00. Contudo, o job de limpeza só será executado manualmente pela interface do hangfire.

Os registros cacheados expiram no horário 00:00 e também são inseridos utilizando como parte da composição do nome da chave a data atual. Dessa forma adiciona-se outra proteção para que se utilize apenas dados cacheados do dia atual.

O endereço para acessar a interface do hangfire é:
- [Hangfire](http://3.140.162.51:8080/hangfire)

## Logs e monitoria
Utilizou-se o Jaeger para esse propósito. Através dele é possível realizar o tracing do request, assim como buscar logs. A aplicação está configurada para filtrar registros no Jaeger utilizando as tags:
- cache=true. Retornará, respectivamente, os requests que utilizaram o cache para responder a solicitação
- cache=false. Retornará, respectivamente, os requests que não utilizaram o cache para responder a solicitação
- statusCode = 400 ou statusCode = 500. Retornará, respectivamente, os requests em que ocorreram algum erro durante a solicitação

O endereço para acesso à interface do Jaeger é:
- [Jaeger](http://3.140.162.51:16686/search)

## Cache utilizado
Para se armazenar os dados a serem cacheados, utilizou-se o Redis. Esse foi acessado dentro da aplicação, através da interface IDistributedCache.

## Considerações
- Para o cálculo do resgate do investimento, ao verificar se ele está com até 3 meses pra vencer, foi considerado 3 meses como 90 dias.
- Se a data de vencimento de um investimento já passou, considerou-se o valor de resgate igual ao valor total
