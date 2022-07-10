FROM node:16 AS builder
WORKDIR /repo
COPY public-client .
RUN npm install
RUN npm run build -- --prod

FROM nginx AS prod
WORKDIR /site
COPY --from=builder /repo/dist/public-client .
COPY docker/public-client-site.conf /etc/nginx/conf.d/default.conf