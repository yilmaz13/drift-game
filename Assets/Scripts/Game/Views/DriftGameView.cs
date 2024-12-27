using UnityEngine;

public class DriftGameView : MonoBehaviour
{
    #region Private Members

    private IDriftGameViewListener _listener;
    private Camera _gameCamera;
    private GameResources _gameResources;

    #endregion

    #region Public Members
    public string CurrentState { get; set; }
    public string TransitionState { get; set; }

    #endregion

    public void Initialize(IDriftGameViewListener listener, Camera gameCamera, GameResources gameResources)
    {
        _listener = listener;
        _gameCamera = gameCamera;
        _gameResources = gameResources;
    } 
  
    public void Clear()
    {       
    }    

    public void Create()
    {        
    }

    public void Show()
    {
       gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
