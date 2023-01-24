FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IoneVectronConverter/IoneVectronConverter.csproj", "IoneVectronConverter/"]
RUN dotnet restore "IoneVectronConverter/IoneVectronConverter.csproj"
COPY . .
WORKDIR "/src/IoneVectronConverter"
RUN dotnet build "IoneVectronConverter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IoneVectronConverter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IoneVectronConverter.dll"]
