FROM microsoft/dotnet:latest

WORKDIR /root/
ADD ./app/ ./app/
WORKDIR /root/app

RUN dotnet restore
RUN dotnet build

# run tests on docker build
RUN dotnet test

# run tests on docker run
ENTRYPOINT ["dotnet", "test"]









