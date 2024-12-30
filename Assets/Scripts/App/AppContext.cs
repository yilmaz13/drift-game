using UnityEngine;
using Assets.Scripts.State;
using Assets.Scripts.Resources;
using Assets.Scripts.Game;

public class AppContext : MonoBehaviour
{
    #region Private Members   
   
    [Header("References")]
    [SerializeField] private SceneReferences _sceneReferences;
    [SerializeField] private ResourceReferences _resourceReferences;
       
    private StateManager _stateManager;
    private UserDataManager _userDataManager;

    #endregion

    #region Unity Members   

    void Start()
    {      
        _stateManager = new StateManager();
        _userDataManager = new UserDataManager();

        _stateManager.AddStates(new LoadingState(_stateManager, _sceneReferences, _resourceReferences));
        _stateManager.AddStates(new GamePlayGameState(_stateManager, _userDataManager, _sceneReferences,  _resourceReferences));
        _stateManager.AddStates(new MenuGameState(_stateManager, _userDataManager,_sceneReferences, _resourceReferences));
        _stateManager.AddStates(new CarSelectionState(_stateManager, _userDataManager, _resourceReferences, _sceneReferences));

        _stateManager.ChangeState(StateNames.Game);

        _sceneReferences.PopupManager.Initialize();

    }

    void Update()
    {
        _stateManager.GetCurrentState().UpdateState();
    }

    private void OnDestroy()
    {
        _stateManager.GetCurrentState().Deactivate();
    }
    #endregion
}
