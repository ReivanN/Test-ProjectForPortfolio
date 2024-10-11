using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject[] blockPrefabs; // Массив префабов блоков
    public float[] spawnChances; // Шансы появления каждого префаба, например [0.7, 0.2, 0.1]
    public int columns = 4; // Количество колонок
    public int rows = 5; // Количество строк
    public float columnSpacing = 2f; // Расстояние между колонками
    public float rowSpacing = 1.5f; // Расстояние между строками
    public Vector3 startPosition = Vector3.zero; // Начальная позиция для всей сетки

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // Рассчитываем позицию каждого блока: x для колонок, y для строк
                Vector3 position = new Vector3(x * columnSpacing, -y * rowSpacing, 0) + startPosition;
                
                // Выбираем префаб на основе шансов появления
                GameObject blockPrefab = GetRandomPrefab();
                
                // Создаем блок на рассчитанной позиции
                Instantiate(blockPrefab, position, Quaternion.identity);
            }
        }
    }

    GameObject GetRandomPrefab()
    {
        float randomValue = Random.Range(0f, 1f); // Генерируем случайное число от 0 до 1
        float cumulative = 0f;

        // Проходим по каждому префабу и проверяем его вероятность
        for (int i = 0; i < blockPrefabs.Length; i++)
        {
            cumulative += spawnChances[i]; // Суммируем шансы
            if (randomValue < cumulative)
            {
                return blockPrefabs[i]; // Возвращаем выбранный префаб
            }
        }

        // Если что-то пошло не так (например, суммы вероятностей не равны 1), возвращаем первый префаб по умолчанию
        return blockPrefabs[0];
    }
}