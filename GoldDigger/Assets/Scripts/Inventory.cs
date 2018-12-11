using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory<Space> : MonoBehaviour
{
    public Space Maxspace;

    public Inventory(Space maxspace)
    {
        Maxspace = maxspace;
    }
    class KeyValueListe<ItemName, AmountOf>
    {
        class Item
        {
            public ItemName ItemName;
            public AmountOf AmountOf;
            public Item next;
            public Item(ItemName itemName, AmountOf amountOf)
            {
                itemName = ItemName;
                amountOf = AmountOf;
            }
        }
        Item start, end;

        public void Add()
        { }

        public void Remove()
        { }


    }
}
