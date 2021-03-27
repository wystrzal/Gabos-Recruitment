# Gabos-Recruitment

## General info

Application has two types of views, one with the method for search data about subjects and the second view with method to check the subject.

User can choose the specification by which he will want to search for data. (Nip / Regon / Bank account).

Application uses two API URLs from https://www.gov.pl/web/kas/api-wykazu-podatnikow-vat, for test and production. User can choose between them in views.

## Example Test Api Data

NIP :

3245174504

1854510877

REGON :

79156739856513

93992478603234

BANK ACCOUNT :

70506405335016096312945164

20028681823250598006154766

## Setup

Docker - Requirements:

- Docker
- Visual Studio 2019 v16.8+
- .NET 5.0 SDK
- DevExpress - https://go.devexpress.com/DevexpressDownload_UniversalTrial.aspx

To run App open Gabos Recruitment.sln with Visual Studio, right click on docker-compose choose Set as Startup Project and press F5.

Without Docker - Requirements:

- SQL Server Management Studio
- Microsoft SQL Server
- Visual Studio 2019 v16.8+
- .NET 5.0 SDK
- DevExpress - https://go.devexpress.com/DevexpressDownload_UniversalTrial.aspx

To run App open Gabos Recruitment.sln with Visual Studio, navigate to Gabos_Recruitment.Blazor.Server -> appsettings.json and change ConnectionStrings to localdb.  

"ConnectionStrings": {
  "ConnectionString": "Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Gabos_Recruitment",
  "EasyTestConnectionString": "Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Gabos_RecruitmentEasyTest"
},

After that right click on Gabos_Recruitment.Blazor.Server choose Set as Startup Project and press F5.


The first run may take a while.
