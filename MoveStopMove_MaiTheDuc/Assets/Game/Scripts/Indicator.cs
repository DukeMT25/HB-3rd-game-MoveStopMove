using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : GameUnit
{
    [SerializeField] RectTransform rect;
    [SerializeField] Image iconImg;
    [SerializeField] Image directImg;
    [SerializeField] RectTransform direct;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI scoreTxt;

    [SerializeField] CanvasGroup canvasGroup;

    Transform target;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;

    Vector3 viewPoint;

    Vector2 viewPointX = new Vector2(0.075f, 0.925f);
    Vector2 viewPointY = new Vector2(0.05f, 0.85f);

    Vector2 viewPointInCameraX = new Vector2(0.075f, 0.925f);
    Vector2 viewPointInCameraY = new Vector2(0.05f, 0.95f);

    Camera Camera => CameraFollow.Instance.Camera;

    private bool IsInCamera => viewPoint.x > viewPointInCameraX.x && viewPoint.x < viewPointInCameraX.y && viewPoint.y > viewPointInCameraY.x && viewPoint.y < viewPointInCameraY.y;

    private void LateUpdate()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay))
        {
            viewPoint = Camera.WorldToViewportPoint(target.position);
            direct.gameObject.SetActive(!IsInCamera);
            nameTxt.gameObject.SetActive(IsInCamera);

            viewPoint.x = Mathf.Clamp(viewPoint.x, viewPointX.x, viewPointX.y);
            viewPoint.y = Mathf.Clamp(viewPoint.y, viewPointY.x, viewPointY.y);

            Vector3 targetSPoint = Camera.ViewportToScreenPoint(viewPoint) - screenHalf;
            Vector3 playerSPoint = Camera.WorldToScreenPoint(LevelManager.Instance.player.TF.position) - screenHalf;
            rect.anchoredPosition = targetSPoint;

            direct.up = (targetSPoint - playerSPoint).normalized;
        }
    }

    public override void OnInit()
    {
        SetScore(0);
        SetColor(new Color(Random.value, Random.value, Random.value, 1));
        SetAlpha(GameManager.Instance.IsState(GameState.Gameplay) ? 1 : 0);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        OnInit();
    }

    public void SetScore(int score)
    {
        scoreTxt.SetText(score.ToString());
    }

    public void SetName(string name)
    {
        nameTxt.SetText("");
        nameTxt.SetText(name);
    }

    private void SetColor(Color color)
    {
        iconImg.color = color;
        nameTxt.color = color;
        directImg.color = color;
    }

    public void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    public override void OnDespawn() 
    {
        SimplePool.Despawn(this);
    }
}
