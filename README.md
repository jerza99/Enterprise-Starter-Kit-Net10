# Enterprise Starter Kit - .NET 10 Clean Architecture

<div align="center">

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=for-the-badge&logo=aspnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

**La base más sólida para tus proyectos de .NET 10. Implementa Clean Architecture en minutos, no en semanas.**

[Características](#-características) • [Instalación](#-instalación) • [Uso](#-uso) • [Arquitectura](#-arquitectura) • [Tecnologías](#-tecnologías)

</div>

---

## 📋 Tabla de Contenidos

- [Características](#-características)
- [Instalación](#-instalación)
- [Configuración](#-configuración)
- [Uso](#-uso)
- [Arquitectura](#-arquitectura)
- [Tecnologías](#-tecnologías)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Contribuir](#-contribuir)
- [Licencia](#-licencia)

## ✨ Características

### 🔐 Autenticación y Autorización
- **ASP.NET Core Identity 10** completamente configurado
- Sistema de registro e inicio de sesión robusto
- Validaciones en frontend y backend
- Manejo de errores profesional con Result Pattern
- Soporte para roles y políticas (extensible)

### 🏗️ Clean Architecture
- **4 capas bien definidas** siguiendo principios SOLID
- Separación clara de responsabilidades
- Desacoplamiento total entre capas
- Fácil de mantener y escalar
- Listo para agregar nuevas funcionalidades

### 🗄️ Entity Framework Core 10
- Configurado con SQL Server
- Migraciones automáticas incluidas
- Repositorio genérico implementado
- Patrón Unit of Work (extensible)
- Optimizado para rendimiento

### 🎨 UI Profesional
- **Bootstrap 5.3** integrado
- Diseño moderno y responsive
- Sistema de diseño CSS modular
- Páginas de Login y Registro profesionales
- Navegación condicional según autenticación

## 🚀 Instalación

### Requisitos Previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) o [SQL Server LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)

### Pasos de Instalación

1. **Clonar o descargar el proyecto**
   ```bash
   git clone <repository-url>
   cd CleanIdentityStarter
   ```

2. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

3. **Configurar la base de datos**
   
   Edita `StarterKit.WebUI/appsettings.json` y configura tu cadena de conexión:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=StarterKitDb;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

4. **Aplicar migraciones**
   ```bash
   cd StarterKit.WebUI
   dotnet ef database update --project ..\StarterKit.Infrastructure\StarterKit.Infrastructure.csproj
   ```

5. **Ejecutar la aplicación**
   ```bash
   dotnet run
   ```

6. **Abrir en el navegador**
   ```
   https://localhost:5001
   ```

## ⚙️ Configuración

### Configuración de Identity

Las opciones de Identity están configuradas en `Program.cs`:

```csharp
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    // Configuración de contraseñas
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    // Configuración de usuarios
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
```

Puedes modificar estos valores según tus necesidades.

### Personalización del Diseño

El sistema de diseño está en `StarterKit.WebUI/wwwroot/css/site.css` usando variables CSS:

```css
:root {
  --color-primary: #4e54c8;
  --color-primary-dark: #3d43a8;
  --gradient-main: linear-gradient(135deg, #4e54c8 0%, #764ba2 100%);
  /* ... más variables */
}
```

## 📖 Uso

### Registro de Usuario

1. Navega a `/Account/Register`
2. Completa el formulario con:
   - Nombre y Apellido
   - Correo electrónico
   - Nombre de usuario
   - Contraseña (mínimo 6 caracteres)
3. Haz clic en "Registrarse"

### Inicio de Sesión

1. Navega a `/Account/Login`
2. Ingresa tu correo electrónico o nombre de usuario
3. Ingresa tu contraseña
4. Opcionalmente marca "Recordarme"
5. Haz clic en "Iniciar Sesión"

### Cerrar Sesión

Haz clic en "Cerrar Sesión" en el menú de navegación cuando estés autenticado.

## 🏛️ Arquitectura

Este proyecto sigue los principios de **Clean Architecture** con 4 capas principales:

```
┌─────────────────────────────────────┐
│         StarterKit.WebUI            │  ← Capa de Presentación
│  (Controllers, Views, wwwroot)      │
└─────────────────────────────────────┘
                  ↓
┌─────────────────────────────────────┐
│      StarterKit.Application         │  ← Capa de Aplicación
│   (DTOs, Interfaces, Servicios)     │
└─────────────────────────────────────┘
                  ↓
┌─────────────────────────────────────┐
│        StarterKit.Domain             │  ← Capa de Dominio
│    (Entities, Common, Business)     │
└─────────────────────────────────────┘
                  ↓
┌─────────────────────────────────────┐
│     StarterKit.Infrastructure        │  ← Capa de Infraestructura
│ (Data, Repositories, Services, EF)   │
└─────────────────────────────────────┘
```

### Principios Aplicados

- **Dependency Inversion**: Las capas superiores no dependen de las inferiores
- **Single Responsibility**: Cada clase tiene una única responsabilidad
- **Open/Closed**: Abierto para extensión, cerrado para modificación
- **Interface Segregation**: Interfaces específicas y pequeñas
- **Dependency Injection**: Inyección de dependencias en toda la aplicación

## 🛠️ Tecnologías

### Backend
- **.NET 10** - Framework principal
- **ASP.NET Core MVC** - Framework web
- **ASP.NET Core Identity 10** - Autenticación y autorización
- **Entity Framework Core 10** - ORM
- **SQL Server** - Base de datos

### Frontend
- **Bootstrap 5.3** - Framework CSS
- **Bootstrap Icons 1.11.3** - Iconos
- **Razor Pages/Views** - Motor de vistas
- **jQuery** - Biblioteca JavaScript
- **Google Fonts (Inter)** - Tipografía

### Herramientas
- **Entity Framework Core Tools** - Migraciones
- **Visual Studio / VS Code** - IDE

## 📁 Estructura del Proyecto

```
CleanIdentityStarter/
│
├── StarterKit.Domain/
│   ├── Common/
│   │   └── BaseEntity.cs          # Entidad base con campos comunes
│   └── Entities/
│       └── User.cs                 # Entidad Usuario extendiendo IdentityUser
│
├── StarterKit.Application/
│   ├── DTOs/
│   │   ├── AuthResult.cs           # Resultado de autenticación
│   │   ├── LoginUserDto.cs         # DTO para login
│   │   └── RegisterUserDto.cs     # DTO para registro
│   └── Interfaces/
│       ├── IAuthService.cs         # Interfaz del servicio de autenticación
│       ├── IRepository.cs          # Interfaz del repositorio genérico
│       └── IUserRepository.cs      # Interfaz del repositorio de usuarios
│
├── StarterKit.Infrastructure/
│   ├── Data/
│   │   └── ApplicationDbContext.cs # Contexto de Entity Framework
│   ├── Migrations/                 # Migraciones de base de datos
│   ├── Repositories/
│   │   ├── Repository.cs           # Implementación del repositorio genérico
│   │   └── UserRepository.cs       # Implementación del repositorio de usuarios
│   └── Services/
│       └── AuthService.cs          # Implementación del servicio de autenticación
│
└── StarterKit.WebUI/
    ├── Controllers/
    │   ├── AccountController.cs    # Controlador de autenticación
    │   └── HomeController.cs        # Controlador principal
    ├── Views/
    │   ├── Account/
    │   │   ├── Login.cshtml         # Vista de login
    │   │   └── Register.cshtml     # Vista de registro
    │   ├── Home/
    │   │   ├── Index.cshtml         # Página principal
    │   │   └── Privacy.cshtml       # Política de privacidad
    │   └── Shared/
    │       └── _Layout.cshtml       # Layout principal
    └── wwwroot/
        └── css/
            └── site.css             # Estilos personalizados
```

## 🔧 Extensión del Proyecto

### Agregar una Nueva Entidad

1. **Crear la entidad en Domain**
   ```csharp
   public class Product : BaseEntity
   {
       public string Name { get; set; }
       public decimal Price { get; set; }
   }
   ```

2. **Agregar al DbContext**
   ```csharp
   public DbSet<Product> Products { get; set; }
   ```

3. **Crear migración**
   ```bash
   dotnet ef migrations add AddProduct --project ..\StarterKit.Infrastructure
   ```

4. **Aplicar migración**
   ```bash
   dotnet ef database update --project ..\StarterKit.Infrastructure
   ```

### Agregar un Nuevo Servicio

1. **Crear interfaz en Application**
   ```csharp
   public interface IProductService
   {
       Task<ProductDto> GetByIdAsync(Guid id);
   }
   ```

2. **Implementar en Infrastructure**
   ```csharp
   public class ProductService : IProductService
   {
       // Implementación
   }
   ```

3. **Registrar en Program.cs**
   ```csharp
   builder.Services.AddScoped<IProductService, ProductService>();
   ```

## 📝 Notas Importantes

- El proyecto usa **Guid** como tipo de ID para todas las entidades
- Las migraciones están incluidas y listas para usar
- El diseño es completamente responsive
- El código sigue las mejores prácticas de .NET

## 🤝 Contribuir

Este es un producto comercial. Si deseas contribuir o reportar problemas, por favor contacta al desarrollador.

## 📄 Licencia

Este proyecto está bajo una licencia comercial. Ver el archivo [LICENSE](LICENSE) para más detalles.

## 📧 Soporte

Para soporte, preguntas o sugerencias, por favor contacta al desarrollador.

---

<div align="center">

**Desarrollado con ❤️ usando .NET 10 y Clean Architecture**

⭐ Si este proyecto te ha sido útil, considera darle una estrella

</div>
