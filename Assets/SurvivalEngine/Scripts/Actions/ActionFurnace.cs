﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalEngine
{
    /// <summary>
    /// Melt an item in the furnace
    /// </summary>

    [CreateAssetMenu(fileName = "Action", menuName = "SurvivalEngine/Actions/Furnace", order = 50)]
    public class ActionFurnace : MAction
    {
        public ItemData melt_item;
        public int item_quantity_in = 1;
        public int item_quantity_out = 1;
        public float duration = 1f; //In game hours

        //Merge action
        public override void DoAction(PlayerCharacter character, InventorySlot slot, Selectable select)
        {
            InventoryData inventory = slot.inventory;

            Furnace furnace = select.GetComponent<Furnace>();
            if (furnace != null && furnace.CountItemSpace() >= item_quantity_out)
            {
                furnace.PutItem(slot.GetItem(), melt_item, duration, item_quantity_out);
                inventory.RemoveItemAt(slot.slot, item_quantity_in);
            }
        }

        public override bool CanDoAction(PlayerCharacter character, InventorySlot slot, Selectable select)
        {
            Furnace furnace = select.GetComponent<Furnace>();
            InventoryData inventory = slot.inventory;
            InventoryItemData iidata = inventory?.GetInventoryItem(slot.slot);
            return furnace != null && iidata != null && furnace.CountItemSpace() >= item_quantity_out && iidata.quantity >= item_quantity_in;
        }
    }

}
