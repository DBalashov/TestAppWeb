# docker build -t test-api -f Dockerfile . --no-cache

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

COPY . .
RUN dotnet build -c Release -r win-x64 --no-self-contained

COPY /bin/Release/net6.0/win-x64/ /app/

EXPOSE 7500/tcp

ENTRYPOINT ["dotnet", "TestApi.dll"]
