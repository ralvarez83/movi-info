FROM node:22.2.0-alpine as build

LABEL maintainer="rubenag83@gmail.com"
LABEL aaplication="movi-info"

ARG VITE_DOT_NET_BACK=/api/

WORKDIR /app

COPY package.json .

RUN npm install

COPY tsconfig.* .
COPY vite.config.ts .
COPY index.html .
#COPY .env .
COPY public /app/public
COPY src /app/src

RUN npm run build

#FROM node:slim
FROM nginxinc/nginx-unprivileged:stable-bookworm-perl

COPY ./nginx/nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist /usr/share/nginx/html

