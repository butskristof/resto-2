#!/bin/sh

# generate string that puts the env vars into JS code
JSON_STRING='window.configs = { \
  "VITE_RESTO_API_BASE_URL":"'"${VITE_RESTO_API_BASE_URL}"'", \
};'

# append the line in index.html with the JS code generated above
sed -i "s@// CONFIGURATIONS_PLACEHOLDER@${JSON_STRING}@" /usr/share/nginx/html/index.html

exec "$@"