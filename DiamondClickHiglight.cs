using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiamondClickHiglight : MonoBehaviour
{

    public List<Renderer> tilesToBeRed;

    void Update()
    {
        HighlightDiamonds();
    }

    void HighlightDiamonds()
    {
        foreach (Tile tile in PathfindingManager.Instance.tilePath)
        {            
            Renderer[] diamondRenderer = tile.GetComponentsInChildren<Renderer> ();
            foreach (Renderer diamond in diamondRenderer) {
                if (diamond.gameObject.tag == "Diamond" && !tilesToBeRed.Contains(diamond) && diamond.gameObject.GetComponentInParent<Tile>() != PlayerPosition.playerTile)
                {
                    tilesToBeRed.Add(diamond);
                    diamond.material.SetColor("_EmissionColor", new Color(0.43f, 0.66f, 0.7f, 0));
                }
            }
        }

        foreach (Renderer rend in tilesToBeRed)
        {
            if (!PathfindingManager.Instance.tilePath.Contains(rend.gameObject.GetComponentInParent<Tile>()))
            {
                rend.material.SetColor("_EmissionColor", Color.black);
                tilesToBeRed.Remove(rend);
            }
        }
    }
}
