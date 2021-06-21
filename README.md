
1-) run cmd 
2-) run command : docker-compose --project-name eaton up -d
3-) run command: docker exec -it cassandra bash
4-) run command: cqlsh
5-) after that copy the script below and run it inside cqlsh

CREATE KEYSPACE eatondb 
WITH REPLICATION = 
{ 'class' : 'SimpleStrategy', 'replication_factor' : 1 };

6-) after that you also need to copy script below to cqlsh and press enter

CREATE COLUMNFAMILY Telemetry( 
Id varchar,
DeviceName varchar,
Temperature int,
TimeStamp timestamp,
Latitude double,
Longitude double,
PRIMARY KEY (Id))
