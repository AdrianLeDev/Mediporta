FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY ["MediportaSOTags/MediportaSOTags.csproj", "MediportaSOTags/"]
COPY ["MediportaSOTags.IntegrationTests/MediportaSOTags.IntegrationTests.csproj", "MediportaSOTags.IntegrationTests/"]
COPY ["MediportaSOTags.UnitTests/MediportaSOTags.UnitTests.csproj", "MediportaSOTags.UnitTests/"]
RUN dotnet restore "MediportaSOTags/MediportaSOTags.csproj"
COPY . .
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet ef database update -p "MediportaSOTags/MediportaSOTags.csproj"
RUN dotnet publish "MediportaSOTags/MediportaSOTags.csproj" -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/MediportaSOTags/MPSOT_DB.db .
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "MediportaSOTags.dll" ]