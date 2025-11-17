using Mandatory2DGameFramework.Core.World;
using Mandatory2DGameFramework.Enums;
using Mandatory2DGameFramework.Template.Base;

namespace GameFrameworkConsoleApp.Services
{
    /// <summary>
    /// Interface for setting up the game world and creatures.
    /// Follows Dependency Inversion Principle - depends on abstractions.
    /// </summary>
    public interface IGameWorldSetupService
    {
        World CreateWorld();
        Creature CreateCreature(ClassType classType, string name);
        void EquipCreatureWithArmor(Creature creature);
    }
}

