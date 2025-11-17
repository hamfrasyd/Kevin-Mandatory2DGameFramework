using Mandatory2DGameFramework.Core.World;
using Mandatory2DGameFramework.Enums;
using Mandatory2DGameFramework.Factory.AbstractFactory.ConcreteFactories;
using Mandatory2DGameFramework.Factory.FactoryMethod;
using Mandatory2DGameFramework.Template.Base;
using Mandatory2DGameFramework.Factory.AbstractFactory;
using Mandatory2DGameFramework.Composite.Interfaces;

namespace GameFrameworkConsoleApp.Services
{
    /// <summary>
    /// Service responsible for setting up the game world and creatures.
    /// Follows Single Responsibility Principle - only handles game world setup.
    /// Uses Factory Method pattern from framework to create creatures.
    /// </summary>
    public class GameWorldSetupService : IGameWorldSetupService
    {
        public World CreateWorld()
        {
            return new World(maxX: 100, maxY: 100, name: "Fantasy Realm");
        }

        public Creature CreateCreature(ClassType classType, string name)
        {
            return CreatureFactory.CreateCreature(classType, name);
        }

        public void EquipCreatureWithArmor(Creature creature)
        {
            var factory = GetItemFactoryForCreature(creature);
            if (factory is null) return;

            // DRY: Use array to avoid repetition
            var armorPieces = new[]
            {
                factory.CreateHelmet("Helmet", 10),
                factory.CreateChestArmor("Chest Armor", 20),
                factory.CreateLegArmor("Leg Armor", 30)
            };

            foreach (var armor in armorPieces)
            {
                if (armor is IGameItem gameItem)
                {
                    creature.EquippedArmor.Add(gameItem);
                }
            }
        }

        private IItemFactory? GetItemFactoryForCreature(Creature creature)
        {
            return creature switch
            {
                Mandatory2DGameFramework.Template.Implementations.Warrior => new WarriorItemFactory(),
                Mandatory2DGameFramework.Template.Implementations.Mage => new MageItemFactory(),
                Mandatory2DGameFramework.Template.Implementations.Hunter => new HunterItemFactory(),
                _ => null
            };
        }
    }
}

