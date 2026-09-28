# Ⲕⲁⲧⲁⲙⲉⲣⲟⲥ - katameros-api

API for the daily readings in the Coptic Orthodox Church.
Supports multiple bible versions and languages.

https://api.katameros.app/readings/gregorian/03-05-2023?languageId=2

Website https://katameros.app/en/

Front end : https://github.com/pierresaid/katameros-web-app

[What is the coptic lectionary](https://chatgpt.com/?q=What%20is%20the%20coptic%20lectionary)

## Usage

Base URL: `https://api.katameros.app/`

### Readings

```http
GET /readings/gregorian/{date}
GET /readings/coptic/{date}
```

- `date`: `dd-MM-yyyy`, a Gregorian date on `/gregorian`, a Coptic date on `/coptic`.
- `languageId` (optional): see [Languages and Bibles](#languages-and-bibles). Defaults to French (1).
- `bibleId` (optional): a Bible of the requested language. Defaults to the language's default Bible.

### Feasts and fasts

```http
GET /feasts/{year}/{languageId}
GET /fasts/{year}/{languageId}
```

- `year`: a Gregorian year, e.g. `2026`.
- `languageId`: see [Languages and Bibles](#languages-and-bibles).

https://api.katameros.app/feasts/2026/2

https://api.katameros.app/fasts/2026/2

## Languages and Bibles

| languageId | Language | bibleId |
| --- | --- | --- |
| 1 | French | 1 Louis Segond 1910 |
| 2 | English | 2 NKJV |
| 3 | Arabic | 11 Van Dyck, with diacritics (default) · 3 Van Dyck, without diacritics |
| 4 | Italian | 4 CEI 1974 (default) · 5 CEI 2008 (Psalms RIV) |
| 6 | German | 7 Einheitsübersetzung der Heiligen Schrift (1980) [Quadro-Bibel 5.0] |
| 7 | Polish | 8 Uwspółcześniona Biblia gdańska |
| 8 | Spanish | 9 Reina Valera 1865 |
| 9 | Dutch | 10 Stichting Herziening Statenvertaling (HSV) |
| 10 | Thai | 12 Thai Standard Version (THSV) |

The Synaxarium is only included in French, English, Arabic, Italian and Dutch.

## Database

The lectionary tables, Bible texts, Synaxarium, feasts and fasts are in one SQLite file, [`Core/KatamerosDatabase.db`](https://github.com/pierresaid/katameros-api/raw/master/Core/KatamerosDatabase.db).

## Motivation

This project was heavily inspired by the online Coptic lectionary in PHP. ([Just like this site](https://st-takla.org/zJ/index.php?option=com_katamaros&sm=3-3&c=&tht=&dbl=en&Itemid=3))

I wanted to re-create this project in an API to be able to use the daily readings on different clients.

## Run the project

Install the [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0), then:

```bash
git clone https://github.com/pierresaid/katameros-api.git
cd katameros-api/API
dotnet run
```

The API listens on http://localhost:5241, with Swagger UI at http://localhost:5241/swagger. You can also open `Katameros.sln` in [Visual Studio](https://visualstudio.microsoft.com/).
