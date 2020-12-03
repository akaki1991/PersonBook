
<h2>PersonBook</h2>
   <p> Application is created for "Company" as task project.<p>
    <br />
  <br>  

  ## Navigation

- [About](#About)
- [Installation](#installation)
- [Prerequisites](#Prerequisites)
---

## About
Project is written in Microsofts latest technologies for 03/12/2020.

- .Net Core 3.1
-  EF Core
-  Swashbuckle
-  MSSQL Server
-  Serilog

The main purpose of this project was to show creators ability of coding and solving tasks. 
Still you will find it useful, there might be some good technical and architectural points.

Trying to follow DDD architecture and CQRS patern, several projects are made.
The main project that should be started is <b>PersonBook.Api</b> (Rest API).
For CQRS ReadModels are generated istead of different databse I seperated update and read models by schema, 
because there was no need seperate databse.

---

## Prerequisites

- .Net core 3.1 and MSSQL SERVER should be installed. 
- You may need to change connection string, just target the server where you have permission to create database

---

## Installation
- Clone github repository, or download and unzip it. 
- Make startup project PersonBook.Api.
- Here is the connection string: Data Source=localhost;Initial Catalog=PersonBookDb; integrated security=true
  Make sure you have MSSQL Server with access of creating database. If localhost don't work for you configure it as you wish.
- run "update-database" command in package manager console, if you use visual studio. 
  Don't forget startup project should be PersonBook.Api, but db context is in PersonBook.Infrastrucutre project.

  ---

 ## Author
    Akaki Chkopoia

    - Linkedin: [Akaki Chkopoia] (https://www.linkedin.com/in/akaki-chkopoia-21603896/)
