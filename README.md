# PruebaBack_Senegocia

Este proyecto consiste en una API para una red estudiantil, la cual permite registrar estudiantes, cursos y notas asociadas a cada curso, además de generar reportes de promedios tanto por curso como por estudiante.

## Tecnologias utilizadas.

* ASP.NET Core
* Entity Framework Core
* SQL Server
* C#
* Swagger (documentación de API)

## Configuración del proyecto

1. Clona el repositorio

```bash
git clone https://github.com/Benjax14/PruebaBack_Senegocia.git
cd PruebaBack_Senegocia
```

2. Verifica la cadena de conexión en `appsettings.json`:

```json
"DefaultConnection": "Server=localhost;Database=Senegocia;Trusted_Connection=True;TrustServerCertificate=True;"
```

3. Ejecuta migraciones:

```Consola del Administrador de paquetes
update-database
```


> Nota: Este comando se ejecuta desde la "Consola del Administrador de paquetes" en Visual Studio 2022, disponible en la pestaña "Herramientas"..

4. Corre la aplicación y accede al Swagger en:

```bash
https://localhost:<puerto>/swagger
```

## Decisiones Tecnicas

### Esquema de Base de datos

Se eligió el siguiente las siguientes tablas:

* `Student`
* `Course`
* `Student_Course`
* `Result`

Se optó por usar una tabla intermedia (`Student_Course`) para representar la inscripción de estudiantes a cursos. Esta estructura ofrece mayor flexibilidad y orden al manejar relaciones muchos a muchos.

### Swagger

El proyecto incluye Swagger para facilitar la documentación y pruebas de los endpoints. Sin embargo, también puedes consumir la API desde herramientas como Postman o Insomnia, utilizando las siguientes rutas:

```
https://localhost:<puerto>/api/<Entidad>
```

Dependiendo del método HTTP utilizado (GET, POST, PUT, DELETE), se ejecutará una acción distinta sobre la entidad.