using GameFrameworkConsoleApp.Observers;
using Mandatory2DGameFramework.Domain.Enums;
using Mandatory2DGameFramework.Domain.Environment;
using Mandatory2DGameFramework.Domain.Logging.Observers;

namespace GameFrameworkConsoleApp.Services
{
    /// <summary>
    /// Service responsible for running the game simulation.
    /// Follows Single Responsibility Principle - only handles game simulation flow.
    /// Follows Dependency Inversion Principle - depends on abstractions (interfaces).
    /// </summary>
    public class GameSimulationRunner : IGameSimulationRunner
    {
        private readonly IConsoleDisplayService _displayService;
        private readonly IGameWorldSetupService _worldSetupService;

        public GameSimulationRunner(IConsoleDisplayService displayService, IGameWorldSetupService worldSetupService)
        {
            _displayService = displayService;
            _worldSetupService = worldSetupService;
        }

        public void Run()
        {
            _displayService.DisplayHeader("2D Game Framework Demo");

            // Setup world
            World world = _worldSetupService.CreateWorld();
            _displayService.DisplayMessage($"World '{world.Name}' created ({world.MaxX}x{world.MaxY})");
            _displayService.DisplaySeparator();

            // Create creatures using Factory Method pattern
            Creature warrior = _worldSetupService.CreateCreature(ClassType.Warrior, "Thorin");
            Creature mage = _worldSetupService.CreateCreature(ClassType.Mage, "Gandalf");
            Creature hunter = _worldSetupService.CreateCreature(ClassType.Hunter, "Legolas");

            // Equip armor using Abstract Factory pattern
            _worldSetupService.EquipCreatureWithArmor(warrior);
            _worldSetupService.EquipCreatureWithArmor(mage);
            _worldSetupService.EquipCreatureWithArmor(hunter);

            // Attach observers (Observer pattern)
            var consoleObserver = new ConsoleObserver();
            var combatLogger = CombatLogger.Instance; // Access singleton to initialize combat logging
            
            warrior.Attach(consoleObserver);
            warrior.Attach(combatLogger);
            
            mage.Attach(consoleObserver);
            mage.Attach(combatLogger);
            
            hunter.Attach(consoleObserver);
            hunter.Attach(combatLogger);

            // Add creatures to world
            world.AddCreature(warrior);
            world.AddCreature(mage);
            world.AddCreature(hunter);

            _displayService.DisplayHeader("Creatures");
            DisplayCreatures(warrior, mage, hunter);

            // Demonstrate combat (Strategy + Template Method patterns)
            _displayService.DisplayHeader("Combat");
            warrior.PerformAttack(mage, range: 1);
            _displayService.DisplayCreatureInfo(mage);
            _displayService.DisplaySeparator();

            mage.PerformAttack(hunter, range: 25);
            _displayService.DisplayCreatureInfo(hunter);
            _displayService.DisplaySeparator();

            hunter.PerformAttack(warrior, range: 15);
            _displayService.DisplayCreatureInfo(warrior);
            _displayService.DisplaySeparator();

            // Demonstrate property changes
            var weapon = warrior.EquippedWeapon;
            if (weapon is Mandatory2DGameFramework.Core.Items.AttackItem attackItem)
            {
                _displayService.DisplayMessage($"Upgrading {attackItem.Name}: {attackItem.Hit} -> 40 damage");
                attackItem.Hit = 40;
            }
            _displayService.DisplaySeparator();

            // Final state
            _displayService.DisplayHeader("Final State");
            DisplayCreatures(warrior, mage, hunter);

            _displayService.DisplayMessage("Demo completed! Check logs for details.");
        }

        // DRY: Helper method to avoid repetition
        private void DisplayCreatures(params Creature[] creatures)
        {
            foreach (var creature in creatures)
            {
                _displayService.DisplayCreatureInfo(creature);
            }
        }
    }
}

