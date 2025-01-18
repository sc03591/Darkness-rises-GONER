using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NodeMannager : MonoBehaviour
{
    public int gridWidth = 10;            // Antal kolonner i begge retninger
    public int gridHeight = 10;           // Antal rækker i begge retninger
    public float nodeSpacing = 1.5f;      // Afstand mellem noder

    public GameObject nodePrefab;         // Prefab for noderne
    public List<ResourceData> resourceDataList; // Liste over resurser med deres vægte
    public int resourceSpawnCount = 10;   // Antal resurser, der skal spawnes

    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>(); // Brug Dictionary til negative koordinater

    void Start()
    {
        GenerateGrid();
        SpawnRandomResources();
    }

    void GenerateGrid()
    {
        for (int x = -gridWidth; x <= gridWidth; x++)
        {
            for (int y = -gridHeight; y <= gridHeight; y++)
            {
                // Beregn placeringen i 2D
                Vector2 position = new Vector2(x * nodeSpacing, y * nodeSpacing);

                // Spawn en node i 2D
                GameObject nodeObject = Instantiate(nodePrefab, position, Quaternion.identity, transform);
                nodeObject.name = $"Node_{x}_{y}";

                // Tilføj Node-komponent til objektet
                Node node = nodeObject.AddComponent<Node>();
                node.gridPosition = new Vector2Int(x, y);

                // Gem noden i grid
                grid[new Vector2Int(x, y)] = node;
            }
        }
    }

    void SpawnRandomResources()
    {
        int spawnedResources = 0;

        while (spawnedResources < resourceSpawnCount)
        {
            // Vælg en tilfældig position inden for grid'ets område
            int randomX = Random.Range(-gridWidth, gridWidth + 1);
            int randomY = Random.Range(-gridHeight, gridHeight + 1);

            Vector2Int randomPosition = new Vector2Int(randomX, randomY);

            if (grid.ContainsKey(randomPosition))
            {
                Node randomNode = grid[randomPosition];

                // Tjek om noden allerede har en ressource
                if (randomNode != null && !randomNode.HasResource())
                {
                    // Vælg en ressource baseret på vægte
                    GameObject resourcePrefab = GetRandomResourceByWeight();
                    randomNode.PlaceResource(resourcePrefab);
                    spawnedResources++;
                }
            }
        }
    }

    GameObject GetRandomResourceByWeight()
    {
        // Beregn den samlede vægt af alle resurser
        int totalWeight = 0;
        foreach (var resourceData in resourceDataList)
        {
            totalWeight += resourceData.weight;
        }

        // Vælg en tilfældig værdi inden for vægtintervallet
        int randomWeight = Random.Range(0, totalWeight);

        // Find hvilken ressource, der matcher det tilfældige vægtinterval
        int currentWeight = 0;
        foreach (var resourceData in resourceDataList)
        {
            currentWeight += resourceData.weight;
            if (randomWeight < currentWeight)
            {
                return resourceData.resourcePrefab;
            }
        }

        return null; // Hvis noget går galt, returner null (burde aldrig ske)
    }
}

[System.Serializable]
public class ResourceData
{
    public GameObject resourcePrefab; // Prefab for ressource
    public int weight;               // Vægt for ressource-sjældenhed (højere værdi = mere almindelig)
}