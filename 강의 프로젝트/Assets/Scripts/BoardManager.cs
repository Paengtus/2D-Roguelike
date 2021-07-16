using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;
public class BoardManager : MonoBehaviour
{
    [Serializable]
    //�ּ� �ִ밪�� ���� Ŭ����
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }
   
    //������ ������
    public int colums = 8;
    public int rows = 8;
    //���� ���ο� �����ϴ� ���İ� ���� ����
    public Count foodCount = new Count(1, 5);
    public Count wallCount = new Count(5, 9);

    //���� ���ο� �����ϴ� ������Ʈ ��������� ��� �ִ� ����
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] outerwallTiles;
    public GameObject[] enemyTiles;

    //�ٴ� Ÿ���� ��ġ���� �����ϴ� ����Ʈ
    List<Vector3> gridPositions = new List<Vector3>();
    //�ٴ� Ÿ���� ������ �ִ� ������Ʈ
    Transform boardHolder;

    //��� ��ġ���� ��� �ִ� �׸����� �ϳ��� ���� ���������� �����Ѵ�.
    Vector3 RandomPosition()
    {
        //�ε���
        int randomIndex = Random.Range(0, gridPositions.Count);
        //��ġ
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }
    //������ ��ġ�� ��� ���� ��ġ���� ��� ��� �ִ� �׸��� �ʱ�ȭ�Ѵ�.
    void InitiallizeList()
    {
        gridPositions.Clear();

        for(int i = 1; i < colums-1; i++)
        {
            for(int j = 1; j < rows-1; j++)
            {
                gridPositions.Add(new Vector3(i, j, 0));
            }
        }
    }
    //�⺻ �ٴ� Ÿ���� ��� �Լ�
    void BoardSetup()
    {
        //�ٴ��� ��� �ִ� Board��� ���� ������Ʈ�� �����
        boardHolder = new GameObject("Board").transform;
        for (int i = -1; i < colums + 1; i++)
        {
            for (int j = -1; j < rows + 1; j++)
            {
                //floorTiles�� �� ������ Ÿ���� �����Ѵ�.
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                //��ġ���� �׵θ� �̸� outerWallTiles���� ������ Ÿ���� �����Ѵ�.
                if (i == -1 || i == colums || j == -1 || j == rows)
                {
                    toInstantiate = outerwallTiles[Random.Range(0, outerwallTiles.Length)];
                }
                //������ Ÿ���� ���ӻ� ��ȯ�Ѵ�.
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                //��ȯ�� Ÿ�ϵ� Board��� ���� ������Ʈ �ȿ� �ִ´�.
                instance.transform.SetParent(boardHolder);
            }
        }
    }
    //������Ʈ�� ������ ��ġ�� Instantiate�ϴ� �Լ�
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
       int objectCount = Random.Range(minimum, maximum + 1);

       for(int i = 0; i < objectCount; i++)
       {
            Vector3 randomPosition = RandomPosition();

            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
       }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitiallizeList();
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);

        int enemyCount = (int)Mathf.Log(level, 2f);//level�� 3�϶� 1 �̻��� �ȴ�.
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

        Instantiate(exit, new Vector3(colums - 1, rows - 1, 0), Quaternion.identity);

    }
}
