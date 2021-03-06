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

EXPOSE 6000 6001

# Generate certifiacte
RUN openssl genrsa 2048 > server2_private.pem
RUN openssl req -x509 -new -key server2_private.pem -out server2_public.pem -passout pass:password -subj "/C=GB/ST=Nottingham/L=Nottinghamshire/O=SoftwareDeveloper.Blog/OU=IT/CN=server2/emailAddress=tometchy@gmail.com"
RUN openssl pkcs12 -export -in server2_public.pem -inkey server2_private.pem -out server2.pfx -passout pass:password 
RUN openssl pkcs12 -in server2.pfx -out server2.crt -nokeys -passin pass:password

ENTRYPOINT ["/bin/bash", "./entrypoint.sh"]