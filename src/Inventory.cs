class Inventory
{
// fields
private int maxWeight;
private Dictionary<string, Item> items;
// constructor
public Inventory(int maxWeight)
{
this.maxWeight = maxWeight;
this.items = new Dictionary<string, Item>();
}


// methods

public int TotalWeight()
{
int total = 0;
// TODO implement:
// loop through the items, and add all the weights
foreach (Item item in items.Values)
{
    total += item.Weight;
}
return total;
}
public int FreeWeight() 
{
// TODO implement:
// compare MaxWeight and TotalWeight()
return maxWeight - TotalWeight();
}





public bool Put(string itemName, Item item)
{
// TODO implement:
// Check the Weight of the Item and check
// for enough space in the Inventory
// Does the Item fit?
// !Put Item in the items Dictionary
// !Return true/false for success/failure

if (item.Weight <= FreeWeight())
{
    items[itemName] = item;
    return true;
}
return false;
}

public Item Get(string itemName)
{
// TODO implement:
// Find Item in items Dictionary
// remove Item from items Dictionary if found
// return Item or null
if (items.ContainsKey(itemName))
{
    Item item = items[itemName];
    items.Remove(itemName);
    return item;
}
    return null;
}
public string ShowInventory()
{
    // TODO implement:
    if (items.Count == 0)
    {
        return "Nothing in this room.";
    }
    return string.Join(", ", items.Keys);
}
}