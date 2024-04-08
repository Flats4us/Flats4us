FROM node:16-alpine AS build

WORKDIR /app
COPY . .

RUN npm install
RUN npm run build --env=commitHash=$COMMIT_HASH --env=commitDate=$COMMIT_DATE

FROM nginx:alpine
COPY --from=build /app/dist/angular/ /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80