using Player;
using UnityEngine;

public class InitialCutSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject firstWalkToPoint;
    private PlayerController _playerController;
    private CutsceneState _cutsceneState = CutsceneState.INITIAL_WALK;

    enum CutsceneState
    {
        INITIAL_WALK
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
            if (Vector2.Distance(player.transform.position, firstWalkToPoint.transform.position) <= 0.1f)
            {
                _playerController.dir = Vector2.down * Time.deltaTime;
            }
            else
            {
                
            }
        }
    }
}