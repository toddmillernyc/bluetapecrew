# BlueTapeCrew

[![N|Solid](https://bluetapecrew.com/content/logo.png)](https://bluetapecrew.com)

BlueTapeCrew is a client website from my freelancing days

I was far less experienced when I initially pushed this to production and have been improving the source gradually over time.

12-21-2019 - ASP.NET 4.5 to .NET Core 3.1 Migration Complete

## Features
  - Front end based on [KeenThemes] Metronic Store Template
  - Paypal integration
  - Mobile Repsonsive
  - Client Accessable Admin
  - Built in Image Handling
  - SSL
  - GMail SMTP
 
## Tech
BlueTapeCrew uses a typical .NET stack:
- Anemic Domain Model W/Service Layer
- Dependency Injection
- I did not use an existing schema or site design.
- ASP.NET Core MVC
- SQL Server
- Razor
- AngularJS
- JQuery
- HTML5
- CSS

* **Live Site: Azure Web App/SQL Server Instance** https://bluetapecrew.com

## Development

*I am open to contributions, and you are welcome to use the site code with one caveat; the Admin contains elements from a purchased templates, you must purchase a license from [KeenThemes] (Metronic Admin Template)

## Roadmap
- Impliment Admin & Client Side SPA w/modern Javascript Framework
- Social Logins: (as of 12/21 broken from .NET Core config)
- Write Unit tests
- Configure Scope of DI Container
- Add support for multi-tenancy
- Strip template elements from Admin
- Complete Admin functionality so site is fully configurable from UI
- Complete de-branding of the source
- Replace stored procedures and views with ORM queries

## Installation

### Environment
**IDE:** Visual Studio 2019
**.NET Core** Version: 3.1

### Connetion String
 - DefaultConnection

### Databae Migrations
Add Code-First Migration from Package Manager Console
(You will have to do this once for each connection string.  Follow the prompts in package manager console)
```sh
$ Add-Migration "[DESCRIPTION]"
```
Update Database
```sh
$ Update-Database
```

### SQL Server Stored Procedures and Views
You will need to create these stored procedures and Views on your SQL Database:
#### CartView View
```sh
create view [dbo].[CartView] AS
SELECT Cart.Id AS Id,CartId, [Count] As Quantity,Styles.ProductId,ProductName,LinkName,Price, StyleId,Colors.ColorText,Products.[Description],
CONCAT('Color: ',ColorText,'; Size: ',SizeText) As StyleText,
([Count] * Price) AS SubTotal,CartImages.ImageData as ImageData
FROM Cart INNER JOIN Styles ON Cart.StyleId = Styles.Id
	INNER JOIN Products ON Styles.ProductId = Products.Id
	INNER JOIN Sizes On Styles.SizeId = Sizes.Id
	INNER JOIN Colors On Styles.ColorId = Colors.Id
	Left JOIN CartImages On CartImages.ProductId = Products.Id
```
#### StyleView View
```sh
create view [dbo].[StyleView] as
select Styles.Id as Id,ProductId,SizeId,SizeOrder,SizeText,ColorId,ColorText,Price,SizeText + ' / ' + ColorText AS StyleText
from Styles inner join Sizes ON SizeId = Sizes.id
			inner join Colors on ColorId = Colors.Id
```

[KeenThemes]: <http://keenthemes.com/free-bootstrap-templates/fully-responsive-bootstrap-based-ecommerce-frontend-theme>
[Todd Miller]: <https://toddmiller.nyc>
[BlueTapeCrew]: <https://bluetapecrew.com>
