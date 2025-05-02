FROM mcr.microsoft.com/dotnet/sdk:7.0

ENV PATH=$PATH:/usr/share/dotnet

RUN apt-get update && apt-get install -y curl git

RUN dotnet --version
