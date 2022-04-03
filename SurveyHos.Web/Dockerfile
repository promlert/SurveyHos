#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SurveyHos.Web/SurveyHos.Web.csproj", "SurveyHos.Web/"]
COPY ["Survey.Module/Survey.Module.csproj", "Survey.Module/"]
COPY ["SurveyHos.Theme/SurveyHos.Theme.csproj", "SurveyHos.Theme/"]
RUN dotnet restore "SurveyHos.Web/SurveyHos.Web.csproj"
COPY . .
WORKDIR "/src/SurveyHos.Web"
RUN dotnet build "SurveyHos.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SurveyHos.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SurveyHos.Web.dll"]