﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalEngine
{
    /// <summary>
    /// Cut a plant and return it to growth stage 0, and gain items (cut grass)
    /// </summary>

    [CreateAssetMenu(fileName = "Action", menuName = "SurvivalEngine/Actions/CutPlant", order = 50)]
    public class ActionCutPlant : AAction
    {
        public override void DoAction(PlayerCharacter character, Selectable select)
        {
            Plant plant = select.GetComponent<Plant>();
            if (plant != null)
            {
                string animation = character.Animation ? character.Animation.take_anim : "";
                character.TriggerAnim(animation, plant.transform.position);
                character.TriggerBusy(0.5f, () =>
                {
                    if (plant != null)
                    {
                        Destructible destruct = plant.Destructible;
                        destruct.SpawnLoots();
                        plant.GrowPlant(0);
                        TheAudio.Get().PlaySFX3D("destruct", destruct.death_sound, plant.transform.position);
                    }
                });
            }
        }

        public override bool CanDoAction(PlayerCharacter character, Selectable select)
        {
            return select.GetComponent<Plant>();
        }
    }

}