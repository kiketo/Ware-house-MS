# Databases: Teamwork Assignment

## Project Description

Create a project of your choice and implement it using Code First approach with Entity Framework. You must use SQL Server 2017 as your database. Part of the data in SQL Server must be provided via external files 
(XML, JSON, zip, etc.) of your choice. You should create PDF reports based on your application logic. They should consists of meaningful data.

- Project examples:
  - Sports ranking
  - Store management system
  - Movie database
  - Book database
  
## Preliminary Requirements

Before you start writing code and building databases, please take your time to write a simple project specification. Together with your team members, read the requirements below and answer the following questions in a (README in your repo) in a style of your choosing.

- What is the name of your team?
- Who is your team leader?
- Who are your team members?
- What is your project going to be about?
- What features will it consist of? Explain their purpose. (Try to be as granular as possible.)
- Create a kanban board with the following data, fill it and keep it updated:
  - Name of Feature
  - Feature Owner (who will write it?)
  - Estimated time it would take (in hours, **don't overthink it**)
  - Actual time it took (in hours)
  - Estimated time it would take to unit test (in hours)
  - Actual time it took to unit test (in hours)
- For the board you could use Trello or GitLab's project system.
  - If your selected tool does not support time estimation (for example Trello), just write it in the card's description or use an addon.

Try to adhere to this project specification and make your project as close to it as possible. As you implement each feature, write down the time it really took you and compare them with the estimate. Do not be surprised if the difference between them is great, that's completely normal when you do something like this for the first time. Also, don't go crazy on features, implement a few but implement them amazingly! 

You have **until Wednesday afternoon** to present this specification to either Edo or Kiro in person or via PM in the forum, and commit it to your repository.

## General Requirements

- Completely finished project is not obligatory required. It will not be a big problem if your project is not completely finished or is not working greatly
- This team work project is for educational purposes
- Always remember, quality over quantity!

## Must Requirements

- Use Code First approach
- Use Entity Framework Core 2.0+
- Use SQL Server 2017
- At least five tables in the SQL Server database
- Provide at least two type of relations in the database and use both attributes and the Fluent API (Model builder) for configuration
- The user should be able to manipulate the database through the client (basic CRUD)
- Provide some usable user interface for the client (preferably console)
- Write unit tests for the majority of your application's features. Unisolated tests are not considered valid.
- Follow the SOLID principles and the OOP principles. The lack of SRP or DI will be punished by death.

## Should Requirements

- Load some of the data from external files (Either Excel, XML, JSON, zip, etc.) of your choice
  - For XML files should be read / written through the standard .NET parsers (of your choice)
  - For JSON serializations use a non-commercial free library / framework of your choice.
- Generate PDF reports based on your application's data. The PDF doesn't have to be pretty.
  - For PDF reports use a non-commercial free library / framework of your choice.

## Could Requirements

- You could use Service Layer of your choice

## Project Defense

Each team member will have around 20 minutes to:

- Present the project overall
- Explain how they have contributed to the project
- Explain the source code of their teammates
- Answer some theoretical questions related to this course and all the previous ones.

## Give Feedback about Your Teammates

You will be invited to provide feedback about all your teammates, their attitude to this project, their technical skills, their team working skills, their contribution to the project, etc. **The feedback is important part of the project evaluation so take it seriously and be honest.**
