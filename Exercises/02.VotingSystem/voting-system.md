# Exercise 02 - Voting System

## Voting System Architecture:
![Voting System Architecture](https://github.com/haimkabesa/docker-course/blob/master/Exercises/02.VotingSystem/voting-app-architecture.png)

## Background:

This project is a generic voting system, While the system is active, users of this system can vote for one of the options. At the end of the voting period, the option with the most votings number will win the vote.
however, It worth to mention that the system is still under heavy development.

You are the DevOps engineer that support the VotingSystem dev team.
Your task is to allow the dev team to spin up a local environment of the VotingSystem on their local workstation using docker-compose.

Joe is one of the members of the development team who works on the Voting System project.
Below you can find some useful information on his ToDo list that may help you to accomplish your task.

Joe's ToDo List:
1. Should add services in the docker-compose.yml for Redis and RabbitMQ services:
    1. Redis:
        - Service name: voting.db
        - The container name must be votingdb
        - Must be available in his default port

    1. RabbitMQ:
        - Service name: rabbitmq
        - Must be available in his default ports
        - the service must provide the following environment variables: user, password and vhost (all values should be defaults [look here](https://hub.docker.com/_/rabbitmq/)) 

1. There is a known issue with the x service... before it starts rabbitmq must be up and ready. This constraint is due to the fact that the x service is trying to establish a connection with the rabbitmq service at start time. Unfortunately, no fault tolerance mechanism where implemented yet to handle the exception that is thrown.


Goodluck!
