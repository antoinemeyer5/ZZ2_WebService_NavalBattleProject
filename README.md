# ZZ2_WebService_NavalBattleProject
Online naval battle application. Developed in C# with .NetCore 7.

## Project informations

[comment]: <> (Badges: [todo])

### Status

In development

### Tools

Visual Studio 2019 or 2022 Community

.NET Core 7.0

### Languages

C# 

### Arhitecture

API ResT

NetCore API - MVC Pattern

### Roadmap

Started on: 16-DEC-2022

Due on: 05-MAR-2023

## Course informations

Subject: Web Service

Teacher: "CHEVILLOT, Pierre-Loïc" <pierre-loic.chevillot@capgemini.com>;

## Group informations

Number: 10

Members:

| | Last name | First name | Specialty | E-mail |
|---|---|---|---|---|
| M. | CLIQUOT | Theo | F2 | Theo.CLIQUOT@etu.uca.fr |
| M. | MEYER | Antoine | F2 | Antoine.MEYER@etu.uca.fr |
| M. | ZOGHLAMI | Abdeljalil | F5 | Abdeljalil.ZOGHLAMI@etu.uca.fr |
| MME. | BOULARD | Lyloo | F5 | Lyloo.BOULARD@etu.uca.fr |

[comment]: <> (## Installation ```git clone```)

## Convention in the code

### Language

English

### Vocabulary

| Variable type | Case |
|---|---|
| Class, Property, Method | PascalCase |
| Variable | camelCase |

### Objects

* Map 

Represents the game board of a player. A game is composed of 2 ``Map`` objects.

```C# 
public class Map
{
  List<List<int>> body; //0:empty; -1:touched; -2:missed; [shipIdentifier]:ship
  string name;
  List<Ship> associatedShips; //example: [ship1; ship3; ship4]
}
```

* Ship

Represents a ``Ship``. It is an abstract global class and not directly instantiable. It gathers the attributes common to all ``Ship``.

```C#
public class Ship
{

}
```
