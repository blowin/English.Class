# English.Class

Simple rest API

## Available endpoints

* Groups

  | Endpoint                                       | Method |
  |------------------------------------------------|--------|
  | api/group?pageNumber=1&pageSize=20             | GET    |
  | api/group/03d39fa2-5f66-4559-82c4-2966260acfaf | GET    |
  | api/group                                      | POST   |
  | api/group/rename                               | PUT    |
  | api/group                                      | DEL    |
  
* Students
  
  | Endpoint    | Method |
  |-------------|--------|
  | api/student | POST   |
  | api/student | DEL    |
  
* Homework
  
  | Endpoint    | Method |
  |-------------|--------|
  | api/homework | POST   |
  | api/homework | DEL    |
    
* Schedule
  
  | Endpoint    | Method |
  |-------------|--------|
  | api/schedule | POST   |
  | api/schedule | DEL    |
  
* Additional endpoints

  | Endpoint    | Method |
  |-------------|--------|
  | api/health | GET   |
  
## Swagger

Available at http://localhost:5000/swagger
