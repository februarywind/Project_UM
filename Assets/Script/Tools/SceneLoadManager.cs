using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneName
{
    Title, Vilige, Boss
}
public class SceneLoadManager : Singleton<SceneLoadManager>
{
    [SerializeField] Image loadingPanel;
    [SerializeField] float sceneMindelay = 3f;
    [SerializeField] TMP_Text loadingText;

    private Coroutine sceneCoroutine;
    private Coroutine textCoroutine;

    private string[] viligeLoadText = { "마을로 이동하는 중", "마을로 이동하는 중.", "마을로 이동하는 중..", "마을로 이동하는 중..." };
    private string[] bossLoadText = { "던전으로 이동하는 중", "던전으로 이동하는 중.", "던전으로 이동하는 중..", "던전으로 이동하는 중..." };

    public void MoveScene(SceneName scene)
    {
        if (sceneCoroutine != null)
        {
            Debug.Log("이미 씬을 로드하는 중");
            return;
        }
        sceneCoroutine = StartCoroutine(LoadSceneAsync(scene));
    }
    IEnumerator LoadSceneAsync(SceneName scene)
    {
        textCoroutine = StartCoroutine(LoadingTextChange(scene));
        loadingPanel.gameObject.SetActive(true);
        loadingPanel.DOColor(Color.black, 1f);
        float delay = 0;

        // 비동기로 씬을 로드하고 준비되어도 바로 변경하지 않는다.
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)scene);
        asyncOperation.allowSceneActivation = false;

        // 최소 딜레이 시간이 지나고 씬도 로딩되었다면 변경
        while (sceneMindelay > delay || asyncOperation.progress < 0.9f)
        {
            delay += Time.deltaTime;
            yield return null;
        }

        StopCoroutine(textCoroutine);
        asyncOperation.allowSceneActivation = true;

        loadingPanel.DOColor(new Vector4(0, 0, 0, 0), 1f);
        loadingText.DOColor(new Vector4(1, 1, 1, 0), 1f);
        yield return Util.GetDelay(1f);

        loadingPanel.gameObject.SetActive(false);
        sceneCoroutine = null;
    }
    IEnumerator LoadingTextChange(SceneName scene)
    {
        int i = 0;
        string[] loadText = scene == SceneName.Vilige ? viligeLoadText : bossLoadText;
        loadingText.color = Color.white;
        while (true)
        {
            if (i == loadText.Length)
            {
                i = 0;
            }
            loadingText.text = loadText[i++];
            yield return Util.GetDelay(0.3f);
        }
    }
}
