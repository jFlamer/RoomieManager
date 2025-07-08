# RoomieManager – Task Management System for Roommates

## 👤 Authors

* [Jagoda Flejmer](https://github.com/jFlamer)
* [Jakub Kierznowski](https://github.com/qualv13)

## 📝 Project Overview

**RoomieManager** is a web application built with ASP.NET Core MVC that helps roommates efficiently manage shared household responsibilities. The system allows users to create and assign tasks, improving organization and communication in a shared living space.

## 🎯 Purpose

The primary goal of the application is to streamline task coordination among roommates. It enables users to:

* Create to-do lists
* Assign tasks to specific users
* Track task completion status
* Manage schedules for regular chores

## ⚙️ Key Features

* **Authentication & Authorization**: User login via token-based authentication.
* **Task Management**: Create, edit, and delete tasks with ease.
* **Task Assignment**: Assign tasks to individual roommates.
* **Effort Points System**: Each task carries a set effort point value, which is awarded to users upon completion.
* **RESTful API**: Access all core functionalities via a structured REST API.

## 🚀 Usage Examples

### 1. Login

```bash
curl -X POST http://localhost:5035/api/login -d "userName=ts3&password=ts3"
```

Example token:
`b738c4cd-ae85-4d33-bb6b-eacda197f19a`

### Assign yourself to task with ID 1

```bash
curl -X POST http://localhost:5035/api/tasks/1/assign/ -H "token: f9fc902f-dd55-4ca3-82ce-571db8226460"
```

### Create a new task with specified type ID

```bash
curl -X POST http://localhost:5035/api/tasks/ -H "token: f9fc902f-dd55-4ca3-82ce-571db8226460" -d "typeId=1"
```

### Delete task by ID

```bash
curl -X DELETE http://localhost:5035/api/tasks/13 -H "token: f9fc902f-dd55-4ca3-82ce-571db8226460"
```

### Get available tasks

```bash
curl -X GET http://localhost:5035/api/tasks/available -H "token: f9fc902f-dd55-4ca3-82ce-571db8226460"
```

### Get all tasks

```bash
curl -X GET http://localhost:5035/api/tasks/all -H "token: 755eb074-1de8-49c5-b2fa-55bb0ff42c71"
```

### Get all available task types

```bash
curl -X GET http://localhost:5035/api/taskTypes/all -H "token: f9fc902f-dd55-4ca3-82ce-571db8226460"
```

## 📂 Project Structure

```plaintext
RoomieManager/
│
├── Controllers/
│   ├── HomeController.cs
│   ├── RoommatesController.cs
│   ├── TasksController.cs
│   ├── NotesController.cs
│   ├── StatsController.cs
│   └── ApiController.cs          <-- REST API
│
├── Models/
│   ├── User.cs
│   ├── TaskItem.cs
│   ├── AssignedTask.cs
│   ├── Note.cs
│   └── AppDbContext.cs
│
├── Views/
│   ├── Home/
│   ├── Roommates/
│   ├── Tasks/
│   ├── Notes/
│   └── Shared/
│
├── Data/
│   └── DbInitializer.cs         <-- Adds admin and test data
│
├── Program.cs
├── Startup.cs
└── appsettings.json
```

## 🗄️ Database Structure

* **Task Types**: General task categories
* **Tasks**: Detailed tasks linked to types, with assignees, deadlines, and status
* **Preferences**: Each user's preference level (as percentages) for different task types
* **Task Properties**: Status, assigned user, taken date, deadline (nullable if not yet started)
* **Archive Table**: For storing completed or rejected tasks

## 🛠️ Technologies

* **Backend**: ASP.NET Core MVC
* **Database**: Entity Framework Core with SQLite
* **Authentication**: Token-based system
* **API**: RESTful endpoints

## 📄 License

This project is licensed under the MIT License.

