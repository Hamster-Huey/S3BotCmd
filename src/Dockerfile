FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY src/S3BotCmd.csproj src/
RUN dotnet restore src/S3BotCmd.csproj
COPY . .
WORKDIR /src/src
RUN dotnet build S3BotCmd.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish S3BotCmd.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "S3BotCmd.dll"]
