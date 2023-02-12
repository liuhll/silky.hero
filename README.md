# Silky.Hero 后台管理系统

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](./LICENSE)
[![Commit](https://img.shields.io/github/last-commit/liuhll/silky.hero)](https://img.shields.io/github/last-commit/liuhll/silky.hero)
[![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2Fliuhll%2Fsilky.hero&count_bg=%2379C83D&title_bg=%23555555&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)

## 项目简介
Silky.Hero是基于[Silky微服务框架](https://docs.silky-fk.com/)开发的[RBAC权限管理系统](https://zhuanlan.zhihu.com/p/104849603),开发者可以根据该框架快速构建一个业务管理系统。

演示地址: https://hero.silky-fk.com/  

管理员: amdin  密码: 123qweR!

普通用户: liuhll 密码:123qweR!

## 内置功能

1. 用户管理：用户是系统操作者，该功能主要完成系统用户配置。
2. 部门管理：配置系统组织机构（公司、部门、小组），树结构展现支持数据权限。
3. 岗位管理：配置系统用户所属担任职务。
4. 菜单管理：配置系统菜单，操作权限，按钮权限标识等。
5. 角色管理：角色菜单权限分配、设置角色按机构进行数据范围权限划分。
6. 字典管理：对系统中经常使用的一些较为固定的数据进行维护。
7. 审计日志：系统正常操作日志记录和查询；系统异常信息日志记录和查询。
8. 租户管理: 管配置系统所有的的租户以及该租户所属的系统版本。
9. 版本管理: 指定系统版本以及版本功能。
10. 系统接口：根据业务代码自动生成相关的swagger webapi接口文档。

## 微服务列表

|  标识  | 微服务 | 描述 | 数据库  | 备注 |
| :---- | :------ | :---- | :------- | :------- |
| Account | 账户 | 主要负责用户登陆认证、获取当前登陆用户信息等服务 | Silky.Identity | 与`Identity`服务共享数据以及领域服务 |
| BasicData | 基础数据 | 主要维护系统中较为固定的基础数据(如字典)的服务 | Silky.BasicData | |
| Identity | 身份信息 | 负责系统中的用户、角色等数据的维护以及功能权限、数据权限的授权管理 | Silky.Identity | |
| Organization | 组织机构 | 负责维护组织机构的数据以及相关业务服务 | Silky.Organization | |
| Position | 岗位 | 负责维护岗位的数据以及相关业务服务 | Silky.Position | |
| Permission | 权限 | 负责系统中功能以及菜单的数据维护以及对接口的授权认证服务 | Silky.Permission | |
| Saas | 软件运营 | 负责维护系统所有的功能以及系统版本能力 | Silky.Saas | |
| Log | 日志 | 负责维护系统中生成的日志信息 | Silky.Log | |
| Gateway | 网关 | 负责集群对外提供统一的webapi,以及通过调用`Permission`提供的接口实现对访问借口的授权 | | |


## 开发环境

Silky.Hero默认使用[Nacos](https://nacos.io/zh-cn/)作为服务注册中心和服务配置中心,使用Mysql作为数据库服务器;

1. 进入到`deploy/docker-compose/infrastr`目录,通过如下命令快速安装部署Nacos服务:

```shell

docker network create silky_service_net # 首先通过该命令创建 silky_service_net 虚拟网络

docker-compose -f docker-compose.nacos.mysql.yaml up -d # 运行 nacos集群以来的数据库服务

docker-compose -f docker-compose.nacos.cluster-hostname.yaml up -d # 等待 mysql服务启动后再运行该命令启动nacos集群服务

```

2. 等nacos服务启动成功后,通过浏览器打开https://127.0.0.1:8848/nacos，通过账号/密码:nacos/nacos登陆管理端,然后创建`silky`和`silky_hero_config`名称空间,然后通过**配置管理/配置管理/导入配置**功能将位于`services/Shared/Configs/silky.hero.dev.zip`的配置包导入到nacos系统中;

3. 通过如下命令启动redis服务

```shell

docker-compose -f docker-compose.redis.yaml up -d

```

3. 通过如下命令启动rabbitmq服务

```shell

docker-compose -f docker-compose.rabbitmq.yaml up -d

```

4. 通过如下命令启动mysql服务

```shell

docker-compose -f docker-compose.mysql.yaml up -d

```

5. 通过IDE *Visual Studio* 或是 *Rider* 打开解决方案`Services/Silky.Hero.sln`后进行开发调试;应用启动后,系统将自动创建数据库和新增种子数据。

> Notes
>  silky还支持使用Zookeeper或是Consul作为服务注册中心,使用Apllo作为服务配置中心,您可以查看[官方文档](https://docs.silky-fk.com/)或是[教学视频](https://space.bilibili.com/354560671/channel/seriesdetail?sid=2797330)来学习如何使用和配置。


## 打包部署

进入`build`目录后,可以通过`build-push-images.ps1`脚本实现对各个为服务应用的源码构建和docker镜像打包,通过`Services`指定需要构建的微服务应用;

```shell

./build-push-images.ps1

```

该脚本有如下的参数:

|  参数名  | 描述 | 缺省值 | 备注 |
| :---- | :------ | :---- | :------- |
|  publish  | 构建源码并执行发布 | $true |  |
|  clean  | 构建源码时是否清理本地nuget包缓冲| $false | 构建发布源码时生效  |
|  buildImages  | 是否构建docker镜像 | $true |  |
|  pushImages  | 是否推送docker镜像到镜像仓库 | $false |  |
|  imageTag  | 制定的镜像Tag |  | 默认值为`latest`  |
|  registry  | docker仓库地址 | |  推送时生效，为空时默认推送到官方仓库|
|  dockerUser  | 仓库用户名 | | 推送时生效 |
|  dockerPwd  | 仓库密码 | | 推送时生效 |


### docker-compose部署

### k8s部署

## 演示图


