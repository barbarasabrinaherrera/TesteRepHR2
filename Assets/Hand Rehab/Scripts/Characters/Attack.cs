using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public Element element;

    public Element Element
    {
        get => default;
        set
        {
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (damage == 0)
        {
            damage = 25;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player") || obj.CompareTag("Enemy"))
        {
            //Debug.Log("Passou pelo primeiro IF, próximo é verificar o personagem.");
            CountDamage(obj);
        }
        /*if (this.tag.Equals("Boulder"))
        {
            var audio = GetComponent<AudioSource>();
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }*/
        //Debug.Log("O objeto da colisão é: "+ obj.name);
        //Debug.Log("Objeto a ser deletado: "+ this.gameObject.name);
        Destroy(this.gameObject, 1);
    }

    private void CountDamage(GameObject obj)
    {
        Character character = obj.GetComponent<Character>();
        //Debug.Log("O personagem atingido foi: " + character.name != null);
        //Debug.Log("Elemento utilizado foi: " + element);
        character.Hit(damage, element);
    }

    private void OnTriggerStay(Collider collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player") || obj.CompareTag("Enemy"))
        {
            Character character = obj.GetComponent<Character>();
            character.Hit(25f, element);
        }
    }

    public void Shoot(Vector3 origin, Vector3 target)
    {
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        var direction = target - origin;
        this.gameObject.transform.position = origin + direction.normalized * 2;
        rb.AddForce(direction * 150);
        GameObject.Destroy(this.gameObject, 5);
    }
}
