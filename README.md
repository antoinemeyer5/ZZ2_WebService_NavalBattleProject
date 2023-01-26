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
| Variable private | _pascal |
| Variable public | Pascal |

### Objects

#### Map 

Represents the game board of a player. A game is composed of 2 ``Map`` objects.

```C# 
public class Map
{
  //attributes
  //public
  public  int                 Id { get; }
  public  string              Name { get; set; }
  //private
  private List<List<int>>     _body;             //-1:empty; -2:touched; -3:missed; [shipIdentifier]:ship
  private int                 _lineMax = 10;
  private int                 _columnMax = 10;
  private List<Ship>          _associatedShips;  //example: [ship1; ship3; ship4]
  private Player              _associatedPlayer;
  private HashSet<(int, int)> _listTarget = new HashSet<(int, int)>();
}
```

#### Ship

Represents a ``Ship``. It is an abstract global class and not directly instantiable. It gathers the attributes common to all ``Ship``.

```C#
public class Ship
{
  //attributes
  public  static  int         Id;                         //unique
  private         string      _name          { get; set; } //name by default
  private         int         _size          { get; set; } //size of the Ship
  private         Orientation _orientation   { get; set; } //0:horizontal; 1:vertical 
  private         (int, int)  _position  { get; set; }
  private         int         _lifePoint     { get; set; } //init = size; dead if == 0
}
```

* Aircraft carrier (fr:porte-avions)

```C#
public class Carrier : Ship
{
  //constructor
  public Carrier((int, int) position, Orientation orientation)
    : base("Carrier", position, 5, orientation)
  { }
}
```

* Battleship (fr:cuirassé)

```C#
public class Battleship : Ship
{
  //constructor
  public Battleship((int, int) position, Orientation orientation)
    : base("Battleship", position, 4, orientation)
  { }
}
```

#### Player

Represents a ``Player``. Allows to keep some information about the ``Player`` and associate it with a ``Map``, a history of games played and more.

```C#
public class Player
{
  public  string      Name { get; }
  private List<int>   _history;      //id of `Game`
}
```

#### Game

Represents a ``Game``. Saves the final ``Map``s, the result and the winner as well as the duration of the ``Game``.

```C#
public class Game
{
  //public
  public  int     IdGame { get; }
  //private
  private int     _result; //0:player one win; 1:player two win
  private string  _winnerName;
  private float   _duration; //in minutes
  private Map[]   _mapHistory { get; } = new Map[2];
}
```
