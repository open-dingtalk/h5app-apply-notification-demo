# 代码模板——消息通知

## 1. 背景介绍

企业生产管理系统结合消息通知功能，可以让企业在各个运营环节都能够收到实时消息提醒，以便相关人员能够在第一时间了解到各生产线生产状况，及时处理异常，确保生产流程顺利进行。同时可与多条生产线打通数据，定期向负责人推送生产报告等，帮助企业负责人快速、清晰地实时了解生产动态。

## 2. 项目结构

> 此demo为.net构建的项目，主要功能包括：等。

以下为目录结构与部分核心代码文件：

```
.
├── AccessToken
│   └── AccessToken.cs     ————获取access_token
├── App_Data
├── Controllers
│   └── ProductController.cs     ————核心api调用
├── README.md     ————说明文档
├── ScanLoginDemo.csproj
├── Views
│   └── Product
│       └── Index.cshtml     ————核心页面
├── Web.config     ————应用配置
└── packages.config
```

## 3. 开发环境准备

1. 需要有一个钉钉注册企业，如果没有可以创建：https://oa.dingtalk.com/register_new.htm#/

2. 成为钉钉开发者，参考文档：https://open.dingtalk.com/document/org/become-a-dingtalk-developer

3. 登录钉钉开放平台后台创建一个H5应用： https://open-dev.dingtalk.com/#/index

4. 配置应用：

  ① 配置开发管理，参考文档：https://open.dingtalk.com/document/org/configure-orgapp

  **此处配置“应用首页地址”需公网地址，若无公网ip，可使用钉钉内网穿透工具：** https://open.dingtalk.com/document/resourcedownload/http-intranet-penetration

  ![img](https://img.alicdn.com/imgextra/i4/O1CN01QGY87t1lOZN65XHqR_!!6000000004809-2-tps-2870-1070.png)

  ② 配置相关权限，参考文档：https://open.dingtalk.com/document/orgapp-server/address-book-permissions

  本demo使用接口权限：

- 成员信息读权限

![img](https://img.alicdn.com/imgextra/i2/O1CN01n0QZM321k7rcBwfsr_!!6000000007022-2-tps-2822-1080.png)

## 4. 启动项目

1. 启动前置条件：

- 安装配置了.net开发环境


2. 下载项目：

```shell
git clone https://github.com/open-dingtalk/h5app-apply-notification-demo.git
```

3. 修改Web.config，配置应用信息，相关参数获取方法如下：

- 获取corpId——开发者后台首页：https://open-dev.dingtalk.com/#/index ![](https://img.alicdn.com/imgextra/i2/O1CN01amtWue1l5nAYRc2hd_!!6000000004768-2-tps-1414-321.png)

- 获取appKey、appSecret、agentId——开发者后台 -> 应用开发 -> 企业内部开发 -> 进入应用 -> 基础信息![](https://img.alicdn.com/imgextra/i3/O1CN01Rpfg001aSjEIczA85_!!6000000003329-2-tps-905-464.png)

4. 启动后配置：

① **配置访问地址**

启动项目后，推荐使用内网穿透工具获取临时域名（**钉钉内网穿透工具：** https://open.dingtalk.com/document/resourcedownload/http-intranet-penetration）

配置首页链接：复制域名 -> 进入开发者后台 -> 进入应用 -> 开发管理 -> 粘贴到“应用首页地址”、“PC端首页地址”

配置登录重定向地址：复制域名 -> 进入开发者后台 -> 进入应用 -> 钉钉登录与分享 -> 粘贴到“回调域名”点击添加

- 生成的域名： ![](https://img.alicdn.com/imgextra/i3/O1CN01lN8Myr1XIFJmlDSWf_!!6000000002900-2-tps-898-510.png)

- 配置地址： ![](https://img.alicdn.com/imgextra/i1/O1CN01IWleEp1Kw0hX9suby_!!6000000001227-2-tps-1408-489.png)

5. 访问页面：使用移动端钉钉访问应用

## 5. 页面展示

首页  ![](https://img.alicdn.com/imgextra/i2/O1CN01unXSBR1oskM1MRBS9_!!6000000005281-2-tps-397-443.png)

消息通知  ![](https://img.alicdn.com/imgextra/i2/O1CN01VosOoa1lnIna1tL38_!!6000000004863-2-tps-500-309.png)


## 6. 参考文档

1. 获取企业内部应用access_token，文档链接：https://open.dingtalk.com/document/orgapp-server/obtain-orgapp-token
2. 根据userId获取用户详情，文档链接：https://open.dingtalk.com/document/orgapp-server/query-user-details
3. 发送消息通知，文档链接：https://open.dingtalk.com/document/orgapp-server/asynchronous-sending-of-enterprise-session-messages
4. 上传媒体文件，文档链接：https://open.dingtalk.com/document/orgapp-server/upload-media-files


## 7.免责声明

- 此代码示例仅用于学习参考，请勿直接用作生产线上代码，如线上使用出现故障损失，后果自行承担。
- 此示例中“内网穿透工具”仅用于开发测试阶段，请勿用于生成线上环境，如出现稳定性问题，后果自行承担。

## **8.常见问题**

Q：为什么启动后页面功能报错？

A：请检查相关权限是否申请。

-----

Q：为什么应用打开是错误页面？

A：请检查是否启动内网穿透工具并配置了应用首页地址。

