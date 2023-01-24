FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IoneVectronConverterBlazor/IoneVectronConverterBlazor.csproj", "IoneVectronConverterBlazor/"]
RUN dotnet restore "IoneVectronConverterBlazor/IoneVectronConverterBlazor.csproj"
COPY . .
WORKDIR "/src/IoneVectronConverterBlazor"
RUN dotnet build "IoneVectronConverterBlazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IoneVectronConverterBlazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IoneVectronConverterBlazor.dll"]
