# Agar.io

<p align="center">
  <img src="/docs/Agar.ioHeader.png" data-canonical-src="/docs/Agar.ioHeader.png"/>
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
private List<Entity> GetAllEntities()
{
    List<Entity> allEntities = new List<Entity>();

    allEntities.AddRange(_players);
    allEntities.AddRange(_food);

    return allEntities;
}

private void SpawnFood()
{
    for (var i = 0; i < FoodCount; i++)
    {
        Food food = new Food(GetRandomPosition());

        _food.Add(food);
        Board.AddEntityToBoard(food);
    }
}

public Player AddPlayer()
{
    Player player = new Player(GetRandomPosition());

    _players.Add(player);
    Board.AddEntityToBoard(player);

    return player;
}
```

---

## Pictures

[![Picture1](/docs/Agar.io.png)](/docs/Agar.io.png)

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
