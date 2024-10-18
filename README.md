# Mongo.TestContainer
![image](https://github.com/user-attachments/assets/01dc3db4-f534-4b53-8337-0f1b0002a048)

## Description

This project implements a web application using Docker, MongoDB, and the Repository Pattern. 
The application utilizes Testcontainers.MongoDb to spin up a temporary MongoDB instance with 
seed data for testing purposes on how docker can be used in practical case.

## Features
### Repository Pattern: 
Clean separation of concerns, with data access managed through MongoDB repositories.
### Docker Integration: 
The web application is containerized and can be run with Docker.
### Temporary Seed Data: 
On application startup, seed data is inserted into the temporary MongoDB instance.
### Testcontainers.MongoDb: 
MongoDB is spun up in a Docker container during testing, ensuring an isolated and consistent testing environment.
### Logs:
Docker Desktop logs are exposed as endpoints for easier debugging.

## Setup

To run this project, follow these steps:

1. Clone the repository.
2. Open the project and set Mongo.TestContainer as Startup project.
3. Run Docker Desktop and make sure it is running as desktop-linux.
4. Build and run the project.

## Contributing

Contributions are welcome! If you would like to contribute to this project, please follow these guidelines:

1. Fork the repository.
2. Create a new branch.
3. Make your changes.
4. Test your changes.
5. Submit a pull request.


## Contact

If you have any questions or suggestions, please feel free to open Issue.
