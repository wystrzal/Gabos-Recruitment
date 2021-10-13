#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY /NuGet.config .
COPY ["Gabos_Recruitment.Blazor.Server/Gabos_Recruitment.Blazor.Server.csproj", "Gabos_Recruitment.Blazor.Server/"]
COPY ["Gabos_Recruitment.Module/Gabos_Recruitment.Module.csproj", "Gabos_Recruitment.Module/"]
COPY ["Gabos_Recruitment.Module.Blazor/Gabos_Recruitment.Module.Blazor.csproj", "Gabos_Recruitment.Module.Blazor/"]
RUN dotnet restore "Gabos_Recruitment.Blazor.Server/Gabos_Recruitment.Blazor.Server.csproj"
COPY . .
WORKDIR "/src/Gabos_Recruitment.Blazor.Server"
RUN dotnet build "Gabos_Recruitment.Blazor.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gabos_Recruitment.Blazor.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gabos_Recruitment.Blazor.Server.dll"]