# Docker file Instructions:
# -------------------------

# FROM
# -----------
FROM alpine:3.6

# ENV
# -----------
ENV OS GNU/Linux
ENV KERNEL_RELEASE 3.10.0-862.el7.x86_64
ENV PROCESSOR x86_64

ENV OS=GNU/Linux \
    KERNEL_RELEASE=3.10.0-862.el7.x86_64 \
    PROCESSOR=x86_64

# ARG
# -----------

# Define an argument
ARG KERNEL_RELEASE=3.7.5-201.fc18.x86_64

# Define an environment variable
ENV OS=GNU/Linux \
    KERNEL_RELEASE=${KERNEL_RELEASE} \
    PROCESSOR=x86_64


# RUN
# -----------

# --- v1 ---

FROM alpine:latest

RUN apk -v --update python py-pip 
RUN pip install awscli==1.14.5 
RUN rm /var/cache/apk/* 

WORKDIR /home/aws


# --- v2 ---

FROM alpine:latest

RUN apk -v --update python py-pip && \
    pip install awscli==1.14.5 && \
    rm /var/cache/apk/* 

WORKDIR /home/aws


# COPY
# -----------

# File or directory called my source copied to /my_dir
COPY my_source /my_dir

# File called my_source copied as /my_dir/my_source
# Directory called my_source copied as /my_dir 
COPY my_source /my_dir/

# File or directory called my_source copied as /bar 
# (relative path)
COPY path_to/my_source /my_dir

# All files and directories located at path_to,
# copied to directory /my_dir
COPY path_to/my_* /my_dir/

# File or directory called my_source copied as my_dir
# located relative to previous WORKDIR instruction
COPY my_source my_dir


# CMD
# -----------

# Exec form
#CMD ["apache2ctl", "-D", "FOREGROUND"]


# Shell form
CMD apache2ctl -D FOREGROUND


# ENTRYPOINT
# -------------

# Exec form
#ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]

# Shell form
ENTRYPOINT dotnet run --server.urls http://0.0.0.0:5000


# ONBUILD
# -------------

# Base image
FROM maven:3-jdk-8

RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

ONBUILD ADD . /usr/src/app
ONBUILD RUN mvn install

# Derived image 
FROM maven:3.3-jdk-8-onbuild
CMD ["java","-jar","/usr/src/app/target/demo-1.0-SNAPSHOT-jar-with-dependencies.jar"]

