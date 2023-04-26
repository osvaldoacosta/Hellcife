using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour, IDamageable
{
    public bool isOnGameOver= false;
    [SerializeField] bool isPlayer;
    [SerializeField] string bloodType;
    [SerializeField] ObjectPool bloodParticlePool;
    private EphemeralParticle ephemeralParticle;
    [SerializeField] private bool bleeds;
    [SerializeField] private float current_health;
    [SerializeField] private float max_health;

    void Start()
    {
        GameEventManager.instance.onGameOver+= onGameOver;
        isPlayer= (gameObject.tag == "Player");
        switch (bloodType)
        {
            case "red":
                bloodParticlePool = GameObject.FindWithTag("BloodParticlePool").GetComponent<ObjectPool>();
                break;
            case "goo":
                bloodParticlePool = GameObject.FindWithTag("GooParticlePool").GetComponent<ObjectPool>();
                break;
            default:
                bloodParticlePool = GameObject.FindWithTag("BloodParticlePool").GetComponent<ObjectPool>();
                break;
        }
        bleeds= true;
        max_health = 100;
    }
    public float GetHealth()
    {
        return current_health;
    }
    public float GetMaxHealth()
    {
        return max_health;
    }
    public void SetMaxHealth(float health)
    {
        this.max_health = health;
    }
    public void OnEnable(){
        current_health= max_health;
    }
    //dano sem particula
    public void TakeDamage(float damage)
    {
       GameObject ephemeralParticleObject = bloodParticlePool.GetPooledObject();
        if(ephemeralParticleObject != null){
            ephemeralParticle = ephemeralParticleObject.GetComponent<EphemeralParticle>();
            ephemeralParticle.transform.position= transform.position;
            ephemeralParticle.Emit(5);
        }
        current_health -= damage;
        IsDead();
        Debug.Log("target hp: "+current_health);
    }
    
    //Usado para qd tiver curas por exemplo
    public void Heal(float heal)
    {
        if(heal+current_health <= max_health)
        {
            current_health += heal;
        }
        else
        {
            current_health = max_health;
        }
    }
    public bool IsDead()
    {
        if (current_health <= 0)
        {
            if(isPlayer){
                if(isOnGameOver) return true;
                isOnGameOver= true;
                EphemeralParticle deathBlood= GameObject.FindGameObjectWithTag("DeathBloodObjectPool").GetComponent<ObjectPool>().GetPooledObject().GetComponent<EphemeralParticle>();
                deathBlood.transform.position= transform.position;
                deathBlood.Emit(500, 7f);
                GameEventManager.instance.gameOver();
                GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<Animator>().SetBool("GameOver", true);
            }
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }
    IEnumerator switchScenes(int secs)
    {
        yield return new WaitForSeconds(secs);
        SceneManager.LoadScene("MainMenu");
    }
    public void onGameOver(){
        StartCoroutine(switchScenes(3));
    }
}
