FROM node:latest as build
LABEL maintainer="1029765111@qq.com"

WORKDIR /app
COPY . .
ARG mode=production

# RUN npm install -g cnpm --registry=https://registry.npm.taobao.org
# RUN npm config set registry https://registry.yarnpkg.com
RUN npm config set registry https://mirrors.cloud.tencent.com/npm

RUN npm cache clean -force

RUN npm install && \
  npm run build:${mode}

FROM nginx:latest
LABEL maintainer="1029765111@qq.com"

COPY --from=build /app/dist /app
COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
ENTRYPOINT ["nginx","-g","daemon off;"]
