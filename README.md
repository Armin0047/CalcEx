# CalcEx

CalcEx is a Persian-language Windows Forms desktop application for calculating expiry-related inventory data and displaying or printing reports. It reads its data from Microsoft SQL Server and uses Stimulsoft Reports for report output.

## Technology

- C# / Windows Forms
- .NET Framework 4.5.2
- SQL Server through OleDb
- Stimulsoft Reports

## Requirements

- Windows and Visual Studio with .NET Framework 4.5.2 targeting support
- A SQL Server database with the schema and stored procedures expected by CalcEx
- Stimulsoft assemblies version 2021.4.2 and FontAwesome.Sharp version 5.15.3

## Open the project

Open `CalcEx.csproj` in Visual Studio. The project currently references Stimulsoft and FontAwesome assemblies through the local path `..\..\DrugRepo\DrugRepo\bin\Debug`. Update those reference paths on machines where the dependencies are installed elsewhere. The third-party assemblies are not included in this repository.

The application loads `ReportCalcEx\RP_Ex.mrt` using a relative path. The report templates are not included in the current source tree; obtain them from an authorized source and place them in the `ReportCalcEx` folder. Run the application with the project root as its working directory.

## Database connection

The default connection targets local SQL Server instance `.\sql2019`, database `emamreza`, using Windows authentication. A host application can set the form's `srvname`, `dbname`, `uname`, and `psw` fields before showing it to supply another connection. Do not commit real credentials to the repository.

The database must contain the tables and stored procedures expected by the application, including `DrugLink` and the expiry calculation procedure.

## Main files

- `frm_CalcEx.cs` and its Designer files: main form, filters, data retrieval, and printing
- `frm_ListKala.cs`: item selection form
- `Classes/Database.cs`: OleDb query execution
- `Classes/DatabaseSQL.cs`: connection string construction
- `ReportCalcEx/`: expected location for Stimulsoft report templates
