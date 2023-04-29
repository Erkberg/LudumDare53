using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class PlayerSubBasket : MonoBehaviour
    {
        public List<PlayerSubBasketPosition> positions;

        public PlayerSubBasketPosition GetNextFreePosition()
        {
            foreach(PlayerSubBasketPosition position in positions)
            {
                if(!position.interactable)
                {
                    return position;
                }
            }

            return null;
        }

        public void RemoveInteractable(Interactable interactable)
        {
            foreach(PlayerSubBasketPosition subBasketPosition in positions)
            {
                if(subBasketPosition.interactable == interactable)
                {
                    subBasketPosition.interactable = null;
                }
            }
        }
    }
}
