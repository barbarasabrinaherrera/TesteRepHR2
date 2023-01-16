using System.Collections;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    private GameObject _player;
    private Terrain _terrain;

    public Enemy Enemy
    {
        get => default;
        set
        {
        }
    }

    // Start is called before the first frame update

    void Start()
    {
        _player = GameObject.Find("Player");
        if (_player == null)
            Debug.LogError("Player is null");

        _terrain = GameObject.Find("Terrain").GetComponent<Terrain>();
        if (_terrain == null)
            Debug.LogError("Terrain is null");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemies(Element element, float spawnTime)
    {
        StartCoroutine(SpawnEnemiesRoutine(element, spawnTime));
    }

    private IEnumerator SpawnEnemiesRoutine(Element element, float spawnTime)
    {
        yield return new WaitForSeconds(spawnTime);

        GameObject copy = GameObject.Instantiate(_enemyPrefab);
        var enemyInstance = copy.GetComponent<Enemy>();
        enemyInstance.type = new CharType(element);
        copy.GetComponent<Renderer>().material.color = enemyInstance.type.color;
        copy.transform.position = _player.transform.position + GenerateRandomDistance();
        Vector3 enemyPosition = copy.transform.position;
        enemyPosition.y = _terrain.SampleHeight(enemyPosition) + _terrain.transform.position.y;
        copy.transform.position = enemyPosition;
    }

    private Vector3 GenerateRandomDistance()
    {
        /*Vector3 vetor = Random.onUnitSphere * 7;
        
        if(Mathf.Abs(vetor.x) < 4 && Mathf.Abs(vetor.z) < 4) 
        {
            vetor.x += vetor.x < 0 ? -4:4;
        }*/
        float x = Random.Range(4, 7);
        float z = Random.Range(-7, 7);

        return new Vector3(x, 0, z);//valores escolhidos para ficarem frente a posicao inicial do jogador
    }
}