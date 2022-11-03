using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool action/Plow")]
public class PlowTile : ToolAction
{
    [SerializeField] List<TileBase> canplow;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);

        if (canplow.Contains(tileToPlow) == false)
        {
            return false;
        }

        tileMapReadController.cropsManager.Plow(gridPosition);



        return true;
    }
}
