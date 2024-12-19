# Distributed Architecture for Process Mining Platform
This is the repository that holds the implementation of the solution proposed in the master's thesis *A Distributed Architecture for Process Mining: Deploying and Executing Process Mining Pipelines* by Sergi Doce and Hamed Tounsi at Danmarks Tekniske Universitet.

This is the implementation for a DAPM Peer, which, in conjunction with other DAPM Peers, form the DAPM Platform. This platform allows users to share resources and build and execute process mining pipelines with them. 

## Microservices
The project is developed using C# and .NET and is based on microservices. Each microservice has its own project. The following are the microservices implementing the features of DAPM:

- DAPM.ResourceRegistryMS.Api (Resource Registry Microservice)
- DAPM.PipelineOrchestratorMS.Api (Pipeline Orchestrator Microservice)
- DAPM.RepositoryMS.Api (Repository Microservice)
- DAPM.OperatorMS.Api (Operator Microservice)

Besides these microservices, the DAPM.Orchestrator project holds the implementation for the DAPM Orchestrator, which is a service that orchestrates the execution of the different microservices. Finally, the DAPM.ClientApi and DAPM.PeerApi projects implement the Client API and Peer API respectively.

The RabbitMQLibrary folder holds all the code related to asynchronous communication using message queues. It contains the consumers and producers base clases, as well as the message models.

## Projects Structure and Software Principles
In this project we have made great emphasis on the decoupling of the software, the single responsability principle, and the dependency inversion principle. Because of this, the implementation in each microservice is often divided in different layers:

- External interface layer, handling communication with the exterior. In this case this is implemented by RabbitMQ Consumers in the Consumers folder.
- Services layer, implementing business logic and using the data models specific to that microservice, leaving behind the models used in the API.
- Repositories layer, implementing communication with any data persistance service such as databases.

All these layers are abstracted using interfaces, so their implementations can be changed easily.

Due to time and resource limitations during the project, these principles are not always applied. Despite this, this principles have allowed us to implement an easily extensible software for future developers.

### Prerequisites

- **Git**: Ensure Git is installed to clone the repository.
- **Docker**: Install Docker. Refer to the [Docker installation guide](https://docs.docker.com/engine/install/) for details.

### Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/gh05tdog/DAPM_Master_Thesis_Group_D
   ```

2. Navigate to the `DAPM-Frontend` directory and build the containers:
   ```
   cd DAPM-Frontend
   docker compose build
   ```

3. Navigate to the `DAPM` directory and build the containers:
   ```
   cd DAPM
   docker compose build
   ```

## Running the Application

After the installation, execute the following steps to start the application:

1. Start the frontend:
   ```
   cd DAPM-Frontend
   docker compose up -d
   ```

2. Start the backend services:
   ```
   cd DAPM
   docker compose up -d
   ```

Once the commands are executed successfully, the application will be running.

### Access the Application

- **Front-end Application**: Open [http://localhost:3000](http://localhost:3000) in your web browser.
- **Keycloak Front-end Service**: Open [http://localhost:8888](http://localhost:8888) in your web browser.

### API Interaction

- The Client API is available at [http://localhost:5000/swagger/v1/swagger.json](http://localhost:5000/swagger/v1/swagger.json).

## Licensing

This project is licensed under the Apache License, Version 2.0. For details, see the [Apache 2.0 License](https://www.apache.org/licenses/LICENSE-2.0).

