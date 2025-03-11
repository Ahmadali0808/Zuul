class Player
{
    //fields
    public int Health { get; private set; }
    public Inventory Backpack { get; private set; }
    public Room CurrentRoom { get; set; }

    public Player()
    {
        CurrentRoom = null;
        Health = 100;
        Backpack = new Inventory(25);
    }

    // Method to decrease the player's Health
    public int Damage(int amount)
    {
        Health -= amount;
        if (Health < 0)
        {
            Health = 0;
        }
        return Health;
    }

    // Method to increase the player's Health
    public int Heal(int amount)
    {
        Health += amount;
        if (Health > 100)
        {
            Health = 100;
        }
        return Health;
    }

    // Method to check if the player is alive
    public bool IsAlive()
    {
        return Health > 0;
    }

    // Method to get the player's current Health
    public int GetHealth()
    {
        return Health;
    }
    public bool TakeFromChest(string itemName)
    {
        // Remove the Item from the Room
        Item item = CurrentRoom.Chest.Get(itemName);
        if (item == null)
        {
            Console.WriteLine("Item is not in Room");
            return false;
        }

        // Put it in your backpack.
        if (Backpack.Put(itemName, item))
        {
            Console.WriteLine($"you have picked a {itemName}.");
            return true;
        }
        // If the item doesn't fit your backpack, put it back in the chest.

        else
        {
            CurrentRoom.Chest.Put(itemName, item);
            Console.WriteLine("The item doesnt fit.");
            return false;
        }
    }






    public bool DropToChest(string itemName)
    {
        // TODO implement:
        // Remove Item from your inventory.
        // Add the Item to the Room
        // Inspect returned values
        // Communicate to the user what's happening
        // Return true/false for success/failure


        // Remove Item from your inventory.
        //Item item = backpack.Get(itemName);
        //if (item == null)
        //{
        ///Console.WriteLine("You dont have that item.");
        return false;
    }
}   

