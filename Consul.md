# Consul

## run consul on docker

启动两组集群

```yml
version: "3.6"

services:
  consul_server_1:
    image: consul:1.7
    container_name: consul_server_1
    restart: always
    network_mode: "host"
    command: agent -server -client=0.0.0.0 -bootstrap-expect=3 -node=consul_server_1 -datacenter=dc_001 -data-dir /consul/data -config-dir /consul/config
  consul2:
  
    image: consul:1.7
    container_name: consul_server_2
    networks:
      - consul
    restart: always
    command: agent -server -client=0.0.0.0 -retry-join=consul_server_1 -node=consul_server_2 -datacenter=dc_001 -data-dir /consul/data -config-dir /consul/config
  consul3:
    image: consul:1.7
    container_name: consul_server_3
    network_mode: "host"
    restart: always
    command: agent -server -client=0.0.0.0 -retry-join=consul_server_1 -node=consul_server_3 -datacenter=dc_001 -data-dir /consul/data -config-dir /consul/config
  consul4:
    image: consul:1.7
    container_name: consul_client_1
    network_mode: "host"
    restart: always
    ports:
      - 18500:8500
    command: agent -client=0.0.0.0 -retry-join=consul_server_1 -ui -node=consul_client_1 -datacenter=dc_001 -data-dir /consul/data -config-dir /consul/config
  consul5:
    image: consul:1.7
    container_name: consul_server_5
    network_mode: "host"
    restart: always
    command: agent -server -client=0.0.0.0 -bootstrap-expect=3 -node=consul_server_5 -datacenter=dc_002 -data-dir /consul/data -config-dir /consul/config
  consul6:
    image: consul:1.7
    container_name: consul_server_6
    network_mode: "host"
    restart: always
    command: agent -server -client=0.0.0.0 -retry-join=consul_server_5 -node=consul_server_6 -datacenter=dc_002 -data-dir /consul/data -config-dir /consul/config
  consul7:
    image: consul:1.7
    container_name: consul_server_7
    network_mode: "host"
    restart: always
    command: agent -server -client=0.0.0.0 -retry-join=consul_server_5 -node=consul_server_7 -datacenter=dc_002 -config-dir /consul/config
  consul8:
    image: consul:1.7
    container_name: consul_client_2
    network_mode: "host"
    restart: always
    ports:
      - 28500:8500
    command: agent -client=0.0.0.0 -retry-join=consul_server_5 -ui -node=consul_client_2 -datacenter=dc_002 -config-dir /consul/config

networks:
  consul:
    driver: bridge
```
