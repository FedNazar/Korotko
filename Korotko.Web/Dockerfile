ARG API_SERVER_URL=https://localhost:5000

FROM httpd:2.4-alpine AS web

ARG API_SERVER_URL

COPY ["static/", "/usr/local/apache2/htdocs"]

WORKDIR /usr/local/apache2/htdocs/scripts/shared
RUN echo "export const SERVER_URL=\"$API_SERVER_URL\"" > server-url.js

WORKDIR /usr/local/apache2/conf
COPY ["httpd-docker.conf", "/usr/local/apache2/conf/httpd.conf"]
