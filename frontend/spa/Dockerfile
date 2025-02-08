# build Vue app as SPA
FROM --platform=$BUILDPLATFORM node:lts-hydrogen as build-stage
WORKDIR /app

COPY package*.json ./
RUN npm ci

COPY . ./
RUN npm run build

# set up nginx server
FROM nginx:stable-alpine as production-stage
COPY docker/nginx.conf /etc/nginx/nginx.conf
COPY --from=build-stage /app/dist /usr/share/nginx/html

# set env vars on container start
COPY docker/entrypoint.sh /usr/share/nginx/
ENTRYPOINT ["/usr/share/nginx/entrypoint.sh"]

CMD ["nginx", "-g", "daemon off;"]