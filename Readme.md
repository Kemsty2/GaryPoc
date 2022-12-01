# Elsa POC

This repository contains the source code of a proof of concept on how to build an EDI software using [Elsa](https://elsa-workflows.github.io/)

## Table of Contents

- [Elsa POC](#elsa-poc)
  - [Table of Contents](#table-of-contents)
  - [Requirements](#requirements)
  - [Installation](#installation)
    - [Clone the project](#clone-the-project)
    - [Start keycloak with docker](#start-keycloak-with-docker)
    - [Start Elsa Dashboard](#start-elsa-dashboard)
    - [Start ElsaEdiBackend](#start-elsaedibackend)
    - [Start ProcessEndpoint](#start-processendpoint)

## Requirements

To setup this project on your personal computer, make sure your computer meets the following requirements.

- [Visual Studio 2022 or VsCode](https://visualstudio.microsoft.com/vs/)
- [.Net 6 SDK and Runtine](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Firefox or Google Chrome Navigator](https://www.google.com/intl/fr/chrome/)
- [Install Docker and Docker Compose](https://docs.docker.com/desktop/install/windows-install/)

Follow the link of each requirements to install that on your computer

## Installation

### Clone the project

After installing the requirements, simply clone the project from devops repository

```ps
PM> git clone https://github.com/Kemsty2/GaryPoc.git
```

### Start keycloak with docker

```ps
PM> docker run -d -p 3125:8080 -e KEYCLOAK_USER=admin -e KEYCLOAK_PASSWORD=admin -e KEYCLOAK_IMPORT=/tmp/elsa-realm.json -v ./elsa-realm.json:/tmp/elsa-realm.json jboss/keycloak
```

### Start Elsa Dashboard

Navigate through the project folder and type the following command

```ps
PM> cd ElsaDesigner\elsa-workflows-studio
PM> npm install
```

After the npm package is successfully install, type the following command

```ps
PM> npm start
```

This command will start the dashboard and navigate to [https://localhost:3125](https://localhost:3125) to access it.

Connect to the dashboard using the following credential

- **username**: user_test
- **password**: user_test

### Start ElsaEdiBackend

Navigate through the project folder and type the following command

```ps
PM> cd ElsaEdiBackend\ElsaEdiBackend
PM> dotnet restore
```

After the nuget package is successfully restored, type the following command

```ps
PM> dotnet run
```

This command will start the backend and you can access the swagger by navigating to [https://localhost:7193/swagger](https://localhost:7193/swagger).

You can test the swagger by using the following credential

- **username**: user_test
- **password**: user_test
  
### Start ProcessEndpoint

Navigate through the project folder and type the following command

```ps
PM> cd ProcessEndpoint\ProcessEndpoint
PM> dotnet restore
```

After the nuget package is successfully restored, type the following command

```ps
PM> dotnet run
```

This command will start the backend and you can access the swagger by navigating to [https://localhost:7058](https://localhost:7058).

You can test the swagger by using the following credential

- **username**: user_test
- **password**: user_test
