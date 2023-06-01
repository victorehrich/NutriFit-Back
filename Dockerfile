FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
COPY *.sln .
COPY NutriFitBack/*.csproj ./NutriFitBack/
COPY NutriFit.Application.Input/*.csproj ./NutriFit.Application.Input/
COPY NutriFit.Application.Output/*.csproj ./NutriFit.Application.Output/
COPY NutriFit.Domain/*.csproj ./NutriFit.Domain/

COPY NutriFit.Infrastructure.Input/*.csproj ./NutriFit.Infrastructure.Input/
COPY NutriFit.Infrastructure.Output/*.csproj ./NutriFit.Infrastructure.Output/
COPY NutriFit.Infrastructure.Shared/*.csproj ./NutriFit.Infrastructure.Shared/

RUN dotnet restore


WORKDIR /app
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
EXPOSE 8082
COPY --from=build /app .
ENTRYPOINT ["dotnet", "/app/NutriFitBack.dll"]