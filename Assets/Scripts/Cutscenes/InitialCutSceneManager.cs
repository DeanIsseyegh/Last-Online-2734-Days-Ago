using Player;
using UnityEngine;
using UnityEngine.EventSystems;

public class InitialCutSceneManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject subscriptionPopup;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject firstWalkToPoint;
    private PlayerController _playerController;
    private CutsceneState _cutsceneState = CutsceneState.INITIAL_WALK;

    private bool _hasClickedPlayTrial;

    enum CutsceneState
    {
        INITIAL_WALK,
        SUBSCRIPTION_POPUP
    }

    private void Awake()
    {
        _playerController = player.GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerController.isControlsEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_cutsceneState == CutsceneState.INITIAL_WALK)
        {
            if (Vector2.Distance(player.transform.position, firstWalkToPoint.transform.position) > 0.1f)
            {
                _playerController.dir = Vector2.down * Time.deltaTime;
            }
            else
            {
                _playerController.dir = Vector2.zero;
                _cutsceneState = CutsceneState.SUBSCRIPTION_POPUP;
                subscriptionPopup.SetActive(true);
            }
        }

        if (_cutsceneState == CutsceneState.SUBSCRIPTION_POPUP)
        {
            if (_hasClickedPlayTrial)
            {
                subscriptionPopup.SetActive(false);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Clicked " + name);
            _hasClickedPlayTrial = true;
        }
    }
}