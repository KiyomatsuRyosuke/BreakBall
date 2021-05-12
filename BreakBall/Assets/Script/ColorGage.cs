using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Image系はUIなので追加
using UnityEngine.UI;

public class ColorGage : MonoBehaviour
{
    // ターゲットマネージャー.
    GameObject manager;
    TargetManager script;

    public float _powerGage = 0;
    public bool PowerUpFrag = true;

    private bool JumpFrag = false;
    private float JumpCount = 0;

    private float JumpBackTime = 0.5f;      // ジャンプしてから戻るまでの時間.
    private float maxLine = 0.6f;           // ジャンプの最大値

    //Image型の変数_imageを宣言しておく
    public Image _image;

    // Start is called before the first frame update
    void Start()
    {
        //thisいらないけど、自分のだよ、ということで説明のために
        _image = this.GetComponent<Image>();

        // ターゲットマネージャー.
        manager = GameObject.Find("TargetManager");
        script = manager.GetComponent<TargetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (script.TimeOverFrag == false)
        {
            // プレイヤーがジャンプ中じゃないなら
            if (JumpFrag == false)
            {
                // ジャンプボタンを離したらジャンプ力決定.
                if (Input.GetKey("space"))
                {
                    if (PowerUpFrag == true)
                    {
                        _powerGage += Time.deltaTime;
                        if (_powerGage >= maxLine)
                        {
                            PowerUpFrag = false;
                        }
                    }
                    else
                    {
                        _powerGage -= Time.deltaTime;
                        if (_powerGage <= 0)
                        {
                            PowerUpFrag = true;
                        }
                    }
                }
                if (Input.GetKeyUp("space"))
                {
                    JumpFrag = true;
                    _powerGage = 0;
                }
            }
            else
            {
                JumpCount += Time.deltaTime;
                if (JumpCount > JumpBackTime)
                {
                    JumpCount = 0;
                    JumpFrag = false;
                }
            }

            //最大が500なので、割り算して比率で突っ込む
            _image.fillAmount = _powerGage / maxLine;
        }
    }
}
