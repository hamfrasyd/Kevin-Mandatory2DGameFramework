# KevMandatory2DGameFramework

> **📚 [Full API Documentation](http://primpio.com/Mandatory2DGameFramework/Documentation/html/index.html)**

A comprehensive .NET 8.0 library for building 2D games with a focus on clean architecture and design patterns. This framework provides a robust foundation for game development with creature management, combat systems, equipment handling, world management, and comprehensive logging capabilities.

## Table of Contents

- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Core Concepts](#core-concepts)
- [Usage Examples](#usage-examples)
- [Design Patterns](#design-patterns)
- [Project Structure](#project-structure)
- [Contributing](#contributing)

## Features

- 🎮 **Creature System**: Pre-built creature classes (Warrior, Mage, Hunter) with extensible base class
- ⚔️ **Combat System**: Flexible attack strategies (Melee/Ranged) with damage calculation and defense mitigation
- 🛡️ **Equipment System**: Weapons and armor with decorator pattern for stat modifications
- 🌍 **World Management**: 2D world with boundaries and object tracking
- 📝 **Logging**: Built-in application and combat logging with observer pattern
- ⚙️ **Configuration**: XML-based configuration system
- 🏭 **Factory Patterns**: Abstract Factory and Factory Method for object creation
- 🎯 **SOLID Principles**: Clean, maintainable code following SOLID principles

## Requirements

- .NET 8.0 SDK or later
- Visual Studio 2022 or VS Code

## Installation

### Option 1: Project Reference (Recommended for Development)

If you have the source code, add a project reference to your `.csproj` file:

```xml
<ItemGroup>
  <ProjectReference Include="path\to\Mandatory2DGameFramework\Mandatory2DGameFramework.csproj" />
</ItemGroup>
```

### Option 2: NuGet Package
#### [Nuget link](https://www.nuget.org/packages/KevinsMandatory2DGameFramework)

```bash
dotnet add package  KevinsMandatory2DGameFramework 
```

## Quick Start

### 1. Create a World

```csharp
using Mandatory2DGameFramework.Domain.Environment;

World world = new World(maxX: 100, maxY: 100, name: "My Game World");
```

### 2. Create Creatures

```csharp
using Mandatory2DGameFramework.Domain.Creatures.Factories;
using Mandatory2DGameFramework.Domain.Enums;

// Use the factory to create creatures
Creature warrior = CreatureFactory.CreateCreature(ClassType.Warrior, "Thorin");
Creature mage = CreatureFactory.CreateCreature(ClassType.Mage, "Gandalf");
Creature hunter = CreatureFactory.CreateCreature(ClassType.Hunter, "Legolas");

// Add creatures to the world
world.AddCreature(warrior);
world.AddCreature(mage);
world.AddCreature(hunter);
```

### 3. Equip Items

```csharp
using Mandatory2DGameFramework.Domain.Equipment.Factories;
using Mandatory2DGameFramework.Domain.Equipment.Armor;
using Mandatory2DGameFramework.Domain.Equipment.Weapons;
using Mandatory2DGameFramework.Domain.Enums;

// Create items using Abstract Factory
IItemFactory warriorFactory = new WarriorItemFactory();
IWeapon sword = warriorFactory.CreateWeapon("Iron Sword", damage: 30, range: 1);
IDefenceItem helmet = warriorFactory.CreateHelmet("Iron Helmet", defense: 10);

// Equip the creature
warrior.EquipWeapon(sword);
warrior.EquippedArmor.Add(helmet);
```

### 4. Set Up Logging

```csharp
using Mandatory2DGameFramework.Domain.Logging.Observers;
using Mandatory2DGameFramework.Domain.Logging.Infrastructure;
using System.Diagnostics;

// Initialize logger
MyLogger.Instance.SetDefaultLevel(SourceLevels.All);

// Attach observers to creatures
warrior.Attach(CombatLogger.Instance);
warrior.Attach(ApplicationLogger.Instance);
```

### 5. Perform Combat

```csharp
// Perform an attack
warrior.PerformAttack(mage, range: 1);

// Check creature status
Console.WriteLine(warrior.ToString());
// Output: [Creature: Thorin] ID: 1 - HP: 1000/1000 - Status: Alive - Weapon: Iron Sword - Armor: 1 pieces (10 defense) - Position: (0, 0)
```

## Core Concepts

### Creatures

Creatures are the main entities in your game. The framework provides three pre-built classes:

- **Warrior**: Melee fighter with high HP (1000), uses Plate armor and Swords
- **Mage**: Ranged caster with medium HP (800), uses Cloth armor and Staves  
- **Hunter**: Ranged attacker with medium-high HP (900), uses Leather armor and Guns

You can extend the `Creature` base class to create custom creature types.

### World

The `World` class manages all entities in your game:
- Enforces world boundaries
- Tracks all creatures and world objects
- Provides methods to add/remove entities

### Combat System

The combat system uses the **Strategy Pattern** for flexible attack behaviors:

- **MeleeAttackStrategy**: For close-range combat
- **RangedAttackStrategy**: For long-range combat

Damage calculation includes:
- Base weapon damage
- Class-specific modifiers (via Template Method pattern)
- Defense mitigation from armor

### Equipment System

- **Weapons**: Implement `IWeapon` interface, provide damage and range
- **Armor**: Implement `IDefenceItem` interface, provide defense values
- **Decorator Pattern**: Modify armor stats dynamically (boost/weaken defense)

### Logging

The framework includes two built-in loggers:

- **ApplicationLogger**: Logs object creation, modifications, and general events
- **CombatLogger**: Logs combat-related events (attacks, damage, deaths)

Both use the **Observer Pattern** to automatically log creature events.

## Usage Examples

### Complete Example: Battle Simulation

```csharp
using Mandatory2DGameFramework.Domain.Environment;
using Mandatory2DGameFramework.Domain.Creatures.Factories;
using Mandatory2DGameFramework.Domain.Equipment.Factories;
using Mandatory2DGameFramework.Domain.Enums;
using Mandatory2DGameFramework.Domain.Logging.Observers;
using Mandatory2DGameFramework.Domain.Logging.Infrastructure;
using System.Diagnostics;

// Initialize logging
MyLogger.Instance.SetDefaultLevel(SourceLevels.All);

// Create world
World world = new World(100, 100, "Battle Arena");

// Create creatures
Creature warrior = CreatureFactory.CreateCreature(ClassType.Warrior, "Thorin");
Creature mage = CreatureFactory.CreateCreature(ClassType.Mage, "Gandalf");

// Equip warrior
IItemFactory warriorFactory = new WarriorItemFactory();
warrior.EquipWeapon(warriorFactory.CreateWeapon("Excalibur", 50, 1));
warrior.EquippedArmor.Add(warriorFactory.CreateHelmet("Dragon Helm", 15));
warrior.EquippedArmor.Add(warriorFactory.CreateChestArmor("Plate Mail", 25));

// Equip mage
IItemFactory mageFactory = new MageItemFactory();
mage.EquipWeapon(mageFactory.CreateWeapon("Staff of Power", 40, 20));
mage.EquippedArmor.Add(mageFactory.CreateHelmet("Wizard Hat", 5));

// Attach observers
warrior.Attach(CombatLogger.Instance);
mage.Attach(CombatLogger.Instance);

// Add to world
world.AddCreature(warrior);
world.AddCreature(mage);

// Battle!
warrior.PerformAttack(mage, range: 1);  // Warrior attacks mage
mage.PerformAttack(warrior, range: 25); // Mage attacks from range

// Check results
Console.WriteLine(warrior.ToString());
Console.WriteLine(mage.ToString());
```

### Custom Creature Class

```csharp
using Mandatory2DGameFramework.Domain.Creatures.Base;
using Mandatory2DGameFramework.Domain.Combat.Strategies;

public class Paladin : Creature
{
    public Paladin(string name, int maxHP) : base(name, maxHP)
    {
        // Set attack strategy
        SetAttackStrategy(new MeleeAttackStrategy());
    }

    protected override int ApplyAttackModifiers(int baseDamage)
    {
        // Paladins deal extra damage when above 50% HP
        if (HitPoint > MaxHP / 2)
        {
            return (int)(baseDamage * 1.2); // 20% bonus
        }
        return baseDamage;
    }
}
```

### Using Decorators for Armor Modifications

```csharp
using Mandatory2DGameFramework.Domain.Equipment.Decorators;
using Mandatory2DGameFramework.Domain.Equipment.Armor;
using Mandatory2DGameFramework.Domain.Enums;

// Create base armor
DefenceItem baseHelmet = new DefenceItem("Basic Helmet", ArmorSlot.Head, ArmorType.Plate, 10);

// Apply decorators
IDefenceItem enhancedHelmet = new BoostDefenceDecorator(baseHelmet, 5); // +5 defense
IDefenceItem weakenedHelmet = new WeakenDefenceDecorator(enhancedHelmet, 2); // -2 defense

// Final defense: 10 + 5 - 2 = 13
warrior.EquippedArmor.Add(weakenedHelmet);
```

## Design Patterns

This framework demonstrates several design patterns:

- **Factory Method**: `CreatureFactory` creates different creature types
- **Abstract Factory**: `IItemFactory` creates families of related items (Warrior/Mage/Hunter items)
- **Strategy**: `IAttackStrategy` for different attack behaviors
- **Observer**: `ICreatureObserver` for event logging and notifications
- **Decorator**: `DefenceItemDecorator` for dynamic armor stat modifications
- **Composite**: `EquippedArmorSet` for managing armor collections
- **Template Method**: `Creature.PerformAttack()` with customizable modifiers

## Project Structure

The project is organized using a **domain-driven structure** that groups related functionality together:

```
Mandatory2DGameFramework/
├── Domain/                    # Domain-driven organization
│   ├── Creatures/            # Creature system
│   │   ├── Base/             # Creature base class
│   │   ├── Classes/          # Warrior, Mage, Hunter implementations
│   │   ├── Factories/        # CreatureFactory
│   │   └── Interfaces/       # IAttackable
│   ├── Combat/               # Combat system
│   │   ├── Strategies/       # MeleeAttackStrategy, RangedAttackStrategy
│   │   └── Interfaces/       # IAttackStrategy
│   ├── Equipment/            # Equipment system
│   │   ├── Weapons/          # AttackItem, IWeapon
│   │   ├── Armor/            # DefenceItem, EquippedArmorSet, interfaces
│   │   ├── Decorators/       # Armor stat modifiers
│   │   └── Factories/        # Item factories (Warrior, Mage, Hunter)
│   ├── Environment/          # World management
│   │   ├── World.cs          # World class
│   │   └── WorldObject.cs    # Base world object
│   ├── Logging/              # Logging system
│   │   ├── Infrastructure/   # MyLogger
│   │   ├── Observers/         # ApplicationLogger, CombatLogger
│   │   └── Interfaces/       # ICreatureObserver
│   └── Enums/                # Game enumerations
│       ├── ClassType.cs
│       ├── WeaponType.cs
│       ├── ArmorType.cs
│       └── ArmorSlot.cs
└── Configuration/            # Configuration system
    ├── ConfigReaders/         # ConfigReader, GameConfig
    └── XMLConfigurations/     # GameConfiguration.xml
```

## Contributing

Contributions are welcome! Please ensure your code follows the existing patterns and SOLID principles used throughout the framework.

## License

MIT License - see LICENSE.txt for details.

---

**Need more details?** Check out the [full API documentation](http://primpio.com/Mandatory2DGameFramework/Documentation/html/index.html) for comprehensive class and method references.
