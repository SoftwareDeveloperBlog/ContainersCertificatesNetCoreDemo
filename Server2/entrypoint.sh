if [ -d "/certs" ]; then 
  cp -v server2* /certs/; 
else 
  echo "No certs directory"; 
fi

dotnet Server2.dll