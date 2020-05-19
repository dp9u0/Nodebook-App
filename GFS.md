# GFS

## Intro

### why

* 组件失效被认为是常态事件,而不是意外事件
* 文件巨大
* 多数修改都是文件追加而不是覆盖

## 概述

### 架构

* 一个Master 节点
* 多台Chunk 节点

![GFS Architecture](./GFS/GFS-Architecture.jpg)

### Chunk

64M,从元数据数量 操作次数等方面设计较大的chunk,但是chunk过大带来热点问题.

### 元数据

所有的元数据都保存在 Master 服务器的内存中,Master 服务器在启动时,或者有新的 Chunk 服务器加入时,向各个 Chunk 服务器轮询它们所存储的 Chunk 的信息

* 文件和 Chunk 的命名空间
* 文件和 Chunk 的对应关系
* 每个 Chunk 副本的存放地点

### 操作日志

操作日志包含了关键的元数据变更历史记录(文件 命名空间 Chunk),作为判断同步操作顺序的逻辑时间基线

定时将日志压缩为 Checkpoint,恢复状态仅需要最新的Checkpoint 以及后续的日志

### 一致性模型

| 操作     | 写             | 追加             |
| -------- | -------------- | ---------------- |
| 串行成功 | 已定义         | 已定义部分不一致 |
| 并行成功 | 一致但是未定义 | 已定义部分不一致 |
| 失败     | 不一致         | 不一致           |

对于修改操作的文件范围 region:

* 一致: 客户端从哪个副本读取`region`都是相同结果
* 已定义: region 一致 并且立刻看到写入的所有内容

经过了一系列的成功的修改操作之后,GFS**最终会**确保被修改的文件 region 是已定义的,并且包含最后一次修改操作写入的数据

## Reference

* [深入浅出Google File System](https://www.jianshu.com/p/58cb14cc1199)
* [The Google File System 论文笔记](https://juejin.im/post/5d9dc4d2e51d4578453274cd)
* [Google File System 论文解读](https://juejin.im/entry/5d8b068a518825096a1868d4)
