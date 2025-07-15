FROM eap-cr.advantech.com.tw/mirror-mcr/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 443
FROM eap-cr.advantech.com.tw/mirror-mcr/dotnet/sdk:8.0 AS build
ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false
ARG BUILD_CONFIGURATION=empty
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done
RUN dotnet restore "./src/HelloWorld.HttpApi.Host/HelloWorld.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/HelloWorld.HttpApi.Host"
RUN dotnet build "./HelloWorld.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build
FROM build AS publish
ARG BUILD_CONFIGURATION=empty
RUN dotnet publish "./HelloWorld.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloWorld.HttpApi.Host.dll"]