# Схеми до лабораторної роботи 3.1 - варіант 2

## 1. Діаграми класів

### 1.1. Предметна область: сутності та здібності

```mermaid
---
config:
  layout: elk
---
classDiagram
    direction TB

    class IIdentifiable {
        <<interface>>
        +string Id
    }

    class Person {
        <<abstract>>
        +string FirstName
        +string LastName
        +string Id
    }

    class Student {
        +int Course
        +string StudentId
        +string ArrivalCity
        +string Passport
        +int StudyHours
        +int Dances
        +Study()
        +Dance()
    }

    class TaxiDriver {
        +string DriverLicense
        +int Trips
        +int StudyHours
        +int Dances
        +Drive()
        +Study()
        +Dance()
    }

    class Acrobat {
        +string CertificateId
        +int ExperienceYears
        +int StudyHours
        +int Dances
        +Study()
        +Dance()
    }

    class IStudier {
        <<interface>>
        +Study()
    }

    class IDriver {
        <<interface>>
        +Drive()
    }

    class IDancer {
        <<interface>>
        +Dance()
    }

    class Validator {
        <<static>>
        +IsValid(pattern, value) bool
        +Require(pattern, value, fieldName)
    }

    IIdentifiable <|.. Person : реалізує
    Person <|-- Student : успадковує
    Person <|-- TaxiDriver : успадковує
    Person <|-- Acrobat : успадковує

    IStudier <|.. Student : реалізує
    IStudier <|.. TaxiDriver : реалізує
    IStudier <|.. Acrobat : реалізує
    IDriver <|.. TaxiDriver : реалізує
    IDancer <|.. Student : реалізує
    IDancer <|.. TaxiDriver : реалізує
    IDancer <|.. Acrobat : реалізує

    Person ..> Validator : перевіряє ім'я та прізвище
    Student ..> Validator : перевіряє свої поля
    TaxiDriver ..> Validator : перевіряє свої поля
    Acrobat ..> Validator : перевіряє свої поля
```

### 1.2. Збереження даних: репозиторій, сховище, серіалізація

```mermaid
---
config:
  layout: elk
---
classDiagram
    direction TB

    class PersonRepository {
        +Add(person)
        +GetAll() Person[]
        +FindById(id) Person
        +FindByLastName(lastName) Person[]
        +Remove(id) bool
        +Update(person)
    }

    class IFileStorage {
        <<interface>>
        +ReadLines() string[]
        +WriteLines(lines)
        +AppendLines(lines)
    }

    class FileStorage {
        +ReadLines() string[]
        +WriteLines(lines)
        +AppendLines(lines)
    }

    class SerializerRegistry {
        +IPersonSerializer[] All
        +ByTypeName(typeName) IPersonSerializer
        +For(person) IPersonSerializer
    }

    class IPersonSerializer {
        <<interface>>
        +string TypeName
        +FieldDefinition[] Fields
        +CanSerialize(person) bool
        +GetValues(person) string[]
        +Create(data) Person
    }

    class PersonSerializerBase {
        <<abstract>>
    }

    class StudentSerializer
    class TaxiDriverSerializer
    class AcrobatSerializer

    class FieldDefinition {
        +string Key
        +string Label
        +string Pattern
        +bool Prompted
        +IsValid(value) bool
    }

    class RecordData {
        +string TypeName
        +string[] Keys
        +string[] Values
        +Get(key) string
        +GetInt(key) int
    }

    class RecordParser {
        <<static>>
        +Parse(lines) RecordData[]
    }

    class RecordWriter {
        <<static>>
        +Format(serializer, person) string[]
    }

    class Person {
        <<abstract>>
    }

    PersonRepository ..> IFileStorage : читає і пише рядки
    PersonRepository ..> SerializerRegistry : шукає серіалізатор типу
    PersonRepository ..> RecordParser : розбирає рядки файлу
    PersonRepository ..> RecordWriter : формує рядки запису
    PersonRepository ..> Person : повертає і зберігає

    IFileStorage <|.. FileStorage : реалізує

    SerializerRegistry ..> IPersonSerializer : зберігає масив серіалізаторів
    IPersonSerializer <|.. PersonSerializerBase : реалізує
    PersonSerializerBase <|-- StudentSerializer : успадковує
    PersonSerializerBase <|-- TaxiDriverSerializer : успадковує
    PersonSerializerBase <|-- AcrobatSerializer : успадковує

    IPersonSerializer ..> FieldDefinition : описує поля типу
    IPersonSerializer ..> RecordData : створює особу з даних
    RecordParser ..> RecordData : повертає прочитані записи
    RecordWriter ..> IPersonSerializer : бере ключі та значення
```

Клас `FileStorage` та інтерфейс `IFileStorage` винесені в окремий проєкт `Lab1.Storage`. Усі інші класи належать проєкту `Lab1.App`.

### 1.3. Керування: меню, дії та завдання варіанта

```mermaid
---
config:
  layout: elk
---
classDiagram
    direction TB

    class Program {
        -Main()
    }

    class ConsoleMenu {
        +Run()
        -AddPerson()
        -ShowAll()
        -RemovePerson()
        -FindByLastName()
        -FindById()
        -ExecuteAction()
        -ShowVariantTask()
    }

    class PersonRepository {
        +Add(person)
        +GetAll() Person[]
        +Update(person)
    }

    class SerializerRegistry {
        +All
        +For(person) IPersonSerializer
    }

    class IPersonAction {
        <<interface>>
        +string Name
        +CanExecute(person) bool
        +Execute(person)
    }

    class StudyAction
    class DriveAction
    class DanceAction

    class StudentStatistics {
        -string universityCity
        +CountFirstYear(people) int
        +GetFirstYearFromOtherCities(people) Student[]
        +PercentFromOtherCities(people) double
    }

    class DemoDataSeeder {
        +Seed() int
    }

    class Person {
        <<abstract>>
    }

    class Student

    Program ..> ConsoleMenu : створює і запускає
    Program ..> PersonRepository : створює і передає залежності
    Program ..> SerializerRegistry : створює серіалізатори
    Program ..> IPersonAction : створює дії

    ConsoleMenu ..> PersonRepository : додає, шукає, видаляє
    ConsoleMenu ..> SerializerRegistry : бере типи та поля для вводу
    ConsoleMenu ..> IPersonAction : виконує доступні дії
    ConsoleMenu ..> StudentStatistics : розв'язує завдання варіанта
    ConsoleMenu ..> DemoDataSeeder : завантажує тестові дані

    DemoDataSeeder ..> PersonRepository : додає записи

    IPersonAction <|.. StudyAction : реалізує
    IPersonAction <|.. DriveAction : реалізує
    IPersonAction <|.. DanceAction : реалізує
    IPersonAction ..> Person : змінює стан особи

    StudentStatistics ..> Student : аналізує курс і місто прибуття
    Person <|-- Student : успадковує
```

## 2. Взаємодія об'єктів: додавання особи

```mermaid
sequenceDiagram
    actor User as Користувач
    participant Menu as ConsoleMenu
    participant Reg as SerializerRegistry
    participant Ser as IPersonSerializer
    participant Repo as PersonRepository
    participant Wr as RecordWriter
    participant St as FileStorage

    User->>Menu: пункт 1 - Додати особу
    Menu->>Reg: отримати список типів
    Reg-->>Menu: Student, TaxiDriver, Acrobat
    User->>Menu: вибір типу

    loop для кожного поля типу
        Menu->>User: запит значення
        User->>Menu: введення
        Note over Menu: перевірка регулярним виразом, повтор при помилці
    end

    Menu->>Ser: Create(RecordData)
    Ser-->>Menu: Person (конструктор ще раз перевіряє дані)
    Menu->>Repo: Add(person)
    Repo->>Repo: FindById - перевірка унікальності
    Repo->>Wr: Format(serializer, person)
    Wr-->>Repo: рядки запису
    Repo->>St: AppendLines(lines)
    St-->>Repo: запис дописано у people.txt
    Repo-->>Menu: готово
    Menu-->>User: Особу додано
```

## 3. Завдання варіанта: відсоток студентів 1-го курсу з інших міст

```mermaid
---
config:
  layout: elk
---
flowchart TD
    A(["Початок"]) --> B["Прочитати всіх осіб з файлу: GetAll"]
    B --> C["total = 0, other = 0"]
    C --> D{"Є ще особа?"}
    D -->|"так"| E{"Це студент 1-го курсу?"}
    E -->|"ні"| D
    E -->|"так"| F["total = total + 1"]
    F --> G{"Місто прибуття не дорівнює місту університету?"}
    G -->|"ні"| D
    G -->|"так"| H["other = other + 1, додати студента до результату"]
    H --> D
    D -->|"ні"| I{"total = 0?"}
    I -->|"так"| J["Відсоток = 0"]
    I -->|"ні"| K["Відсоток = other / total * 100"]
    J --> L(["Вивести кількість, відсоток і дані студентів"])
    K --> L
```

## 4. Читання файлу: розбір записів

```mermaid
---
config:
  layout: elk
---
flowchart TD
    A(["Початок"]) --> B["Прочитати рядки файлу в масив"]
    B --> C["inRecord = false, результат = порожній масив"]
    C --> D{"Є ще рядок?"}
    D -->|"так"| E["line = рядок без пробілів по краях"]
    E --> F{"Рядок порожній?"}
    F -->|"так"| D
    F -->|"ні"| G{"inRecord = false?"}

    G -->|"так"| H{"Рядок схожий на заголовок: Тип Назва?"}
    H -->|"ні"| X1["FormatException: невірний заголовок"]
    H -->|"так"| I["Запам'ятати тип і назву, inRecord = true"]
    I --> D

    G -->|"ні"| J{"Знайдено пару: ключ і значення?"}
    J -->|"так"| K["Додати ключ і значення до запису"]
    J -->|"ні"| L{"Це символ відкриття чи закриття запису?"}
    L -->|"ні"| X2["FormatException: невірний рядок"]
    L -->|"так"| M
    K --> M{"Рядок закінчується на такі символи: дужка і крапка з комою?"}
    M -->|"так"| N["Створити RecordData, додати до результату, inRecord = false"]
    M -->|"ні"| D
    N --> D

    D -->|"ні"| O{"inRecord = true?"}
    O -->|"так"| X3["FormatException: запис не завершений"]
    O -->|"ні"| P(["Повернути масив записів"])
```
