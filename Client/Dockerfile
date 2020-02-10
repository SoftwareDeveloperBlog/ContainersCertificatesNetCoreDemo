FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

COPY *.sh out/

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /app
COPY --from=build-env /app/out .

#ENV SSL_CERT_FILE /dev/null
#ENV SSL_CERT_FILE /usr/local/share/ca-certificates/server1.pfx
#ENV SSL_CERT_DIR /usr/local/share/ca-certificates

ENTRYPOINT ["/bin/bash", "./entrypoint.sh"]