using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<int> inventory { get; private set; }
    public int EquippedOutfit { get; private set; }
    public int EquippedSword { get; private set; }

    void Awake()
    {
        inventory = new List<int>();
        Load_Inventory();
        Load_EquippedItems();
    }

    void Load_Inventory()
    {
        if (PlayerPrefs.HasKey("inventoryCount"))
        {
            for (int i = 0; i < PlayerPrefs.GetInt("inventoryCount"); i++)
            {
                AddItem(PlayerPrefs.GetInt("inventory" + i.ToString()));
            }
        }
        else
        {
            PlayerPrefs.SetInt("inventory0", (int)Items.ItemType.DefaultSkin);
            PlayerPrefs.SetInt("inventory1", (int)Items.ItemType.DefaultSword);
            AddItem((int)Items.ItemType.DefaultSkin);
            AddItem((int)Items.ItemType.DefaultSword);
        }
    }

    void Load_EquippedItems()
    {
        if (PlayerPrefs.HasKey("equippedOutfit"))
            EquippedOutfit = PlayerPrefs.GetInt("equippedOutfit");
        else
        {
            PlayerPrefs.SetInt("equippedOutfit", (int)Items.ItemType.DefaultSkin);
            EquippedOutfit = (int)Items.ItemType.DefaultSkin;
        }

        if (PlayerPrefs.HasKey("equippedSword"))
            EquippedSword = PlayerPrefs.GetInt("equippedSword");
        else
        {
            PlayerPrefs.SetInt("equippedSword", (int)Items.ItemType.DefaultSword);
            EquippedSword = (int)Items.ItemType.DefaultSword;
        } 
    }

    public void AddItem(int item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
            PlayerPrefs.SetInt("inventoryCount", inventory.Count);
        }
    }

}
