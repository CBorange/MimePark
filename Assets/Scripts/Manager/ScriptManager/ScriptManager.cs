using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Assets.Scripts.Manager.ScriptManager;

public class ScriptManager : MonoBehaviour
{
    // 불러온 현재 스테이지 대화창 정보
    private delegate void DialogEndCallback();
    private DialogData dialogSequence;
    private Dictionary<string, Sprite> dialogSpriteResources;
    private Dictionary<string, DialogEndCallback> dialogEndCallbackDic;
    private DialogEndCallback[] dialogEndCallbacks;
    private int nowDialogPage = 0;
    public int nowStage;

    // Dialog 오브젝트
    public GameObject dialogUI;
    public Image dialogBG;
    public Image characterImage;
    public Text characterName;
    public Text scriptText;
    public Button dialogButton;

    // Dialog End시 Callback용 변수
    private bool beforeCallbackIsEnd = false;
    public Image gameGuideImage;
    public GameObject monsterPool;
    public VideoPlayer endingCreditPlayer;

    private void Start()
    {
        InitializeDictionary();
        LoadStageDialogData();

        if (dialogSequence.readOnSceneLoad)
            StartDialog();
    }

    // 현재 스테이지의 대화 스크립트 로드
    private void InitializeDictionary()
    {
        // 다이얼로그에서 사용할 스프라이트 리소스 로드
        dialogSpriteResources = new Dictionary<string, Sprite>();
        dialogSpriteResources.Add("PlayerImage", Resources.Load<Sprite>("Sprite/DialogUI/PlayerImage"));
        dialogSpriteResources.Add("None", Resources.Load<Sprite>("Sprite/DialogUI/None"));
        dialogSpriteResources.Add("DialogBG_L", Resources.Load<Sprite>("Sprite/DialogUI/DialogBG_L"));
        dialogSpriteResources.Add("DialogBG_R", Resources.Load<Sprite>("Sprite/DialogUI/DialogBG_R"));

        // 다이얼로그 종료 콜백 함수 캐싱
        dialogEndCallbackDic = new Dictionary<string, DialogEndCallback>();
        dialogEndCallbackDic.Add("PrintGameGuide", PrintGameGuide);
        dialogEndCallbackDic.Add("PlayEndingCredit", PlayEndingCredit);
        dialogEndCallbackDic.Add("ActiveMonster", ActiveMonster);
        dialogEndCallbackDic.Add("None", DoNothing);
    }
    private void ApplyDialogData()
    {
        Sprite loadedDialogBG;
        dialogSpriteResources.TryGetValue(dialogSequence.scriptData[nowDialogPage].dialogBG,out loadedDialogBG);
        dialogBG.sprite = loadedDialogBG;

        Sprite loadedCharacterImage;
        dialogSpriteResources.TryGetValue(dialogSequence.scriptData[nowDialogPage].characterSprite, out loadedCharacterImage);
        characterImage.sprite = loadedCharacterImage;

        characterName.text = dialogSequence.scriptData[nowDialogPage].characterName;
        scriptText.text = dialogSequence.scriptData[nowDialogPage].script;
    }
    private void LoadStageDialogData()
    {
        // 스크립트 로드
        string jsonData = File.ReadAllText($"{Application.dataPath}/StreamingAssets/DialogData/stage{nowStage}.json");
        dialogSequence = JsonUtility.FromJson<DialogData>(jsonData);

        // 종료 Callback 로드
        string[] dividedCallbacks = dialogSequence.endCallback.Split(',');
        dialogEndCallbacks = new DialogEndCallback[dividedCallbacks.Length];
        for (int i = 0; i < dividedCallbacks.Length; ++i)
        {
            DialogEndCallback loadedCallback;
            dialogEndCallbackDic.TryGetValue(dividedCallbacks[i],out loadedCallback);
            dialogEndCallbacks[i] = loadedCallback;
        }
    }

    private void EndDialog()
    {
        dialogUI.SetActive(false);
    }

    // 대화창 시작
    public void StartDialog()
    {
        dialogUI.SetActive(true);
        ApplyDialogData();
    }

    // 다음 대화창으로 이동
    public void GoToNextDialog()
    {
        nowDialogPage += 1;
        if (nowDialogPage >= dialogSequence.dialogLength)
        {
            EndDialog();
            StartCoroutine(IE_ExecuteEndCallback());
            return;
        }
        ApplyDialogData();
    }

    // 다음 다이얼로그 종료 콜백 실행
    private IEnumerator IE_ExecuteEndCallback()
    {
        for (int i = 0; i < dialogEndCallbacks.Length; ++i)
        {
            dialogEndCallbacks[i]();
            yield return new WaitUntil(() => beforeCallbackIsEnd);
            if (dialogEndCallbacks.Length < 2)
                break;
        }
    }

    // 다이얼로그 EndCallback
    #region PrintGameGuide
    private void PrintGameGuide()
    {
        beforeCallbackIsEnd = true;
        StartCoroutine(DisplayGuide());
    }

    private IEnumerator DisplayGuide()
    {
        gameGuideImage.gameObject.SetActive(true);
        //Image Fade In
        while (true)
        {
            gameGuideImage.color = new Color(
                gameGuideImage.color.r, gameGuideImage.color.g, gameGuideImage.color.b, gameGuideImage.color.a + Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (gameGuideImage.color.a >= 1)
                break;
        }
        yield return new WaitForSeconds(5.0f);

        //Image Fade Out
        while (true)
        {
            gameGuideImage.color = new Color(
                gameGuideImage.color.r, gameGuideImage.color.g, gameGuideImage.color.b, gameGuideImage.color.a - Time.deltaTime);
            yield return new WaitForEndOfFrame();
            if (gameGuideImage.color.a <= 0)
                break;
        }
        gameGuideImage.gameObject.SetActive(false);
        beforeCallbackIsEnd = true;
    }
    #endregion

    #region PlayEndingCredit
    private void PlayEndingCredit()
    {
        beforeCallbackIsEnd = false;
        StartCoroutine(PlayVideoAndWaitOver());
    }
    private IEnumerator PlayVideoAndWaitOver()
    {
        endingCreditPlayer.gameObject.SetActive(true);
        endingCreditPlayer.Play();
        yield return new WaitForSeconds(12.0f);
        beforeCallbackIsEnd = true;
        SceneManager.LoadScene(1);
    }
    #endregion

    #region ActiveMonster
    private void ActiveMonster()
    {
        beforeCallbackIsEnd = false;
        monsterPool.SetActive(true);
        beforeCallbackIsEnd = true;
    }
    #endregion

    #region DoNothing
    private void DoNothing()
    {
        beforeCallbackIsEnd = true;
    }
    #endregion
}
