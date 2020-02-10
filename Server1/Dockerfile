FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5000 5001

# Generate certifiacte
RUN openssl genrsa 2048 > server1_private.pem
RUN openssl req -x509 -new -key server1_private.pem -out server1_public.pem -passout pass:password -subj "/C=GB/ST=Nottingham/L=Nottinghamshire/O=SoftwareDeveloper.Blog/OU=IT/CN=localhost/emailAddress=tometchy@gmail.com"
RUN openssl pkcs12 -export -in server1_public.pem -inkey server1_private.pem -out server1.pfx -passout pass:password 

ENTRYPOINT ["dotnet", "Server1.dll"]