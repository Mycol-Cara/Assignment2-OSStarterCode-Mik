# Assignment2-OSStarterCode- Car Rental App
## Table Of contents 
- [Description](#Description)
- [Features](#Features)
- [Prerequisites](#Prerequisites)
- [Installing](#Installing)
- [Design](#Design)
- [Running Tests](#Running Tests)

## Description
A BREAD web app tool to manage a vehicle rental business.
The Program is written in C#.


## Installing
### STEP 1: Create MySQL database and User
```bash
mysql -u root
```
```sql 
CREATE DATABASE `cars` CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_general_ci';
CREATE DATABASE `cars_test` CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_general_ci';
CREATE USER 'cars'@'localhost' IDENTIFIED WITH mysql_native_password BY 'Password1';
GRANT ALL PRIVILEGES ON cars.* TO 'cars'@'localhost';

GRANT ALL PRIVILEGES ON cars_test.* TO 'nmt_fleet_manager'@'localhost';

GRANT USAGE ON *.* TO 'cars'@'localhost';
FLUSH PRIVILEGES;

```

MySQL statement 
```sql
CREATE TABLE `fuelpurchases` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `vehicleid` int(11) NOT NULL,
  `amount` int(11) DEFAULT NULL,
  `price` double DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
```
```sql
CREATE TABLE `journies` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `vehicleid` int(11) NOT NULL,
  `distanceTravelled` int(11) DEFAULT NULL,
  `journeyAt` datetime DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  `paid` int(11) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
```
```sql
CREATE TABLE `services` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `vehicleid` int(11) NOT NULL,
  `odometer` int(11) DEFAULT NULL,
  `serviceCount` int(11) DEFAULT NULL,
  `serviceDate` datetime DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `updated` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
```
```sql
CREATE TABLE `vehicles` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `vehicleid` int(11) NOT NULL,
  `model` mediumtext,
  `manufacturer` mediumtext,
  `makeyear` int(11) DEFAULT NULL,
  `odometer` int(11) DEFAULT NULL,
  `registration` mediumtext,
  `tankcapacity` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
```
Note: MySQL default port is 3306
### Step 2: Clone the repo
```bash  
https://github.com/Mycol-Cara/Assignment2-OSStarterCode-Mik
```
### Step 3: Install NuGet Packages
- NewtonSoft.Json
- MySql Commands

## Screenshots
![mainWindow](./images/mainWindow.PNG)
![browseVehicles](./images/browseVehicles.PNG)
![details](./images/details.PNG)
![filterResult](./images/filterResult.PNG)
![adminTools](./images/adminTools.PNG)
![adminControlls](./images/admincontrolls.PNG)
![loadingData](./images/loadingData.PNG)



Starter code for assignment 2 of the Open Source and Testing course.

This repository contains the classes for the vehicle rental system.
You can modify and extend the existing code as well as add new classes to make the application functional and user-friendly.
The User Interface can be either GUI or console based.
Git source control must be used to track different versions of the application over time.
