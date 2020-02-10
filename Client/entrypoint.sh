sleep 1s

cp -v /certs/* /usr/local/share/ca-certificates/
update-ca-certificates

dotnet Client.dll
