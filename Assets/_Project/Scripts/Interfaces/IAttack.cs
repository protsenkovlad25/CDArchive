using UnityEngine;

public interface IAttack
{
    float ShootInterval {  get; set; }

    void Init(Rigidbody2D rb, Transform t);
    void Attack();
    void StartAttack();
    void StopAttack();
    void Update();
}
