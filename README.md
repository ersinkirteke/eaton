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

7-) run kafka.eaton.producer.api to send a message to the kafka firstly. send a post request with a producer method with sample json data below

sample json data: {
  "deviceName": "eatonups45",
  "temperature": 45,
  "timeStamp": "2021-06-20T18:48:22.641Z",
  "latitude": 38.41273,
  "longitude": 27.13838
}

![image](https://user-images.githubusercontent.com/16955249/122738249-7e3a4580-d28a-11eb-8ff3-ebe6a54899b8.png)

8-) run kafka.eaton.consumer.api to consume messages from kafka, it persist data to cassandra.

9-) You can run test project in both two project (kafka.eaton.producer.api, kafka.eaton.consumer.api)

10-) You can find a system design documents for predictpulse inside repository

