using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;
public class BoardManager : MonoBehaviour
{
    [Serializable]
    //최소 최대값이 들어가는 클래스
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
   
    //게임판 사이즈
    public int colums = 8;
    public int rows = 8;
    //게임 내부에 존재하는 음식과 벽의 갯수
    public Count foodCount = new Count(1, 5);
    public Count wallCount = new Count(5, 9);

    //게임 내부에 존재하는 오브젝트 프리펩들을 담고 있는 변수
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] outerwallTiles;
    public GameObject[] enemyTiles;

    //바닥 타일의 위치값을 저장하는 리스트
    List<Vector3> gridPositions = new List<Vector3>();
    //바닥 타일을 가지고 있는 오브젝트
    Transform boardHolder;

    //모든 위치값을 담고 있는 그릇에서 하나씩 빼서 랜덤값으로 리턴한다.
    Vector3 RandomPosition()
    {
        //인덱스
        int randomIndex = Random.Range(0, gridPositions.Count);
        //위치
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }
    //랜덤한 위치를 얻기 위해 위치값을 모두 담고 있는 그릇을 초기화한다.
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
    //기본 바닥 타일을 까는 함수
    void BoardSetup()
    {
        //바닥을 담고 있는 Board라는 게임 오브젝트를 만들고
        boardHolder = new GameObject("Board").transform;
        for (int i = -1; i < colums + 1; i++)
        {
            for (int j = -1; j < rows + 1; j++)
            {
                //floorTiles들 중 랜덤한 타일을 선택한다.
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                //위치값이 테두리 이면 outerWallTiles들중 랜덤한 타일을 선택한다.
                if (i == -1 || i == colums || j == -1 || j == rows)
                {
                    toInstantiate = outerwallTiles[Random.Range(0, outerwallTiles.Length)];
                }
                //선택한 타일을 게임상에 소환한다.
                GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                //소환한 타일들 Board라는 게임 오브젝트 안에 넣는다.
                instance.transform.SetParent(boardHolder);
            }
        }
    }
    //오브젝트를 랜덤한 위치에 Instantiate하는 함수
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

        int enemyCount = (int)Mathf.Log(level, 2f);//level이 3일때 1 이상이 된다.
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

        Instantiate(exit, new Vector3(colums - 1, rows - 1, 0), Quaternion.identity);

    }
}
