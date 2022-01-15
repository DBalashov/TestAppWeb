# docker build -t test-app-web -f Dockerfile . --no-cache

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

COPY . .
RUN dotnet build -c Release -r win-x64 --no-self-contained

COPY /bin/Release/net6.0/win-x64/ /app/
COPY /wwwroot/ /App/wwwroot/

EXPOSE 5000/tcp

ENTRYPOINT ["dotnet", "TestAppWeb.dll"]
