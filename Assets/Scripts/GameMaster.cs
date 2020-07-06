using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    private GameMaster() { }
    public static GameMaster gamemaster = null;
    private void Awake()
    {
        if (gamemaster == null)
            gamemaster = this;
        else if (gamemaster != this)
            Destroy(gameObject);
    }

    private const int MAX_MAGAGIZE_CAPACITY = 30;
    public Image panel_magazine;

    private List<GameObject> list_image_bullet = new List<GameObject>();
    private int i_player_magazine;

    private void Start()
    {
        InitMagazie();
    }

    private void FixedUpdate()
    {
        DisplayMagazine();
    }

    private void InitMagazie()
    {
        for (int i = 0; i < panel_magazine.transform.childCount; i++)
        {
            list_image_bullet.Add(panel_magazine.transform.GetChild(i).gameObject);
            list_image_bullet[i].GetComponent<Image>().enabled = false;
        }
    }

    private void DisplayMagazine()
    {
        // 최적화가 필요하면 이 부분을 개선할것
        // fixedupdate가 아니라 이벤트전달식으로 최소한으로 줄일것
        i_player_magazine = GunMaster.gunmaster.gMagCap();
        InitMagazie();

        for (int i = 0; i < i_player_magazine; i++)
        {
            list_image_bullet[i].GetComponent<Image>().enabled = true;
        }
    }
}
