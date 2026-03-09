# Tutorial

## Running the App

Execute in Powershell 

```
build.ps1
```

Or execute in macOS/Linux shell:

```
chmod +x build.sh
./build.sh
```

Run 

```
docker-compose up
```

If you have any problem try 

```
docker-compose up --force-recreate
```

## Manual testing the App

In Postman you can create these use cases

### In the first run, it will be empty

GET http://localhost:5000/api/Greetings

### Now you can add a Greetings

POST http://localhost:5000/api/Greetings

Content-Type: application/json

{
    "Message" : "Hello World"
}

### Now you can see that greeting

GET http://localhost:5000/api/Greetings/{xxxx-xxxx-xxxx-xxx}


### Now you can add some more greetings

POST http://localhost:5000/api/Greetings

Content-Type: application/json

{
    "Message" : "Hello NetCoreConf"
}

### Now you can see all the greeetings

GET http://localhost:5000/api/Greetings

### And now you can delete one greeetings

DELETE http://localhost:5000/api/Greetings/{xxxx-xxxx-xxxx-xxx}

## Architecture-as-Code (AaC)

This repository now includes architecture tests in `GreetingsUnitTest/ArchitectureTests.cs`.

Current enforced rules:

- `GreetingsCore` must not depend on `GreetingsApp`.
- `GreetingsCore.Model` must not depend on `GreetingsCore.Adapters`.
- `GreetingsCore.Ports` must not depend on `GreetingsApp`.

An additional aspirational rule is included as `[Explicit]`:

- Controllers should not depend directly on `GreetingsCore.Adapters.Db`.

Run AaC tests with:

```bash
dotnet test GreetingsUnitTest/GreetingsUnitTest.csproj --filter "FullyQualifiedName~ArchitectureTests"
```

Recommended CI step:

```bash
dotnet restore
dotnet build --no-restore
dotnet test --no-build
```


