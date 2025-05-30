## Struktura

aby korzystać z api, zaloguj się:
curl -X POST http://localhost:5035/api/login -d "userName=ts3&password=ts3"

przykładowy token:
"b738c4cd-ae85-4d33-bb6b-eacda197f19a"

przypisz się do taska o id 1
curl -X POST http://localhost:5035/api/tasks/1/assign/ -H "token: f9fc902f-dd55-4ca3-82ce-571db8226460"

stwórz nowy task o podanym id
curl -X POST http://localhost:5035/api/tasks/ -H "token: f9fc902f-dd55-4ca3-82ce-571db8226460" -d "typeId=1"

usuń task o id
curl -X DELETE http://localhost:5035/api/tasks/13 -H "token: f9fc902f-dd55-4ca3-82ce-571db8226460"

pozyskaj dostępne taski
curl -X GET http://localhost:5035/api/tasks/available -H "token: f9fc902f-dd55-4ca3-82ce-571db8226460"

pozyskaj wszystkie taski
curl -X GET http://localhost:5035/api/tasks/all -H "token: 755eb074-1de8-49c5-b2fa-55bb0ff42c71"

pozyskaj typy możliwych tasków
curl -X GET http://localhost:5035/api/taskTypes/all -H "token: f9fc902f-dd55-4ca3-82ce-571db8226460"



```postgresql
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
│   └── DbInitializer.cs         <-- Dodaje admina i testowe dane
│
├── Program.cs
├── Startup.cs
└── appsettings.json
```


## Baza danych
* typy czynności(bez godziny itd.) 1...n task(już szczegółowo, kto kiedy itd.)
* preferencje (w procentach) każdej osoby do danego typu czynnośi
* priorytety jako rozszerzenie kolumn
* taski: status, osoba i data może być NULL, lepiej rozszerzać kolumnami, task_taken_by, task_taken_date, task_deadline, task_status (jeżeli ktoś odrzuci review spowrotem NULL)
* tabela archiwalna
