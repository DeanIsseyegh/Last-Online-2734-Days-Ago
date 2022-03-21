using DialogueEditor;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialCutSceneManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject subscriptionPopup;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject firstWalkToPoint;
    [SerializeField] private GameObject friendsIcon;
    [SerializeField] private GameObject friendsArea;
    [SerializeField] private Dialogue legendaryWarriorFirstDialogue;
    [SerializeField] private Dialogue legendaryWarriorEndingDialogue;
    [SerializeField] private GameObject transitionToFish;
    [SerializeField] private GameObject northPosition;
    [SerializeField] private GameObject eastPosition;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip finalDialogueMusic;
    [SerializeField] private GameObject legendaryWarrior;
    [SerializeField] private float legendaryWarriorSpeed = 3f;
    [SerializeField] private GameObject endPopup;
    [SerializeField] private GameObject dialogueInstructions;

    private PlayerController _playerController;
    private CutsceneState _cutsceneState = CutsceneState.INITIAL_WALK;
    private float initialWalkTimeSinceStarted = 0;

    private bool _hasClickedPlayTrial;
    private bool _hasClickedFriendsIcon;
    private bool _hasClickedOnlineFriend;
    private bool _canPlayerMove;
    private int _playerPrefsHasStarted;
    private bool _hasFinalDialogueStarted;
    private bool _shouldPlayInitalCutscene;

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
        _playerPrefsHasStarted = PlayerPrefs.GetInt("HasStarted", 0);

        if (_playerPrefsHasStarted == 0)
        {
            PlayerPrefs.SetInt("HasStarted", 1);
            _playerController.isControlsEnabled = false;
            _shouldPlayInitalCutscene = true;
            _cutsceneState = CutsceneState.INITIAL_WALK;
        }
        else
        {
            transitionToFish.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("MainHubExitPos").Equals("North"))
        {
            _playerController.gameObject.transform.position = northPosition.transform.position;
        } else if (PlayerPrefs.GetString("MainHubExitPos").Equals("East"))
        {
            _playerController.gameObject.transform.position = eastPosition.transform.position;
        }
        PlayerPrefs.SetString("MainHubExitPos", "");


        if (PlayerPrefs.GetInt("HasWonFishing") >= 1 && PlayerPrefs.GetInt("HasWonCombat") >= 1 && PlayerPrefs.GetInt("HasStarted") >= 1)
        {
            if (!_hasFinalDialogueStarted)
            {
                legendaryWarrior.SetActive(true);
                legendaryWarrior.transform.position += (Vector3) Vector2.MoveTowards(legendaryWarrior.transform.position, player.transform.position, 100).normalized * Time.deltaTime * legendaryWarriorSpeed;
                if (Vector2.Distance(player.transform.position, legendaryWarrior.transform.position) <= 2f)
                {
                    _hasFinalDialogueStarted = true;
                    legendaryWarriorEndingDialogue.StartConvo();
                    ConversationManager.OnConversationEnded += EndGame;
                    _playerController.isControlsEnabled = false;
                    _playerController.dir = Vector2.zero;
                    audioSource.clip = finalDialogueMusic;
                    audioSource.Play();
                }
            }
        }

        if (!_shouldPlayInitalCutscene) return;

        if (_cutsceneState == CutsceneState.INITIAL_WALK)
        {
            initialWalkTimeSinceStarted += Time.deltaTime;
            if (Vector2.Distance(player.transform.position, firstWalkToPoint.transform.position) > 0.1f ||
                initialWalkTimeSinceStarted >= 1.5f)
            {
                _playerController.dir = Vector2.right * Time.deltaTime;
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
                dialogueInstructions.SetActive(true);
                legendaryWarriorFirstDialogue.StartConvo();
                _cutsceneState = CutsceneState.DIALOGUE_STARTED;
            }
        }

        if (_cutsceneState == CutsceneState.DIALOGUE_STARTED)
        {
            transitionToFish.SetActive(true);
            ConversationManager.OnConversationEnded += EnablePlayerControls;
        }
    }
    
    
    private void EndGame()
    {
        endPopup.SetActive(true);
        Destroy(player);
        Destroy(legendaryWarrior);
        ConversationManager.OnConversationEnded -= EndGame;
    }
    
    
    private void EnablePlayerControls()
    {
        _canPlayerMove = true;
        _playerController.isControlsEnabled = true;
        ConversationManager.OnConversationEnded -= EnablePlayerControls;
        dialogueInstructions.SetActive(false);
    }

    public void OnDialogueEnd()
    {
        _canPlayerMove = true;
        _playerController.isControlsEnabled = true;
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