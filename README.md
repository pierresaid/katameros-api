# Ⲕⲁⲧⲁⲙⲉⲣⲟⲥ - katameros-api

API for the daily readings in the Coptic Orthodox Church.
Supports multiple bible versions and langages.

https://api.katameros.app/readings/gregorian/03-05-2023?languageId=2

Website https://katameros.app/?lang=en

Front end : https://github.com/pierresaid/katameros-web-app

[What is the coptic lectionnary](https://coptic-wiki.org/lectionary#:~:text=Generate%20in%20PDF-,lectionary,-A%20set%20of)


## Usage

**BaseUrl**
`https://api.katameros.app/`

### Readings

**Endpoint**
```http
GET /readings/{calendar}/{date}
```

**Parameters**
| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| calendar | string | Yes | The calendar used for the date. Can be either gregorian or coptic. |
| date | string | Yes | The date in the format `dd-mm-yyyy`. |
| languageId | integer | No | The ID of the language to use. Can be either 1 for French, 2 for English, 3 for Arabic, 4 for Italian. If no languageId is specified, the API will default to French. |
| bibleId | integer | No | The ID of the Bible to use. |

**Example Requests**
```http
GET https://api.katameros.app/readings/gregorian/03-05-2023?languageId=2
GET https://api.katameros.app/readings/coptic/25-08-1739
GET https://api.katameros.app/readings/gregorian/03-05-2023?languageId=4&bibleId=5
```
**Languages**
| Language | ID |
| --- | --- |
| French | 1 |
| English | 2 |
| Arabic | 3 |
| Italian | 4 |
| German | 6 |
| Polish | 7 |
| Spanish | 8 |

**Bibles**
| Id | Name | Language |
| --- | --- | --- |
| 1 | Louis Segond 1910 (LSG) - 1910, Deutérocanonique : Bible de Jérusalem (JER) - 1973 | French |
| 2 | NKJV | English |
| 3 | Arabic | Arabic |
| 4 | Riveduta 1927 (RIV) | Italian |
| 5 | CEI 2008 (Psalms RIV) | Italian |
| 7 | Einheitsübersetzung der Heiligen Schrift (1980) [Quadro-Bibel 5.0] | German |
| 8 | Uwspółcześniona Biblia gdańska | Polish |
| 9 | Reina Valera 1865 | Spanish |


### Feasts
**Endpoint**
```http
GET /feasts/{year}/{languageId}
```
| Parameter | Type | Format |
| --- | --- | --- |
| year | integer | YYYY |
| languageId | integer | 1-8 |

**Example Requests**
```http
GET https://api.katameros.app/feasts/2023/2
```

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

[SQLITE Database](https://github.com/pierresaid/katameros-api/blob/master/Core/KatamerosDatabase.db)

## Todo
All the readings references comes from database except the feasts some are missing see [board](https://github.com/pierresaid/katameros-api/projects/1)
