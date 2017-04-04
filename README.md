# BlueTapeCrew

[![N|Solid](https://bluetapecrew.com/content/logo.png)](https://bluetapecrew.com)

BlueTapeCrew is a client website I built from scratch in ASP.NET.

I was far less experienced when I initially pushed this to production and have been improving the source gradually over time.

## Features
  - Front end based on [KeenThemes] Metronic Store Template
  - Social Logins: Instagram, Google+, Microsoft, FaceBook
  - Paypal integration
  - MailChimp Subsription Integration
  - Mobile Repsonsive
  - Client Accessable Admin
  - Built in Image Handling
  - SSL
  - GMail SMTP
 
## Tech
BlueTapeCrew uses a typical .NET stack:
- Anemic Domain Model W/Service Layer
- Dependency Injection
- Homegrown, I did not use an existing schema or site design.
- ASP.NET MVC 5
- SQL Server
- Razor
- AngularJS Admin
- HTML5
- CSS

* **Live Site: Azure Web App/SQL Server Instance** https://bluetapecrew.com

## Development

*I am open to contributions, and you are welcome to use the site code with one caveat; the Admin contains elements from a purchased templates, you must purchase a license from [KeenThemes] (Metronic Admin Template)

## Roadmap
- Write Unit tests
- Configure Scope of DI Container
- Migrate to ASP.NET Core
- Add support for multi-tenancy
- Strip paid template elements from Admin
- Complete Admin functionality so site is fully configurable from UI
- Complete de-branding of the source
- Replace stored procedures and views with ORM queries
- Convert admin app to 100% angular

## Installation

### Environment
**IDE:** Visual Studio 2015
**.NET** Version: 4.5

### Connetion Strings
**There are two connection strings:**
 - UserEntities, which handles MS User Identity
 - BtcEntities

Add connection strings to web.config:
```sh
<connectionStrings>
  <add name="UserEntities"
    connectionString="Server=[SERVER];Database=[DATABASE];User ID=[USER];Password=[PASS];Trusted_Connection=False;Encrypt=True;Connection Timeout=30;" providerName="System.Data.SqlClient" />
  <add name="BtcEntities" 
    connectionString="data source=[SERVER];initial catalog=[DATABASE];persist security info=True;user id=[USER];password=               [PASS];MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
```

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

### Local Setup
Right click on Web.config in Visual Studio solution explorer
"Add Config Transform"
Do it 2x, it will add Web.Release.confg & Web.Debug config
Add a transform in Web.Debug.config for the connection strings (Web.Debug.config is in .gitignore)
**To run on Azure:** Just add the connection strings to your Azure Web App configuration

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
#### GetCart Stored Procedure
```sh
CREATE PROCEDURE [dbo].[uspGetCart] @SessionId char(24)
AS
SELECT * 
FROM CartView
WHERE CartId = @SessionId
```
[KeenThemes]: <http://keenthemes.com/free-bootstrap-templates/fully-responsive-bootstrap-based-ecommerce-frontend-theme>
[Todd Miller]: <https://toddmiller.nyc>
[BlueTapeCrew]: <https://bluetapecrew.com>

