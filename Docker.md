# Docker

## 基本概念

* 镜像(`Image`) : 分层存储
* 容器(`Container`) : 类比面向对象 : 容器是镜像(类型)创建出的实例
* 仓库(`Repository`) : 镜像存储,一系列镜像通过tag标记,组成一个 `Repository`,托管到 `Registry` 上

* 安装方式: 略
* docker 命令通过远程调用(RESTful API)的方式,调用 docker engine,执行命令

## 镜像

镜像 : `[<仓库名>[:<标签>]]` or `<镜像ID>`

* docker pull
* docker image ls
* docker image rm
* (__慎用__)docker commit : `docker commit [选项] <容器ID或容器名> [<仓库名>[:<标签>]]`
* docker diff <容器ID或容器名>
* `docker history <镜像ID或[<仓库名>[:<标签>]]>`

### Dockerfile

docker build [选项] <上下文路径/URL/->

* `docker build -t nginx:v3 .` : 上下文路径指定 COPY 等命令执行时 的源路径
* `docker build https://github.com/twang2218/gitlab-ce-zh.git#:111`
* `cat Dockerfile | docker build -`

Dockerfile 中每一个指令都会建立一个新的层

* FROM : 基础 (空白的镜像`scratch`)
* WORKDIR : 工作路径
* RUN : (Build时)执行命令 `RUN npm install` or `RUN ["npm","install"]` `RUN [ "sh", "-c", "echo $HOME" ]`
* COPY / ADD : 文件复制 添加
* CMD : 容器启动后执行的命令,容器内没有后台服务的概念,命令是以前台形式运行的,命令执行完,容器就退出 如果使用 docker run xxxx 可以自定义 CMD
* ENTRYPOINT : 参数传给 ENTRYPOINT 中定义的命令 (CMD是固定的 使用时 只会改变部分参数 可以使用ENTRYPOINT 定义好固定部分通过 run/CMD 传入参数)
* ENV : runtime环境变量
* ARG : build 时环境变量
* VOLUME : volumn 映射 功能等效于 -v 参数 : `docker run -d -v mydata:/data xxxx`
* EXPOSE ：暴露容器端口 ,不同的是 参数 -p 功能是端口映射
* USER : 指定用户/用户组
* ONBUILD : 基础镜像不执行,后续镜像

```shell
ONBUILD COPY ./package.json /app
ONBUILD RUN [ "npm", "install" ]
ONBUILD COPY . /app/
```

### 多步骤实例

```shell
# Step 1 Build Frontend
FROM node:alpine as frontend
COPY package.json /app/
RUN cd /app \
&& npm install --registry=https://registry.npm.taobao.org
COPY webpack.mix.js /app/
COPY resources/assets/ /app/resources/assets/
RUN cd /app \
&& npm run production
FROM composer as composer
COPY database/ /app/database/
COPY composer.json /app/
RUN cd /app \
&& composer config -g repo.packagist composer https://mirrors.aliyun.com/composer/ \
&& composer install \
--ignore-platform-reqs \
--no-interaction \
--no-plugins \
--no-scripts \
--prefer-dist
# Step 2 集成 frontend
FROM php:7.2-fpm-alpine as laravel
ARG LARAVEL_PATH=/app/laravel
COPY --from=composer /app/vendor/ ${LARAVEL_PATH}/vendor/
COPY . ${LARAVEL_PATH}
COPY --from=frontend /app/public/js/ ${LARAVEL_PATH}/public/js/
COPY --from=frontend /app/public/css/ ${LARAVEL_PATH}/public/css/
COPY --from=frontend /app/mix-manifest.json ${LARAVEL_PATH}/mixmanifest.json
RUN cd ${LARAVEL_PATH} \
&& php artisan package:discover \
&& mkdir -p storage \
&& mkdir -p storage/framework/cache \
&& mkdir -p storage/framework/sessions \
&& mkdir -p storage/framework/testing \
&& mkdir -p storage/framework/views \
&& mkdir -p storage/logs \
&& chmod -R 777 storage
# Step 3 Build
FROM nginx:alpine as nginx
ARG LARAVEL_PATH=/app/laravel
COPY laravel.conf /etc/nginx/conf.d/
COPY --from=laravel ${LARAVEL_PATH}/public ${LARAVEL_PATH}/public
```

### manifest 构建支持多种架构的镜像

### 镜像实现原理

## 容器

* docker run -i -t : 交互 分配伪终端并绑定到容器STD IN/OUT
* docker run -d : 后台运行
* docker container start/stop
* docker exec -it xxx bash : 进入容器并执行命令 这时候输入 exit 不会终止容器 但是 docker attach 会导致容器停止
* docker container rm / prune : 删除/清理容器

## 仓库

* docker login/logout
* docker search/pull
* docker push

### Docker Hub Automated Builds

### 私有仓库

* docker-registry
* Nexus

## 数据

类似于mount,将本地文件系统挂在到容器中,用于持久化数据等功能

* 数据卷
  * 创建 `docker volume create my-vol`
  * 挂载 `docker run -d -P --name web --mount source=my-vol,target=/webapp`
  * or `docker run -d -P --name web -v my-vol:/wepapp`
* 挂载主机目录
  * 挂载 `docker run -d -P --name web --mount type=bind,source=/src/webapp,target=/opt/webapp`
  * or `docker run -d -P --name web -v /src/webapp:/opt/webapp`

## 网络

* 端口映射 `docker run -P` `docker run -p 5000:5000`
* `docker port xxx` 查看端口
* `docker network create -d bridge local_bridge` 创建网络
* `docker run -i -t --network local_bridge --name u004 ubuntu:18.04 bash` run 指定网络

### 高级网络配置

## Docker Buildx

## Docker Compose

## Swarm

## 原理与实现

## etcd

## coreos

## k8s
