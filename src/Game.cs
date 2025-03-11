using System;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		Room attic = new Room("in the attic");
		Room basement = new Room("in the basement");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);
		outside.AddExit("down", basement); // Add exit to basement

		theatre.AddExit("west", outside);

		pub.AddExit("east", outside);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);
		lab.AddExit("up", attic); // Add exit to attic

		office.AddExit("west", lab);

		attic.AddExit("down", lab); // Add exit back to lab

		basement.AddExit("up", outside); // Add exit to outside

		Item medkit = new Item(5, "You are heald");
		Item Position = new Item(3, "You gained 10 Health");

		// Start game outside
		player.CurrentRoom = outside;
		outside.Chest.Put("medkit", medkit);
	}
	// khgk
	// Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if (command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				PrintLook();
				break;
			case "status":
				PrintStatus();
				break;
			case "take":
				Take(command);
				break;

		}

		return wantToQuit;
	}

	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	private void Take(Command command)
	{
		if (!command.HasSecondWord())
		 {
		 	Console.WriteLine("Take what?");
		 	return;
		 }
		 string itemName = command.SecondWord;
		 Item item = player.CurrentRoom.Chest.Get(itemName);

		 if (item == null)
		 {
		 	Console.WriteLine("There is no" + itemName + "here.");
		 	return;
		 }

		 else if (player.Backpack.Put(itemName, item))
		 {
			Console.WriteLine("You have picked up a " + itemName);
		 }
		 else
		{
		 	Console.WriteLine("You cannot pick up a " + itemName + " because it is too heavy.");
		 }
	
	}

	private void PrintLook()
	{
		Console.WriteLine("The room contains:" + player.CurrentRoom.Chest.ShowInventory());

	}

	private void PrintStatus()
	{
		Console.WriteLine("Your current Health is: " + player.GetHealth());
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if (!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to " + direction + "!");
			return;
		}

		// Damage the player by 10 Health points
		player.Damage(10);
		if (!player.IsAlive())
		{
			Console.WriteLine("You have died.");
			return;
		}

		player.CurrentRoom = nextRoom;
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}
}