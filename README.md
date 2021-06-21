
1-) run cmd <br />
2-) run command : docker-compose --project-name eaton up -d <br />
3-) run command: docker exec -it cassandra bash <br />
4-) run command: cqlsh <br />

![image](https://user-images.githubusercontent.com/16955249/122736899-27803c00-d289-11eb-8377-35bd9503de67.png)

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
