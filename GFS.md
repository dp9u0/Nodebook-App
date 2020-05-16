# GFS

## Intro

### why

* 组件失效被认为是常态事件,而不是意外事件
* 文件巨大
* 多数修改都是文件追加而不是覆盖

## 架构

* 一个Master 节点
* 多台Chunk 节点

![GFS Architecture](./GFS/GFS-Architecture.jpg)

## Reference

* [深入浅出Google File System](https://www.jianshu.com/p/58cb14cc1199)
* [The Google File System 论文笔记](https://juejin.im/post/5d9dc4d2e51d4578453274cd)
* [Google File System 论文解读](https://juejin.im/entry/5d8b068a518825096a1868d4)
