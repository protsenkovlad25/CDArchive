using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] private GridSpace _gameSpace;
    [SerializeField] private Unit _player;
    [SerializeField] private Transform _entitiesParent;
    [SerializeField] private Transform _activeEntitiesParent;

    public GridSpace GameSpace => _gameSpace;
    public Unit Player => _player;
    public Transform EntitiesParent => _entitiesParent;
    public Transform ActiveEntitiesParent => _activeEntitiesParent;

    public void ChangeActiveState(bool state)
    {
        gameObject.SetActive(state);
    }
}
