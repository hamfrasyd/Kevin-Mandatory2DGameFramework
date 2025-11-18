# Project Reorganization Plan
## From Design Pattern-Based to Domain-Driven Structure

### Executive Summary

This document outlines a plan to reorganize the `Mandatory2DGameFramework` project from a **design pattern-based structure** (organized by patterns like Factory, Strategy, Observer, etc.) to a **domain-driven structure** (organized by business/domain concepts like Creatures, Combat, Equipment, etc.).

**Goal**: Make the codebase more intuitive for developers working on game features, where related functionality is grouped together regardless of which design patterns are used.

---

## Current Structure Analysis

### Current Organization (Pattern-Based)

```
Mandatory2DGameFramework/
├── Composite/              # Composite pattern
│   ├── Implementations/
│   │   └── EquippedArmorSet.cs
│   └── Interfaces/
│       └── IGameItem.cs
├── Decorator/              # Decorator pattern
│   ├── Base/
│   │   └── DefenceItemDecorator.cs
│   ├── ConcreteDecorators/
│   │   ├── BoostDefenceDecorator.cs
│   │   └── WeakenDefenceDecorator.cs
│   └── Interfaces/
│       └── IDefenceItem.cs
├── Factory/                # Factory patterns
│   ├── AbstractFactory/
│   │   ├── ConcreteFactories/
│   │   │   ├── HunterItemFactory.cs
│   │   │   ├── MageItemFactory.cs
│   │   │   └── WarriorItemFactory.cs
│   │   └── IItemFactory.cs
│   └── FactoryMethod/
│       └── CreatureFactory.cs
├── Observer/               # Observer pattern
│   ├── Implementations/
│   │   ├── ApplicationLogger.cs
│   │   └── CombatLogger.cs
│   └── Interfaces/
│       └── ICreatureObserver.cs
├── Strategy/               # Strategy pattern
│   ├── Implementations/
│   │   ├── MeleeAttackStrategy.cs
│   │   └── RangedAttackStrategy.cs
│   └── Interfaces/
│       └── IAttackStrategy.cs
├── Template/               # Template Method pattern
│   ├── Base/
│   │   └── Creature.cs
│   └── Implementations/
│       ├── Hunter.cs
│       ├── Mage.cs
│       └── Warrior.cs
├── Core/                   # Core entities (mixed)
│   ├── Interfaces/
│   │   ├── IAttackable.cs
│   │   └── IWeapon.cs
│   ├── Items/
│   │   ├── AttackItem.cs
│   │   └── DefenceItem.cs
│   └── World/
│       ├── World.cs
│       └── WorldObject.cs
├── Enums/                  # Enumerations
│   ├── ArmorSlot.cs
│   ├── ArmorType.cs
│   ├── ClassType.cs
│   └── WeaponType.cs
├── Configraitons/          # Configuration
│   ├── ConfigReaders/
│   │   ├── ConfigReader.cs
│   │   └── GameConfig.cs
│   └── XMLConfigurations/
│       └── GameConfiguration.xml
└── Loggers/                # Logging infrastructure
    └── MyLogger.cs
```

### Issues with Current Structure

1. **Scattered Related Code**: Equipment-related code is split across `Core/Items/`, `Composite/`, and `Decorator/`
2. **Pattern-First Thinking**: Developers must know which pattern is used to find code
3. **Mixed Concerns**: `Core/` contains both items and world, but items are also in other folders
4. **Unclear Boundaries**: Where does combat logic live? (Strategy pattern, but also in Creature)
5. **Hard to Extend**: Adding a new creature type requires understanding multiple pattern folders

---

## Proposed Domain-Driven Structure

### New Organization (Domain-Based)

```
Mandatory2DGameFramework/
├── Domain/
│   ├── Creatures/                    # All creature-related code
│   │   ├── Base/
│   │   │   └── Creature.cs           # (from Template/Base/)
│   │   ├── Classes/                  # Creature implementations
│   │   │   ├── Warrior.cs           # (from Template/Implementations/)
│   │   │   ├── Mage.cs
│   │   │   └── Hunter.cs
│   │   ├── Factories/
│   │   │   └── CreatureFactory.cs    # (from Factory/FactoryMethod/)
│   │   └── Interfaces/
│   │       └── IAttackable.cs        # (from Core/Interfaces/)
│   │
│   ├── Combat/                       # All combat-related code
│   │   ├── Strategies/               # Attack strategies
│   │   │   ├── MeleeAttackStrategy.cs    # (from Strategy/Implementations/)
│   │   │   └── RangedAttackStrategy.cs
│   │   └── Interfaces/
│   │       └── IAttackStrategy.cs    # (from Strategy/Interfaces/)
│   │
│   ├── Equipment/                    # All equipment-related code
│   │   ├── Weapons/
│   │   │   ├── AttackItem.cs         # (from Core/Items/)
│   │   │   └── IWeapon.cs            # (from Core/Interfaces/)
│   │   ├── Armor/
│   │   │   ├── DefenceItem.cs        # (from Core/Items/)
│   │   │   ├── EquippedArmorSet.cs   # (from Composite/Implementations/)
│   │   │   ├── IDefenceItem.cs       # (from Decorator/Interfaces/)
│   │   │   └── IGameItem.cs          # (from Composite/Interfaces/)
│   │   ├── Decorators/               # Armor modifications
│   │   │   ├── Base/
│   │   │   │   └── DefenceItemDecorator.cs
│   │   │   └── BoostDefenceDecorator.cs
│   │   │   └── WeakenDefenceDecorator.cs
│   │   └── Factories/                # Equipment factories
│   │       ├── IItemFactory.cs       # (from Factory/AbstractFactory/)
│   │       ├── HunterItemFactory.cs
│   │       ├── MageItemFactory.cs
│   │       └── WarriorItemFactory.cs
│   │
│   ├── World/                        # World management
│   │   ├── World.cs                  # (from Core/World/)
│   │   └── WorldObject.cs            # (from Core/World/)
│   │
│   └── Logging/                      # All logging-related code
│       ├── Infrastructure/
│       │   └── MyLogger.cs           # (from Loggers/)
│       ├── Observers/                 # Logging observers
│       │   ├── ApplicationLogger.cs   # (from Observer/Implementations/)
│       │   └── CombatLogger.cs
│       └── Interfaces/
│           └── ICreatureObserver.cs   # (from Observer/Interfaces/)
│
├── Configuration/                    # Configuration system
│   ├── ConfigReaders/
│   │   ├── ConfigReader.cs
│   │   └── GameConfig.cs
│   └── XMLConfigurations/
│       └── GameConfiguration.xml
│
└── Common/                           # Shared types and utilities
    └── Enums/
        ├── ArmorSlot.cs
        ├── ArmorType.cs
        ├── ClassType.cs
        └── WeaponType.cs
```

### Key Principles

1. **Domain Grouping**: Related functionality lives together (e.g., all equipment code in `Equipment/`)
2. **Clear Boundaries**: Each domain folder is self-contained
3. **Patterns as Implementation Details**: Design patterns are still used, but not the primary organization
4. **Intuitive Navigation**: Developers think "I need equipment code" → go to `Domain/Equipment/`
5. **Scalability**: Easy to add new domains (e.g., `Domain/Inventory/`, `Domain/Quests/`)

---

## Detailed Migration Map

### 1. Creatures Domain

**Current Locations:**
- `Template/Base/Creature.cs`
- `Template/Implementations/Warrior.cs`, `Mage.cs`, `Hunter.cs`
- `Factory/FactoryMethod/CreatureFactory.cs`
- `Core/Interfaces/IAttackable.cs`

**New Location:** `Domain/Creatures/`

**Files to Move:**
```
Template/Base/Creature.cs                    → Domain/Creatures/Base/Creature.cs
Template/Implementations/Warrior.cs          → Domain/Creatures/Classes/Warrior.cs
Template/Implementations/Mage.cs             → Domain/Creatures/Classes/Mage.cs
Template/Implementations/Hunter.cs            → Domain/Creatures/Classes/Hunter.cs
Factory/FactoryMethod/CreatureFactory.cs     → Domain/Creatures/Factories/CreatureFactory.cs
Core/Interfaces/IAttackable.cs                → Domain/Creatures/Interfaces/IAttackable.cs
```

**Namespace Changes:**
- `Mandatory2DGameFramework.Template.Base` → `Mandatory2DGameFramework.Domain.Creatures.Base`
- `Mandatory2DGameFramework.Template.Implementations` → `Mandatory2DGameFramework.Domain.Creatures.Classes`
- `Mandatory2DGameFramework.Factory.FactoryMethod` → `Mandatory2DGameFramework.Domain.Creatures.Factories`
- `Mandatory2DGameFramework.Core.Interfaces` (for IAttackable) → `Mandatory2DGameFramework.Domain.Creatures.Interfaces`

---

### 2. Combat Domain

**Current Locations:**
- `Strategy/Implementations/MeleeAttackStrategy.cs`
- `Strategy/Implementations/RangedAttackStrategy.cs`
- `Strategy/Interfaces/IAttackStrategy.cs`

**New Location:** `Domain/Combat/`

**Files to Move:**
```
Strategy/Implementations/MeleeAttackStrategy.cs   → Domain/Combat/Strategies/MeleeAttackStrategy.cs
Strategy/Implementations/RangedAttackStrategy.cs  → Domain/Combat/Strategies/RangedAttackStrategy.cs
Strategy/Interfaces/IAttackStrategy.cs            → Domain/Combat/Interfaces/IAttackStrategy.cs
```

**Namespace Changes:**
- `Mandatory2DGameFramework.Strategy.Implementations` → `Mandatory2DGameFramework.Domain.Combat.Strategies`
- `Mandatory2DGameFramework.Strategy.Interfaces` → `Mandatory2DGameFramework.Domain.Combat.Interfaces`

---

### 3. Equipment Domain

**Current Locations:**
- `Core/Items/AttackItem.cs`
- `Core/Items/DefenceItem.cs`
- `Core/Interfaces/IWeapon.cs`
- `Composite/Implementations/EquippedArmorSet.cs`
- `Composite/Interfaces/IGameItem.cs`
- `Decorator/Base/DefenceItemDecorator.cs`
- `Decorator/ConcreteDecorators/BoostDefenceDecorator.cs`
- `Decorator/ConcreteDecorators/WeakenDefenceDecorator.cs`
- `Decorator/Interfaces/IDefenceItem.cs`
- `Factory/AbstractFactory/ConcreteFactories/*ItemFactory.cs`
- `Factory/AbstractFactory/IItemFactory.cs`

**New Location:** `Domain/Equipment/`

**Files to Move:**
```
Core/Items/AttackItem.cs                        → Domain/Equipment/Weapons/AttackItem.cs
Core/Interfaces/IWeapon.cs                      → Domain/Equipment/Weapons/IWeapon.cs
Core/Items/DefenceItem.cs                       → Domain/Equipment/Armor/DefenceItem.cs
Composite/Implementations/EquippedArmorSet.cs   → Domain/Equipment/Armor/EquippedArmorSet.cs
Composite/Interfaces/IGameItem.cs                → Domain/Equipment/Armor/IGameItem.cs
Decorator/Interfaces/IDefenceItem.cs            → Domain/Equipment/Armor/IDefenceItem.cs
Decorator/Base/DefenceItemDecorator.cs          → Domain/Equipment/Decorators/Base/DefenceItemDecorator.cs
Decorator/ConcreteDecorators/BoostDefenceDecorator.cs → Domain/Equipment/Decorators/BoostDefenceDecorator.cs
Decorator/ConcreteDecorators/WeakenDefenceDecorator.cs → Domain/Equipment/Decorators/WeakenDefenceDecorator.cs
Factory/AbstractFactory/IItemFactory.cs          → Domain/Equipment/Factories/IItemFactory.cs
Factory/AbstractFactory/ConcreteFactories/WarriorItemFactory.cs → Domain/Equipment/Factories/WarriorItemFactory.cs
Factory/AbstractFactory/ConcreteFactories/MageItemFactory.cs    → Domain/Equipment/Factories/MageItemFactory.cs
Factory/AbstractFactory/ConcreteFactories/HunterItemFactory.cs  → Domain/Equipment/Factories/HunterItemFactory.cs
```

**Namespace Changes:**
- `Mandatory2DGameFramework.Core.Items` → `Mandatory2DGameFramework.Domain.Equipment.Weapons` (for AttackItem) or `Domain.Equipment.Armor` (for DefenceItem)
- `Mandatory2DGameFramework.Core.Interfaces` (for IWeapon) → `Mandatory2DGameFramework.Domain.Equipment.Weapons`
- `Mandatory2DGameFramework.Composite.Implementations` → `Mandatory2DGameFramework.Domain.Equipment.Armor`
- `Mandatory2DGameFramework.Composite.Interfaces` → `Mandatory2DGameFramework.Domain.Equipment.Armor`
- `Mandatory2DGameFramework.Decorator.*` → `Mandatory2DGameFramework.Domain.Equipment.Decorators.*`
- `Mandatory2DGameFramework.Factory.AbstractFactory.*` → `Mandatory2DGameFramework.Domain.Equipment.Factories.*`

---

### 4. World Domain

**Current Locations:**
- `Core/World/World.cs`
- `Core/World/WorldObject.cs`

**New Location:** `Domain/World/`

**Files to Move:**
```
Core/World/World.cs          → Domain/World/World.cs
Core/World/WorldObject.cs    → Domain/World/WorldObject.cs
```

**Namespace Changes:**
- `Mandatory2DGameFramework.Core.World` → `Mandatory2DGameFramework.Domain.World`

---

### 5. Logging Domain

**Current Locations:**
- `Loggers/MyLogger.cs`
- `Observer/Implementations/ApplicationLogger.cs`
- `Observer/Implementations/CombatLogger.cs`
- `Observer/Interfaces/ICreatureObserver.cs`

**New Location:** `Domain/Logging/`

**Files to Move:**
```
Loggers/MyLogger.cs                          → Domain/Logging/Infrastructure/MyLogger.cs
Observer/Implementations/ApplicationLogger.cs → Domain/Logging/Observers/ApplicationLogger.cs
Observer/Implementations/CombatLogger.cs      → Domain/Logging/Observers/CombatLogger.cs
Observer/Interfaces/ICreatureObserver.cs       → Domain/Logging/Interfaces/ICreatureObserver.cs
```

**Namespace Changes:**
- `Mandatory2DGameFramework.Loggers` → `Mandatory2DGameFramework.Domain.Logging.Infrastructure`
- `Mandatory2DGameFramework.Observer.Implementations` → `Mandatory2DGameFramework.Domain.Logging.Observers`
- `Mandatory2DGameFramework.Observer.Interfaces` → `Mandatory2DGameFramework.Domain.Logging.Interfaces`

---

### 6. Configuration

**Current Location:** `Configraitons/` (note: typo in folder name)

**New Location:** `Configuration/` (fixed spelling)

**Files to Move:**
```
Configraitons/ConfigReaders/ConfigReader.cs     → Configuration/ConfigReaders/ConfigReader.cs
Configraitons/ConfigReaders/GameConfig.cs      → Configuration/ConfigReaders/GameConfig.cs
Configraitons/XMLConfigurations/GameConfiguration.xml → Configuration/XMLConfigurations/GameConfiguration.xml
```

**Namespace Changes:**
- `Mandatory2DGameFramework.Configraitons.*` → `Mandatory2DGameFramework.Configuration.*`

**Note:** This also fixes the typo in the folder name.

---

### 7. Common Types

**Current Location:** `Enums/`

**New Location:** `Common/Enums/`

**Files to Move:**
```
Enums/ArmorSlot.cs    → Common/Enums/ArmorSlot.cs
Enums/ArmorType.cs    → Common/Enums/ArmorType.cs
Enums/ClassType.cs    → Common/Enums/ClassType.cs
Enums/WeaponType.cs   → Common/Enums/WeaponType.cs
```

**Namespace Changes:**
- `Mandatory2DGameFramework.Enums` → `Mandatory2DGameFramework.Common.Enums`

---

## Migration Steps

### Phase 1: Preparation
1. **Create new folder structure** (empty folders)
2. **Backup current codebase** (git commit current state)
3. **Create a branch** for reorganization: `git checkout -b refactor/domain-driven-structure`

### Phase 2: Move Files (One Domain at a Time)
For each domain:
1. **Create target folders**
2. **Move files** to new locations
3. **Update namespaces** in moved files
4. **Update all using statements** in the entire codebase
5. **Test compilation** after each domain migration
6. **Commit** after each successful domain migration

### Phase 3: Update References
1. **Search and replace** all namespace references in:
   - All `.cs` files in the framework
   - All `.cs` files in `GameFrameworkConsoleApp`
   - Any test projects
   - Documentation files (if they reference namespaces)

### Phase 4: Cleanup
1. **Delete old empty folders**
2. **Update `.csproj` file** if needed (usually not required for folder moves)
3. **Run full test suite**
4. **Update README.md** with new structure
5. **Update documentation** if it references folder structure

### Phase 5: Verification
1. **Build solution** - ensure no compilation errors
2. **Run application** - ensure runtime behavior is unchanged
3. **Code review** - verify all references updated
4. **Merge to main** when complete

---

## Benefits of New Structure

### 1. **Intuitive Navigation**
- **Before**: "Where is armor code?" → Check `Core/Items/`, `Composite/`, `Decorator/`
- **After**: "Where is armor code?" → Go to `Domain/Equipment/Armor/`

### 2. **Easier Feature Development**
- Adding a new weapon type: All code in `Domain/Equipment/Weapons/`
- Adding a new creature: All code in `Domain/Creatures/Classes/`
- Adding combat mechanics: All code in `Domain/Combat/`

### 3. **Better Code Discovery**
- New developers can explore by domain rather than needing to understand design patterns first
- Related code is co-located, making it easier to understand relationships

### 4. **Maintainability**
- Changes to equipment system are isolated to `Domain/Equipment/`
- Easier to identify what needs updating when requirements change

### 5. **Scalability**
- Easy to add new domains: `Domain/Inventory/`, `Domain/Quests/`, `Domain/Spells/`
- Clear boundaries prevent code from being scattered

### 6. **Patterns Still Visible**
- Design patterns are still used (Strategy, Factory, Observer, etc.)
- They're just organized by domain rather than being the primary organization
- Patterns become implementation details, not organizational structure

---

## Potential Challenges & Solutions

### Challenge 1: Circular Dependencies
**Risk**: Moving files might create circular dependencies between domains.

**Solution**: 
- Keep shared interfaces in `Common/` or appropriate domain
- Use dependency inversion (interfaces in one domain, implementations in another)
- Example: `IAttackable` in `Domain/Creatures/Interfaces/` is used by `Domain/Combat/`

### Challenge 2: Breaking Changes
**Risk**: External projects using this framework might break.

**Solution**:
- This is a **breaking change** for namespace references
- Consider versioning (e.g., v2.0.0) if publishing as NuGet package
- Provide migration guide for consumers
- Or maintain backward compatibility with type aliases (not recommended long-term)

### Challenge 3: Large Number of Files to Update
**Risk**: Many files need namespace updates.

**Solution**:
- Use IDE refactoring tools (Visual Studio "Rename" with "Update references")
- Use find-and-replace with regex for namespace updates
- Migrate one domain at a time to catch issues early

### Challenge 4: Git History
**Risk**: Moving files loses git history.

**Solution**:
- Use `git mv` instead of delete + create
- Or use `git log --follow` to track file history
- Consider `git filter-branch` or `git filter-repo` for better history preservation

---

## Example: Finding Code After Reorganization

### Scenario 1: "I need to add a new weapon type"
**Before (Pattern-Based):**
1. Check `Core/Items/` for weapon base class
2. Check `Factory/AbstractFactory/` for factory pattern
3. Check `Core/Interfaces/` for IWeapon interface
4. Check `Enums/` for WeaponType enum

**After (Domain-Based):**
1. Go to `Domain/Equipment/Weapons/` - everything is there!
2. Check `Common/Enums/` for WeaponType if needed

### Scenario 2: "I need to modify combat damage calculation"
**Before (Pattern-Based):**
1. Check `Strategy/Implementations/` for attack strategies
2. Check `Template/Base/Creature.cs` for damage application
3. Check `Core/Interfaces/` for IAttackable

**After (Domain-Based):**
1. Go to `Domain/Combat/Strategies/` for attack strategies
2. Go to `Domain/Creatures/Base/Creature.cs` for damage application
3. Go to `Domain/Creatures/Interfaces/` for IAttackable

### Scenario 3: "I need to add logging for equipment changes"
**Before (Pattern-Based):**
1. Check `Observer/Implementations/` for logger
2. Check `Core/Items/` for equipment classes
3. Check `Composite/` for armor sets

**After (Domain-Based):**
1. Go to `Domain/Logging/Observers/` for logger
2. Go to `Domain/Equipment/` for all equipment classes

---

## Recommended Tools for Migration

1. **Visual Studio / Rider**: Built-in refactoring tools for namespace changes
2. **PowerShell / Bash Scripts**: For bulk file moves (be careful!)
3. **Find & Replace**: For namespace updates across files
4. **Git**: For tracking changes and preserving history
5. **Unit Tests**: To verify behavior hasn't changed

---

## Post-Migration Checklist

- [ ] All files moved to new locations
- [ ] All namespaces updated
- [ ] All `using` statements updated
- [ ] Solution builds without errors
- [ ] All tests pass
- [ ] Application runs correctly
- [ ] Documentation updated
- [ ] README.md updated with new structure
- [ ] Old empty folders deleted
- [ ] Git history preserved (if possible)
- [ ] Code review completed
- [ ] Breaking changes documented (if publishing)

---

## Alternative: Hybrid Approach

If you want to keep some pattern-based organization while moving to domain-driven:

```
Mandatory2DGameFramework/
├── Domain/
│   ├── Creatures/
│   ├── Combat/
│   ├── Equipment/
│   ├── World/
│   └── Logging/
├── Patterns/                    # Keep pattern examples/docs
│   ├── Factory/
│   ├── Strategy/
│   └── Observer/
└── Common/
```

**Not Recommended**: This adds confusion. Better to fully commit to domain-driven structure.

---

## Conclusion

This reorganization will make the codebase more maintainable, intuitive, and scalable. While it requires significant effort upfront, the long-term benefits for development velocity and code clarity are substantial.

**Estimated Time**: 4-8 hours for complete migration (depending on tooling and codebase size)

**Risk Level**: Medium (breaking changes, but straightforward refactoring)

**Recommendation**: Proceed with migration, one domain at a time, with thorough testing after each step.

---

## Questions or Concerns?

If you encounter issues during migration:
1. Check namespace references are all updated
2. Verify `using` statements in all files
3. Ensure project references are correct
4. Test compilation frequently during migration
5. Consider using IDE refactoring tools instead of manual changes

