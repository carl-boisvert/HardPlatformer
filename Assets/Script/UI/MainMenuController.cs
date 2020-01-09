using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField]
    private GameObject _mainCanvas;

    [SerializeField]
    private Button _singlePlayerButton;

    [SerializeField]
    private Button _quitButton;

    [SerializeField]
    private Button _multiplayerButton;
    [SerializeField]
    private GameObject _multiplayerCanvas;
    [SerializeField]
    private Button _multiplayerBackButton;


    [SerializeField]
    private Button _coopButton;
    [SerializeField]
    private GameObject _coopCanvas;
    [SerializeField]
    private Button _coopBackButton;

    [SerializeField]
    private Button _optionsButton;
    [SerializeField]
    private GameObject _optionsCanvas;
    [SerializeField]
    private Button _optionsBackButton;

    [SerializeField]
    private string _playScene;

    private GameObject _currentCanvas;

    // Use this for initialization
    void Start()
    {
        _currentCanvas = _mainCanvas;
        _singlePlayerButton.onClick.AddListener(SinglePlayer);
        _multiplayerButton.onClick.AddListener(Multiplayer);
        _coopButton.onClick.AddListener(Coop);
        _optionsButton.onClick.AddListener(Options);

        _multiplayerBackButton.onClick.AddListener(BackToMainScreen);
        //_coopBackButton.onClick.AddListener(BackToMainScreen);
        _optionsBackButton.onClick.AddListener(BackToMainScreen);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SinglePlayer()
    {
        SceneManager.LoadScene(_playScene);
    }

    void Multiplayer()
    {
        SwitchScreen(_multiplayerCanvas);
    }

    void Coop()
    {
        SwitchScreen(_coopCanvas);
    }

    void Options()
    {
        SwitchScreen(_optionsCanvas);
    }

    void BackToMainScreen()
    {
        SwitchScreen(_mainCanvas);
    }

    void SwitchScreen(GameObject screen)
    {
        _currentCanvas.SetActive(false);
        _currentCanvas = screen;
        _currentCanvas.SetActive(true);
    }
}
