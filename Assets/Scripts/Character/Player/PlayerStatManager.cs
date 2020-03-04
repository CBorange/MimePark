using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatManager : MonoBehaviour
{
    #region Stat
    public bool IsAlive = true;
    public bool Shileld = false;
    #region HP Related
        public float HP;
        public int RunningHPIndex = 4;
        public int NowHPImageStage = 2;
        private Sprite[] OriginalHPImage;
        public Image[] HPImage;
    #endregion
    #endregion
    private PlayerAnimCntrl AnimCntrl;

    private void Start()
    {
        Caching();
    }
    private void Caching()
    {
        AnimCntrl = GetComponent<PlayerAnimCntrl>();
        OriginalHPImage = new Sprite[3];
        OriginalHPImage[0] = Resources.Load<Sprite>("Sprite/HP/HP_Empty");
        OriginalHPImage[1] = Resources.Load<Sprite>("Sprite/HP/HP_Half");
        OriginalHPImage[2] = Resources.Load<Sprite>("Sprite/HP/HP_Full");
    }
    public void GetHit(float Damage)
    {
        if (!IsAlive || Shileld)
            return;
        HP -= Damage;
        ChangeHearthByHit();
        if (HP <= 0)
        {
            IsAlive = false;
            AnimCntrl.DeadAnim();
            //gameObject.SetActive(false);
        }
        StartCoroutine(ProcessShiledActivate());
    }
    public void GetHeal(float heal)
    {
        if (!IsAlive || HP >= 5)
        {
            return;
        }
        ChangeHearthByHeal(heal);
    }
    private IEnumerator ProcessShiledActivate()
    {
        Shileld = true;
        yield return new WaitForSeconds(0.5f);
        Shileld = false;
    }
    private void ChangeHearthByHit()
    {
        NowHPImageStage -= 1;
        HPImage[RunningHPIndex].sprite = OriginalHPImage[NowHPImageStage];
        if (NowHPImageStage == 0)
        {
            RunningHPIndex -= 1;
            NowHPImageStage = 2;
        }
    }
    private void ChangeHearthByHeal(float heal)
    {
        for (int i = 0; i < 4; ++i)
        {
            if (NowHPImageStage == 2)
            {
                RunningHPIndex += 1;
                NowHPImageStage = 0;
            }
            NowHPImageStage += 1;
            HP += 0.5f;
            HPImage[RunningHPIndex].sprite = OriginalHPImage[NowHPImageStage];
            if (HP > 4.5f)
                break;
        }
    }
}
