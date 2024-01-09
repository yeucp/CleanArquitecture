
# Clean Arquitecture

C# Clean arquitecture oriented to Driven Domain Desing. This project use MS SqlServer as Database, C# as backend language and JWT for authentication.




## License

This project was made under MIT License for more information read: [mit-license.org](https://mit-license.org/)

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)


## Authors

- [@yeucp](https://www.github.com/yeucp)


## Documentation




### Instalation

Instalation (Using Package Manager Console VS 2022)

```bash
  Update-Database
```

Create a new Migration

```bash
  Add-Migration MigrationNameHere -Project Infrastructure -StartupProject WebAPI -OutputDir Persistence/Migrations
```

Remove Migration

```bash
  Remove-Migration -Project Insfrastructure
```

Update Database

```bash
  Update-Database
```
    