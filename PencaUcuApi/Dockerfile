# Use the .NET SDK image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy files
COPY . .

# Restore dependencies
RUN dotnet restore


# Specify the entry point
CMD ["dotnet", "watch", "run", "--project", "PencaUcuApi.csproj", "--urls", "http://*:80"]
