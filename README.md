## Struktura

aby korzystać z api:
curl -X POST http://localhost:5035/api/login -d "userName=ts3&password=ts3"

przykładowy token:
"b738c4cd-ae85-4d33-bb6b-eacda197f19a"

curl -X POST http://localhost:5035/api/tasks/1/assign/ -H "token: 84e98fa5-ab6a-49dd-a905-b89298bc93fa"

curl -X POST http://localhost:5035/api/tasks/13 -H "token: 84e98fa5-ab6a-49dd-a905-b89298bc93fa" -d "typeId=1"


curl -X DELETE http://localhost:5035/api/tasks/13 -H "token: 84e98fa5-ab6a-49dd-a905-b89298bc93fa" 


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
