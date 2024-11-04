// File: IceWallManager.cs
using UnityEngine;
using System.Collections;

public class IceWallManager : MonoBehaviour
{
    public static IceWallManager Instance;
    public GameObject wallPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlaceWall(Vector3 position, Quaternion rotation)
    {
        GameObject wall = Instantiate(wallPrefab, position, rotation);
        StartCoroutine(DestroyWallAfterDelay(wall, 5f));
    }

    private IEnumerator DestroyWallAfterDelay(GameObject wall, float delay)
    {
        yield return new WaitForSeconds(delay - 1f);

        Vector3 initialPosition = wall.transform.position;
        Vector3 loweredPosition = initialPosition - new Vector3(0, 2f, 0);

        float elapsedTime = 0f;
        float lowerDuration = 1f;
        while (elapsedTime < lowerDuration)
        {
            wall.transform.position = Vector3.Lerp(initialPosition, loweredPosition, elapsedTime / lowerDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(wall);
    }
}
