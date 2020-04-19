# Consul

## run consul on docker

```shell
docker pull consul:1.7
```

```shell
# run server 1
docker run -d -p 8510:8500 --restart=always -v /var/consul/data/server1:/consul/data -v /var/consul/conf/server1:/consul/config -e CONSUL_BIND_INTERFACE='eth0' --privileged=true --name=consul_server_1 consul:1.7 agent -server -bootstrap-expect=3 -ui -node=consul_server_1 -client='0.0.0.0' -data-dir /consul/data -config-dir /consul/config -datacenter=xdp_dc;
```

```shell
JOIN_IP="$(docker inspect -f '{{.NetworkSettings.IPAddress}}' consul_server_1)";
```

```shell
# run server 2
docker run -d -p 8520:8500 --restart=always -v /var/consul/data/server2:/consul/data -v /var/consul/conf/server2:/consul/config -e CONSUL_BIND_INTERFACE='eth0' --privileged=true --name=consul_server_2 consul:1.7 agent -server -ui -node=consul_server_2 -client='0.0.0.0' -datacenter=xdp_dc -data-dir /consul/data -config-dir /consul/config -join=$JOIN_IP;　　
```

```shell
# run server 3
docker run -d -p 8530:8500 --restart=always -v /var/consul/data/server3:/consul/data -v /var/consul/conf/server3:/consul/config -e CONSUL_BIND_INTERFACE='eth0' --privileged=true --name=consul_server_3 consul:1.7 agent -server -ui -node=consul_server_3 -client='0.0.0.0' -datacenter=xdp_dc -data-dir /consul/data -config-dir /consul/config -join=$JOIN_IP;
```

```shell
# run server 4
docker run -d -p 8540:8500 --restart=always -v /var/consul/data/server4:/consul/data -v /var/consul/conf/server4:/consul/config -e CONSUL_BIND_INTERFACE='eth0' --privileged=true --name=consul_server_4 consul:1.7 agent -server -ui -node=consul_server_4 -client='0.0.0.0' -datacenter=xdp_dc -data-dir /consul/data -config-dir /consul/config -join=$JOIN_IP;
```

```shell
# run client 1
docker run -d -p 18510:8500 --restart=always -v /var/consul/conf/client1:/consul/config -e CONSUL_BIND_INTERFACE='eth0' --name=consul_client_1 consul:1.7 agent -node=consul_client_1 -join=$JOIN_IP -client='0.0.0.0' -datacenter=xdp_dc -config-dir /consul/config
```

```shell
# run client 2
docker run -d -p 18520:8500 --restart=always -v /var/consul/conf/client2:/consul/config -e CONSUL_BIND_INTERFACE='eth0' --name=consul_client_2 consul:1.7 agent -node=consul_client_2 -join=$JOIN_IP -client='0.0.0.0' -datacenter=xdp_dc -config-dir /consul/config
```
