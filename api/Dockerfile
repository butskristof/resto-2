FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln ./
COPY **/*.csproj ./
COPY **/**/**/*.csproj ./
COPY **/**/**/**/*.csproj ./
RUN dotnet sln list | grep ".csproj" \
    | while read -r line; do \ 
    mkdir -p $(dirname $line); \
    mv $(basename $line) $(dirname $line); \
    done;
RUN dotnet restore

# copy rest of the source code and build
COPY src/. ./src/
WORKDIR /app/src
RUN dotnet publish -c Release -o /app --no-restore 4-presentation/Api

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
copy --from=build /app ./
ENTRYPOINT ["dotnet", "Resto.Api.dll"]
