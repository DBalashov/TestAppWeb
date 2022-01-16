# docker build -t test-app-web -f Dockerfile . --no-cache
# ~220 MB

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app

COPY . ./
RUN dotnet build -c Release -r linux-x64 --self-contained -o out

FROM ubuntu:18.04
RUN apt-get update -qq && apt-get install -y -qq libssl-dev
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=build-env /app/wwwroot/ /app/wwwroot/
RUN chmod +x ./TestAppWeb

EXPOSE 5000/tcp

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENTRYPOINT ["./TestAppWeb"]
