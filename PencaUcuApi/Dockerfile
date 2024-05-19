# Use the .NET SDK image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY PencaUcuApi.csproj .

# Restore dependencies
RUN dotnet restore

# Build and publish a release
#RUN dotnet publish -c Release -o out

COPY . .

# Build runtime image
#FROM mcr.microsoft.com/dotnet/aspnet:8.0
#WORKDIR /App

# Copy published files from build-env
#COPY --from=build-env /App/out .

# Specify the entry point
CMD ["dotnet", "watch", "run", "--project", "PencaUcuApi.csproj", "--urls", "http://*:80"]
