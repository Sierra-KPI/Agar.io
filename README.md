# Agar.io

<p align="center">
  <img src="https://github.com/Sierra-KPI/Agar.io/blob/master/docs/Agar.io.png" data-canonical-src="https://github.com/Sierra-KPI/Agar.io/blob/master/docs/Agar.io.png" />
</p>

## Table of Contents

- [Description](#description)
- [Badges](#badges)
- [Contributing](#contributing)
- [License](#license)

### Description

Agar.io is a massively multiplayer online action game created by Brazilian developer Matheus Valadares. Players control one or more circular cells in a map representing a Petri dish. The goal is to gain as much mass as possible by eating agar and cells smaller than the player's cell while avoiding larger ones which can eat the player's cells. Each player starts with one cell ~~, but players can split a cell into two once it reaches a sufficient mass, allowing them to control multiple cells~~. The name comes from the substance agar, used to culture bacteria.

## Badges

[![Theme](https://img.shields.io/badge/Theme-GameDev-blueviolet)](https://img.shields.io/badge/Theme-GameDev-blueviolet)
[![Game](https://img.shields.io/badge/Game-Agario-blueviolet)](https://img.shields.io/badge/Game-Agario-blueviolet)

---

## Example

```csharp
public bool MakeMove(Cell from, Cell to, Cell through)
{
    var moves = GetPossiblePlayersMoves(from, through);
    return Array.Exists(moves, element => element == to);
}

public bool PlaceWall(Wall wall)
{
    var cell1ID = GetIdOfCellByCoordinates(wall.Coordinates);
    var cell2ID = GetIdOfCellByCoordinates(wall.EndCoordinates);
    int diff = GetDiffId(cell1ID, cell2ID);
            
    _walls = _walls.Where(elem =>
    {
        //replace to GetIdOfCellByCoordinates
        var wallCell1 = GetCellByCoordinates(elem.Coordinates);
        var wallCell2 = GetCellByCoordinates(elem.EndCoordinates);
        return (wallCell1.Id != cell1ID || wallCell2.Id != cell2ID) &&
        (wallCell1.Id != cell1ID - diff || wallCell2.Id != cell2ID - diff) &&
        (wallCell1.Id != cell1ID + diff || wallCell2.Id != cell2ID + diff) &&
        (wallCell1.Id != cell1ID || wallCell2.Id != cell1ID + diff);
            }).ToList();

        _placedWalls.Add(wall);
        return true;
    }
}
```

---

## Pictures

[![Picture1](https://github.com/Sierra-KPI/Quoridor/blob/main/docs/ConsoleGame.png)](https://github.com/Sierra-KPI/Quoridor/blob/main/docs/ConsoleGame.png)

---

## Contributing

> To get started...

### Step 1

- 🍴 Fork this repo!

### Step 2

- **HACK AWAY!** 🔨🔨🔨

---

## License

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

- **[MIT license](http://opensource.org/licenses/mit-license.php)**
- Copyright 2021 © <a href="https://github.com/Sierra-KPI" target="_blank">Sierra</a>.
