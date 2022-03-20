using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InitialCutSceneManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject subscriptionPopup;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject firstWalkToPoint;
    [SerializeField] private GameObject friendsIcon;
    [SerializeField] private GameObject friendsArea;
    [SerializeField] private Dialogue legendaryWarriorFirstDialogue;

    private PlayerController _playerController;
    private CutsceneState _cutsceneState = CutsceneState.INITIAL_WALK;
    private float initialWalkTimeSinceStarted = 0;

    private bool _hasClickedPlayTrial;
    private bool _hasClickedFriendsIcon;
    private bool _hasClickedOnlineFriend;
    private bool _canPlayerMove;

    enum CutsceneState
    {
        INITIAL_WALK,
        SUBSCRIPTION_POPUP,
        FRIENDS_ICON_NOTIFICATION_FLASH,
        ON_FRIENDS_LIST,
        DIALOGUE_STARTED
    }

    private void Awake()
    {
        _playerController = player.GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerController.isControlsEnabled = false;
        PlayerPrefs.SetInt("HasStarted", 1);
    }

    // Update is called once per frame
    void Update()
    {
        // if (PlayerPrefs.GetInt("HasStarted") )
        if (_cutsceneState == CutsceneState.INITIAL_WALK)
        {
            initialWalkTimeSinceStarted += Time.deltaTime;
            if (Vector2.Distance(player.transform.position, firstWalkToPoint.transform.position) > 0.1f ||
                initialWalkTimeSinceStarted >= 1.5f)
            {
                _playerController.dir = Vector2.down * Time.deltaTime;
            }
            else
            {
                _playerController.dir = Vector2.zero;
                subscriptionPopup.SetActive(true);
                
                _cutsceneState = CutsceneState.SUBSCRIPTION_POPUP;
            }
        }

        if (_cutsceneState == CutsceneState.SUBSCRIPTION_POPUP)
        {
            if (_hasClickedPlayTrial)
            {
                subscriptionPopup.SetActive(false);
                Transform notification = friendsIcon.transform.GetChild(1);
                notification.gameObject.SetActive(true);
                friendsIcon.GetComponentInChildren<Button>().enabled = true;

                _cutsceneState = CutsceneState.FRIENDS_ICON_NOTIFICATION_FLASH;
            }
        }

        if (_cutsceneState == CutsceneState.FRIENDS_ICON_NOTIFICATION_FLASH)
        {
            if (_hasClickedFriendsIcon)
            {
                friendsIcon.GetComponentInChildren<Button>().enabled = false;
                Transform notification = friendsIcon.transform.GetChild(1);
                notification.gameObject.SetActive(false);

                friendsArea.SetActive(true);

                _cutsceneState = CutsceneState.ON_FRIENDS_LIST;
            }
        }

        if (_cutsceneState == CutsceneState.ON_FRIENDS_LIST)
        {
            if (_hasClickedOnlineFriend)
            {
                friendsArea.SetActive(false);
                legendaryWarriorFirstDialogue.StartConvo();
                _cutsceneState = CutsceneState.DIALOGUE_STARTED;
            }
        }

        if (_cutsceneState == CutsceneState.DIALOGUE_STARTED)
        {
            //Do nothing, wait for void OnDialogueEnd() to be called
        }
    }

    public void OnDialogueEnd()
    {
        _canPlayerMove = true;
    }
    
    public void OnOnlineFriendClick()
    {
        _hasClickedOnlineFriend = true;
    }
    public void OnFriendsIconClick()
    {
        _hasClickedFriendsIcon = true;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _hasClickedPlayTrial = true;
        }
    }
}