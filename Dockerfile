FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["Src/EasyChallenge.API/EasyChallenge.API.csproj", "Src/EasyChallenge.API/"]
COPY ["Src/EasyChallenge.Bootstrap/EasyChallenge.Bootstrap.csproj", "Src/EasyChallenge.Bootstrap/"]
COPY ["Src/EasyChallenge.Application/EasyChallenge.Application.csproj", "Src/EasyChallenge.Application/"]
COPY ["Src/EasyChallenge.Domain/EasyChallenge.Domain.csproj", "Src/EasyChallenge.Domain/"]
RUN dotnet restore "Src/EasyChallenge.API/EasyChallenge.API.csproj"
COPY . .
WORKDIR "/src/Src/EasyChallenge.API"
RUN dotnet build "EasyChallenge.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasyChallenge.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyChallenge.API.dll"]