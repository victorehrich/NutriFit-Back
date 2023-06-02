FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src

COPY *.sln .
COPY NutriFitBack/*.csproj ./NutriFitBack/
COPY NutriFit.Application.Input/*.csproj ./NutriFit.Application.Input/
COPY NutriFit.Application.Output/*.csproj ./NutriFit.Application.Output/
COPY NutriFit.Domain/*.csproj ./NutriFit.Domain/

COPY NutriFit.Infrastructure.Input/*.csproj ./NutriFit.Infrastructure.Input/
COPY NutriFit.Infrastructure.Output/*.csproj ./NutriFit.Infrastructure.Output/
COPY NutriFit.Infrastructure.Shared/*.csproj ./NutriFit.Infrastructure.Shared/

RUN dotnet restore

COPY . .

WORKDIR /src/NutriFitBack
RUN dotnet build -c Release -o /app

WORKDIR /src/NutriFit.Application.Input
RUN dotnet build -c Release -o /app

WORKDIR /src/NutriFit.Application.Output
RUN dotnet build -c Release -o /app

WORKDIR /src/NutriFit.Domain
RUN dotnet build -c Release -o /app

WORKDIR /src/NutriFit.Infrastructure.Input
RUN dotnet build -c Release -o /app

WORKDIR /src/NutriFit.Infrastructure.Output
RUN dotnet build -c Release -o /app

WORKDIR /src/NutriFit.Infrastructure.Shared
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NutriFitBack.dll"]