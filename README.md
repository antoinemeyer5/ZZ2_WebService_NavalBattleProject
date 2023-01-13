# ZZ2_WebService_NavalBattleProject |||||||
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

### Architecture

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

#### Map 

Represents the game board of a player. A game is composed of 2 ``Map`` objects.

```C# 
public class Map
{
  private List<List<int>> body;             //-1:empty; -2:touched; -3:missed; [shipIdentifier]:ship
  private string          name;
  private List<Ship>      associatedShips;  //example: [ship1; ship3; ship4]
  private Player          associatedPlayer;
}
```

#### Ship

Represents a ``Ship``. It is an abstract global class and not directly instantiable. It gathers the attributes common to all ``Ship``.

```C#
public class Ship
{
  //attributes
  public  static  int     id;                         //unique
  private         string  name          { get; set; } //name by default
  private         int     size          { get; set; } //size of the Ship
  private         int     orientation   { get; set; } //0:horizontal; 1:vertical 
  private         int     hookX         { get; set; } //boat hooking X point 
  private         int     hookY         { get; set; } //boat hooking Y point
  private         int     lifePoint     { get; set; } //init = size; dead if == 0
  
  //constructor
  public Ship()
  {
    id = id + 1;
    name = "Default Ship";
    size = 0;
    orientation = 1;
    hookX = -1;
    hookY = -1;
    lifePoint = size;
  }
}
```

* Aircraft carrier (fr:porte-avions)

```C#
public class AircraftCarrier : Ship
{
  //constructor
  public AircraftCarrier() : base()
  {
    name = "Aircraft Carrier";
    size = 5;
    lifePoint = size;
  }
}
```

* Trawler (fr:chalutier)

```C#
public class Trawler : Ship
{
  //constructor
  public Trawler() : base()
  {
    name = "Trawler";
    size = 2;
    lifePoint = size;
  }
}
```

#### Player

Represents a ``Player``. Allows to keep some information about the ``Player`` and associate it with a ``Map``, a history of games played and more.

```C#
public class Player
{
  string name;
  List<Game> history;
}
```

#### Game

Represents a ``Game``. Saves the final ``Map``s, the result and the winner as well as the duration of the ``Game``.

```C#
public class Game
{
  Map mapPlayerOne;
  Map mapPlayerTwo;
  int result; //0:player one win; 1:player two win
  string winnerName;
  float duration; //in minutes
}
```
