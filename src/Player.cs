class Player
{
    private int health;
    public Room CurrentRoom { get; set; }

    public Player()
    {
        CurrentRoom = null;
        health = 100;
    }

    // Method to decrease the player's health
    public int Damage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
        return health;
    }

    // Method to increase the player's health
    public int Heal(int amount)
    {
        health += amount;
        if (health > 100)
        {
            health = 100;
        }
        return health;
    }

    // Method to check if the player is alive
    public bool IsAlive()
    {
        return health > 0;
    }

    // Method to get the player's current health
    public int GetHealth()
    {
        return health;
    }
}