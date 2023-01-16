using UnityEngine;
using UnityEngine.UI;

//Enemy and Player
public class Character : MonoBehaviour
{
    public float maxHp;
    public float hp;
    public Slider hpBar;
    public Image fill;
    public CharType type;

    public GameController _gameController;

    public DifficultyManager DifficultyManager
    {
        get => default;
        set
        {
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Fallback hp
        if (maxHp == 0)
        {
            maxHp = 100;
        }
        hp = maxHp;
        if (hpBar != null)
        {
            hpBar.maxValue = maxHp;
            hpBar.value = hp;
            fill.color = Color.green;
        }
        if (type == null)
        {
            type = new CharType(Element.NORMAL);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    public void Hit(float damage, Element element)
    {
        float damageMultiplier = 1;
        switch (DifficultyManager.gameDifficulty)
        {
            case DifficultyManager.Difficulty.Easy: //dano 2
                damageMultiplier *= 2;
                break;
            case DifficultyManager.Difficulty.Medium: //dano 1
                break;
            case DifficultyManager.Difficulty.Hard: //dano 0.5
                damageMultiplier *= 0.5f;
                break;
        }
        if(this.tag == "Player")
        { 
            bool isSheldActive = GameObject.Find("AbilityController").GetComponent<AbilityManager>()?.isShieldActive??false; 
            if (isSheldActive)
            {
                return;
            }
        }
        if (this.tag == "Player")
            damageMultiplier = 1 / damageMultiplier;

        if (element == type.getWeakness())
        {
            damageMultiplier *= 2;
        }
        else if (element == type.element)
        {
            damageMultiplier *= 0.5f;
        }

        hp -= damage * damageMultiplier;
        if (hp < 0)
        {
            hp = 0;
        }
        if (hp <= 0)
        {
            if (this.tag == "Enemy")
            {
                this.gameObject.SetActive(false);
                CancelInvoke();
                Enemy.numberOfEnemies--;
                GameObject.Destroy(this);
            }
            else if (tag == "Player")
            {
                _gameController.GameOver(); //Msg Game Over (TODO)
            }
        }
        if (this.hpBar != null)
        {
            hpBar.value = hp;
            float hpRatio = hp / maxHp;
            if (hpRatio > 0.5)
            {
                fill.color = Color.green;
            }
            else if (hpRatio > 0.25)
            {
                fill.color = Color.yellow;
            }
            else
            {
                fill.color = Color.red;
            }
        }

    }
}
