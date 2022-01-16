# docker build -t test-app-web -f Dockerfile . --no-cache
# 716 MB
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app

COPY . ./
RUN dotnet build -c Release -r win-x64 --no-self-contained -o out

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5000/tcp

ENTRYPOINT ["dotnet", "TestAppWeb.dll"]
