using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    public int growTimer;
    public int growStage;
    public CropGrowing crop;
    public SpriteRenderer renderer;

    private int age;
    public CropTile(int age)
    {
        this.age = age;
    }

    public int getAge()
    {
        return this.age;
    }
}

public class CropsManager : TimeAgent
{
    
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] GameObject cropsSpriteprefab;

    Dictionary<Vector2Int, CropTile> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropTile>();
        onTimeTick += Tick;
        Init();
    }

    public void Tick()
    {
        foreach(CropTile cropTile in crops.Values)
        {
            if(cropTile.crop == null) { continue; }
            if (cropTile.growStage >= cropTile.crop.growthStageTime.Count) { continue; }//coutinue คือ ขึ้นลูปใหม่(ก้อง)

            cropTile.growTimer += 1;

            if (cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])//[Pointer]
            {
                cropTile.renderer.gameObject.SetActive(true);
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];

                cropTile.growStage += 1;
            }

            if(cropTile.growTimer >= cropTile.crop.timeToGrow)
            {
                Debug.Log("Finally Plant Growing");
                cropTile.crop = null;
            }
        }
    }

    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }

    public void Plow(Vector3Int position)
    {
        if (crops.ContainsKey((Vector2Int)position))
        {
            return;
        }

        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, CropGrowing toSeed)
    {
        targetTilemap.SetTile(position, seeded);

        crops[(Vector2Int)position].crop = toSeed;
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        CropTile crop = new CropTile(0);
        crops.Add((Vector2Int)position, crop);

        GameObject go = Instantiate(cropsSpriteprefab);
        go.transform.position = targetTilemap.CellToWorld(position);
        //go.transform.position = Vector3.forward * 0.01f;
        go.SetActive(false);
        crop.renderer = go.GetComponent<SpriteRenderer>();

        foreach (KeyValuePair<Vector2Int, CropTile> icrop in crops)
        {
            Debug.Log("Key" + icrop.Key + " Value " + icrop.Value.getAge());
        }

        targetTilemap.SetTile(position, plowed);
    }
}
