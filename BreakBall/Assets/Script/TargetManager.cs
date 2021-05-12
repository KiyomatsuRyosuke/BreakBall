using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour
{
    private int TargetMax = 2;
    public int NowTargetNum = 0;
    private int ResetCount = 0;

    private int threeUp = 25;
    private int fourUp = 50;

    private float waitCount = 0;
    private const float waitTime = 0.3f;

    int start = 0;
    int end = 8;

    static private Vector3 DownLeft = new Vector3(-10.5f, 5.5f, 18);
    static private Vector3 DownCenter = new Vector3(-0.5f, 5.5f, 18);
    static private Vector3 DownRight = new Vector3(9.5f, 5.5f, 18);
    static private Vector3 MiddleLeft = new Vector3(-10.5f, 15.3f, 18);
    static private Vector3 MiddleCenter = new Vector3(-0.5f, 15.3f, 18);
    static private Vector3 MiddleRight = new Vector3(9.5f, 15.3f, 18);
    static private Vector3 UpLeft = new Vector3(-10.5f, 25.1f, 18);
    static private Vector3 UpCenter = new Vector3(-0.5f, 25.1f, 18);
    static private Vector3 UpRIght = new Vector3(9.5f, 25.1f, 18);

    Vector3[] place = { DownLeft, DownCenter, DownRight, MiddleLeft, MiddleCenter, MiddleRight, UpLeft, UpCenter, UpRIght };

    public GameObject TargetObject;

    // ��������.
    public Text timeText;                        //���Ԃ�\������Text�^�̕ϐ�.
    public float countdown = 8.0f;             //�c�莞��.
    private const float TimeLimit = 3.0f;    //�^�C�����~�b�g.
    public bool TimeOverFrag = false;       //�^�C���I�[�o�[�t���O.
    public GameObject timeOverText;      //�^�C���I�[�o�[�e�L�X�g.

    public GameObject RetryButton;        // Retry�{�^��.
    public GameObject TitleButton;         // Go Title�{�^��.

    // �T�E���h
    public AudioClip sound1;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //�T�E���h��Component���擾
        audioSource = GetComponent<AudioSource>();

        // �^�C���I�[�o�[�e�L�X�g
        timeOverText.SetActive(false);
        // Retry�{�^��
        RetryButton.SetActive(false);
        // Go Title�{�^��
        TitleButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeOverFrag == false)
        {
            if (ResetCount > threeUp)
            {
                TargetMax = 3;
                if (ResetCount > fourUp)
                {
                    TargetMax = 4;
                }
            }
            if (NowTargetNum == 0)
            {
                if (waitCount <= 0)
                {
                    //��(sound1)��炷
                    audioSource.PlayOneShot(sound1);

                    List<int> numbers = new List<int>();
                    for (int j = start; j <= end; j++)
                    {
                        numbers.Add(j);
                    }
                    for (int i = 0; i < TargetMax; i++)
                    {
                        int index = Random.Range(0, numbers.Count);
                        int ransu = numbers[index];

                        GameObject target = Instantiate(TargetObject);
                        target.transform.position = place[ransu];
                        Debug.Log(ransu);

                        numbers.RemoveAt(index);
                        NowTargetNum++;
                    }
                }
                else
                {
                    waitCount -= Time.deltaTime;
                }

            }

            //////////////////////////////////////////////////
            // ��������.
            //////////////////////////////////////////////////
            //���Ԃ��J�E���g�_�E������
            countdown -= Time.deltaTime;
            //���Ԃ�\������
            timeText.text = string.Format("Time:{0}", countdown);
            //countdown��0�ȉ��ɂȂ����Ƃ�
            if (countdown <= 0)
            {
                timeText.text = "Time:0.0000";
                TimeOverFrag = true;

                StartCoroutine("TimeOver");
            }
        }
    }

    public void Hit()
    {
        NowTargetNum--;
        if(NowTargetNum == 0)
        {
            waitCount = waitTime;
            countdown += TimeLimit;
            ResetCount++;
        }
    }

    IEnumerator TimeOver()
    {
        timeOverText.SetActive(true);
        RetryButton.SetActive(true);
        TitleButton.SetActive(true);
        yield return new WaitForSeconds(2.0f);
    }
}
