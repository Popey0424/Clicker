using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using static UnityEditor.PlayerSettings;
using System.Threading;

public class MonsterUI : MonoBehaviour
{
    public int Life;
    public TextMeshProUGUI TextLife;
    public int _lifeMax;
    public Image ImageLifeFilll;
    private SpriteRenderer _spriteRenderer;
    public int Coins = 0;
    public Canvas Canvas;

    public TextMeshProUGUI enemyname;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void UpdateLife()
    {
        TextLife.text = $"{Life}/{_lifeMax}";

        float percent = (float)Life / (float)_lifeMax;
        ImageLifeFilll.fillAmount = percent;

    }

    public void Hit(int damage)
    {
        Life -= damage;
        UpdateLife();
        transform.DOComplete();
        transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.3f);
    }



    public void SetMonster(MonsterInfos infos)
    {
        _lifeMax = infos.Life;
        Life = _lifeMax;
        Coins = infos.coins;

        enemyname.text = infos.enemyname.ToString();
        UpdateLife();

        _spriteRenderer.sprite = infos.sprite;
    }

    public bool IsAlive()
    {
        return Life > 0;
    }



}
