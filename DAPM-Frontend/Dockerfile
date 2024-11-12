FROM node:18-alpine AS builder

WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
ARG ENV_FILE
COPY ${ENV_FILE} .env
RUN npm run build

FROM nginx:1.19.0

WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=builder /app/build .

COPY ./nginx/certs/cert.pem /etc/ssl/certs/cert.pem
COPY ./nginx/certs/key.pem /etc/ssl/private/key.pem

COPY ./nginx/nginx.conf /etc/nginx/nginx.conf

EXPOSE 443
ENTRYPOINT ["nginx", "-g", "daemon off;"]
