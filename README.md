
# Tasty Delivery Microsserviço Pagamento 🍕


App que conecta clientes ao restaurante Tasty, sem passar pela
camada de atendimento presencial.

### :: Buildando e rodando o projeto

**`docker-compose up `**

### :: Acessando a documentação

- Disponível em `localhost:8000/docs` e/ou `localhost:8000/redoc`


- Database: Couchbase - NOSQL
Microsserviço: Tasty_Delivery_Service_PIX



![image](https://github.com/ZuchiVictor/-Tasty_Delivery_PIX/assets/28466435/9439c766-5fdc-4844-b0c5-26f15393664a)

Padrão Saga escolhido: 
O padrão de Orquestração é utilizado por vários motivos:

Controle Centralizado: Um serviço orquestrador centraliza a lógica de negócio e o fluxo de transações, facilitando o controle e monitoramento.

Simplicidade de Implementação: A lógica complexa é gerida por um único serviço, simplificando a coordenação entre microserviços.

Erro e Recuperação: O orquestrador pode gerenciar erros e realizar compensações de maneira coordenada, garantindo a consistência do sistema.

Visibilidade e Rastreamento: A centralização permite melhor rastreamento e visibilidade do estado da saga, essencial para depuração e auditoria.

Este padrão é especialmente útil quando a complexidade do fluxo de trabalho é alta e requer uma coordenação precisa entre os serviços.



Descrição do Diagrama:
User Interface (UI):

O ponto de entrada onde os usuários interagem com o sistema.
API Gateway:

Gerencia todas as solicitações recebidas da UI e distribui para os microserviços apropriados.
Microserviços:

Payment Service: Gerencia as transações de pagamento.
Order Service: Cuida da criação e gestão de pedidos.
Delivery Service: Organiza e rastreia as entregas.
RabbitMQ:

Utilizado para comunicação assíncrona entre os microserviços.
Facilita a implementação da saga.
Databases:

Cada microserviço possui seu próprio banco de dados para manter a independência e a escalabilidade.
Saga Orchestrator:

Coordena o fluxo de transações entre os microserviços, garantindo a consistência e gerenciando a lógica de compensação em caso de falhas.