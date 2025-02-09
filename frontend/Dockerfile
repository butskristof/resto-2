FROM --platform=$BUILDPLATFORM node:lts-hydrogen as build-spa
ARG SPA_PATH=./spa
WORKDIR /app
RUN chown node:node ./
USER node

COPY ${SPA_PATH}/package*.json ./
RUN npm ci

COPY ${SPA_PATH}/. ./
RUN npm run build

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build-bff
ARG TARGETARCH
ARG BFF_PATH=./bff
WORKDIR /src

COPY ${BFF_PATH}/Resto.Bff.csproj ./
RUN dotnet restore -a $TARGETARCH
COPY ${BFF_PATH}/. ./
COPY --from=build-spa /app/dist ./wwwroot
RUN dotnet publish -a $TARGETARCH -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
copy --from=build-bff /app ./
ENTRYPOINT ["dotnet", "Resto.Bff.dll"]