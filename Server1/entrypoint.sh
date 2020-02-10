if [ -d "/certs" ]; then 
  cp -v server1* /certs/; 
else 
  echo "No certs directory"; 
fi

dotnet Server1.dll