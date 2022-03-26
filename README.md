# Ⲕⲁⲧⲁⲙⲉⲣⲟⲥ - katameros-api

API for the daily readings in the Coptic Orthodox Church.
Supports multiple bible versions and langages.

Website https://katameros.app/?lang=en

Front end : https://github.com/pierresaid/katameros-web-app

## Tech Stack:
- .Net core API
- SQL Server
- Entity Framework

![image](https://user-images.githubusercontent.com/32095218/160257589-5f66fff0-596e-4b24-a9ad-9741cce3705e.png)
> Database diagram

*This is not the complete diagram as there are also other tables for translations and metadata.*
*See full schema in /Database folder*


## Motivation :

This project was heavily inspired by the online coptic lectionnary in php. ([Just like this site](https://st-takla.org/zJ/index.php?option=com_katamaros&sm=3-3&c=&tht=&dbl=en&Itemid=3))

I wanted to re-create this project in an API to be able to use the daily readings on different clients.


## Run the project

Either use [VisualStudio](https://visualstudio.microsoft.com/) and open the project in `API/Katameros.sln`

Or 
- Install [dotnet sdk](https://dotnet.microsoft.com/en-us/download)
- `dotnet run` in the `API` folder

[Database](https://drive.google.com/drive/folders/18B8KzMw49UfUBqrAjeki-ew_UOTHnNHk?usp=sharing)

## Todo
All the readings references comes from database except the feasts some are missing see [board](https://github.com/pierresaid/katameros-api/projects/1)
