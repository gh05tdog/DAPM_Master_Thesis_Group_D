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

## Running the Application Locally

### Prerequisites

- **Git**: Ensure Git is installed to clone the repository.
- **Docker**: Install Docker. Refer to the [Docker installation guide](https://docs.docker.com/engine/install/) for details.

### Setup the Application

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

### Start the Application

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

- **Front-end Application**: Open https://localhost in your web browser.
- **Keycloak Front-end Service**: Open https://localhost:8888 in your web browser.

### API Interaction

- The Client API swagger is available at https://localhost/api/clientapi/swagger.
- The Peer API swagger is available at https://localhost/api/peerapi/swagger.
- The Access Control API swagger is available at https://localhost/api/accesscontrol/swagger.

## Running the Application on the VM

### Prerequisites
Before running the application on the virtual machine, ensure the following:
- **VPN**: Access to the VM will require using the DTU VPN.
- **Access to the VM**: Verify that you have access to the VM at se2-d.compute.dtu.dk either by accessing https://se2-d.compute.dtu.dk or using ssh to connect to it.
- **Jenkins Setup**: Jenkins must be installed and configured on the VM with the appropriate build jobs set up, which is configured here in the Jenkinsfile. Refer to the [Jenkins installation guide](https://www.jenkins.io/doc/book/installing/) for more details.

### Deploying the Application Using Jenkins (DTU VPN Required)
- Log in to Jenkins on the VM by navigating to http://se2-d.compute.dtu.dk:8080/.
- Locate the 'deploy' job and click 'Build Now' which will checkout 'main' and deploy the front-end and back-end.
- Once the builds are complete, the application will be deployed and running on the VM.

### Access the Application on the VM (DTU VPN Required)

- **Front-end Application**: Open https://se2-d.compute.dtu.dk in your web browser.
- **Keycloak Front-end Service**: Open https://se2-d.compute.dtu.dk:8888 in your web browser.

### API Interaction on the VM (DTU VPN Required)

- The Client API swagger is available at https://se2-d.compute.dtu.dk/api/clientapi/swagger.
- The Peer API swagger is available at https://se2-d.compute.dtu.dk/api/peerapi/swagger.
- The Access Control API swagger is available at https://se2-d.compute.dtu.dk/api/accesscontrol/swagger.

### Alternative: Running the Application Manually on the VM (DTU VPN Required)
If you prefer to deploy the application manually instead of using Jenkins, you can follow the same steps as for local deployment:

1. Log in to the VM via SSH.
2. Clone or navigate to the appropriate repositories on the VM.
3. Build and start the Docker containers for both the front-end and back-end services using the commands provided in the local setup instructions.

Once deployed, the application will be accessible at the same URLs as specified above.

## Predefined Users and Passwords

The application contains several predefined user logins with different roles:

| Username           | Password  | Role                                                                 |
|--------------------|-----------|----------------------------------------------------------------------|
| Manager            | password  | Manager (OrganizationManager, PipelineManager, ResourceManager, RepositoryManager) |
| OrganizationManager| password  | OrganizationManager                                                 |
| PipelineManager    | password  | PipelineManager                                                     |
| ResourceManager    | password  | ResourceManager                                                     |
| RepositoryManager  | password  | RepositoryManager                                                   |
| test               | test      |                                                                      |

## Keycloak Admin Login
**Username:** admin
**Password:** admin

## Jenkins Admin Login
**Username:** admin
**Password:** ffe149e7f8794de9aeeb8c76011cac07

## Github Authors to Student Numbers

Github Author, Student Number
| GitHub Author     | Student Number |
|--------------------|----------------|
| gh05tdog          | s224754        |
| HLLissau          | s204436        |
| LucasJuel         | s224742        |
| Martin-Surlykke   | s224793        |
| Pakkutaq          | s224750        |
| ravvnen           | s233478        |
| RasmusUK          | s233787        |
| ssschoubye        | s224756        |
| xDelux            | s195467        |

## Licensing

This project is licensed under the Apache License, Version 2.0. For details, see the [Apache 2.0 License](https://www.apache.org/licenses/LICENSE-2.0).

