FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG TARGETARCH
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln ./
COPY Directory.Packages.props ./
COPY **/*.csproj ./
COPY **/**/**/*.csproj ./
COPY **/**/**/**/*.csproj ./
RUN dotnet sln list | grep ".csproj" \
    | while read -r line; do \
    mkdir -p $(dirname $line); \
    mv $(basename $line) $(dirname $line); \
    done;
RUN dotnet restore -a $TARGETARCH

# copy rest of the source code and build
COPY src/. ./src/
WORKDIR /app/src
RUN dotnet publish -a $TARGETARCH -c Release -o /app --no-restore 4-presentation/Api

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
ENV TZ=Etc/UTC
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Resto.Api.dll"]
