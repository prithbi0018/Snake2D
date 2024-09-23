
using UnityEngine;


public class GameHandler : MonoBehaviour {

    private void Start() {
        Debug.Log("GameHandler.Start");

        GameObject snakeHeadGameObject = new GameObject();
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.i.snakeHeadSprite;
    }

}
