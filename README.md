# BlueTapeCrew

[![N|Solid](https://bluetapecrew.com/content/logo.png)](https://bluetapecrew.com)
- 12-25-2020 - Upgrade .NET Core 3.1 to .NET Core 5 [React Code](https://github.com/toddmillernyc/bluetapecrew/tree/develop/src/React.Site/ClientApp/src)
- 12-21-2019 - ASP.NET 4.5 to .NET Core 3.1 Migration Complete 
- 10-17-2020 - Merged React Front-End Work into develop branch

BlueTapeCrew is a client website from my freelancing days

## Features
  - Front end based on [KeenThemes] Metronic Store Template
  - Paypal integration
  - Mobile Repsonsive
  - Client Accessable Admin
  - Built in Image Handling
  - SSL
  - GMail SMTP
 
## Tech
- Developing new React UI with GraphQL Backend [React Client](https://github.com/toddmillernyc/bluetapecrew/tree/develop/src/React.Site/ClientApp/src)
- N-Layer Organization
- ASP.NET Core MVC
- SQL Server / Entity Framework Migrations
- Razor
- AngularJS / JQuery
- HTML5
- CSS

* **Live Site: https://bluetapecrew.com

## Development

*I am open to contributions, and you are welcome to use the site code with one caveat; the Admin contains elements from a purchased templates, you must purchase a license from [KeenThemes] (Metronic Admin Template)

## Roadmap
- New React front-end [React Code](https://github.com/toddmillernyc/bluetapecrew/tree/develop/src/React.Site/ClientApp/src)
- Continuing Development in REACT site with GraphQL interface

## Installation

### Environment
**IDE:** Visual Studio 2019
**.NET Core** Version: 3.1

### Connetion String
 - DefaultConnection

### Databae Migrations
Now lives in the Entities Project
Add Code-First Migration from Package Manager Console
(You will have to do this once for each connection string.  Follow the prompts in package manager console)
```sh
$ Add-Migration "[DESCRIPTION]"
```
Update Database
```sh
$ Update-Database
```

### SQL Views
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
